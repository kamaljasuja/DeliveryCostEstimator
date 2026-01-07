using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryCostEstimator.Core.Models;

namespace DeliveryCostEstimator.Core.Interfaces
{
    public interface IOfferRule
    {
        string OfferCode { get; }
        bool IsApplicable(Package package);
        decimal CalculateDiscount(decimal cost);
    }
}
