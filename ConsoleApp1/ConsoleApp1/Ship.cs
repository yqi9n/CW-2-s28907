namespace ConsoleApp1;

public class Ship
{
    public double MaxSpeed { get; }
    public int MaxContainerCount { get; }
    public double MaxTotalWeight { get; }

    //lista prywatna konternerów
    private List<Container> containers = new();

    //wersja listy kontenerów tylko do przeglądania 
    public IReadOnlyList<Container> Containers => containers.AsReadOnly();

    public Ship(double maxSpeed, int maxContainerCount, double maxTotalWeight)
    {
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxTotalWeight = maxTotalWeight;
    }

    public void AddContainer(Container container)
    {
        if (containers.Count >= MaxContainerCount)
            throw new InvalidOperationException("Cannot add more than " + MaxContainerCount + " containers.");

        double currentTotalWeight = 0;
        foreach (var c in containers)
        {
            currentTotalWeight += c.Weight + c.CargoWeight;
        }

        double newTotalWeight = currentTotalWeight + container.Weight + container.CargoWeight;

        if (newTotalWeight > MaxTotalWeight * 1000) // poprawka z ton na kg
            throw new InvalidOperationException("Adding this container would exceed the ship's weight capacity.");

        containers.Add(container);
    }


    public void RemoveContainer(string serialNumber)
    {
        Container containerToRemove = null;
        
        //szukamy póki nie znajdziemy kontenera o danym nr 
        foreach (var container in containers)
        {
            if (container.SerialNumber == serialNumber)
            {
                containerToRemove = container;
                break;
            }
        }
        
        if (containerToRemove == null)
            throw new InvalidOperationException($"Cannot find a container with the: {serialNumber} serial number.");
        
        containers.Remove(containerToRemove);
    }

    public void DisplayAllContainers()
    {
        if (containers.Count == 0)
        {
            Console.WriteLine("No containers on the ship");
            return;
        }
        
        Console.WriteLine("List of containers on the ship: ");
        foreach (var container in containers)
        {
            Console.WriteLine(container);
        }
        
    }
    //bierzemy numer seryjny kontenera ktory chcemy usunac i kontener do dodania
    public void ReplaceContainer(string serialNumberToRemove, Container newContainer)
    {
        Container containerToRemove = null;
        foreach (var container in containers)
        {
            if (container.SerialNumber == serialNumberToRemove)
            {
                containerToRemove = container;
                break;
            }
        }
        
        if (containerToRemove == null)
            throw new InvalidOperationException($"Cannot find a container with the: {serialNumberToRemove} serial number.");
        
        double currentTotalWeight = 0;
        foreach (var c in containers)
        {
            currentTotalWeight += c.Weight + c.CargoWeight;
        }
        
        double newTotalWeight = currentTotalWeight 
                                - (containerToRemove.Weight + containerToRemove.CargoWeight)
                                + (newContainer.Weight + newContainer.CargoWeight);
        //sprawdzamy wymog wagowy, ilosc kontenerow sie nie zmienia
        if(newTotalWeight > MaxTotalWeight * 1000)
            throw new InvalidOperationException("Replacement exceeds ship's weight limits");
        
        containers.Remove(containerToRemove);
        containers.Add(newContainer);
    }

    public void UnloadAllContainers()
    {
        foreach (var c in containers)
        {
            c.UnloadCargo();
        }
    }
    
    public static void TransferContainer(Ship from, Ship to, string serialNumber)
    {
        Container containerToMove = null;

        foreach (var container in from.containers)
        {
            if (container.SerialNumber == serialNumber)
            {
                containerToMove = container;
                break;
            }
        }

        if (containerToMove == null)
            throw new InvalidOperationException($"Container {serialNumber} not found on source ship.");
        
        to.AddContainer(containerToMove);
        
        from.containers.Remove(containerToMove);
    }

    public void PrintContainer(string serialNumber)
    {
        foreach (var container in containers)
        {
            if (container.SerialNumber == serialNumber)
            {
                Console.WriteLine(container);
                return;
            }
        }
        Console.WriteLine($"No container found with [ {serialNumber} ]serial number");
    }
    
    public void UnloadContainer(string serialNumber)
    {
        foreach (var container in containers)
        {
            if (container.SerialNumber == serialNumber)
            {
                container.UnloadCargo();
                return;
            }
        }

        throw new InvalidOperationException($"Container with serial number {serialNumber} not found.");
    }


    public void PrintShipSummary()
    {
        Console.WriteLine("summary:" +
                          $"\n maxSpeed: {MaxSpeed} knots " +
                          $"\n maxContainerCount: {MaxContainerCount}" +
                          $"\n maxTotalWeight: {MaxTotalWeight}" +
                          $"\n Current containers count: {containers.Count}");

        double totalWeight = 0;
        foreach (var c in containers)
        {
            totalWeight += c.Weight + c.CargoWeight;
        }

        Console.WriteLine($"Current total weight: " + Math.Round(totalWeight / 1000, 2)+ " tons\n");
        
    }

}