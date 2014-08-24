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
    public partial class MainForm : Form
    {
        public enum enCommType
        {
            Broadcasting,
            HalfDuplex,
            Simplex
        }
        
        /* Private variables */
        private EdgeList adj = new EdgeList();
        private List<List<string>> modelTraces;
        private string sAdjFileName = String.Empty;
        private string modelFile = String.Empty;
        enCommType commType = enCommType.Broadcasting;
        List<String> adjListLines = new List<string>();

        /* A list of processes and a list of channels */
        List<Node> nodeList = new List<Node>();

        /* The current line of the output window (trace number) */
        private int line = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                /* Clear all variables */
                sAdjFileName = String.Empty;
                modelFile = String.Empty;
                adj = new EdgeList();
                modelTraces = new List<List<string>>();
                adjListLines = new List<string>();
                nodeList = new List<Node>();
                richTextBoxOutput.Clear();
                
                bool bFirstLine = true;
                adjListLines = new List<string>();

                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        sAdjFileName = Path.GetFileNameWithoutExtension(ofd.FileName);

                        toolStripStatusLabelFileName.Text = Path.GetFileName(ofd.FileName);
                        
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
                                        /* Remove all leading and trailing white spaces */
                                        line = line.Trim();

                                        /* Add the line to the list reference */
                                        adjListLines.Add(line);

                                        /* Determine the type of synchronisation. Here we are looking for '(' or '{' */
                                        if (bFirstLine)
                                        {
                                            bFirstLine = false;

                                            if (!String.IsNullOrEmpty(line))
                                            {
                                                if (line[0] == '(')
                                                {
                                                    if (line.Length == 5)
                                                    {
                                                        commType = enCommType.Simplex;
                                                    }
                                                    else
                                                    {
                                                        commType = enCommType.Broadcasting;
                                                    }
                                                }
                                                else if (line[0] == '{')
                                                {
                                                    commType = enCommType.HalfDuplex;
                                                }
                                            }
                                        }

                                        /* Break the line up into the definitions, removing the leading and trailing synchronisation selector characters */
                                        List<String> parameters = line.Trim().Trim(new char[] { '(', ')', '{', '}' }).Split(new char[] { ',' }).ToList();

                                        /* Parse the line */
                                        if (parameters.Count > 1)
                                        {
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

                        switch (commType)
                        {
                            case enCommType.Broadcasting:
                                radioButtonBroadcast.Checked = true;
                                tl.ParseGraphNeigbours(adj);
                                break;

                            case enCommType.Simplex:
                                radioButtonSimplex.Checked = true;
                                tl.ParseGraphSimplex(adj);
                                break;

                            case enCommType.HalfDuplex:
                                radioButtonHalfDuplex.Checked = true;
                                tl.ParseGraphHalfDuplex(adj);
                                break;

                            default:
                                break;
                        }

                        nodeList = tl.NodeList;

                        ClearInput();

                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while parsing the in input file, please check that file is in the correct format", "Input File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(sAdjFileName))
            {
                MessageBox.Show("Please load an input file before generating traces", "Input file required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            GenerateTraces();
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            if (nodeList.Count == 0)
            {
                MessageBox.Show("Please load an input file before generating traces", "Input file required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
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
                    msb = ModelBuilder.BuilModel(nodeList, new List<List<string>>());
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
                    sfd.FileName = String.Format("{0}.csp", sAdjFileName);

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
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while building the model file, please check that file is in the correct format", "Input File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }
        }

        private void buttonModelCheck_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(sAdjFileName))
            {
                MessageBox.Show("Please load an input file before generating traces", "Input file required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            InvokeModelChecker(modelFile);
        }

        private void buttonModelCheckFile_Click(object sender, EventArgs e)
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
            ClearInput();
        }

        private void ClearInput()
        {
            textBoxResPass.Text = "0";
            textBoxResTotal.Text = "0";
            textBoxResFail.Text = "0";
            textBoxResError.Text = "0";
            textBoxTraceCount.Text = "0";
        }

        #region ProCSP functions
        private void InvokeModelChecker(String sFileName)
        {
            try
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
            catch
            {
                MessageBox.Show("An error ocurred while trying to invoke the model checker, please ensure that the path to ProB is set-up correclty in the environment variables", "ProB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    /* [total/1,true/1,false/1,unknown/0] */
                    String[] results = e.Data.Trim(new char[] { '[', ']' }).Split(new char[] { ',' });

                    foreach (string res in results)
                    {
                        String[] values = res.Split(new char[] { '/' });

                        UpdateModelCheckingResults(values);
                    }
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

        #endregion


        /* This function generates the possible traces between the processes */
        private void GenerateTraces()
        {
            try
            {
                /* For the synchronisation set, we iterate through all the processes, get their traces and add their synchronisation traces to a list */
                /* TODO: This will add duplicates, but this will have no effect */
                List<Csp.Trace> syncAdvancedLeft = new List<Csp.Trace>();
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

                modelTraces = ExpandSyncTraces2014(tracesAdvanced);

                printSet(modelTraces, "OptParTh");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while generating the traces, please select a valid input file.", "Input File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Trace Manipulation
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
        #endregion

        #region Set Functions
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
        #endregion

        #region Output functions
        private void AddOutputLine(String text = "")
        {
            Action action = () => richTextBoxOutput.AppendText(String.Format("{0}\n", text));
            this.Invoke(action);
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
        #endregion
    }
}
