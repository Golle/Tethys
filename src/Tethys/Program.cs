using System.Diagnostics;
using System.Numerics;
using Tethys.Graphics;
using Tethys.Logging;
using Tethys.Platform.Windows;

var start = new Vector2(0, 0);
var stop = new Vector2(1023, 767);


var startIncrease = true;
var stopIncrease = false;

var height = 768;
var width = 1024;

Logger.Start();
{
    using var window = Window.Create(width, height, "A title");
    using var deviceContext = DeviceContext.Create(window);
    using var renderContext = new RenderContext(window.Width, window.Height);
    var timer = Stopwatch.StartNew();
    var frames = 0;
    while (window.Update())
    {

        if (startIncrease)
        {
            start += Vector2.UnitY * 0.1f;
            if (start.Y >= height -1)
            {
                start.Y = height-1;
                startIncrease = false;
            }
        }
        else
        {
            start -= Vector2.UnitY * 0.1f;
            if (start.Y <= 0)
            {
                start.Y = 0;
                startIncrease = true;
            }
        }

        if (stopIncrease)
        {
            stop += Vector2.UnitY*0.3f;
            if (stop.Y >= height - 1)
            {
                stop.Y = height - 1;
                stopIncrease = false;
            }
        }
        else
        {
            stop -= Vector2.UnitY * 0.3f;
            if (stop.Y <= 0)
            {
                stop.Y = 0;
                stopIncrease = true;
            }
        }

        // Do the stuff
        renderContext.Clear();
        renderContext.DrawLine(start, stop);
        renderContext.Render(deviceContext);
    
        if (timer.Elapsed.TotalSeconds >= 1.0)
        {
            Logger.Info($"FPS {frames / timer.Elapsed.TotalSeconds}");
            timer.Restart();
            frames = 0;
        }
        else
        {
            frames++;
        }
    }
}

Logger.Shutdown();
