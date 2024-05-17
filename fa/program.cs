using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fans
{
    public class State
    {
        public State(string name, bool isAcceptState)
        {
            Name = name;
            IsAcceptState = isAcceptState;
            Transitions = new Dictionary<char, State>();

        }

        public string Name { get; }
        public bool IsAcceptState { get; }
        public Dictionary<char, State> Transitions { get; private set; }

        public void SetTransition(char ch, State state) => Transitions.Add(ch, state);
    }

    public abstract class AFA
    {
        protected abstract State InitialState { get; }
        public bool? Run(IEnumerable<char> s)
        {
            State currentState = InitialState;
            foreach (var c in s)
            {
                currentState = currentState.Transitions[c];
                if (currentState == null)
                    return null;
            }
            return currentState.IsAcceptState;

        }
    }

    public class FA1 : AFA
    {
        private readonly static State a = new State("a", false);
        private readonly static State b = new State("b", true);
        private readonly static State c = new State("c", false);

        public FA1()
        {
            a.Transitions['0'] = b;
            a.Transitions['1'] = a;
            b.Transitions['0'] = c;
            b.Transitions['1'] = b;
            c.Transitions['0'] = c;
            c.Transitions['1'] = c;
        }

        protected override State InitialState => a;
    }

    public class FA2 : AFA
    {
        private readonly static State a = new State("a", false);
        private readonly static State b = new State("b", false);
        private readonly static State c = new State("c", true);
        private readonly static State d = new State("d", false);

        public FA2()
        {
            a.Transitions['0'] = d;
            a.Transitions['1'] = b;
            b.Transitions['0'] = c;
            b.Transitions['1'] = a;
            c.Transitions['0'] = b;
            c.Transitions['1'] = d;
            d.Transitions['0'] = a;
            d.Transitions['1'] = c;
        }

        protected override State InitialState => a;
    }

    public class FA3 : AFA
    {
        private readonly static State a = new State("a", false);
        private readonly static State b = new State("b", false);
        private readonly static State c = new State("c", true);

        public FA3()
        {
            a.Transitions['0'] = a;
            a.Transitions['1'] = b;
            b.Transitions['0'] = a;
            b.Transitions['1'] = c;
            c.Transitions['0'] = c;
            c.Transitions['1'] = c;
        }

        protected override State InitialState => a;
    }

    class Program
    {
        static void Main(string[] args)
        {
            String s = "01111";
            FA1 fa1 = new FA1();
            bool? result1 = fa1.Run(s);
            Console.WriteLine(result1);
            FA2 fa2 = new FA2();
            bool? result2 = fa2.Run(s);
            Console.WriteLine(result2);
            FA3 fa3 = new FA3();
            bool? result3 = fa3.Run(s);
            Console.WriteLine(result3);
        }
    }
}
