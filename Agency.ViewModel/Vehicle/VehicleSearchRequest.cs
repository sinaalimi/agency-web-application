using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Vehicle
{
    public class VehicleSearchRequest : BaseSearchRequest
    {
        public VehicleSearchRequest()
        {
            CurrentSort = "Name";
        }

        [DisplayName("نام ماشین")]
        public string Name { get; set; }


        [DisplayName("نوع ماشین")]
        public string Type { set; get; }

        public static class ApplicantSortBy
        {
            public const string Name = nameof(Name);
            public const string Type = nameof(Type);

        }
    }
}
