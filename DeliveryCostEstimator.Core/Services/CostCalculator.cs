using DeliveryCostEstimator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryCostEstimator.Core.Models;
namespace DeliveryCostEstimator.Core.Services
{
    public class CostCalculator
    {
        private const decimal BaseCost = 100;

        public decimal Calculate(Package p)
        {
            return BaseCost + (p.Weight * 10) + (p.Distance * 5);
        }
    }
}
