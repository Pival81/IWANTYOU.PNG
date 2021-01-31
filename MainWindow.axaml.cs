using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace Avalonia.NETCoreApp2
{
	public class MainWindow : Window
	{
		public static List<string> Strings = new List<string>()
		{
			"Hey, tu!",
			"Si, tu!",
			"Sei un simp!"
		};
		int Counter = 0;
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
			SetText();
		}

		public void SetText()
		{
			this.FindControl<TextBlock>("text").Text = Strings[Counter];
		}

		public void Next()
		{
			if (Counter == Strings.Count - 1)
			{
				Console.WriteLine("uscita");
				Hide();
				return;
			}
			Counter++;
			SetText();
		}
		private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
		{
			Next();
		}

		private void WindowBase_OnDeactivated(object? sender, EventArgs e)
		{
			Topmost = true;
			Activate();
		}
	}
}