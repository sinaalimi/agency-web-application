using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.StateCity
{
    public class EditStateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "لطفا نام استان را وارد کنید")]
        [DisplayName("استان")]
        public string Name { get; set; }
    }
}
