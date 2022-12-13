﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ceTe.DynamicPDF.PageElements;
using HospitalAPI.Storage;
using HospitalLibrary.Appointments;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Medicines;
using HospitalLibrary.Patients;
using HospitalLibrary.PDFGeneration;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Symptoms;

namespace HospitalLibrary.Examination
{
    public class ExaminationReportService: IExaminationReportService
    {

        private IUnitOfWork _unitOfWork;
        private IExaminationReportValidator _validator;
        private IStorage _storage;
        private IPDFGenerator _generator;

        public ExaminationReportService(IUnitOfWork unitOfWork, IExaminationReportValidator validator, IStorage storage, IPDFGenerator generator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _storage = storage;
            _generator = generator;
        }
        
        public async Task<ExaminationReport> Create(ExaminationReport report)
        {
            _validator.ValidateCreate(report);
            var symptoms = _unitOfWork.SymptomRepository.PopulateRange(report.Symptoms);
            var notExisting = FindNotExisting(report.Symptoms, symptoms.ToList());
            symptoms = symptoms.Concat(notExisting);
            report.Symptoms = symptoms.ToList();
            await GeneratePdf(report);
            _unitOfWork.ExaminationReportRepository.Add(report);
            _unitOfWork.ExaminationReportRepository.Save();
            return report;
        }
        
        public ExaminationReport GetByExamination(int examinationId)
        {
            return _unitOfWork.ExaminationReportRepository.GetByExamination(examinationId);
        }

        public ExaminationReport GetById(int id)
        {
            return _unitOfWork.ExaminationReportRepository.GetOne(id);
        }


        private IEnumerable<Symptom> FindNotExisting(List<Symptom> old, List<Symptom> existing)
        {
            return old.Where(s => existing.All(e => e.Id != s.Id)).ToList();
        }
        private void PopulateMedicines(ExaminationReport examinationReport)
        {
            List<int> ids = examinationReport.Prescriptions.Select(p => p.MedicineId).ToList();
            List<Medicine> medicines = _unitOfWork.MedicineRepository.GetAllByIds(ids);
            foreach(Prescription p in examinationReport.Prescriptions)
            {
                Medicine medicine = medicines.Find(m => p.MedicineId.Equals(m.Id));
                p.Medicine = medicine;
            }
        }

        private async Task GeneratePdf(ExaminationReport examinationReport)
        {
            Appointment appointment = _unitOfWork.AppointmentRepository.GetOne(examinationReport.ExaminationId);
            Patient patient = _unitOfWork.PatientRepository.GetOne(appointment.PatientId.Value);
            PopulateMedicines(examinationReport);
            var pdf = _generator.GenerateExaminationReportPdf(examinationReport, patient);
            string file = await _storage.UploadFile(pdf, $"examination-report-{DateTime.Now.ToString("ddMMyyyyhhmmss")}-{patient.Id}");
            examinationReport.Url = file;
        }
    }
}