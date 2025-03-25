using System;

namespace ConsoleApp1
{

    public abstract class Container
    {
        public string SerialNumber { get; }

        protected double Height { get; }
        protected double OwnWeight { get; }
        protected double Depth { get; }
        protected double LoadCapacity { get; }

        public double CargoWeight { get; protected set; }
        
        //waga do odczytu
        public double Weight => OwnWeight;

        public Container(string type, double height,
            double ownWeight, double depth, double loadCapacity)
        {
            Height = height;
            OwnWeight = ownWeight;
            Depth = depth;
            LoadCapacity = loadCapacity;

            SerialNumber = GenerateSerialNumber(type); //typ bedzie L/G/C 
        }

        /*static zeby counter dzialal globalnie
         a nie dla pojedynczego kontenera*/
        private static int counter = 1;

        private String GenerateSerialNumber(string type) =>
            $"KON-{type}-{counter++.ToString("D4")}";

        //moze byc overrideowana dla dod. obostrzeń w innych kontenerach
        public virtual void LoadCargo(double cargoWeight)
        {
            if (cargoWeight > LoadCapacity)
                throw new OverfillException();

            CargoWeight = cargoWeight;
        }

        public virtual void UnloadCargo() => CargoWeight = 0;

        public override string ToString() =>
            $"{SerialNumber} - Load: {CargoWeight}kg / {LoadCapacity}kg";

    }
}