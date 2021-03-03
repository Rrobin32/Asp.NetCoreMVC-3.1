using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* Custome MiddleWare */
            /* 
             * We can have "n" number of middleware in a program but the only thing is important is the order of the middleware in which they are used.
             * "next()" is used to call the next middleware. and the response is in the same manner in whichn the middleware is called.
             * Asp.Net Core create an HTTP Application Pipeline that processes the request.
             * The Http Pipleline is congigure in configure method of Startup.cs.
             * All the request to the application goes through the HTTP pipeline.
             * An middleware is a piece of code(Component) which is used in http pipeline.
             * in an application we can use mutliple middleware.
             * Middleware has access to all the request and response.

             */
            app.Use(async (Context, next) =>
                {
                    await Context.Response.WriteAsync("Hello my first Middleware");

                    await next();

                    await Context.Response.WriteAsync("\nHello my first Middleware after next method is called");
                });

            app.Use(async (Context, next) =>
            {
                await Context.Response.WriteAsync("\nHello my second Middleware");

                await next();

                await Context.Response.WriteAsync("\nHello my second Middleware after next method is called");
            });

            app.Use(async (Context, next) =>
                {
                    await Context.Response.WriteAsync("\nHello my third Middleware");

                    await next();
                });

            /* To map a url with the particular resource.
             * Order of middleware is very important if we remove this  "app.UseRouting();" this the program will throw an error.
             * If we move this middleware to the last then also the program will throw an error.
             */
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                /* mapping a url with the particular resource. */
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("\nHello World!");
                });

                /* mapping a url with the particular resource. */
                endpoints.Map("/mvc", async context =>
                {
                    await context.Response.WriteAsync("\nWelcome to the ASP.Net Core MVC!");
                });
                /* Difference between Map() and MapGet() is MapGet() only handele the get() for the particular Route but Map() will handele all request comming to the 
                   Particular Route.  
                 */
            });
        }
    }
}
