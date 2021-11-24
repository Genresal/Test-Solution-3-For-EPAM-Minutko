﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Business.Services
{
    public interface IDbService
    {
        public void CheckDb();
        public void CreateDb();
        public void DropDb();
        public void CreateTables();
        public void DropTables();
    }
}
