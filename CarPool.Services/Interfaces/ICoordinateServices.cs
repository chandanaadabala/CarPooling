using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Interfaces
{
    public interface ICoordinateServices
    {
        void CalculateDistance(ref List<StationViewModel> stations);
    }
}
