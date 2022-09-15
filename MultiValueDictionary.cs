using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_3._0
{
    public class MultiValueDictionary<Key, Value> : Dictionary<Key, List<Value>>
    {

        public void Add(Key key, Value value)
        {
            List<Value> values;
            if (!this.TryGetValue(key, out values))
            {
                values = new List<Value>();
                this.Add(key, values);
            }
            values.Add(value);
        }

    }
}
