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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btnCloseWindow = new System.Windows.Forms.Button();
            this.chkTRWeaponDamage = new System.Windows.Forms.CheckBox();
            this.chkTRWeaponScaling = new System.Windows.Forms.CheckBox();
            this.chkTRWeaponStamina = new System.Windows.Forms.CheckBox();
            this.chkTRWeaponWeight = new System.Windows.Forms.CheckBox();
            this.chkTRWeaponStatMin = new System.Windows.Forms.CheckBox();
            this.chkTRWeaponDefense = new System.Windows.Forms.CheckBox();
            this.chkTRArmorResistance = new System.Windows.Forms.CheckBox();
            this.chkTRArmorPoise = new System.Windows.Forms.CheckBox();
            this.chkTRArmorWeight = new System.Windows.Forms.CheckBox();
            this.chkTRSpellRequirements = new System.Windows.Forms.CheckBox();
            this.chkTRSpellSlotSize = new System.Windows.Forms.CheckBox();
            this.chkTRSpellQuantity = new System.Windows.Forms.CheckBox();
            this.chkTRCamera = new System.Windows.Forms.CheckBox();
            this.gbWeaponCategory = new System.Windows.Forms.GroupBox();
            this.gbArmorCategory = new System.Windows.Forms.GroupBox();
            this.gbSpellCategory = new System.Windows.Forms.GroupBox();
            this.gbOtherCategory = new System.Windows.Forms.GroupBox();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnCloseWindow
            // 
            this.btnCloseWindow.Location = new System.Drawing.Point(413, 277);
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
            this.chkTRWeaponDamage.Size = new System.Drawing.Size(141, 17);
            this.chkTRWeaponDamage.TabIndex = 1;
            this.chkTRWeaponDamage.Text = "Weapon damage DRBS";
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
            this.chkTRWeaponScaling.Size = new System.Drawing.Size(136, 17);
            this.chkTRWeaponScaling.TabIndex = 1;
            this.chkTRWeaponScaling.Text = "Weapon scaling DRBS";
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
            this.chkTRWeaponStamina.Size = new System.Drawing.Size(139, 17);
            this.chkTRWeaponStamina.TabIndex = 1;
            this.chkTRWeaponStamina.Text = "Weapon stamina DRBS";
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
            this.chkTRWeaponWeight.Size = new System.Drawing.Size(134, 17);
            this.chkTRWeaponWeight.TabIndex = 1;
            this.chkTRWeaponWeight.Text = "Weapon weight DRBS";
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
            this.chkTRWeaponStatMin.Size = new System.Drawing.Size(168, 17);
            this.chkTRWeaponStatMin.TabIndex = 1;
            this.chkTRWeaponStatMin.Text = "Weapon minimum stats DRBS";
            this.tooltip.SetToolTip(this.chkTRWeaponStatMin, "Randomize weapon\'s minimum stats by generating a random number instead of shuffli" +
        "ng vanilla values.\nDon\'t Randomize by Shuffle needs to be on for this to functio" +
        "n.");
            this.chkTRWeaponStatMin.UseVisualStyleBackColor = true;
            // 
            // chkTRWeaponDefense
            // 
            this.chkTRWeaponDefense.AutoSize = true;
            this.chkTRWeaponDefense.Checked = true;
            this.chkTRWeaponDefense.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRWeaponDefense.Location = new System.Drawing.Point(273, 84);
            this.chkTRWeaponDefense.Name = "chkTRWeaponDefense";
            this.chkTRWeaponDefense.Size = new System.Drawing.Size(141, 17);
            this.chkTRWeaponDefense.TabIndex = 1;
            this.chkTRWeaponDefense.Text = "Weapon defense DRBS";
            this.tooltip.SetToolTip(this.chkTRWeaponDefense, "Randomize weapon\'s defense values by generating random numbers instead of shuffli" +
        "ng vanilla values.\nSupports weapon shield split.\nDon\'t Randomize by Shuffle need" +
        "s to be on for this to function.");
            this.chkTRWeaponDefense.UseVisualStyleBackColor = true;
            // 
            // chkTRArmorResistance
            // 
            this.chkTRArmorResistance.AutoSize = true;
            this.chkTRArmorResistance.Checked = true;
            this.chkTRArmorResistance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRArmorResistance.Location = new System.Drawing.Point(36, 126);
            this.chkTRArmorResistance.Name = "chkTRArmorResistance";
            this.chkTRArmorResistance.Size = new System.Drawing.Size(137, 17);
            this.chkTRArmorResistance.TabIndex = 1;
            this.chkTRArmorResistance.Text = "Armor resistance DRBS";
            this.tooltip.SetToolTip(this.chkTRArmorResistance, "Randomize armor\'s defense values by generating random numbers instead of shufflin" +
        "g vanilla values.\nDon\'t Randomize by Shuffle needs to be on for this to function" +
        ".");
            this.chkTRArmorResistance.UseVisualStyleBackColor = true;
            // 
            // chkTRArmorPoise
            // 
            this.chkTRArmorPoise.AutoSize = true;
            this.chkTRArmorPoise.Checked = true;
            this.chkTRArmorPoise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRArmorPoise.Location = new System.Drawing.Point(273, 126);
            this.chkTRArmorPoise.Name = "chkTRArmorPoise";
            this.chkTRArmorPoise.Size = new System.Drawing.Size(114, 17);
            this.chkTRArmorPoise.TabIndex = 1;
            this.chkTRArmorPoise.Text = "Armor poise DRBS";
            this.tooltip.SetToolTip(this.chkTRArmorPoise, "Randomize armor\'s poise values by generating random numbers instead of shuffling " +
        "vanilla values.\nDon\'t Randomize by Shuffle needs to be on for this to function.");
            this.chkTRArmorPoise.UseVisualStyleBackColor = true;
            // 
            // chkTRArmorWeight
            // 
            this.chkTRArmorWeight.AutoSize = true;
            this.chkTRArmorWeight.Checked = true;
            this.chkTRArmorWeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRArmorWeight.Location = new System.Drawing.Point(36, 149);
            this.chkTRArmorWeight.Name = "chkTRArmorWeight";
            this.chkTRArmorWeight.Size = new System.Drawing.Size(120, 17);
            this.chkTRArmorWeight.TabIndex = 1;
            this.chkTRArmorWeight.Text = "Armor weight DRBS";
            this.tooltip.SetToolTip(this.chkTRArmorWeight, "Randomize armor\'s weight values by generating random numbers instead of shuffling" +
        " vanilla values.\nDon\'t Randomize by Shuffle needs to be on for this to function." +
        "");
            this.chkTRArmorWeight.UseVisualStyleBackColor = true;
            // 
            // chkTRSpellRequirements
            // 
            this.chkTRSpellRequirements.AutoSize = true;
            this.chkTRSpellRequirements.Checked = true;
            this.chkTRSpellRequirements.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRSpellRequirements.Location = new System.Drawing.Point(36, 191);
            this.chkTRSpellRequirements.Name = "chkTRSpellRequirements";
            this.chkTRSpellRequirements.Size = new System.Drawing.Size(145, 17);
            this.chkTRSpellRequirements.TabIndex = 1;
            this.chkTRSpellRequirements.Text = "Spell requirements DRBS";
            this.tooltip.SetToolTip(this.chkTRSpellRequirements, "Randomize spell requirements by generating random numbers instead of shuffling va" +
        "nilla values.\nDon\'t Randomize by Shuffle needs to be on for this to function.");
            this.chkTRSpellRequirements.UseVisualStyleBackColor = true;
            // 
            // chkTRSpellSlotSize
            // 
            this.chkTRSpellSlotSize.AutoSize = true;
            this.chkTRSpellSlotSize.Checked = true;
            this.chkTRSpellSlotSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRSpellSlotSize.Location = new System.Drawing.Point(273, 191);
            this.chkTRSpellSlotSize.Name = "chkTRSpellSlotSize";
            this.chkTRSpellSlotSize.Size = new System.Drawing.Size(122, 17);
            this.chkTRSpellSlotSize.TabIndex = 1;
            this.chkTRSpellSlotSize.Text = "Spell slot size DRBS";
            this.tooltip.SetToolTip(this.chkTRSpellSlotSize, resources.GetString("chkTRSpellSlotSize.ToolTip"));
            this.chkTRSpellSlotSize.UseVisualStyleBackColor = true;
            // 
            // chkTRSpellQuantity
            // 
            this.chkTRSpellQuantity.AutoSize = true;
            this.chkTRSpellQuantity.Checked = true;
            this.chkTRSpellQuantity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRSpellQuantity.Location = new System.Drawing.Point(36, 214);
            this.chkTRSpellQuantity.Name = "chkTRSpellQuantity";
            this.chkTRSpellQuantity.Size = new System.Drawing.Size(122, 17);
            this.chkTRSpellQuantity.TabIndex = 1;
            this.chkTRSpellQuantity.Text = "Spell quantity DRBS";
            this.tooltip.SetToolTip(this.chkTRSpellQuantity, "Randomize spell quantity by generating random numbers instead of shuffling vanill" +
        "a values.\nDon\'t Randomize by Shuffle needs to be on for this to function.");
            this.chkTRSpellQuantity.UseVisualStyleBackColor = true;
            // 
            // chkTRCamera
            // 
            this.chkTRCamera.AutoSize = true;
            this.chkTRCamera.Checked = true;
            this.chkTRCamera.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRCamera.Location = new System.Drawing.Point(36, 254);
            this.chkTRCamera.Name = "chkTRCamera";
            this.chkTRCamera.Size = new System.Drawing.Size(95, 17);
            this.chkTRCamera.TabIndex = 1;
            this.chkTRCamera.Text = "Camera DRBS";
            this.tooltip.SetToolTip(this.chkTRCamera, "Randomize camera parameters by generating random numbers instead of shuffling van" +
        "illa values.\nDon\'t Randomize by Shuffle needs to be on for this to function.");
            this.chkTRCamera.UseVisualStyleBackColor = true;
            // 
            // gbWeaponCategory
            // 
            this.gbWeaponCategory.AutoSize = true;
            this.gbWeaponCategory.Location = new System.Drawing.Point(12, 12);
            this.gbWeaponCategory.Name = "gbWeaponCategory";
            this.gbWeaponCategory.Size = new System.Drawing.Size(450, 89);
            this.gbWeaponCategory.TabIndex = 2;
            this.gbWeaponCategory.TabStop = false;
            this.gbWeaponCategory.Text = "Weapon Settings:";
            // 
            // gbArmorCategory
            // 
            this.gbArmorCategory.AutoSize = true;
            this.gbArmorCategory.Location = new System.Drawing.Point(12, 107);
            this.gbArmorCategory.Name = "gbArmorCategory";
            this.gbArmorCategory.Size = new System.Drawing.Size(450, 59);
            this.gbArmorCategory.TabIndex = 2;
            this.gbArmorCategory.TabStop = false;
            this.gbArmorCategory.Text = "Armor Settings:";
            // 
            // gbSpellCategory
            // 
            this.gbSpellCategory.AutoSize = true;
            this.gbSpellCategory.Location = new System.Drawing.Point(12, 172);
            this.gbSpellCategory.Name = "gbSpellCategory";
            this.gbSpellCategory.Size = new System.Drawing.Size(450, 57);
            this.gbSpellCategory.TabIndex = 2;
            this.gbSpellCategory.TabStop = false;
            this.gbSpellCategory.Text = "Spell Settings:";
            // 
            // gbOtherCategory
            // 
            this.gbOtherCategory.AutoSize = true;
            this.gbOtherCategory.Location = new System.Drawing.Point(12, 235);
            this.gbOtherCategory.Name = "gbOtherCategory";
            this.gbOtherCategory.Size = new System.Drawing.Size(450, 36);
            this.gbOtherCategory.TabIndex = 2;
            this.gbOtherCategory.TabStop = false;
            this.gbOtherCategory.Text = "Other Settings:";
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
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.btnCloseWindow);
            this.Controls.Add(this.chkTRWeaponDamage);
            this.Controls.Add(this.chkTRWeaponScaling);
            this.Controls.Add(this.chkTRWeaponStamina);
            this.Controls.Add(this.chkTRWeaponWeight);
            this.Controls.Add(this.chkTRWeaponStatMin);
            this.Controls.Add(this.chkTRWeaponDefense);
            this.Controls.Add(this.chkTRArmorResistance);
            this.Controls.Add(this.chkTRArmorPoise);
            this.Controls.Add(this.chkTRArmorWeight);
            this.Controls.Add(this.chkTRSpellRequirements);
            this.Controls.Add(this.chkTRSpellSlotSize);
            this.Controls.Add(this.chkTRSpellQuantity);
            this.Controls.Add(this.chkTRCamera);
            this.Controls.Add(this.gbWeaponCategory);
            this.Controls.Add(this.gbArmorCategory);
            this.Controls.Add(this.gbSpellCategory);
            this.Controls.Add(this.gbOtherCategory);
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
        public System.Windows.Forms.CheckBox chkTRWeaponDefense;
        public System.Windows.Forms.CheckBox chkTRArmorResistance;
        public System.Windows.Forms.CheckBox chkTRArmorPoise;
        public System.Windows.Forms.CheckBox chkTRArmorWeight;
        public System.Windows.Forms.CheckBox chkTRSpellRequirements;
        public System.Windows.Forms.CheckBox chkTRSpellSlotSize;
        public System.Windows.Forms.CheckBox chkTRSpellQuantity;
        public System.Windows.Forms.CheckBox chkTRCamera;
        public System.Windows.Forms.GroupBox gbWeaponCategory;
        public System.Windows.Forms.GroupBox gbArmorCategory;
        public System.Windows.Forms.GroupBox gbSpellCategory;
        public System.Windows.Forms.GroupBox gbOtherCategory;
        //public System.Windows.Forms.GroupBox gbEnemyCategory;
        //public System.Windows.Forms.GroupBox gbNPCPCCategory;
        public System.Windows.Forms.ToolTip tooltip;
    }
}