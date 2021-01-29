using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Option
{
    public class HotelOptionViewModel
    {
        public Guid Id { get; set; }

        public Guid HotelId { get; set; }

        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا هزینه را وارد کنید")]
        public int Price { get; set; }
    }
}
