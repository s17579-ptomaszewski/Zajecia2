using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zajecia2.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = @"Middleware/requestsLog.txt";
            context.Request.EnableBuffering();
            String linia = "";

            context.Request.EnableBuffering();
            if(context.Request!=null)
            {
                string path2 = context.Request.Path;
                string method = context.Request.Method;
                string queryString = context.Request.QueryString.ToString();
                string bodyStr = "";
                linia += method + path2;
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                linia += bodyStr;
                linia += queryString;

                using (StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine(linia);
                    file.WriteLine();
                }

            }



            if (_next != null)
            {
                await _next(context);
            }
        }
    }
}
