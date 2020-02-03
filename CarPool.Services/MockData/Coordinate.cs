using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.MockData

{
    public class Coordinate
    {
        public static Dictionary<string, Coordinate> Locations =
            new Dictionary<string, Coordinate>
            {
                {"Mumbai", new Coordinate(19.073,72.883)},
                {"Delhi", new Coordinate(28.652,77.231)},
                {"Bengaluru", new Coordinate(12.972,77.594)},
                {"Kolkata", new Coordinate(22.563,88.363)},
                {"Chennai", new Coordinate(13.088,80.278)},
                {"Ahmedabad", new Coordinate(23.026,72.587)},
                {"Hyderabad", new Coordinate(17.384,78.456)},
                {"Pune", new Coordinate(18.52,73.855)},
                {"Surat", new Coordinate(21.196,72.83)},
                {"Kanpur", new Coordinate(26.465,80.35)},
                {"Jaipur", new Coordinate(26.92,75.788)},
                {"Lucknow", new Coordinate(26.839,80.923)},
            };
        public Coordinate(double lattitude, double longitude)
        {
            Lattitude = lattitude;
            Longitude = longitude;
        }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
