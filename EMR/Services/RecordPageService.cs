﻿using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using EMR.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EMR.Services
{
    public class RecordPageService : BasePageService<Record, RecordViewModel>, IRecordPageService
    {
        private readonly IRecordService _recordService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IPositionService _positionService;

        public RecordPageService(IRecordService recordService
            , IDoctorService doctorService
            , IPatientService patientService
            , IPositionService positionService
            , IMapper mapper) : base(recordService, mapper)
        {
            _recordService = recordService;
            _doctorService = doctorService;
            _patientService = patientService;
            _positionService = positionService;
        }

        public IEnumerable<Position> GetDoctorPositions()
        {
            return _positionService.GetAll();
        }

        public IEnumerable<RecordViewModel> LoadTable(RecordSearchModel searchParameters)
        {
            IEnumerable<Record> rawResult;

            if (searchParameters.PatientId > 0)
            {
                rawResult = _recordService.GetByColumn(nameof(searchParameters.PatientId), searchParameters.PatientId.ToString());
            }
            else if (searchParameters.DoctorId > 0)
            {
                rawResult = _recordService.GetByColumn(nameof(searchParameters.DoctorId), searchParameters.DoctorId.ToString());
            }
            else
            {
                rawResult = _recordService.GetAll();
            }

            var result = _mapper.Map<IEnumerable<Record>, IEnumerable<RecordViewModel>>(rawResult);

            if (searchParameters.DoctorPositions.Count > 0)
            {
                var filterParams = searchParameters.DoctorPositions.Select(r => r.Id);
                result = result.Where(r => filterParams.Contains(r.DoctorPositionId));
            }

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            if (!string.IsNullOrEmpty(searchParameters.DateRange.Start))
            {
                startDate = DateTime.Parse(searchParameters.DateRange.Start);
            }

            if (!string.IsNullOrEmpty(searchParameters.DateRange.End))
            {
                endDate = DateTime.Parse(searchParameters.DateRange.End);
            }

            if (startDate != DateTime.MinValue)
            {
                result = result.Where(u => u.ModifiedDate >= startDate);
            }

            if (endDate != DateTime.MinValue)
            {
                result = result.Where(u => u.ModifiedDate <= endDate);
            }

            if (!string.IsNullOrEmpty(searchParameters.Diagnosis))
            {
                result = result.Where(r => r.Diagnosis != null && r.Diagnosis.ToString().ToUpper().Contains(searchParameters.Diagnosis.ToUpper()));
            }

            var searchBy = searchParameters.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.PatientName != null && r.PatientName.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                r.DoctorName != null && r.DoctorName.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                r.Diagnosis != null && r.Diagnosis.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            return result.Order(searchParameters);
        }

        public RecordDetailsViewModel Details(int id)
        {
            Record rawResult;
            if(id == 0)
            {
                rawResult = _recordService.GetLast();
            }
            else
            {
                rawResult = _recordService.GetById(id);
            }
            
            var result = _mapper.Map<Record, RecordDetailsViewModel>(rawResult);
            result.SickLeave.RecordId = result.Id;

            return result;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            var result = _doctorService.GetAll();
            return result;
        }

        public IEnumerable<Patient> GetPatients()
        {
            var result = _patientService.GetAll();
            return result;
        }

        public Patient GetPatient(int id)
        {
            var result = _patientService.GetById(id);
            return result;
        }

        public List<SelectListItem> PrepareDoctors()
        {
            var rawResult = _doctorService.GetAll();
            var result = _mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorViewModel>>(rawResult);

            List<SelectListItem> positions = new List<SelectListItem>();
            positions.AddRange(result
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = "Dr. " + x.FirstName + " " + x.LastName }).ToList());

            return positions;
        }

        public List<SelectListItem> PreparePatients()
        {
            var rawResult = _patientService.GetAll();
            var result = _mapper.Map<IEnumerable<Patient>, IEnumerable<PatientViewModel>>(rawResult);

            List<SelectListItem> positions = new List<SelectListItem>();
            positions.AddRange(result
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.FirstName + " " + x.LastName }).ToList());

            return positions;
        }
    }
}
