using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Option;

namespace Agency.ViewModel.Cost
{
    public class ChildCostViewModel
    {
        public int Count { get; set; }

        /// <summary>
        /// هزینه پایه
        /// </summary>
        public int BasePrice { get; set; }
        public List<OptionViewModel> OpttionList { get; set; }
        public int TotalCost { get; set; }
    }
}
