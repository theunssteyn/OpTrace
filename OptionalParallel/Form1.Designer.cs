namespace OpTrace
{
    partial class Form1
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
            this.button11 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonInterleave = new System.Windows.Forms.Button();
            this.buttonTraceDiff = new System.Windows.Forms.Button();
            this.buttonGeneralParallel = new System.Windows.Forms.Button();
            this.buttonOptionalParallel = new System.Windows.Forms.Button();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBoxProcessName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddProcess = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxProcessTraces = new System.Windows.Forms.TextBox();
            this.buttonClearOutput = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
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
            this.radioButtonSimplex = new System.Windows.Forms.RadioButton();
            this.radioButtonBroadcast = new System.Windows.Forms.RadioButton();
            this.radioButtonP2p = new System.Windows.Forms.RadioButton();
            this.textBoxTraceCount = new System.Windows.Forms.TextBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.radioButtonHalfDuplex = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button11);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.buttonInterleave);
            this.groupBox1.Controls.Add(this.buttonTraceDiff);
            this.groupBox1.Controls.Add(this.buttonGeneralParallel);
            this.groupBox1.Controls.Add(this.buttonOptionalParallel);
            this.groupBox1.Location = new System.Drawing.Point(273, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(132, 64);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 8;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(115, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Equal?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Gen-Inter";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Opt-Inter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonInterleave
            // 
            this.buttonInterleave.Location = new System.Drawing.Point(7, 78);
            this.buttonInterleave.Name = "buttonInterleave";
            this.buttonInterleave.Size = new System.Drawing.Size(102, 23);
            this.buttonInterleave.TabIndex = 4;
            this.buttonInterleave.Text = "Interleave";
            this.buttonInterleave.UseVisualStyleBackColor = true;
            this.buttonInterleave.Click += new System.EventHandler(this.buttonInterleave_Click);
            // 
            // buttonTraceDiff
            // 
            this.buttonTraceDiff.Location = new System.Drawing.Point(115, 34);
            this.buttonTraceDiff.Name = "buttonTraceDiff";
            this.buttonTraceDiff.Size = new System.Drawing.Size(102, 23);
            this.buttonTraceDiff.TabIndex = 3;
            this.buttonTraceDiff.Text = "Difference";
            this.buttonTraceDiff.UseVisualStyleBackColor = true;
            this.buttonTraceDiff.Click += new System.EventHandler(this.buttonTraceDiff_Click);
            // 
            // buttonGeneralParallel
            // 
            this.buttonGeneralParallel.Location = new System.Drawing.Point(6, 48);
            this.buttonGeneralParallel.Name = "buttonGeneralParallel";
            this.buttonGeneralParallel.Size = new System.Drawing.Size(103, 23);
            this.buttonGeneralParallel.TabIndex = 2;
            this.buttonGeneralParallel.Text = "General Parallel";
            this.buttonGeneralParallel.UseVisualStyleBackColor = true;
            this.buttonGeneralParallel.Click += new System.EventHandler(this.buttonGeneralParallel_Click);
            // 
            // buttonOptionalParallel
            // 
            this.buttonOptionalParallel.Location = new System.Drawing.Point(6, 19);
            this.buttonOptionalParallel.Name = "buttonOptionalParallel";
            this.buttonOptionalParallel.Size = new System.Drawing.Size(103, 23);
            this.buttonOptionalParallel.TabIndex = 0;
            this.buttonOptionalParallel.Text = "Optional Parallel";
            this.buttonOptionalParallel.UseVisualStyleBackColor = true;
            this.buttonOptionalParallel.Click += new System.EventHandler(this.buttonOptionalParallel_Click);
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.BackColor = System.Drawing.Color.MidnightBlue;
            this.richTextBoxOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxOutput.ForeColor = System.Drawing.Color.White;
            this.richTextBoxOutput.Location = new System.Drawing.Point(12, 317);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(1149, 147);
            this.richTextBoxOutput.TabIndex = 1;
            this.richTextBoxOutput.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.textBoxProcessName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.buttonAddProcess);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxProcessTraces);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 299);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(120, 128);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(9, 169);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(240, 95);
            this.listBox1.TabIndex = 6;
            // 
            // textBoxProcessName
            // 
            this.textBoxProcessName.Location = new System.Drawing.Point(52, 21);
            this.textBoxProcessName.Name = "textBoxProcessName";
            this.textBoxProcessName.Size = new System.Drawing.Size(100, 20);
            this.textBoxProcessName.TabIndex = 5;
            this.textBoxProcessName.Text = "P";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Traces";
            // 
            // buttonAddProcess
            // 
            this.buttonAddProcess.Location = new System.Drawing.Point(9, 140);
            this.buttonAddProcess.Name = "buttonAddProcess";
            this.buttonAddProcess.Size = new System.Drawing.Size(75, 23);
            this.buttonAddProcess.TabIndex = 1;
            this.buttonAddProcess.Text = "Add Process";
            this.buttonAddProcess.UseVisualStyleBackColor = true;
            this.buttonAddProcess.Click += new System.EventHandler(this.buttonAddProcess_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // textBoxProcessTraces
            // 
            this.textBoxProcessTraces.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProcessTraces.Location = new System.Drawing.Point(52, 55);
            this.textBoxProcessTraces.Multiline = true;
            this.textBoxProcessTraces.Name = "textBoxProcessTraces";
            this.textBoxProcessTraces.Size = new System.Drawing.Size(195, 58);
            this.textBoxProcessTraces.TabIndex = 0;
            this.textBoxProcessTraces.Text = "<ia,!b,id>";
            // 
            // buttonClearOutput
            // 
            this.buttonClearOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearOutput.Location = new System.Drawing.Point(1086, 470);
            this.buttonClearOutput.Name = "buttonClearOutput";
            this.buttonClearOutput.Size = new System.Drawing.Size(75, 23);
            this.buttonClearOutput.TabIndex = 3;
            this.buttonClearOutput.Text = "Clear";
            this.buttonClearOutput.UseVisualStyleBackColor = true;
            this.buttonClearOutput.Click += new System.EventHandler(this.buttonClearOutput_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Location = new System.Drawing.Point(398, 223);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(92, 56);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Simulation";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(7, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(280, 223);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "Combine";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.buttonClear);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.textBoxTraceCount);
            this.groupBox4.Controls.Add(this.button12);
            this.groupBox4.Controls.Add(this.button10);
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Location = new System.Drawing.Point(529, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(515, 221);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Refinement Checks";
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
            this.groupBox5.Location = new System.Drawing.Point(320, 19);
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
            this.buttonClear.Location = new System.Drawing.Point(7, 182);
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
            this.groupBox6.Controls.Add(this.radioButtonP2p);
            this.groupBox6.Location = new System.Drawing.Point(217, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(97, 144);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Communication";
            // 
            // radioButtonSimplex
            // 
            this.radioButtonSimplex.AutoSize = true;
            this.radioButtonSimplex.Location = new System.Drawing.Point(6, 67);
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
            this.radioButtonBroadcast.Location = new System.Drawing.Point(6, 44);
            this.radioButtonBroadcast.Name = "radioButtonBroadcast";
            this.radioButtonBroadcast.Size = new System.Drawing.Size(73, 17);
            this.radioButtonBroadcast.TabIndex = 2;
            this.radioButtonBroadcast.TabStop = true;
            this.radioButtonBroadcast.Text = "Broadcast";
            this.radioButtonBroadcast.UseVisualStyleBackColor = true;
            // 
            // radioButtonP2p
            // 
            this.radioButtonP2p.AutoSize = true;
            this.radioButtonP2p.Location = new System.Drawing.Point(6, 21);
            this.radioButtonP2p.Name = "radioButtonP2p";
            this.radioButtonP2p.Size = new System.Drawing.Size(87, 17);
            this.radioButtonP2p.TabIndex = 1;
            this.radioButtonP2p.Text = "Point to point";
            this.radioButtonP2p.UseVisualStyleBackColor = true;
            // 
            // textBoxTraceCount
            // 
            this.textBoxTraceCount.Location = new System.Drawing.Point(139, 195);
            this.textBoxTraceCount.Name = "textBoxTraceCount";
            this.textBoxTraceCount.Size = new System.Drawing.Size(70, 20);
            this.textBoxTraceCount.TabIndex = 5;
            this.textBoxTraceCount.Text = "0";
            this.textBoxTraceCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(112, 107);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(97, 23);
            this.button12.TabIndex = 4;
            this.button12.Text = "Model check file";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(7, 49);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(98, 23);
            this.button10.TabIndex = 3;
            this.button10.Text = "Generate Traces";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(7, 107);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(98, 23);
            this.button9.TabIndex = 2;
            this.button9.Text = "Model Check";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(7, 78);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(98, 23);
            this.button8.TabIndex = 1;
            this.button8.Text = "Build Model";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(7, 20);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(98, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "Load Network...";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // radioButtonHalfDuplex
            // 
            this.radioButtonHalfDuplex.AutoSize = true;
            this.radioButtonHalfDuplex.Location = new System.Drawing.Point(6, 90);
            this.radioButtonHalfDuplex.Name = "radioButtonHalfDuplex";
            this.radioButtonHalfDuplex.Size = new System.Drawing.Size(80, 17);
            this.radioButtonHalfDuplex.TabIndex = 4;
            this.radioButtonHalfDuplex.Text = "Half Duplex";
            this.radioButtonHalfDuplex.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 505);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonClearOutput);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.richTextBoxOutput);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Optional Parallel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonGeneralParallel;
        private System.Windows.Forms.Button buttonOptionalParallel;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxProcessName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxProcessTraces;
        private System.Windows.Forms.Button buttonClearOutput;
        private System.Windows.Forms.Button buttonTraceDiff;
        private System.Windows.Forms.Button buttonInterleave;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TextBox textBoxResError;
        private System.Windows.Forms.TextBox textBoxResFail;
        private System.Windows.Forms.TextBox textBoxResPass;
        private System.Windows.Forms.TextBox textBoxResTotal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTraceCount;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButtonP2p;
        private System.Windows.Forms.RadioButton radioButtonBroadcast;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.RadioButton radioButtonSimplex;
        private System.Windows.Forms.RadioButton radioButtonHalfDuplex;
    }
}

