using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkEvents
{
	public class Counter
	{
		private int _count;

		public Action Increment { get; set; }

		public Counter()
		{
			_count = 0;
			Increment = () =>
			{
				_count++;
				Console.WriteLine($"Счетчик: {_count}");
			};
		}
	}
}
