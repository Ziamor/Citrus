#region Using Statements
using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
#endregion

namespace Project_Citrus
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            using (var game = new Citrus())
                game.Run();
        }
    }
#endif
}
