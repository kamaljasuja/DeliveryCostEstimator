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
    public class OfferEngine
    {
        private readonly List<OfferRule> _rules;

        public OfferEngine(List<OfferRule> rules)
        {
            _rules = rules ?? new List<OfferRule>();
        }

        public decimal GetDiscount(Package pkg, decimal cost)
        {
            var rule = _rules.FirstOrDefault(r =>
                r.OfferCode == pkg.OfferCode &&
                r.IsApplicable(pkg));

            return rule?.CalculateDiscount(cost) ?? 0;
        }
    }
}
