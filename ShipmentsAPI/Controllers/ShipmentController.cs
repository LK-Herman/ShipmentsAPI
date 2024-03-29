﻿using Microsoft.AspNetCore.Mvc;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.Services;
using System;
using System.Collections.Generic;

namespace ShipmentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            this.shipmentService = shipmentService;
        }

        [HttpGet("scheduled")]
        public ActionResult<List<ShipmentDto>> GetScheduled()
        {
            var shipments = shipmentService.GetScheduleShipments();
            return Ok(shipments);
        }
        [HttpPost("search")]
        public ActionResult<PageResult<ShipmentDto>> Search([FromBody] QueryShipments filter)
        {
            var shipments = shipmentService.Search(filter);
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        public ActionResult<List<ShipmentDto>> GetById([FromRoute] Guid id)
        {
            var shipment = shipmentService.GetById(id);
            return Ok(shipment);
        }
        [HttpPost]
        public ActionResult<Guid> Create([FromBody] CreateShipmentDto dto)
        {
            var newShipmentId = shipmentService.Create(dto);
            return Ok(newShipmentId);
        }

        [HttpPost("addPO/{shipmentId}/{orderId}")]
        public ActionResult AddPurchaseOrder([FromRoute] Guid shipmentId, [FromRoute] Guid orderId)
        {
            shipmentService.AddOrderToShipment(shipmentId, orderId);
            return Ok();
        }
        [HttpPost("addOrders/{shipmentId}")]
        public ActionResult AddPurchaseOrder([FromRoute] Guid shipmentId, [FromBody] Guid[] ordersIds)
        {
            shipmentService.AddOrdersToShipment(shipmentId, ordersIds);
            return Ok();
        }

        [HttpPost("removePo/{shipmentId}/{orderId}")]
        public ActionResult RemovePurchaseOrder([FromRoute] Guid shipmentId, [FromRoute] Guid orderId)
        {
            shipmentService.RemoveOrderFromShipment(shipmentId, orderId);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<ShipmentBriefDto> Update([FromRoute] Guid id, [FromBody] UpdateShipmentDto dto)
        {
            var updatedShipment = shipmentService.Update(id, dto);
            return Ok(updatedShipment);
        }
        [HttpPut("{shipmentId}/{statusId}")]
        public ActionResult ChangeStatus([FromRoute] Guid shipmentId, [FromRoute] int statusId)
        {
            shipmentService.ChangeStatus(shipmentId, statusId);
            return Ok();
        }
        [HttpPut("area/{shipmentId}/{areaId}")]
        public ActionResult ChangeWarehouseLocation([FromRoute] Guid shipmentId, [FromRoute] int areaId)
        {
            shipmentService.ChangeWarehouseLocation(shipmentId, areaId);
            return Ok();
        }
        [HttpPut("area/{shipmentId}")]
        public ActionResult RemoveWarehouseLocation([FromRoute] Guid shipmentId)
        {
            shipmentService.RemoveWarehouseLocation(shipmentId);
            return Ok();
        }
        [HttpPut("forwarder/{shipmentId}/{forwarderId}")]
        public ActionResult ChangeStatus([FromRoute] Guid shipmentId, [FromRoute] Guid forwarderId)
        {
            shipmentService.ChangeForwarder(shipmentId, forwarderId);
            return Ok();
        }
        [HttpPut("atd/{shipmentId}/{dateTime}")]
        public ActionResult ChangeTimeOfDeparture([FromRoute] Guid shipmentId, [FromRoute] string dateTime)
        {
            shipmentService.ChangeDepartureDate(shipmentId, dateTime);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            shipmentService.Delete(id);
            return Ok();
        }
    }
}
