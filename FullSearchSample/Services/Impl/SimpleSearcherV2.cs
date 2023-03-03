using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearchSample.Services.Impl
{
    public class SimpleSearcherV2
    {
        public void SearchV1 (string word, IEnumerable<string> data)
        {
            foreach (var item in data)
            {
                if(item.Contains(word, StringComparison.InvariantCultureIgnoreCase))
                    Console.WriteLine(item);
            }
        }

    }
}
