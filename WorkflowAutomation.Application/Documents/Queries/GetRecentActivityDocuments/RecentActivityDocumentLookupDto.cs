﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments
{
    public class RecentActivityDocumentLookupDto
    {
        public string Titile { set; get; }

        public DateTime LastActivityDate { get;set; }
    }
}