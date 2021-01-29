using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DomainClasses.Entities.common
{
    public abstract class BaseEntity : Entity
    {
        #region Properties
        /// <summary>
        /// gets or sets Identifier of this Entity
        /// </summary>
        public virtual Guid Id { get; set; }
        #endregion
    }
}
