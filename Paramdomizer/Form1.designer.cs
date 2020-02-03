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
            this.chkWeaponSpeffects = new System.Windows.Forms.CheckBox();
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
            this.checkBoxWeaponStamina = new System.Windows.Forms.CheckBox();
            this.checkBoxWeaponDefense = new System.Windows.Forms.CheckBox();
            this.checkBoxWeaponShieldSplit = new System.Windows.Forms.CheckBox();
            this.checkBoxWeaponFistNo = new System.Windows.Forms.CheckBox();
            this.checkBoxDoTrueRandom = new System.Windows.Forms.CheckBox();
            this.checkBoxUniversalizeCasters = new System.Windows.Forms.CheckBox();
            this.checkBoxRandomizeSpellRequirements = new System.Windows.Forms.CheckBox();
            this.checkBoxRandomizeSpellSlotSize = new System.Windows.Forms.CheckBox();
            this.checkBoxRandomizeSpellQuantity = new System.Windows.Forms.CheckBox();
            this.checkBoxArmorWeight = new System.Windows.Forms.CheckBox();
            this.checkBoxArmorPoise = new System.Windows.Forms.CheckBox();
            this.checkBoxArmorResistance = new System.Windows.Forms.CheckBox();
            this.checkBoxArmorspEffect = new System.Windows.Forms.CheckBox();
            this.checkBoxStartingGifts = new System.Windows.Forms.CheckBox();
            this.checkBoxPreventSpellGifts = new System.Windows.Forms.CheckBox();
            this.checkBoxNerfHumanityBullets = new System.Windows.Forms.CheckBox();
            this.checkBoxStartingGiftsAmount = new System.Windows.Forms.CheckBox();
            this.checkBoxStartingClasses = new System.Windows.Forms.CheckBox();
            this.checkBoxForceUseableStartSpells = new System.Windows.Forms.CheckBox();
            this.checkBoxForceUseableStartWeapons = new System.Windows.Forms.CheckBox();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.gbWeaponCategory = new System.Windows.Forms.GroupBox();
            this.gbSpellCategory = new System.Windows.Forms.GroupBox();
            this.gbEnemiesCategory = new System.Windows.Forms.GroupBox();
            this.gbOtherCategory = new System.Windows.Forms.GroupBox();
            this.gbSharedCategory = new System.Windows.Forms.GroupBox();
            this.gbArmorCategory = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.gbWeaponCategory.SuspendLayout();
            this.gbSpellCategory.SuspendLayout();
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
            this.btnSubmit.Location = new System.Drawing.Point(869, 434);
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
            this.chkRingSpeffects.Location = new System.Drawing.Point(504, 267);
            this.chkRingSpeffects.Name = "chkRingSpeffects";
            this.chkRingSpeffects.Size = new System.Drawing.Size(168, 17);
            this.chkRingSpeffects.TabIndex = 7;
            this.chkRingSpeffects.Text = "Randomize SPeffects on rings";
            this.tooltip.SetToolTip(this.chkRingSpeffects, "Randomizes the effects of rings amongst each other.");
            this.chkRingSpeffects.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(701, 371);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(243, 43);
            this.lblMessage.TabIndex = 6;
            this.lblMessage.Text = "label1";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMessage.Visible = false;
            // 
            // chkWeaponModels
            // 
            this.chkWeaponModels.AutoSize = true;
            this.chkWeaponModels.Checked = true;
            this.chkWeaponModels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeaponModels.Location = new System.Drawing.Point(30, 116);
            this.chkWeaponModels.Name = "chkWeaponModels";
            this.chkWeaponModels.Size = new System.Drawing.Size(156, 17);
            this.chkWeaponModels.TabIndex = 13;
            this.chkWeaponModels.Text = "Randomize weapon models";
            this.tooltip.SetToolTip(this.chkWeaponModels, "Randomizes weapon models.");
            this.chkWeaponModels.UseVisualStyleBackColor = true;
            // 
            // chkWeaponDamage
            // 
            this.chkWeaponDamage.AutoSize = true;
            this.chkWeaponDamage.Checked = true;
            this.chkWeaponDamage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeaponDamage.Location = new System.Drawing.Point(30, 93);
            this.chkWeaponDamage.Name = "chkWeaponDamage";
            this.chkWeaponDamage.Size = new System.Drawing.Size(161, 17);
            this.chkWeaponDamage.TabIndex = 12;
            this.chkWeaponDamage.Text = "Randomize weapon damage";
            this.tooltip.SetToolTip(this.chkWeaponDamage, "Randomizes weapon damage.\nEffected by Don\'t randomize by shuffle.");
            this.chkWeaponDamage.UseVisualStyleBackColor = true;
            // 
            // chkWeaponMoveset
            // 
            this.chkWeaponMoveset.AutoSize = true;
            this.chkWeaponMoveset.Checked = true;
            this.chkWeaponMoveset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeaponMoveset.Location = new System.Drawing.Point(265, 93);
            this.chkWeaponMoveset.Name = "chkWeaponMoveset";
            this.chkWeaponMoveset.Size = new System.Drawing.Size(168, 17);
            this.chkWeaponMoveset.TabIndex = 11;
            this.chkWeaponMoveset.Text = "Randomize weapon movesets";
            this.tooltip.SetToolTip(this.chkWeaponMoveset, "Randomizes weapon movesets.");
            this.chkWeaponMoveset.UseVisualStyleBackColor = true;
            // 
            // chkBullets
            // 
            this.chkBullets.AutoSize = true;
            this.chkBullets.Checked = true;
            this.chkBullets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBullets.Location = new System.Drawing.Point(33, 348);
            this.chkBullets.Name = "chkBullets";
            this.chkBullets.Size = new System.Drawing.Size(112, 17);
            this.chkBullets.TabIndex = 9;
            this.chkBullets.Text = "Randomize bullets";
            this.tooltip.SetToolTip(this.chkBullets, "Randomizes bullets in a lot of ways. ei: it\'s damage, damage type, movement, amon" +
        "g other things.\nAppears to effect both player and enemy projectiles.");
            this.chkBullets.UseVisualStyleBackColor = true;
            // 
            // chkKnockback
            // 
            this.chkKnockback.AutoSize = true;
            this.chkKnockback.Checked = true;
            this.chkKnockback.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKnockback.Location = new System.Drawing.Point(268, 325);
            this.chkKnockback.Name = "chkKnockback";
            this.chkKnockback.Size = new System.Drawing.Size(169, 17);
            this.chkKnockback.TabIndex = 16;
            this.chkKnockback.Text = "Randomize attack knockback";
            this.tooltip.SetToolTip(this.chkKnockback, "Randomizes attack knockback.\nAffects all weapons and common moves like ladder kic" +
        "ks.\nAffects Enemy attacks too.");
            this.chkKnockback.UseVisualStyleBackColor = true;
            this.chkKnockback.CheckedChanged += new System.EventHandler(this.chkKnockback_CheckedChanged);
            // 
            // chkSpeffects
            // 
            this.chkSpeffects.AutoSize = true;
            this.chkSpeffects.Checked = true;
            this.chkSpeffects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpeffects.Location = new System.Drawing.Point(33, 277);
            this.chkSpeffects.Name = "chkSpeffects";
            this.chkSpeffects.Size = new System.Drawing.Size(185, 17);
            this.chkSpeffects.TabIndex = 5;
            this.chkSpeffects.Text = "Randomize SPeffects on enemies";
            this.tooltip.SetToolTip(this.chkSpeffects, "Randomizes special effects on enemies as well as their attacks.");
            this.chkSpeffects.UseVisualStyleBackColor = true;
            // 
            // chkWeaponSpeffects
            // 
            this.chkWeaponSpeffects.AutoSize = true;
            this.chkWeaponSpeffects.Checked = true;
            this.chkWeaponSpeffects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeaponSpeffects.Location = new System.Drawing.Point(265, 162);
            this.chkWeaponSpeffects.Name = "chkWeaponSpeffects";
            this.chkWeaponSpeffects.Size = new System.Drawing.Size(169, 17);
            this.chkWeaponSpeffects.TabIndex = 6;
            this.chkWeaponSpeffects.Text = "Randomize weapon SPeffects";
            this.tooltip.SetToolTip(this.chkWeaponSpeffects, "Randomizes the special effects of weapons, like poison buildup, the chaos blade\'s" +
        " life loss and the grass crest shield\'s regen.");
            this.chkWeaponSpeffects.UseVisualStyleBackColor = true;
            // 
            // chkVoices
            // 
            this.chkVoices.AutoSize = true;
            this.chkVoices.Checked = true;
            this.chkVoices.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVoices.Location = new System.Drawing.Point(739, 267);
            this.chkVoices.Name = "chkVoices";
            this.chkVoices.Size = new System.Drawing.Size(132, 17);
            this.chkVoices.TabIndex = 10;
            this.chkVoices.Text = "Randomize voice lines";
            this.tooltip.SetToolTip(this.chkVoices, "Randomizes voice lines.");
            this.chkVoices.UseVisualStyleBackColor = true;
            // 
            // chkTurnSpeeds
            // 
            this.chkTurnSpeeds.AutoSize = true;
            this.chkTurnSpeeds.Checked = true;
            this.chkTurnSpeeds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTurnSpeeds.Location = new System.Drawing.Point(268, 254);
            this.chkTurnSpeeds.Name = "chkTurnSpeeds";
            this.chkTurnSpeeds.Size = new System.Drawing.Size(166, 17);
            this.chkTurnSpeeds.TabIndex = 3;
            this.chkTurnSpeeds.Text = "Randomize enemy turn speed";
            this.tooltip.SetToolTip(this.chkTurnSpeeds, "Randomizes enemy turn speed.");
            this.chkTurnSpeeds.UseVisualStyleBackColor = true;
            // 
            // chkHitboxSizes
            // 
            this.chkHitboxSizes.AutoSize = true;
            this.chkHitboxSizes.Checked = true;
            this.chkHitboxSizes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHitboxSizes.Location = new System.Drawing.Point(268, 348);
            this.chkHitboxSizes.Name = "chkHitboxSizes";
            this.chkHitboxSizes.Size = new System.Drawing.Size(136, 17);
            this.chkHitboxSizes.TabIndex = 18;
            this.chkHitboxSizes.Text = "Randomize hitbox sizes";
            this.tooltip.SetToolTip(this.chkHitboxSizes, "Randomizes hitbox sizes of attacks.\nAffects both the player\'s attacks and enemy a" +
        "ttacks.\nAffects each weapon differently.\nCommon attacks like those on ladders ar" +
        "e affected.");
            this.chkHitboxSizes.UseVisualStyleBackColor = true;
            // 
            // chkStaggerLevels
            // 
            this.chkStaggerLevels.AutoSize = true;
            this.chkStaggerLevels.Checked = true;
            this.chkStaggerLevels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStaggerLevels.Location = new System.Drawing.Point(33, 325);
            this.chkStaggerLevels.Name = "chkStaggerLevels";
            this.chkStaggerLevels.Size = new System.Drawing.Size(147, 17);
            this.chkStaggerLevels.TabIndex = 17;
            this.chkStaggerLevels.Text = "Randomize stagger levels";
            this.tooltip.SetToolTip(this.chkStaggerLevels, "Randomizes stagger levels.\nAffects both how much stagger enemies and players do.\n" +
        "Each attack will get a different stagger including moves like ladder kicks.");
            this.chkStaggerLevels.UseVisualStyleBackColor = true;
            // 
            // chkAggroRadius
            // 
            this.chkAggroRadius.AutoSize = true;
            this.chkAggroRadius.Checked = true;
            this.chkAggroRadius.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAggroRadius.Location = new System.Drawing.Point(33, 254);
            this.chkAggroRadius.Name = "chkAggroRadius";
            this.chkAggroRadius.Size = new System.Drawing.Size(140, 17);
            this.chkAggroRadius.TabIndex = 2;
            this.chkAggroRadius.Text = "Randomize aggro radius";
            this.tooltip.SetToolTip(this.chkAggroRadius, "Randomizes the aggro range of enemies.");
            this.chkAggroRadius.UseVisualStyleBackColor = true;
            // 
            // chkItemAnimations
            // 
            this.chkItemAnimations.AutoSize = true;
            this.chkItemAnimations.Checked = true;
            this.chkItemAnimations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkItemAnimations.Location = new System.Drawing.Point(504, 244);
            this.chkItemAnimations.Name = "chkItemAnimations";
            this.chkItemAnimations.Size = new System.Drawing.Size(154, 17);
            this.chkItemAnimations.TabIndex = 14;
            this.chkItemAnimations.Text = "Randomize item animations";
            this.tooltip.SetToolTip(this.chkItemAnimations, "Randomizes the animations of items.\nCan be extra difficult if your estus is repla" +
        "ced with a slow animation.");
            this.chkItemAnimations.UseVisualStyleBackColor = true;
            // 
            // chkMagicAnimations
            // 
            this.chkMagicAnimations.AutoSize = true;
            this.chkMagicAnimations.Checked = true;
            this.chkMagicAnimations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMagicAnimations.Location = new System.Drawing.Point(501, 191);
            this.chkMagicAnimations.Name = "chkMagicAnimations";
            this.chkMagicAnimations.Size = new System.Drawing.Size(163, 17);
            this.chkMagicAnimations.TabIndex = 15;
            this.chkMagicAnimations.Text = "Randomize magic animations";
            this.tooltip.SetToolTip(this.chkMagicAnimations, "Randomizes the animations of magic.");
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
            this.chkRandomFaceData.AutoSize = true;
            this.chkRandomFaceData.Checked = true;
            this.chkRandomFaceData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRandomFaceData.Location = new System.Drawing.Point(739, 244);
            this.chkRandomFaceData.Name = "chkRandomFaceData";
            this.chkRandomFaceData.Size = new System.Drawing.Size(139, 17);
            this.chkRandomFaceData.TabIndex = 22;
            this.chkRandomFaceData.Text = "Random NPC face data";
            this.tooltip.SetToolTip(this.chkRandomFaceData, "Randomizes the faces of npcs who use the player model.\nAlso randomizes starting f" +
        "ace data for players.");
            this.chkRandomFaceData.UseVisualStyleBackColor = true;
            // 
            // checkBoxRemaster
            // 
            this.checkBoxRemaster.AutoSize = true;
            this.checkBoxRemaster.Location = new System.Drawing.Point(33, 440);
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
            this.checkBoxWeaponScaling.AutoSize = true;
            this.checkBoxWeaponScaling.Checked = true;
            this.checkBoxWeaponScaling.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponScaling.Location = new System.Drawing.Point(30, 139);
            this.checkBoxWeaponScaling.Name = "checkBoxWeaponScaling";
            this.checkBoxWeaponScaling.Size = new System.Drawing.Size(156, 17);
            this.checkBoxWeaponScaling.TabIndex = 24;
            this.checkBoxWeaponScaling.Text = "Randomize weapon scaling";
            this.tooltip.SetToolTip(this.checkBoxWeaponScaling, "Randomizes the scaling of a weapon.\nEffected by Don\'t randomize by shuffle.");
            this.checkBoxWeaponScaling.UseVisualStyleBackColor = true;
            // 
            // checkBoxWeaponStatMin
            // 
            this.checkBoxWeaponStatMin.AutoSize = true;
            this.checkBoxWeaponStatMin.Checked = true;
            this.checkBoxWeaponStatMin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponStatMin.Location = new System.Drawing.Point(30, 162);
            this.checkBoxWeaponStatMin.Name = "checkBoxWeaponStatMin";
            this.checkBoxWeaponStatMin.Size = new System.Drawing.Size(188, 17);
            this.checkBoxWeaponStatMin.TabIndex = 25;
            this.checkBoxWeaponStatMin.Text = "Randomize weapon minimum stats";
            this.tooltip.SetToolTip(this.checkBoxWeaponStatMin, resources.GetString("checkBoxWeaponStatMin.ToolTip"));
            this.checkBoxWeaponStatMin.UseVisualStyleBackColor = true;
            // 
            // checkBoxWeaponWeight
            // 
            this.checkBoxWeaponWeight.AutoSize = true;
            this.checkBoxWeaponWeight.Checked = true;
            this.checkBoxWeaponWeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponWeight.Location = new System.Drawing.Point(265, 116);
            this.checkBoxWeaponWeight.Name = "checkBoxWeaponWeight";
            this.checkBoxWeaponWeight.Size = new System.Drawing.Size(154, 17);
            this.checkBoxWeaponWeight.TabIndex = 27;
            this.checkBoxWeaponWeight.Text = "Randomize weapon weight";
            this.tooltip.SetToolTip(this.checkBoxWeaponWeight, "Randomizes the weight of a weapon.\nEffected by Don\'t randomize by shuffle.");
            this.checkBoxWeaponWeight.UseVisualStyleBackColor = true;
            // 
            // checkBoxWeaponStamina
            // 
            this.checkBoxWeaponStamina.AutoSize = true;
            this.checkBoxWeaponStamina.Checked = true;
            this.checkBoxWeaponStamina.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponStamina.Location = new System.Drawing.Point(265, 139);
            this.checkBoxWeaponStamina.Name = "checkBoxWeaponStamina";
            this.checkBoxWeaponStamina.Size = new System.Drawing.Size(159, 17);
            this.checkBoxWeaponStamina.TabIndex = 25;
            this.checkBoxWeaponStamina.Text = "Randomize weapon stamina";
            this.tooltip.SetToolTip(this.checkBoxWeaponStamina, "Randomizes the stamina usage of weapons.\nEffected by Don\'t randomize by shuffle.");
            this.checkBoxWeaponStamina.UseVisualStyleBackColor = true;
            // 
            // checkBoxWeaponDefense
            // 
            this.checkBoxWeaponDefense.AutoSize = true;
            this.checkBoxWeaponDefense.Checked = true;
            this.checkBoxWeaponDefense.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponDefense.Location = new System.Drawing.Point(30, 185);
            this.checkBoxWeaponDefense.Name = "checkBoxWeaponDefense";
            this.checkBoxWeaponDefense.Size = new System.Drawing.Size(161, 17);
            this.checkBoxWeaponDefense.TabIndex = 41;
            this.checkBoxWeaponDefense.Text = "Randomize weapon defense";
            this.tooltip.SetToolTip(this.checkBoxWeaponDefense, "Randomizes the the defense values a weapon or shield would have if you were to bl" +
        "ock with it.");
            this.checkBoxWeaponDefense.UseVisualStyleBackColor = true;
            // 
            // checkBoxWeaponShieldSplit
            // 
            this.checkBoxWeaponShieldSplit.AutoSize = true;
            this.checkBoxWeaponShieldSplit.Checked = true;
            this.checkBoxWeaponShieldSplit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponShieldSplit.Location = new System.Drawing.Point(265, 185);
            this.checkBoxWeaponShieldSplit.Name = "checkBoxWeaponShieldSplit";
            this.checkBoxWeaponShieldSplit.Size = new System.Drawing.Size(137, 17);
            this.checkBoxWeaponShieldSplit.TabIndex = 42;
            this.checkBoxWeaponShieldSplit.Text = "Treat shields seperately";
            this.tooltip.SetToolTip(this.checkBoxWeaponShieldSplit, "Shields will have a different randomization pool from regular weapons\nShields wil" +
        "l not be affected by the damage or defense values of normal weapons and vice ver" +
        "sa.");
            this.checkBoxWeaponShieldSplit.UseVisualStyleBackColor = true;
            // 
            // checkBoxWeaponFistNo
            // 
            this.checkBoxWeaponFistNo.AutoSize = true;
            this.checkBoxWeaponFistNo.Checked = true;
            this.checkBoxWeaponFistNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeaponFistNo.Location = new System.Drawing.Point(30, 208);
            this.checkBoxWeaponFistNo.Name = "checkBoxWeaponFistNo";
            this.checkBoxWeaponFistNo.Size = new System.Drawing.Size(105, 17);
            this.checkBoxWeaponFistNo.TabIndex = 43;
            this.checkBoxWeaponFistNo.Text = "Don\'t modify fists";
            this.tooltip.SetToolTip(this.checkBoxWeaponFistNo, "Fists will not be modified by damage randomization of any other type of weapon ra" +
        "ndomization");
            this.checkBoxWeaponFistNo.UseVisualStyleBackColor = true;
            // 
            // checkBoxDoTrueRandom
            // 
            this.checkBoxDoTrueRandom.AutoSize = true;
            this.checkBoxDoTrueRandom.Location = new System.Drawing.Point(33, 406);
            this.checkBoxDoTrueRandom.Name = "checkBoxDoTrueRandom";
            this.checkBoxDoTrueRandom.Size = new System.Drawing.Size(153, 30);
            this.checkBoxDoTrueRandom.TabIndex = 26;
            this.checkBoxDoTrueRandom.Text = "Don\'t randomize by shuffle;\nrandomize by random";
            this.tooltip.SetToolTip(this.checkBoxDoTrueRandom, resources.GetString("checkBoxDoTrueRandom.ToolTip"));
            this.checkBoxDoTrueRandom.UseVisualStyleBackColor = true;
            // 
            // checkBoxUniversalizeCasters
            // 
            this.checkBoxUniversalizeCasters.AutoSize = true;
            this.checkBoxUniversalizeCasters.Checked = true;
            this.checkBoxUniversalizeCasters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUniversalizeCasters.Location = new System.Drawing.Point(501, 145);
            this.checkBoxUniversalizeCasters.Name = "checkBoxUniversalizeCasters";
            this.checkBoxUniversalizeCasters.Size = new System.Drawing.Size(120, 17);
            this.checkBoxUniversalizeCasters.TabIndex = 28;
            this.checkBoxUniversalizeCasters.Text = "Universalize casters";
            this.tooltip.SetToolTip(this.checkBoxUniversalizeCasters, "Casting items like the pyromancy flame, logan\'s catalyst, and talismans will now " +
        "be able to cast all spell/pyromancy/miracle types");
            this.checkBoxUniversalizeCasters.UseVisualStyleBackColor = true;
            // 
            // checkBoxRandomizeSpellRequirements
            // 
            this.checkBoxRandomizeSpellRequirements.AutoSize = true;
            this.checkBoxRandomizeSpellRequirements.Checked = true;
            this.checkBoxRandomizeSpellRequirements.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRandomizeSpellRequirements.Location = new System.Drawing.Point(736, 145);
            this.checkBoxRandomizeSpellRequirements.Name = "checkBoxRandomizeSpellRequirements";
            this.checkBoxRandomizeSpellRequirements.Size = new System.Drawing.Size(166, 17);
            this.checkBoxRandomizeSpellRequirements.TabIndex = 29;
            this.checkBoxRandomizeSpellRequirements.Text = "Randomize spell requirements";
            this.tooltip.SetToolTip(this.checkBoxRandomizeSpellRequirements, "Randomizes Spells/Pyromancies/Miracles stat requirements.");
            this.checkBoxRandomizeSpellRequirements.UseVisualStyleBackColor = true;
            // 
            // checkBoxRandomizeSpellSlotSize
            // 
            this.checkBoxRandomizeSpellSlotSize.AutoSize = true;
            this.checkBoxRandomizeSpellSlotSize.Checked = true;
            this.checkBoxRandomizeSpellSlotSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRandomizeSpellSlotSize.Location = new System.Drawing.Point(501, 168);
            this.checkBoxRandomizeSpellSlotSize.Name = "checkBoxRandomizeSpellSlotSize";
            this.checkBoxRandomizeSpellSlotSize.Size = new System.Drawing.Size(143, 17);
            this.checkBoxRandomizeSpellSlotSize.TabIndex = 30;
            this.checkBoxRandomizeSpellSlotSize.Text = "Randomize spell slot size";
            this.tooltip.SetToolTip(this.checkBoxRandomizeSpellSlotSize, "Randomizes Spells/Pyromancies/Miracles slot size.");
            this.checkBoxRandomizeSpellSlotSize.UseVisualStyleBackColor = true;
            // 
            // checkBoxRandomizeSpellQuantity
            // 
            this.checkBoxRandomizeSpellQuantity.AutoSize = true;
            this.checkBoxRandomizeSpellQuantity.Checked = true;
            this.checkBoxRandomizeSpellQuantity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRandomizeSpellQuantity.Location = new System.Drawing.Point(736, 168);
            this.checkBoxRandomizeSpellQuantity.Name = "checkBoxRandomizeSpellQuantity";
            this.checkBoxRandomizeSpellQuantity.Size = new System.Drawing.Size(143, 17);
            this.checkBoxRandomizeSpellQuantity.TabIndex = 31;
            this.checkBoxRandomizeSpellQuantity.Text = "Randomize spell quantity";
            this.tooltip.SetToolTip(this.checkBoxRandomizeSpellQuantity, "Randomizes Spells/Pyromancies/Miracles quantity. (how many times it can be shot)");
            this.checkBoxRandomizeSpellQuantity.UseVisualStyleBackColor = true;
            // 
            // checkBoxArmorWeight
            // 
            this.checkBoxArmorWeight.AutoSize = true;
            this.checkBoxArmorWeight.Checked = true;
            this.checkBoxArmorWeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxArmorWeight.Location = new System.Drawing.Point(736, 82);
            this.checkBoxArmorWeight.Name = "checkBoxArmorWeight";
            this.checkBoxArmorWeight.Size = new System.Drawing.Size(142, 17);
            this.checkBoxArmorWeight.TabIndex = 37;
            this.checkBoxArmorWeight.Text = "Randomize armor weight";
            this.tooltip.SetToolTip(this.checkBoxArmorWeight, "Randomizes the weight of armor.");
            this.checkBoxArmorWeight.UseVisualStyleBackColor = true;
            // 
            // checkBoxArmorPoise
            // 
            this.checkBoxArmorPoise.AutoSize = true;
            this.checkBoxArmorPoise.Checked = true;
            this.checkBoxArmorPoise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxArmorPoise.Location = new System.Drawing.Point(501, 103);
            this.checkBoxArmorPoise.Name = "checkBoxArmorPoise";
            this.checkBoxArmorPoise.Size = new System.Drawing.Size(136, 17);
            this.checkBoxArmorPoise.TabIndex = 38;
            this.checkBoxArmorPoise.Text = "Randomize armor poise";
            this.tooltip.SetToolTip(this.checkBoxArmorPoise, "Randomizes the poise of armor.");
            this.checkBoxArmorPoise.UseVisualStyleBackColor = true;
            // 
            // checkBoxArmorResistance
            // 
            this.checkBoxArmorResistance.AutoSize = true;
            this.checkBoxArmorResistance.Checked = true;
            this.checkBoxArmorResistance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxArmorResistance.Location = new System.Drawing.Point(501, 82);
            this.checkBoxArmorResistance.Name = "checkBoxArmorResistance";
            this.checkBoxArmorResistance.Size = new System.Drawing.Size(159, 17);
            this.checkBoxArmorResistance.TabIndex = 39;
            this.checkBoxArmorResistance.Text = "Randomize armor resistance";
            this.tooltip.SetToolTip(this.checkBoxArmorResistance, "Randomizes the defense values of armor as well as it\'s resistance values.");
            this.checkBoxArmorResistance.UseVisualStyleBackColor = true;
            // 
            // checkBoxArmorspEffect
            // 
            this.checkBoxArmorspEffect.AutoSize = true;
            this.checkBoxArmorspEffect.Checked = true;
            this.checkBoxArmorspEffect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxArmorspEffect.Location = new System.Drawing.Point(736, 105);
            this.checkBoxArmorspEffect.Name = "checkBoxArmorspEffect";
            this.checkBoxArmorspEffect.Size = new System.Drawing.Size(157, 17);
            this.checkBoxArmorspEffect.TabIndex = 40;
            this.checkBoxArmorspEffect.Text = "Randomize armor SPeffects";
            this.tooltip.SetToolTip(this.checkBoxArmorspEffect, "Randomizes the special effects of armor like that of the mask of the father.");
            this.checkBoxArmorspEffect.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartingGifts
            // 
            this.checkBoxStartingGifts.AutoSize = true;
            this.checkBoxStartingGifts.Checked = true;
            this.checkBoxStartingGifts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStartingGifts.Location = new System.Drawing.Point(504, 290);
            this.checkBoxStartingGifts.Name = "checkBoxStartingGifts";
            this.checkBoxStartingGifts.Size = new System.Drawing.Size(138, 17);
            this.checkBoxStartingGifts.TabIndex = 44;
            this.checkBoxStartingGifts.Text = "Randomize starting gifts";
            this.tooltip.SetToolTip(this.checkBoxStartingGifts, "Randomizes the starting gifts and changes the english menus to correspend with th" +
        "e changes.\nNoteworthy: the 2 starting rings get randomized into other rings and " +
        "the master key does not get randomized");
            this.checkBoxStartingGifts.UseVisualStyleBackColor = true;
            // 
            // checkBoxPreventSpellGifts
            // 
            this.checkBoxPreventSpellGifts.AutoSize = true;
            this.checkBoxPreventSpellGifts.Checked = true;
            this.checkBoxPreventSpellGifts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPreventSpellGifts.Location = new System.Drawing.Point(739, 290);
            this.checkBoxPreventSpellGifts.Name = "checkBoxPreventSpellGifts";
            this.checkBoxPreventSpellGifts.Size = new System.Drawing.Size(109, 17);
            this.checkBoxPreventSpellGifts.TabIndex = 44;
            this.checkBoxPreventSpellGifts.Text = "Prevent spell gifts";
            this.tooltip.SetToolTip(this.checkBoxPreventSpellGifts, "Prevents Sorceries, Miracles, or Pyromancies from being valid starting gifts.\nonl" +
        "y applies if starting gifts are randomized");
            this.checkBoxPreventSpellGifts.UseVisualStyleBackColor = true;
            // 
            // checkBoxNerfHumanityBullets
            // 
            this.checkBoxNerfHumanityBullets.AutoSize = true;
            this.checkBoxNerfHumanityBullets.Checked = true;
            this.checkBoxNerfHumanityBullets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNerfHumanityBullets.Location = new System.Drawing.Point(33, 371);
            this.checkBoxNerfHumanityBullets.Name = "checkBoxNerfHumanityBullets";
            this.checkBoxNerfHumanityBullets.Size = new System.Drawing.Size(124, 17);
            this.checkBoxNerfHumanityBullets.TabIndex = 45;
            this.checkBoxNerfHumanityBullets.Text = "Nerf humanity bullets";
            this.tooltip.SetToolTip(this.checkBoxNerfHumanityBullets, resources.GetString("checkBoxNerfHumanityBullets.ToolTip"));
            this.checkBoxNerfHumanityBullets.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartingGiftsAmount
            // 
            this.checkBoxStartingGiftsAmount.AutoSize = true;
            this.checkBoxStartingGiftsAmount.Checked = true;
            this.checkBoxStartingGiftsAmount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStartingGiftsAmount.Location = new System.Drawing.Point(504, 313);
            this.checkBoxStartingGiftsAmount.Name = "checkBoxStartingGiftsAmount";
            this.checkBoxStartingGiftsAmount.Size = new System.Drawing.Size(171, 17);
            this.checkBoxStartingGiftsAmount.TabIndex = 44;
            this.checkBoxStartingGiftsAmount.Text = "Randomize starting gift amount";
            this.tooltip.SetToolTip(this.checkBoxStartingGiftsAmount, "Randomizes the amount each starting gift is. (ei 10 firebombs)\nThis feature never" +
        " randomizes by shuffle, even when dont randomize by shuffle is off.");
            this.checkBoxStartingGiftsAmount.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartingClasses
            // 
            this.checkBoxStartingClasses.AutoSize = true;
            this.checkBoxStartingClasses.Checked = true;
            this.checkBoxStartingClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStartingClasses.Location = new System.Drawing.Point(739, 313);
            this.checkBoxStartingClasses.Name = "checkBoxStartingClasses";
            this.checkBoxStartingClasses.Size = new System.Drawing.Size(154, 17);
            this.checkBoxStartingClasses.TabIndex = 44;
            this.checkBoxStartingClasses.Text = "Randomize starting classes";
            this.tooltip.SetToolTip(this.checkBoxStartingClasses, resources.GetString("checkBoxStartingClasses.ToolTip"));
            this.checkBoxStartingClasses.UseVisualStyleBackColor = true;
            // 
            // checkBoxForceUseableStartSpells
            // 
            this.checkBoxForceUseableStartSpells.AutoSize = true;
            this.checkBoxForceUseableStartSpells.Checked = true;
            this.checkBoxForceUseableStartSpells.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxForceUseableStartSpells.Location = new System.Drawing.Point(257, 68);
            this.checkBoxForceUseableStartSpells.Name = "checkBoxForceUseableStartSpells";
            this.checkBoxForceUseableStartSpells.Size = new System.Drawing.Size(159, 17);
            this.checkBoxForceUseableStartSpells.TabIndex = 45;
            this.checkBoxForceUseableStartSpells.Text = "Force useable starting spells";
            this.tooltip.SetToolTip(this.checkBoxForceUseableStartSpells, resources.GetString("checkBoxForceUseableStartSpells.ToolTip"));
            this.checkBoxForceUseableStartSpells.UseVisualStyleBackColor = true;
            // 
            // checkBoxForceUseableStartWeapons
            // 
            this.checkBoxForceUseableStartWeapons.AutoSize = true;
            this.checkBoxForceUseableStartWeapons.Checked = true;
            this.checkBoxForceUseableStartWeapons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxForceUseableStartWeapons.Location = new System.Drawing.Point(257, 144);
            this.checkBoxForceUseableStartWeapons.Name = "checkBoxForceUseableStartWeapons";
            this.checkBoxForceUseableStartWeapons.Size = new System.Drawing.Size(176, 17);
            this.checkBoxForceUseableStartWeapons.TabIndex = 45;
            this.checkBoxForceUseableStartWeapons.Text = "Force useable starting weapons";
            this.tooltip.SetToolTip(this.checkBoxForceUseableStartWeapons, "Forces the weapons that a class starts with to be useable by that class (prevents" +
        " some softlocking).\nIf this setting is off the straightsword hilt will still be " +
        "universally useable.");
            this.checkBoxForceUseableStartWeapons.UseVisualStyleBackColor = true;
            // 
            // tooltip
            // 
            this.tooltip.AutoPopDelay = 32767;
            this.tooltip.InitialDelay = 500;
            this.tooltip.ReshowDelay = 100;
            // 
            // gbWeaponCategory
            // 
            this.gbWeaponCategory.AutoSize = true;
            this.gbWeaponCategory.Controls.Add(this.checkBoxForceUseableStartWeapons);
            this.gbWeaponCategory.Location = new System.Drawing.Point(8, 64);
            this.gbWeaponCategory.Name = "gbWeaponCategory";
            this.gbWeaponCategory.Size = new System.Drawing.Size(468, 180);
            this.gbWeaponCategory.TabIndex = 32;
            this.gbWeaponCategory.TabStop = false;
            this.gbWeaponCategory.Text = "Weapon Settings:";
            // 
            // gbSpellCategory
            // 
            this.gbSpellCategory.AutoSize = true;
            this.gbSpellCategory.Controls.Add(this.checkBoxForceUseableStartSpells);
            this.gbSpellCategory.Location = new System.Drawing.Point(479, 123);
            this.gbSpellCategory.Name = "gbSpellCategory";
            this.gbSpellCategory.Size = new System.Drawing.Size(465, 104);
            this.gbSpellCategory.TabIndex = 33;
            this.gbSpellCategory.TabStop = false;
            this.gbSpellCategory.Text = "Spell Settings:";
            // 
            // gbEnemiesCategory
            // 
            this.gbEnemiesCategory.AutoSize = true;
            this.gbEnemiesCategory.Location = new System.Drawing.Point(11, 231);
            this.gbEnemiesCategory.Name = "gbEnemiesCategory";
            this.gbEnemiesCategory.Size = new System.Drawing.Size(465, 64);
            this.gbEnemiesCategory.TabIndex = 34;
            this.gbEnemiesCategory.TabStop = false;
            this.gbEnemiesCategory.Text = "Enemy Settings:";
            // 
            // gbOtherCategory
            // 
            this.gbOtherCategory.AutoSize = true;
            this.gbOtherCategory.Location = new System.Drawing.Point(482, 214);
            this.gbOtherCategory.Name = "gbOtherCategory";
            this.gbOtherCategory.Size = new System.Drawing.Size(466, 116);
            this.gbOtherCategory.TabIndex = 35;
            this.gbOtherCategory.TabStop = false;
            this.gbOtherCategory.Text = "Other Settings:";
            // 
            // gbSharedCategory
            // 
            this.gbSharedCategory.AutoSize = true;
            this.gbSharedCategory.Location = new System.Drawing.Point(11, 301);
            this.gbSharedCategory.Name = "gbSharedCategory";
            this.gbSharedCategory.Size = new System.Drawing.Size(465, 87);
            this.gbSharedCategory.TabIndex = 36;
            this.gbSharedCategory.TabStop = false;
            this.gbSharedCategory.Text = "Enemy (and Player) Settings:";
            // 
            // gbArmorCategory
            // 
            this.gbArmorCategory.AutoSize = true;
            this.gbArmorCategory.Location = new System.Drawing.Point(479, 64);
            this.gbArmorCategory.Name = "gbArmorCategory";
            this.gbArmorCategory.Size = new System.Drawing.Size(465, 58);
            this.gbArmorCategory.TabIndex = 41;
            this.gbArmorCategory.TabStop = false;
            this.gbArmorCategory.Text = "Armor Settings:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(773, 441);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(90, 13);
            this.lblVersion.TabIndex = 41;
            this.lblVersion.Text = "DEV version 0.3d";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 470);
            this.Controls.Add(this.checkBoxNerfHumanityBullets);
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
            this.Controls.Add(this.checkBoxWeaponStamina);
            this.Controls.Add(this.checkBoxWeaponDefense);
            this.Controls.Add(this.checkBoxWeaponShieldSplit);
            this.Controls.Add(this.checkBoxWeaponFistNo);
            this.Controls.Add(this.chkWeaponMoveset);
            this.Controls.Add(this.checkBoxDoTrueRandom);
            this.Controls.Add(this.checkBoxUniversalizeCasters);
            this.Controls.Add(this.checkBoxRandomizeSpellRequirements);
            this.Controls.Add(this.checkBoxRandomizeSpellSlotSize);
            this.Controls.Add(this.checkBoxRandomizeSpellQuantity);
            this.Controls.Add(this.checkBoxArmorWeight);
            this.Controls.Add(this.checkBoxArmorPoise);
            this.Controls.Add(this.checkBoxArmorResistance);
            this.Controls.Add(this.checkBoxArmorspEffect);
            this.Controls.Add(this.checkBoxStartingGifts);
            this.Controls.Add(this.checkBoxPreventSpellGifts);
            this.Controls.Add(this.checkBoxStartingGiftsAmount);
            this.Controls.Add(this.checkBoxStartingClasses);
            this.Controls.Add(this.chkBullets);
            this.Controls.Add(this.chkKnockback);
            this.Controls.Add(this.chkSpeffects);
            this.Controls.Add(this.chkWeaponSpeffects);
            this.Controls.Add(this.chkVoices);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtGamePath);
            this.Controls.Add(this.lblGamePath);
            this.Controls.Add(this.lblSeed);
            this.Controls.Add(this.txtSeed);
            this.Controls.Add(this.gbEnemiesCategory);
            this.Controls.Add(this.gbOtherCategory);
            this.Controls.Add(this.gbSharedCategory);
            this.Controls.Add(this.gbArmorCategory);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.gbWeaponCategory);
            this.Controls.Add(this.gbSpellCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Paramdomizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbWeaponCategory.ResumeLayout(false);
            this.gbWeaponCategory.PerformLayout();
            this.gbSpellCategory.ResumeLayout(false);
            this.gbSpellCategory.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkWeaponSpeffects;
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
        private System.Windows.Forms.CheckBox checkBoxWeaponStamina;
        private System.Windows.Forms.CheckBox checkBoxWeaponDefense;
        private System.Windows.Forms.CheckBox checkBoxWeaponShieldSplit;
        private System.Windows.Forms.CheckBox checkBoxWeaponFistNo;
        private System.Windows.Forms.CheckBox checkBoxDoTrueRandom;
        private System.Windows.Forms.CheckBox checkBoxUniversalizeCasters;
        private System.Windows.Forms.CheckBox checkBoxRandomizeSpellRequirements;
        private System.Windows.Forms.CheckBox checkBoxRandomizeSpellSlotSize;
        private System.Windows.Forms.CheckBox checkBoxRandomizeSpellQuantity;
        private System.Windows.Forms.CheckBox checkBoxArmorWeight;
        private System.Windows.Forms.CheckBox checkBoxArmorPoise;
        private System.Windows.Forms.CheckBox checkBoxArmorResistance;
        private System.Windows.Forms.CheckBox checkBoxArmorspEffect;
        private System.Windows.Forms.CheckBox checkBoxStartingGifts;
        private System.Windows.Forms.CheckBox checkBoxPreventSpellGifts;
        private System.Windows.Forms.CheckBox checkBoxNerfHumanityBullets;
        private System.Windows.Forms.CheckBox checkBoxStartingGiftsAmount;
        private System.Windows.Forms.CheckBox checkBoxStartingClasses;
        private System.Windows.Forms.CheckBox checkBoxForceUseableStartSpells;
        private System.Windows.Forms.CheckBox checkBoxForceUseableStartWeapons;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.GroupBox gbWeaponCategory;
        private System.Windows.Forms.GroupBox gbSpellCategory;
        private System.Windows.Forms.GroupBox gbEnemiesCategory;
        private System.Windows.Forms.GroupBox gbOtherCategory;
        private System.Windows.Forms.GroupBox gbSharedCategory;
        private System.Windows.Forms.GroupBox gbArmorCategory;
        private System.Windows.Forms.Label lblVersion;
    }
}

