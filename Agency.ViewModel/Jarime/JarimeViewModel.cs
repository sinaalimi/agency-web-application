using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Jarime
{
   public class JarimeViewModel
    {
        public Guid PersonId { get; set; }
        public Guid ReserveId { get; set; }
        public string TourName { get; set; }
        public int Percent { get; set; }
        public int Price { get; set; }
        public string ParentName { get; set; }
        public string PersonName { get; set; }
        public int PenaltyPrice { get; set; }
        public int FinalTotalPrice { get; set; }
        public string CodeRahgiri { get; set; }
        public bool IsParent { get; set; }
    }
}
