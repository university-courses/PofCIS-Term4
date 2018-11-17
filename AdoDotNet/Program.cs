using System;

using AdoTask = AdoDotNet.Task.Task;

namespace AdoDotNet
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var task = new AdoTask();
			if (task.DbConnect())
			{
				Console.WriteLine("Successfully connected!");
				if (task.DbDisconnect())
				{
					Console.WriteLine("Successfully disconnected!");
				}
			}
		}
	}
}
