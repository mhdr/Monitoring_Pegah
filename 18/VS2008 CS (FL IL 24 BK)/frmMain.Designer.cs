namespace VS2005_CS__ETH_BK_DI8_DO4_
{
   partial class frmMain
   {
      /// <summary>
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      
      #region Vom Windows Form-Designer generierter Code

      /// <summary>
      /// Erforderliche Methode für die Designerunterstützung.
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent()
      {
      	this.components = new System.ComponentModel.Container();
      	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      	this.tabProcessData = new System.Windows.Forms.TabPage();
      	this._ctrlVarOutput1 = new PhoenixContact.HFI.Visualization.ctrlVarOutput();
      	this._ctrlVarInput1 = new PhoenixContact.HFI.Visualization.ctrlVarInput();
      	this.tabController = new System.Windows.Forms.TabPage();
      	this._ctrlMessageClient1 = new PhoenixContact.HFI.Visualization.ctrlMessageClient();
      	this._ctrlIBS_Diag1 = new PhoenixContact.HFI.Visualization.ctrlIBS_Diag();
      	this._ctrlController1 = new PhoenixContact.HFI.Visualization.ctrlController();
      	this.tcAXI = new System.Windows.Forms.TabControl();
      	this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
      	this.tabProcessData.SuspendLayout();
      	this.tabController.SuspendLayout();
      	this.tcAXI.SuspendLayout();
      	this.SuspendLayout();
      	// 
      	// tabProcessData
      	// 
      	this.tabProcessData.Controls.Add(this._ctrlVarOutput1);
      	this.tabProcessData.Controls.Add(this._ctrlVarInput1);
      	this.tabProcessData.Location = new System.Drawing.Point(4, 25);
      	this.tabProcessData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      	this.tabProcessData.Name = "tabProcessData";
      	this.tabProcessData.Size = new System.Drawing.Size(1339, 853);
      	this.tabProcessData.TabIndex = 1;
      	this.tabProcessData.Text = "Interbus and I/O Data";
      	this.tabProcessData.UseVisualStyleBackColor = true;
      	// 
      	// _ctrlVarOutput1
      	// 
      	this._ctrlVarOutput1.ControlText = "ctrlVarOutput";
      	this._ctrlVarOutput1.EditActivate = true;
      	this._ctrlVarOutput1.Location = new System.Drawing.Point(544, 10);
      	this._ctrlVarOutput1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      	this._ctrlVarOutput1.Name = "_ctrlVarOutput1";
      	this._ctrlVarOutput1.Size = new System.Drawing.Size(512, 354);
      	this._ctrlVarOutput1.TabIndex = 1;
      	// 
      	// _ctrlVarInput1
      	// 
      	this._ctrlVarInput1.ControlText = "ctrlVarInput";
      	this._ctrlVarInput1.Location = new System.Drawing.Point(11, 10);
      	this._ctrlVarInput1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      	this._ctrlVarInput1.Name = "_ctrlVarInput1";
      	this._ctrlVarInput1.Size = new System.Drawing.Size(512, 354);
      	this._ctrlVarInput1.TabIndex = 0;
      	// 
      	// tabController
      	// 
      	this.tabController.Controls.Add(this._ctrlMessageClient1);
      	this.tabController.Controls.Add(this._ctrlIBS_Diag1);
      	this.tabController.Controls.Add(this._ctrlController1);
      	this.tabController.Location = new System.Drawing.Point(4, 25);
      	this.tabController.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      	this.tabController.Name = "tabController";
      	this.tabController.Size = new System.Drawing.Size(1339, 853);
      	this.tabController.TabIndex = 0;
      	this.tabController.Text = "Controller and communication";
      	this.tabController.UseVisualStyleBackColor = true;
      	// 
      	// _ctrlMessageClient1
      	// 
      	this._ctrlMessageClient1.ControlText = "ctrlMessageClient";
      	this._ctrlMessageClient1.Location = new System.Drawing.Point(11, 388);
      	this._ctrlMessageClient1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      	this._ctrlMessageClient1.MaximumSize = new System.Drawing.Size(683, 433);
      	this._ctrlMessageClient1.MinimumSize = new System.Drawing.Size(683, 433);
      	this._ctrlMessageClient1.Name = "_ctrlMessageClient1";
      	this._ctrlMessageClient1.Size = new System.Drawing.Size(683, 433);
      	this._ctrlMessageClient1.TabIndex = 0;
      	// 
      	// _ctrlIBS_Diag1
      	// 
      	this._ctrlIBS_Diag1.ControlText = "ctrlIBS_Diag";
      	this._ctrlIBS_Diag1.Location = new System.Drawing.Point(715, 388);
      	this._ctrlIBS_Diag1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      	this._ctrlIBS_Diag1.MaximumSize = new System.Drawing.Size(496, 345);
      	this._ctrlIBS_Diag1.MinimumSize = new System.Drawing.Size(496, 345);
      	this._ctrlIBS_Diag1.Name = "_ctrlIBS_Diag1";
      	this._ctrlIBS_Diag1.Size = new System.Drawing.Size(496, 345);
      	this._ctrlIBS_Diag1.TabIndex = 0;
      	// 
      	// _ctrlController1
      	// 
      	this._ctrlController1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      	this._ctrlController1.ControlText = "ctrlController";
      	this._ctrlController1.Location = new System.Drawing.Point(11, 10);
      	this._ctrlController1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      	this._ctrlController1.MaximumSize = new System.Drawing.Size(939, 359);
      	this._ctrlController1.MinimumSize = new System.Drawing.Size(435, 359);
      	this._ctrlController1.Name = "_ctrlController1";
      	this._ctrlController1.Size = new System.Drawing.Size(939, 359);
      	this._ctrlController1.TabIndex = 0;
      	// 
      	// tcAXI
      	// 
      	this.tcAXI.Controls.Add(this.tabController);
      	this.tcAXI.Controls.Add(this.tabProcessData);
      	this.tcAXI.Location = new System.Drawing.Point(16, 15);
      	this.tcAXI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      	this.tcAXI.Name = "tcAXI";
      	this.tcAXI.SelectedIndex = 0;
      	this.tcAXI.Size = new System.Drawing.Size(1347, 882);
      	this.tcAXI.TabIndex = 1;
      	// 
      	// tmrUpdate
      	// 
      	this.tmrUpdate.Enabled = true;
      	// 
      	// frmMain
      	// 
      	this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      	this.ClientSize = new System.Drawing.Size(1379, 912);
      	this.Controls.Add(this.tcAXI);
      	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      	this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      	this.MaximizeBox = false;
      	this.MinimumSize = new System.Drawing.Size(1383, 934);
      	this.Name = "frmMain";
      	this.Text = "192.168.17.18";
      	this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      	this.Load += new System.EventHandler(this.frmMain_Load);
      	this.tabProcessData.ResumeLayout(false);
      	this.tabController.ResumeLayout(false);
      	this.tcAXI.ResumeLayout(false);
      	this.ResumeLayout(false);
      }

      #endregion

      private System.Windows.Forms.TabPage tabProcessData;
      private PhoenixContact.HFI.Visualization.ctrlVarOutput _ctrlVarOutput1;
      private PhoenixContact.HFI.Visualization.ctrlVarInput _ctrlVarInput1;
      private System.Windows.Forms.TabPage tabController;
      private PhoenixContact.HFI.Visualization.ctrlMessageClient _ctrlMessageClient1;
      private PhoenixContact.HFI.Visualization.ctrlIBS_Diag _ctrlIBS_Diag1;
      private PhoenixContact.HFI.Visualization.ctrlController _ctrlController1;
      private System.Windows.Forms.TabControl tcAXI;
      private System.Windows.Forms.Timer tmrUpdate;


   }
}

