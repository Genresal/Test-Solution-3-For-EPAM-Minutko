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
        public void CreateDb()
        {
            _dbService.CreateDb();
        }
        public void DropDb()
        {
            _dbService.DropDb();
        }
        public void CreateTables()
        {
            _dbService.CreateTables();
        }
        public void DropTables()
        {
            _dbService.DropTables();
        }

    }
}
