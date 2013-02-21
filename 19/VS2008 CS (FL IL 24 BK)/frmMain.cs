using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HFI_Demo;
using PhoenixContact.HFI;

namespace VS2005_CS__ETH_BK_DI8_DO4_
{
   public partial class frmMain : Form
   {
      // Variable for the instance from the application class
      HFI_Appl myApplication;

      
      public frmMain()
      {
         InitializeComponent();
      }

      /// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
            myApplication.Dispose();

				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

      private void frmMain_Load(object sender, System.EventArgs e)
      {
         // Create the instance from the application class
         myApplication = new HFI_Appl();
         
         // Initialize the controller control
         // Add a controller to the control
         _ctrlController1.AddObject(myApplication.Controller);    
         
         // Calling from the controller event for the first initialization
         _ctrlController1_OnSelectController(this, myApplication.Controller);
         
         // Assign update timer to the controls 
         _ctrlController1.UpdateData         = tmrUpdate;     
         _ctrlVarInput1.UpdateData           = tmrUpdate;      
         _ctrlVarOutput1.UpdateData          = tmrUpdate;
         _ctrlMessageClient1.UpdateData      = tmrUpdate;
         _ctrlIBS_Diag1.UpdateData           = tmrUpdate;
       
         // Fill the controlls with actual objects
         _ctrlController1.OnSelectController +=new PhoenixContact.HFI.Visualization.SelectControllerHandler(_ctrlController1_OnSelectController);

          _ctrlVarOutput1.EditActivate = true;
      }

      private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
      {
         if (myApplication != null)
         {
            myApplication.Dispose();
         }
      }

      private void _ctrlController1_OnSelectController(object Sender, Object Controller)
      {
         // Add Inputs 
         _ctrlVarInput1.ClearObject();

         foreach(VarInput i in myApplication.Controller.InputObjectList)
         {
            _ctrlVarInput1.AddObject(i);              
         }

         // Add Outputs 
         _ctrlVarOutput1.ClearObject();

         foreach(VarOutput i in myApplication.Controller.OutputObjectList)
         {
            _ctrlVarOutput1.AddObject(i);              
         }

         // Add Message Clients
         _ctrlMessageClient1.ClearObject();

         foreach(MessageClient i in myApplication.Controller.MessageObjectList)
         {
            _ctrlMessageClient1.AddObject(i);              
         }

         // Assign the actual controller to the ctrlIBS_Diag control 
         _ctrlIBS_Diag1.ControlerConnection = Controller as IInterbusG4;;
      }

   }
}
