using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowlCamlQuery.DecoratorPattern.RCQ
{
    public class DecoratorB : IComponent
    {
        IComponent component;
        public bool state;

        public DecoratorB(IComponent c)
        {
            component = c;
        }

        public string Operation()
        {
            // Site List and ContentTypeName
            // GetContentTypeColumns
            return "GetContentTypeColumns";
        }

    }
}
