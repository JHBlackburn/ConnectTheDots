using System;
using System.Collections.Generic;

namespace ConnectTheDotsService.Models
{
    public partial class Dot
    {
        public Dot()
        {
            Connection = new HashSet<Connection>();
        }

        public int DotId { get; set; }
        public string DotName { get; set; }
        public decimal? Xcoordinate { get; set; }
        public decimal? Ycoordinate { get; set; }

        public ICollection<Connection> Connection { get; set; }
    }
}
