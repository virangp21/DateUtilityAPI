using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateUtilityAPI.Domain;
using DateUtilityAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DateUtilityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateCalculatorController : ControllerBase
    {
        private readonly IDateHelperService service;

        public DateCalculatorController(IDateHelperService service)
        {
            this.service = service;
        }

        [HttpGet("WorkingDays")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetWorkingDaysCount(DateTime fromDate, DateTime toDate)
        {
           
            if (fromDate == default(DateTime) || toDate == default(DateTime))
            {
                return BadRequest("From Date and To Date must be a valid date");
            }
            if (toDate<fromDate)
            {
                return BadRequest("To Date must be larger than From Date");
            }

            var result = service.CountWorkingDays(fromDate, toDate);

            return Ok(new { NumberOfWorkingDays = result});

        }

        [HttpGet("WorkingDays/{state}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStateWorkingDaysCount(State state,DateTime fromDate, DateTime toDate)
        {

            if (fromDate == default(DateTime) || toDate == default(DateTime))
            {
                return BadRequest("From Date and To Date must be a valid date");
            }
            if (toDate < fromDate)
            {
                return BadRequest("To Date must be larger than From Date");
            }

            var result = service.CountWorkingDays(fromDate, toDate,state);

            return Ok(new { NumberOfWorkingDays = result });

        }
    }
}