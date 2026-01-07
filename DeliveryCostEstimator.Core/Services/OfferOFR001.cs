using DeliveryCostEstimator.Core.Interfaces;
using DeliveryCostEstimator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryCostEstimator.Core.Interfaces;
using DeliveryCostEstimator.Core.Models;

namespace DeliveryCostEstimator.Core.Services
{
    public class OfferOFR001 : IOfferRule
    {
        public string OfferCode => "OFR001";

        public bool IsApplicable(Package p)
            => p.Weight <= 200 && p.Distance <= 200;

        public decimal CalculateDiscount(decimal cost)
            => cost * 0.10m;
    }
}
