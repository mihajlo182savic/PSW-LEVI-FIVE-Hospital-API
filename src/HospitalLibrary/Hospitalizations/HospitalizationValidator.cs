﻿using System;
using HospitalLibrary.Hospitalizations.Dtos;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Hospitalizations
{
    public class HospitalizationValidator: IHospitalizationValidator
    {
        private IUnitOfWork _unitOfWork;

        public HospitalizationValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public void ValidateCreate(Hospitalization hospitalization)
        {
            if (!_unitOfWork.MedicalRecordRepository.Exists(hospitalization.MedicalRecordId))
                throw new BadRequestException("Medical record doesn't exist!");
            if (!_unitOfWork.BedRepository.IsBedFree(hospitalization.BedId))
                throw new BadRequestException("Bed is currently taken!");
        }

        public void ValidateEndHospitalization(Hospitalization hospitalization, EndHospitalizationDTO dto)
        {
            if (hospitalization == null)
                throw new NotFoundException("Hospitalization with given id doesn't exist!");
            if (hospitalization.StartTime.CompareTo(dto.EndTime) >= 0)
                throw new BadRequestException("End Time should be after start time!");
        }
    }
}