using DeliveryCostEstimator.Core.Models;

namespace DeliveryCostEstimator.Core.Services;

public class ShipmentPlanner
{
    public void PlanDeliveries(List<Package> packages, List<Vehicle> vehicles)
    {
        if (packages == null || !packages.Any()) return;
        if (vehicles == null || !vehicles.Any()) return;
        if (vehicles.Any(v => v.Speed <= 0)) throw new ArgumentException("Vehicle speed must be greater than zero.");
        var remaining = new List<Package>(packages);

        while (remaining.Any())
        {
            // Vehicle which becomes free first
            var vehicle = vehicles.OrderBy(v => v.AvailableAt).First();
            var currentTime = vehicle.AvailableAt;

            List<Package> bestShipment = new();
            decimal bestWeight = 0;

            // Try all combinations using bitmask
            int n = remaining.Count;
            int maxMask = 1 << n;

            for (int mask = 1; mask < maxMask; mask++)
            {
                var temp = new List<Package>();
                decimal weight = 0;

                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        weight += remaining[i].Weight;
                        if (weight > vehicle.MaxWeight)
                        {
                            temp.Clear();
                            break;
                        }
                        temp.Add(remaining[i]);
                    }
                }

                if (temp.Count == 0) continue;

                var tempMaxDistance = temp.Max(p => p.Distance);
                var tempTime = tempMaxDistance / vehicle.Speed;
                var bestTime = bestShipment.Any()
                    ? bestShipment.Max(p => p.Distance) / vehicle.Speed
                    : decimal.MaxValue;

                if (temp.Count > bestShipment.Count ||
                   (temp.Count == bestShipment.Count && weight > bestWeight) ||
                   (temp.Count == bestShipment.Count && weight == bestWeight && tempTime < bestTime))
                {
                    bestShipment = temp;
                    bestWeight = weight;
                }

            }

            var maxDistance = bestShipment.Max(p => p.Distance);
            var travelTime = System.Math.Floor(maxDistance / vehicle.Speed * 100) / 100;

            foreach (var pkg in bestShipment)
            {
                pkg.DeliveryTime = currentTime + System.Math.Floor(pkg.Distance / vehicle.Speed * 100) / 100;
                remaining.Remove(pkg);
            }

            vehicle.AvailableAt = currentTime + (2 * travelTime);
        }
    }
}
