# LGC-API-Testing-challenge2
API testing using Nunit

# API Testing with NUnit, RestSharp, and ExtentReports

This NUnit Test Project in Visual Studio is a set of API tests written in C# using NUnit as the testing framework, RestSharp for making HTTP requests, and ExtentReports for generating test reports.
- **TestProject1** is the project solution
- **APITesting.csproj** is the test project
- **UnitTest1.cs** is the test containing all the tests
- **TestReport.html** is the html report generated.

## Prerequisites

Ensure you have the following software installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- [NUnit](https://nunit.org/)
- [RestSharp](https://restsharp.dev/)
- [ExtentReports](https://extentreports.com/)

# You can install the required NuGet packages using either step1 or step2

1.In terminal use the following commands:

```bash
dotnet add package NUnit
dotnet add package RestSharp
dotnet add package ExtentReports --version 4.1.1 

2. Commands used in the NuGet Package Manager Console in Visual Studio.

Install-Package NUnit
Install-Package RestSharp
Install-Package ExtentReports -Version 4.1.1 
```

## Installation
Clone the repository:

```bash
git clone https://github.com/kavyaputta92/LGC-API-Testing-challenge2.git
cd LGC-API-Testing-challenge2\TestProject1 
```
# Install the dependencies:

```bash
dotnet restore
```
## Running the Tests
You can run the tests using the following command:

```bash
dotnet test
```
## Test Scenarios

The project contains the following API test cases:

Test_GetPostById:
Sends a GET request to fetch a post by ID.
Verifies that the response status code is 200 and the response contains the expected userId.

Test_CreateNewPost:

Sends a POST request to create a new post.
Verifies that the response status code is 201 and the response contains the expected title.
Test_UpdatePost:

Sends a PUT request to update an existing post.
Verifies that the response status code is 200 and the response contains the updated title.
Test_DeletePost:

Sends a DELETE request to delete a post by ID.
Verifies that the response status code is 200 or 204, and the response body is empty.


Alternatively you can execute the tests from visual studio IDE, Test>Run All Tests
## Generating Reports
The test report is generated automatically in the root directory under \bin\Debug\net8.0 as TestReport.html. It includes detailed information about each test case, including status, request/response details, and any errors encountered.