using System.ComponentModel.DataAnnotations;

namespace FireOnWheels.Web.ViewModels
{
    public class Location

    {

        [Key]

        public int Key { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }

        public string City { get; set; }



    }
}
