using System;
using System.Text;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using Tcp.NET.Server;
using Tcp.NET.Server.Models;

namespace Avalonia.NETCoreApp2
{
	public class App : Application
	{
		public MainWindow window;
		public override void Initialize()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public override void OnFrameworkInitializationCompleted()
		{
			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				desktop.MainWindow = new MainWindow();
				window = desktop.MainWindow as MainWindow;
			}
			base.OnFrameworkInitializationCompleted();
		}
	}
}