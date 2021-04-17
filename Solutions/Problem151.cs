using System.Collections.Generic;
using System.Linq;

namespace EulerProject
{
	class Problem151
	{
		class Sheet
		{
			private static readonly Dictionary<Value, List<Value>> _generators
				= new Dictionary<Value, List<Value>>
				{
					[Value.A2] = new List<Value> { Value.A3, Value.A4, Value.A5 },
					[Value.A3] = new List<Value> { Value.A4, Value.A5 },
					[Value.A4] = new List<Value> { Value.A5 },
					[Value.A5] = new List<Value>(),
				};

			public enum Value { A2, A3, A4, A5 };

			public Value SheetValue { get; }

			public Sheet(Value sheetValue)
			{
				SheetValue = sheetValue;
			}

			public List<Sheet> Produce()
				=> _generators[SheetValue].Select(val => new Sheet(val)).ToList();
		}

		class State
		{
			public decimal Probability { get; set; }
			public List<Sheet> Sheets { get; set; }

			public State ChangeStateViaSheet(int sheetIdx)
			{
				var state = new State
				{
					Probability = 1.0m / Sheets.Count * Probability,
					Sheets = new List<Sheet>(Sheets.Where((s, i) => i != sheetIdx))
				};
				state.Sheets.AddRange(Sheets[sheetIdx].Produce());

				return state;
			}
		}

		public decimal SeenOneSheet()
		{
			var initialState = new State
			{
				Probability = 1m,
				Sheets = new List<Sheet>
				{
					new Sheet(Sheet.Value.A2),
					new Sheet(Sheet.Value.A3),
					new Sheet(Sheet.Value.A4),
					new Sheet(Sheet.Value.A5),
				}
			};

			var times = ProcessStates(initialState);
			return times;
		}

		private decimal ProcessStates(State initialState)
		{
			decimal overalProbability = 0m;

			var path = new Stack<State>();
			path.Push(initialState);
			while (path.Count > 0)
			{
				var state = path.Pop();

				if (state.Sheets.Count == 1)
				{
					if (state.Sheets[0].SheetValue == Sheet.Value.A5)
						continue;
					overalProbability += state.Probability;
				}

				for (int i = 0; i < state.Sheets.Count; i++)
				{
					path.Push(state.ChangeStateViaSheet(i));
				}
			}

			return overalProbability;
		}
	}
}
