using DeliveryCostEstimator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DeliveryCostEstimator.Core.Models;
using System.Text.Json;
namespace DeliveryCostEstimator.Infrastructure.Config
{
    public static class OfferConfigLoader
    {
        public static List<OfferRule> Load(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<OfferRule>>(json);
        }
    }
}
