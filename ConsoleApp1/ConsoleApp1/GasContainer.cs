namespace ConsoleApp1;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get;  }

    public GasContainer(double height, double ownWeight, double depth, double loadCapacity, double pressure)
        : base("G", height, ownWeight, depth, loadCapacity)
    {
        Pressure = pressure;
    }

    public override void UnloadCargo()
    {
        CargoWeight *= 0.05;
    }
    
    public override void LoadCargo(double cargoWeight)
    {
        if (cargoWeight > LoadCapacity)
        {
            NotifyHazard($"Próba załadunku {cargoWeight}kg przekracza dopuszczalną pojemność ({LoadCapacity}kg).");
            throw new OverfillException();
        }

        base.LoadCargo(cargoWeight);
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[Hazard] {SerialNumber}: {message}");
    }
        
}