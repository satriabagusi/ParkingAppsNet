# Simple Parking Apps using .NET 6

This Project created using .NET SDK Version 6.0


## Installation
1. Firstly, Clone or Download this Repo to your local directory
2. You need .NET SDK (https://dotnet.microsoft.com/en-us/download)
3. Open terminal from your IDE or direct open terminal from local directory  



## Run Locally

Go to the project directory

```bash
  cd my-project
```

Run the Project from Terminal

```bash
  dotnet run
```

Create Parking Lot

```bash
  create_parking_lot <lot_number>
  
  e.g :
  $ create_parking_lot 3
```
Input Vehicle to Parking Lot
```bash
  park <plate_number> <vehicle_color> <vehicle_type>

  e.g :
  $ park B-1234-XYZ Putih Mobil
  $ park B-3141-ZZZ Hitam Motor
```
Remove Vehicle from Parking Lot
```bash
  leave <lot_number>

  e.g :
  $ leave 5
```

See List Vehicle on Parking Lot
```bash
  $ status
```

Total Available Lot
```bash
  $ total_available_lot
```

Total Filled Lot
```bash
  $ total_filled_lot
```

See total vehicle based on Vehicle Type
```bash
  type_of_vehicles <vehicle_type>

  e.g :
  $ type_of_vehicles Motor
  $ type_of_vehicles Mobil
```

See list vehicle based on Odd or Even Plate Number
```bash
  $registration_numbers_for_vehicles_with_odd_plate

  e.g :
  $ registration_numbers_for_vehicles_with_even_plate
```

Find vehicle lot number based on Vehicle Color
```bash
  slot_numbers_for_vehicles_with_colour <color>

  e.g :
  $ slot_numbers_for_vehicles_with_colour Red
```

Find vehicle license plate based on Vehicle Color
```bash
  registration_numbers_for_vehicles_with_colour <color>

  e.g :
  $ registration_numbers_for_vehicles_with_colour Red
```

Find vehicle lot number based on Plate Number
```bash
  slot_number_for_registration_number <plate_number>

  e.g :
  $ slot_number_for_registration_number B-1234-XYZ
```


