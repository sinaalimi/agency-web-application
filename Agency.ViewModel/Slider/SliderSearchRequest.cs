using System.ComponentModel;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Slider
{
    public class SliderSearchRequest : BaseSearchRequest
    {
        public SliderSearchRequest()
        {
            CurrentSort = "Index";
        }

        [DisplayName("اندیس")]
        public int Index { get; set; }

        public string Title { set; get; }


        public static class ApplicantSortBy
        {
            public const string Index = nameof(Index);
            public const string Title = nameof(Title);
        }
    }
}
