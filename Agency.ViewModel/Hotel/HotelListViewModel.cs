using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Hotel
{
    public class HotelListViewModel
    {
        public HotelListViewModel()
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
                new SelectListItem
                {
                    Value = HotelSearchRequest.ApplicantSortBy.State,
                    Text = "استان"
                },
                new SelectListItem
                {
                    Value = HotelSearchRequest.ApplicantSortBy.Degree,
                    Text = "درجه"
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

        public HotelSearchRequest SearchRequest { get; set; }

        public IEnumerable<SelectListItem> SortOrderList { get; set; }

        public IEnumerable<SelectListItem> SortableList { get; set; }

        public IEnumerable<SelectListItem> PageSizeList { get; set; }
        public IList<ShowHotelViewModel> Hotels { get; set; }
    }
}
