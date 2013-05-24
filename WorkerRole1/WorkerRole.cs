using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private HttpSelfHostServer _server;

        public override void Run()
        {
            while (true)
            {
                Thread.Sleep(10000);
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            Listen();
            return base.OnStart();
        }

        public override void OnStop()
        {
            _server.CloseAsync().Wait();
            base.OnStop();
        }

        private void Listen()
        {
            var ip = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["HttpIn"].IPEndpoint;
            string address = string.Format("http://{0}:{1}/", ip.Address, ip.Port);
            var config = new HttpSelfHostConfiguration(address);

            
            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            _server = new HttpSelfHostServer(config);
            
            _server.OpenAsync().Wait();
        }

        
    }
}
