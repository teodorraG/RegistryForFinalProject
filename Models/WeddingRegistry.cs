using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class WeddingRegistry
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string BrideName { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string GroomName { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string CityOfEvent { get; set; }
        
        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public DateTime DateOfEvent { get; set; }
    }
}
