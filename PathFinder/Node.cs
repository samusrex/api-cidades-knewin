using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api
{
    public class Node
    {
        public Cidades Cidade { get; }

        public Node(Cidades cidade)
        {
            Cidade = cidade;
        }

        readonly List<Edge> _edges = new List<Edge>();
        public IEnumerable<Edge> Edges => _edges;

        public IEnumerable<NeighborhoodInfo> Neighbors =>
            from edge in Edges
            select new NeighborhoodInfo(
                edge.Node1 == this ? edge.Node2 : edge.Node1,
                edge.Value
                );

        private void Assign(Edge edge)
        {
            _edges.Add(edge);
        }

        public void ConnectTo(Node other, int connectionValue)
        {
            Edge.Create(connectionValue, this, other);
        }

        public struct NeighborhoodInfo
        {
            public Node Node { get; }
            public int WeightToNode { get; }

            public NeighborhoodInfo(Node node, int weightToNode)
            {
                Node = node;
                WeightToNode = weightToNode;
            }
        }

        public class Edge
        {
            public int Value { get; }
            public Node Node1 { get; }
            public Node Node2 { get; }

            public Edge(int value, Node node1, Node node2)
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Edge value needs to be positive.");
                }
                Value = value;
                Node1 = node1;
                node1.Assign(this);
                Node2 = node2;
                node2.Assign(this);
            }

            public static Edge Create(int value, Node node1, Node node2)
            {
                return new Edge(value, node1, node2);
            }
        }
    }

}
