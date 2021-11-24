using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Repositories
{
    public interface IDbRepository
    {
        public void CreateDb();
        public void DropDb();
        public void CreateTables();
        bool IsDbExist();
    }
}
