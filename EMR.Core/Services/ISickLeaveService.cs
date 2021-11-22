using EMR.Business.Models;
using System.Linq;

namespace EMR.Business.Services
{
    public interface ISickLeaveService
    {
        SickLeave FindById(int id);
        void Create(SickLeave model);
        void Update(SickLeave Model);
        void Delete(int id);
    }
}