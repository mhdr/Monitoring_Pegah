using System;
using System.Collections.Generic;
using System.Linq;
using PhoenixContact.HFI;

namespace SharedLibraryMonitoringDevice
{
    public sealed class HFI_Appl19 : IDisposable
    {
        public List<InOut> InOutList = null;

        // Information: 
        // If you are using this programm you have to disable the
        // PnP Mode and enable the expert mode of the FL IL 24 BK

        #region *** Variable Declaration **************************************************

        // Create the controller
        public Controller_IBS_ETH Controller;

        #region *** Create input variables

        public VarInput MODULE_1_IN = new VarInput(0, PD_Length.Word, 16, 0);
        public VarInput MODULE_2_IN = new VarInput(2, PD_Length.DWord, 32, 0);
        public VarInput MODULE_3_IN = new VarInput(6, PD_Length.DWord, 32, 0);
        public VarInput MODULE_4_IN = new VarInput(10, PD_Length.DWord, 32, 0);
        public VarInput MODULE_5_IN = new VarInput(14, PD_Length.DWord, 32, 0);
        public VarInput MODULE_6_IN = new VarInput(18, PD_Length.DWord, 32, 0);
        public VarInput MODULE_7_IN = new VarInput(22, PD_Length.DWord, 32, 0);
        public VarInput MODULE_8_IN = new VarInput(26, PD_Length.DWord, 32, 0);
        public VarInput MODULE_9_IN = new VarInput(30, PD_Length.DWord, 32, 0);
        public VarInput MODULE_10_IN = new VarInput(34, PD_Length.DWord, 32, 0);
        public VarInput MODULE_11_IN = new VarInput(38, PD_Length.DWord, 32, 0);
        public VarInput MODULE_12_IN = new VarInput(42, PD_Length.DWord, 32, 0);
        public VarInput MODULE_13_IN = new VarInput(46, PD_Length.DWord, 32, 0);
        public VarInput MODULE_14_IN = new VarInput(50, PD_Length.DWord, 32, 0);
        public VarInput MODULE_15_IN = new VarInput(54, PD_Length.DWord, 32, 0);
        public VarInput MODULE_16_IN = new VarInput(58, PD_Length.DWord, 32, 0);
        public VarInput MODULE_17_IN = new VarInput(62, PD_Length.DWord, 32, 0);
        public VarInput MODULE_18_IN = new VarInput(66, PD_Length.DWord, 32, 0);
        public VarInput MODULE_19_IN = new VarInput(70, PD_Length.DWord, 32, 0);

        public VarInput Module_2_1_In = new VarInput(2, PD_Length.Word, 16, 0);
        public VarInput Module_2_2_In = new VarInput(4, PD_Length.Word, 16, 0);
        public VarInput Module_3_1_In = new VarInput(6, PD_Length.Word, 16, 0);
        public VarInput Module_3_2_In = new VarInput(8, PD_Length.Word, 16, 0);
        public VarInput Module_4_1_In = new VarInput(10, PD_Length.Word, 16, 0);
        public VarInput Module_4_2_In = new VarInput(12, PD_Length.Word, 16, 0);
        public VarInput Module_5_1_In = new VarInput(14, PD_Length.Word, 16, 0);
        public VarInput Module_5_2_In = new VarInput(16, PD_Length.Word, 16, 0);
        public VarInput Module_6_1_In = new VarInput(18, PD_Length.Word, 16, 0);
        public VarInput Module_6_2_In = new VarInput(20, PD_Length.Word, 16, 0);
        public VarInput Module_7_1_In = new VarInput(22, PD_Length.Word, 16, 0);
        public VarInput Module_7_2_In = new VarInput(24, PD_Length.Word, 16, 0);
        public VarInput Module_8_1_In = new VarInput(26, PD_Length.Word, 16, 0);
        public VarInput Module_8_2_In = new VarInput(28, PD_Length.Word, 16, 0);
        public VarInput Module_9_1_In = new VarInput(30, PD_Length.Word, 16, 0);
        public VarInput Module_9_2_In = new VarInput(32, PD_Length.Word, 16, 0);
        public VarInput Module_10_1_In = new VarInput(34, PD_Length.Word, 16, 0);
        public VarInput Module_10_2_In = new VarInput(36, PD_Length.Word, 16, 0);
        public VarInput Module_11_1_In = new VarInput(38, PD_Length.Word, 16, 0);
        public VarInput Module_11_2_In = new VarInput(40, PD_Length.Word, 16, 0);
        public VarInput Module_12_1_In = new VarInput(42, PD_Length.Word, 16, 0);
        public VarInput Module_12_2_In = new VarInput(44, PD_Length.Word, 16, 0);
        public VarInput Module_13_1_In = new VarInput(46, PD_Length.Word, 16, 0);
        public VarInput Module_13_2_In = new VarInput(48, PD_Length.Word, 16, 0);
        public VarInput Module_14_1_In = new VarInput(50, PD_Length.Word, 16, 0);
        public VarInput Module_14_2_In = new VarInput(52, PD_Length.Word, 16, 0);
        public VarInput Module_15_1_In = new VarInput(54, PD_Length.Word, 16, 0);
        public VarInput Module_15_2_In = new VarInput(56, PD_Length.Word, 16, 0);
        public VarInput Module_16_1_In = new VarInput(58, PD_Length.Word, 16, 0);
        public VarInput Module_16_2_In = new VarInput(60, PD_Length.Word, 16, 0);
        public VarInput Module_17_1_In = new VarInput(62, PD_Length.Word, 16, 0);
        public VarInput Module_17_2_In = new VarInput(64, PD_Length.Word, 16, 0);
        public VarInput Module_18_1_In = new VarInput(66, PD_Length.Word, 16, 0);
        public VarInput Module_18_2_In = new VarInput(68, PD_Length.Word, 16, 0);
        public VarInput Module_19_1_In = new VarInput(70, PD_Length.Word, 16, 0);
        public VarInput Module_19_2_In = new VarInput(72, PD_Length.Word, 16, 0);

        #endregion

        #region *** Create output variables

        public VarOutput MODULE_2_OUT = new VarOutput(0, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_3_OUT = new VarOutput(4, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_4_OUT = new VarOutput(8, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_5_OUT = new VarOutput(12, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_6_OUT = new VarOutput(16, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_7_OUT = new VarOutput(20, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_8_OUT = new VarOutput(24, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_9_OUT = new VarOutput(28, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_10_OUT = new VarOutput(32, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_11_OUT = new VarOutput(36, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_12_OUT = new VarOutput(40, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_13_OUT = new VarOutput(44, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_14_OUT = new VarOutput(48, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_15_OUT = new VarOutput(52, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_16_OUT = new VarOutput(56, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_17_OUT = new VarOutput(60, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_18_OUT = new VarOutput(64, PD_Length.DWord, 32, 0);
        public VarOutput MODULE_19_OUT = new VarOutput(68, PD_Length.DWord, 32, 0);

        public VarOutput Module_2_1_Out = new VarOutput(2, PD_Length.Word, 16, 0);
        public VarOutput Module_2_2_Out = new VarOutput(4, PD_Length.Word, 16, 0);
        public VarOutput Module_3_1_Out = new VarOutput(6, PD_Length.Word, 16, 0);
        public VarOutput Module_3_2_Out = new VarOutput(8, PD_Length.Word, 16, 0);
        public VarOutput Module_4_1_Out = new VarOutput(10, PD_Length.Word, 16, 0);
        public VarOutput Module_4_2_Out = new VarOutput(12, PD_Length.Word, 16, 0);
        public VarOutput Module_5_1_Out = new VarOutput(14, PD_Length.Word, 16, 0);
        public VarOutput Module_5_2_Out = new VarOutput(16, PD_Length.Word, 16, 0);
        public VarOutput Module_6_1_Out = new VarOutput(18, PD_Length.Word, 16, 0);
        public VarOutput Module_6_2_Out = new VarOutput(20, PD_Length.Word, 16, 0);
        public VarOutput Module_7_1_Out = new VarOutput(22, PD_Length.Word, 16, 0);
        public VarOutput Module_7_2_Out = new VarOutput(24, PD_Length.Word, 16, 0);
        public VarOutput Module_8_1_Out = new VarOutput(26, PD_Length.Word, 16, 0);
        public VarOutput Module_8_2_Out = new VarOutput(28, PD_Length.Word, 16, 0);
        public VarOutput Module_9_1_Out = new VarOutput(30, PD_Length.Word, 16, 0);
        public VarOutput Module_9_2_Out = new VarOutput(32, PD_Length.Word, 16, 0);
        public VarOutput Module_10_1_Out = new VarOutput(34, PD_Length.Word, 16, 0);
        public VarOutput Module_10_2_Out = new VarOutput(36, PD_Length.Word, 16, 0);
        public VarOutput Module_11_1_Out = new VarOutput(38, PD_Length.Word, 16, 0);
        public VarOutput Module_11_2_Out = new VarOutput(40, PD_Length.Word, 16, 0);
        public VarOutput Module_12_1_Out = new VarOutput(42, PD_Length.Word, 16, 0);
        public VarOutput Module_12_2_Out = new VarOutput(44, PD_Length.Word, 16, 0);
        public VarOutput Module_13_1_Out = new VarOutput(46, PD_Length.Word, 16, 0);
        public VarOutput Module_13_2_Out = new VarOutput(48, PD_Length.Word, 16, 0);
        public VarOutput Module_14_1_Out = new VarOutput(50, PD_Length.Word, 16, 0);
        public VarOutput Module_14_2_Out = new VarOutput(52, PD_Length.Word, 16, 0);
        public VarOutput Module_15_1_Out = new VarOutput(54, PD_Length.Word, 16, 0);
        public VarOutput Module_15_2_Out = new VarOutput(56, PD_Length.Word, 16, 0);
        public VarOutput Module_16_1_Out = new VarOutput(58, PD_Length.Word, 16, 0);
        public VarOutput Module_16_2_Out = new VarOutput(60, PD_Length.Word, 16, 0);
        public VarOutput Module_17_1_Out = new VarOutput(62, PD_Length.Word, 16, 0);
        public VarOutput Module_17_2_Out = new VarOutput(64, PD_Length.Word, 16, 0);
        public VarOutput Module_18_1_Out = new VarOutput(66, PD_Length.Word, 16, 0);
        public VarOutput Module_18_2_Out = new VarOutput(68, PD_Length.Word, 16, 0);
        public VarOutput Module_19_1_Out = new VarOutput(70, PD_Length.Word, 16, 0);
        public VarOutput Module_19_2_Out = new VarOutput(72, PD_Length.Word, 16, 0);
        #endregion

        #region *** Create PCP variables


        #endregion

        #endregion


        #region *** Constructor Declaration ***********************************************

        /// <summary>
        /// Constructor
        /// </summary>      
        public HFI_Appl19()
        {
            // Create the controller
            Controller = new Controller_IBS_ETH("FL IL 24 BK");

            // Settings for the controller         
            Controller.Description = "FL BK for Demonstaration";

            Controller.Startup = ControllerStartup.WithoutConfiguration;
            Controller.Connection = "192.168.17.19";

            Controller.UpdateProcessDataCycleTime = 20;
            Controller.UpdateMailboxTime = 50;

            Controller.WatchdogDeactivate = true;
            Controller.WatchdogTimeout = WatchdogMonitoringTime.Intervall_1048ms;

            // The Controller Configuration property contains special configurations for the controller
            //         Controller.Configuration.DNS_NameResolution     = true;
            //         Controller.Configuration.EnableIBS_Indications  = true;
            Controller.Configuration.ExpertModeActivate = false;
            //         Controller.Configuration.Read_IBS_Cycletime     = false;
            //         Controller.Configuration.UpdateControllerState  = 100;

            #region *** Add Input variables

            //Controller.AddObject(MODULE_1_IN);
            //Controller.AddObject(MODULE_2_IN);
            //Controller.AddObject(MODULE_3_IN);
            //Controller.AddObject(MODULE_4_IN);
            //Controller.AddObject(MODULE_5_IN);
            //Controller.AddObject(MODULE_6_IN);
            //Controller.AddObject(MODULE_7_IN);
            //Controller.AddObject(MODULE_8_IN);
            //Controller.AddObject(MODULE_9_IN);
            //Controller.AddObject(MODULE_10_IN);
            //Controller.AddObject(MODULE_11_IN);
            //Controller.AddObject(MODULE_12_IN);
            //Controller.AddObject(MODULE_13_IN);
            //Controller.AddObject(MODULE_14_IN);
            //Controller.AddObject(MODULE_15_IN);
            //Controller.AddObject(MODULE_16_IN);
            //Controller.AddObject(MODULE_17_IN);
            //Controller.AddObject(MODULE_18_IN);
            //Controller.AddObject(MODULE_19_IN);

            Controller.AddObject(Module_2_2_In);
            Controller.AddObject(Module_2_1_In);
            Controller.AddObject(Module_3_2_In);
            Controller.AddObject(Module_3_1_In);
            Controller.AddObject(Module_4_2_In);
            Controller.AddObject(Module_4_1_In);
            Controller.AddObject(Module_5_2_In);
            Controller.AddObject(Module_5_1_In);
            Controller.AddObject(Module_6_2_In);
            Controller.AddObject(Module_6_1_In);
            Controller.AddObject(Module_7_2_In);
            Controller.AddObject(Module_7_1_In);
            Controller.AddObject(Module_8_2_In);
            Controller.AddObject(Module_8_1_In);
            Controller.AddObject(Module_9_2_In);
            Controller.AddObject(Module_9_1_In);
            Controller.AddObject(Module_10_2_In);
            Controller.AddObject(Module_10_1_In);
            Controller.AddObject(Module_11_2_In);
            Controller.AddObject(Module_11_1_In);
            Controller.AddObject(Module_12_2_In);
            Controller.AddObject(Module_12_1_In);
            Controller.AddObject(Module_13_2_In);
            Controller.AddObject(Module_13_1_In);
            Controller.AddObject(Module_14_2_In);
            Controller.AddObject(Module_14_1_In);
            Controller.AddObject(Module_15_2_In);
            Controller.AddObject(Module_15_1_In);
            Controller.AddObject(Module_16_2_In);
            Controller.AddObject(Module_16_1_In);
            Controller.AddObject(Module_17_2_In);
            Controller.AddObject(Module_17_1_In);
            Controller.AddObject(Module_18_2_In);
            Controller.AddObject(Module_18_1_In);
            Controller.AddObject(Module_19_2_In);
            Controller.AddObject(Module_19_1_In);

            #endregion

            #region *** Add output variables

            //Controller.AddObject(MODULE_2_OUT);
            //Controller.AddObject(MODULE_3_OUT);
            //Controller.AddObject(MODULE_4_OUT);
            //Controller.AddObject(MODULE_5_OUT);
            //Controller.AddObject(MODULE_6_OUT);
            //Controller.AddObject(MODULE_7_OUT);
            //Controller.AddObject(MODULE_8_OUT);
            //Controller.AddObject(MODULE_9_OUT);
            //Controller.AddObject(MODULE_10_OUT);
            //Controller.AddObject(MODULE_11_OUT);
            //Controller.AddObject(MODULE_12_OUT);
            //Controller.AddObject(MODULE_13_OUT);
            //Controller.AddObject(MODULE_14_OUT);
            //Controller.AddObject(MODULE_15_OUT);
            //Controller.AddObject(MODULE_16_OUT);
            //Controller.AddObject(MODULE_17_OUT);
            //Controller.AddObject(MODULE_18_OUT);
            //Controller.AddObject(MODULE_19_OUT);

            Controller.AddObject(Module_2_2_Out);
            Controller.AddObject(Module_2_1_Out);
            Controller.AddObject(Module_3_2_Out);
            Controller.AddObject(Module_3_1_Out);
            Controller.AddObject(Module_4_2_Out);
            Controller.AddObject(Module_4_1_Out);
            Controller.AddObject(Module_5_2_Out);
            Controller.AddObject(Module_5_1_Out);
            Controller.AddObject(Module_6_2_Out);
            Controller.AddObject(Module_6_1_Out);
            Controller.AddObject(Module_7_2_Out);
            Controller.AddObject(Module_7_1_Out);
            Controller.AddObject(Module_8_2_Out);
            Controller.AddObject(Module_8_1_Out);
            Controller.AddObject(Module_9_2_Out);
            Controller.AddObject(Module_9_1_Out);
            Controller.AddObject(Module_10_2_Out);
            Controller.AddObject(Module_10_1_Out);
            Controller.AddObject(Module_11_2_Out);
            Controller.AddObject(Module_11_1_Out);
            Controller.AddObject(Module_12_2_Out);
            Controller.AddObject(Module_12_1_Out);
            Controller.AddObject(Module_13_2_Out);
            Controller.AddObject(Module_13_1_Out);
            Controller.AddObject(Module_14_2_Out);
            Controller.AddObject(Module_14_1_Out);
            Controller.AddObject(Module_15_2_Out);
            Controller.AddObject(Module_15_1_Out);
            Controller.AddObject(Module_16_2_Out);
            Controller.AddObject(Module_16_1_Out);
            Controller.AddObject(Module_17_2_Out);
            Controller.AddObject(Module_17_1_Out);
            Controller.AddObject(Module_18_2_Out);
            Controller.AddObject(Module_18_1_Out);
            Controller.AddObject(Module_19_2_Out);
            Controller.AddObject(Module_19_1_Out);
            #endregion

            #region *** Add PCP variables

            this.SetInOutList();

            #endregion

            // Callbacks from the controller  

            // Called once for each bus cycle
            Controller.OnUpdateProcessData += new UpdateProcessDataHandler(Controller_OnUpdateProcessData);

            // Called once for each mailbox cycle
            Controller.OnUpdateMailbox += new UpdateMailboxHandler(Controller_OnUpdateMailbox);

            // Called whenever an error occurs in the controller object
            Controller.OnException += new PhoenixContact.PxC_Library.Util.ExceptionHandler(Controller_OnException);

            #region *** Create PCP callbacks


            #endregion
        }

        #endregion


        #region *** Events From the Controller ********************************************

        /// <summary>
        /// Called once for each bus cycle
        /// </summary>
        /// <param name="state"></param>
        private void Controller_OnUpdateProcessData(object Sender)
        {
            // TODO insert your process data handling (application) here  
        }


        /// <summary>
        /// Called once for each mailbox cycle
        /// </summary>
        /// <param name="Sender"></param>
        private void Controller_OnUpdateMailbox(object Sender)
        {
            // TODO insert your mailbox handling here (is called once for each MX cycle)
        }

        /// <summary>
        ///  Called whenever an error occurs in the controller object
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Diagnostic"></param>
        void Controller_OnException(Exception ExceptionData)
        {
            // Shows each error message
            //WinForms.MessageBoxShow(ExceptionData);
            System.Diagnostics.Trace.WriteLine(ExceptionData);

            // TODO your error handling can be inserted here
        }

        // Events from the PCP devices

        #region *** Create PCP events


        #endregion

        #endregion


        #region *** IDisposable Member ****************************************************

        public void Dispose()
        {
            if (Controller != null)
            {
                if (Controller.Connect || Controller.Error)
                {
                    Controller.Disable();

                    while (Controller.Connect || Controller.Error)
                    {
                        System.Threading.Thread.Sleep(10);
                    }
                }

                Controller.Dispose();
            }
        }

        #endregion
        private void SetInOutList()
        {
            InOutList = new List<InOut>();

            InOutList.Add(new InOut(29, Module_2_1_In, Module_2_1_Out));
            InOutList.Add(new InOut(30, Module_2_2_In, Module_2_2_Out));
            InOutList.Add(new InOut(31, Module_3_1_In, Module_3_1_Out));
            InOutList.Add(new InOut(32, Module_3_2_In, Module_3_2_Out));
            InOutList.Add(new InOut(33, Module_4_1_In, Module_4_1_Out));
            InOutList.Add(new InOut(34, Module_4_2_In, Module_4_2_Out));
            InOutList.Add(new InOut(35, Module_5_1_In, Module_5_1_Out));
            InOutList.Add(new InOut(36, Module_5_2_In, Module_5_2_Out));
            InOutList.Add(new InOut(37, Module_6_1_In, Module_6_1_Out));
            InOutList.Add(new InOut(38, Module_6_2_In, Module_6_2_Out));
            InOutList.Add(new InOut(39, Module_7_1_In, Module_7_1_Out));
            InOutList.Add(new InOut(40, Module_7_2_In, Module_7_2_Out));
            InOutList.Add(new InOut(41, Module_8_1_In, Module_8_1_Out));
            InOutList.Add(new InOut(42, Module_8_2_In, Module_8_2_Out));
            InOutList.Add(new InOut(43, Module_9_1_In, Module_9_1_Out));
            InOutList.Add(new InOut(44, Module_9_2_In, Module_9_2_Out));
            InOutList.Add(new InOut(45, Module_10_1_In, Module_10_1_Out));
            InOutList.Add(new InOut(46, Module_10_2_In, Module_10_2_Out));
            InOutList.Add(new InOut(47, Module_11_1_In, Module_11_1_Out));
            InOutList.Add(new InOut(48, Module_11_2_In, Module_11_2_Out));
            InOutList.Add(new InOut(49, Module_12_1_In, Module_12_1_Out));
            InOutList.Add(new InOut(50, Module_12_2_In, Module_12_2_Out));
            InOutList.Add(new InOut(51, Module_13_1_In, Module_13_1_Out));
            InOutList.Add(new InOut(52, Module_13_2_In, Module_13_2_Out));
            InOutList.Add(new InOut(53, Module_14_1_In, Module_14_1_Out));
            InOutList.Add(new InOut(54, Module_14_2_In, Module_14_2_Out));
            InOutList.Add(new InOut(55, Module_15_1_In, Module_15_1_Out));
            InOutList.Add(new InOut(56, Module_15_2_In, Module_15_2_Out));
            InOutList.Add(new InOut(57, Module_16_1_In, Module_16_1_Out));
            InOutList.Add(new InOut(58, Module_16_2_In, Module_16_2_Out));
            InOutList.Add(new InOut(59, Module_17_1_In, Module_17_1_Out));
            InOutList.Add(new InOut(60, Module_17_2_In, Module_17_2_Out));
            InOutList.Add(new InOut(61, Module_18_1_In, Module_18_1_Out));
            InOutList.Add(new InOut(62, Module_18_2_In, Module_18_2_Out));
            InOutList.Add(new InOut(63, Module_19_1_In, Module_19_1_Out));
            InOutList.Add(new InOut(64, Module_19_2_In, Module_19_2_Out));
        }


        public ProcessDataResult GetInputValue(int id)
        {
            ProcessDataResult result = null;
            try
            {
                double value = InOutList.FirstOrDefault(x => x.Id == id).Input.Value;
                result = new ProcessDataResult(value / Properties.Settings.Default.ScaleCoefficient, false);
            }
            catch (Exception ex)
            {
                result = new ProcessDataResult(0, true);
            }

            return result;
        }

        public DeviceStatusCache GetDeviceStatus()
        {
            DeviceStatusCache result = null;

            if (Controller.Error)
            {
                result = new DeviceStatusCache(25, "Error", "The 'Controller' class is reporting an error", StatusColor.Red, "192.168.1.19");
            }
            else if (Controller.Run)
            {
                result = new DeviceStatusCache(24, "Run", "The 'Controller' class is ready for operation. Input and output data is being exchanged cyclically", StatusColor.Green, "192.168.1.19");
            }

            return result;
        }

    }

}
