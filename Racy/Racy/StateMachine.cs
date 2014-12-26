using System;
using System.Collections.Generic;
using System.Linq;

namespace Racy
{
    public class StateMachine<TState, TTrigger> where TState : struct where TTrigger : struct
    {
        private readonly List<StateConfiguration<TState, TTrigger>> _configurations;

        public StateMachine(TState state)
        {
            State = state;
            _configurations = new List<StateConfiguration<TState, TTrigger>>();
        }

        public StateConfiguration<TState, TTrigger> this[TState state]
        {
            get
            {
                var config = _configurations.FirstOrDefault(c => Equals(c.State, state));
                if (config == null)
                {
                    config = new StateConfiguration<TState, TTrigger>(state);
                    _configurations.Add(config);
                }
                return config;
            }
        }

        public TState State { get; private set; }

        public bool CanFire(TTrigger trigger)
        {
            var firer = GetFirer(trigger);
            return firer != null;
        }

        private TriggerFirer<TState, TTrigger> GetFirer(TTrigger trigger)
        {
            var config = _configurations.FirstOrDefault(c => c.State.Equals(State));
            if (config == null)
            {
                return null;
            }
            var firer = config.TriggerFirers.FirstOrDefault(t => t.Trigger.Equals(trigger) && t.State != null);
            return firer;
        }

        public void Fire(TTrigger trigger)
        {
            var firer = GetFirer(trigger);
            if (firer == null
                || firer.State == null)
            {
                throw new Exception("cannot fire the trigger at current state.");
            }

            var config = _configurations.FirstOrDefault(c => c.State.Equals(State));
            if (config != null
                && config.OnExit != null)
            {
                config.OnExit(State, trigger, firer.State.Value);
            }

            var lastState = State;
            State = firer.State.Value;

            config = _configurations.FirstOrDefault(c => c.State.Equals(State));
            if (config != null
                && config.OnExit != null)
            {
                config.OnEntry(lastState, trigger, State);
            }
        }
    }

    public class StateConfiguration<TState, TTrigger> where TState : struct where TTrigger : struct
    {
        internal readonly TState State;
        internal readonly List<TriggerFirer<TState, TTrigger>> TriggerFirers;

        public Action<TState, TTrigger, TState> OnEntry;
        public Action<TState, TTrigger, TState> OnExit;

        internal StateConfiguration(TState state)
        {
            State = state;
            TriggerFirers = new List<TriggerFirer<TState, TTrigger>>();
        }

        public TriggerFirer<TState, TTrigger> CanBeFired(TTrigger trigger)
        {
            var triggerFirer = TriggerFirers.FirstOrDefault(t => t.Trigger.Equals(trigger));
            if (triggerFirer == null)
            {
                triggerFirer = new TriggerFirer<TState, TTrigger>(trigger);
                TriggerFirers.Add(triggerFirer);
            }
            return triggerFirer;
        }
    }

    public class TriggerFirer<TState, TTrigger> where TState : struct where TTrigger : struct
    {
        internal readonly TTrigger Trigger;
        internal TState? State;

        internal TriggerFirer(TTrigger trigger)
        {
            Trigger = trigger;
        }

        public void To(TState state)
        {
            State = state;
        }
    }
}