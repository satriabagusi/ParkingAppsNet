// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ParkingApps{

    class Program{
        
        static List<ParkingLot> parkingLots;
        static int totalLots;

        static void Main(string[] args){
            parkingLots = new List<ParkingLot>();
            totalLots = 0;

            bool exit = false;

            while (!exit) {
                Console.Write("Put your command here : ");
                string menu = Console.ReadLine();
                string[] menuSplit = menu.Split(' ');

                switch (menuSplit[0].ToLower()){
                    case "create_parking_lot":
                        // Console.Write(menu);
                        if (menuSplit.Length == 2) {
                            int totalLot = Convert.ToInt32(menuSplit[1]);
                            CreateParkingLot(totalLot);
                        }else{
                            Console.WriteLine("Command format is incorrect. Usage: create_parking_lot <lotCount>");
                        }
                        break;
                    case "total_available_lot":
                        GetAvailableLot();
                        break;
                    case "total_filled_lot":
                        Console.WriteLine("Total filled lot : " + parkingLots.Count);
                        break;
                    case "park" :
                        if (menuSplit.Length == 4){
                            string licensePlate = menuSplit[1];
                            string vehicleColor = menuSplit[2];
                            string vehicleType = menuSplit[3];
                            Park(licensePlate, vehicleColor, vehicleType);
                        }else{
                            Console.WriteLine("Command format is incorrect. Usage: park <licensePlate> <vehicleColor> <vehicleType>");
                        }
                        break;
                    case "status":
                        Status();
                        break;
                    case "leave":
                        if(menuSplit.Length == 2){
                            int lotNumber = Convert.ToInt32(menuSplit[1]);
                            Leave(lotNumber);
                        }else{
                            Console.WriteLine("Command format is incorrect. Usage: leave <lotNumber>");
                        }
                        break;
                    case "type_of_vehicles":
                        if(menuSplit.Length == 2){
                            string vehicleType = menuSplit[1];
                            GetTotalVehicleByType(vehicleType);
                        }else{
                            Console.WriteLine("Command format is incorrect. Usage: type_of_vehicles <vehicleType>");
                        }
                        break;
                    case "registration_numbers_for_vehicles_with_odd_plate":
                        GetVehiclesWithOddPlate();
                        break;
                    case "registration_numbers_for_vehicles_with_even_plate":
                        GetVehiclesWithEvenPlate();
                        break;
                    case "slot_number_for_registration_number":
                        if (menuSplit.Length == 2){
                            string licensePlate = menuSplit[1];
                            GetSlotNumberByLicensePlate(licensePlate);
                        }else{
                            Console.WriteLine("Command format is incorrect. Usage: slot_number_for_registration_number <licensePlate>");
                        }
                        break;
                    case "registration_numbers_for_vehicles_with_colour":
                        if (menuSplit.Length == 2){
                            string color = menuSplit[1];
                            GetLicensePlateByColor(color);
                        }else{
                            Console.WriteLine("Command format is incorrect. Usage: registration_numbers_for_vehicles_with_colour <colour>");
                        }
                        break;
                    case "slot_numbers_for_vehicles_with_colour":
                        if (menuSplit.Length == 2){
                            string color = menuSplit[1];
                            GetSlotNumbersByColor(color);
                        }else{
                            Console.WriteLine("Command format is incorrect. Usage: registration_numbers_for_vehicles_with_colour <colour>");
                        }
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                    Console.WriteLine("Invalid Command.");
                    break;
                }
            }

            static void CreateParkingLot(int totalLot){
                if (totalLots > 0){
                    Console.WriteLine("Parking lot has already been created.");
                    return;
                }

                totalLots = totalLot;
                for (int i = 0; i < totalLots; i++){
                    parkingLots.Add(null);
                }

                Console.WriteLine("Created a parking lot with {0} slots", totalLots);
            }

            static void Park(string licensePlate, string vehicleColor, string vehicleType){
                if (totalLots == 0){
                    Console.WriteLine("Parking lot has not been created.");
                    return;
                }

                int availableLot = GetAvailableLot();
                if (availableLot == -1) {
                    Console.WriteLine("Sorry, Parking lot is full.");
                    return;
                }

                bool vehicleWithLicenseExists = false;
                int type = GetVehicleType(vehicleType);

                // foreach(ParkingLot lot in parkingLots){
                //     if(lot.Vehicle.LicensePlate == licensePlate && lot.Vehicle.Type == type){
                //         vehicleWithLicenseExists = true;
                //         break;
                //     }
                // }

                if(!vehicleWithLicenseExists){
                    if (type == -1) {
                        Console.WriteLine("Jenis Kendaraan selain Motor/Mobil tidak bisa Parkir");
                        return;
                    }

                    Vehicle vehicle = new Vehicle(type, licensePlate, vehicleColor);
                    ParkingLot newLot = new ParkingLot(vehicle, DateTime.Now);
                    parkingLots[availableLot] = newLot;

                    Console.WriteLine("Allocated slot number: {0}", availableLot + 1);
                }else{
                    Console.WriteLine("Same plate and vehicle type already exists.");
                }
            }

            static void Leave(int lotNumber){
                if (totalLots == 0){
                    Console.WriteLine("Parking lot has not been created.");
                    return;
                }

                if (lotNumber < 1 || lotNumber > totalLots){
                    Console.WriteLine("Invalid lot number.");
                    return;
                }

                ParkingLot lot = parkingLots[lotNumber - 1];
                if (lot == null){
                    Console.WriteLine("Slot number {0} is already free", lotNumber);
                }

                TimeSpan duration = DateTime.Now - lot.EntryTime;
                double hoursParked = Math.Ceiling(duration.TotalHours);
                double totalPriceParked = 0;

                if (hoursParked >= 0 &&  hoursParked <= 1){
                    totalPriceParked = totalPriceParked + 2000;
                }else if (hoursParked >= 2){
                    totalPriceParked = totalPriceParked + 200 + (hoursParked * 1500);
                }


                parkingLots[lotNumber - 1] = null;

                Console.WriteLine("Park Duration : {0} hour(s).", hoursParked);
                Console.WriteLine("Parking fee: {0}", totalPriceParked);

                Console.WriteLine("Slot number {0} is free", lotNumber);

            }

            static void Status(){
                if (totalLots == 0){
                    Console.WriteLine("Parking lot has not been created.");
                    return;
                }

                Console.WriteLine("Slot\tPlat Nomor\tJenis Kendaraan\t\tWarna Kendaraan");

                for (int i = 0; i < parkingLots.Count; i++){
                    if (parkingLots[i] != null){
                        Console.WriteLine("{0}\t{1}\t{2}\t\t\t{3}", i+1, parkingLots[i].Vehicle.LicensePlate, GetVehicleTypeName(parkingLots[i].Vehicle.Type), parkingLots[i].Vehicle.Color);
                    }
                }
            }

            static void GetTotalVehicleByType(string vehicleType){
                if (totalLots == 0){
                    Console.WriteLine("Parking lot has not been created.");
                    return;
                }

                int type = GetVehicleType(vehicleType);
                if (type == -1){
                    Console.WriteLine("Invalid vehicle type");
                    return;
                }

                int vehicleCount = 0;

                foreach (ParkingLot lot in parkingLots){
                    if (lot != null && lot.Vehicle.Type == type){
                        vehicleCount++;
                    }
                }

                Console.WriteLine(vehicleCount);
            }

            static void GetVehiclesWithOddPlate(){
                if (totalLots == 0){
                    Console.WriteLine("Parking lot has not been created.");
                    return;
                }

                List<string> oddPlates = new List<string>();

                foreach (ParkingLot lot in parkingLots){
                    if (lot != null){
                        int parseToIntPlate = int.Parse(Regex.Match(lot.Vehicle.LicensePlate, @"\d+").Value);
                        int lastDigit = parseToIntPlate % 10;
                        if (lastDigit % 2 != 0){
                            oddPlates.Add(lot.Vehicle.LicensePlate);
                        }
                    }
                }

                if (oddPlates.Count == 0){
                    Console.WriteLine("Odd Plate Number Vehicle not found.");
                    return;
                }

                foreach (string plate in oddPlates){
                    Console.Write("{0}, ", plate);
                }
                Console.WriteLine("");
            }

            static void GetVehiclesWithEvenPlate(){
                if (totalLots == 0){
                    Console.WriteLine("Parking lot has not been created.");
                    return;
                }

                List<string> evenPlates = new List<string>();

                foreach (ParkingLot lot in parkingLots){
                    if (lot != null){
                        int parseToIntPlate = int.Parse(Regex.Match(lot.Vehicle.LicensePlate, @"\d+").Value);
                        int lastDigit = parseToIntPlate % 10;
                        if (lastDigit % 2 == 0){
                            evenPlates.Add(lot.Vehicle.LicensePlate);
                        }
                    }
                }

                if(evenPlates.Count == 0){
                    Console.WriteLine("Even Plate Numbers Vehicles not found.");
                    return;
                }

                evenPlates.Sort();
                foreach (string plate in evenPlates){
                    Console.WriteLine(plate);
                }
                Console.WriteLine("");

            }

            static int GetAvailableLot(){
                for (int i = 0; i < parkingLots.Count; i++){
                    if (parkingLots[i] == null){
                        return i;
                    }
                }
                return -1;
            }

            static void GetLicensePlateByColor(string color){
                if (totalLots == 0){
                    Console.WriteLine("Parking lot has not been created.");
                }

                List<string> licensePlates = new List<string>();

                foreach (ParkingLot lot in parkingLots){
                    if (lot != null && string.Equals(lot.Vehicle.Color, color, StringComparison.OrdinalIgnoreCase)){
                        licensePlates.Add(lot.Vehicle.LicensePlate);
                    }
                }

                if (licensePlates.Count == 0){
                    Console.WriteLine("No vehicle found on parking lot, with the color {0}", color);
                }else{
                    foreach (string licensePlate in licensePlates){
                        Console.Write("{0}, ", licensePlate);
                    }
                    Console.WriteLine();

                }
            }

            static void GetSlotNumbersByColor(string color){
                if(totalLots == 0){
                    Console.WriteLine("Parking lot has not been created.");
                }

                List<int> slotNumbers = new List<int>();

                for (int i = 0; i < parkingLots.Count; i++){
                    if (parkingLots[i] != null && string.Equals(parkingLots[i].Vehicle.Color, color, StringComparison.OrdinalIgnoreCase)){
                        slotNumbers.Add(i+1);
                    }
                }

                if (slotNumbers.Count == 0){
                    Console.WriteLine("No vehicles found with the color '{0}'", color);
                }else{
                    foreach(int slotNumber in slotNumbers){
                        Console.Write("{0}, ",slotNumber);
                    }
                    Console.WriteLine();
                }
            }

            static void GetSlotNumberByLicensePlate(string licensePlate){
                if (totalLots == 0){
                    Console.WriteLine("Parking lot has not been created.");
                    return;
                }

                int slotNumber = -1;

                for (int i = 0; i < parkingLots.Count; i++){
                    if (parkingLots[i] != null && string.Equals(parkingLots[i].Vehicle.LicensePlate, licensePlate, StringComparison.OrdinalIgnoreCase)){
                        slotNumber = i + 1;
                        break;
                    }
                }

                if(slotNumber == -1){
                    Console.WriteLine("not found.");
                }else{
                    Console.WriteLine(slotNumber);
                }

            }

            static int GetVehicleType(string vehicleType){
                if (string.Equals(vehicleType, "Mobil", StringComparison.OrdinalIgnoreCase)){
                    return 1;
                }else if(string.Equals(vehicleType, "Motor", StringComparison.OrdinalIgnoreCase)){
                    return 2;
                }else{
                    return -1;
                }
            }

            static string GetVehicleTypeName(int type){
                switch(type){
                    case 1:
                        return "Mobil";
                    case 2:
                        return "Motor";
                    default:
                        return "Undefined";
                }
            }

        }
    }

    class Vehicle {
        public int Type {get;}
        public string LicensePlate{get;}
        public string Color{get;}

        public Vehicle(int type, string licensePlate, string color){
            Type = type;
            LicensePlate = licensePlate;
            Color = color;
        }
    }

    class ParkingLot{
        public Vehicle Vehicle {get;}

        public DateTime EntryTime {get;}
        public ParkingLot(Vehicle vehicle, DateTime entryTime){
            Vehicle = vehicle;
            EntryTime = entryTime;
        }
    }
}
