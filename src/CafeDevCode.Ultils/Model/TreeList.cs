using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Ultils.Model
{
    public class TreeList<T>
    {
        public int Level { get; set; }
        public T? Node { get; set; }
        public IEnumerable<TreeList<T>>? Childrens { get; set; }
    }
}
