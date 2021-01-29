using System.Collections.Generic;
using System.Web.Mvc;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Reserve
{
    public class ReserveListViewModel
    {

        public ReserveListViewModel()
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
                //    Value = VehicleSearchRequest.ApplicantSortBy.Type,
                //    Text = "نوع"
                //},
                //new SelectListItem
                //{
                //    Value =ReserveSearchRequest.ApplicantSortBy.RegisterTime,
                //    Text = "تاریخ ثبت"
                //},
                new SelectListItem
                {
                    Value =ReserveSearchRequest.ApplicantSortBy.Name,
                    Text = "نام"
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
        public IList<ShowReserveViewModel> Reserves { get; set; }

        public ReserveSearchRequest SearchRequest { get; set; }

        public IEnumerable<SelectListItem> SortOrderList { get; set; }

        public IEnumerable<SelectListItem> SortableList { get; set; }

        public IEnumerable<SelectListItem> PageSizeList { get; set; }


    }
   
}
