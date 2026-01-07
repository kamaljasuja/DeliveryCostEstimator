using Xunit;
using DeliveryCostEstimator.Core.Services;
using DeliveryCostEstimator.Core.Models;
using System.Collections.Generic;

namespace DeliveryCostEstimator.Tests;

public class ShipmentPlannerTests
{
    [Fact]
    public void PlanDeliveries_CalculatesCorrectDeliveryTimes_ForGivenScenario()
    {
        // ... (Existing test implementation) ...
        var packages = new List<Package>
        {
            new Package { Id = "PKG1", Weight = 50, Distance = 30, OfferCode = "OFR001" },
            new Package { Id = "PKG2", Weight = 75, Distance = 125, OfferCode = "OFFR0008" },
            new Package { Id = "PKG3", Weight = 175, Distance = 100, OfferCode = "OFR003" },
            new Package { Id = "PKG4", Weight = 110, Distance = 60, OfferCode = "OFR002" },
            new Package { Id = "PKG5", Weight = 155, Distance = 95, OfferCode = "NA" }
        };

        var vehicles = new List<Vehicle>
        {
            new Vehicle { Speed = 70, MaxWeight = 200 },
            new Vehicle { Speed = 70, MaxWeight = 200 }
        };

        var planner = new ShipmentPlanner();

        planner.PlanDeliveries(packages, vehicles);
        
        var p1 = packages.Find(p => p.Id == "PKG1");
        var p2 = packages.Find(p => p.Id == "PKG2");
        var p3 = packages.Find(p => p.Id == "PKG3");
        var p4 = packages.Find(p => p.Id == "PKG4");
        var p5 = packages.Find(p => p.Id == "PKG5");

        Assert.Equal(3.98, (double)System.Math.Round(p1.DeliveryTime, 2));
        Assert.Equal(1.78, (double)System.Math.Round(p2.DeliveryTime, 2));
        Assert.Equal(1.42, (double)System.Math.Round(p3.DeliveryTime, 2));
        Assert.Equal(0.85, (double)System.Math.Round(p4.DeliveryTime, 2));
        Assert.Equal(4.19, (double)System.Math.Round(p5.DeliveryTime, 2));
    }

    [Fact]
    public void PlanDeliveries_ThrowsArgumentException_WhenVehicleSpeedIsZero()
    {
        var packages = new List<Package> { new Package { Id = "PKG1", Weight = 10, Distance = 100 } };
        var vehicles = new List<Vehicle> { new Vehicle { Speed = 0, MaxWeight = 100 } };
        var planner = new ShipmentPlanner();

        Assert.Throws<ArgumentException>(() => planner.PlanDeliveries(packages, vehicles));
    }

    [Fact]
    public void PlanDeliveries_DoesNothing_WhenPackagesListIsEmpty()
    {
        var packages = new List<Package>();
        var vehicles = new List<Vehicle> { new Vehicle { Speed = 70, MaxWeight = 200 } };
        var planner = new ShipmentPlanner();

        // Should not throw
        planner.PlanDeliveries(packages, vehicles);
    }
    
    [Fact]
    public void PlanDeliveries_DoesNothing_WhenVehiclesListIsEmpty()
    {
         var packages = new List<Package> { new Package { Id = "PKG1", Weight = 10, Distance = 100 } };
         var vehicles = new List<Vehicle>();
         var planner = new ShipmentPlanner();

         // Should not throw, just returns
         planner.PlanDeliveries(packages, vehicles);
    }
}
