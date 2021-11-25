﻿using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Mapper;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
