using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BreakEvenCalculator;

namespace BreakEvenCalculatorReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreakEvenCalculatorController : ControllerBase
    {
        private readonly ILogger<BreakEvenCalculatorController> _logger;

        public BreakEvenCalculatorController(ILogger<BreakEvenCalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try {
                string oddsParam = GetOddsParam();
                var odds = new AmericanOdds(oddsParam);
                return Ok(new GetResponse{ BreakEvenPercentage = odds.GetBreakEvenPercentage() });
            } catch {
                return BadRequest();
            }
        }

        private string GetOddsParam() {
            string firstOddsParam = Request.Query["odds"].First();
            char firstChar = firstOddsParam[0];
            return firstChar == '-' || firstChar == '+' ? firstOddsParam : $"+{firstOddsParam}";
        }
    }

    public class GetResponse {
      public double BreakEvenPercentage { get; set; }
    }
}
