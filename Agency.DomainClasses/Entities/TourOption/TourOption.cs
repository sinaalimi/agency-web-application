using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.tour;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.TourOption
{
    public class TourOption
    {
        public Guid Id { get; set; }

        public Guid TourId { get; set; }

        public string Title { get; set; }

        //public int Cost { get; set; }

        public TourOption()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        public virtual Tour Tour { get; set; }

        //public virtual Option.Option Option { get; set; }
    }
}
