# StoreApi

Applaudo Technical Test

There is 2 ways of running the app, with physical database or In-Memory approach. To change this running loca, you need to modify the environment variable:

Development -> _In-Memory_
Test -> _SqlServer_


To Run the App in Sql Server you just need to set the Data Source on the Connection String located on the file appsettings.Development.json.

In my case I used a SQL Server SQLEXPRESS instance so currently is
`Data Source=localhost\\SQLEXPRESS;Initial Catalog=StoreDb;Integrated Security=True`

You should modify it depending of the Data Source you have available.

The default data is loaded from the Program Method so there is no need to use other files.

Then To Run the API use the default profile *IIS Express* (uses SSL) or the *StoreApi* with the StoreApi as StartUp project (default).

After run you will see the main view of Swagger documentation, you can use this to test all the Api actions. 

For login you could register a user with the Role *Admin* or *Buyer*, but you can use these default accounts:
 - User: admin01, Password: admin01 *Admin*
 - User: user02, Password: user02 *Buyer*

After login, use the token to authenticate. To access the restricted api's actions you need to add the header:
`Authorization: Bearer <token>`

Postman Api Test Link
https://www.getpostman.com/collections/c42c7952b695b7e3f429
