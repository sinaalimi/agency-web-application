using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.NewPerson
{
    public class NewPerson
    {
        #region properties
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

       // [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "ایمیل خود را به درستی وارد کنید")]
        public string Email { get; set; }

        public string Mobile { get; set; }

        #endregion

        #region Ctor

        public NewPerson()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        #endregion

        #region navigation

        public virtual User.User User { get; set; }

        public virtual ICollection<PersonRoomHotel.PersonRoomHotel> PersonRoomHotels { get; set; }

        #endregion

    }
}
