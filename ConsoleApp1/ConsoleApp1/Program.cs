using System;

namespace ConsoleApp1;

public class Program
{
    static void Main(string[] args)
    {
        // dwa statki 
        var ship1 = new Ship(25, 5, 50); // max 5 kontenerów, 50 ton
        var ship2 = new Ship(20, 5, 50);

        // tworzymy kontenery jeden z każdego typu
        var liquid = new LiquidContainer(2.5, 1000, 3, 8000, false); // niehazardowy
        var gas = new GasContainer(2.5, 1200, 3, 6000, 1.5);
        var fridge = new RefrigeratedContainer(2.5, 900, 3, 7000, "Meat", 6);

        // ładujemy ładunek do kontenerów
        liquid.LoadCargo(4000);
        gas.LoadCargo(3000);
        fridge.LoadCargo(5000);

        // wrzucamy na pierwszy statek
        ship1.AddContainer(liquid);
        ship1.AddContainer(gas);
        ship1.AddContainer(fridge);

        // wypisujemy wszystko co w srodku
        ship1.PrintShipSummary();

        // wyładuj jeden po nr seryjnym
        ship1.UnloadContainer(gas.SerialNumber);

        // zamiana kontenera
        var otherFridge = new RefrigeratedContainer(2.5, 950, 3, 6500, "Fish", 13);
        otherFridge.LoadCargo(4000);
        ship1.ReplaceContainer(fridge.SerialNumber, otherFridge);

        // przenoszenie kontenera między statkami
        Ship.TransferContainer(ship1, ship2, otherFridge.SerialNumber);

        // wypisz statki po przeniesieniu
        Console.WriteLine("\n--- After transfer ---");
        ship1.PrintShipSummary();
        ship2.PrintShipSummary();

        // rozładuj wszystkie na ship1
        ship1.UnloadAllContainers();

        // wypisz jeden kontener
        ship2.PrintContainer(otherFridge.SerialNumber);
    }
    
}