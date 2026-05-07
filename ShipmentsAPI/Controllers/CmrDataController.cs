using Microsoft.AspNetCore.Mvc;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.Services;
using System;

namespace ShipmentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CmrDataController : ControllerBase
    {
        private readonly ICmrDataService cmrDataService;

        public CmrDataController(ICmrDataService cmrDataService)
        {
            this.cmrDataService = cmrDataService;
        }

        [HttpGet]
        public ActionResult<CmrDataDto> Get([FromQuery] Guid shipmentId, [FromQuery] Guid customerId)
        {
            var result = cmrDataService.GetByShipmentAndCustomer(shipmentId, customerId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<CmrDataDto> Upsert([FromBody] CreateCmrDataDto dto)
        {
            var result = cmrDataService.Upsert(dto);
            return Ok(result);
        }
    }
}
