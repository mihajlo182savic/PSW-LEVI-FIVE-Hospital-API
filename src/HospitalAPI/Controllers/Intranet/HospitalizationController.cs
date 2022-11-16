﻿using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Hospitalizations.Dtos;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.MedicalRecords.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [ApiController]
    [Route("intranet/hospitalization")]
    public class HospitalizationController: ControllerBase
    {
        private IHospitalizationService _hospitalizationService;
        private IMedicalRecordService _medicalRecordService;

        public HospitalizationController(IHospitalizationService hospitalizationService, IMedicalRecordService medicalRecordService)
        {
            _hospitalizationService = hospitalizationService;
            _medicalRecordService = medicalRecordService;
        }

        [HttpPost]
        public IActionResult CreateHospitalization([FromBody] CreateHospitalizationDTO createHospitalizationDTO )
        {
            MedicalRecord medicalRecord = _medicalRecordService.CreateOrGet(createHospitalizationDTO.PatientId);
            createHospitalizationDTO.MedicalRecordId = medicalRecord.Id;
            Hospitalization hospitalization = _hospitalizationService.Create(createHospitalizationDTO.MapToModel());
            return Ok(hospitalization);
        }

        [HttpPatch]
        [Route("/end/{id:int}")]
        public IActionResult EndHospitalization([FromBody] EndHospitalizationDTO endHospitalizationDto, int id)
        {
            Hospitalization hospitalization = _hospitalizationService.EndHospitalization(id, endHospitalizationDto);
            return Ok(hospitalization);
        }
    }
}