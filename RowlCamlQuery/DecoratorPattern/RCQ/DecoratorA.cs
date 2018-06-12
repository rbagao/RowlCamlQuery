using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowlCamlQuery.DecoratorPattern.RCQ
{
    public class DecoratorA : IComponent
    {
        IComponent component;
        public DecoratorA(IComponent c)
        {
            component = c;
        }

        public string Operation()
        {
            // Site List and ContentTypeName
            // GetContentTypes
            return "GetContentTypes";
        }

    }
}
