namespace Paramdomizer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtSeed = new System.Windows.Forms.TextBox();
            this.lblSeed = new System.Windows.Forms.Label();
            this.lblGamePath = new System.Windows.Forms.Label();
            this.txtGamePath = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.chkRingSpeffects = new System.Windows.Forms.CheckBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.chkWeaponModels = new System.Windows.Forms.CheckBox();
            this.chkWeaponDamage = new System.Windows.Forms.CheckBox();
            this.chkWeaponMoveset = new System.Windows.Forms.CheckBox();
            this.chkBullets = new System.Windows.Forms.CheckBox();
            this.chkKnockback = new System.Windows.Forms.CheckBox();
            this.chkSpeffects = new System.Windows.Forms.CheckBox();
            this.chkAttackSpeffects = new System.Windows.Forms.CheckBox();
            this.chkVoices = new System.Windows.Forms.CheckBox();
            this.chkTurnSpeeds = new System.Windows.Forms.CheckBox();
            this.chkHitboxSizes = new System.Windows.Forms.CheckBox();
            this.chkStaggerLevels = new System.Windows.Forms.CheckBox();
            this.chkAggroRadius = new System.Windows.Forms.CheckBox();
            this.chkItemAnimations = new System.Windows.Forms.CheckBox();
            this.chkMagicAnimations = new System.Windows.Forms.CheckBox();
            this.btnOpenFolderDialog = new System.Windows.Forms.Button();
            this.chkRandomFaceData = new System.Windows.Forms.CheckBox();
            this.checkBoxRemaster = new System.Windows.Forms.CheckBox();
            this.checkBoxWeaponScaling = new System.Windows.Forms.CheckBox();
            this.checkBoxWeaponStatMin = new System.Windows.Forms.CheckBox();
            this.checkBoxWeaponWeight = new System.Windows.Forms.CheckBox();
            this.checkBoxDoTrueRandom = new System.Windows.Forms.CheckBox();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // txtSeed
            // 
            this.txtSeed.Location = new System.Drawing.Point(77, 32);
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(362, 20);
            this.txtSeed.TabIndex = 1;
            // 
            // lblSeed
            // 
            this.lblSeed.AutoSize = true;
            this.lblSeed.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblSeed.Location = new System.Drawing.Point(36, 35);
            this.lblSeed.Name = "lblSeed";
            this.lblSeed.Size = new System.Drawing.Size(35, 13);
            this.lblSeed.TabIndex = 1;
            this.lblSeed.Text = "Seed:";
            // 
            // lblGamePath
            // 
            this.lblGamePath.AutoSize = true;
            this.lblGamePath.Location = new System.Drawing.Point(8, 9);
            this.lblGamePath.Name = "lblGamePath";
            this.lblGamePath.Size = new System.Drawing.Size(63, 13);
            this.lblGamePath.TabIndex = 2;
            this.lblGamePath.Text = "Game Path:";
            // 
            // txtGamePath
            // 
            this.txtGamePath.Location = new System.Drawing.Point(77, 6);
            this.txtGamePath.Name = "txtGamePath";
            this.txtGamePath.Size = new System.Drawing.Size(328, 20);
            this.txtGamePath.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(358, 298);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 20;
            this.btnSubmit.Text = "Go";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // chkRingSpeffects
            // 
            this.chkRingSpeffects.AutoSize = true;
            this.chkRingSpeffects.Checked = true;
            this.chkRingSpeffects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRingSpeffects.Location = new System.Drawing.Point(11, 222);
            this.chkRingSpeffects.Name = "chkRingSpeffects";
            this.chkRingSpeffects.Size = new System.Drawing.Size(168, 17);
            this.chkRingSpeffects.TabIndex = 7;
            this.chkRingSpeffects.Text = "Randomize SPeffects on rings";
            this.tooltip.SetToolTip(this.chkRingSpeffects, "Randomizes special effects of rings. Not effected by DoTrueRandom.");
            this.chkRingSpeffects.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(193, 288);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(159, 43);
            this.lblMessage.TabIndex = 6;
            this.lblMessage.Text = "label1";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMessage.Visible = false;
            // 
            // chkWeaponModels
            // 
            this.chkWeaponModels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWeaponModels.AutoSize = true;
            this.chkWeaponModels.Checked = true;
            this.chkWeaponModels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeaponModels.Location = new System.Drawing.Point(265, 108);
            this.chkWeaponModels.Name = "chkWeaponModels";
            this.chkWeaponModels.Size = new System.Drawing.Size(156, 17);
            this.chkWeaponModels.TabIndex = 13;
            this.chkWeaponModels.Text = "Randomize weapon models";
            this.tooltip.SetToolTip(this.chkWeaponModels, "Randomizes weapon models. Not effected by DoTrueRandom.");
            this.chkWeaponModels.UseVisualStyleBackColor = true;
            // 
            // chkWeaponDamage
            // 
            this.chkWeaponDamage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWeaponDamage.AutoSize = true;
            this.chkWeaponDamage.Checked = true;
            this.chkWeaponDamage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeaponDamage.Location = new System.Drawing.Point(265, 86);
            this.chkWeaponDamage.Name = "chkWeaponDamage";
            this.chkWeaponDamage.Size = new System.Drawing.Size(161, 17);
            this.chkWeaponDamage.TabIndex = 12;
            this.chkWeaponDamage.Text = "Randomize weapon damage";
            this.tooltip.SetToolTip(this.chkWeaponDamage, "Randomizes weapon damage. Effected by DoTrueRandom.");
            this.chkWeaponDamage.UseVisualStyleBackColor = true;
            // 
            // chkWeaponMoveset
            // 
            this.chkWeaponMoveset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWeaponMoveset.AutoSize = true;
            this.chkWeaponMoveset.Checked = true;
            this.chkWeaponMoveset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeaponMoveset.Location = new System.Drawing.Point(265, 64);
            this.chkWeaponMoveset.Name = "chkWeaponMoveset";
            this.chkWeaponMoveset.Size = new System.Drawing.Size(168, 17);
            this.chkWeaponMoveset.TabIndex = 11;
            this.chkWeaponMoveset.Text = "Randomize weapon movesets";
            this.tooltip.SetToolTip(this.chkWeaponMoveset, "Randomizes weapon movesets. Not effected by DoTrueRandom.");
            this.chkWeaponMoveset.UseVisualStyleBackColor = true;
            // 
            // chkBullets
            // 
            this.chkBullets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBullets.AutoSize = true;
            this.chkBullets.Checked = true;
            this.chkBullets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBullets.Location = new System.Drawing.Point(265, 175);
            this.chkBullets.Name = "chkBullets";
            this.chkBullets.Size = new System.Drawing.Size(112, 17);
            this.chkBullets.TabIndex = 9;
            this.chkBullets.Text = "Randomize bullets";
            this.tooltip.SetToolTip(this.chkBullets, "Randomizes bullets. Not effected by DoTrueRandom.");
            this.chkBullets.UseVisualStyleBackColor = true;
            // 
            // chkKnockback
            // 
            this.chkKnockback.AutoSize = true;
            this.chkKnockback.Checked = true;
            this.chkKnockback.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKnockback.Location = new System.Drawing.Point(12, 109);
            this.chkKnockback.Name = "chkKnockback";
            this.chkKnockback.Size = new System.Drawing.Size(169, 17);
            this.chkKnockback.TabIndex = 16;
            this.chkKnockback.Text = "Randomize attack knockback";
            this.tooltip.SetToolTip(this.chkKnockback, "Randomizes attack knockback. Not effected by DoTrueRandom.");
            this.chkKnockback.UseVisualStyleBackColor = true;
            // 
            // chkSpeffects
            // 
            this.chkSpeffects.AutoSize = true;
            this.chkSpeffects.Checked = true;
            this.chkSpeffects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpeffects.Location = new System.Drawing.Point(11, 177);
            this.chkSpeffects.Name = "chkSpeffects";
            this.chkSpeffects.Size = new System.Drawing.Size(185, 17);
            this.chkSpeffects.TabIndex = 5;
            this.chkSpeffects.Text = "Randomize SPeffects on enemies";
            this.tooltip.SetToolTip(this.chkSpeffects, "Randomizes special effects on enemies. Not effected by DoTrueRandom.");
            this.chkSpeffects.UseVisualStyleBackColor = true;
            // 
            // chkAttackSpeffects
            // 
            this.chkAttackSpeffects.AutoSize = true;
            this.chkAttackSpeffects.Checked = true;
            this.chkAttackSpeffects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAttackSpeffects.Location = new System.Drawing.Point(11, 199);
            this.chkAttackSpeffects.Name = "chkAttackSpeffects";
            this.chkAttackSpeffects.Size = new System.Drawing.Size(181, 17);
            this.chkAttackSpeffects.TabIndex = 6;
            this.chkAttackSpeffects.Text = "Randomize SPeffects on attacks";
            this.tooltip.SetToolTip(this.chkAttackSpeffects, "Randomizes special effects on attacks. Not effected by DoTrueRandom.");
            this.chkAttackSpeffects.UseVisualStyleBackColor = true;
            // 
            // chkVoices
            // 
            this.chkVoices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkVoices.AutoSize = true;
            this.chkVoices.Checked = true;
            this.chkVoices.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVoices.Location = new System.Drawing.Point(265, 199);
            this.chkVoices.Name = "chkVoices";
            this.chkVoices.Size = new System.Drawing.Size(132, 17);
            this.chkVoices.TabIndex = 10;
            this.chkVoices.Text = "Randomize voice lines";
            this.tooltip.SetToolTip(this.chkVoices, "Randomizes voice lines. Not effected by DoTrueRandom.");
            this.chkVoices.UseVisualStyleBackColor = true;
            // 
            // chkTurnSpeeds
            // 
            this.chkTurnSpeeds.AutoSize = true;
            this.chkTurnSpeeds.Checked = true;
            this.chkTurnSpeeds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTurnSpeeds.Location = new System.Drawing.Point(12, 86);
            this.chkTurnSpeeds.Name = "chkTurnSpeeds";
            this.chkTurnSpeeds.Size = new System.Drawing.Size(166, 17);
            this.chkTurnSpeeds.TabIndex = 3;
            this.chkTurnSpeeds.Text = "Randomize enemy turn speed";
            this.tooltip.SetToolTip(this.chkTurnSpeeds, "Randomizes enemy turn speed. Not effected by DoTrueRandom.");
            this.chkTurnSpeeds.UseVisualStyleBackColor = true;
            // 
            // chkHitboxSizes
            // 
            this.chkHitboxSizes.AutoSize = true;
            this.chkHitboxSizes.Checked = true;
            this.chkHitboxSizes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHitboxSizes.Location = new System.Drawing.Point(12, 154);
            this.chkHitboxSizes.Name = "chkHitboxSizes";
            this.chkHitboxSizes.Size = new System.Drawing.Size(136, 17);
            this.chkHitboxSizes.TabIndex = 18;
            this.chkHitboxSizes.Text = "Randomize hitbox sizes";
            this.tooltip.SetToolTip(this.chkHitboxSizes, "Randomizes hitbox sizes. Not effected by DoTrueRandom.");
            this.chkHitboxSizes.UseVisualStyleBackColor = true;
            // 
            // chkStaggerLevels
            // 
            this.chkStaggerLevels.AutoSize = true;
            this.chkStaggerLevels.Checked = true;
            this.chkStaggerLevels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStaggerLevels.Location = new System.Drawing.Point(12, 131);
            this.chkStaggerLevels.Name = "chkStaggerLevels";
            this.chkStaggerLevels.Size = new System.Drawing.Size(147, 17);
            this.chkStaggerLevels.TabIndex = 17;
            this.chkStaggerLevels.Text = "Randomize stagger levels";
            this.tooltip.SetToolTip(this.chkStaggerLevels, "Randomizes stagger levels. Not effected by DoTrueRandom.");
            this.chkStaggerLevels.UseVisualStyleBackColor = true;
            // 
            // chkAggroRadius
            // 
            this.chkAggroRadius.AutoSize = true;
            this.chkAggroRadius.Checked = true;
            this.chkAggroRadius.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAggroRadius.Location = new System.Drawing.Point(12, 64);
            this.chkAggroRadius.Name = "chkAggroRadius";
            this.chkAggroRadius.Size = new System.Drawing.Size(140, 17);
            this.chkAggroRadius.TabIndex = 2;
            this.chkAggroRadius.Text = "Randomize aggro radius";
            this.tooltip.SetToolTip(this.chkAggroRadius, "Randomizes the aggro range of enemies. Not effected by DoTrueRandom.");
            this.chkAggroRadius.UseVisualStyleBackColor = true;
            // 
            // chkItemAnimations
            // 
            this.chkItemAnimations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkItemAnimations.AutoSize = true;
            this.chkItemAnimations.Checked = true;
            this.chkItemAnimations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkItemAnimations.Location = new System.Drawing.Point(265, 130);
            this.chkItemAnimations.Name = "chkItemAnimations";
            this.chkItemAnimations.Size = new System.Drawing.Size(154, 17);
            this.chkItemAnimations.TabIndex = 14;
            this.chkItemAnimations.Text = "Randomize item animations";
            this.tooltip.SetToolTip(this.chkItemAnimations, "Randomizes the animations of items. Not effected by DoTrueRandom.");
            this.chkItemAnimations.UseVisualStyleBackColor = true;
            // 
            // chkMagicAnimations
            // 
            this.chkMagicAnimations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMagicAnimations.AutoSize = true;
            this.chkMagicAnimations.Checked = true;
            this.chkMagicAnimations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMagicAnimations.Location = new System.Drawing.Point(265, 152);
            this.chkMagicAnimations.Name = "chkMagicAnimations";
            this.chkMagicAnimations.Size = new System.Drawing.Size(163, 17);
            this.chkMagicAnimations.TabIndex = 15;
            this.chkMagicAnimations.Text = "Randomize magic animations";
            this.tooltip.SetToolTip(this.chkMagicAnimations, "Randomizes the animations of magic. Not effected by DoTrueRandom.");
            this.chkMagicAnimations.UseVisualStyleBackColor = true;
            // 
            // btnOpenFolderDialog
            // 
            this.btnOpenFolderDialog.Location = new System.Drawing.Point(411, 6);
            this.btnOpenFolderDialog.Name = "btnOpenFolderDialog";
            this.btnOpenFolderDialog.Size = new System.Drawing.Size(28, 20);
            this.btnOpenFolderDialog.TabIndex = 21;
            this.btnOpenFolderDialog.Text = "...";
            this.tooltip.SetToolTip(this.btnOpenFolderDialog, "Open Folder");
            this.btnOpenFolderDialog.UseVisualStyleBackColor = true;
            this.btnOpenFolderDialog.Click += new System.EventHandler(this.btnOpenFolderDialog_Click);
            // 
            // chkRandomFaceData
            // 
            this.chkRandomFaceData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRandomFaceData.AutoSize = true;
            this.chkRandomFaceData.Checked = true;
            this.chkRandomFaceData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRandomFaceData.Location = new System.Drawing.Point(265, 222);
            this.chkRandomFaceData.Name = "chkRandomFaceData";
            this.chkRandomFaceData.Size = new System.Drawing.Size(144, 17);
            this.chkRandomFaceData.TabIndex = 22;
            this.chkRandomFaceData.Text = "Random NPC Face Data";
            this.tooltip.SetToolTip(this.chkRandomFaceData, "Randomizes the faces of npcs. Not effected by DoTrueRandom.");
            this.chkRandomFaceData.UseVisualStyleBackColor = true;
            // 
            // checkBoxRemaster
            // 
            this.checkBoxRemaster.AutoSize = true;
            this.checkBoxRemaster.Location = new System.Drawing.Point(11, 304);
            this.checkBoxRemaster.Name = "checkBoxRemaster";
            this.checkBoxRemaster.Size = new System.Drawing.Size(176, 17);
            this.checkBoxRemaster.TabIndex = 23;
            this.checkBoxRemaster.Text = "DARK SOULS: REMASTERED";
            this.tooltip.SetToolTip(this.checkBoxRemaster, "Enable this if you are using the remastered version.");
            this.checkBoxRemaster.UseVisualStyleBackColor = true;
            this.checkBoxRemaster.CheckedChanged += new System.EventHandler(this.checkBoxRemaster_CheckedChanged);
            // 
            // checkBoxWeaponScaling
            // 
            this.checkBoxWeaponScaling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxWeaponScaling.AutoSize = true;
            this.checkBoxWeaponScaling.Checked = true;
            this.checkBoxWeaponScaling.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponScaling.Location = new System.Drawing.Point(265, 245);
            this.checkBoxWeaponScaling.Name = "checkBoxWeaponScaling";
            this.checkBoxWeaponScaling.Size = new System.Drawing.Size(156, 17);
            this.checkBoxWeaponScaling.TabIndex = 24;
            this.checkBoxWeaponScaling.Text = "Randomize weapon scaling";
            this.tooltip.SetToolTip(this.checkBoxWeaponScaling, "Randomizes the scaling of a weapon. Effected by DoTrueRandom.");
            this.checkBoxWeaponScaling.UseVisualStyleBackColor = true;
            // 
            // checkBoxWeaponStatMin
            // 
            this.checkBoxWeaponStatMin.AutoSize = true;
            this.checkBoxWeaponStatMin.Checked = true;
            this.checkBoxWeaponStatMin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponStatMin.Location = new System.Drawing.Point(11, 243);
            this.checkBoxWeaponStatMin.Name = "checkBoxWeaponStatMin";
            this.checkBoxWeaponStatMin.Size = new System.Drawing.Size(188, 17);
            this.checkBoxWeaponStatMin.TabIndex = 25;
            this.checkBoxWeaponStatMin.Text = "Randomize weapon minimum stats";
            this.tooltip.SetToolTip(this.checkBoxWeaponStatMin, "Randomizes the minimum stats for a weapon. Effected by DoTrueRandom.");
            this.checkBoxWeaponStatMin.UseVisualStyleBackColor = true;
            // 
            // checkBoxWeaponWeight
            // 
            this.checkBoxWeaponWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxWeaponWeight.AutoSize = true;
            this.checkBoxWeaponWeight.Checked = true;
            this.checkBoxWeaponWeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponWeight.Location = new System.Drawing.Point(265, 268);
            this.checkBoxWeaponWeight.Name = "checkBoxWeaponWeight";
            this.checkBoxWeaponWeight.Size = new System.Drawing.Size(154, 17);
            this.checkBoxWeaponWeight.TabIndex = 27;
            this.checkBoxWeaponWeight.Text = "Randomize weapon weight";
            this.tooltip.SetToolTip(this.checkBoxWeaponWeight, "Randomizes the weight of a weapon. Effected by DoTrueRandom.");
            this.checkBoxWeaponWeight.UseVisualStyleBackColor = true;
            // 
            // checkBoxDoTrueRandom
            // 
            this.checkBoxDoTrueRandom.AutoSize = true;
            this.checkBoxDoTrueRandom.Location = new System.Drawing.Point(11, 268);
            this.checkBoxDoTrueRandom.Name = "checkBoxDoTrueRandom";
            this.checkBoxDoTrueRandom.Size = new System.Drawing.Size(256, 17);
            this.checkBoxDoTrueRandom.TabIndex = 26;
            this.checkBoxDoTrueRandom.Text = "Don\'t randomize by shuffle; randomize by random";
            this.tooltip.SetToolTip(this.checkBoxDoTrueRandom, resources.GetString("checkBoxDoTrueRandom.ToolTip"));
            this.checkBoxDoTrueRandom.UseVisualStyleBackColor = true;
            // 
            // tooltip
            // 
            this.tooltip.AutomaticDelay = 250;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 329);
            this.Controls.Add(this.checkBoxRemaster);
            this.Controls.Add(this.chkRandomFaceData);
            this.Controls.Add(this.btnOpenFolderDialog);
            this.Controls.Add(this.chkMagicAnimations);
            this.Controls.Add(this.chkItemAnimations);
            this.Controls.Add(this.chkRingSpeffects);
            this.Controls.Add(this.chkTurnSpeeds);
            this.Controls.Add(this.chkHitboxSizes);
            this.Controls.Add(this.chkStaggerLevels);
            this.Controls.Add(this.chkAggroRadius);
            this.Controls.Add(this.chkWeaponModels);
            this.Controls.Add(this.chkWeaponDamage);
            this.Controls.Add(this.checkBoxWeaponScaling);
            this.Controls.Add(this.checkBoxWeaponStatMin);
            this.Controls.Add(this.checkBoxWeaponWeight);
            this.Controls.Add(this.chkWeaponMoveset);
            this.Controls.Add(this.checkBoxDoTrueRandom);
            this.Controls.Add(this.chkBullets);
            this.Controls.Add(this.chkKnockback);
            this.Controls.Add(this.chkSpeffects);
            this.Controls.Add(this.chkAttackSpeffects);
            this.Controls.Add(this.chkVoices);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtGamePath);
            this.Controls.Add(this.lblGamePath);
            this.Controls.Add(this.lblSeed);
            this.Controls.Add(this.txtSeed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Paramdomizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.Label lblSeed;
        private System.Windows.Forms.Label lblGamePath;
        private System.Windows.Forms.TextBox txtGamePath;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.CheckBox chkWeaponModels;
        private System.Windows.Forms.CheckBox chkWeaponDamage;
        private System.Windows.Forms.CheckBox chkWeaponMoveset;
        private System.Windows.Forms.CheckBox chkBullets;
        private System.Windows.Forms.CheckBox chkKnockback;
        private System.Windows.Forms.CheckBox chkSpeffects;
        private System.Windows.Forms.CheckBox chkAttackSpeffects;
        private System.Windows.Forms.CheckBox chkVoices;
        private System.Windows.Forms.CheckBox chkTurnSpeeds;
        private System.Windows.Forms.CheckBox chkHitboxSizes;
        private System.Windows.Forms.CheckBox chkStaggerLevels;
        private System.Windows.Forms.CheckBox chkAggroRadius;
        private System.Windows.Forms.CheckBox chkRingSpeffects;
        private System.Windows.Forms.CheckBox chkItemAnimations;
        private System.Windows.Forms.CheckBox chkMagicAnimations;
        private System.Windows.Forms.Button btnOpenFolderDialog;
        private System.Windows.Forms.CheckBox chkRandomFaceData;
        private System.Windows.Forms.CheckBox checkBoxRemaster;
        private System.Windows.Forms.CheckBox checkBoxWeaponScaling;
        private System.Windows.Forms.CheckBox checkBoxWeaponStatMin;
        private System.Windows.Forms.CheckBox checkBoxWeaponWeight;
        private System.Windows.Forms.CheckBox checkBoxDoTrueRandom;
        private System.Windows.Forms.ToolTip tooltip;
    }
}

