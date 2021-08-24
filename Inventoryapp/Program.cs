using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sentry;

namespace Inventoryapp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Init the Sentry SDK
            SentrySdk.Init(o =>
            {
                // Tells which project in Sentry to send events to:
                o.Dsn = "https://392507d35f8d4095a963085d11561675@o971379.ingest.sentry.io/5923705";
                // When configuring for the first time, to see what the SDK is doing:
                o.Debug = true;
                // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
                // We recommend adjusting this value in production.
                o.TracesSampleRate = 1.0;
            });
            // Configure WinForms to throw exceptions so Sentry can capture them.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            // Any other configuration you might have goes here...
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new splashscreen());
            SentrySdk.CaptureMessage("hello world");
        }
    }
}
