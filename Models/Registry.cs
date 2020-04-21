using RegistryForFinalProject.Constants;
using RegistryForFinalProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class Registry
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string Name { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string Location { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public DateTime DateOfEvent { get; set; }

        [Required]
        public RegistryType RegistryType { get; set; }

        public List<RegistryItems> Items { get; set; } = new List<RegistryItems>();

        [Required]
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
