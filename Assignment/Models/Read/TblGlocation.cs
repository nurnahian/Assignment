using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assignment.Models.Read
{
    public partial class TblGlocation
    {
        public int IntId { get; set; }
        public int IntWorkplaceId { get; set; }
        public string StrWorkplace { get; set; }
        public string StrWorkplaceGroup { get; set; }
        public string StrBusinessUnitName { get; set; }
        public string StrGoogleLocationName { get; set; }
    }
}
