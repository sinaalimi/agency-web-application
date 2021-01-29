using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Entities.Hotel
{
    public enum RoomType
    {
        [Display(Name = "شخصی")]
        Personal,
        [Display(Name = "عمومی")]
        General
    }
}
