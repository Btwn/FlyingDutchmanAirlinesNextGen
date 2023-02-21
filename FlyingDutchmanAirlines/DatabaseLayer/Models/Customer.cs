using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public sealed class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public ICollection<Booking> Bookings { get; } = new List<Booking>();

    public Customer(string name)
    {
        Name = name;
    }
}
