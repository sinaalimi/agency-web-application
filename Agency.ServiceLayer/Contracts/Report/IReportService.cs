using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Report;

namespace Agency.ServiceLayer.Contracts.Report
{
    public interface IReportService
    {
        List<ToursReportViewModel> ReportTours(Guid id);

        List<OfficePersonCostsViewModel> Costs(Guid id);

        List<PersonInsuranceListViewModel> Insurance(Guid id);

        List<OfficePersonSeatViewModel> SeatsReport(Guid id);

        ListTourViewModel GetPagedList(ReportSearchRequest request);

        ListTourViewModel GetPagedListAsync(Guid tourid, Guid userid);

        List<CancelledToursReportViewModel> CanceledReserves(Guid id);
    }
}
