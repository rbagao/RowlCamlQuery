using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowlCamlQuery.DecoratorPattern.Sample
{
    public class DecoratorB : IComponent
    {
        IComponent component;
        public string addedState = "past the Coffee Shop ";

        public DecoratorB(IComponent c)
        {
            component = c;
        }

        public string Operation()
        {
            string s = component.Operation();
            s += "to school ";
            return s;
        }

        public string AddedBehavior()
        {
            return "and I bought a cappuccino ";
        }
    }
}
