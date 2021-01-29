using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Cost;

namespace Agency.ServiceLayer.Contracts.Cost
{
    public interface ICostService
    {
        ShowCostViewModel GetEditView(Guid id);


    }
}
