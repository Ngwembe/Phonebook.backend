# Phonebook Backend solution
This repository has the back-end services that are used to create and retrieving contact details. 

The solution is written in C#, targeting the .NET 6.0 (LTS). 

The services has Docker support enabled. The services will run on Dcker containers if one has Docker Desktop installed on the device.

In order to narrow the scope of the project. I've disabled the JWT token verification with the code intact for reference purposes. The auth n and auth z functionality is found in the #Absa.Phonebook.Authservice service. 

In order to run the solution, open it on Visual Studio or any of your favourite IDE. Select multiple project run on the solution level to ensure all the services will be running when the client application does the HTTP requests.
