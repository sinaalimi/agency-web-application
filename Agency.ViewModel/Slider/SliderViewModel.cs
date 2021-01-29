using System;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Slider
{
    public class SliderViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public string PicSrc { get; set; }

        public string Title { get; set; }

        public string Describ { get; set; }

        public string ButtonTitle { get; set; }

        public string Link { get; set; }

        public int Index { get; set; }

        public Guid UserId { get; set; }
    }
}
