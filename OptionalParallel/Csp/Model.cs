using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpTrace.Csp
{
    public class Model
    {
        public void ModelTraces(Dictionary<string, List<string>> translatedDict)
        {
            /* T54a = D -> mq -> wp -> B -> STOP
             * assert MAIN [T= T54a
             */

            foreach (var item in translatedDict)
            {
                String modelString = String.Format(@"T{0} = {1}
assert MAIN [T= T{0}", item.Key, BuildTraces2(item.Value));
                Console.WriteLine(modelString);
            }
        }

        private String BuildTraces2(List<String> traces)
        {
            traces.Add("STOP");
            return String.Join(" -> ", traces);
        }

    }
}
