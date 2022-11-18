﻿using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Allergens.Interfaces
{
    public interface IAllergenRepository: IBaseRepository<Allergen>
    {
        Task<Allergen> GetOneByName(string name);
    }
}