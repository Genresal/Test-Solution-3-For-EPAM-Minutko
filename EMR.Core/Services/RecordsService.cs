﻿using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class RecordService : BaseService<Record>, IRecordService
    {
        public RecordService(IRepository<Record> r) : base (r)
        {
        }
    }
}
