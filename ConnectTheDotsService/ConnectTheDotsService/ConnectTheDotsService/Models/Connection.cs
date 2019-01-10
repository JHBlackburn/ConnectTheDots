using System;
using System.Collections.Generic;

namespace ConnectTheDotsService.Models
{
    public partial class Connection
    {
        public int ConnectionId { get; set; }
        public int DotSource { get; set; }
        public int DotSync { get; set; }
        public DateTime ConnectionDateTime { get; set; }

        public Dot DotSourceNavigation { get; set; }
    }
}
