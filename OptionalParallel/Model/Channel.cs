using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpTrace.Model
{
    public class Channel
    {
        /* The types of channel definitions */
        public enum enChannelType
        {
            Transmit,
            Receive,
            Transceive
        }
        
        public Node ChannelIdIn { get; set; }  /* Left */
        public Node ChannelIdOut { get; set; } /* Right */
        public List<Csp.Trace> Alphabet { get; private set; }

        /* A Channel has 4 traces */
        public Csp.Trace leftTxTrace { get; set; }
        public Csp.Trace leftRxTrace { get; set; }
        public Csp.Trace rightTxTrace { get; set; }
        public Csp.Trace rightRxTrace { get; set; }

        /* New version of Channel */
        public String Name 
        {
            get
            {
                return String.Format("Chan{0}_{1}", MasterNode.NodeId, publicTrace.Name);
            }
        }
        public Csp.Trace privateTrace { get; set; }
        public Csp.Trace publicTrace { get; set; }
        public Node MasterNode { get; set; }
        public enChannelType ChanType { get; set; }

        public Channel()
        {

        }

        public Channel(Node idIn, Node idOut)
        {
            this.ChannelIdIn = idIn;
            this.ChannelIdOut = idOut;

            this.Alphabet = new List<Csp.Trace>();
        }

        public String GetAlphabetStringOld()
        {
            /*
             aCPQ = {| tx_P, tx_Q, rx_Q, rx_P |}
             */

            return String.Format("aC{0}{1} = {{| {2}, {3}, {4}, {5} |}}", ChannelIdIn.NodeId, ChannelIdOut.NodeId, GetTraceName(leftTxTrace), GetTraceName(leftRxTrace), GetTraceName(rightTxTrace), GetTraceName(rightRxTrace));
        }

        public String GetAlphabetString()
        {
            /*
             aCPQ = {| tx_P, tx_Q, rx_Q, rx_P |}
             */

            return String.Format("aC{0}{2} = {{| {1}, {2} |}}", MasterNode.NodeId, privateTrace.Name, publicTrace.Name);
        }

        public String GetDefinitionStringOld()
        {
            /*  ChanRQ = (tx_R -> (rx_Q -> ChanRQ [] ChanRQ)) [] (rx_Q -> ChanRQ) 
                ChanQR = (tx_Q -> (rx_R -> ChanQR [] ChanQR)) [] (rx_R -> ChanQR) 
                CHANRQ = ChanRQ|||ChanQR
            */
            //return String.Format("CHAN{0}{1} = ((tx_{0} -> (rx_{1} -> CHAN{0}{1} [] CHAN{0}{1})) [] (rx_{1} -> CHAN{0}{1})) ||| ((tx_{1} -> (rx_{0} -> CHAN{0}{1} [] CHAN{0}{1})) [] (rx_{0} -> CHAN{0}{1}))", ChannelIdIn, ChannelIdOut);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Chan{0}{1} = ({2} -> ({3} -> Chan{0}{1} [] Chan{0}{1})) [] ({3} -> Chan{0}{1})", ChannelIdIn.NodeId, ChannelIdOut.NodeId, GetTraceName(leftTxTrace), GetTraceName(leftRxTrace)));
            sb.AppendLine(String.Format("Chan{1}{0} = ({2} -> ({3} -> Chan{1}{0} [] Chan{1}{0})) [] ({3} -> Chan{1}{0})", ChannelIdIn.NodeId, ChannelIdOut.NodeId, GetTraceName(rightTxTrace), GetTraceName(rightRxTrace)));
            sb.AppendLine(String.Format("CHAN{0}{1} = Chan{0}{1}|||Chan{1}{0}", ChannelIdIn.NodeId, ChannelIdOut.NodeId));

            return sb.ToString();
        }

        public String GetDefinitionString()
        {
            /*  ChanRQ = (tx_R -> (rx_Q -> ChanRQ [] ChanRQ)) [] (rx_Q -> ChanRQ) 
                ChanQR = (tx_Q -> (rx_R -> ChanQR [] ChanQR)) [] (rx_R -> ChanQR) 
                CHANRQ = ChanRQ|||ChanQR
            */
            //return String.Format("CHAN{0}{1} = ((tx_{0} -> (rx_{1} -> CHAN{0}{1} [] CHAN{0}{1})) [] (rx_{1} -> CHAN{0}{1})) ||| ((tx_{1} -> (rx_{0} -> CHAN{0}{1} [] CHAN{0}{1})) [] (rx_{0} -> CHAN{0}{1}))", ChannelIdIn, ChannelIdOut);



            StringBuilder sb = new StringBuilder();

            switch (ChanType)
            {
                case enChannelType.Transmit:
                    {
                        sb.Append(String.Format("Chan{0}_{2} = ({1} -> ({2} -> Chan{0}_{2} [] Chan{0}_{2}))", MasterNode.NodeId, privateTrace.Name, publicTrace.Name));
                    }
                    break;
                case enChannelType.Receive:
                    {
                        sb.Append(String.Format("Chan{0}_{1} = ({1} -> ({2} -> Chan{0}_{1} [] Chan{0}_{1}))", MasterNode.NodeId, publicTrace.Name, privateTrace.Name));
                    }
                    break;
                case enChannelType.Transceive:
                    {
                        sb.Append(String.Format("Chan{0}_{2} = ({1} -> ({2} -> Chan{0}_{2} [] Chan{0}_{2})) [] ({2} -> ({1} -> Chan{0}_{2} [] Chan{0}_{2}))", MasterNode.NodeId, privateTrace.Name, publicTrace.Name));
                    }
                    break;
                default:
                    break;
            }
            
            return sb.ToString();
        }

        private static string GetTraceName(Csp.Trace trace)
        {
            String retString = null;

            if (trace.Name.Contains("!"))
            {
                retString = trace.Name.Replace("!", "tx");
            }
            else if (trace.Name.Contains("?"))
            {
                retString = trace.Name.Replace("?", "rx"); ;
            }
            else
            {
                retString = trace.Name;
            }

            return retString;
        }

        public List<string> GetAlphabetStringListOld()
        {
            return new List<string>() { GetTraceName(leftTxTrace), GetTraceName(leftRxTrace), GetTraceName(rightTxTrace), GetTraceName(rightRxTrace) };
        }

        /* The channel has only one public and one private trace to report */
        public List<string> GetAlphabetStringList()
        {
            return new List<string>() { privateTrace.Name, publicTrace.Name };
        }
    }
}
