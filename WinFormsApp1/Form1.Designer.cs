namespace PerlinDemoForms
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.debug = new System.Windows.Forms.Label();
            this.buttonEnergyEffect = new System.Windows.Forms.Button();
            this.buttonWindEffect = new System.Windows.Forms.Button();
            this.modeLabel = new System.Windows.Forms.Label();
            this.buttonMountainsEffect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(12, 44);
            this.canvas.Margin = new System.Windows.Forms.Padding(0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(480, 320);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 16;
            this.timer1.Tag = "timer1";
            this.timer1.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // debug
            // 
            this.debug.AutoSize = true;
            this.debug.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.debug.Location = new System.Drawing.Point(451, 416);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(0, 20);
            this.debug.TabIndex = 1;
            // 
            // buttonEnergyEffect
            // 
            this.buttonEnergyEffect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(74)))), ((int)(((byte)(84)))));
            this.buttonEnergyEffect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnergyEffect.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonEnergyEffect.Location = new System.Drawing.Point(12, 12);
            this.buttonEnergyEffect.Name = "buttonEnergyEffect";
            this.buttonEnergyEffect.Size = new System.Drawing.Size(154, 29);
            this.buttonEnergyEffect.TabIndex = 2;
            this.buttonEnergyEffect.Text = "Mode: Energy Effect";
            this.buttonEnergyEffect.UseVisualStyleBackColor = false;
            this.buttonEnergyEffect.Click += new System.EventHandler(this.buttonEnergyEffect_Click);
            // 
            // buttonWindEffect
            // 
            this.buttonWindEffect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(74)))), ((int)(((byte)(84)))));
            this.buttonWindEffect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWindEffect.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonWindEffect.Location = new System.Drawing.Point(172, 12);
            this.buttonWindEffect.Name = "buttonWindEffect";
            this.buttonWindEffect.Size = new System.Drawing.Size(143, 29);
            this.buttonWindEffect.TabIndex = 3;
            this.buttonWindEffect.Text = "Mode: Wind Effect";
            this.buttonWindEffect.UseVisualStyleBackColor = false;
            this.buttonWindEffect.Click += new System.EventHandler(this.buttonWindEffect_Click);
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.modeLabel.Location = new System.Drawing.Point(12, 375);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(143, 20);
            this.modeLabel.TabIndex = 4;
            this.modeLabel.Text = "Current Mode: None";
            // 
            // buttonMountainsEffect
            // 
            this.buttonMountainsEffect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(74)))), ((int)(((byte)(84)))));
            this.buttonMountainsEffect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMountainsEffect.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonMountainsEffect.Location = new System.Drawing.Point(321, 12);
            this.buttonMountainsEffect.Name = "buttonMountainsEffect";
            this.buttonMountainsEffect.Size = new System.Drawing.Size(171, 29);
            this.buttonMountainsEffect.TabIndex = 5;
            this.buttonMountainsEffect.Text = "Mode: Mountain Effect";
            this.buttonMountainsEffect.UseVisualStyleBackColor = false;
            this.buttonMountainsEffect.Click += new System.EventHandler(this.buttonMountainsEffect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(34)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(942, 593);
            this.Controls.Add(this.buttonMountainsEffect);
            this.Controls.Add(this.modeLabel);
            this.Controls.Add(this.buttonWindEffect);
            this.Controls.Add(this.buttonEnergyEffect);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.canvas);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(960, 640);
            this.MinimumSize = new System.Drawing.Size(960, 640);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox canvas;
        private System.Windows.Forms.Timer timer1;
        private Label debug;
        private Button buttonEnergyEffect;
        private Button buttonWindEffect;
        private Label modeLabel;
        private Button buttonMountainsEffect;
    }
}