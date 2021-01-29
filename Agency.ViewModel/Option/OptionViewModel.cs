using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Option
{
    public class OptionViewModel
    {
        public Guid Id { get; set; }

        public Guid TourId { get; set; }

        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        public string Title { get; set; }

        [Required(ErrorMessage = "لطفا هزینه را وارد کنید")]
        public int Cost { get; set; }
    }
}
