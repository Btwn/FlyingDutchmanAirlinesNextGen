using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public sealed class Airport
{
    public int AirportId { get; set; }

    public string City { get; set; }

    public string Iata { get; set; }

    public ICollection<Flight> FlightDestinationNavigations { get; } = new List<Flight>();

    public ICollection<Flight> FlightOriginNavigations { get; } = new List<Flight>();
}
