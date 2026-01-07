using DeliveryCostEstimator.Core.Models;
using DeliveryCostEstimator.Core.Services;
using DeliveryCostEstimator.Infrastructure.Config;

// INPUT
try
{
    // INPUT
    Console.WriteLine("Enter base delivery cost and number of packages:");
    var baseInput = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (baseInput == null || baseInput.Length < 2) throw new FormatException("Invalid base input. Expected: <base_cost> <no_of_packages>");
    
    decimal baseCost = decimal.Parse(baseInput[0]);
    int packageCount = int.Parse(baseInput[1]);

    if (baseCost < 0 || packageCount <= 0) throw new ArgumentException("Base cost and package count must be positive.");

    var packages = new List<Package>();
    Console.WriteLine("Enter package details:");

    for (int i = 0; i < packageCount; i++)
    {
        var p = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (p == null || p.Length < 3) throw new FormatException($"Invalid package input for package {i + 1}");

        packages.Add(new Package
        {
            Id = p[0],
            Weight = decimal.Parse(p[1]),
            Distance = decimal.Parse(p[2]),
            OfferCode = p.Length > 3 ? p[3] : "NA"
        });
    }

    Console.WriteLine("Enter number of vehicles, max speed, max carriable weight:");
    var vehicleInput = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (vehicleInput == null || vehicleInput.Length < 3) throw new FormatException("Invalid vehicle input. Expected: <no_of_vehicles> <max_speed> <max_weight>");

    int vehicleCount = int.Parse(vehicleInput[0]);
    decimal speed = decimal.Parse(vehicleInput[1]);
    decimal maxWeight = decimal.Parse(vehicleInput[2]);

    if (vehicleCount <= 0 || speed <= 0 || maxWeight <= 0) throw new ArgumentException("Vehicle inputs must be positive numbers.");

    var vehicles = Enumerable.Range(1, vehicleCount)
        .Select(_ => new Vehicle
        {
            Speed = speed,
            MaxWeight = maxWeight
        }).ToList();

    // LOAD OFFERS
    var offers = OfferConfigLoader.Load("Config/offers.json");

    // SERVICES
    var offerEngine = new OfferEngine(offers);
    var calculator = new CostCalculator();

    // COST CALCULATION
    foreach (var pkg in packages)
    {
        var cost = baseCost + (pkg.Weight * 10) + (pkg.Distance * 5);
        var discount = offerEngine.GetDiscount(pkg, cost);
        pkg.DeliveryCost = cost - discount;
    }

    // DELIVERY TIME
    new ShipmentPlanner().PlanDeliveries(packages, vehicles);

    // OUTPUT (PDF FORMAT)
    foreach (var p in packages)
    {
        var originalCost = baseCost + (p.Weight * 10) + (p.Distance * 5);
        var discount = originalCost - p.DeliveryCost;

        Console.WriteLine(
            $"{p.Id} {discount:F0} {p.DeliveryCost:F0} {Math.Round(p.DeliveryTime, 2, MidpointRounding.AwayFromZero):F2}"
        );
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Environment.Exit(1);
}
