using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowlCamlQuery.DecoratorPattern.Sample
{
    public class Component : IComponent
    {
        public string Operation()
        {
            return "I am walking";
        }
    }
}
