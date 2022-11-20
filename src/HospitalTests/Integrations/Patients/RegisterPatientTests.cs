﻿using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Patients;

[Collection("Test")]
public class PatientTests: BaseIntegrationTest
{
    public PatientTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public void Register_patient_success()
    {
        using var scope = Factory.Services.CreateScope();
        List<AllergenDTO> allergens = new List<AllergenDTO>
        {
            new AllergenDTO("Milk"),
            new AllergenDTO("Cetirizine")
        };
        var controller = new AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>(),
                                    scope.ServiceProvider.GetRequiredService<IEmailService>());
        CreatePatientDTO createPatientDTO = new CreatePatientDTO("Pera", "Peric", "gmail123@gmail.com","29857236",
            "5455454",new DateTime(2001,2,25),"Mikse Dimitrijevica 42",BloodType.ZERO_NEGATIVE,
            "pRoXm","radipls",allergens,"67867867");
        createPatientDTO.Id = 3;
        var result = ((OkObjectResult)controller.RegisterPatient(createPatientDTO).Result).Value as PatientDTO;
        result.ShouldNotBeNull();
    }
    [Fact]
    public void Register_patient_failed()
    {
        using var scope = Factory.Services.CreateScope();
        List<AllergenDTO> allergens = new List<AllergenDTO>
        {
            new AllergenDTO("Milk"),
            new AllergenDTO("Cetirizine")
        };
        var controller = new AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>(),
                                    scope.ServiceProvider.GetRequiredService<IEmailService>());
        CreatePatientDTO createPatientDTO = new CreatePatientDTO("Pera", "Peric", "gmail123@gmail.com","29857236",
            "5455454",new DateTime(2001,2,25),"Mikse Dimitrijevica 42",BloodType.ZERO_NEGATIVE,
            "pRoXm","radipls",allergens,"26549037");
        createPatientDTO.Id = 3;
        Should.Throw<AggregateException>(() => ((OkObjectResult)controller.RegisterPatient(createPatientDTO).Result).Value);
    }
}