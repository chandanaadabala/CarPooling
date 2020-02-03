using CarPool.Models;
using CarPool.Services;
using CarPool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPool.UI
{
    //This class contains functionality of user who creates a ride
    class RideCreatorView
    {
        IRideServices _rideServices;
        IBookingServices _bookingServices;
        public RideCreatorView()
        {
            _rideServices = new RideServices();
            _bookingServices = new BookingServices();

        }
        internal void DisplayCreatedRides(List<RideViewModel> rides, string userID)
        {
            
            Console.WriteLine("Created Rides");
            Console.WriteLine("*********************************************");
            int i = 1;
            foreach (RideViewModel ride in rides)
            {
                List<StationViewModel> stations = ride.Stations.OrderBy(s => s.OrderNo).ToList();
                StationViewModel srcStation = stations[0];
                StationViewModel desStation = stations[stations.Count() - 1];
                Console.WriteLine(i + ") " + "Ride from " + srcStation.Name + " to " + desStation.Name +
                                        "\n  On: " + srcStation.Time);
                Console.Write("  Via: ");
                for (int j = 1; j < stations.Count - 1; j++)
                {
                    Console.Write("->" + stations[j].Name);
                }
                Console.Write("\n Ride status: ");
                if (DateTime.Compare(srcStation.Time, DateTime.Now) > 0) Console.WriteLine("Not started");
                else if (DateTime.Compare(srcStation.Time, DateTime.Now) < 0 && DateTime.Compare(srcStation.Time, DateTime.Now) <= 0)
                    Console.WriteLine("Completed");
                else if (DateTime.Compare(srcStation.Time, DateTime.Now) <= 0 && DateTime.Compare(srcStation.Time, DateTime.Now) > 0)
                    Console.WriteLine("Started");
                Console.WriteLine(" PricePerKM: " + ride.PricePerKM);
                i++;
            }
            Console.WriteLine(i + ") back");

        }
   

        internal void RideCreatorInterface(List<RideViewModel> rides, string userID)
        {
            try
            {
                Console.WriteLine("Select an option:");
                int choice = int.Parse(Console.ReadLine());
                if (choice == rides.Count + 1) return;
                if (choice > rides.Count + 1) throw new FormatException();
                RideViewModel selectedRide = rides[choice - 1];
                bool exit = true;
                do
                {
                    Console.WriteLine("1. View Bookings\n2. View Requests\n3. Back");
                    string option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            DisplayBookings(selectedRide);
                            break;
                        case "2":
                            string bookingID = DisplayRequests(selectedRide);
                            if (!string.IsNullOrEmpty(bookingID)) ApproveOrRejectbooking(bookingID);
                            break;
                        case "3":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice!");
                            break;
                    }
                } while (!exit);
            }
            catch(FormatException fe)
            {
                Console.WriteLine("Invalid option!");
            }
            catch(IndexOutOfRangeException ie)
            {
                Console.WriteLine("Invalid option!");
            }
            
        }

        private void ApproveOrRejectbooking(string bookingID)
        {
            Console.WriteLine("Select \n1. Approve\n2. Reject");
            switch(Console.ReadLine())
            {
                case "1":
                    _bookingServices.ApproveBooking(bookingID);
                    Console.WriteLine("Approved!");
                    break;
                case "2":
                    _bookingServices.RejectBooking(bookingID);
                    Console.WriteLine("Rejected!");
                    break;
                default:
                    break;

            }
        }

        private string DisplayRequests(RideViewModel selectedRide)
        {
            List<BookingViewModel> bookings = _bookingServices.GetUnApprovedBookingsOfRide(selectedRide.ID);
            if (bookings.Count == 0)
            {
                Console.WriteLine("No Requets!");
                return null;

            }
            else
            {
                try
                {
                    Console.WriteLine("Available Requests :");
                    int i = 1;
                    foreach (BookingViewModel booking in bookings)
                    {
                        Console.WriteLine(i + ") Requested By:");
                        Console.WriteLine("  Name:" + booking.BookedBy.Name +
                            "\n  Email:" + booking.BookedBy.Email +
                            "\n  MobileNumber:" + booking.BookedBy.MobileNum);
                        Console.WriteLine("  Booking details:");
                        Console.WriteLine("  Pick-Up: " + booking.BoardingStation.Name +
                                           "\n  Drop-Off: " + booking.DroppingStation.Name +
                                           "\n  Seats: " + booking.Seats);
                        i++;
                    }
                    Console.WriteLine(i + ") Back");
                    Console.WriteLine("Select an option:");
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == bookings.Count + 1) return null;
                    if (choice > bookings.Count + 1) throw new FormatException();
                    return bookings[choice - 1].ID;
                  
                }
                catch(FormatException fe)
                {
                    Console.WriteLine("Invalid option!");
                    return null;
                }
                catch(IndexOutOfRangeException ie)
                {
                    Console.WriteLine("Invalid option!");
                    return null;
                }
                
            }
        }

        private void DisplayBookings(RideViewModel selectedRide)
        {
            List<BookingViewModel> bookings = _bookingServices.GetApprovedBookingsOfRide(selectedRide.ID);
            if (bookings.Count == 0)
                Console.WriteLine("No bookings!");
            else
            {
                Console.WriteLine("Approved bookings:");
                
                foreach (BookingViewModel booking in bookings)
                {
                    Console.WriteLine( ") Booked By:");
                    Console.WriteLine("  Name:" + booking.BookedBy.Name +
                        "\n  Email:" + booking.BookedBy.Email +
                        "\n  MobileNumber:" + booking.BookedBy.MobileNum);
                    Console.WriteLine("  Booking details:");
                    Console.WriteLine("  Pick-Up: " + booking.BoardingStation.Name +
                                       "\n  Drop-Off: " + booking.DroppingStation.Name +
                                       "\n  Seats: " + booking.Seats);
                    Console.WriteLine("*******************************************************");
                }
                
            }
        }
    }
}
