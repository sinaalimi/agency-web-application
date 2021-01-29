using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.State
{
    public class State
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public State()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        public virtual ICollection<City.City> Cities { get; set; }
        public virtual ICollection<Hotel.Hotel> Hotels { get; set; }
        public virtual ICollection<tour.Tour> Tours { get; set; }
    }
}
