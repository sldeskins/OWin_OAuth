using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatanIntro
{
    using System.IO;
    using AppFunc = Func<IDictionary<string, object>, Task>;

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
            //// Note: after starting the app. open a browser with the uri above, http://localhost:8080, to see the example outout

            //// exmple 2 getting the welcome page or other page for app
            //app.UseWelcomePage();

            ////example 1 - having hello world show in the browser
            //app.Run(ctx =>
            //{
            //    return ctx.Response.WriteAsync("Hello world!");
            //});

            ////before example 11
            //app.Use<HelloWorldComponent>();

            ////example 11B - now looks like 2
            app.UseHelloWorld();
        }

    }

    ////Example 11A - to get something to look like Example 2 using our created "Hello!!"
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
        ////Example -unaliased
        //public HelloWorldComponent (Func<IDictionary<string, object>, Task> next)
        ////Example  - aliased using AppFunc - see using AppFunc = Func<IDictionary<string, object>, Task>; place earlier in the class
        public HelloWorldComponent ( AppFunc next )
        {
            _next = next;
        }

        ////Example - throws an error since it does not conform to the OWIN spec for return
        //public Task Invoke ( IDictionary<string, object> environment )
        //{
        //   return null;
        //}

        ////Example - see all the key values in the environment - put break point where method is entered
        //public async Task Invoke ( IDictionary<string, object> environment )
        //{
        //    await _next(environment);
        //}

        //Example 10 
        public Task Invoke ( IDictionary<string, object> environment )
        {
            var response = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response))
            {
                return writer.WriteAsync("Hello!!");
            }

        }
    }
}

