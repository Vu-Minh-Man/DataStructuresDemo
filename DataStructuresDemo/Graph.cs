using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresDemo
{
    public class Graph
    {
        private IDictionary<string, Vertex?> _vertices;

        public Graph()
        {
            _vertices = new Dictionary<string, Vertex?>();
        }

        public void AddVertex(string label)
        {
            if (HasVertex(label))
                return;

            _vertices.Add(label, new Vertex(label));
        }

        public void RemoveVertex(string label)
        {
            if (!HasVertex(label))
                return;

            foreach (var vertex in _vertices.Values.Where(x => x.Label != label))
                vertex.RemoveEdge(_vertices[label]);

            _vertices.Remove(label);
        }

        public void AddEdge(string startLabel, string endLabel)
        {
            if (!IsValidEdge(startLabel, endLabel))
                throw new ArgumentNullException();

            AddEdges(_vertices[startLabel], _vertices[endLabel]);
        }

        public void RemoveEdge(string startLabel, string endLabel)
        {
            if (!IsValidEdge(startLabel, endLabel))
                return;

            RemoveEdge(_vertices[startLabel], _vertices[endLabel]);
        }

        private void AddEdges(Vertex startVertex, Vertex endVertex)
        {
            if (!startVertex.HasEdge(endVertex))
                startVertex.AddEdge(endVertex);
        }

        private void RemoveEdge(Vertex startVertex, Vertex endVertex)
        {
            if (startVertex.HasEdge(endVertex))
                startVertex.RemoveEdge(endVertex);
        }

        private bool IsValidVertex(string label)
        {
            return !string.IsNullOrWhiteSpace(label);
        }

        private bool HasVertex(string label)
        {
            return _vertices.ContainsKey(label);
        }

        private bool IsValidEdge(string startLabel, string endLabel)
        {
            return IsValidVertex(startLabel) &&
                IsValidVertex(endLabel) &&
                startLabel != endLabel &&
                HasVertex(startLabel) &&
                HasVertex(endLabel);
        }

        private class Vertex
        {
            internal string Label { get; set; }
            internal LinkedList<Vertex?> Edges { get; set; }

            internal Vertex(string label)
            {
                if (string.IsNullOrWhiteSpace(label))
                    throw new ArgumentNullException("label must be a string other than null or whitespaces-only.");

                Label = label;
                Edges = new LinkedList<Vertex?>();
            }

            internal void AddEdge(Vertex vertex)
            {
                Edges.AddLast(vertex);
            }

            internal void RemoveEdge(Vertex vertex)
            {
                Edges.Remove(vertex);
            }

            internal bool HasEdge(Vertex vertex)
            {
                return Edges.Contains(vertex);
            }

        }
    }
}
