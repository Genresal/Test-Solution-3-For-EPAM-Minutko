using EMR.Business.Models;
using EMR.Business.Services;

namespace EMR.Services
{
    public class HomePageService : IHomePageService
    {
        IDbService _dbService;

        public HomePageService(IDbService s)
        {
            _dbService = s;
        }
        public DbStatus GetDbStatus()
        {
            return _dbService.GetDbStatus();
        }

        public void CreateDefaultDate()
        {
            _dbService.CreateDefaultDate();
        }

        public void DropTables()
        {
            _dbService.DropTables();
        }

    }
}
