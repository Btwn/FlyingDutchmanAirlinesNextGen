using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.Views;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FlyingDutchmanAirlines.ServiceLayer
{
    public class FlightService
    {
        private readonly AirportRepository _airportRepository;
        private readonly FlightRepository _flightRepository;

        public FlightService(AirportRepository airportRepository, FlightRepository flightRepository)
        {
            _airportRepository = airportRepository;
            _flightRepository = flightRepository;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public FlightService()
        {
            if (Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
                throw new Exception("This constructor should only be used for testing");
        }

        public virtual async IAsyncEnumerable<FlightView> GetFlights()
        {
            IEnumerable<Flight> flights = _flightRepository.GetFlights();


            foreach (Flight flight in flights)
            {
                Airport originAirport;
                Airport destinationAirport;


                try
                {
                    originAirport = await _airportRepository.GetAirportByID(flight.Origin);
                    destinationAirport = await _airportRepository.GetAirportByID(flight.Destination);
                }
                catch(FlightNotFoundException)
                {
                    throw new FlightNotFoundException();
                }
                catch (Exception)
                {
                    throw new ArgumentException();
                }

                yield return new FlightView(flight.FlightNumber.ToString(),
                    (originAirport.City, originAirport.Iata),
                    (destinationAirport.City, destinationAirport.Iata));
            }
        }

        public virtual async Task<FlightView> GetFlightByFlightNumber(int flightNumber)
        {
            try
            {
                Flight flight = await _flightRepository.GetFlightByFlightNumber(flightNumber);
                Airport originAirport = await _airportRepository.GetAirportByID(flight.Origin);
                Airport destinationAirport = await _airportRepository.GetAirportByID(flight.Destination);
                return new FlightView(
                    flight.FlightNumber.ToString(),
                    (originAirport.City, originAirport.Iata),
                    (destinationAirport.City, destinationAirport.Iata));
            }
            catch (FlightNotFoundException)
            {
                throw new FlightNotFoundException();
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }
    }
}
