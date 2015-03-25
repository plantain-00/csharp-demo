﻿namespace JsonConverter.Nodes
{
    public abstract class JToken
    {
        public abstract string ToString(Formatting formatting, int spaceNumber = 4);

        public override string ToString()
        {
            return ToString(Formatting.None);
        }

        public T As<T>() where T : JToken
        {
            return this as T;
        }
    }
}