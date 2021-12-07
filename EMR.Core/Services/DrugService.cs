using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class DrugService : BaseBusinessService<Drug>, IDrugService
    {
        private readonly IRepository<RecordTreatment> _treatmentRepository;
        public DrugService(IRepository<Drug> r, IRepository<RecordTreatment> drugRepository) : base (r)
        {
            _treatmentRepository = drugRepository;
        }

        public IEnumerable<Drug> GetDrugsForRecord(int RecordId)
        {
            var relations = _treatmentRepository.GetByColumn(nameof(RecordTreatment.RecordId), RecordId.ToString());
            if (!relations.Any())
            {
                return new List<Drug>();
            }

            return _mainRepository.GetByColumn("Id", relations.Select(x => x.DrugId.ToString()).ToList());
        }

        public void Create(Drug model, int recordId)
        {
            _mainRepository.Create(model, recordId);
        }
    }
}