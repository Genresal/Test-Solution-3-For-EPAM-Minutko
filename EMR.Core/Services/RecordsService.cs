﻿using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace EMR.Business.Services
{
    public class RecordService : BaseBusinessService<Record>, IBusinessService<Record>
    {
        private readonly IRepository<Diagnosis> _diagnosisRepository;
        private readonly ILogger<RecordService> _logger;
        public RecordService(IRepository<Record> recordRepository, IRepository<Diagnosis> diagnosisRepository, ILogger<RecordService> logger) : base (recordRepository)
        {
            _diagnosisRepository = diagnosisRepository;
            _logger = logger;
        }

        public override void Update(Record model)
        {
            _diagnosisRepository.Update(model.Diagnosis);
            base.Update(model);
        }
    }
}
