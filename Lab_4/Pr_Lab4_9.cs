/*
    Координація служб аеропорту (Mediator Pattern)
    учасники взаємодіють через AirportCoordinator.
*/

using System;

public interface IFlightMediator
{
    void Relay(object source, string signal);
}

public class AirportCoordinator : IFlightMediator
{
    public Aircraft CurrentAircraft { get; set; } = null!;
    public RefuelCrew RefuelUnit { get; set; } = null!;

    public void Relay(object source, string signal)
    {
        if (source is Aircraft && signal == "LandingCheck")
        {
            Console.WriteLine("Координатор: Смуга №2 підтверджена, посадку дозволено.");
            CurrentAircraft.ExecuteLanding();
            RefuelUnit.StartRefuelPreparation();
            return;
        }

        if (source is RefuelCrew && signal == "RefuelReady")
        {
            Console.WriteLine("Координатор: Наземна команда повідомила про готовність до заправки.");
        }
    }
}

public class Aircraft
{
    private readonly IFlightMediator _dispatcher;

    public Aircraft(IFlightMediator dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public void AskForLanding()
    {
        Console.WriteLine("Борт A-302: запитую захід на посадку.");
        _dispatcher.Relay(this, "LandingCheck");
    }

    public void ExecuteLanding()
    {
        Console.WriteLine("Борт A-302: виконую посадку, рухаюсь до стоянки.");
    }
}

public class RefuelCrew
{
    private readonly IFlightMediator _dispatcher;

    public RefuelCrew(IFlightMediator dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public void StartRefuelPreparation()
    {
        Console.WriteLine("Паливна служба: автоцистерна виїхала, заправка готується.");
        _dispatcher.Relay(this, "RefuelReady");
    }
}

public static class Program
{
    public static void Main()
    {
        var coordinator = new AirportCoordinator();
        var aircraft = new Aircraft(coordinator);
        var refuelCrew = new RefuelCrew(coordinator);

        coordinator.CurrentAircraft = aircraft;
        coordinator.RefuelUnit = refuelCrew;

        aircraft.AskForLanding();
    }
}
