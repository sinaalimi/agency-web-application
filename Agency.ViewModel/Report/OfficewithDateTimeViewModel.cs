using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Report
{
    public class OfficewithDateTimeViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string TourName { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime ReserveDate { get; set; }

        public string Address { get; set; }

        public string Mobile { get; set; }

       }
}
