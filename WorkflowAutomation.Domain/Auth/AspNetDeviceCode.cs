﻿using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class AspNetDeviceCode
    {
        public string UserCode { get; set; } = null!;
        public string DeviceCode { get; set; } = null!;
        public string? SubjectId { get; set; }
        public string? SessionId { get; set; }
        public string ClientId { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime Expiration { get; set; }
        public string Data { get; set; } = null!;
    }
}
