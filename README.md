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

POST _/sales_
```
{
  carId,
  date
}
```

GET _/sales?month=5&year=2023_
```
[{  
  carName,
  carColour,
  quantity,
  month,
  year
}]
```

## UI
Create a simple angular SPA front-end which shows the sales of all the cars, where month and year can be user-inputs. It will be bonus to extend this to be able to select month range for the report (e.g. July-2022 to Dec-2023).
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
