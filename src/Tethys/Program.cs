using System;
using System.Threading.Tasks;
using Tethys.Logging;

Logger.Start();


Logger.Info("This is some nice info");


await Task.Delay(4000);


Logger.Shutdown();