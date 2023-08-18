# Transaction Management API

This is a Transaction Management API built with .NET 6.0 and MongoDB.

## Getting Started

1. Access [mongodb.com](https://www.mongodb.com/) and create a new MongoDB instance.

2. Replace the connection string in the file `appsettings.json` located in the `.API` domain with your MongoDB connection string:

   ```json
   "MongoDb": {
       "MongoConnection": "YOUR_CONNECTION_STRING_HERE",
       "MongoDatabase": "your-database-name"
   }
   
You can run the application either via Docker or via IIS.

Running via Docker
sh
Copy code
docker build -t transaction-api .
docker run -p 8080:80 transaction-api
The API will be available at http://localhost:8080.

Running via IIS
Open the solution in Visual Studio with .NET 6.0 installed and press F5 to run the application.

Prerequisites
.NET 6.0 SDK
Endpoints
GET /api/TransactionSummary: Get transaction summaries grouped by date.

POST /api/Transaction: Create a new transaction.

DELETE /api/Transaction/{id}: Delete a transaction by ID.

Contributors
Enrique Hinojosa

Contact Information
License
This project is licensed under the MIT License.

