using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.Cost
{
    public class Cost
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid ReserveId { get; set; }
        public int PersonCost { get; set; }
        public int PersonPenaltyCost { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? CancelDate { get; set; }

        public string CancelFilePath { get; set; }

        #endregion

        #region MyRegion
        public Cost()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Navigation
        public virtual Person.Person Person { get; set; }
        public virtual Reserve.Reserve Reserve { get; set; }
        #endregion

    }
}
