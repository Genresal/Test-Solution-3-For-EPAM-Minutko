using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class ProcedureService : BaseBusinessService<Procedure>, IProcedureService
    {
        private readonly IRepository<RecordTreatment> _treatmentRepository;
        public ProcedureService(IRepository<Procedure> r, IRepository<RecordTreatment> treatmentRepository) : base (r)
        {
            _treatmentRepository = treatmentRepository;
        }

        public IEnumerable<Procedure> GetProceduresForRecord(int RecordId)
        {
            var relations = _treatmentRepository.GetByColumn(nameof(RecordTreatment.RecordId), RecordId.ToString());
            if (!relations.Any())
            {
                return new List<Procedure>();
            }
            return _mainRepository.GetByColumn("Id", relations.Select(x => x.ProcedureId.ToString()).ToList());
        }
    }
}
