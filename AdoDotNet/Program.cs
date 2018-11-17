using System;

using AdoTask = AdoDotNet.Task.Task;

namespace AdoDotNet
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var task = new AdoTask();
			if (task.ConnectToDatabase())
			{
				Console.WriteLine("Successfully connected!");
				if (task.DisconnectFromDatabase())
				{
					Console.WriteLine("Successfully disconnected!");
				}
			}
		}
	}
}
