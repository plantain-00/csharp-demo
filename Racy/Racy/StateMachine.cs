using System;
using System.Collections.Generic;
using System.Linq;

namespace Racy
{
    public class StateMachine<TState, TTrigger> where TState : struct where TTrigger : struct
    {
        private readonly Dictionary<TState, Action<StateRelation<TState, TTrigger>>> _onEntryActions;
        private readonly Dictionary<TState, Action<StateRelation<TState, TTrigger>>> _onExitActions;
        private readonly List<StateRelation<TState, TTrigger>> _relations;

        public StateMachine(TState state)
        {
            State = state;
            _relations = new List<StateRelation<TState, TTrigger>>();
            _onEntryActions = new Dictionary<TState, Action<StateRelation<TState, TTrigger>>>();
            _onExitActions = new Dictionary<TState, Action<StateRelation<TState, TTrigger>>>();
        }

        public TState State { get; private set; }

        public void Config(TState from, TTrigger by, TState to)
        {
            var relation = _relations.FirstOrDefault(c => Equals(c.By, by) && Equals(c.From, from));
            if (relation == null)
            {
                _relations.Add(new StateRelation<TState, TTrigger>(from, by, to));
            }
            else
            {
                relation.To = to;
            }
        }

        public void ConfigOnEntry(TState state, Action<StateRelation<TState, TTrigger>> action)
        {
            if (_onEntryActions.ContainsKey(state))
            {
                _onEntryActions[state] = action;
            }
            else
            {
                _onEntryActions.Add(state, action);
            }
        }

        public void ConfigOnExit(TState state, Action<StateRelation<TState, TTrigger>> action)
        {
            if (_onExitActions.ContainsKey(state))
            {
                _onExitActions[state] = action;
            }
            else
            {
                _onExitActions.Add(state, action);
            }
        }

        public bool CanFire(TTrigger trigger)
        {
            var relation = _relations.FirstOrDefault(c => Equals(c.By, trigger) && Equals(c.From, State));
            return relation != null;
        }

        public void Fire(TTrigger trigger)
        {
            var relation = _relations.FirstOrDefault(c => Equals(c.By, trigger) && Equals(c.From, State));
            if (relation == null)
            {
                throw new Exception("cannot fire the trigger at current state.");
            }

            State = relation.To;

            if (_onEntryActions.ContainsKey(relation.From))
            {
                _onEntryActions[relation.From](relation);
            }

            if (_onExitActions.ContainsKey(relation.To))
            {
                _onExitActions[relation.To](relation);
            }
        }
    }
}