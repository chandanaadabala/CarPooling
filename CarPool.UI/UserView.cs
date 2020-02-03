using CarPool.Models;
using CarPool.Services;
using CarPool.Services.Interfaces;
using CarPool.Services.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPool.UI
{
    class UserView
    {
        IUserServices _userServices;
        ICarServices _carServices;
        ICoordinateServices _coordinateServices;
        IRideServices _rideServices;
        IBookingServices _bookingServices;
        public UserView()
        {
            _userServices = new UserServices();
            _carServices = new CarServices();
            _coordinateServices = new CoordinateServices();
            _rideServices = new RideServices();
            _bookingServices = new BookingServices();
        }

        public string SignUp()
        {
            Console.WriteLine("SIGN-UP");
            Console.WriteLine("*********************************************************");
            UserViewModel user = new UserViewModel();
            while(true)
            {
                Console.WriteLine("Enter your name:");
                user.Name = Console.ReadLine();
                if (Validation.IsValidName(user.Name))
                    break;
                Console.WriteLine("Invalid name!");
            }
            while(true)
            {
                Console.WriteLine("Enter your email:");
                user.Email = Console.ReadLine();
                if (_userServices.IsEmailExist(user.Email))
                {
                    Console.WriteLine("Email already registered!");
                    return null;
                }
                if (!Validation.IsValidEmail(user.Email))
                {
                    Console.WriteLine("Invalid email!");
                    continue;
                }
                break;
            }
            while(true)
            {
                Console.WriteLine("Enter your password:");
                user.Password = PasswordMasker.GetPassword();
                if (Validation.IsValidPassword(user.Password))
                    break;
                Console.WriteLine("Invalid password!");
            }
            while(true)
            {
                Console.WriteLine("Enter your mobile number");
                user.MobileNum = Console.ReadLine();
                if (Validation.IsValidMblNumber(user.MobileNum))
                    break;
                Console.WriteLine("Invalid mobile number!");
            }
            Console.WriteLine("Do you want to add your car details?");
            Console.WriteLine("1. Yes\n2. No");
            switch (Console.ReadLine())
            {
                case "1":
                    while(true)
                    {
                        Console.WriteLine("Enter your car name:");
                        user.Car.Name = Console.ReadLine();
                        if (Validation.IsValidName(user.Car.Name))
                            break;
                        Console.WriteLine("Invalid name!");
                    }
                    while(true)
                    {
                        Console.WriteLine("Enter your car number:");
                        user.Car.NumberPlate = Console.ReadLine();
                        if(!_carServices.IsCarExist(user.Car.NumberPlate))
                            if (Validation.IsValidNumberPlate(user.Car.NumberPlate))
                                break;
                            else
                                Console.WriteLine("Invalid number!");
                        else
                            Console.WriteLine("Car already registered!");
                    }
                    break;
                case "2":
                    break;
                default:
                    break;
            }
            return _userServices.AddUser(user);
            
        }
        public string SignIn()
        {
            Console.WriteLine("Enter you email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = PasswordMasker.GetPassword();
            return _userServices.Login(email, password);
        }

        
        public void UserInterface(string userID)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Select\n" +
                    "1. View profile\n" +
                    "2. Update profile\n" +
                    "3. Create a ride\n" +
                    "4. Book a ride\n" +
                    "5. View created rides\n" +
                    "6. View bookings\n" +
                    "7. Logout");
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        DisplayProfile(userID);
                        break;
                    case "2":
                        UpdateProfile(userID);
                        break;
                    case "3":
                        CreateRide(userID);
                        break;
                    case "4":
                        BookRide(userID);
                        break;
                    case "5":                        
                        RideCreatorView rideCreatorView = new RideCreatorView();
                        List<RideViewModel> rides = _rideServices.GetUserCreatedRides(userID);
                        if(rides.Count==0)
                        {
                            Console.WriteLine("No rides!");
                            break;
                        }
                        rideCreatorView.DisplayCreatedRides(rides,userID);
                        rideCreatorView.RideCreatorInterface(rides,userID);
                        break;
                    case "6":
                        RideTakerView rideTakerView = new RideTakerView();
                        rideTakerView.DisplayBookings(userID);
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Select valid option!");
                        break;
                }
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
            }
        }

        //This method take input from user and display available rides and calls CreateBooking method in services
        private void BookRide(string userID)
        {
            try
            {
                Console.WriteLine("Available Locations");
                int locIdx = 1;
                List<string> locations = new List<string>(Coordinate.Locations.Keys);
                foreach (string location in locations)
                {
                    Console.WriteLine(locIdx + ") " + location);
                    locIdx++;
                }
                Console.WriteLine("Select locations from above list by entering its option number");
                Console.WriteLine("****************************************************");
                Console.WriteLine("Boarding Location:");
                string boarding = locations[int.Parse(Console.ReadLine()) - 1];
                Console.WriteLine("Dropping Location:");
                string dropping = locations[int.Parse(Console.ReadLine()) - 1];
                DateTime date;
                while (true)
                {
                    Console.WriteLine("Date:");
                    Console.WriteLine("Note: Enter date in format 'dd/mm/yyyy'");
                    date = DateTime.Parse(Console.ReadLine());
                    if (!HelperClass.IsEarlierDate(date))
                        break;
                    Console.WriteLine("You have entered earlier date!");

                }
                List<RideViewModel> rides = _rideServices.GetAvailableRides(boarding, dropping, date, userID);
                if (rides.Count == 0)
                {
                    Console.WriteLine("No rides available!");
                    return;
                }
                int rideIdx = 1;
                Console.WriteLine("Available ride from " + boarding + " to " + dropping);
                foreach (RideViewModel ride in rides)
                {
                    StationViewModel srcStation = ride.Stations.SingleOrDefault(s => s.Name == boarding);
                    StationViewModel desStation = ride.Stations.SingleOrDefault(s => s.Name == dropping);
                    Console.WriteLine(rideIdx + ") " +
                                        " Rider: " + ride.RideBy.Name +
                                        "\n Rider Email: " + ride.RideBy.Email +
                                        "\n Boarding Time: " + srcStation.Time.ToShortTimeString() +
                                        "\n Dropping Time: " + desStation.Time.ToShortTimeString() +
                                        "\n Available Seats: " + ride.AvailableSeats +
                                        "\n Distance: "+ (desStation.DistanceFromSrc - srcStation.DistanceFromSrc)+
                                        "\n PricePerSeat: " + ride.PricePerKM * ((decimal)(desStation.DistanceFromSrc - srcStation.DistanceFromSrc)));
                    List<StationViewModel> stations = ride.Stations.OrderBy(s => s.OrderNo).ToList();
                    Console.Write("Ride Map: ");
                    foreach (StationViewModel station in stations)
                    {
                        Console.Write("->" + station.Name);
                    }
                    Console.WriteLine("\n");
                }
                Console.WriteLine("Select your option: ");
                RideViewModel selectedRide = rides[int.Parse(Console.ReadLine()) - 1];
                Console.WriteLine("No.of Seats: ");
                int seats = int.Parse(Console.ReadLine());
                BookingViewModel booking = new BookingViewModel
                {
                    BoardingStation = selectedRide.Stations.SingleOrDefault(s => s.Name == boarding),
                    DroppingStation = selectedRide.Stations.SingleOrDefault(s => s.Name == dropping),
                    Approval = null,
                    Seats = seats
                };
                booking.Ride = selectedRide;
                booking.BookedBy.ID = userID;
                if (_bookingServices.CreateBooking(booking) == null)
                    Console.WriteLine("Booking Failed!");
                else
                    Console.WriteLine("Booking Success!");
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

      
        //This function takes input ride details as input and calls AddRide method in services
        private void CreateRide(string userID)
        {
            try
            {
                if (!_carServices.IsUserHaveCar(userID))
                {
                    Console.WriteLine("Unable to create a ride!");
                    Console.WriteLine("Register your car and come again!");
                    return;
                }
                Console.WriteLine("Available Locations");
                int locIdx = 1;
                List<string> locations = new List<string>(Coordinate.Locations.Keys);
                foreach (string location in locations)
                {
                    Console.WriteLine(locIdx + ") " + location);
                    locIdx++;
                }
                Console.WriteLine("Select locations from above list by entering its option number");
                Console.WriteLine("****************************************************");
                RideViewModel ride = new RideViewModel();
                ride.RideBy.ID = userID;
                List<StationViewModel> stations = new List<StationViewModel>();
                Console.WriteLine("Create You Ride");
                Console.WriteLine("*******************************************");
                Console.WriteLine("Available seats:");
                ride.AvailableSeats = int.Parse(Console.ReadLine());

                {
                    StationViewModel station = new StationViewModel();
                    Console.WriteLine("Boarding Location : ");
                    station.Name = locations[int.Parse(Console.ReadLine()) - 1];
                    Console.WriteLine("Boarding DateTime : ");
                    Console.WriteLine("Enter in format 'dd/MM/yyyy HH:mm:ss' ");
                    station.Time = DateTime.Parse(Console.ReadLine());
                    if (HelperClass.IsEarlierDate(station.Time))
                    {
                        Console.WriteLine("You have entered an earlier date!");
                        return;
                    }
                    station.OrderNo = 0;
                    station.DistanceFromSrc = 0;
                    stations.Add(station);
                }

                Console.WriteLine("Number of stopovers : ");
                ride.NumOfStopOvers = int.Parse(Console.ReadLine());
                int i = 1;

                while (i <= ride.NumOfStopOvers)
                {
                    StationViewModel station = new StationViewModel();
                    Console.WriteLine("StopOver "+i+" : ");
                    station.Name = locations[int.Parse(Console.ReadLine()) - 1];
                    if (station.Name == stations[stations.Count() - 1].Name)
                    {
                        Console.WriteLine("You have entered your previous station!");
                        return;
                    }
                    Console.WriteLine("Arrival Time : ");
                    Console.WriteLine("Enter in format 'dd/MM/yyyy HH:mm:ss' ");
                    station.Time = DateTime.Parse(Console.ReadLine());
                    if (HelperClass.IsEarlierDate(station.Time))
                    {
                        Console.WriteLine("You have entered an earlier date!");
                        return;
                    }
                    station.OrderNo = i;
                    
                    stations.Add(station);
                    i++;
                }
                {
                    StationViewModel station = new StationViewModel();
                    Console.WriteLine("Dropping Location : ");
                    station.Name = locations[int.Parse(Console.ReadLine()) - 1];
                    if (station.Name == stations[stations.Count() - 1].Name)
                    {
                        Console.WriteLine("You have entered your previous station!");
                        return;
                    }
                    Console.WriteLine("Dropping DateTime : ");
                    Console.WriteLine("Enter in format 'dd/MM/yyyy HH:mm:ss' ");
                    station.Time = DateTime.Parse(Console.ReadLine());
                    if (HelperClass.IsEarlierDate(station.Time))
                    {
                        Console.WriteLine("You have entered an earlier date!");
                        return;
                    }
                    station.OrderNo = i;
                   
                    stations.Add(station);
                }
               
                _coordinateServices.CalculateDistance(ref stations);
                ride.Stations = stations;
                if (_rideServices.CreateRide(ride))
                    Console.WriteLine("Ride created successfully");
                else
                    Console.WriteLine("Ride creation unsuccesful");
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

        //this functions takes imput from user to update user details
        private void UpdateProfile(string userID)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select the field to update");
                Console.WriteLine("1. Name\n2. Email\n3. MobileNumber\n4. Password\n5. Car Details\n6. Back");
                string choice = Console.ReadLine();
                Console.Clear();
                switch(choice)
                {
                    case "1":
                        while(true)
                        {
                            Console.WriteLine("Enter name:");
                            string value = Console.ReadLine();
                            if(!Validation.IsValidName(value))
                            {
                                Console.WriteLine("Invalid name!");
                                continue;
                            }
                            _userServices.UpdateProfile(userID, "Name", value);
                            break;
                        }
                        break;
                    case "2":
                        while(true)
                        {
                            Console.WriteLine("Enter email:");
                            string value = Console.ReadLine();
                            if (!Validation.IsValidEmail(value))
                            {
                                Console.WriteLine("Invalid email!");
                                continue;
                            }
                            if(_userServices.IsEmailExist(value))
                            {
                                Console.WriteLine("Email already registered!");
                                continue;
                            }
                            _userServices.UpdateProfile(userID, "Email", value);
                            break;
                        }
                        break;
                    case "3":
                        while(true)
                        {
                            Console.WriteLine("Enter mobile number:");
                            string value = Console.ReadLine();
                            if(!Validation.IsValidMblNumber(value))
                            {
                                Console.WriteLine("Invalid mobile number!");
                                continue;
                            }
                            _userServices.UpdateProfile(userID, "MobileNum", value);
                            break;
                        }
                        break;
                    case "4":
                        while(true)
                        {
                            Console.WriteLine("Enter password:");
                            string value = PasswordMasker.GetPassword();
                            if (!Validation.IsValidPassword(value))
                            {
                                Console.WriteLine("Invalid password!");
                                continue;
                            }
                            _userServices.UpdateProfile(userID, "Password", value);
                            break;
                        }
                        break;
                    case "5":
                        {
                            CarViewModel car = new CarViewModel();
                            while (true)
                            {
                                Console.WriteLine("Enter your car name:");
                                car.Name = Console.ReadLine();
                                if (Validation.IsValidName(car.Name))
                                    break;
                                Console.WriteLine("Invalid name!");
                            }
                            while (true)
                            {
                                Console.WriteLine("Enter your car number:");
                                car.NumberPlate = Console.ReadLine();
                                if (!_carServices.IsCarExist(car.NumberPlate))
                                    if (Validation.IsValidNumberPlate(car.NumberPlate))
                                        break;
                                    else
                                        Console.WriteLine("Invalid number!");
                                else
                                    Console.WriteLine("Car already registered!");
                            }
                            if (!_carServices.UpdateCar(userID, car))
                                Console.WriteLine("Updation failed!");
                            else
                                Console.WriteLine("Successfully updated!");
                            
                        }
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
            }
        }

        // used to display user profile
        private void DisplayProfile(string userID)
        {
            UserViewModel user = _userServices.GetProfile(userID);
            Console.WriteLine("Your Profile");
            Console.WriteLine("***********************************************************************");
            Console.WriteLine(" Name = " + user.Name+
                              "\n Email = " + user.Email);
            if (!string.IsNullOrEmpty(user.MobileNum))
                Console.WriteLine(" Mobile Number = " + user.MobileNum);
            if (user.Car != null)
                Console.WriteLine(" Car Name = " + user.Car.Name +
                                  "\n Number Plate = " + user.Car.NumberPlate );
        }
    }
}
