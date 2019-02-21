using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RandomNameGenerator;
using SerializeVSJson.Models;
using SerializeVSJson.Service;

namespace SerializeVSJson.Controllers
{
    public class Status
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public User User { get; set; }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }



    public class HomeController : Controller
    {
        private readonly TemplateRenderService _renderService;

        public HomeController(TemplateRenderService renderService)
        {
            _renderService = renderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> GetTemplate()
        {
            var status = new Status() {
                Date = DateTime.Now,
                User = new User() {
                    FirstName = NameGenerator.GenerateFirstName(Gender.Male),
                    LastName = NameGenerator.GenerateLastName()
                }
            };
            var tpl = await System.IO.File.ReadAllTextAsync("./status.json");
            var tplKey = "statusKey";
            var payload = await _renderService.Render(tplKey, tpl, status);
            return payload.Replace(System.Environment.NewLine, "").Replace(" ","");
        }

        public async Task<string> GetGenerated()
        {

            var card = new AdaptiveCard("1.1");

            var container = new AdaptiveContainer();
            var colSet = new AdaptiveColumnSet();

            var column = new AdaptiveColumn();
            var image = new AdaptiveImage() {
                Style = AdaptiveImageStyle.Person,
                Url = new Uri("https://www.google.de")

            };
            column.Items.Add(image);


            column = new AdaptiveColumn();
            var textBlock = new AdaptiveTextBlock("Updated his Status")
            {
               Weight= AdaptiveTextWeight.Bolder,
               Wrap = true

            };
            column.Items.Add(image);
            colSet.Columns.Add(column);

            column = new AdaptiveColumn();
            textBlock = new AdaptiveTextBlock("Updated his Status")
            {
                Spacing = AdaptiveSpacing.None,
                Weight = AdaptiveTextWeight.Bolder,
                Wrap = true

            };
            column.Items.Add(image);
            column.Items.Add(textBlock);
            colSet.Columns.Add(column);

            container.Items.Add(colSet);
            card.Body.Add(colSet);


            container = new AdaptiveContainer();
            colSet = new AdaptiveColumnSet();


            column = new AdaptiveColumn();
            image = new AdaptiveImage()
            {
                HorizontalAlignment = AdaptiveHorizontalAlignment.Right,
                Url = new Uri("https://www.google.de"),

            };
            column.Items.Add(image);


            column = new AdaptiveColumn();
            textBlock = new AdaptiveTextBlock("Status Message")
            {
                Weight = AdaptiveTextWeight.Bolder,
                Wrap = true

            };
            column.Items.Add(image);
            column.Items.Add(textBlock);
            colSet.Columns.Add(column);

            column = new AdaptiveColumn();
            textBlock = new AdaptiveTextBlock("Updated his Status")
            {
                Spacing = AdaptiveSpacing.None,
                Weight = AdaptiveTextWeight.Bolder,
                Wrap = true

            };
            column.Items.Add(image);
            colSet.Columns.Add(column);

            container.Items.Add(colSet);
            card.Body.Add(colSet);


            var returndata = JsonConvert.SerializeObject(card);
            return returndata;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
