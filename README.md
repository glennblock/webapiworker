webapiworker
============

This sample demonstrates a Hello API using ASP.NET Web API self-hosted in Worker role on Windows Azure


# A few pointers:

* You need to create an input [endpoint] (https://github.com/glennblock/webapiworker/blob/master/ServiceDefinition.csdef#L6) listening on port 80 externally. 
*	You need to set the WorkerRole to run as "elevated". This is required because self-host sits on top of http.sys which requires elevation
* In WorkerRole.cs you listen on that [endpoint] (https://github.com/glennblock/webapiworker/blob/master/WorkerRole1/WorkerRole.cs#L48)

# What is happening in WorkerRole.cs

* `OnStart` calls the `Listen` method to start the Web Api Host.
* `Listen` does the following
  * The host address is constructed by pulling the Address and Port from the input endpoint. (L48)
  * An `HttpSelfHostConfiguration` object is constructed passing in the host address (L50)
  * The default route is mapped on the config object (L53)
  * An `HttpSelfHostServer` is constructed (L57)
  * The server is opened. (L59)
* `OnStop` closes the server (L42)
