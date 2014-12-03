using System;

namespace Stateless.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var phoneCall = new StateMachine<State, Trigger>(State.OffHook);

            phoneCall.Configure(State.OffHook).Permit(Trigger.CallDialed, State.Ringing);

            phoneCall.Configure(State.Ringing).Permit(Trigger.HungUp, State.OffHook).Permit(Trigger.CallConnected, State.Connected);

            phoneCall.Configure(State.Connected).OnEntry(t => Console.WriteLine("[Timer:] Call started at {0}", DateTime.Now)).OnExit(t => Console.WriteLine("[Timer:] Call ended at {0}", DateTime.Now)).Permit(Trigger.LeftMessage, State.OffHook).Permit(Trigger.HungUp, State.OffHook).Permit(Trigger.PlacedOnHold, State.OnHold);

            phoneCall.Configure(State.OnHold).SubstateOf(State.Connected).Permit(Trigger.TakenOffHold, State.Connected).Permit(Trigger.HungUp, State.OffHook).Permit(Trigger.PhoneHurledAgainstWall, State.PhoneDestroyed);

            Console.WriteLine(phoneCall.State);
            phoneCall.Fire(Trigger.CallDialed);
            Console.WriteLine(phoneCall.State);
            phoneCall.Fire(Trigger.CallConnected);
            Console.WriteLine(phoneCall.State);
            phoneCall.Fire(Trigger.PlacedOnHold);
            Console.WriteLine(phoneCall.State);
            phoneCall.Fire(Trigger.TakenOffHold);
            Console.WriteLine(phoneCall.State);
            phoneCall.Fire(Trigger.HungUp);
            Console.WriteLine(phoneCall.State);

            Console.ReadKey(true);
        }

        private enum State
        {
            OffHook,
            Ringing,
            Connected,
            OnHold,
            PhoneDestroyed
        }

        private enum Trigger
        {
            CallDialed,
            HungUp,
            CallConnected,
            LeftMessage,
            PlacedOnHold,
            TakenOffHold,
            PhoneHurledAgainstWall
        }
    }
}