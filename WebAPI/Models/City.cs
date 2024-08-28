using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public string Country { get; set; }=string.Empty;
        public DateTime LastUpdatedOn { get; set; }
        public int LastupdatedBy { get; set; }
    }
}