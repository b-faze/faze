namespace Faze.Rendering.Playground
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.borderTrackBar = new System.Windows.Forms.TrackBar();
            this.borderTextBox = new System.Windows.Forms.TextBox();
            this.sizeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sizeTrackBar = new System.Windows.Forms.TrackBar();
            this.maxDepthTrackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.maxDepthTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.presetDdl = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.viewportLeftTxt = new System.Windows.Forms.TextBox();
            this.viewportTopTxt = new System.Windows.Forms.TextBox();
            this.viewportScaleTxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.borderTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxDepthTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(500, 500);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Interval = 33;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(518, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Border";
            // 
            // borderTrackBar
            // 
            this.borderTrackBar.Location = new System.Drawing.Point(624, 183);
            this.borderTrackBar.Maximum = 100;
            this.borderTrackBar.Name = "borderTrackBar";
            this.borderTrackBar.Size = new System.Drawing.Size(163, 45);
            this.borderTrackBar.TabIndex = 3;
            this.borderTrackBar.ValueChanged += new System.EventHandler(this.borderTrackBarChanged);
            // 
            // borderTextBox
            // 
            this.borderTextBox.Location = new System.Drawing.Point(518, 183);
            this.borderTextBox.Name = "borderTextBox";
            this.borderTextBox.ReadOnly = true;
            this.borderTextBox.Size = new System.Drawing.Size(100, 23);
            this.borderTextBox.TabIndex = 4;
            // 
            // sizeTextBox
            // 
            this.sizeTextBox.Location = new System.Drawing.Point(518, 84);
            this.sizeTextBox.Name = "sizeTextBox";
            this.sizeTextBox.ReadOnly = true;
            this.sizeTextBox.Size = new System.Drawing.Size(100, 23);
            this.sizeTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(518, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Size";
            // 
            // sizeTrackBar
            // 
            this.sizeTrackBar.Location = new System.Drawing.Point(624, 84);
            this.sizeTrackBar.Minimum = 1;
            this.sizeTrackBar.Name = "sizeTrackBar";
            this.sizeTrackBar.Size = new System.Drawing.Size(163, 45);
            this.sizeTrackBar.TabIndex = 6;
            this.sizeTrackBar.Value = 1;
            this.sizeTrackBar.ValueChanged += new System.EventHandler(this.sizeTrackBarChanged);
            // 
            // maxDepthTrackBar
            // 
            this.maxDepthTrackBar.Location = new System.Drawing.Point(624, 135);
            this.maxDepthTrackBar.Name = "maxDepthTrackBar";
            this.maxDepthTrackBar.Size = new System.Drawing.Size(163, 45);
            this.maxDepthTrackBar.TabIndex = 6;
            this.maxDepthTrackBar.Value = 1;
            this.maxDepthTrackBar.ValueChanged += new System.EventHandler(this.maxDepthTrackBarChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(518, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Render Depth";
            // 
            // maxDepthTextBox
            // 
            this.maxDepthTextBox.Location = new System.Drawing.Point(518, 135);
            this.maxDepthTextBox.Name = "maxDepthTextBox";
            this.maxDepthTextBox.ReadOnly = true;
            this.maxDepthTextBox.Size = new System.Drawing.Size(100, 23);
            this.maxDepthTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(518, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Preset";
            // 
            // presetDdl
            // 
            this.presetDdl.FormattingEnabled = true;
            this.presetDdl.Location = new System.Drawing.Point(518, 31);
            this.presetDdl.Name = "presetDdl";
            this.presetDdl.Size = new System.Drawing.Size(188, 23);
            this.presetDdl.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(712, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(522, 489);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 10;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(518, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Viewport";
            // 
            // viewportLeftTxt
            // 
            this.viewportLeftTxt.Location = new System.Drawing.Point(518, 251);
            this.viewportLeftTxt.Name = "viewportLeftTxt";
            this.viewportLeftTxt.ReadOnly = true;
            this.viewportLeftTxt.Size = new System.Drawing.Size(79, 23);
            this.viewportLeftTxt.TabIndex = 4;
            // 
            // viewportTopTxt
            // 
            this.viewportTopTxt.Location = new System.Drawing.Point(603, 251);
            this.viewportTopTxt.Name = "viewportTopTxt";
            this.viewportTopTxt.ReadOnly = true;
            this.viewportTopTxt.Size = new System.Drawing.Size(79, 23);
            this.viewportTopTxt.TabIndex = 4;
            // 
            // viewportScaleTxt
            // 
            this.viewportScaleTxt.Location = new System.Drawing.Point(688, 251);
            this.viewportScaleTxt.Name = "viewportScaleTxt";
            this.viewportScaleTxt.ReadOnly = true;
            this.viewportScaleTxt.Size = new System.Drawing.Size(79, 23);
            this.viewportScaleTxt.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 524);
            this.Controls.Add(this.viewportScaleTxt);
            this.Controls.Add(this.viewportTopTxt);
            this.Controls.Add(this.viewportLeftTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.presetDdl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maxDepthTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maxDepthTrackBar);
            this.Controls.Add(this.sizeTrackBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sizeTextBox);
            this.Controls.Add(this.borderTextBox);
            this.Controls.Add(this.borderTrackBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.borderTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxDepthTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar borderTrackBar;
        private System.Windows.Forms.TextBox borderTextBox;
        private System.Windows.Forms.TextBox sizeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar sizeTrackBar;
        private System.Windows.Forms.TrackBar maxDepthTrackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox maxDepthTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox presetDdl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox viewportLeftTxt;
        private System.Windows.Forms.TextBox viewportTopTxt;
        private System.Windows.Forms.TextBox viewportScaleTxt;
    }
}

