using System.Collections.Generic;
using System.Web.Mvc;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Slider
{
    public class SliderListViewModel
    {
        public SliderListViewModel()
        {
            #region SortOrderList
            SortOrderList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = SortDirectionMode.Desc,
                    Text = "نزولی"

                }
                ,
                new SelectListItem
                {
                    Value = SortDirectionMode.Asc,
                    Text = "صعودی"
                }
            };
            #endregion

            #region SortableList

            SortableList = new List<SelectListItem>
            {
                //new SelectListItem
                //{
                //    Value = SortByMode.CreatedOn,
                //    Text = "تاریخ درج"
                //},
                //new SelectListItem
                //{
                //    Value = SortByMode.ModifiedOn,
                //    Text = "تاریخ آخرین تغییر"
                //},
                new SelectListItem
                {
                    Value = SliderSearchRequest.ApplicantSortBy.Index,
                    Text = "اندیس"
                },
                new SelectListItem
                {
                    Value = SliderSearchRequest.ApplicantSortBy.Title,
                    Text = "عنوان"
                }
            };

            #endregion

            #region PageSizeList

            PageSizeList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "10",
                    Text = "۱۰"
                },
                new SelectListItem
                {
                    Value = "20",
                    Text = "۲۰"
                },
                new SelectListItem
                {
                    Value = "30",
                    Text = "۳۰"
                },
                new SelectListItem
                {
                    Value = "50",
                    Text = "۵۰"
                },
                new SelectListItem
                {
                    Value = "100",
                    Text = "۱۰۰"
                }
            };

            #endregion
        }
        public IList<SliderViewModel> Slides { get; set; }

        public SliderSearchRequest SearchRequest { get; set; }

        public IEnumerable<SelectListItem> SortOrderList { get; set; }

        public IEnumerable<SelectListItem> SortableList { get; set; }

        public IEnumerable<SelectListItem> PageSizeList { get; set; }

    }
}
