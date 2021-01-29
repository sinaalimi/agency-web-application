using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.StateCity
{
    public class StateSearchRequest:BaseSearchRequest
    {
        public StateSearchRequest()
        {
            CurrentSort = "Name";
        }

        [DisplayName("نام استان")]
        public string Name { get; set; }


       
        public static class ApplicantSortBy
        {
            public const string Name = nameof(Name);

        }


    }
}
