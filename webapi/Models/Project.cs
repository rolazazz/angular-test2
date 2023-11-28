using webapi.Models;

namespace Test1.Models
{
    public class Project : BaseClass
    {
        public string Name { get; set; }



        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] { Id, Name };
        }

    }
}