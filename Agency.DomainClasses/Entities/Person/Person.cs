using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Vehicle;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.Person
{
    public class Person
    {
        #region properties
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ParentId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// نام پدر سرپرست تور
        /// </summary>
        public string ParentName { get; set; }

        public string NationalCode { get; set; }

        public string IdentityCode { get; set; }

        public string PhoneNumber { get; set; }

        public string Mobile { get; set; }

        public DateTime BirthDay { get; set; }

        public string BirthPlace { get; set; }

        public string Job { get; set; }

        public AgeRange AgeRange { get; set; }

        public bool IsGroup { get; set; }

        public bool IsParent { get; set; }

        public string Relation { get; set; }

        public bool Gender { get; set; }

        public string Address { get; set; }
        #endregion

        #region ctor

        public Person()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region navigation
        public virtual Cost.Cost Cost { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
       
        public virtual ICollection<Reserve.Reserve> Reserves { get;set; }
        #endregion
    }
}
