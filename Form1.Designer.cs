
using System;
using System.Windows.Forms;
using NAudio.Wave;


namespace WindowsFormsApp1
{

    partial class SOUNDBOOOOOOORD
    {
       
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SOUNDBOOOOOOORD));
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowPanel
            // 
            this.flowPanel.Controls.Add(this.buttonPlay);
            this.flowPanel.Controls.Add(this.buttonStop);
            this.flowPanel.Controls.Add(this.button2);
            this.flowPanel.Controls.Add(this.comboBox1);
            this.flowPanel.Controls.Add(this.comboBox2);
            this.flowPanel.Location = new System.Drawing.Point(0, 0);
            this.flowPanel.Margin = new System.Windows.Forms.Padding(10);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(281, 89);
            this.flowPanel.TabIndex = 0;
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(3, 3);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "Refresh";
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(84, 3);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Dispose";
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(165, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Change Folder";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(274, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(3, 59);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(274, 21);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(294, 37);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(35, 13);
            this.lbl1.TabIndex = 2;
            this.lbl1.Text = "label1";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(294, 62);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(35, 13);
            this.lbl2.TabIndex = 3;
            this.lbl2.Text = "label1";
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(3, 3);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 6;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.button2_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnRecord);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(341, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(173, 27);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(341, 44);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(173, 45);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.Value = 20;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(385, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Volume:20%";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 96);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(514, 251);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // SOUNDBOOOOOOORD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 348);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(531, 387);
            this.Name = "SOUNDBOOOOOOORD";
            this.Text = "SOUNDBOOOOOOORD";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.SOUNDBOOOOOOORD_SizeChanged);
            this.flowPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion
        private System.Windows.Forms.Button button1;
        private FlowLayoutPanel flowPanel;
        private Button buttonPlay;
        private Button buttonStop;
        private Label lbl1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label lbl2;
        private Button btnRecord;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button2;
        private TrackBar trackBar1;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel2;
    }
}

