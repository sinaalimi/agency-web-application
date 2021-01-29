using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Agency.ViewModel.Slider
{
    public class AddSlideViewModel
    {
        [DisplayName("انتخاب تصویر")]
        [Required(ErrorMessage = "لطفا عکس را انتخاب کنید")]
        public HttpPostedFileBase PicSrFile { get; set; }
        public string PicSrc { set; get; }


        [DisplayName("عنوان اسلاید")]
        [Required(ErrorMessage = "لطفا عنوان اسلاید را وارد کنید")]
        public string Title { get; set; }

        [DisplayName("عنوان دکمه")]
        //[Required(ErrorMessage = "لطفا عنوان دکمه را وارد کنید")]
        public string ButtonTitle { get; set; }


        [DisplayName("لینک")]
        //[Required(ErrorMessage = "لطفا لینک را وارد کنید")]
        public string Link { get; set; }

        [DisplayName("توضیحات")]
        
        public string Describ { get; set; }


        [RegularExpression(@"^[0-9]+$")]
        [Required(ErrorMessage = "لطفا اندیس اسلاید را وارد کنید")]
        [DisplayName("اندیس اسلاید")]

        public int Index { get; set; }


    }
}
