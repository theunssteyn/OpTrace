using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpTrace.Csp
{
    public class Trace
    {
        public String ProcessId { get; set; }        
        public String Name { get; set; }        
        public List<Trace> SyncSet { get; set; }
        public bool synchronisation { get; set; }
        public bool Synced { get; set; }

        public Trace()
        {
            SyncSet = new List<Trace>();
            Synced = false;
        }

        /* Copy Constructor */
        public Trace(Trace trace)
        {
            if (trace != null)
            {
                this.synchronisation = trace.synchronisation;
                this.Name = trace.Name;
                this.ProcessId = trace.ProcessId;
                this.Synced = trace.Synced;

                this.SyncSet = new List<Trace>();
                /* Go through all possible synced traces */
                foreach (Trace tr in trace.SyncSet)
                { 
                    /* Add a copy if the synced trace */
                    this.SyncSet.Add(new Trace(tr));
                }
            }
        }

        /* Return the name of the trace */
        public override string ToString()
        {
            return String.Format("{0}{1}", ProcessId, Name);
        }

        
    }
}
