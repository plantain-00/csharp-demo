using System;

namespace Racy
{
    public enum State
    {
        OffHook,
        Ringing,
        Connected,
        OnHold
    }

    public enum Trigger
    {
        HungUp,
        CallConnected,
        LeftMessage,
        PlacedOnHold,
        CallDialled
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var phoneCall = new StateMachine<State, Trigger>(State.OffHook);

            phoneCall.Config(State.OffHook, Trigger.CallDialled, State.Ringing);

            phoneCall.Config(State.Ringing, Trigger.HungUp, State.OffHook);
            phoneCall.Config(State.Ringing, Trigger.CallConnected, State.Connected);

            phoneCall.Config(State.Connected, Trigger.LeftMessage, State.OffHook);
            phoneCall.Config(State.Connected, Trigger.HungUp, State.OffHook);
            phoneCall.Config(State.Connected, Trigger.PlacedOnHold, State.OnHold);

            phoneCall.ConfigOnEntry(State.Connected, r => Console.WriteLine("{0} {1} {2}", r.By, r.From, r.To));
            phoneCall.ConfigOnExit(State.Connected, r => Console.WriteLine("{0} {1} {2}", r.By, r.From, r.To));

            phoneCall.Fire(Trigger.CallDialled);
            Console.WriteLine(phoneCall.State);

            phoneCall.Fire(Trigger.CallConnected);
            Console.WriteLine(phoneCall.State);

            phoneCall.Fire(Trigger.LeftMessage);
            Console.WriteLine(phoneCall.State);
            Console.Read();
        }
    }
}