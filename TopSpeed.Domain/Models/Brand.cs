﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Domain.Common;

namespace TopSpeed.Domain.Models
{
        public class Brand  : BaseModel
        {



            [Required]
            public string Name { get; set; } 


            [Display(Name = "Establish Year")]

            [Required]
            public int EstablishedYear { get; set; }
        [Display(Name = "Brand Logo ")]
        public string BrandLogo { get; set; } = string.Empty;
        }
}
