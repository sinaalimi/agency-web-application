using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Agency.ViewModel.StateCity
{
   public class CreateCityViewModel
    {
        [Required(ErrorMessage = "لطفا استان را انتخاب کنید")]
        [DisplayName("استان")]
        public Guid StateId { get; set; }
        public List<SelectListItem> States { get; set; }

        [Required(ErrorMessage = "لطفا نام شهر را وارد کنید")]
        [DisplayName("شهر")]
        public string Name { get; set; }


        public Guid UserId { get; set; }
    }
}
