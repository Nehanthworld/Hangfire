using Hangfire;
using HangfireApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireApp.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Fireandforget()
        {
            BackgroundJob.Enqueue(() => SampleMethod());
            return View("Index");
        }
        public IActionResult Delayed()
        {
            BackgroundJob.Schedule(() => SampleMethod(), TimeSpan.FromMinutes(1));
            return View("Index");
        }
        public IActionResult Recurring()
        {
            RecurringJob.AddOrUpdate(() => SampleMethod(), Cron.Minutely);
            return View("Index");
        }
        public IActionResult Continuations()
        {
            var jobId = BackgroundJob.Schedule(() => SampleMethod(), TimeSpan.FromDays(7));

            BackgroundJob.ContinueJobWith(jobId, () => SampleMethod());

            return View("Index");
        }
        public void SampleMethod()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Welcome to ff jobs");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
