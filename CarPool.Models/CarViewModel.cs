using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Models
{
    public class CarViewModel
    {
        public CarViewModel()
        {

        }
        public CarViewModel(string id, string name, string numberPlate)
        {
            ID = id;
            Name = name;
            NumberPlate = numberPlate;

        }
        public string ID { get; set; }
        public string Name { get; set; }
        public string NumberPlate { get; set; }
    }
}
