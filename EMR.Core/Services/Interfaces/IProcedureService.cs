﻿using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    /// <summary>
    /// Base repository
    /// </summary>
    public interface IProcedureService : IBusinessService<Procedure>
    {
        IEnumerable<Procedure> GetProceduresForRecord(int RecordId);
    }


}