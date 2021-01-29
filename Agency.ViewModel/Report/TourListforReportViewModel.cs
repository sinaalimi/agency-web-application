using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Report
{
    public class TourListforReportViewModel
    {
        public Guid Id { get; set; }

        public string TourName { get; set; }

        [UIHint("PersianDatePicker")]
        public DateTime SatrtDate { get; set; }

        public string SourceCity { get; set; }
        
        public string DestinationCity { get; set; }

        public Guid? UserId { get; set; }

        public DateTime ReportItemStartDate { get; set; }
        public DateTime ReportItemEndDate { get; set; }
    }
}
