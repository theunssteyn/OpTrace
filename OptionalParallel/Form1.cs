using OpTrace.Csp;
using OpTrace.Graph;
using OpTrace.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpTrace
{
    public partial class Form1 : Form
    {
        public enum enCommType
        {
            Broadcasting,
            HalfDuplex,
            Simplex
        }

        enCommType commType = enCommType.Broadcasting;
        List<String> adjListLines = new List<string>();

        /* This is the dictionary keeping track of all the processes */
        private Dictionary<String, Dictionary<String, List<String>>> ProcessDictBig = new Dictionary<string,Dictionary<string,List<string>>>();

        private Dictionary<String, List<List<String>>> ProcessDict = new Dictionary<string,List<List<string>>>();
        private Dictionary<String, List<String>> OptParDict = new Dictionary<string, List<string>>();
        private Dictionary<String, List<String>> GenParDict = new Dictionary<string, List<string>>();
        private Dictionary<String, List<String>> InterDict = new Dictionary<string, List<string>>();

        private int line = 0;

        private string modelFile = "";

        /* A list of processes and a list of channels */
        List<Node> nodeList = new List<Node>();
        List<Channel> channelList = new List<Channel>();

        public Form1()
        {
            InitializeComponent();

            bs.DataSource = ProcessDict;
            //listBox1.DataSource = bs;
            //listBox1.DisplayMember = "Key";
        }

        BindingSource bs = new BindingSource();
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonAddProcess_Click(object sender, EventArgs e)
        {
            /* Read the process name and its traces from the relevant textboxes */
            String processName = textBoxProcessName.Text.Trim();

            String traceString = textBoxProcessTraces.Text.Trim();
            /* Now we must make a List<List<String>> of the traces */
            List<List<String>> traceList = new List<List<string>>();
            String[] traceLines = traceString.Split(new char[] { '\r', '\n' });
            foreach (String line in traceLines)
            {
                if (!String.IsNullOrEmpty(line))
                {
                    /* Remove leading and trailing '<', '>' and tokenise on ',' */
                    traceList.Add(line.Trim(new char[] { '<', '>' }).Split(new char[] { ',' }).ToList());
                }
            }

            ProcessDict.Add(processName, traceList);

            Dictionary<String, List<String>> dict = new Dictionary<string, List<string>>();
            foreach (var item in traceList)
            {
                dict.Add(GetKey(item), item);
            }
            ProcessDictBig.Add(processName, dict);

            bs.ResetBindings(false);

            ClearProcessInput();
        }

        private static string GetKey(List<string> history)
        {
            return String.Join("", history);
        }

        private void ClearProcessInput()
        {
            textBoxProcessName.Clear();
            textBoxProcessTraces.Clear();
        }

        private void buttonOptionalParallel_Click(object sender, EventArgs e)
        {
            Operators ops = new Operators();
            OptParDict = ops.OptParallel(ProcessDict["P"], ProcessDict["Q"], ProcessDict["X"][0]);
            printSet(OptParDict, "OptPar");
        }

        private void buttonGeneralParallel_Click(object sender, EventArgs e)
        {
            Operators ops = new Operators();
            GenParDict = ops.GenParallel(ProcessDict["P"], ProcessDict["Q"], ProcessDict["X"][0]);
            printSet(GenParDict, "GenPar");
        }

        private void buttonInterleave_Click(object sender, EventArgs e)
        {
            Operators ops = new Operators();
            InterDict = ops.GenParallel(ProcessDict["P"], ProcessDict["Q"], new List<string>());
            printSet(InterDict, "Interleave");
        }

        private void printSet(IEnumerable<List<string>> setList, string Header = "")
        {
            line = 0; /* Clear the trace number */
            AddOutputLine(String.Format("========{0}========", Header));
            foreach (List<String> item in setList)
            {
                printTrace(item);
            }
            AddOutputLine("===================");
            AddOutputLine();
        }

        private void printSet(Dictionary<String, List<String>> setDict, string Header = "")
        {
            line = 0; /* Clear the trace number */
            AddOutputLine(String.Format("========{0}========", Header));
            foreach (List<String> item in setDict.Values)
            {
                printTrace(item);
            }
            AddOutputLine("===================");
            AddOutputLine();
        }


        private void printSet(List<List<Csp.Trace>> tracesAdvanced, string Header)
        {
            line = 0; /* Clear the trace number */
            AddOutputLine(String.Format("========{0}========", Header));
            foreach (List<Csp.Trace> item in tracesAdvanced)
            {
                printTrace(item);
            }
            AddOutputLine("===================");
            AddOutputLine();
        }

        private void printTrace(List<Csp.Trace> traces)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Csp.Trace trace in traces)
            {
                sb.Append(String.Format("{0},", trace.Name));
            }
            String tempTrace = sb.ToString().Trim(new char[] { ',' });
            AddOutputLine(String.Format("[{00}]: <{1}>", ++line, tempTrace));
        }
        
        private void printTrace(List<String> trace)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in trace)
            {
                sb.Append(String.Format("{0},", str));
            }
            String tempTrace = sb.ToString().Trim(new char[] { ',' });
            AddOutputLine(String.Format("[{00}]: <{1}>", ++line, tempTrace));
        }

        private void AddOutputLine(String text = "")
        {
            Action action = () => richTextBoxOutput.AppendText(String.Format("{0}\n", text));
            this.Invoke(action);
        }

        private void buttonClearOutput_Click(object sender, EventArgs e)
        {
            ProcessDict = new Dictionary<string, List<List<string>>>();
            ProcessDictBig = new Dictionary<string, Dictionary<string, List<string>>>();
            richTextBoxOutput.Clear();
        }

        private void buttonTraceDiff_Click(object sender, EventArgs e)
        {
            Dictionary<String, List<String>> resultDict;
            resultDict = Sets.GetDifferences(GenParDict, OptParDict);

            printSet(resultDict, "(A-B) U (B-A)");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<String, List<String>> resultDict;
            resultDict = OptParDict.RemoveSet(InterDict);

            printSet(resultDict, "Opt-Inter");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<String, List<String>> resultDict;
            resultDict = GenParDict.RemoveSet(InterDict);

            printSet(resultDict, "Gen-Inter");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<String, List<String>> optInter = OptParDict.RemoveSet(InterDict);
            Dictionary<String, List<String>> genInter = GenParDict.RemoveSet(InterDict);

            if (optInter.TraceEquivalent(genInter))
            {
                MessageBox.Show("Equal!");
            }
            else
            {
                MessageBox.Show("Not Equal!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<String> syncList = ProcessDictBig["X"].First().Value;
            Dictionary<String, List<String>> res = ProcessDictBig["P"].OptParallelExt(ProcessDictBig["Q"], syncList).OptParallelExt(ProcessDictBig["R"], syncList);

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            listBox1.DataSource = bs;
            listBox1.DisplayMember = "Key";
            bs.ResetBindings(false);
            //listBox1.ValueMember = "Value";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> ProcessP = new Dictionary<string, List<string>>();
            ProcessP.Add("mp", new List<string> { "m_P" });
            ProcessP.Add("mpA", new List<string> { "m_P", "A" });
            ProcessP.Add("B", new List<string> { "B" });
            ProcessP.Add("Bwp", new List<string> { "B", "w_P" });


            Dictionary<string, List<string>> ProcessQ = new Dictionary<string, List<string>>();
            ProcessQ.Add("mq", new List<string> { "m_Q" });
            ProcessQ.Add("mqB", new List<string> { "m_Q", "B" });
            ProcessQ.Add("A", new List<string> { "A" });
            ProcessQ.Add("Awq", new List<string> { "A", "w_Q" });            
            
            List<string> SyncX = new List<string>();
            new List<string> { "A", "B" };

            Dictionary<String, List<String>> res = ProcessP.OptParallelExt(ProcessQ, SyncX);

            Csp.Model mod = new Csp.Model();
            mod.ModelTraces(res);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dictionary<String, List<String>> testDict = OptParDict.RemoveSet(InterDict)/*.RemoveSet(GenParDict)*/;

            printSet(testDict, "Test");
        }

        //AdjacencyList<String> adj = null;
        EdgeList adj = new EdgeList();
        private List<List<string>> modelTraces;
        private string sAdjFileName = String.Empty;
        private void button7_Click(object sender, EventArgs e)
        {
            //bool bAdjInit = true;
            adjListLines = new List<string>();

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    sAdjFileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                    using (FileStream fs = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (BufferedStream bs = new BufferedStream(fs))
                        {
                            using (StreamReader sr = new StreamReader(bs))
                            {
                                string line;
                                int lineId = 1;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    /* Add the line to the list reference */
                                    adjListLines.Add(line);

                                    /* Break the line up into the definitions */
                                    List<String> parameters = line.Split(new char[] { ',' }).ToList();

                                    /* Parse the line */
                                    if (parameters.Count > 1)
                                    {
                                        /* Initialise the adjacency list */
                                        //if (bAdjInit)
                                        //{
                                        //    bAdjInit = false;
                                        //    adj = new AdjacencyList<string>(parameters[0]);
                                        //}

                                        //for (int i = 1; i < parameters.Count; i++)
                                        //{
                                        //    adj.AddEdge(parameters[0], parameters[i]);
                                        //}

                                        /* Edges */
                                        for (int i = 1; i < parameters.Count; i++)
                                        {
                                            adj.AddEdge(parameters[0], parameters[i], lineId);
                                        }

                                        /* Increment the line id */
                                        lineId++;
                                    }
                                }
                            }
                        }
                    }

                    /* Convert the adjacency list into a list of processes and channels */
                    Translator tl = new Translator();

                    if (radioButtonBroadcast.Checked)
                    {
                        tl.ParseGraphNeigbours(adj);
                        commType = enCommType.Broadcasting;
                    }
                    else if (radioButtonSimplex.Checked)
                    {
                        tl.ParseGraphSimplex(adj);
                        commType = enCommType.Simplex;
                    }
                    else if (radioButtonHalfDuplex.Checked)
                    {
                        tl.ParseGraphHalfDuplex(adj);
                        commType = enCommType.HalfDuplex;
                    }
                    else
                    {
                        tl.ParseGraphP2P(adj);
                    }
                    
                    nodeList = tl.NodeList;
                    channelList = tl.ChannelList;

                    modelTraces = new List<List<string>>();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            /* We need to split the traces, because the files are too large for ProCSP */
            StringBuilder msb = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            if (modelTraces != null)
            {
                if (modelTraces.Count >= 10000)
                {
                    msb = ModelBuilder.BuilModel(nodeList, modelTraces.GetRange(0, 10000));
                }
                else
                {
                    msb = ModelBuilder.BuilModel(nodeList, modelTraces);
                }
            }
            else
            {
                msb = ModelBuilder.BuilModel(nodeList,  new List<List<string>>());
            }

            /* Format the output file according to the input file and the selected communication */
            sb.AppendLine("---------------------------------------");
            sb.AppendLine("-- Auto generated CSPM model by OpTrace");
            sb.AppendLine(String.Format("-- {0}", commType));
            sb.AppendLine("-- Input:");
            sb.Append(FormatAdjacencyListOutput(adjListLines));
            sb.AppendLine("---------------------------------------");
            sb.AppendLine();
            sb.Append(msb.ToString());

            /* Save the file */
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSP Files | *.csp";
                sfd.DefaultExt = "csp";
                sfd.FileName = String.Format("{0}.csp",sAdjFileName);

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString());
                        modelFile = sfd.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Exception:\n{0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private string FormatAdjacencyListOutput(List<string> lines)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var line in lines)
            {
                if (!String.IsNullOrEmpty(line))
                {
                    sb.AppendLine(String.Format("-- {0}", line));
                }
            }

            return sb.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            InvokeModelChecker(modelFile);
        }

        private void InvokeModelChecker(String sFileName)
        {
            Process p = new Process();
            /* What process to call */
            p.StartInfo.FileName = "probcli.exe";
            p.StartInfo.Arguments = String.Format(@"{0} -assertions", sFileName);
            /* Flags */
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.ErrorDialog = false;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.EnableRaisingEvents = true;
            /*Event listeners */
            p.OutputDataReceived += p_OutputDataReceived;
            p.ErrorDataReceived += p_ErrorDataReceived;
            p.Exited += p_Exited;
            /* Start the process */
            p.Start();
            /* Start to read the output */
            p.BeginErrorReadLine();
            p.BeginOutputReadLine();
        }

        void p_Exited(object sender, EventArgs e)
        {
            MessageBox.Show("Finished");
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine(String.Format("ERROR: {0}", e.Data));
            }
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                if (e.Data.StartsWith("[total/"))
                {
                    /* [total/66,true/65,false/1,unknown/0] */
                    String[] results = e.Data.Trim(new char[] { '[', ']' }).Split(new char[] { ',' });

                    foreach (string res in results)
                    {
                        String[] values = res.Split(new char[] { '/' });

                        UpdateModelCheckingResults(values);
                    }

                    //Console.WriteLine(String.Format("DATA: {0}", e.Data));
                    //MessageBox.Show(e.Data);
                }
                else if (e.Data.Contains("is *not* a trace refinement"))
                {
                    Console.WriteLine(e.Data);
                }
                else if (e.Data.Contains("CSP: TRACE"))
                {
                    String sTraceNr = "0";
                    String[] test = e.Data.Split(new char[] { ' ' });
                    foreach (string str in test)
                    {
                        if (str.Contains("TRACE_"))
                        {
                            String[] traceText = str.Split(new char[] { '_' });
                            sTraceNr = traceText[1];
                        }
                    }

                    UpdateTextBox(textBoxTraceCount, sTraceNr);
                }
            }
        }

        private void UpdateModelCheckingResults(string[] result)
        {
            String parameter = result[0];
            String value = result[1];

            switch (parameter)
            {
                case "total":
                    {
                        UpdateTextBox(textBoxResTotal, value);
                    }
                    break;
                case "true":
                    {
                        UpdateTextBox(textBoxResPass, value);
                    }
                    break;
                case "false":
                    {
                        UpdateTextBox(textBoxResFail, value);
                    }
                    break;
                case "unknown":
                    {
                        UpdateTextBox(textBoxResError, value);
                    }
                    break;
                default:
                    break;
            }
        }

        private void UpdateTextBox(TextBox textBox, String text)
        {
            Action action = () => textBox.Text = text;
            this.Invoke(action);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GenerateTraces2014();
        }

        /* This function generates the possible traces with the adjacency list definition */
        private void GenerateTraces()
        {
            /* For the synchronisation set, we iterate through all the processes, get their traces and add their synchronisation traces to a list */
            /* TODO: This will add duplicates, but this will have no effect */
            List<Csp.Trace> syncAdvanced = new List<Csp.Trace>();
            foreach (Node node in nodeList)
            {
                foreach (List<Csp.Trace> traceList in node.GetTracesAdvanced())
                {
                    /* Add all the traces which are sync traces */
                    syncAdvanced.AddRange(traceList.Where(p => p.synchronisation == true));
                }
            }

            List<List<Csp.Trace>> tracesAdvanced = nodeList[0].GetTracesAdvanced();
            for (int k = 1; k < nodeList.Count; k++)
            {
                Operators ops = new Operators();

                /* SYNC TRACES NEED TO BE EXPANDED BEFORE THE NEXT CALL IS MADE */
                tracesAdvanced = ops.OptParallelAdvanced(tracesAdvanced, nodeList[k].GetTracesAdvanced(), syncAdvanced);
                //tracesAdvanced = ExpandSyncTraces(tracesAdvanced);
                Console.WriteLine(tracesAdvanced.Count);
                
            }

            AddOutputLine(String.Format("OptParTh: {0}", tracesAdvanced.Count));

            modelTraces = PrepareTracesForModelChecking(tracesAdvanced);

//            printSet(modelTraces, "OptParTh");

        }

        /* This function generates the possible traces between the processes */
        private void GenerateTraces2014()
        {
            /* For the synchronisation set, we iterate through all the processes, get their traces and add their synchronisation traces to a list */
            /* TODO: This will add duplicates, but this will have no effect */
            List<Csp.Trace> syncAdvancedLeft = new List<Csp.Trace>();
            //foreach (Node node in nodeList)
            //{
            //    foreach (List<Csp.Trace> traceList in node.GetTracesAdvanced2014())
            //    {
            //        /* Add all the traces which are sync traces */
            //        syncAdvanced.AddRange(traceList.Where(p => p.synchronisation == true));
            //    }
            //}

            List<List<Csp.Trace>> tracesAdvanced = nodeList[0].GetTracesAdvanced2014();
            syncAdvancedLeft = nodeList[0].GetSyncTraces2014();
            for (int k = 1; k < nodeList.Count; k++)
            {
                List<Csp.Trace> syncAdvancedRight = nodeList[k].GetSyncTraces2014();
                Operators ops = new Operators();
                tracesAdvanced = ops.OptParallelAdvanced(tracesAdvanced, nodeList[k].GetTracesAdvanced2014(), Intersect(syncAdvancedLeft, syncAdvancedRight));
                syncAdvancedLeft = Union(syncAdvancedLeft, syncAdvancedRight);
            }

            AddOutputLine(String.Format("OptParTh: {0}", tracesAdvanced.Count));

            //modelTraces = PrepareTracesForModelChecking(tracesAdvanced);
            modelTraces = ExpandSyncTraces2014(tracesAdvanced);

            printSet(tracesAdvanced, "OptParTh");
        }

        /* This function gets the intersection of two sets of synchronising traces */
        private List<Csp.Trace> Intersect(List<Csp.Trace> s, List<Csp.Trace> t)
        {
            List<Csp.Trace> retList = new List<Csp.Trace>();

            /* Try to match all the traces of s to the traces of t */
            foreach (var trace in s)
            {
                if (trace.Name.StartsWith("!"))
                {
                    /* Add all "sending" traces */
                    if (!retList.Any(p => p.Name == trace.Name))
                    {
                        retList.Add(new Csp.Trace(trace));
                    }
                }
                else if (trace.Name.StartsWith("?"))
                {
                    /* Only add this trace if there is a corresponging receive trace in the other trace set */
                    String senderTraceName = String.Format("!{0}", trace.Name.TrimStart(new char[] { '?' }));
                    if (t.Any(p => p.Name == senderTraceName))
                    {
                        if (!retList.Any(p => p.Name == trace.Name))
                        {
                            retList.Add(new Csp.Trace(trace));
                        }
                    }
                }
            }

            /* Try to match all the traces of t to the traces of s */
            foreach (var trace in t)
            {
                if (trace.Name.StartsWith("!"))
                {
                    /* Add all "sending" traces */
                    if (!retList.Any(p => p.Name == trace.Name))
                    {
                        retList.Add(new Csp.Trace(trace));
                    }
                }
                else if (trace.Name.StartsWith("?"))
                {
                    /* Only add this trace if there is a corresponging receive trace in the other trace set */
                    String senderTraceName = String.Format("!{0}", trace.Name.TrimStart(new char[] { '?' }));
                    if (s.Any(p => p.Name == senderTraceName))
                    {
                        if (!retList.Any(p => p.Name == trace.Name))
                        {
                            retList.Add(new Csp.Trace(trace));
                        }
                    }
                }
            }

            return retList;
        }

        /* This function gets the union of two sets of synchronising traces */
        private List<Csp.Trace> Union(List<Csp.Trace> s, List<Csp.Trace> t)
        {
            List<Csp.Trace> retList = new List<Csp.Trace>();

            /* Add all traces of s */
            foreach (var trace in s)
            {
                if (!retList.Any(p => p.Name == trace.Name))
                {
                    retList.Add(new Csp.Trace(trace));
                }
            }

            /* Add all traces of t */
            foreach (var trace in t)
            {
                if (!retList.Any(p => p.Name == trace.Name))
                {
                    retList.Add(new Csp.Trace(trace));
                }
            }

            return retList;
        }

        /* Tis function removes invalid traces */
        private void RemoveInvalidTraces(ref List<List<Csp.Trace>> traceList)
        {
            List<List<Csp.Trace>> newTraceList = new List<List<Csp.Trace>>();

            /* Go through all the trace sets */
            foreach (var traces in traceList)
            {
                /* We need to add the empty traces aswell */
                if (traces.Count == 0)
                {
                    newTraceList.Add(traces);
                }
                else if (!traces[0].Name.StartsWith("?"))/* Add all trace sets to the new tracelist which do NOT start with ? */
                {
                    newTraceList.Add(traces);
                }
            }

            /* Set the input traceList to the new tracelist */
            traceList = newTraceList;
        }


        /* This function expands all the synchronisation events as well as the tx and rx events */
        private List<List<Csp.Trace>> ExpandSyncTraces(List<List<Csp.Trace>> traces)
        {
            List<List<Csp.Trace>> retList = new List<List<Csp.Trace>>();

            foreach (List<Csp.Trace> tracelist in traces)
            {
                List<Csp.Trace> expandedTraceList = new List<Csp.Trace>();

                foreach (Csp.Trace trace in tracelist)
                {
                    if (trace.SyncSet != null)
                    {
                        expandedTraceList.Add(new Csp.Trace(trace));
                        
                        /* Debug test */
                        if (trace.SyncSet.Count > 1)
                        {
                            throw new Exception("Trace sync issues");
                        }

                        foreach (Csp.Trace tr in trace.SyncSet)
                        {
                            /* Add all the synchronisations, there should be at most 1 in the list */
                            expandedTraceList.Add(new Csp.Trace(tr));
                        }
                    }
                    else
                    {
                        expandedTraceList.Add(new Csp.Trace(trace));
                    }
                }
                
                //if (expandedTraceList.Count > 0)
                {
                    retList.Add(expandedTraceList);
                }
            }

            return retList;
        }

        /* This function expands all the synchronisation events as well as the tx and rx events */
        /* NOTE: This function always assumes that a private trace as well as a public trace is executed on a trasmit event */
        private List<List<String>> ExpandSyncTraces2014(List<List<Csp.Trace>> traces)
        {
            List<List<String>> retList = new List<List<String>>();

            foreach (List<Csp.Trace> tracelist in traces)
            {
                List<String> expandedTraceList = new List<String>();

                foreach (Csp.Trace trace in tracelist)
                {
                    if (trace.Name.StartsWith("!"))
                    {
                        /* Transmitting trace */
                        expandedTraceList.Add(GetPrivateTrace(trace));
                        expandedTraceList.Add(GetPublicTrace(trace)); 
                    }
                    else if (trace.Name.StartsWith("?"))
                    {
                        /* Receiving trace */
                        expandedTraceList.Add(GetPrivateTrace(trace));
                    }
                    else
                    {
                        expandedTraceList.Add(trace.Name);
                    }
                }

                retList.Add(expandedTraceList);
            }

            return retList;
        }

        /* This function returns the public trace */
        private string GetPublicTrace(Csp.Trace trace)
        {
            return trace.Name.TrimStart(new char[] { '!', '?' });
        }

        private string GetPrivateTrace(Csp.Trace trace)
        {
            return String.Format("{0}{1}", GetPublicTrace(trace), trace.ProcessId.ToLower());
        }

        /* This function expands all the synchronisation events as well as the tx and rx events */
        private List<List<String>> PrepareTracesForModelChecking(List<List<Csp.Trace>> tracesAdvanced)
        {
            List<List<String>> traceListString = new List<List<string>>();

            foreach (List<Csp.Trace> tracelist in tracesAdvanced)
            {
                List<String> traceString = new List<string>();

                foreach (Csp.Trace trace in tracelist)
                {
                    if (trace.SyncSet != null)
                    {
                        traceString.Add(GetTraceName(trace));        /* Save the trace. eg, tx_p*/
                        foreach (Csp.Trace tr in trace.SyncSet)
                        {
                            traceString.Add(GetTraceName(tr));   /* Save the synchronising trace. eq. rx_q */
                        }
                    }
                    else
                    {
                        /* Add the name of the trace to the string trace list */
                        traceString.Add(GetTraceName(trace));
                    }
                }

                traceListString.Add(traceString);
            }

            return traceListString;
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

        /* This function renames the traces based on their type */
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

        private void button11_Click(object sender, EventArgs e)
        {
            Operators ops = new Operators();
            var OptParDict = ops.OptParallelList(ProcessDict["P"], ProcessDict["Q"], ProcessDict["X"][0]);
            printSet(OptParDict, "OptPar");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSP Files | *.csp";
                ofd.DefaultExt = "csp";

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    InvokeModelChecker(ofd.FileName);
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxResPass.Text = "0";
            textBoxResTotal.Text = "0";
            textBoxResFail.Text = "0";
            textBoxResError.Text = "0";
            textBoxTraceCount.Text = "0";
        }
    
    }
}
