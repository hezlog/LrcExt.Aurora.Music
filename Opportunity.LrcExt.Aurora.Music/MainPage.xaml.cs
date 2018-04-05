﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Opportunity.LrcExt.Aurora.Music
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Dispatcher.Begin(async () =>
            {
                var r = await Launcher.QueryUriSupportAsync(AuroraSettings, LaunchQuerySupportType.Uri);
                this.canLaunch = (r == LaunchQuerySupportStatus.Available);
                this.Bindings.Update();
            });
        }

        private bool canLaunch = false;

        private static Uri AuroraSettings = new Uri("as-music:///settings", UriKind.Absolute);

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var r = await Launcher.QueryUriSupportAsync(AuroraSettings, LaunchQuerySupportType.Uri);
            var lr = false;
            if (r == LaunchQuerySupportStatus.Available)
                lr = await Launcher.LaunchUriAsync(AuroraSettings);
            else
                lr = await Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?ProductId=9nblggh6jvdt"));

            if (lr)
                Application.Current.Exit();
        }
    }
}