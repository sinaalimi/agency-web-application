using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Customer
{
    public class CustomerDetailViewModel
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string SsNumber { get; set; }

        public string NationalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegisterDate { get; set; }

        public string Job { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string HomeAddress { get; set; }

        public string BusinessAddress { get; set; }

        public string GroupName { get; set; }


        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }
        #endregion
    }
}
