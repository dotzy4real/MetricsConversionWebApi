using System;
using System.Collections.Generic;
using System.Text;

namespace MetricsConversion.Domain.Common
{
    public abstract partial class BaseEntityWithAudit: BaseEntity
    {
        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public virtual DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets Creating User
        /// </summary>
        public virtual int CreatedBy { get; set; }


        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public virtual DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets user updating
        /// </summary>
        public virtual int UpdatedBy { get; set; }
    }
}
