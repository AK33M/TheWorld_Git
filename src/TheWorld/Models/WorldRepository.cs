﻿using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public void AddStop(Stop newStop, string tripName, string userName)
        {
            var theTrip = GetTripByName(tripName, userName);

            if (theTrip.Stops.Any())
                newStop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            else
                newStop.Order = 1;
            theTrip.Stops.Add(newStop);
            //_context.Stops.Add(newStop);
        }

        public void AddTrip(Trip newTrip, string userName)
        {
            newTrip.UserName = userName;
            _context.Add(newTrip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips
                    .OrderBy(t => t.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from databse", ex);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips
                    .Include(x => x.Stops)
                    .OrderBy(t => t.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips with stops from databse", ex);
                return null;
            }
        }

        public Trip GetTripByName(string tripName, string userName)
        {
            return _context.Trips.Include(t => t.Stops)
                .Where(t => t.Name == tripName  && t.UserName == userName)
                .FirstOrDefault();
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            try
            {
                return _context.Trips
                    .Include(x => x.Stops)
                    .OrderBy(x => x.Name)
                    .Where(x => x.UserName == name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips with stops from databse", ex);
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
