﻿using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class Status
    {
        public Status()
        {
            Documents = new HashSet<Document>();
        }

        public int IdStatus { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Document> Documents { get; set; }
    }
}
