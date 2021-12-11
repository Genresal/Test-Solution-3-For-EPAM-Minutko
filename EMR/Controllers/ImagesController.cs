using EMR.Helpers;
using EMR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ImagesController(IOptions<AzureStorageConfig> config)
        {
            _storageConfig = config.Value;
        }

        public IActionResult Index(int userId)
        {
            return View(userId);
        }

        public IActionResult Delete()
        {
            _ = StorageHelper.DeleteFileFromStorage("104", _storageConfig);

            return View("Index");
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