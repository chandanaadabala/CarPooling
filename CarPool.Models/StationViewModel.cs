using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Models
{
    public class StationViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public double DistanceFromSrc { get; set; }
        public int OrderNo { get; set; }
    }
}
