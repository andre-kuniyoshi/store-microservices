# Store-Microservices
Under development...

Store-microservices is a sample containerized application. It's purpose is to use microservice architecture pattern in practice.

The application consists of a simple online store where you can carry out a purchase flow.

# What are you find in this repository
This application has 6 API Rest, 1 Grpc Server, 1 Api gateway and 1 MVC application. The databases used are SQL Server, PostgreSQL, MongoDB and Redis. The Message broker is RabbitMQ and the services use MassTransit package. All of the applications are containerized and you can run using docker-compose.

Below you will find descriptions of each service and the technologies used.

### - Identity (Authorization Server)
This is an OAuth 2.0/OpenID Connect server. It's implemented using OpenIddict framework.

* ASP.NET Core MVC application
* SQL Server database connection
* OpenIddict - OAuth 2.0/OpenID Connect

### - Basket
This services have the responsability of save the client's cart.

* ASP.NET Core Web API application
* REST API principles, CRUD operations
* Redis Cache connection
* Repository Pattern

### - Catalog
This services have the responsability of the products.

* ASP.NET Core Web API application
* REST API principles, CRUD operations
* MongoDB database connection
* Repository Pattern

### - Register
This services have the responsability of save the client's informations, like address.

* ASP.NET Core Web API application
* REST API principles, CRUD operations
* SQL Server database connection
* Repository Pattern
* Entity Framework Core

### - Inventory
### - Order
### - Api Gateway

### - ASP.NET Core MVC

# Run the project