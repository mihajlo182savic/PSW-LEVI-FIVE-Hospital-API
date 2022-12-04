﻿using HospitalLibrary.Examination;
using HospitalLibrary.Symptoms;
using HospitalLibrary.Symptoms.Dtos;
using HospitalLibrary.Symptoms.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [ApiController, Route("api/intranet/symptoms")]
    [Authorize("Doctor")]
    public class SymptomsController: ControllerBase
    {

        private ISymptomService _symptomService;

        public SymptomsController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [HttpPost]
        public IActionResult Create(CreateSymptomDto symptomDto)
        {
            Symptom symptom = _symptomService.Create(symptomDto.MapToModel());
            return Ok(symptom);
        }
    }
}