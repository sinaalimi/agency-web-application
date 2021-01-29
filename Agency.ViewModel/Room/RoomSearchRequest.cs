using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Room
{
    public class RoomSearchRequest : BaseSearchRequest
    {
        public RoomSearchRequest()
        {
            CurrentSort = "Type";
        }

        //[DisplayName("نام اتاق")]
        //public string Name { get; set; }


        [DisplayName("نوع اتاق")]
        public string Type { set; get; }

        public static class ApplicantSortBy
        {
           // public const string Name = nameof(Name);
            public const string Type = nameof(Type);

        }

    }
}

