using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MessageBoard
{
    public partial class Message
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime? PostedTime { get; set; }
        public bool Updated { get; set; }
        public string Message1 { get; set; }
    }
}
