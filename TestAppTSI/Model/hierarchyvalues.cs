using Microsoft.Azure.TimeSeriesInsights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAppTSI.Model
{
    public class Hierarchyvalues
    {
        public string Name { get; set; }
        public TimeSeriesHierarchySource Source { get; set; }
        public System.Guid? Id { get; set; }
    }
}