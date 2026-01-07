using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryCostEstimator.Core.Models
{
    public class Vehicle
    {
        public decimal MaxWeight { get; set; }
        public decimal Speed { get; set; }
        public decimal AvailableAt { get; set; } = 0;
    }
}
