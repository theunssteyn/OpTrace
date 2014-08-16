using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpTrace.Model
{
    public class ModelBuilder
    {
        private static string GetAlphabetString(List<String> alphabetList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string parameter in alphabetList)
            {
                sb.Append(parameter);
                sb.Append(",");
            }

            return "{|" + sb.ToString().Trim(new char[] { ',' }) + "|}";
        }

        private static string GetParallelOperatorString(List<String> leftAlpha, List<String> rightAlpha)
        {
            /*
             * [ aP || aCPQ ]
             */
            return String.Format("[ {0} || {1} ]", GetAlphabetString(leftAlpha), GetAlphabetString(rightAlpha));
        }

        private static string GetParallelOperatorStringNew(String leftAlpha, String rightAlpha)
        {
            /*
             * [ aP || aCPQ ]
             */
            return String.Format("[ {0} || {1} ]", leftAlpha, rightAlpha);
        }

        private static String BuildSystem(List<Node> nodeList, List<Channel> channelList)
        {
            /*
             * SYSTEM = ((((((NodeP [ aP || aCPQ ] CHANPQ) [ union(aP,aCPQ) || aQ ] NodeQ) [ union(union(aP, aCPQ), aQ) || aCPR ] CHANPR) [ union(union(union(aP, aCPQ) , aQ), aCPR) || aR ] NodeR) [ union(union(union(union(aP, aCPQ) , aQ), aCPR), aR) || aCRQ ] CHANRQ) [ union(union(union(union(union(aP, aCPQ) , aQ), aCPR), aR), aCRQ) || aCPS ] CHANPS) [ union(union(union(union(union(union(union(aP, aCPQ) , aQ), aCPR), aR), aCRQ), aR),aCPS) || aS ] NodeS
             */

            /* We should start with the first Node pair, and then add the nodes thereafter */
            /* We should dissolve the alphabets maybe? */
            /* (NodeP [ aP || aCPQ ] CHANPQ) [ union(aP,aCPQ) || aQ ] NodeQ) */

            StringBuilder sb = new StringBuilder();
            Node previousNode = null;
            List<string> previousAlphabet = new List<string>();
            String sysExpression = "";
            foreach (Node currentNode in nodeList)
            {
                if (previousNode == null)
                {
                    /* This is the first time */
                    previousNode = currentNode;
                    previousAlphabet = previousAlphabet.Union(previousNode.GetAlphabetStringList()).ToList(); /* Save the alphabet history */
                }
                else
                {
                    if (sysExpression == "")
                    {
                        sysExpression = String.Format("(Node{0} {1} Node{2})", previousNode.NodeId, GetParallelOperatorString(previousNode.GetAlphabetStringList(), currentNode.GetAlphabetStringList()), currentNode.NodeId);
                        previousAlphabet = previousAlphabet.Union(currentNode.GetAlphabetStringList()).ToList();
                    }
                    else
                    {
                        sysExpression = String.Format("({0} {1} Node{2})", sysExpression, GetParallelOperatorString(previousAlphabet, currentNode.GetAlphabetStringList()), currentNode.NodeId);
                        previousAlphabet = previousAlphabet.Union(currentNode.GetAlphabetStringList()).ToList();
                    }
                }
            }

            /* Now we add the channels */
            foreach (Channel currentChannel in channelList)
            {
                sysExpression = String.Format("({0} {1} CHAN{2}{3})", sysExpression, GetParallelOperatorString(previousAlphabet, currentChannel.GetAlphabetStringList()), currentChannel.ChannelIdIn.NodeId, currentChannel.ChannelIdOut.NodeId);
                previousAlphabet = previousAlphabet.Union(currentChannel.GetAlphabetStringList()).ToList();
            }

            return sysExpression;
        }

        private static String BuildSystem2014(List<Node> nodeList)
        {
            /*
             * SYSTEM = ((((((NodeP [ aP || aCPQ ] CHANPQ) [ union(aP,aCPQ) || aQ ] NodeQ) [ union(union(aP, aCPQ), aQ) || aCPR ] CHANPR) [ union(union(union(aP, aCPQ) , aQ), aCPR) || aR ] NodeR) [ union(union(union(union(aP, aCPQ) , aQ), aCPR), aR) || aCRQ ] CHANRQ) [ union(union(union(union(union(aP, aCPQ) , aQ), aCPR), aR), aCRQ) || aCPS ] CHANPS) [ union(union(union(union(union(union(union(aP, aCPQ) , aQ), aCPR), aR), aCRQ), aR),aCPS) || aS ] NodeS
             */

            /* We should start with the first Node pair, and then add the nodes thereafter */
            /* We should dissolve the alphabets maybe? */
            /* (NodeP [ aP || aCPQ ] CHANPQ) [ union(aP,aCPQ) || aQ ] NodeQ) */

            StringBuilder sb = new StringBuilder();
            Channel previousChan = null;
            List<string> previousAlphabet = new List<string>();
            String sysExpression = "";

            /* All processes are interleaved */
            sb.Append("( ");
            for (int i = 0; i < nodeList.Count - 1; i++)
            {
                sb.Append(String.Format("Node{0} ||| ", nodeList[i].NodeId));
            }
            sb.Append(String.Format("Node{0} )", nodeList[nodeList.Count - 1].NodeId));

            /* General Parallel with all the channels */
            sb.Append(String.Format(" [| {{{0}}} |] ", FormatPrivateChannelSyncSet(nodeList)));

            foreach (Node currentNode in nodeList)
            {
                foreach (var currentChannel in currentNode.Channels)
                {
                    if (previousChan == null)
                    {
                        /* This is the first time */
                        previousChan = currentChannel;
                        previousAlphabet = previousAlphabet.Union(previousChan.GetAlphabetStringList()).ToList(); /* Save the alphabet history */
                    }
                    else
                    {
                        if (sysExpression == "")
                        {
                            sysExpression = String.Format("({0} {1} {2})", previousChan.Name, GetParallelOperatorString(previousChan.GetAlphabetStringList(), currentChannel.GetAlphabetStringList()), currentChannel.Name);
                            previousAlphabet = previousAlphabet.Union(currentChannel.GetAlphabetStringList()).ToList();
                        }
                        else
                        {
                            sysExpression = String.Format("({0} {1} {2})", sysExpression, GetParallelOperatorString(previousAlphabet, currentChannel.GetAlphabetStringList()), currentChannel.Name);
                            previousAlphabet = previousAlphabet.Union(currentChannel.GetAlphabetStringList()).ToList();
                        }
                    }
                }
            }

            sb.Append(sysExpression);

            return sb.ToString();
        }

        private static string FormatPrivateChannelSyncSet(List<Node> nodeList)
        {
            StringBuilder sb = new StringBuilder();

            List<String> distictList = new List<string>();
            foreach (var node in nodeList)
            {
                foreach (var chan in node.Channels)
                {
                    distictList.Add(chan.privateTrace.Name);
                }
            }
            /* Remove duplicates */
            distictList = distictList.Distinct().ToList();
            for (int i = 0; i < distictList.Count - 1; i++)
            {
                sb.Append(String.Format("{0}, ", distictList[i]));
            }
            sb.Append(String.Format("{0}", distictList[distictList.Count - 1]));

            return sb.ToString();
        }

        public static StringBuilder BuilModel(List<Tuple<String, String>> nodeDef, List<List<String>> traceList)
        {
            /* This is the definition */
            //List<Tuple<String, String>> nodeDef = new List<Tuple<string, string>>();
            //nodeDef.Add(new Tuple<string, string>("P", "Q"));
            //nodeDef.Add(new Tuple<string, string>("P", "R"));
            //nodeDef.Add(new Tuple<string, string>("P", "S"));
            //nodeDef.Add(new Tuple<string, string>("R", "Q"));

            /* Creat a list of processes and a list of channels */
            List<Node> nodeList = new List<Node>();
            List<Channel> channelList = new List<Channel>();

            //List<String> nodeDefs = new List<string>();
            //List<String> channelDefs = new List<string>();

            /* Go through the list, create and reuse where nessesary */
            foreach (var connection in nodeDef)
            {
                /* Left Node */
                if (nodeList.Any(p => p.NodeId == connection.Item1))
                {
                    Console.WriteLine(String.Format("Using node {0}", connection.Item1));
                }
                else
                {
                    Console.WriteLine(String.Format("Creating node {0}", connection.Item1));
                    /* Add the new node to the list */
                    nodeList.Add(new Node(connection.Item1));
                }

                /* Right Node */
                if (nodeList.Any(p => p.NodeId == connection.Item2))
                {
                    Console.WriteLine(String.Format("Using node {0}", connection.Item2));
                }
                else
                {
                    Console.WriteLine(String.Format("Creating node {0}", connection.Item2));
                    /* Add the new node to the list */
                    nodeList.Add(new Node(connection.Item2));
                }

                /* Channel <-> */
                Console.WriteLine(String.Format("Creating channel c[{0}:{1}]", connection.Item1, connection.Item2));
                /* Add the channel to the channel list */
                //channelList.Add(new Channel(connection.Item1, connection.Item2));

                ///* Now we get the aplabets of each process */
                //List<string> aLeft = new List<string>();
                //aLeft.Add(String.Format(@"tx_{0}", connection.Item1));
                //aLeft.Add(String.Format(@"rx_{0}", connection.Item1));
                //aLeft.Add(String.Format(@"m_{0}", connection.Item1));
                //aLeft.Add(String.Format(@"w_{0}", connection.Item1));

                //List<string> aRight = new List<string>();
                //aRight.Add(String.Format(@"tx_{0}", connection.Item2));
                //aRight.Add(String.Format(@"rx_{0}", connection.Item2));
                //aRight.Add(String.Format(@"m_{0}", connection.Item2));
                //aRight.Add(String.Format(@"w_{0}", connection.Item2));

                //List<string> aChannel1 = new List<string>();
                //aChannel1.Add(String.Format(@"tx_{0}", connection.Item1));
                //aChannel1.Add(String.Format(@"rx_{0}", connection.Item2));

                //List<string> aChannel2 = new List<string>();
                //aChannel2.Add(String.Format(@"tx_{0}", connection.Item2));
                //aChannel2.Add(String.Format(@"rx_{0}", connection.Item1));

                //List<string> aCHAN = aChannel1.Union(aChannel1).ToList();

                /* Node defnitions*/
                /* We have a list for process definitions */


                /* Channel definition */
                /* We have a list for channel definitions */

            }

            /* At this point, we know exactly how many uniqe nodes and channels there are */
            StringBuilder sb = new StringBuilder();
            sb.Append("channel ");
            foreach (Node node in nodeList)
            {
                string Id = node.NodeId;
                sb.Append(String.Format(@"tx_{0},", Id));
                sb.Append(String.Format(@"rx_{0},", Id));
                //sb.Append(String.Format(@"m_{0},", Id));
                //sb.Append(String.Format(@"w_{0},", Id));
            }
            String sysChannelString = sb.ToString().Trim(new char[] { ',' });


            /*********************************
             * NOW WE BUILD THE CSP_{M} FILE *
             *********************************/
            StringBuilder cspmSb = new StringBuilder();
            /* CSP Channel definition */
            cspmSb.AppendLine(sysChannelString);
            cspmSb.AppendLine();
            /* Node Definitions */
            foreach (Node node in nodeList)
            {
                cspmSb.AppendLine(node.GetAlphabetString());
                cspmSb.AppendLine(node.GetDefinitionString());
                cspmSb.AppendLine();
            }
            /* Channel Definitions */
            foreach (Channel channel in channelList)
            {
                cspmSb.AppendLine(channel.GetAlphabetString());
                cspmSb.AppendLine(channel.GetDefinitionString());
                cspmSb.AppendLine();
            }

            foreach (var item in nodeList)
            {
                Console.WriteLine(String.Format(@"Node: {0}",item.NodeId));
            }

            foreach (var item in channelList)
            {
                Console.WriteLine(String.Format(@"Channel: in[{0}] out[{1}]", item.ChannelIdIn.NodeId, item.ChannelIdOut.NodeId));
            }


            /* Add the system definition */
            cspmSb.AppendLine(String.Format("SYSTEM = {0}", BuildSystem(nodeList, channelList)));
            cspmSb.AppendLine("MAIN = SYSTEM");

            cspmSb.AppendLine();
            cspmSb.AppendLine(BuildTraceAssertions(traceList));
            cspmSb.AppendLine();

            return cspmSb;

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

        /* This function builds the CSP model based on the process definitions and the generated traces */
        /* NOTE: This function only inserts the sync traces, all other traces are omitted */
        public static StringBuilder BuilModel(List<Node> nodeList, List<List<String>> traceList)
        {
            /**************************
             * CSP CHANNEL DEFINITION *
             **************************/
            StringBuilder sb = new StringBuilder();
            sb.Append("channel ");
            /* Build a list with unique channel events */
            List<String> cspChannelList = new List<string>();
            foreach (var node in nodeList)
            {
                foreach (var channel in node.Channels)
                {
                    /* Add the trace between the Node and the Channel (Private Trace) */
                    if (!cspChannelList.Contains(channel.privateTrace.Name))
                    {
                        cspChannelList.Add(channel.privateTrace.Name);
                    }

                    /* Add the trace between the Channel and the other Channels (Public Trace) */
                    if (!cspChannelList.Contains(channel.publicTrace.Name))
                    {
                        cspChannelList.Add(channel.publicTrace.Name);
                    }
                }
            }
            /* Format the list of string into the correct csp format */            
            foreach (String traceName in cspChannelList)
            {
                sb.Append(String.Format(@"{0},", traceName));
            }
            String sysChannelString = sb.ToString().Trim(new char[] { ',' });

            /*********************************
             * NOW WE BUILD THE CSP_{M} FILE *
             *********************************/
            StringBuilder cspmSb = new StringBuilder();
            /* CSP Channel definition */
            cspmSb.AppendLine(sysChannelString);
            cspmSb.AppendLine();
            /* Node Definitions */
            foreach (Node node in nodeList)
            {
                cspmSb.AppendLine(node.GetAlphabetString());
                cspmSb.AppendLine(node.GetSimpleDefinitionString());
                cspmSb.AppendLine();
            }
            /* Channel Definitions */
            foreach (var node in nodeList)
            {
                foreach (var channel in node.Channels)
                {
                    cspmSb.AppendLine(channel.GetAlphabetString());
                    cspmSb.AppendLine(channel.GetDefinitionString());
                    cspmSb.AppendLine();
                }
            }

            //Console.WriteLine(cspmSb.ToString());

            /* Add the system definition */
            cspmSb.AppendLine(String.Format("SYSTEM = {0}", BuildSystem2014(nodeList)));
            cspmSb.AppendLine("MAIN = SYSTEM");

            cspmSb.AppendLine();
            cspmSb.AppendLine(BuildTraceAssertions(traceList));

            return cspmSb;

        }

        private static String BuildTraceAssertions(List<List<String>> traceList)
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;

            foreach (List<String> trace in traceList)
            {
                String assertionName = String.Format("TRACE_{0}", i);
                sb.Append(assertionName);
                sb.Append(" = ");
                sb.Append(BuildTraces(trace));
                sb.AppendLine();
                sb.Append("assert MAIN [T= ");
                sb.Append(assertionName);
                sb.AppendLine();

                i++;
            }

            return sb.ToString();
        }

        private static String BuildTraces(List<String> traces)
        {
            StringBuilder sb = new StringBuilder();

            foreach (String trace in traces)
            {
                if ((String.IsNullOrEmpty(trace) || String.IsNullOrWhiteSpace(trace)))
                {
                    throw new Exception();
                }
                sb.Append(trace);
                sb.Append(" -> ");
            }
            sb.Append("STOP");

            return sb.ToString();
        }
    }
}
