using ERM.Models;
using ERM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERM.Services
{
    public interface IService
    {
        IQueryable<RecordViewModel> LoadTable(RecordSearchModel searchParameters);
    }
}