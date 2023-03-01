using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class AirportRepository
    {
        FlyingDutchmanAirlinesContext _context;

        public AirportRepository(FlyingDutchmanAirlinesContext context)
        {
            _context = context;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public AirportRepository()
        {
            this._context = new FlyingDutchmanAirlinesContext();
            if (Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
                throw new Exception("This constructor should only be used for testing");
        }

        public virtual async Task<Airport> GetAirportByID(int airportID)
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
