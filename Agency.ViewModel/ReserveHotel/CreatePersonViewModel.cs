using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.ReserveHotel
{
    public class CreatePersonViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
        [StringLength(11, ErrorMessage = "نام نباید بیش تر از 11 حرف باشد")]
        [DisplayName("نام")]
        public string FName { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام خانوادگی نباید بیش تر از 100 حرف باشد")]
        [DisplayName("نام خانوادگی ")]
        public string LName { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل خود را وارد کنید")]
        [DisplayName("ایمیل")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "ایمیل خود را به درستی وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا شماره موبایل خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "موبایل نباید بیش تر از 11 عدد باشد")]
        [DisplayName("موبایل")]
        public string Mobile { get; set; }

        public Guid RoomHotelId { get; set; }
    }
}
