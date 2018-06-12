using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowlCamlQuery.DecoratorPattern.Sample
{
    public class Client
    {
        static void Display(string s, IComponent c)
        {
            Console.WriteLine(s + c.Operation());
        }

        static void Main()
        {
            Console.WriteLine("Decorator Pattern\n");

            IComponent component = new Component();
            Display("1. Basic component: ", component);
            Display("2. A-decorated: ", new DecoratorA(component));
            Display("3. B-decorated: ", new DecoratorB(component));
            Display("4. B-A-decorated: ", new DecoratorB(new DecoratorA(component)));

            // Explicit DecoratorB
            DecoratorB b = new DecoratorB(new Component());
            Display("5. A-B-decorated: ", new DecoratorA(b));

            // Invoking its added state and added behavior
            Console.WriteLine("\t\t\t" + b.addedState + b.AddedBehavior());
        }
    }
}
