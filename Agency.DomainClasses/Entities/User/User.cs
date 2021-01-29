using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.tour;
using Agency.DomainClasses.Entities.Vehicle;
using Microsoft.AspNet.Identity.EntityFramework;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.User
{
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        #region Ctor

        public User()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            //EmailConfirmed = true;

        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// نشانده دهنده قفل بودن کاربر است
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// آیا کاربر سیستمی است؟
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        /// برای مسائل مربوط به همزمانی ها
        /// </summary>
        public byte[] RowVersion { get; set; }

        public string AgencyName { get; set; }

        #endregion

        #region Navigation

        public  virtual ICollection<Seat> Seats { get; set; }

        public virtual ICollection<Tour> Tour { get; set; }

        public virtual  ICollection<Reserve.Reserve> Reserves { get; set; }

        public virtual ICollection<SeatFormat> SeatsFormat { get; set; }
        
        #endregion

    }
}
