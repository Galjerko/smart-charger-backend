﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCharger.Business.DTOs;
using SmartCharger.Business.Interfaces;
using SmartCharger.Business.Services;

namespace SmartCharger.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [Authorize(Policy = "AdminOrCustomer")]
        [HttpGet("{userId}/history")]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetUsersChargingHistory(int userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 5, [FromQuery] string search = null)
        {
            EventResponseDTO response = await _eventService.GetUsersChargingHistory(userId, page, pageSize, search);
            if (response.Success == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
