using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class FlightRepository
    {
        FlyingDutchmanAirlinesContext _context;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public FlightRepository()
        {
            this._context = new FlyingDutchmanAirlinesContext();
            if (Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
                throw new Exception("This constructor should only be used for testing");
        }

        public FlightRepository(FlyingDutchmanAirlinesContext context)
        {
            _context = context;
        }

        public async Task<Flight> GetFlightByFlightNumber(int flightNumber, int originAirportId, int destinationAirportId)
        {
            if (!originAirportId.IsPositive() || !destinationAirportId.IsPositive())
            {
                Console.WriteLine($"Argument Exception in GetFlightByFlightNumber! originAirportId = { originAirportId} : destinationAirportId = { destinationAirportId}");
                throw new ArgumentException("invalid arguments provided");
            }

            if (flightNumber < 0)
            {
                Console.WriteLine($"Could not find flight in GetFlightByFlightNumber! flightNumber = { flightNumber}");
                throw new FlightNotFoundException();
            }

            return await _context.Flights.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber)
                ?? throw new FlightNotFoundException();
        }

        public virtual async Task<Flight> GetFlightByFlightNumber(int flightNumber)
        {
            return await GetFlightByFlightNumber(flightNumber, 1, 1);
        }
    }
}
