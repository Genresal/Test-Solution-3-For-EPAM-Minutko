using EMR.Helpers;
using EMR.Models;
using EMR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EMR.Controllers
{
    public class ImagesController : Controller
    {
        private readonly AzureStorageConfig _storageConfig;
        private readonly IUserPageService _userPageService;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IOptions<AzureStorageConfig> config,
                            IUserPageService userPageService,
                            ILogger<ImagesController> logger)
        {
            _storageConfig = config.Value;
            _userPageService = userPageService;
            _logger = logger;
        }

        public IActionResult Index(int userId)
        {
            return View(userId);
        }

        // POST /api/images/upload
        [HttpPost("api/[controller]/[action]")]
        public async Task<IActionResult> Upload([FromForm] int userId, ICollection<IFormFile> files)
        {
            bool isUploaded = false;

            try
            {
                if (files.Count == 0)
                    return BadRequest("No files received from the upload");

                if (_storageConfig.AccountKey == string.Empty || _storageConfig.AccountName == string.Empty)
                    return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (_storageConfig.ImageContainer == string.Empty)
                    return BadRequest("Please provide a name for your image container in the azure blob storage");

                foreach (var formFile in files)
                {
                    if (StorageHelper.IsImage(formFile))
                    {
                        if (formFile.Length > 0)
                        {
                            using (Stream stream = formFile.OpenReadStream())
                            {
                                isUploaded = await StorageHelper.UploadFileToStorage(stream, userId.ToString(), _storageConfig);
                                string blobUri = "https://" +
                                            _storageConfig.AccountName +
                                            ".blob.core.windows.net/" +
                                            _storageConfig.ImageContainer +
                                            "/" + userId.ToString();

                                _userPageService.SetPhotoUrl(userId, blobUri);
                                _logger.LogWarning($"{User.Identity.Name} loaded new photo for userId = {userId}.");
                            }
                        }
                    }
                    else
                    {
                        return new UnsupportedMediaTypeResult();
                    }
                }

                if (isUploaded)
                {
                    return RedirectToAction("Details", "Users", new { id = userId });
                }
                else
                    return BadRequest("Look like the image couldnt upload to the storage");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}