using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;

namespace EMR.Business.Services
{
    public class RecordService : BaseBusinessService<Record>, IRecordService
    {
        private readonly IRepository<Diagnosis> _diagnosisRepository;
        public RecordService(IRepository<Record> recordRepository, IRepository<Diagnosis> diagnosisRepository, ILogger<RecordService> logger) : base(recordRepository, logger)
        {
            _diagnosisRepository = diagnosisRepository;
        }

        public Record GetLast()
        {
            int lastId = _mainRepository.GetLastId();
            return _mainRepository.GetById(lastId);
        }

        public override void Update(Record model)
        {
            _diagnosisRepository.Update(model.Diagnosis);
            base.Update(model);
        }

    }
}
