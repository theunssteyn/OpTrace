using OpTrace.Csp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpTrace.Model
{
    public class Node
    {
        public String NodeId { get; set; }
        //public List<String> Alphabet { get; set; }
        public List<Channel> Channels { get; set; }
        public List<Csp.Trace> TxAlphabet { get; set; }
        public List<Csp.Trace> RxAlphabet { get; set; }

        public Node(String id)
        {
            NodeId = id;

            Channels = new List<Channel>();

            /* Generate the alphabet of this node */
            TxAlphabet = new List<Trace>();
            RxAlphabet = new List<Trace>();
            
            
            //Alphabet = new List<string>();
            //Alphabet.Add(String.Format(@"tx_{0}", NodeId));
            //Alphabet.Add(String.Format(@"rx_{0}", NodeId));
            //Alphabet.Add(String.Format(@"m_{0}", NodeId));
            //Alphabet.Add(String.Format(@"w_{0}", NodeId));
        }

        /* This function adds a transmit trace */
        public void AddTxTrace(Csp.Trace trace, Channel.enChannelType chanType)
        {
            TxAlphabet.Add(trace);

            /* Create a transmit channel for this trace */
            AddChannel(trace, chanType);
        }

        /* This function adds a transmit trace */
        public void AddRxTrace(Csp.Trace trace, Channel.enChannelType chanType)
        {
            RxAlphabet.Add(trace);

            /* Create a transmit channel for this trace */
            AddChannel(trace, chanType);
        }

        /* This function adds a transmit and a receive trace */
        /* NOTE: This function receives a trace without directional information */
        public void AddTxRxTrace(Trace trace, Channel.enChannelType chanType)
        {
            String sSyncTraceTx = String.Format("!{0}", trace.Name.Trim(new char[] { '!', '?' }));
            String sSyncTraceRx = String.Format("?{0}", trace.Name.Trim(new char[] { '!', '?' }));

            TxAlphabet.Add(new Csp.Trace(trace) { Name = sSyncTraceTx });
            RxAlphabet.Add(new Csp.Trace(trace) { Name = sSyncTraceRx });

            /* Create a transmit channel for this trace */
            AddChannel(trace, chanType);
        }

        public String GetFirstTxTrace()
        {
            if (TxAlphabet != null && TxAlphabet.Count > 0)
            {
                return TxAlphabet.First().Name;
            }
            else
            {
                return String.Empty;
            }
        }

        private void AddChannel(Trace trace, Channel.enChannelType chanType)
        {
            if (trace.synchronisation)
            {
                /* Add the channel to this Node Process */
                Channel channel = new Channel() { ChanType = chanType, publicTrace = GetPublicTrace(trace), privateTrace = GetPrivateTrace(trace), MasterNode = this };
                Channels.Add(channel);
            }
        }

        /* This function receives a trace and returns the private representation of the trace */
        private Trace GetPrivateTrace(Trace trace)
        {
            Trace tempTrace = new Trace(trace);

            /* Create the name of the private trace: Eg: NodeId = P, Trace.Name = !A ===> p_a */
            tempTrace.Name = String.Format("{0}{1}", tempTrace.Name.TrimStart(new char[] { '!', '?' }), this.NodeId.ToLower());

            return tempTrace;
        }

        /* This function receives a trace and returns the public representation of the trace */
        private Trace GetPublicTrace(Trace trace)
        {
            Trace tempTrace = new Trace(trace);

            /* Create the new name of the trace by removing all directional notation. Eg. !A ===> A */
            tempTrace.Name = tempTrace.Name.TrimStart(new char[] { '!', '?' });

            return tempTrace;
        }


        /* This function returns a list of strings representing the alphabet in a format for the model */
        public List<string> GetModelAlphabet()
        {
            List<String> retList = new List<string>();

            foreach(Csp.Trace trace in TxAlphabet)
            {
                retList.Add(GetTraceName(trace));
            }

            return retList;
        }

        public String GetAlphabetStringOld()
        {
            /*
            aP = {| m_P, tx_P, rx_P, w_P |}
            */

            return String.Format("a{0} = {{| m_{0}, tx_{0}, rx_{0}, w_{0} |}}", NodeId);
        }

        public String GetAlphabetString()
        {
            /*
            aP = {| m_P, tx_P, rx_P, w_P |}
            */

            StringBuilder sb = new StringBuilder();
            List<String> alphabetList = new List<string>();

            /* Get all the tx events */
            foreach (Csp.Trace trace in TxAlphabet)
            {
                alphabetList.Add(GetPrivateTrace(trace).Name);
            }
            foreach (Csp.Trace trace in RxAlphabet)
            {
                alphabetList.Add(GetPrivateTrace(trace).Name);
            }

            /* Ensure that all values are distinct */
            alphabetList = alphabetList.Distinct().ToList();

            /* Generate the striong represenation of the traces */
            foreach (string trace in alphabetList)
            {
                sb.Append(String.Format("{0},", trace));
            }
            String alphbetString = sb.ToString().Trim(new char[] { ',' });

            return String.Format("a{0} = {{| {1} |}}", NodeId, alphbetString);
        }

        public List<string> GetAlphabetStringList2()
        {
            List<string> retList = new List<string>();

            /* Get all the tx events */
            foreach (Csp.Trace trace in TxAlphabet)
            {
                retList.Add(GetPrivateTrace(trace).Name);
            }
            foreach (Csp.Trace trace in RxAlphabet)
            {
                retList.Add(GetPrivateTrace(trace).Name);
            }

            return retList;
        }

        public List<string> GetAlphabetStringList()
        {
            List<string> retList = new List<string>();

            /* Get all the tx events */
            foreach (var chan in Channels)
            {
                retList.Add(chan.privateTrace.Name);
            }

            return retList;
        }



        public String GetDefinitionStringOld()
        {
            //return String.Format("Node{0} = m_{0} -> tx_{0} -> Node{0} [] rx_{0} -> w_{0} -> Node{0}", NodeId);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Node{0} = TX{0} [] RX{0}", NodeId));
            sb.AppendLine(String.Format("TX{0} = m_{0} -> tx_{0} -> Node{0}", NodeId));
            sb.AppendLine(String.Format("RX{0} = rx_{0} -> w_{0} -> Node{0}", NodeId));

            return sb.ToString();
        }

        public String GetDefinitionString()
        {
            //return String.Format("Node{0} = m_{0} -> tx_{0} -> Node{0} [] rx_{0} -> w_{0} -> Node{0}", NodeId);

            List<List<Trace>> txPermutations = GetPermutations(TxAlphabet);
            List<List<Trace>> rxPermutations = GetPermutations(RxAlphabet);
            List<String> txProcess = new List<string>();
            List<String> rxProcess = new List<string>();
            foreach (List<Trace> traceList in txPermutations)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Trace trace in traceList)
                {
                    sb.Append(String.Format("{0} -> ", GetTraceName(trace)));
                }
                sb.Append(String.Format("Node{0}", NodeId));
                txProcess.Add(sb.ToString());
            }
            foreach (List<Trace> traceList in rxPermutations)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Trace trace in traceList)
                {
                    sb.Append(String.Format("{0} -> ", GetTraceName(trace)));
                }
                sb.Append(String.Format("Node{0}", NodeId));
                rxProcess.Add(sb.ToString());
            }

            StringBuilder txsb = new StringBuilder();
            int k = 0;
            for (k = 0; k < txProcess.Count - 1; k++)
            {
                txsb.Append(String.Format("{0} [] ", txProcess[k]));
            }
            txsb.Append(String.Format("{0}", txProcess[k]));

            StringBuilder rxsb = new StringBuilder();
            k = 0;
            for (k = 0; k < rxProcess.Count - 1; k++)
            {
                rxsb.Append(String.Format("{0} [] ", rxProcess[k]));
            }
            rxsb.Append(String.Format("{0}", rxProcess[k]));

            StringBuilder sbret = new StringBuilder();
            sbret.AppendLine(String.Format("Node{0} = TX{0} [] RX{0}", NodeId));
            sbret.AppendLine(String.Format("TX{0} = {1}", NodeId, txsb.ToString()));
            sbret.AppendLine(String.Format("RX{0} = {1}", NodeId, rxsb.ToString()));

            return sbret.ToString();
        }

        public String GetSimpleDefinitionString()
        {
            //return String.Format("Node{0} = m_{0} -> tx_{0} -> Node{0} [] rx_{0} -> w_{0} -> Node{0}", NodeId);

            StringBuilder sb = new StringBuilder();

            /* Add the node process defintion start */
            sb.Append(String.Format("Node{0} = ", this.NodeId));
            
            /* Add a process definition for each trace for which a channel exists */
            if (Channels.Count > 0)
            {
                for (int i = 0; i < Channels.Count-1; i++)
                {
                    sb.Append(String.Format("( {0} -> Node{1} ) []", Channels[i].privateTrace.Name, this.NodeId));
                }
                sb.Append(String.Format("( {0} -> Node{1} )", Channels[Channels.Count-1].privateTrace.Name, this.NodeId));
            }
            
            return sb.ToString();
        }

        public List<List<String>> GetTraces()
        {
            List<List<String>> retList = new List<List<string>>();

            //retList.Add(new List<string>() { "" }); /* Empty trace */
            retList.Add(new List<string>() { String.Format("m_{0}", NodeId) });
            retList.Add(new List<string>() { String.Format("m_{0}", NodeId), String.Format("!B", NodeId) });
            retList.Add(new List<string>() { String.Format("?B", NodeId) });
            retList.Add(new List<string>() { String.Format("?B", NodeId), String.Format("w_{0}", NodeId) });
            
            return retList;
        }

        public List<List<Trace>> GetTracesAdvancedOld()
        {
            List<List<Trace>> retList = new List<List<Trace>>();

            retList.Add(new List<Trace>() { new Trace() { ProcessId = NodeId, Name = String.Format("m_{0}", NodeId)} });
            retList.Add(new List<Trace>() { new Trace() { ProcessId = NodeId, Name = String.Format("m_{0}", NodeId) }, new Trace() { ProcessId = NodeId, Name = String.Format("!B", NodeId), synchronisation = true } });
            retList.Add(new List<Trace>() { new Trace() { ProcessId = NodeId, Name = String.Format("?B", NodeId), synchronisation = true } });
            retList.Add(new List<Trace>() { new Trace() { ProcessId = NodeId, Name = String.Format("?B", NodeId), synchronisation = true }, new Trace() { ProcessId = NodeId, Name = String.Format("w_{0}", NodeId) } });

            return retList;
        }

        public List<List<Trace>> GetTracesAdvancedOld2()
        {
            List<List<Trace>> retList = new List<List<Trace>>();

            retList.Add(new List<Trace>() { new Trace() { ProcessId = NodeId, Name = String.Format("!B", NodeId), synchronisation = true } });
            retList.Add(new List<Trace>() { new Trace() { ProcessId = NodeId, Name = String.Format("?B", NodeId), synchronisation = true } });

            return retList;
        }

        /* This function gets the traces, built from its connected channels */
        public List<List<Trace>> GetTracesAdvancedOld3()
        {
            List<List<Trace>> retList = new List<List<Trace>>();

            List<List<List<Trace>>> txList = new List<List<List<Trace>>>();
            List<List<List<Trace>>> rxList = new List<List<List<Trace>>>();
            /* Iterate through the channels of this process and get the synchronisations */
            foreach (Channel chan in Channels)
            {
                foreach (Trace trace in chan.Alphabet)
                {
                    if (trace.Name.Contains("!"))
                    {
                        /* TX trace */
                        /* We create a copy and rename the trace so that it belongs to this node process */
                        Csp.Trace nodeTrace = new Csp.Trace(trace);
                        nodeTrace.ProcessId = NodeId;
                        txList.Add(new List<List<Trace>>() { new List<Trace>() { nodeTrace } });
                    }
                    else if (trace.Name.Contains("?"))
                    {
                        /* RX Trace */
                        /* We create a copy and rename the trace so that it belongs to this node process */
                        Csp.Trace nodeTrace = new Csp.Trace(trace);
                        nodeTrace.ProcessId = NodeId;
                        rxList.Add(new List<List<Trace>>() { new List<Trace>() { nodeTrace } });
                    }
                }
            }

            /* Now we need all variations and permutations of the TX traces and the RX traces */
            /* Trace calculations can be used with the interleaving operator */
            List<List<Csp.Trace>> alphaTxTrace = txList[0];
            for (int k = 1; k < txList.Count; k++)
            {
                Operators op = new Operators();
                alphaTxTrace = op.OptParallelAdvanced(alphaTxTrace, txList[k], new List<Trace>());
            }

            List<List<Csp.Trace>> alphaRxTrace = rxList[0];
            for (int k = 1; k < rxList.Count; k++)
            {
                Operators op = new Operators();
                alphaRxTrace = op.OptParallelAdvanced(alphaRxTrace, rxList[k], new List<Trace>());
            }

            retList.AddRange(alphaTxTrace);
            retList.AddRange(alphaRxTrace);

            return retList;
        }

        public List<List<Trace>> GetPermutations(List<Trace> TraceList)
        {
            List<List<Trace>> retList = new List<List<Trace>>();

            List<List<List<Trace>>> txList = new List<List<List<Trace>>>();
            /* Iterate through the channels of this process and get the synchronisations */
            foreach (Trace trace in TraceList)
            {
                txList.Add(new List<List<Trace>>() { new List<Trace>() { trace } });
            }
            
            /* Now we need all variations and permutations of the TX traces and the RX traces */
            /* Trace calculations can be used with the interleaving operator */
            List<List<Csp.Trace>> alphaTxTrace = txList[0];
            for (int k = 1; k < txList.Count; k++)
            {
                Operators op = new Operators();
                alphaTxTrace = op.OptParallelAdvanced(alphaTxTrace, txList[k], new List<Trace>());
            }

            retList.AddRange(alphaTxTrace.Where(p => p.Count == TxAlphabet.Count));

            return retList;
        }

        public List<List<Trace>> GetTracesAdvanced()
        {
            List<List<Trace>> retList = new List<List<Trace>>();

            List<List<List<Trace>>> txList = new List<List<List<Trace>>>();
            List<List<List<Trace>>> rxList = new List<List<List<Trace>>>();
            /* Iterate through the channels of this process and get the synchronisations */
            foreach (Trace trace in TxAlphabet)
            {
                txList.Add(new List<List<Trace>>() { new List<Trace>() { trace } });
            }

            foreach (Trace trace in RxAlphabet)
            {
                rxList.Add(new List<List<Trace>>() { new List<Trace>() { trace } });
            }

            /* Now we need all variations and permutations of the TX traces and the RX traces */
            /* Trace calculations can be used with the interleaving operator */
            List<List<Csp.Trace>> alphaTxTrace = txList[0];
            for (int k = 1; k < txList.Count; k++)
            {
                Operators op = new Operators();
                alphaTxTrace = op.OptParallelAdvanced(alphaTxTrace, txList[k], new List<Trace>());
            }

            List<List<Csp.Trace>> alphaRxTrace = rxList[0];
            for (int k = 1; k < rxList.Count; k++)
            {
                Operators op = new Operators();
                alphaRxTrace = op.OptParallelAdvanced(alphaRxTrace, rxList[k], new List<Trace>());
            }

            retList.AddRange(alphaTxTrace);
            retList.AddRange(alphaRxTrace);

            return retList;
        }

        /* This function returns all the traces of the process, with all non-synchronising traces hidden */
        public List<List<Trace>> GetTracesAdvanced2014()
        {
            List<List<Trace>> retList = new List<List<Trace>>();

            /* Add all the tx traces to the list */
            foreach (Trace trace in TxAlphabet)
            {
                retList.Add(new List<Trace>() {  trace });
            }
            /* Add all the rx traces to the list */
            foreach (Trace trace in RxAlphabet)
            {
                retList.Add(new List<Trace>() { trace });
            }
            
            return retList;
        }

        /* This function returns all the traces of the process, with all non-synchronising traces hidden */
        public List<Trace> GetSyncTraces2014()
        {
            List<Trace> retList = new List<Trace>();

            /* Add all the tx traces to the list */
            foreach (Trace trace in TxAlphabet.Where(p => p.synchronisation == true))
            {
                if (!retList.Any(p => p.Name == trace.Name))
                {
                    retList.Add(new Trace(trace));
                }
            }
            /* Add all the rx traces to the list */
            foreach (Trace trace in RxAlphabet.Where(p => p.synchronisation == true))
            {
                if (!retList.Any(p => p.Name == trace.Name))
                {
                    retList.Add(new Trace(trace));
                }
            }

            return retList;
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

        private static string GetTraceName2014(Csp.Trace trace)
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

        ///* Add the channel and link any rx events to this process */
        //public bool AddChannel(Channel channel)
        //{
        //    /* Check for the other node, it can either be the left or right node of the channel */
        //    if (channel.ChannelIdIn.NodeId == this.NodeId)
        //    {
        //        /* We are looking for the Out channel */
        //        /* Only add a link if it has not already been added */
        //        Csp.Trace rxTrace = channel.ChannelIdOut.Alphabet.First(p => p.Name.Contains("!"));
        //    }
        //    else if (channel.ChannelIdOut.NodeId == this.NodeId)
        //    {
        //        /* We are looking for the In channel */
        //        /* Only add a link if it has not already been added */ 

        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    /* Successfull process lookup */
        //    return true;
        //}

    }
}
