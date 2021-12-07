using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class RecordService : BaseBusinessService<Record>, IBusinessService<Record>
    {
        private readonly IRepository<Diagnosis> _diagnosisRepository;
        public RecordService(IRepository<Record> recordRepository, IRepository<Diagnosis> diagnosisRepository) : base (recordRepository)
        {
            _diagnosisRepository = diagnosisRepository;
        }

        public override void Update(Record model)
        {
            _diagnosisRepository.Update(model.Diagnosis);
            base.Update(model);
        }
    }
}
