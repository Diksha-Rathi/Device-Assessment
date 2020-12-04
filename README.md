# Device Assessment
Proposed solution for given problem statement - battery replacement monitoring for devices

## Application Setup

### Run using Visual Studio

1. Open .sln file and restore nuget package.
2. Set DeviceAssessment.API as startup project.
3. Run the application using IISExpress. This will open the swagger window to test the API.

To run unit test cases - Build the solution, and run tests from test explorer.

### Run using Visual Studio Code

Execute the following commands in given order from the .sln directory- 

1. `dotnet restore`
2. `dotnet build`
3. `dotnet run --project .\DeviceAssessment.API\DeviceAssessment.API.csproj`

To run unit test cases - `dotnet test`

Validate the API from Postman ([Download](https://www.postman.com/downloads/)). Create post request for `https://localhost:5000/api/battery/monitor`.

## Assumptions

1. The battery performance data sent to API in JSON is strictly provided for 7 days (1 week) only.
2. Data accuracy is maintained in data sent to the API i.e. the data is valid and values that can result in negative drop in percentage are not present.
3. Battery consumption lies within the range [0, 100].
4. Where data is not available (e.g. no data is available for days 3- 7), the battery consumption is assumed as 0.
5. More than one employee can use the tablet on a given day.

## Design

The Web API design has 3 Layers - 
1. Presentation (API) Layer
2. Business Layer
3. Infrastructure(core) Layer

The solution is built in .NET Core 3.1

Swashbuckle has been integrated for API documentation.

Naming convention has been followed such that the service can be extended to assess other devices like - laptop as well with minimum change to existing methods, entities or design. 

#### Algorithm - 

1. Group JSON results by serial number.
2. Sort groups in ascending order of date.
3. If number of data points for a given serial number
   * Equals 1: Add "unknown" to result
   * Greather than 1: 
      If for a given date, the available data is-
      * Greate than 1: Find average for 24 hours.
      * Equal to 1: Find average from the first nearest almost full charge value and reset previously calculated average

## Future scope of work

- [ ] Add integration test.
- [ ] Add logger.
- [ ] Add global exception handling.
- [ ] Create dependency injection layer to improve code decoupling.
- [ ] Improve algorithm to factor in difference in minutes for better accuracy in results and analysis of battery health.
