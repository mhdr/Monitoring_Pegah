using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VS2005_CS__ETH_BK_DI8_DO4_
{
   static class Program
   {
      /// <summary>
      /// Der Haupteinstiegspunkt für die Anwendung.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new frmMain());
      }
   }
}
