using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assignment.Models.Read
{
    public partial class TblUser
    {
        public int Userid { get; set; }
        public string UserEmail { get; set; }
        public string UserPass { get; set; }
        public string UserRole { get; set; }
    }
}
