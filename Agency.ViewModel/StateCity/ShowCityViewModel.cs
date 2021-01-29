using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.StateCity
{
    public class ShowCityViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid StateId { get; set; }
        public string StateName { get; set; }
    }
}
