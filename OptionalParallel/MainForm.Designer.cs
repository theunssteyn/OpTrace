namespace OpTrace
{
    partial class MainForm
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTraceCount = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxResError = new System.Windows.Forms.TextBox();
            this.textBoxResFail = new System.Windows.Forms.TextBox();
            this.textBoxResPass = new System.Windows.Forms.TextBox();
            this.textBoxResTotal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButtonHalfDuplex = new System.Windows.Forms.RadioButton();
            this.radioButtonSimplex = new System.Windows.Forms.RadioButton();
            this.radioButtonBroadcast = new System.Windows.Forms.RadioButton();
            this.buttonModelCheckFile = new System.Windows.Forms.Button();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonModelCheck = new System.Windows.Forms.Button();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.textBoxTraceCount);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.buttonClear);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.buttonModelCheckFile);
            this.groupBox4.Controls.Add(this.buttonGenerate);
            this.groupBox4.Controls.Add(this.buttonModelCheck);
            this.groupBox4.Controls.Add(this.buttonBuild);
            this.groupBox4.Controls.Add(this.buttonLoad);
            this.groupBox4.Location = new System.Drawing.Point(10, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(428, 187);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Refinement Checks";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Traces Checked";
            // 
            // textBoxTraceCount
            // 
            this.textBoxTraceCount.Location = new System.Drawing.Point(157, 138);
            this.textBoxTraceCount.Name = "textBoxTraceCount";
            this.textBoxTraceCount.Size = new System.Drawing.Size(84, 20);
            this.textBoxTraceCount.TabIndex = 10;
            this.textBoxTraceCount.Text = "0";
            this.textBoxTraceCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxResError);
            this.groupBox5.Controls.Add(this.textBoxResFail);
            this.groupBox5.Controls.Add(this.textBoxResPass);
            this.groupBox5.Controls.Add(this.textBoxResTotal);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(251, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(171, 130);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Results";
            // 
            // textBoxResError
            // 
            this.textBoxResError.Location = new System.Drawing.Point(62, 97);
            this.textBoxResError.Name = "textBoxResError";
            this.textBoxResError.Size = new System.Drawing.Size(100, 20);
            this.textBoxResError.TabIndex = 7;
            this.textBoxResError.Text = "0";
            this.textBoxResError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxResFail
            // 
            this.textBoxResFail.Location = new System.Drawing.Point(62, 71);
            this.textBoxResFail.Name = "textBoxResFail";
            this.textBoxResFail.Size = new System.Drawing.Size(100, 20);
            this.textBoxResFail.TabIndex = 6;
            this.textBoxResFail.Text = "0";
            this.textBoxResFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxResPass
            // 
            this.textBoxResPass.Location = new System.Drawing.Point(62, 45);
            this.textBoxResPass.Name = "textBoxResPass";
            this.textBoxResPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxResPass.TabIndex = 5;
            this.textBoxResPass.Text = "0";
            this.textBoxResPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxResTotal
            // 
            this.textBoxResTotal.Location = new System.Drawing.Point(62, 19);
            this.textBoxResTotal.Name = "textBoxResTotal";
            this.textBoxResTotal.Size = new System.Drawing.Size(100, 20);
            this.textBoxResTotal.TabIndex = 4;
            this.textBoxResTotal.Text = "0";
            this.textBoxResTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Error";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Total";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Fail";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Pass";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(338, 158);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 9;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButtonHalfDuplex);
            this.groupBox6.Controls.Add(this.radioButtonSimplex);
            this.groupBox6.Controls.Add(this.radioButtonBroadcast);
            this.groupBox6.Location = new System.Drawing.Point(148, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(97, 91);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Communication";
            // 
            // radioButtonHalfDuplex
            // 
            this.radioButtonHalfDuplex.AutoSize = true;
            this.radioButtonHalfDuplex.Location = new System.Drawing.Point(6, 68);
            this.radioButtonHalfDuplex.Name = "radioButtonHalfDuplex";
            this.radioButtonHalfDuplex.Size = new System.Drawing.Size(80, 17);
            this.radioButtonHalfDuplex.TabIndex = 4;
            this.radioButtonHalfDuplex.Text = "Half Duplex";
            this.radioButtonHalfDuplex.UseVisualStyleBackColor = true;
            // 
            // radioButtonSimplex
            // 
            this.radioButtonSimplex.AutoSize = true;
            this.radioButtonSimplex.Location = new System.Drawing.Point(6, 45);
            this.radioButtonSimplex.Name = "radioButtonSimplex";
            this.radioButtonSimplex.Size = new System.Drawing.Size(61, 17);
            this.radioButtonSimplex.TabIndex = 3;
            this.radioButtonSimplex.Text = "Simplex";
            this.radioButtonSimplex.UseVisualStyleBackColor = true;
            // 
            // radioButtonBroadcast
            // 
            this.radioButtonBroadcast.AutoSize = true;
            this.radioButtonBroadcast.Checked = true;
            this.radioButtonBroadcast.Location = new System.Drawing.Point(6, 20);
            this.radioButtonBroadcast.Name = "radioButtonBroadcast";
            this.radioButtonBroadcast.Size = new System.Drawing.Size(73, 17);
            this.radioButtonBroadcast.TabIndex = 2;
            this.radioButtonBroadcast.TabStop = true;
            this.radioButtonBroadcast.Text = "Broadcast";
            this.radioButtonBroadcast.UseVisualStyleBackColor = true;
            // 
            // buttonModelCheckFile
            // 
            this.buttonModelCheckFile.Location = new System.Drawing.Point(6, 136);
            this.buttonModelCheckFile.Name = "buttonModelCheckFile";
            this.buttonModelCheckFile.Size = new System.Drawing.Size(136, 23);
            this.buttonModelCheckFile.TabIndex = 4;
            this.buttonModelCheckFile.Text = "Trace Refinement  File";
            this.buttonModelCheckFile.UseVisualStyleBackColor = true;
            this.buttonModelCheckFile.Click += new System.EventHandler(this.buttonModelCheckFile_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(7, 49);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(135, 23);
            this.buttonGenerate.TabIndex = 3;
            this.buttonGenerate.Text = "Generate Traces";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonModelCheck
            // 
            this.buttonModelCheck.Location = new System.Drawing.Point(7, 107);
            this.buttonModelCheck.Name = "buttonModelCheck";
            this.buttonModelCheck.Size = new System.Drawing.Size(135, 23);
            this.buttonModelCheck.TabIndex = 2;
            this.buttonModelCheck.Text = "Trace Refinement";
            this.buttonModelCheck.UseVisualStyleBackColor = true;
            this.buttonModelCheck.Click += new System.EventHandler(this.buttonModelCheck_Click);
            // 
            // buttonBuild
            // 
            this.buttonBuild.Location = new System.Drawing.Point(7, 78);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(135, 23);
            this.buttonBuild.TabIndex = 1;
            this.buttonBuild.Text = "Build Model";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(7, 20);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(135, 23);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Load Network...";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.BackColor = System.Drawing.Color.MidnightBlue;
            this.richTextBoxOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxOutput.ForeColor = System.Drawing.Color.White;
            this.richTextBoxOutput.Location = new System.Drawing.Point(10, 204);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(428, 140);
            this.richTextBoxOutput.TabIndex = 8;
            this.richTextBoxOutput.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelFileName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 347);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(450, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel1.Text = "File:";
            // 
            // toolStripStatusLabelFileName
            // 
            this.toolStripStatusLabelFileName.Name = "toolStripStatusLabelFileName";
            this.toolStripStatusLabelFileName.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 369);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.richTextBoxOutput);
            this.Controls.Add(this.groupBox4);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "OpTrace v1.0";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBoxResError;
        private System.Windows.Forms.TextBox textBoxResFail;
        private System.Windows.Forms.TextBox textBoxResPass;
        private System.Windows.Forms.TextBox textBoxResTotal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButtonHalfDuplex;
        private System.Windows.Forms.RadioButton radioButtonSimplex;
        private System.Windows.Forms.RadioButton radioButtonBroadcast;
        private System.Windows.Forms.Button buttonModelCheckFile;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonModelCheck;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.TextBox textBoxTraceCount;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFileName;
        private System.Windows.Forms.Label label1;
    }
}