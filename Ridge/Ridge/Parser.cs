using System.Collections.Generic;

using Racy;

namespace Ridge
{
    public enum ParseState
    {
        PlainText,
        BeforeTagName,
        InTagName,
        AfterTagName,
        BeforeAttributeName,
        InAttributeName,
        AfterAttributeName,
        BeforeAttributeValue,
        InAttributeValue,
        AfterAttributeValue,
        EndTagHead,
        EndTag,
        Escape,
        InSingleQuote,
        InDoubleQuote,
        InTagTail
    }

    public enum ParseTrigger
    {
        LessThan,
        Blank,
        LargerThan,
        Equals,
        SingleQuote,
        DoubleQuote,
        Escape,
        Text
    }

    public class Node
    {
    }

    public class Tag : Node
    {
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<Node> Children { get; set; }
    }

    public class PlainText : Node
    {
        public string Text { get; set; }
    }

    public class Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Parser
    {
        private readonly StateMachine<ParseState, ParseTrigger> _stateMachine;

        public Parser()
        {
            _stateMachine = new StateMachine<ParseState, ParseTrigger>(ParseState.PlainText);

            _stateMachine.Config(ParseState.PlainText, ParseTrigger.LessThan, ParseState.BeforeTagName);
        }

        public List<Node> Parse(string html)
        {
            var result = new List<Node>();
            foreach (var c in html)
            {
                if (c == '<')
                {
                    if (_stateMachine.CanFire(ParseTrigger.LessThan))
                    {
                        _stateMachine.Fire(ParseTrigger.LessThan);
                    }
                }
            }
            return result;
        }
    }
}