using System;
using System.Collections.Generic;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using Tcp.NET.Server;
using Tcp.NET.Server.Models;

namespace Avalonia.NETCoreApp2
{
	class Program
	{
		// Initialization code. Don't use any Avalonia, third-party APIs or any
		// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
		// yet and stuff might break.
		private static void AppMain(Application app, string[] args)
		{   
			// A cancellation token source that will be used to stop the main loop
			var cts = new CancellationTokenSource();
			MainWindow window = null;
			var server = new TcpNETServer(new ParamsTcpServer()
			{
				Port = 8181,
				EndOfLineCharacters = "\r\n"
			});
			server.ConnectionEvent += async (o, e) => Console.WriteLine("evviva");
			server.MessageEvent += async (o, e) =>
			{
				try
				{
					Console.WriteLine(e.Message);
					var json = JObject.Parse(e.Message);
					if (json["msg"].ToString() == "start")
					{
						if (json.ContainsKey("strings"))
						{
							var array = json["strings"] as JArray;
							Console.WriteLine(array);
							MainWindow.Strings = array.ToObject<List<string>>();
						}
						else
						{
							MainWindow.Strings = new List<string>()
							{
								"Hey, tu!",
								"Si, tu!",
								"Sei un simp!"
							};
						}
						Console.WriteLine(json.Value<string>("msg"));
						await Dispatcher.UIThread.InvokeAsync(() =>
						{
							window?.Hide();
							window = new MainWindow();
							window.Show();
						});
					} else if (json["msg"].ToString() == "stop")
					{
						Console.WriteLine(json.Value<string>("msg"));
						await Dispatcher.UIThread.InvokeAsync(() =>
						{
							window?.Hide();
						});
					} else if (json["msg"].ToString() == "next")
					{
						Console.WriteLine(json.Value<string>("msg"));
						await Dispatcher.UIThread.InvokeAsync(() =>
						{
							window?.Next();
						});
					}
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
					throw;
				}
			};
			server.StartAsync().Wait();
			
			// Start the main loop
			app.Run(cts.Token);
		}
		
		public static void Main(string[] args)
		{
			BuildAvaloniaApp()
				.Start(AppMain, args);
		} 

		// Avalonia configuration, don't remove; also used by visual designer.
		public static AppBuilder BuildAvaloniaApp()
			=> AppBuilder.Configure<App>()
				.UsePlatformDetect()
				.LogToTrace();
	}
}
