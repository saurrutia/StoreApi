# StoreApi
Applaudo Technical Test
To Run the App you just need to set the Data Source on the Connection String located on the file appsettings.Development.json.
In my case I used a SQL Server SQLEXPRESS instance so currently is
Data Source=localhost\\SQLEXPRESS;Initial Catalog=StoreDb;Integrated Security=True
You should modify it depending of the Data Source you have available.

The default data is loaded from the OnModelCreating method in the StoreDbContext so there is no need to use other files.
