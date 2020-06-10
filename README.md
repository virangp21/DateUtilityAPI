Notes
============

This project is created to build Date Calculator API that calculates number of working days (Monday to Friday) between given dates for Australia. API calculate number of working days by counting working days as Monday to Friday excluding any public holidays.  

Dependency Injection is used to make it easier to create required objects and decouple application layers. Application is build using  C# and .NET Core 3.1  

There are two variants of API. One that takes in from and to dates and return number of working days between those dates. This API only takes into account public holidays observed by all Australian states.

Another version of API takes three arguments from date, to date and state. This API takes into account public holidays observed by individual Australian state to calculate working days between given dates.

There are 4 projects in solution.

DateUtilityAPI - Contains the main logic for API Controller. Swagger is added to provide API documentation.

DateUtilityAPI.Domain - Contains the domain objects used in application. State enum is created to hold state values.

DateUtilityAPI.Data - Contains the logic to calculate public holidays. In this solution InMemoryDatabase class is created to cater for calculating public holiday dates. Ideally this can be replaced with data coming from a database table that can hold public holiday dates in required format.

DateUtilityAPI.Tests - Contains unit tests for the project.

 

 
