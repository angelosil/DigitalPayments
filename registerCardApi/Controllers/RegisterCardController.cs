using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using registerCardApi.Model;
using registerCardApi.Service;
using System;

namespace registerCardApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterCardController : ControllerBase
    {
        private readonly ILogger<RegisterCardController> _logger;

        public RegisterCardController(ILogger<RegisterCardController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult RegisterCard(Card card)
        {
            try
            {
                CardRegistrationService cardService = new CardRegistrationService();

                TokenDTO tokenDTO = cardService.RegisterCard(card);

                return Ok(tokenDTO);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

           
        }

    }
}
