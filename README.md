## C--Toggl-Api-Client
	C--Toggl-Api-Client is a C# wrapper for the toggl.com api
 
### Code Samples
	The heavy lifting happens in the services dir.
	In the services you can create an instance of a service with an api key or a default api key located in the app.config.
	the api key 2d1d95cef10e17831ec505e9e6f9f7ea is a test account so please use it as you see fit.

#### Get List Of Clients
	var srv = new ClientService();
	var obj = srv.List();

#### Get Current User
	var apiKey="2d1d95cef10e17831ec505e9e6f9f7ea";
	var usrSrv = new Toggl.Services.UserService(apiKey);
	var c = usrSrv.GetCurrent();
	Console.WriteLine(c.FullName);
	Console.WriteLine(c.Email);

#### Get Hours Worked For Time Period
	var apiKey="2d1d95cef10e17831ec505e9e6f9f7ea";
	var timeSrv= new Toggl.Services.TimeEntryService(apiKey);
	var prams = new TimeEntryParams();
	
	// there is an issue with the date ranges have 
	// to step out of the range on the end. 
	// To capture the end of the billing range day + 1
	prams.StartDate = Convert.ToDateTime("11/1/2012");
	prams.EndDate = Convert.ToDateTime("11/16/2012");

	var hours = timeSrv.List(prams)
				.Where(w=>!string.IsNullOrEmpty(w.Description));
	
	hours.Select(s=>s);


#### For More code samples check out the the Toggl.Tests Project
 