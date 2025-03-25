namespace ConsoleApp1;

public class RefrigeratedContainer : Container
{
    public string ProductType { get; }
    public double Temperature { get; }
    
    private static readonly Dictionary<string, double> RequiredTemperatures = new()
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausages", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 }
    };

    public RefrigeratedContainer(double height, double ownWeight,
        double depth, double loadCapacity, string productType, double temperature)
        : base("C", height, ownWeight, depth, loadCapacity)
    {
        if (!RequiredTemperatures.ContainsKey(productType))
            throw new ArgumentException($"Unknown product type: {productType}");
        
        double requiredTemperature = RequiredTemperatures[productType];
        if(temperature < requiredTemperature)   
            throw new ArgumentException($"Temperature {temperature}C is too low for product type: {productType} required temperature: {requiredTemperature} C");
        ProductType = productType;
        Temperature = temperature;
    }
    
    public override string ToString() =>
        $"{SerialNumber} | c: {ProductType} | Temp: {Temperature} C";

}