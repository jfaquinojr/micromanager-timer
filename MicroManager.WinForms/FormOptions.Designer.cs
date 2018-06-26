namespace MicroManager.WinForms
{
    partial class FormOptions
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaptureInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPomodoroDuration = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtShortBreakDuration = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLongBreakDuration = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLongBreakDelay = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSaveOptions = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaptureInterval)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPomodoroDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortBreakDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLongBreakDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLongBreakDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCaptureInterval);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Screenshots";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Screenshot intervals (seconds)";
            // 
            // txtCaptureInterval
            // 
            this.txtCaptureInterval.Location = new System.Drawing.Point(194, 24);
            this.txtCaptureInterval.Name = "txtCaptureInterval";
            this.txtCaptureInterval.Size = new System.Drawing.Size(86, 20);
            this.txtCaptureInterval.TabIndex = 1;
            this.txtCaptureInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLongBreakDelay);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtLongBreakDuration);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtShortBreakDuration);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtPomodoroDuration);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 142);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pomodoro Timer";
            // 
            // txtPomodoroDuration
            // 
            this.txtPomodoroDuration.Location = new System.Drawing.Point(194, 24);
            this.txtPomodoroDuration.Name = "txtPomodoroDuration";
            this.txtPomodoroDuration.Size = new System.Drawing.Size(86, 20);
            this.txtPomodoroDuration.TabIndex = 1;
            this.txtPomodoroDuration.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pomodoro duration (minutes)";
            // 
            // txtShortBreakDuration
            // 
            this.txtShortBreakDuration.Location = new System.Drawing.Point(194, 50);
            this.txtShortBreakDuration.Name = "txtShortBreakDuration";
            this.txtShortBreakDuration.Size = new System.Drawing.Size(86, 20);
            this.txtShortBreakDuration.TabIndex = 3;
            this.txtShortBreakDuration.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Short break duration (minutes)";
            // 
            // txtLongBreakDuration
            // 
            this.txtLongBreakDuration.Location = new System.Drawing.Point(194, 74);
            this.txtLongBreakDuration.Name = "txtLongBreakDuration";
            this.txtLongBreakDuration.Size = new System.Drawing.Size(86, 20);
            this.txtLongBreakDuration.TabIndex = 5;
            this.txtLongBreakDuration.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Long break duration (minutes)";
            // 
            // txtLongBreakDelay
            // 
            this.txtLongBreakDelay.Location = new System.Drawing.Point(194, 100);
            this.txtLongBreakDelay.Name = "txtLongBreakDelay";
            this.txtLongBreakDelay.Size = new System.Drawing.Size(86, 20);
            this.txtLongBreakDelay.TabIndex = 7;
            this.txtLongBreakDelay.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Long break delay (in pomodoros)";
            // 
            // btnSaveOptions
            // 
            this.btnSaveOptions.Location = new System.Drawing.Point(227, 237);
            this.btnSaveOptions.Name = "btnSaveOptions";
            this.btnSaveOptions.Size = new System.Drawing.Size(75, 23);
            this.btnSaveOptions.TabIndex = 4;
            this.btnSaveOptions.Text = "Save";
            this.btnSaveOptions.UseVisualStyleBackColor = true;
            this.btnSaveOptions.Click += new System.EventHandler(this.btnSaveOptions_Click);
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 267);
            this.Controls.Add(this.btnSaveOptions);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaptureInterval)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPomodoroDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortBreakDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLongBreakDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLongBreakDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown txtCaptureInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown txtLongBreakDelay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown txtLongBreakDuration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtShortBreakDuration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtPomodoroDuration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSaveOptions;
    }
}