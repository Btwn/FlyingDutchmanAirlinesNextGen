using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public partial class Customer
{
    public int? CustomerId { get; set; }

    public string Name { get; set; }
}
