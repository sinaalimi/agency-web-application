using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Person;

namespace Agency.ViewModel.Reserve
{
   public class ShowPersonViewModel
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string NationalCode { get; set; }

        public string IdentityCode { get; set; }

        public Guid VehicleId { get; set; }

        public Guid ReserveId { get; set; }

        public bool IsCanceled { get; set; }

        public string CancelFilePath { get; set; }
    }
}
