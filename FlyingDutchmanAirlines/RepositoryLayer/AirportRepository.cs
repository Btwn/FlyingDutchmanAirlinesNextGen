using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class AirportRepository
    {
        FlyingDutchmanAirlinesContext _context;

        public AirportRepository(FlyingDutchmanAirlinesContext context)
        {
            _context = context;
        }

        public async Task<Airport> GetAirportByID(int airportID)
        {
            if (airportID < 0)
            {
                Console.WriteLine($"Argument Exception in GetAirportByID! AirportID = { airportID}");
                throw new ArgumentException();
            }

            return await _context.Airports.FirstOrDefaultAsync(a => a.AirportId == airportID)
                ?? throw new AirportNotFoundException();

        }
    }
}
