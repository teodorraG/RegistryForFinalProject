using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class BabyRegistry
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string Name { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string City { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public DateTime DateOfEvent { get; set; }
    }
}
