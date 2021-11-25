﻿using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class DoctorService : BaseBusinessService<Doctor>
    {
        public DoctorService(IRepository<Doctor> r) : base (r)
        {
        }
    }
}
