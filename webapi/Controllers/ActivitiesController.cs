using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using Test1.Models;


namespace Test1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly ILogger<ActivitiesController> _logger;

        private Activity[] activities;




        public ActivitiesController(ILogger<ActivitiesController> logger)
        {
            _logger = logger;
            //Init_InMemory();
            Init_FromFile();
        }

        private void Init_InMemory()
        {
            var marsRover = new Project { Id = 1, Name = "Mars Rover" };
            var manhattan = new Project { Id = 2, Name = "Manhattan" };

            activities = new Activity[] {
            new Activity() { Project = new Project { Id = 1, Name = "Mars Rover" }, Employee = new Employee{ Id=1, Name="Mario" }, Date=new DateTime(2021,08, 26),Hours=5 },
            new Activity() { Project = new Project { Id = 2, Name = "Manhattan" }, Employee = new Employee{ Id=2, Name="Giovanni" }, Date=new DateTime(2021,08, 30),Hours=3 },
            new Activity() { Project = new Project { Id = 1, Name = "Mars Rover" }, Employee = new Employee{ Id=1, Name="Mario" }, Date=new DateTime(2021,08, 31), Hours=3 },
           };
        }


        private void Init_FromFile()
        {
            string fileName = "example-data.json";
            string jsonString = System.IO.File.ReadAllText(fileName);
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            activities = JsonSerializer.Deserialize<Activity[]>(jsonString, options)!;
        }

        [HttpGet]
        public IEnumerable<Activity> Get([FromQuery] string? aggregations = null)
        {
            if (!string.IsNullOrWhiteSpace(aggregations))
            {   
                

                var items = activities
                // dynamic aggregation using an anonymous type
                .GroupBy(x => new {
                    Project = aggregations.Contains("project") ? x.Project : null,
                    Employee = aggregations.Contains("employee") ? x.Employee : null,
                    Date = aggregations.Contains("date") ? x.Date : null
                }, x => x.Hours)
                .Select(i => new Activity
                {
                    Project = i.Key.Project,
                    Employee = i.Key.Employee,
                    Date = i.Key.Date,
                    Hours = i.Sum()
                })
                //.OrderBy(x => x.Project)
                .DynamicOrder(aggregations.Split(","))
                .ToArray();

                return items;
            }
            else
            {
                return activities; 
            }

        }
    }


    //https://stackoverflow.com/questions/3319730/linq-sorting-on-many-columns-dynamically
    public static class DynamicExtentions
    {
        public static IEnumerable<T> DynamicOrder<T>(this IEnumerable<T> data, string[] orderings) where T : class
        {
            var orderedData = data.OrderBy(x => x.GetPropertyDynamic(orderings.First()));
            foreach (var nextOrder in orderings.Skip(1))
            {
                orderedData = orderedData.ThenBy(x => x.GetPropertyDynamic(nextOrder));
            }
            return orderedData;
        }

        public static object GetPropertyDynamic<Tobj>(this Tobj self, string propertyName) where Tobj : class
        {
            var param = Expression.Parameter(typeof(Tobj), "value");
            var getter = Expression.Property(param, propertyName);
            var boxer = Expression.TypeAs(getter, typeof(object));
            var getPropValue = Expression.Lambda<Func<Tobj, object>>(boxer, param).Compile();
            return getPropValue(self);
        }
    }


}
