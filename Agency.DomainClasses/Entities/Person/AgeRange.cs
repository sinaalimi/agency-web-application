using System.ComponentModel.DataAnnotations;

namespace Agency.DomainClasses.Entities.Person
{
    public enum AgeRange
    {
        [Display(Name = "نوزاد")]
        Baby,
        [Display(Name = "کودک")]
        Child,
        [Display(Name = "بزرگسال")]
        Adult
    }
}
