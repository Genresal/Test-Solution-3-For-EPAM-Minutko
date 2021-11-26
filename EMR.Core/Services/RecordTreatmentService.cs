using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class RecordTreatmentService : BaseBusinessService<RecordTreatment>, ITreatmentService
    {
        IRepository<Drug> _drugRepository;
        IRepository<Procedure> _procedureRepository;
        public RecordTreatmentService(IRepository<RecordTreatment> r, IRepository<Drug> d, IRepository<Procedure> p) : base (r)
        {
            _drugRepository = d;
            _procedureRepository = p;
        }

        public IEnumerable<Drug> GetAllDrugs(int RecordId)
        {
            var relations = _mainRepository.GetByColumn("RecordId", RecordId.ToString());
            if (!relations.Any())
            {
                return new List<Drug>();
            }
            return _drugRepository.GetByColumn("Id", relations.Select(x => x.DrugId.ToString()).ToList());
        }

        public IEnumerable<Procedure> GetAllProcedures(int RecordId)
        {
            var relations = _mainRepository.GetByColumn("RecordId", RecordId.ToString());
            if (!relations.Any())
            {
                return new List<Procedure>();
            }
            return _procedureRepository.GetByColumn("Id", relations.Select(x => x.ProcedureId.ToString()).ToList());
        }
    }
}
