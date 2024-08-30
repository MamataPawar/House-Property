using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is empty")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Country is empty")]
        [StringLength(50,MinimumLength =2)]
        public string Country { get; set; }=string.Empty;
    }
}