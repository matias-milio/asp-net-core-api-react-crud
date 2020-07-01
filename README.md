# MY STORE CRUD WITH ASP.NET NET CORE WEB API + REACT REDUX 

This application carries the same funcionality of the "SIMPLE STORE CRUD IN ASP.NET MVC 5" project but done with .NET Core and React.

It uses features like:
* N-Layer Arquitecture 
* Entity Framework Core (Models done with Scaffold-Db)
* Repository Pattern
* Unit Of Work Classes
* AutoMapper with custom profiles
* NET Core Built in Dependency Injection
* Options Pattern for specific configurations
* Logging with Nlog
* Cached query responses
* Connection string in secrets.json
* Cross-Origin Requests (CORS) to allow async calls from the React app

**Upcoming features**
* Server-Side validation.
* Use of .NET Unit Testing Suite.

React app uses:
* Redux to manage state
* Axios to make api calls
* Material UI Components
* Toast Notifications
* Styled Components

This time I added SQL Authentication to the database to alter the connection string and show some use of db roles.</br>
**The user for the db is "dev" and password "abc123".**

To run this project locally you need to:
* Run the database script
* Build your connection string with the given credentials under the key `DevConnectionString` and store it in appsettings.json or create your secrets.js file.
* Check for the url in the `app.UseCors(options => options.WithOrigins({YOUR_REACT_APP_LOCALHOST})` line in the `Startup.cs` class.
* Run the NET Core API on your local server instance.
* Open the react app and check under `src\actions\api.js` the value of `baseUrl` to match the local server instance of your NET Core API project. For Example: `const baseUrl = "http://localhost:53808/api/"`
* That should be it, run the react app `npm start` and test it for yourself.
