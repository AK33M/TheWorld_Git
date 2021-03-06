﻿using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private IWorldRepository _repository;

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = _repository.GetUserTripsWithStops(User.Identity.Name);
            var results = Mapper.Map<IEnumerable<TripViewModel>>(trips);

            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newTrip = Mapper.Map<Trip>(model);

                    //Save to the database
                    _logger.LogInformation("Attempting to save a new trip");
                    _repository.AddTrip(newTrip, User.Identity.Name);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to Save new trip", ex);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Message = ex.Message });
                }
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json( new { Message = "Failed", ModelState = ModelState });
        }
    }
}
