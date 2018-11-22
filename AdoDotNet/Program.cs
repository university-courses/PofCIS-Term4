using AdoTask = AdoDotNet.Task.Task;

namespace AdoDotNet
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			new AdoTask().ExecuteTask();
		}
	}
}
