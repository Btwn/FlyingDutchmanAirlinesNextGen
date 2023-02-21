using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();
}
