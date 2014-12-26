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

            phoneCall[State.OffHook].CanBeFired(Trigger.CallDialled).To(State.Ringing);

            phoneCall[State.Ringing].CanBeFired(Trigger.HungUp).To(State.OffHook);
            phoneCall[State.Ringing].CanBeFired(Trigger.CallConnected).To(State.Connected);

            phoneCall[State.Connected].OnEntry += (s1, t, s2) => Console.WriteLine("entry {0} from {1} by {2}", s2, s1, t);
            phoneCall[State.Connected].OnExit += (s1, t, s2) => Console.WriteLine("exit {0} to {1} by {2}", s1, s2, t);
            phoneCall[State.Connected].CanBeFired(Trigger.LeftMessage).To(State.OffHook);
            phoneCall[State.Connected].CanBeFired(Trigger.HungUp).To(State.OffHook);
            phoneCall[State.Connected].CanBeFired(Trigger.PlacedOnHold).To(State.OnHold);

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