using OpTrace.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpTrace.Model
{
    public class Translator
    {
        public List<Node> NodeList { get; set; }
        public List<Channel> ChannelList { get; set; }

        /* This is the internally generated synchronisation trace for each edge */
        private char syncTrace = 'A';

        public Translator()
        {
            NodeList = new List<Node>();
            ChannelList = new List<Channel>();
        }

        
        /* The channel alphabet is constructed from its connected processes */
        /* Each process only has ONE transmit trace */
        public void ParseGraphNeigbours(EdgeList adj)
        {
            if (adj != null)
            {
                List<Tuple<string, string>> edges = adj.GetEdges();

                /* A list of processes and a list of channels */
                NodeList = new List<Node>();
                ChannelList = new List<Channel>();

                Node transmitNode = null;
                Node receiveNode = null;

                Csp.Trace transmitNodeTrace = null;
                Csp.Trace receiveNodeTrace = null;

                /* Go through the list, create and reuse where nessesary */
                foreach (var connection in edges)
                {
                    string sSyncTrace = null;

                    /* Transmit Node */
                    if (!NodeList.Any(p => p.NodeId == connection.Item1))
                    {
                        Console.WriteLine(String.Format("Creating node {0}", connection.Item1));
                        /* Add the new node to the list */
                        transmitNode = new Node(connection.Item1);
                        /* Add the tx event to the node */
                        sSyncTrace = GetNextTxTrace();
                        transmitNodeTrace = new Csp.Trace() { ProcessId = transmitNode.NodeId, Name = sSyncTrace, synchronisation = true };
                        transmitNode.AddTxTrace(transmitNodeTrace, Channel.enChannelType.Transmit);
                        /* Add the node to the node list */
                        NodeList.Add(transmitNode);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Using node {0}", connection.Item1));
                        /* Get the existing Node */
                        transmitNode = NodeList.First(p => p.NodeId == connection.Item1);
                        /* Get the existing tx trace, if it does not exist, create one */
                        sSyncTrace = transmitNode.GetFirstTxTrace();

                        if (String.IsNullOrEmpty(sSyncTrace))
                        {
                            /* The channel exists, but a transmit trace is not yet defined, it should be defined now */
                            sSyncTrace = GetNextTxTrace();
                            transmitNodeTrace = new Csp.Trace() { ProcessId = transmitNode.NodeId, Name = sSyncTrace, synchronisation = true };
                            transmitNode.AddTxTrace(transmitNodeTrace, Channel.enChannelType.Transmit);
                        }

                        transmitNodeTrace = transmitNode.TxAlphabet.First(p => p.Name.Contains("!"));
                    }

                    /* Receive Node */
                    if (!NodeList.Any(p => p.NodeId == connection.Item2))
                    {
                        Console.WriteLine(String.Format("Creating node {0}", connection.Item2));
                        /* Add the new node to the list */
                        receiveNode = new Node(connection.Item2);
                        /* Add the tx event to the node */
                        receiveNodeTrace = new Csp.Trace() { ProcessId = receiveNode.NodeId, Name = GetRxTrace(sSyncTrace), synchronisation = true };
                        receiveNode.AddRxTrace(receiveNodeTrace, Channel.enChannelType.Receive);
                        /* Add the node to the node list */
                        NodeList.Add(receiveNode);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Using node {0}", connection.Item2));
                        /* Get the existing Node */
                        receiveNode = NodeList.First(p => p.NodeId == connection.Item2);
                        /* Add the current rx trace to the existing node */
                        receiveNodeTrace = new Csp.Trace() { ProcessId = receiveNode.NodeId, Name = GetRxTrace(sSyncTrace), synchronisation = true };
                        receiveNode.AddRxTrace(receiveNodeTrace, Channel.enChannelType.Receive);
                    }
                }
            }
        }

        /* The channel alphabet is constructed from its connected processes */
        public void ParseGraphSimplex(EdgeList adj)
        {
            if (adj != null)
            {
                List<Tuple<string, string>> edges = adj.GetEdges();

                /* A list of processes and a list of channels */
                NodeList = new List<Node>();
                ChannelList = new List<Channel>();

                Node transmitNode = null;
                Node receiveNode = null;

                Csp.Trace transmitNodeTrace = null;
                Csp.Trace receiveNodeTrace = null;

                /* Go through the list, create and reuse where nessesary */
                foreach (var connection in edges)
                {
                    string sSyncTrace = null;

                    /* Transmit Node */
                    if (!NodeList.Any(p => p.NodeId == connection.Item1))
                    {
                        Console.WriteLine(String.Format("Creating node {0}", connection.Item1));
                        /* Add the new node to the list */
                        transmitNode = new Node(connection.Item1);
                        /* Add the tx event to the node */
                        sSyncTrace = GetNextTxTrace();
                        transmitNodeTrace = new Csp.Trace() { ProcessId = transmitNode.NodeId, Name = sSyncTrace, synchronisation = true };
                        transmitNode.AddTxTrace(transmitNodeTrace, Channel.enChannelType.Transmit);
                        /* Add the node to the node list */
                        NodeList.Add(transmitNode);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Using node {0}", connection.Item1));
                        /* Get the existing Node */
                        transmitNode = NodeList.First(p => p.NodeId == connection.Item1);
                        /* Add the tx event to the node */
                        sSyncTrace = GetNextTxTrace();
                        transmitNodeTrace = new Csp.Trace() { ProcessId = transmitNode.NodeId, Name = sSyncTrace, synchronisation = true };
                        transmitNode.AddTxTrace(transmitNodeTrace, Channel.enChannelType.Transmit);
                    }

                    /* Receive Node */
                    if (!NodeList.Any(p => p.NodeId == connection.Item2))
                    {
                        Console.WriteLine(String.Format("Creating node {0}", connection.Item2));
                        /* Add the new node to the list */
                        receiveNode = new Node(connection.Item2);
                        /* Add the tx event to the node */
                        receiveNodeTrace = new Csp.Trace() { ProcessId = receiveNode.NodeId, Name = GetRxTrace(sSyncTrace), synchronisation = true };
                        receiveNode.AddRxTrace(receiveNodeTrace, Channel.enChannelType.Receive);
                        /* Add the node to the node list */
                        NodeList.Add(receiveNode);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Using node {0}", connection.Item2));
                        /* Get the existing Node */
                        receiveNode = NodeList.First(p => p.NodeId == connection.Item2);
                        /* Add the current rx trace to the existing node */
                        receiveNodeTrace = new Csp.Trace() { ProcessId = receiveNode.NodeId, Name = GetRxTrace(sSyncTrace), synchronisation = true };
                        receiveNode.AddRxTrace(receiveNodeTrace, Channel.enChannelType.Receive);
                    }
                }
            }
        }


            /* The channel alphabet is constructed from its connected processes */
        public void ParseGraphHalfDuplex(EdgeList adj)
        {
            if (adj != null)
            {
                List<Tuple<string, int>> vertices = adj.GetVertices();

                /* A list of processes and a list of channels */
                NodeList = new List<Node>();

                /* Go through the list, create and reuse where nessesary */
                foreach (var process in vertices)
                {
                    String sSyncTrace = GetSyncTraceFromIndex(process.Item2);
                    Node node = null;

                    if (!NodeList.Any(p => p.NodeId == process.Item1))
                    {
                        Console.WriteLine(String.Format("Creating node {0}", process.Item1));
                        /* Add the new node to the list */
                        node = new Node(process.Item1);
                        /* Add the node to the node list */
                        NodeList.Add(node);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Using node {0}", process.Item1));
                        /* Get the existing node */
                        node = NodeList.First(p => p.NodeId == process.Item1);
                    }

                    /* Add the event to the node */
                    Csp.Trace nodeTrace = new Csp.Trace() { ProcessId = node.NodeId, Name = sSyncTrace, synchronisation = true };
                    node.AddTxRxTrace(nodeTrace, Channel.enChannelType.Transceive);
                }
            }
        }

        private static string GetSyncTraceFromIndex(int index)
        {
            string traceName = String.Empty;
            int divider = index;
            int modultor;

            while (divider > 0)
            {
                modultor = (divider - 1) % 26;
                traceName = Convert.ToChar(65 + modultor).ToString() + traceName;
                divider = (int)((divider - modultor) / 26);
            }

            return traceName;
        }

        // (A = 1, B = 2...AA = 27...AAA = 703...)
        private static int GetColNumberFromName(string columnName)
        {
            char[] characters = columnName.ToUpperInvariant().ToCharArray();
            int sum = 0;
            for (int i = 0; i < characters.Length; i++)
            {
                sum *= 26;
                sum += (characters[i] - 'A' + 1);
            }
            return sum;
        }

        
        private char GetNextSyncTrace()
        {
            return syncTrace++;
        }

        private String GetNextTxTrace()
        {
            return String.Format("!{0}", syncTrace++);
        }

        /* This function receives a tx trace (eg. !A) and converts it to an rx trace ?A) */
        private String GetRxTrace(String txTrace)
        {
            return String.Format("?{0}",txTrace.TrimStart(new char[] { '!', '?' }));
        }


        /* the channel alphabet is constructed from its connected processes */
        /* Each process only has ONE transmit trace */
        public void ParseGraphP2P(EdgeList adj)
        {
            if (adj != null)
            {
                List<Tuple<string, string>> edges = adj.GetEdges();

                /* A list of processes and a list of channels */
                NodeList = new List<Node>();
                ChannelList = new List<Channel>();

                Node leftNode = null;
                Node rightNode = null;

                Csp.Trace leftNodeTxTrace = null;
                Csp.Trace rightNodeTxTrace = null;

                char syncChar = 'A';

                /* Go through the list, create and reuse where nessesary */
                foreach (var connection in edges)
                {
                    /* Left Node */
                    if (NodeList.Any(p => p.NodeId == connection.Item1))
                    {
                        Console.WriteLine(String.Format("Using node {0}", connection.Item1));
                        /* Get the existing Node */
                        leftNode = NodeList.First(p => p.NodeId == connection.Item1);
                        /* Add the tx event to the node */
                        leftNodeTxTrace = new Csp.Trace() { ProcessId = leftNode.NodeId, Name = String.Format("!{0}", syncChar++), synchronisation = true };
                        leftNode.TxAlphabet.Add(leftNodeTxTrace);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Creating node {0}", connection.Item1));
                        /* Add the new node to the list */
                        leftNode = new Node(connection.Item1);
                        /* Add the tx event to the node */
                        leftNodeTxTrace = new Csp.Trace() { ProcessId = leftNode.NodeId, Name = String.Format("!{0}", syncChar++), synchronisation = true };
                        leftNode.TxAlphabet.Add(leftNodeTxTrace);
                        /* Add the node to the node list */
                        NodeList.Add(leftNode);
                    }

                    /* Right Node */
                    if (NodeList.Any(p => p.NodeId == connection.Item2))
                    {
                        Console.WriteLine(String.Format("Using node {0}", connection.Item2));
                        /* Get the existing Node */
                        rightNode = NodeList.First(p => p.NodeId == connection.Item2);
                        /* Add the tx event to the node */
                        rightNodeTxTrace = new Csp.Trace() { ProcessId = rightNode.NodeId, Name = String.Format("!{0}", syncChar++), synchronisation = true };
                        rightNode.TxAlphabet.Add(rightNodeTxTrace);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Creating node {0}", connection.Item2));
                        /* Add the new node to the list */
                        rightNode = new Node(connection.Item2);
                        /* Add the tx event to the node */
                        rightNodeTxTrace = new Csp.Trace() { ProcessId = rightNode.NodeId, Name = String.Format("!{0}", syncChar++), synchronisation = true };
                        rightNode.TxAlphabet.Add(rightNodeTxTrace);
                        /* Add the node to the node list */
                        NodeList.Add(rightNode);
                    }

                    /* Channel <-> */
                    Console.WriteLine(String.Format("Creating channel c[{0}:{1}]", connection.Item1, connection.Item2));

                    /* Add the channel to the channel list */
                    Channel channel = new Channel(leftNode, rightNode);
                    ChannelList.Add(channel);

                    /* Add communicating traces to the nodes and the channels */
                    /* Left Node Traces */
                    Csp.Trace leftNodeTxChan = new Csp.Trace(leftNodeTxTrace) { ProcessId = String.Format("{0}{1}", leftNode.NodeId, rightNode.NodeId) };   /* eg. !A */
                    Csp.Trace rightNodeRxChan = new Csp.Trace(leftNodeTxChan);                                                                              /* eg. ?A */
                    rightNodeRxChan.Name = rightNodeRxChan.Name.Replace('!', '?');

                    /* Right Node Traces */
                    Csp.Trace rightNodeTxChan = new Csp.Trace(rightNodeTxTrace) { ProcessId = String.Format("{0}{1}", leftNode.NodeId, rightNode.NodeId) }; /* eg. !B */
                    Csp.Trace leftNodeRxChan = new Csp.Trace(rightNodeTxChan);                                                                              /* eg. ?B */
                    leftNodeRxChan.Name = leftNodeRxChan.Name.Replace('!', '?');

                    /* Assign left node Tx trace to the channel */
                    channel.leftTxTrace = leftNodeTxChan;       /* eg. !A */
                    channel.rightRxTrace = rightNodeRxChan;     /* eg. ?A */
                    channel.rightTxTrace = rightNodeTxChan;    /* eg. !B */
                    channel.leftRxTrace = leftNodeRxChan;       /* eg. ?B */

                    /* Link the channel back to the nodes */
                    leftNode.Channels.Add(channel);
                    rightNode.Channels.Add(channel);

                    /* Add the Rx traces to the nodes */
                    leftNode.RxAlphabet.Add(new Csp.Trace(leftNodeRxChan) { ProcessId = leftNode.NodeId });     /* eg. ?B */
                    rightNode.RxAlphabet.Add(new Csp.Trace(rightNodeRxChan) { ProcessId = rightNode.NodeId });  /* eg. ?A */
                }
            }
        }

        private void Stuff()
        {
            ///* Add communicating traces to the nodes and the channels */
            ///* Left Node Traces */
            //Csp.Trace leftNodeTx = new Csp.Trace() { ProcessId = leftNode.NodeId, Name = String.Format("!{0}", syncChar), synchronisation = true };
            //Csp.Trace leftNodeTxChan = new Csp.Trace() { ProcessId = String.Format("{0}{1}", leftNode.NodeId, rightNode.NodeId), Name = String.Format("!{0}", syncChar), synchronisation = true };

            ///* Right Node Traces */
            //Csp.Trace rightNodeTx = new Csp.Trace() { ProcessId = rightNode.NodeId, Name = String.Format("!{0}", syncChar), synchronisation = true };
            //Csp.Trace rightNodeTxChan = new Csp.Trace() { ProcessId = String.Format("{0}{1}", leftNode.NodeId, rightNode.NodeId), Name = String.Format("!{0}", syncChar), synchronisation = true };

            //leftNode.Alphabet.Add(leftNodeTx);
            //rightNode.Alphabet.Add(rightNodeTx);
        }
    }
}
