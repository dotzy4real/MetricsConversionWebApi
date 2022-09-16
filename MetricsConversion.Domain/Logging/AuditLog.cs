using MetricsConversion.Domain.Common;
using System;


namespace MetricsConversion.Domain.Logging
{
    public partial class AuditLog: BaseEntity
    {
        public string EntityType { get; set; }

        public int EntityID { get; set; }

        public int Action { get; set; }

        public DateTime ActionDate { get; set; }

        public string ObjectString { get; set; }
    }
}
