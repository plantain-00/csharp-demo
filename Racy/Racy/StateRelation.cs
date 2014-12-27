namespace Racy
{
    public class StateRelation<TState, TTrigger>
    {
        public StateRelation(TState from, TTrigger by, TState to)
        {
            From = from;
            By = by;
            To = to;
        }

        public TState From { get; set; }
        public TTrigger By { get; set; }
        public TState To { get; set; }
    }
}