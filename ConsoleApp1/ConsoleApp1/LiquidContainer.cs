namespace ConsoleApp1
{

    public class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous { get; }

        public LiquidContainer(double height, double ownWeight, double depth, double loadCapacity, bool isHazardous)
            : base("L", height, ownWeight, depth, loadCapacity)
        {
            IsHazardous = isHazardous;
        }

        public override void LoadCargo(double cargoWeight)
        {
            double maxCapacity = IsHazardous ? LoadCapacity * 0.5 : LoadCapacity * 0.9;

            if (cargoWeight > maxCapacity)
            {
                NotifyHazard($"Próba załadunku {cargoWeight}kg przekracza dopuszczalny limit ({maxCapacity}kg).");
                throw new OverfillException("Cargo exceeds allowed limit for this container");
            }
            
            base.LoadCargo(cargoWeight);
        }

        public void NotifyHazard(string message)
        {
            Console.WriteLine(message);
        }

        public override string ToString() => 
            base.ToString() + (IsHazardous ? " [Hazardous] " : " [Safe] ");
        
    }
}