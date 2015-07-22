using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KatanaIntroPart2
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    using System.Web.Http;

    class Program
    {
        static void Main ( string[] args )
        {
            string uri = "http://localhost:8080";
            using (WebApp.Start<StartUp>(uri))
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stopping");
            }
        }
    }
    public class StartUp
    {
        public void Configuration ( IAppBuilder app )
        {

            //app.Use(async ( environment, next ) =>
            //{
            //    foreach (var pair in environment.Environment)
            //    {
            //        Console.WriteLine("{0}:{1}", pair.Key, pair.Value);
            //    }
            //    await next();
            //});

            app.Use(async ( environemnt, next ) =>
            {
                Console.WriteLine("Requesting: " + environemnt.Request.Path);

                await next();

                Console.WriteLine("Response: " + environemnt.Response.StatusCode);
            });

            CongifureWebApi(app);
            app.UseHelloWorld();
        }

        private void CongifureWebApi ( IAppBuilder app )
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi",
                "api/{controller}/{id}",
                new
                {
                    id = RouteParameter.Optional
                });
            app.UseWebApi(config);
        }

    }
    public static class AppBuilderExtenstion
    {
        public static void UseHelloWorld ( this IAppBuilder app )
        {
            app.Use<HelloWorldComponent>();
        }
    }
    public class HelloWorldComponent
    {
        AppFunc _next;
        public HelloWorldComponent ( AppFunc next )
        {
            _next = next;
        }


        public Task Invoke ( IDictionary<string, object> environment )
        {
            var response = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response))
            {
                return writer.WriteAsync("Hello2!!");
            }

        }
    }




}
