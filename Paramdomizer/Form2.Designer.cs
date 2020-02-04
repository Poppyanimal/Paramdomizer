namespace Paramdomizer
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.btnCloseWindow = new System.Windows.Forms.Button();
            this.chkTRWeaponDamage = new System.Windows.Forms.CheckBox();
            this.chkTRWeaponScaling = new System.Windows.Forms.CheckBox();
            this.chkTRWeaponStamina = new System.Windows.Forms.CheckBox();
            this.chkTRWeaponWeight = new System.Windows.Forms.CheckBox();
            this.chkTRWeaponStatMin = new System.Windows.Forms.CheckBox();
            this.gbWeaponCategory = new System.Windows.Forms.GroupBox();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnCloseWindow
            // 
            this.btnCloseWindow.Location = new System.Drawing.Point(413, 115);
            this.btnCloseWindow.Name = "btnCloseWindow";
            this.btnCloseWindow.Size = new System.Drawing.Size(75, 23);
            this.btnCloseWindow.TabIndex = 0;
            this.btnCloseWindow.Text = "Close Window";
            this.btnCloseWindow.UseVisualStyleBackColor = true;
            this.btnCloseWindow.Click += new System.EventHandler(this.btnCloseWindow_Click);
            // 
            // chkTRWeaponDamage
            // 
            this.chkTRWeaponDamage.AutoSize = true;
            this.chkTRWeaponDamage.Checked = true;
            this.chkTRWeaponDamage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRWeaponDamage.Location = new System.Drawing.Point(36, 38);
            this.chkTRWeaponDamage.Name = "chkTRWeaponDamage";
            this.chkTRWeaponDamage.Size = new System.Drawing.Size(138, 17);
            this.chkTRWeaponDamage.TabIndex = 1;
            this.chkTRWeaponDamage.Text = "weapon damage DRBS";
            this.tooltip.SetToolTip(this.chkTRWeaponDamage, "Randomize weapon damage by generating a random number instead of shuffling vanill" +
        "a values.\nDon\'t Randomize by Shuffle needs to be on for this to function.");
            this.chkTRWeaponDamage.UseVisualStyleBackColor = true;
            // 
            // chkTRWeaponScaling
            // 
            this.chkTRWeaponScaling.AutoSize = true;
            this.chkTRWeaponScaling.Checked = true;
            this.chkTRWeaponScaling.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRWeaponScaling.Location = new System.Drawing.Point(36, 61);
            this.chkTRWeaponScaling.Name = "chkTRWeaponScaling";
            this.chkTRWeaponScaling.Size = new System.Drawing.Size(133, 17);
            this.chkTRWeaponScaling.TabIndex = 1;
            this.chkTRWeaponScaling.Text = "weapon scaling DRBS";
            this.tooltip.SetToolTip(this.chkTRWeaponScaling, "Randomize weapon scaling by generating a random number instead of shuffling vanil" +
        "la values.\nDon\'t Randomize by Shuffle needs to be on for this to function.");
            this.chkTRWeaponScaling.UseVisualStyleBackColor = true;
            // 
            // chkTRWeaponStamina
            // 
            this.chkTRWeaponStamina.AutoSize = true;
            this.chkTRWeaponStamina.Checked = true;
            this.chkTRWeaponStamina.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRWeaponStamina.Location = new System.Drawing.Point(273, 61);
            this.chkTRWeaponStamina.Name = "chkTRWeaponStamina";
            this.chkTRWeaponStamina.Size = new System.Drawing.Size(136, 17);
            this.chkTRWeaponStamina.TabIndex = 1;
            this.chkTRWeaponStamina.Text = "weapon stamina DRBS";
            this.tooltip.SetToolTip(this.chkTRWeaponStamina, "Randomize weapon stamina by generating a random number instead of shuffling vanil" +
        "la values.\nDon\'t Randomize by Shuffle needs to be on for this to function.");
            this.chkTRWeaponStamina.UseVisualStyleBackColor = true;
            // 
            // chkTRWeaponWeight
            // 
            this.chkTRWeaponWeight.AutoSize = true;
            this.chkTRWeaponWeight.Checked = true;
            this.chkTRWeaponWeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRWeaponWeight.Location = new System.Drawing.Point(273, 38);
            this.chkTRWeaponWeight.Name = "chkTRWeaponWeight";
            this.chkTRWeaponWeight.Size = new System.Drawing.Size(131, 17);
            this.chkTRWeaponWeight.TabIndex = 1;
            this.chkTRWeaponWeight.Text = "weapon weight DRBS";
            this.tooltip.SetToolTip(this.chkTRWeaponWeight, "Randomize weapon weight by generating a random number instead of shuffling vanill" +
        "a values.\nDon\'t Randomize by Shuffle needs to be on for this to function.");
            this.chkTRWeaponWeight.UseVisualStyleBackColor = true;
            // 
            // chkTRWeaponStatMin
            // 
            this.chkTRWeaponStatMin.AutoSize = true;
            this.chkTRWeaponStatMin.Checked = true;
            this.chkTRWeaponStatMin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRWeaponStatMin.Location = new System.Drawing.Point(36, 84);
            this.chkTRWeaponStatMin.Name = "chkTRWeaponStatMin";
            this.chkTRWeaponStatMin.Size = new System.Drawing.Size(165, 17);
            this.chkTRWeaponStatMin.TabIndex = 1;
            this.chkTRWeaponStatMin.Text = "weapon minimum stats DRBS";
            this.tooltip.SetToolTip(this.chkTRWeaponStatMin, "Randomize weapon\'s minimum stats by generating a random number instead of shuffli" +
        "ng vanilla values.\nDon\'t Randomize by Shuffle needs to be on for this to functio" +
        "n.");
            this.chkTRWeaponStatMin.UseVisualStyleBackColor = true;
            // 
            // gbWeaponCategory
            // 
            this.gbWeaponCategory.AutoSize = true;
            this.gbWeaponCategory.Location = new System.Drawing.Point(12, 12);
            this.gbWeaponCategory.Name = "gbWeaponCategory";
            this.gbWeaponCategory.Size = new System.Drawing.Size(432, 89);
            this.gbWeaponCategory.TabIndex = 2;
            this.gbWeaponCategory.TabStop = false;
            this.gbWeaponCategory.Text = "Weapon Settings:";
            // 
            // tooltip
            // 
            this.tooltip.AutoPopDelay = 32767;
            this.tooltip.InitialDelay = 500;
            this.tooltip.ReshowDelay = 100;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 150);
            this.Controls.Add(this.btnCloseWindow);
            this.Controls.Add(this.chkTRWeaponDamage);
            this.Controls.Add(this.chkTRWeaponScaling);
            this.Controls.Add(this.chkTRWeaponStamina);
            this.Controls.Add(this.chkTRWeaponWeight);
            this.Controls.Add(this.chkTRWeaponStatMin);
            this.Controls.Add(this.gbWeaponCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.Text = "Paramdomizer - Don\'t Randomize By Shuffle Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btnCloseWindow;
        public System.Windows.Forms.CheckBox chkTRWeaponDamage;
        public System.Windows.Forms.CheckBox chkTRWeaponScaling;
        public System.Windows.Forms.CheckBox chkTRWeaponWeight;
        public System.Windows.Forms.CheckBox chkTRWeaponStamina;
        public System.Windows.Forms.CheckBox chkTRWeaponStatMin;
        public System.Windows.Forms.GroupBox gbWeaponCategory;
        public System.Windows.Forms.ToolTip tooltip;
    }
}