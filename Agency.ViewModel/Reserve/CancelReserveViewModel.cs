using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Reserve
{
    public class CancelReserveViewModel
    {
        public List<ShowPersonViewModel> Persons { get; set; }

        public string CancelAllFilePath { get; set; }
    }
}
