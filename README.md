# Far Delivery Service

This project is a api to manage points, routes and search for the min time delivery between two given points. The search uses the branch-and-bound algorithm.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

.Net Core 2.1

### Running

Under src/DeliveryService.Api/ folder run :

```
dotnet restore
dotnet run
```

## Running the tests

Go to the folder of the project that you want to test (DeliveryService.Core.Tests or DeliveryService.Api.Tests) and run :

```
dotnet test
```

## Built With

* .NET Core 2.1
* ASP.NET Core 2.1.3
* Entity Framework Core 2.1.3
* XUnit 2.3.1
* Moq 4.10.0
* FluentAssertions 5.4.2

## Authors

* **Billie Thompson** - *Initial work*

## TO DO LIST

* Add Authentication and Authorization to the api
* Add messaging support


