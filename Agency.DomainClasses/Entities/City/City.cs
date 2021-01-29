using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.City
{
    public class City
    {
        public Guid Id { get; set; }

        public Guid StateId { get; set; }

        public string Name { get; set; }


        public City()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        public virtual State.State State { get; set; }

        public virtual ICollection<tour.Tour> Tours { get; set; }

        public virtual ICollection<Hotel.Hotel> Hotels { get; set; } 
    }
}
