using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryCostEstimator.Core.Models
{
    public class OfferRule
    {
        public string OfferCode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal MinWeight { get; set; }
        public decimal MaxWeight { get; set; }
        public decimal MinDistance { get; set; }
        public decimal MaxDistance { get; set; }

        public bool IsApplicable(Package p)
        {
            return p.Weight >= MinWeight &&
                   p.Weight <= MaxWeight &&
                   p.Distance >= MinDistance &&
                   p.Distance <= MaxDistance;
        }

        public decimal CalculateDiscount(decimal cost)
        {
            return cost * (DiscountPercentage / 100);
        }
    }
}
