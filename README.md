# dotnet-sql-codingchallenge

## Database
Create 2 tables in the SQL database:

**Car**
- ID
- Name
- Colour
- Price


**Sales**
- ID
- CarID
- Date

## Web API
Create a new webapi project in C# core with following endpoints:

POST _/cars_
```
{
  name,
  colour,
  price
}
```

GET _/cars_
```
[{
  id,
  name,
  colour,
  price
}]
```

GET _/cars/:carId_
```
{
  id,
  name,
  colour,
  price
}
```

PATCH _/cars/:carId_
```
{
  id,
  name,
  colour,
  price
}
```

DEL _/cars/:carId_ - deletes this car from the db

POST _/sales_
```
{
  carId,
  date
}
```

GET _/sales?startDate=yyyymmdd&endDate=yyyymmdd_
```
[{  
  carName,
  carColour,
  quantity,
  month,
  year
}]
```
e.g. **GET _/sales?startDate=20230701&endDate=20231231** - this will return all sales from 01-July-2023 to 31-Dec-2023

## UI
Create a simple angular SPA front-end which shows the sales of all the cars, where start date and end date can be user-inputs.
- The styling in UI is not important, just a simple table will be sufficient.

# Objectives:
- Web API in C# core
- Angular SPA
- Unit Tests for both backend and frontend
- Use of Dependency Injection
- Clean code which is easier to read and best practices followed
- It is expected to be completed in a week and should not take more than 4 hours in one stretch.

## Bonus Objectives
- Authentication (preferably JWT) - it's ok to hard-code to show the concept of authentication
- Usage of Entity Framework and LINQ to connect with the database
