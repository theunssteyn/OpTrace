using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpTrace.Csp
{
    public static class OperatorsExt
    {
        public static Dictionary<String, List<String>> OptParallelExt(this Dictionary<String, List<String>> TraceSet1, Dictionary<String, List<String>> TraceSet2, List<String> SyncSet)
        {
            /* We reset the results container */
            Dictionary<string, List<string>> resultDictionary = new Dictionary<string, List<string>>();

            /* Run the algo */
            foreach (var Trace1 in TraceSet1)
            {
                foreach (var Trace2 in TraceSet2)
                {
                    optParRecNew(Trace1.Value, Trace2.Value, SyncSet, new List<string>(), resultDictionary);
                }
            }

            /* Return results */
            return resultDictionary;
        }

        public static Dictionary<String, List<String>> OptParallelExt(List<String> Trace1, List<String> Trace2, List<String> SyncSet)
        {
            /* We reset the results container */
            Dictionary<string, List<string>> resultDictionary = new Dictionary<string, List<string>>();

            /* Run the algo */
            optParRecNew(Trace1, Trace2, SyncSet, new List<string>(), resultDictionary);

            /* Return results */
            return resultDictionary;
        }

        private static void SaveTraceToDictionary(Dictionary<String, List<string>> dict, List<string> history)
        {
            try
            {
                String key = GetKey(history);
                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, history);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("HASH FAIL: {0}", ex.Message));
            }
        }

        private static string GetKey(List<string> history)
        {
            return String.Join("", history);
        }

        private static void optParRecNew(List<String> s, List<String> t, List<String> x, List<String> history, Dictionary<String, List<String>> resultDict)
        {
            SaveTraceToDictionary(resultDict, history);

            if ((s.Count == 0) && (t.Count == 0))
            {
                return;
            }
            else if (s.Count == 0)
            {
                List<string> hist2a = new List<string>(history);
                hist2a.Add(t[0]);
                optParRecNew(s, t.GetRange(1, t.Count - 1), x, hist2a, resultDict);
            }
            else if (t.Count == 0)
            {
                List<string> hist1a = new List<string>(history);
                hist1a.Add(s[0]);
                optParRecNew(s.GetRange(1, s.Count - 1), t, x, hist1a, resultDict);
            }
            else
            {
                /* Check if either s or t is in the sync set */
                if (x.Contains(s[0]))
                {
                    if ((s[0][1] == t[0][1]) && (((s[0][0] == '?') && (t[0][0] == '!')) || ((s[0][0] == '!') && (t[0][0] == '?')))) /* Sync: (?x-!x) or (?x-!x) */
                    {
                        List<String> hists = new List<string>(history);
                        hists.Add(s[0]);
                        optParRecNew(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, hists, resultDict); /* s and t move on */
                    }
                }
                else if (x.Contains(t[0]))
                {
                    if ((s[0][1] == t[0][1]) && (((s[0][0] == '?') && (t[0][0] == '!')) || ((s[0][0] == '!') && (t[0][0] == '?')))) /* Sync: (?x-!x) or (?x-!x) */
                    {
                        List<String> histt = new List<string>(history);
                        histt.Add(t[0]);
                        optParRecNew(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, histt, resultDict); /* s and t move on */
                    }
                }

                /* Here we split, because we have 2 combinations */
                List<string> hist1 = new List<string>(history);
                hist1.Add(s[0]);
                optParRecNew(s.GetRange(1, s.Count - 1), t, x, hist1, resultDict);


                List<string> hist2 = new List<string>(history);
                hist2.Add(t[0]);
                optParRecNew(s, t.GetRange(1, t.Count - 1), x, hist2, resultDict);
            }
        }
    }

    public class Operators
    {
        private Dictionary<String, List<String>> setDictionary;
        private Dictionary<String, List<Trace>> setDictionaryAdvanced;

        public Operators()
        {
            setDictionary = new Dictionary<string, List<string>>();
            setDictionaryAdvanced = new Dictionary<string, List<Trace>>();
        }

        public List<List<String>> OptParallelList(List<List<String>> TraceSet1, List<List<String>> TraceSet2, List<String> SyncSet)
        {
            /* We reset the results container */
            setDictionary = new Dictionary<string, List<string>>();

            /* Run the algo */
            foreach (var Trace1 in TraceSet1)
            {
                foreach (var Trace2 in TraceSet2)
                {
                    optParRec(Trace1, Trace2, SyncSet, new List<string>());
                }
            }

            /* Return results */
            return setDictionary.Values.ToList();
        }

        public List<List<Trace>> OptParallelAdvanced(List<List<Trace>> TraceSet1, List<List<Trace>> TraceSet2, List<Trace> SyncSet)
        {
            /* We reset the results container */
            setDictionaryAdvanced = new Dictionary<string, List<Trace>>();

            /* Run the algo */
            foreach (var Trace1 in TraceSet1)
            {
                foreach (var Trace2 in TraceSet2)
                {
                    optParRecAdv(Trace1, Trace2, SyncSet, new List<Trace>(), new List<Trace>());
                }
            }

            /* Return results */
            return setDictionaryAdvanced.Values.ToList();
        }

        public Dictionary<String, List<String>> OptParallel(List<List<String>> TraceSet1, List<List<String>> TraceSet2, List<String> SyncSet)
        {
            /* We reset the results container */
            setDictionary = new Dictionary<string, List<string>>();

            /* Run the algo */
            foreach (var Trace1 in TraceSet1)
            {
                foreach (var Trace2 in TraceSet2)
                {
                    optParRec(Trace1, Trace2, SyncSet, new List<string>());
                }
            }

            /* Return results */
            return setDictionary;
        }

        public Dictionary<String, List<String>> OptParallel(List<String> Trace1, List<String> Trace2, List<String> SyncSet)
        {
            /* We reset the results container */
            setDictionary = new Dictionary<string, List<string>>();

            /* Run the algo */
            optParRec(Trace1, Trace2, SyncSet, new List<string>());

            /* Return results */
            return setDictionary;
        }

        public Dictionary<String, List<String>> GenParallel(List<List<String>> TraceSet1, List<List<String>> TraceSet2, List<String> SyncSet)
        {
            /* We reset the results container */
            setDictionary = new Dictionary<string, List<string>>();

            /* Run the algo */
            foreach (var Trace1 in TraceSet1)
            {
                foreach (var Trace2 in TraceSet2)
                {
                    genParRec(Trace1, Trace2, SyncSet, new List<string>());
                }
            }

            /* Return results */
            return setDictionary;
        }

        public Dictionary<String, List<String>> GenParallel(List<String> Trace1, List<String> Trace2, List<String> SyncSet)
        {
            /* We reset the results container */
            setDictionary = new Dictionary<string, List<string>>();

            /* Run the algo */
            genParRec(Trace1, Trace2, SyncSet, new List<string>());

            /* Return results */
            return setDictionary;
        }

        void genParRec(List<String> s, List<String> t, List<String> x, List<String> history)
        {
            SaveTraceToDictionary(history);

            if ((s.Count == 0) && (t.Count == 0))
            {
                return;
            }
            else if (s.Count == 0)
            {
                /* Check that t[0] is not contained in the sync set */
                if (x.Contains(t[0]))
                {
                    List<String> histt = new List<string>(history);
                    histt.Add("DL");
                    SaveTraceToDictionary(histt);
                    return;
                }
                else
                {
                    List<string> hist2a = new List<string>(history);
                    hist2a.Add(t[0]);
                    genParRec(s, t.GetRange(1, t.Count - 1), x, hist2a);
                }
            }
            else if (t.Count == 0)
            {
                /* Check that t[0] is not contained in the sync set */
                if (x.Contains(s[0]))
                {
                    List<String> hists = new List<string>(history);
                    hists.Add("DL");
                    SaveTraceToDictionary(hists);
                    return;
                }
                else
                {
                    List<string> hist1a = new List<string>(history);
                    hist1a.Add(s[0]);
                    genParRec(s.GetRange(1, s.Count - 1), t, x, hist1a);
                }
            }
            else
            {

                /* Check if either s or t is in the sync set */
                if (x.Contains(s[0]))
                {
                    if (s[0] == t[0]) /* Sync */
                    {
                        List<String> hists = new List<string>(history);
                        hists.Add(s[0]);
                        genParRec(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, hists); /* s and t move on */
                    }
                    else
                    {
                        /* s needs to block */

                        if (x.Contains(t[0])) /* Here we have deadlock, s is waiting for t and t is waiting for s */
                        {
                            List<String> histt = new List<string>(history);
                            histt.Add("DL");
                            SaveTraceToDictionary(histt);
                            return;
                        }
                        else
                        {
                            List<String> histt = new List<string>(history);
                            histt.Add(t[0]);
                            genParRec(s, t.GetRange(1, t.Count - 1), x, histt); /* s stays current, t moves on */
                        }
                    }
                }
                else if (x.Contains(t[0]))
                {
                    if (s[0] == t[0]) /* Sync */
                    {
                        List<String> histt = new List<string>(history);
                        histt.Add(t[0]);
                        genParRec(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, histt); /* s and t move on */
                    }
                    else
                    {
                        /* s needs to block */

                        if (x.Contains(s[0])) /* Here we have deadlock, s is waiting for t and t is waiting for s */
                        {
                            List<String> hists = new List<string>(history);
                            hists.Add("DL");
                            SaveTraceToDictionary(hists);
                            return;
                        }
                        else
                        {
                            List<String> hists = new List<string>(history);
                            hists.Add(s[0]);
                            genParRec(s.GetRange(1, s.Count - 1), t, x, hists); /* s moves on, t stays current */
                        }
                    }
                }
                else
                {
                    /* Here we split, because we have 2 combinations */
                    List<string> hist1 = new List<string>(history);
                    hist1.Add(s[0]);
                    genParRec(s.GetRange(1, s.Count - 1), t, x, hist1);


                    List<string> hist2 = new List<string>(history);
                    hist2.Add(t[0]);
                    genParRec(s, t.GetRange(1, t.Count - 1), x, hist2);
                }
            }
        }

        void genParRecC(List<String> s, List<String> t, List<String> x, List<String> history)
        {
            SaveTraceToDictionary(history);

            if ((s.Count == 0) && (t.Count == 0))
            {
                return;
            }
            else if (s.Count == 0)
            {
                /* Check that t[0] is not contained in the sync set */
                if (x.Contains(t[0]))
                {
                    List<String> histt = new List<string>(history);
                    histt.Add("DL");
                    SaveTraceToDictionary(histt);
                    return;
                }
                else
                {
                    List<string> hist2a = new List<string>(history);
                    hist2a.Add(t[0]);
                    genParRecC(s, t.GetRange(1, t.Count - 1), x, hist2a);
                }
            }
            else if (t.Count == 0)
            {
                /* Check that t[0] is not contained in the sync set */
                if (x.Contains(s[0]))
                {
                    List<String> hists = new List<string>(history);
                    hists.Add("DL");
                    SaveTraceToDictionary(hists);
                    return;
                }
                else
                {
                    List<string> hist1a = new List<string>(history);
                    hist1a.Add(s[0]);
                    genParRecC(s.GetRange(1, s.Count - 1), t, x, hist1a);
                }
            }
            else
            {

                /* Check if either s or t is in the sync set */
                if (x.Contains(s[0]))
                {
                    if ((s[0][1] == t[0][1]) && (((s[0][0] == '?')&&(t[0][0] == '!')) || ((s[0][0] == '!')&&(t[0][0] == '?')))) /* Sync: (?x-!x) or (?x-!x) */
                    {
                        List<String> hists = new List<string>(history);
                        hists.Add(s[0]);
                        genParRecC(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, hists); /* s and t move on */
                    }
                    else
                    {
                        /* s needs to block */

                        if (x.Contains(t[0])) /* Here we have deadlock, s is waiting for t and t is waiting for s */
                        {
                            List<String> histt = new List<string>(history);
                            histt.Add("DL");
                            SaveTraceToDictionary(histt);
                            return;
                        }
                        else
                        {
                            List<String> histt = new List<string>(history);
                            histt.Add(t[0]);
                            genParRecC(s, t.GetRange(1, t.Count - 1), x, histt); /* s stays current, t moves on */
                        }
                    }
                }
                else if (x.Contains(t[0]))
                {
                    if ((s[0][1] == t[0][1]) && (((s[0][0] == '?') && (t[0][0] == '!')) || ((s[0][0] == '!') && (t[0][0] == '?')))) /* Sync: (?x-!x) or (?x-!x) */
                    {
                        List<String> histt = new List<string>(history);
                        histt.Add(t[0]);
                        genParRecC(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, histt); /* s and t move on */
                    }
                    else
                    {
                        /* s needs to block */

                        if (x.Contains(s[0])) /* Here we have deadlock, s is waiting for t and t is waiting for s */
                        {
                            List<String> hists = new List<string>(history);
                            hists.Add("DL");
                            SaveTraceToDictionary(hists);
                            return;
                        }
                        else
                        {
                            List<String> hists = new List<string>(history);
                            hists.Add(s[0]);
                            genParRecC(s.GetRange(1, s.Count - 1), t, x, hists); /* s moves on, t stays current */
                        }
                    }
                }
                else
                {
                    /* Here we split, because we have 2 combinations */
                    List<string> hist1 = new List<string>(history);
                    hist1.Add(s[0]);
                    genParRecC(s.GetRange(1, s.Count - 1), t, x, hist1);


                    List<string> hist2 = new List<string>(history);
                    hist2.Add(t[0]);
                    genParRecC(s, t.GetRange(1, t.Count - 1), x, hist2);
                }
            }
        }

        private void optParRec(List<String> s, List<String> t, List<String> x, List<String> history)
        {
            SaveTraceToDictionary(history);

            if ((s.Count == 0) && (t.Count == 0))
            {
                return;
            }
            else if (s.Count == 0)
            {
                List<string> hist2a = new List<string>(history);
                hist2a.Add(t[0]);
                optParRec(s, t.GetRange(1, t.Count - 1), x, hist2a);
            }
            else if (t.Count == 0)
            {
                List<string> hist1a = new List<string>(history);
                hist1a.Add(s[0]);
                optParRec(s.GetRange(1, s.Count - 1), t, x, hist1a);
            }
            else
            {
                /* Check if either s or t is in the sync set */
                if (x.Contains(s[0]))
                {
                    if (s[0] == t[0]) /* Sync: (x-x)*/
                    {
                        List<String> hists = new List<string>(history);
                        hists.Add(s[0]);
                        optParRec(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, hists); /* s and t move on */
                    }
                }
                else if (x.Contains(t[0]))
                {
                    if (s[0] == t[0]) /* Sync: (x-x)*/
                    {
                        List<String> histt = new List<string>(history);
                        histt.Add(t[0]);
                        optParRec(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, histt); /* s and t move on */
                    }
                }

                /* Here we split, because we have 2 combinations */
                List<string> hist1 = new List<string>(history);
                hist1.Add(s[0]);
                optParRec(s.GetRange(1, s.Count - 1), t, x, hist1);


                List<string> hist2 = new List<string>(history);
                hist2.Add(t[0]);
                optParRec(s, t.GetRange(1, t.Count - 1), x, hist2);
            }
        }

        private void optParRecC(List<String> s, List<String> t, List<String> x, List<String> history)
        {
            SaveTraceToDictionary(history);

            if ((s.Count == 0) && (t.Count == 0))
            {
                return;
            }
            else if (s.Count == 0)
            {
                List<string> hist2a = new List<string>(history);
                hist2a.Add(t[0]);
                optParRecC(s, t.GetRange(1, t.Count - 1), x, hist2a);
            }
            else if (t.Count == 0)
            {
                List<string> hist1a = new List<string>(history);
                hist1a.Add(s[0]);
                optParRecC(s.GetRange(1, s.Count - 1), t, x, hist1a);
            }
            else
            {
                /* Check if either s or t is in the sync set */
                if (x.Contains(s[0]))
                {
                    if ((s[0][1] == t[0][1]) && (((s[0][0] == '?') && (t[0][0] == '!')) || ((s[0][0] == '!') && (t[0][0] == '?')))) /* Sync: (?x-!x) or (?x-!x) */
                    {
                        List<String> hists = new List<string>(history);
                        hists.Add(s[0]);
                        optParRecC(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, hists); /* s and t move on */
                    }
                }
                else if (x.Contains(t[0]))
                {
                    if ((s[0][1] == t[0][1]) && (((s[0][0] == '?') && (t[0][0] == '!')) || ((s[0][0] == '!') && (t[0][0] == '?')))) /* Sync: (?x-!x) or (?x-!x) */
                    {
                        List<String> histt = new List<string>(history);
                        histt.Add(t[0]);
                        optParRecC(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, histt); /* s and t move on */
                    }
                }

                /* Here we split, because we have 2 combinations */
                List<string> hist1 = new List<string>(history);
                hist1.Add(s[0]);
                optParRecC(s.GetRange(1, s.Count - 1), t, x, hist1);


                List<string> hist2 = new List<string>(history);
                hist2.Add(t[0]);
                optParRecC(s, t.GetRange(1, t.Count - 1), x, hist2);
            }
        }

        /* Current 2014 */
        private void optParRecAdv(List<Trace> s, List<Trace> t, List<Trace> x, List<Trace> history, List<Trace> activeSyncTrace)
        {
            SaveTraceToDictionary(history);

            if ((s.Count == 0) && (t.Count == 0))
            {
                return;
            }
            else if (s.Count == 0)
            {
                List<Trace> hist2a = new List<Trace>(history);
                
                /* If we have a transmit trace, add and move on */
                if (t[0].Name.StartsWith("!"))
                {
                    hist2a.Add(new Trace(t[0]));
                    optParRecAdv(s, t.GetRange(1, t.Count - 1), x, hist2a, activeSyncTrace);
                    
                    /* Add the whole range */
                    //hist2a.AddRange(t);
                    //SaveTraceToDictionary(hist2a);
                    //return;
                }
                else if (t[0].Name.StartsWith("?"))
                {
                    /* If synchronisation on this trace is active */
                    if (IsSyncActive(hist2a, t[0]))
                    {
                        /* If this trace has not been seen previously in this trace sequence given in the history */
                        if (!HasBeenSeen(hist2a, t[0]))
                        {
                            /* A sync is active for this trace, and it has not yet been observed, add it and continue */
                            hist2a.Add(new Trace(t[0]));
                            optParRecAdv(s, t.GetRange(1, t.Count - 1), x, hist2a, activeSyncTrace);
                        }
                        else
                        {
                            /* This is a deadlocked sequence, we should stop. A "DL" trace is added to show this */
                            return;
                        }
                    }
                    else
                    /* We have a receive trace, only add it if it is NOT in the syncset */
                    if (!x.Any(p => p.Name == t[0].Name))
                    {
                        hist2a.Add(new Trace(t[0]));
                        optParRecAdv(s, t.GetRange(1, t.Count - 1), x, hist2a, activeSyncTrace);
                    }
                    else
                    {
                        /* This is a deadlocked sequence, we should stop. A "DL" trace is added to show this */
                        //hist2a.Add(new Csp.Trace(t[0]) { Name = "DL" });
                        //SaveTraceToDictionary(hist2a);
                        return;
                    }
                }
                else
                {
                    /* This is not a transmit/receive trace, it can be added */
                    /* Note: This routine makes eplicit use of the transmit and receive trace names */
                    hist2a.Add(new Trace(t[0]));
                    optParRecAdv(s, t.GetRange(1, t.Count - 1), x, hist2a, activeSyncTrace);
                }
            }
            else if (t.Count == 0)
            {
                List<Trace> hist1a = new List<Trace>(history);

                /* If we have a transmit trace, add and move on */
                if (s[0].Name.StartsWith("!"))
                {
                    hist1a.Add(new Trace(s[0]));
                    optParRecAdv(s.GetRange(1, s.Count - 1), t, x, hist1a, activeSyncTrace);

                    /* Add the whole range */
                    //hist1a.AddRange(s);
                    //SaveTraceToDictionary(hist1a);
                    //return;
                }
                else if (s[0].Name.StartsWith("?"))
                {
                    /* If synchronisation on this trace is active */
                    if (IsSyncActive(hist1a, s[0]))
                    {
                        /* If this trace has not been seen previously in this trace sequence given in the history */
                        if (!HasBeenSeen(hist1a, s[0]))
                        {
                            /* A sync is active for this trace, and it has not yet been observed, add it and continue */
                            hist1a.Add(new Trace(s[0]));
                            optParRecAdv(s.GetRange(1, s.Count - 1), t, x, hist1a, activeSyncTrace);
                        }
                        else
                        {
                            /* This is a deadlocked sequence, we should stop. A "DL" trace is added to show this */
                            return;
                        }
                    }
                    else
                    /* We have a receive trace, only add it if it is NOT in the syncset */
                    if (!x.Any(p => p.Name == s[0].Name))
                    {
                        hist1a.Add(new Trace(s[0]));
                        optParRecAdv(s.GetRange(1, s.Count - 1), t, x, hist1a, activeSyncTrace);
                    }
                    else
                    {
                        /* This is a deadlocked sequence, we should stop. A "DL" trace is added to show this */
                        //hist1a.Add(new Csp.Trace(s[0]) { Name = "DL" });
                        //SaveTraceToDictionary(hist1a);
                        return;
                    }
                }
                else
                {
                    /* This is not a transmit/receive trace, it can be added */
                    /* Note: This routine makes eplicit use of the transmit and receive trace names */
                    hist1a.Add(new Trace(s[0]));
                    optParRecAdv(s.GetRange(1, s.Count - 1), t, x, hist1a, activeSyncTrace);
                }
            }
            else
            {
                /* Check if either s or t is in the sync set */
                if (x.Any(p => p.Name == s[0].Name))
                {
                    if ((s[0].Name[1] == t[0].Name[1]) && (((s[0].Name[0] == '?') && (t[0].Name[0] == '!')) || ((s[0].Name[0] == '!') && (t[0].Name[0] == '?')))) /* Sync: (?x-!x) or (?x-!x) */
                    {
                        List<Trace> hists = new List<Trace>(history);

                        /* Always log the transmitting event before the receiving event */
                        if (s[0].Name[0] == '!')
                        {
                            AddSyncActive(ref activeSyncTrace, s[0]);   /* Mark the trace as Synced */
                            hists.Add(new Trace(s[0]));
                            hists.Add(new Trace(t[0]));
                        }
                        else
                        {
                            AddSyncActive(ref activeSyncTrace, t[0]);   /* Mark the trace as Synced */
                            hists.Add(new Trace(t[0]));
                            hists.Add(new Trace(s[0]));
                        }

                        optParRecAdv(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, hists, activeSyncTrace); /* s and t move on */
                    }
                }
                else if (x.Any(p => p.Name == t[0].Name))
                {
                    if ((s[0].Name[1] == t[0].Name[1]) && (((s[0].Name[0] == '?') && (t[0].Name[0] == '!')) || ((s[0].Name[0] == '!') && (t[0].Name[0] == '?')))) /* Sync: (?x-!x) or (?x-!x) */
                    {
                        List<Trace> histt = new List<Trace>(history);
                        
                        /* Always log the transmitting event before the receiving event */
                        if (t[0].Name[0] == '!')
                        {
                            AddSyncActive(ref activeSyncTrace, t[0]);   /* Mark the trace as Synced */
                            histt.Add(new Trace(t[0]));   
                            histt.Add(new Trace(s[0])); 
                        }
                        else
                        {
                            AddSyncActive(ref activeSyncTrace, s[0]);   /* Mark the traces as Synced */
                            histt.Add(new Trace(s[0]));   
                            histt.Add(new Trace(t[0]));   
                        }

                        optParRecAdv(s.GetRange(1, s.Count - 1), t.GetRange(1, t.Count - 1), x, histt, activeSyncTrace); /* s and t move on */
                    }
                }

                /* Here we split, because we have 2 combinations */

                /* These calls should be limited to the step laws */
                /* If we have a transmit trace, add and move on */
                List<Trace> hist2 = new List<Trace>(history);
                if (t[0].Name.StartsWith("!"))
                {
                    hist2.Add(new Trace(t[0]));
                    optParRecAdv(s, t.GetRange(1, t.Count - 1), x, hist2, activeSyncTrace);
                }
                else if (t[0].Name.StartsWith("?"))
                {
                    /* If synchronisation on this trace is active */
                    if (IsSyncActive(hist2, t[0]))
                    {
                        /* If this trace has not been seen previously in this trace sequence given in the history */
                        if (!HasBeenSeen(hist2, t[0]))
                        {
                            /* A sync is active for this trace, and it has not yet been observed, add it and continue */
                            hist2.Add(new Trace(t[0]));
                            optParRecAdv(s, t.GetRange(1, t.Count - 1), x, hist2, activeSyncTrace);
                        }
                        else
                        {
                            /* This is a deadlocked sequence, we should stop. A "DL" trace is added to show this */
                            return;
                        }
                    }
                    else
                    /* We have a receive trace, only add it if it is NOT in the syncset */
                    if (!x.Any(p => p.Name == t[0].Name))
                    {
                        hist2.Add(new Trace(t[0]));
                        optParRecAdv(s, t.GetRange(1, t.Count - 1), x, hist2, activeSyncTrace);
                    }
                    else
                    {
                        /* This is a deadlocked sequence, we should stop. A "DL" trace is added to show this */
                        //hist2.Add(new Csp.Trace(t[0]) { Name = "DLL" });
                        //SaveTraceToDictionary(hist2);
                        //return;
                    }
                }
                else
                {
                    /* This is not a transmit/receive trace, it can be added */
                    /* Note: This routine makes eplicit use of the transmit and receive trace names */
                    hist2.Add(new Trace(t[0]));
                    optParRecAdv(s, t.GetRange(1, t.Count - 1), x, hist2, activeSyncTrace);
                }
                
                /* If we have a transmit trace, add and move on */
                List<Trace> hist1 = new List<Trace>(history);
                if (s[0].Name.StartsWith("!"))
                {
                    hist1.Add(new Trace(s[0]));
                    optParRecAdv(s.GetRange(1, s.Count - 1), t, x, hist1, activeSyncTrace);
                }
                else if (s[0].Name.StartsWith("?"))
                {
                    /* If synchronisation on this trace is active */
                    if (IsSyncActive(hist1, s[0]))
                    {
                        /* If this trace has not been seen previously in this trace sequence given in the history */
                        if (!HasBeenSeen(hist1, s[0]))
                        {
                            /* A sync is active for this trace, and it has not yet been observed, add it and continue */
                            hist1.Add(new Trace(s[0]));
                            optParRecAdv(s.GetRange(1, s.Count - 1), t, x, hist1, activeSyncTrace);
                        }
                        else
                        {
                            /* This is a deadlocked sequence, we should stop. A "DL" trace is added to show this */
                            return;
                        }
                    }
                    else
                    /* We have a receive trace, only add it if it is NOT in the syncset */
                    if (!x.Any(p => p.Name == s[0].Name))
                    {
                        hist1.Add(new Trace(s[0]));
                        optParRecAdv(s.GetRange(1, s.Count - 1), t, x, hist1, activeSyncTrace);
                    }
                    else
                    {
                        /* This is a deadlocked sequence, we should stop. A "DL" trace is added to show this */
                        //hist1.Add(new Csp.Trace(s[0]) { Name = "DLL" });
                        //SaveTraceToDictionary(hist1);
                        //return;
                    }
                }
                else
                {
                    /* This is not a transmit/receive trace, it can be added */
                    /* Note: This routine makes eplicit use of the transmit and receive trace names */
                    hist1.Add(new Trace(s[0]));
                    optParRecAdv(s.GetRange(1, s.Count - 1), t, x, hist1, activeSyncTrace);
                }
            }
        }

        /* This function adds a tx trace to the supplied SyncActive list */
        private void AddSyncActive(ref List<Csp.Trace> syncActiveList, Csp.Trace trace)
        {
            if (!syncActiveList.Any(p => p.Name == trace.Name))
            {
                syncActiveList.Add(new Csp.Trace(trace));
            }
        }

        /* This function checks if the Tx trace is active for the given rx trace */
        private bool IsSyncActive(List<Csp.Trace> syncActiveList, Csp.Trace trace)
        {
            /* Create the tx trace search name */
            String sTxTraceName = String.Format("!{0}", trace.Name.TrimStart(new char[] { '?' }));
            return syncActiveList.Any(p => p.Name == sTxTraceName);
        }

        /* This function checks that the specified trace has not been observed since its tx trace was observed */
        /* NOTE: This function assumes that a txTrace is present */
        private bool HasBeenSeen(List<Csp.Trace> historyList, Csp.Trace trace)
        {
            /* Create the tx trace search name */
            String sTxTraceName = String.Format("!{0}", trace.Name.TrimStart(new char[] { '?' }));

            /* Backtrack the history */
            if (historyList.Count > 0)
            {
                for (int i = historyList.Count - 1; i >= 0; i--)
                {
                    if ((historyList[i].Name == trace.Name) && (historyList[i].ProcessId == trace.ProcessId))
                    {
                        /* The trace has been seen, return true */
                        return true;
                    }
                    else if (historyList[i].Name == sTxTraceName)
                    {
                        /* The tx trace has been seen. If we get here, it means that the input trace has not been seen. */
                        return false;
                    }
                }
            }

            return false;
        }


        private void SaveTraceToDictionary(List<Trace> history)
        {
            try
            {
                String key = GetKey(history);
                if (!setDictionaryAdvanced.ContainsKey(key))
                {
                    setDictionaryAdvanced.Add(key, history);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("HASH FAIL: {0}", ex.Message));
            }
        }

        private void SaveTraceToDictionary(List<string> history)
        {
            try
            {
                String key = GetKey(history);
                if (!setDictionary.ContainsKey(key))
                {
                    setDictionary.Add(key, history);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("HASH FAIL: {0}", ex.Message));
            }
        }

        private static string GetKey(List<string> history)
        {
            return String.Join("", history);
        }

        private string GetKey(List<Trace> history)
        {
            return String.Join("", history);
        }
    }
}
