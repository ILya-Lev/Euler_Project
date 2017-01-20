using MoreLinq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EulerProject
{
	public class Vertex
	{
		public int Id { get; set; }
		public List<Edge> Edges { get; } = new List<Edge>();
	}

	public class Edge
	{
		public int Length { get; set; }
		public Vertex Start { get; set; }
		public Vertex Finish { get; set; }
	}

	public class Problem107
	{
		private readonly Dictionary<int, Vertex> _allVertices = new Dictionary<int, Vertex>();
		private readonly List<Edge> _allEdges = new List<Edge>();

		public int WeightDifference()
		{
			var total = TotalWeight();
			var mst = MstWeight();

			return total - mst;
		}
		private void LoadFromFile(string fileName = @"resources\p107_network.txt")
		{
			//data is already loaded - no need to do it again
			if (_allVertices.Count != 0) return;

			File.ReadLines(fileName)
				.Select((line, idx) =>
				{
					if (!_allVertices.ContainsKey(idx))
						_allVertices.Add(idx, new Vertex { Id = idx });
					var currentVertex = _allVertices[idx];

					var edges = line.Split(new[] { ',' }).Select(part =>
					{
						int result;
						if (!int.TryParse(part, out result))
							result = 0;
						return result;
					})
						.Select((length, id) =>
						{
							if (!_allVertices.ContainsKey(id))
								_allVertices.Add(id, new Vertex { Id = id });
							var vertex = _allVertices[id];
							return new Edge { Length = length, Start = currentVertex, Finish = vertex };
						}).Where(e => e.Length != 0)
						.ToList();

					currentVertex.Edges.AddRange(edges);
					_allEdges.AddRange(edges);

					return currentVertex;
				}).ToList();
		}

		private int TotalWeight()
		{
			LoadFromFile();

			var doubleSum = _allEdges.Sum(edge => edge.Length);
			return doubleSum / 2;
		}

		private int MstWeight()
		{
			LoadFromFile();

			var mst = new MinimalSpanningTree();
			mst.AddVertex(_allVertices[0], null);

			while (mst.Count < _allVertices.Count)
			{
				var edge = mst.MinExternalEdge();
				var vertex = !mst.AlreadySeenVertex(edge.Finish) ? edge.Finish : edge.Start;
				mst.AddVertex(vertex, edge);
			}

			var minimalSpanningTreeEdges = mst.MinimalSpanningTreeEdges();
			return minimalSpanningTreeEdges.Sum(e => e.Length);
		}

		private class MinimalSpanningTree
		{
			private readonly Dictionary<Vertex, Edge> _seenVertices = new Dictionary<Vertex, Edge>();
			private readonly HashSet<Edge> _outgoingEdges = new HashSet<Edge>();

			public int Count => _seenVertices.Count;

			public void AddVertex(Vertex vertex, Edge edge)
			{
				_seenVertices.Add(vertex, edge);
				vertex.Edges.ForEach(e => _outgoingEdges.Add(e));

				_outgoingEdges.RemoveWhere(e => _seenVertices.ContainsKey(e.Finish)
											 && _seenVertices.ContainsKey(e.Start));
			}

			public Edge MinExternalEdge()
				=> _outgoingEdges?.Count > 0 ? _outgoingEdges.MinBy(e => e.Length) : null;

			public bool AlreadySeenVertex(Vertex vertex) => _seenVertices.ContainsKey(vertex);

			public List<Edge> MinimalSpanningTreeEdges()
				=> _seenVertices.Select(pair => pair.Value).Where(e => e != null).ToList();
		}
	}
}
