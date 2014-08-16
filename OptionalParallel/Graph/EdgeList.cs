using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpTrace.Graph
{
    public class EdgeList
    {
        private List<Tuple<String, String>> edges { get; set; }
        private List<Tuple<String, int>> vertices { get; set; }
        
        public EdgeList()
        {
            edges = new List<Tuple<string, string>>();
            vertices = new List<Tuple<string,int>>();
        }

        public List<Tuple<String, String>> GetEdges()
        {
            return edges;
        }

        public List<Tuple<String, int>> GetVertices()
        {
            return vertices;
        }

        public void AddEdge(string p1, string p2, int lineId)
        {
            /* Add the edge */
            edges.Add(new Tuple<String, String>(p1, p2));
            
            /* Add the vertice */
            if (!vertices.Any(p => (p.Item1 == p1) && (p.Item2 == lineId)))
            {
                vertices.Add(new Tuple<String, int>(p1, lineId));
            }
            if (!vertices.Any(p => (p.Item1 == p2) && (p.Item2 == lineId)))
            {
                vertices.Add(new Tuple<String, int>(p2, lineId));
            }
        }
    }
}
