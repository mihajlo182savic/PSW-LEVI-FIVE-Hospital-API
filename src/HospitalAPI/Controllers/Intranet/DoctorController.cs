﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Dtos;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/doctors")]
    [ApiController]
    public class DoctorController: ControllerBase
    {
        private IDoctorService _doctorService;
        private IEmailService _emailService;

        public DoctorController(IDoctorService doctorService, IEmailService emailService)
        {
            _doctorService = doctorService;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("internal-medicine/registration")]
        [Authorize(Roles="Patient")]
        public async Task<IActionResult> GetIternalMedicineDoctorsForPatientRegistration()
        {
            IEnumerable<Doctor> doctors = await _doctorService.GetIternalMedicineDoctorsForPatientRegistration();
            List <PatientsDoctorDTO> doctorDTOs = new List<PatientsDoctorDTO>();
            foreach (Doctor doctor in doctors)
            {
                PatientsDoctorDTO doctorDTO = new PatientsDoctorDTO(doctor.Name, doctor.Surname,doctor.Uid);
                doctorDTOs.Add(doctorDTO);
            }
            return Ok(doctorDTOs);
        }
        [HttpPost]
        [Authorize(Roles="Doctor")]
        public IActionResult Create([FromBody] CreateDoctorDTO doctorDto)
        {
            Doctor created = _doctorService.Create(doctorDto.MapToModel());
            _emailService.SendWelcomeEmail(created.Email);
            return Ok(created);
        }

        [HttpGet]
        [Authorize(Roles="Doctor")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Doctor> doctors = await _doctorService.GetAll();
            return Ok(doctors);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("statistics/DocsWithPopularity")]
        public async Task<IActionResult> GetDoctorsWithPopularity([FromQuery(Name = "minAge")] int minAge = 0, [FromQuery(Name = "maxAge")] int maxAge = 666)
        {
            List<Doctor> docs = (List<Doctor>)await _doctorService.GetDoctorsByAgeRange(minAge, maxAge);
            var docsWithPopularity = new List<DoctorWithPopularityDTO>();
            foreach (Doctor doctor in docs)
                docsWithPopularity.Add(new DoctorWithPopularityDTO(doctor.Id, doctor.Patients.Count, doctor.Name, doctor.Surname));
            return Ok(docsWithPopularity.AsEnumerable());
        }
    }
}