# Consumer Cellular SDET - API Test Suite
NUnit test suite written in C# demonstrating API test automation against a REST endpoint.

## What this covers
- GET request validation (status code, response structure, data integrity)
- POST request validation (object creation, status code)
- Null-safe response handling
- SetUp/TearDown lifecycle management with RestSharp

## Tech stack
- C# / .NET 10
- NUnit
- RestSharp

## Running the tests
```
dotnet test
```

## Notes
The suite targets [JSONPlaceholder](https://jsonplaceholder.typicode.com/), a public REST API 
used as a stand-in for real endpoint testing. Test structure mirrors how I would approach 
automated checks within a domain framework. Organized by endpoint, asserting on status codes 
and response content, with future possibilities for greater input validation ranges and 
data cleanup on write operations.