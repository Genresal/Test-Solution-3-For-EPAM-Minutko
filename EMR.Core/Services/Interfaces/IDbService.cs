using EMR.Business.Models;

namespace EMR.Business.Services
{
    public interface IDbService
    {
        public DbStatus GetDbStatus();
        public void CreateDefaultDate();
        public void DropTables();
    }
}
