using Tethys.Logging;
using Tethys.Platform.Windows;

Logger.Start();

Logger.Info("This is some nice info");

using var window = Window.Create(1024, 768, "A title");
while (window.Update())
{
    // Do the stuff
}

Logger.Shutdown();