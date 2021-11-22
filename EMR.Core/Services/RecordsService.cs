using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class RecordService : BaseService<Record>, IRecordService
    {
        IRepository<Doctor> _doctorsRepository;
        IRepository<Patient> _patientsRepository;

        public RecordService(IRepository<Record> r
                        , IRepository<Doctor> rr
                        , IRepository<Patient> rrr) : base (r)
        {
            _doctorsRepository = rr;
            _patientsRepository = rrr;
        }
    }
}
