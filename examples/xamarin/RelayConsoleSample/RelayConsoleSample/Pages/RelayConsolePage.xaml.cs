﻿/*
 * Copyright 2019, Digi International Inc.
 * 
 * Permission to use, copy, modify, and/or distribute this software for any
 * purpose with or without fee is hereby granted, provided that the above
 * copyright notice and this permission notice appear in all copies.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
 * WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
 * ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
 * WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
 * ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
 * OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
 */

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RelayConsoleSample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RelayConsolePage : CustomContentPage
	{
		private RelayConsolePageViewModel relayConsolePageViewModel;

		public RelayConsolePage(BleDevice device)
		{
			InitializeComponent();
			relayConsolePageViewModel = new RelayConsolePageViewModel(device);
			BindingContext = relayConsolePageViewModel;

			// Register the back button action.
			if (EnableBackButtonOverride)
			{
				CustomBackButtonAction = async () =>
				{
					// Ask the user if wants to close the connection.
					if (await DisplayAlert("Disconnect device", "Do you want to disconnect the XBee device?", "Yes", "No"))
						relayConsolePageViewModel.DisconnectDevice();
				};
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			relayConsolePageViewModel.RegisterEventHandler();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			relayConsolePageViewModel.UnregisterEventHandler();
		}

		public void SendButtonClicked(object sender, System.EventArgs e)
		{
			relayConsolePageViewModel.SendRelayMessage();
		}

		public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;
		}
	}
}