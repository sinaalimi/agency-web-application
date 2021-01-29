using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Common;
namespace Agency.ViewModel.Customer
{
    public class CustomerListViewModel
    {
        public CustomerListViewModel()
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
                    Value = CustomerSearchRequest.ApplicantSortBy.LastName,
                    Text = "نام خانوادگی"
                },
                new SelectListItem
                {
                    Value = CustomerSearchRequest.ApplicantSortBy.RegisterDate,
                    Text = "تاریخ تولد"
                },
                new SelectListItem
                {
                    Value = CustomerSearchRequest.ApplicantSortBy.GroupId,
                    Text = "گروه"
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

      

      
        public IList<CustomerViewModel> Customers { get; set; }

        public CustomerSearchRequest SearchRequest { get; set; }

        public IEnumerable<SelectListItem> SortOrderList { get; set; }

        public IEnumerable<SelectListItem> SortableList { get; set; }

        public IEnumerable<SelectListItem> PageSizeList { get; set; }
    }
}
