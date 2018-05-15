# TogglAPI.Net

TogglAPI.Netstandard is a .Net Standard C# wrapper for the toggl.com api

## Warning!
For correct usage you need to change default client-key. You can find it on bottom of the page (https://www.toggl.com/app/profile). You need a **pro** version to use tasks, otherwise some tests will fail.

## Code Samples

The heavy lifting happens in the dir Toggl/Services.
In the services you can create an instance of a service with an api key or a default api key located in the app.config.
the api key 2d1d95cef10e17831ec505e9e6f9f7ea is a test account so please use it as you see fit.

## Get List Of Clients

        var srv = new ClientService();
        var obj = srv.List();

###OR

        var t = new Toggl.Toggl();
        var obj = t.Client.List();

## Get Current User

        var apiKey="2d1d95cef10e17831ec505e9e6f9f7ea";
        var usrSrv = new Toggl.Services.UserService(apiKey);
        var c = usrSrv.GetCurrent();
        Console.WriteLine(c.FullName);
        Console.WriteLine(c.Email);

###OR

        var apiKey="2d1d95cef10e17831ec505e9e6f9f7ea";
        var t = new Toggl.Toggl(apiKey);
        var c = t.User.GetCurrent();
        Console.WriteLine(c.FullName);
        Console.WriteLine(c.Email);

## Get Hours Worked For Time Period

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

###OR

        var timeSrv= new Toggl.Toggl().TimeEntry;
        var prams = new TimeEntryParams();

        // there is an issue with the date ranges have
        // to step out of the range on the end.
        // To capture the end of the billing range day + 1
        prams.StartDate = Convert.ToDateTime("11/1/2012");
        prams.EndDate = Convert.ToDateTime("11/16/2012");

        var hours = timeSrv.List(prams)
                                .Where(w=>!string.IsNullOrEmpty(w.Description));

        hours.Select(s=>s).Dump();

## For more code samples check out the the Toggl.Tests Project

## Copyright

Copyright (c) 2014 Pirozhenko Ilya. See LICENSE for details.