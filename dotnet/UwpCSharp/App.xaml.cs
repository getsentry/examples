using Sentry.Protocol;
using System;
using System.Runtime.ExceptionServices;
using System.Security;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using UnhandledExceptionEventArgs = Windows.UI.Xaml.UnhandledExceptionEventArgs;

namespace Sentry.Samples.Uwp
{
    sealed partial class App : Application
    {
        public App()
        {
            SentrySdk.Init(options =>
            {
                options.Dsn = "https://eb18e953812b41c3aeb042e666fd3b5c@o447951.ingest.sentry.io/5428537";
                options.CacheDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            });
            //Event required for registering unhandled exceptions from UWP.
            Current.UnhandledException += ExceptionHandler;
            Current.EnteredBackground += OnSleep;
            Current.LeavingBackground += OnResume;

            InitializeComponent();
            Suspending += OnSuspending;
        }


        [HandleProcessCorruptedStateExceptions, SecurityCritical]
        internal void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            //We need to backup the reference, because the Exception reference last for one access.
            //After that, a new  Exception reference is going to be set into e.Exception.
            var exception = e.Exception;
            if (exception != null)
            {
                exception.Data[Mechanism.HandledKey] = false;
                exception.Data[Mechanism.MechanismKey] = "Application.UnhandledException";
                SentrySdk.CaptureException(exception);
                //If you are not going to use the Cache functionality, it's recommended to flush 
                //Sentry for forcing it to send the Exception before the app closes.
                //  SentrySdk.FlushAsync(TimeSpan.FromSeconds(10)).Wait();
            }
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame is null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;

            }

            if (!e.PrelaunchActivated)
            {
                if (rootFrame.Content is null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        private void OnResume(object sender, LeavingBackgroundEventArgs e)
            => SentrySdk.AddBreadcrumb("OnResume", "app.lifecycle", "event");

        private void OnSleep(object sender, EnteredBackgroundEventArgs e)
            => SentrySdk.AddBreadcrumb("OnSleep", "app.lifecycle", "event");

    }
}
