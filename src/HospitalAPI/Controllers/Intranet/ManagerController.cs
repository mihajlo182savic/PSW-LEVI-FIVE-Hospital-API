
﻿using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Managers.Interfaces;
using HospitalLibrary.Patients.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

    }
}