using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Option;

namespace Agency.ViewModel.Cost
{
    public class ShowCostViewModel
    {

        public AdultCostViewModel Adult{ get; set; } 
        public ChildCostViewModel Child { get; set; } 
        public BabyCostViewModel Baby { get; set; }
        public int InsurancPrice { get; set; }
        public int TotalCost { get; set; }
        public Guid ReserveId { get; set; }
        public List<OptionViewModel> OptionList { get; set; }
        public int CoupleBedPrice { get; set; }
        public bool CoupleBed { get; set; }

        /// <summary>
        /// مسیر آخرین فایلی که ثبت شده است
        /// </summary>
        public string ContractFilepath { get; set; }

        public bool IsAllSeatsReserved { get; set; }

        /// <summary>
        /// رزرو تایید نهایی شده است
        /// </summary>
        public bool IsTemporary { get; set; }

        public ShowCostViewModel()
        {
            IsAllSeatsReserved = true;
        }
    }
}
