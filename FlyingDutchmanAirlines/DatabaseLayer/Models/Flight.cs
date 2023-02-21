using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public partial class Flight
{
    public int? FlightNumber { get; set; }

    public int? Origin { get; set; }

    public int? Destination { get; set; }
}
