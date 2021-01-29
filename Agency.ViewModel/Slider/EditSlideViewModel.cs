using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Agency.ViewModel.Slider
{
    public class EditSlideViewModel
    {
        public Guid Id { get; set; }


        [DisplayName("انتخاب تصویر")]
        public HttpPostedFileBase PicSrFile { get; set; }
        public string PicSrc { set; get; }


        [DisplayName("عنوان اسلاید")]
        public string Title { get; set; }


        [DisplayName("توضیحات")]
        public string Describ { get; set; }

        [DisplayName("عنوان دکمه")]
        public string ButtonTitle { get; set; }


        [DisplayName("لینک")]
        public string Link { get; set; }


        [RegularExpression(@"^[0-9]+$")]
        [Required(ErrorMessage = "لطفا اندیس اسلایدی که میخواهید جایگزین این اسلاید شود را وارد کنید")]
        [DisplayName("اندیس اسلاید")]

        public int Index { get; set; }


    }
}
