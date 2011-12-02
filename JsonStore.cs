using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Net
{
    public class JsonStore : Store
    {
        public string Target
        {
            get;
            set;
        }

        public string IdAttribute
        {
            get;
            set;
        }

        public string ServiceObject
        {
            get;
            set;
        }
    }
}
