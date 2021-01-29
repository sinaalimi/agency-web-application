using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Entities.Person
{
    public enum Ages
    {
        [Display(Name="نوزاد")]
        Baby,
        [Display(Name = "کودک")]
        Child,
        [Display(Name = "بزرگسال")]
        Adult
    }
}
