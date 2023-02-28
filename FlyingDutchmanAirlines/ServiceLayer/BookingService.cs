﻿using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace FlyingDutchmanAirlines.ServiceLayer
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly FlightRepository _flightRepository;

        public BookingService(BookingRepository repository, CustomerRepository customerRepository, FlightRepository flightRepository)
        {
            _bookingRepository = repository;
            _customerRepository = customerRepository;
            _flightRepository = flightRepository;
        }

        public async Task<(bool, Exception)>CreateBooking(string customerName, int flightNumber)
        {
            if (string.IsNullOrEmpty(customerName) || !flightNumber.IsPositive())
                return (false, new ArgumentException());

            

            try
            {
                Customer customer = await GetCustomerFromDatabase(customerName)
                       ?? await AddCustomerToDatabase(customerName);

                if (!await FlightExistsInDatabase(flightNumber))
                    throw new CouldNotAddBookingToDatabaseException();

                await _bookingRepository.CreateBooking(customer.CustomerId, flightNumber);
            }
            catch (Exception exception)
            {
                return (false, exception);
            }
            return (true, null);
        }

        private async Task<bool> FlightExistsInDatabase(int flightNumber)
        {
            try
            {
                return await _flightRepository.GetFlightByFlightNumber(flightNumber) != null;
            }
            catch (FlightNotFoundException)
            {
                return false;
            }
        }

        private async Task<Customer> GetCustomerFromDatabase(string name)
        {
            try
            {
                return await _customerRepository.GetCustomerByName(name);
            }
            catch (CustomerNotFoundException)
            {
                return null;
            }
            catch (Exception exception)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException
                    ?? new Exception()).Throw();
                return null;
            }
        }
        private async Task<Customer> AddCustomerToDatabase(string name)
        {
            await _customerRepository.CreateCustomer(name);
            return await _customerRepository.GetCustomerByName(name);
        }
    }
}
