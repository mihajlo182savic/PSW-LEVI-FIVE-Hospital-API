﻿using System.Threading.Tasks;

namespace HospitalLibrary.AnnualLeaves.Interfaces
{
    public interface IAnnualLeaveValidator
    {
        void Validate(AnnualLeave annualLeave);
    }
}