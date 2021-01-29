using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Customer
{
    public class CustomerSearchRequest : BaseSearchRequest
    {
        public CustomerSearchRequest()
        {
            CurrentSort = "RegisterDate";
        }

        [DisplayName("اندیس")]
        public int Index { get; set; }


        [DisplayName("نام خانوادگی")]
        public string LastName { set; get; }


        [DisplayName("تاریخ ثبت")]
        [UIHint("PersianDatePickerInputSm")]
        [DataType(DataType.Date)]
        public DateTime? RegisterDate { set; get; }


        [DisplayName("گروه")]
        public Guid GroupId { get; set; }

        //[DisplayName("اعمال تاریخ")]
       // public bool SearchByDate {set; get; }

        public static class ApplicantSortBy
        {
            public const string Index = nameof(Index);
            public const string LastName = nameof(LastName);
            public const string RegisterDate = nameof(RegisterDate);
            public const string GroupId = nameof(GroupId);

        }
    }
}
