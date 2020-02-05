﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MeowDSIO;
using MeowDSIO.DataFiles;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Paramdomizer
{
    public partial class Form1 : Form
    {
        string gameDirectory = "";
        Form2 TRForm;

        public Form1()
        {
            InitializeComponent();
            TRForm = new Form2();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //check if running exe from data directory
            gameDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            if (File.Exists(gameDirectory + (checkBoxRemaster.Checked ? "\\DarkSoulsRemastered.exe" : "\\DARKSOULS.exe")))
            {
                //exe is in a valid game directory, just use this as the path instead of asking for input
                txtGamePath.Text = gameDirectory;
                txtGamePath.ReadOnly = true;

                if (!File.Exists(gameDirectory + "\\param\\GameParam\\GameParam.parambnd" + (checkBoxRemaster.Checked ? ".dcx" : "")))
                {
                    if (checkBoxRemaster.Checked)
                    {
                        //invalid directory
                        lblMessage.Text = "Invalid Dark Souls: Remastered game directory; No GameParam found.";
                    }
                    else
                    {
                        //user hasn't unpacked their game
                        lblMessage.Text = "You don't seem to have an unpacked Dark Souls: Prepare to Die Edition installation. Please run UDSFM and come back :)";
                    }
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                }
                else if (!File.Exists(gameDirectory + "\\msg\\ENGLISH\\item.msgbnd" + (checkBoxRemaster.Checked ? ".dcx" : "")) || !File.Exists(gameDirectory + "\\msg\\ENGLISH\\menu.msgbnd" + (checkBoxRemaster.Checked ? ".dcx" : "")))
                {
                    if (checkBoxRemaster.Checked)
                    {
                        //invalid directory
                        lblMessage.Text = "Invalid Dark Souls: Remastered game directory; Missing ENGLISH msgbnd files.";
                    }
                    else
                    {
                        //user hasn't unpacked their game
                        lblMessage.Text = "You don't seem to have an unpacked Dark Souls: Prepare to Die Edition installation. Please run UDSFM and come back :)";
                    }
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }

        private void btnOpenFolderDialog_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtGamePath.Text = dialog.FileName;
                gameDirectory = dialog.FileName;

                lblMessage.Text = "";
                lblMessage.Visible = true;

                if (!File.Exists(gameDirectory + (checkBoxRemaster.Checked ? "\\DarkSoulsRemastered.exe" : "\\DARKSOULS.exe")))
                {
                    lblMessage.Text = $"Not a valid {(checkBoxRemaster.Checked ? "Dark Souls: Remastered" : "Dark Souls: Prepare to Die Edition")} Data directory.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else if (!File.Exists(gameDirectory + "\\param\\GameParam\\GameParam.parambnd" + (checkBoxRemaster.Checked ? ".dcx" : "")))
                {
                    if (checkBoxRemaster.Checked)
                    {
                        //invalid directory
                        lblMessage.Text = "Invalid Dark Souls: Remastered game directory; No GameParam found.";
                    }
                    else
                    {
                        //user hasn't unpacked their game
                        lblMessage.Text = "You don't seem to have an unpacked Dark Souls: Prepare to Die Edition installation. Please run UDSFM and come back :)";
                    }
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else if (!File.Exists(gameDirectory + "\\msg\\ENGLISH\\item.msgbnd" + (checkBoxRemaster.Checked ? ".dcx" : "")) || !File.Exists(gameDirectory + "\\msg\\ENGLISH\\menu.msgbnd" + (checkBoxRemaster.Checked ? ".dcx" : "")))
                {
                    if (checkBoxRemaster.Checked)
                    {
                        //invalid directory
                        lblMessage.Text = "Invalid Dark Souls: Remastered game directory; Missing ENGLISH msgbnd files.";
                    }
                    else
                    {
                        //user hasn't unpacked their game
                        lblMessage.Text = "You don't seem to have an unpacked Dark Souls: Prepare to Die Edition installation. Please run UDSFM and come back :)";
                    }
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            //check that entered path is valid
            gameDirectory = txtGamePath.Text;

            //reset message label
            lblMessage.Text = "";
            lblMessage.ForeColor = new Color();
            lblMessage.Visible = true;

            if (!File.Exists(gameDirectory + (checkBoxRemaster.Checked ? "\\DarkSoulsRemastered.exe" : "\\DARKSOULS.exe")))
            {
                lblMessage.Text = $"Not a valid {(checkBoxRemaster.Checked ? "Dark Souls: Remastered" : "Dark Souls: Prepare to Die Edition")} Data directory.";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            else if (!File.Exists(gameDirectory + "\\param\\GameParam\\GameParam.parambnd" + (checkBoxRemaster.Checked ? ".dcx" : "")))
            {
                if (checkBoxRemaster.Checked)
                {
                    //invalid directory
                    lblMessage.Text = "Invalid Dark Souls: Remastered game directory; No GameParam found.";
                }
                else
                {
                    //user hasn't unpacked their game
                    lblMessage.Text = "You don't seem to have an unpacked Dark Souls: Prepare to Die Edition installation. Please run UDSFM and come back :)";
                }
                lblMessage.ForeColor = Color.Red;
                return;
            }
            else if (!File.Exists(gameDirectory + "\\msg\\ENGLISH\\item.msgbnd" + (checkBoxRemaster.Checked ? ".dcx" : "")) || !File.Exists(gameDirectory + "\\msg\\ENGLISH\\menu.msgbnd" + (checkBoxRemaster.Checked ? ".dcx" : "")))
            {
                if (checkBoxRemaster.Checked)
                {
                    //invalid directory
                    lblMessage.Text = "Invalid Dark Souls: Remastered game directory; Missing ENGLISH msgbnd files.";
                }
                else
                {
                    //user hasn't unpacked their game
                    lblMessage.Text = "You don't seem to have an unpacked Dark Souls: Prepare to Die Edition installation. Please run UDSFM and come back :)";
                }
                lblMessage.ForeColor = Color.Red;
                return;
            }

            //update label on a new thread
            Progress<string> progress = new Progress<string>(s => lblMessage.Text = s);
            await Task.Factory.StartNew(() => UiThread.WriteToInfoLabel(progress));

            //generate a seed if needed
            if (txtSeed.Text == "")
            {
                string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random seedGen = new Random();
                for (int i = 0; i < 15; i++)
                {
                    txtSeed.Text += validChars[seedGen.Next(validChars.Length)];
                }
            }

            string seed = txtSeed.Text;

            //restore files from backup if load from backup is enabled
            if (checkBoxLoadFromBackup.Checked && File.Exists(gameDirectory + $"\\param\\GameParam\\GameParam.parambnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak"))
            {
                File.Delete(gameDirectory + $"\\param\\GameParam\\GameParam.parambnd{(checkBoxRemaster.Checked ? ".dcx" : "")}");
                File.Copy(gameDirectory + $"\\param\\GameParam\\GameParam.parambnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak",
                    gameDirectory + $"\\param\\GameParam\\GameParam.parambnd{(checkBoxRemaster.Checked ? ".dcx" : "")}");
            }
            if (checkBoxLoadFromBackup.Checked && File.Exists(gameDirectory + $"\\msg\\ENGLISH\\menu.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak"))
            {
                File.Delete(gameDirectory + $"\\msg\\ENGLISH\\menu.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}");
                File.Copy(gameDirectory + $"\\msg\\ENGLISH\\menu.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak",
                    gameDirectory + $"\\msg\\ENGLISH\\menu.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}");
            }

            //create backup of gameparam
            if (!File.Exists(gameDirectory + $"\\param\\GameParam\\GameParam.parambnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak"))
            {
                File.Copy(gameDirectory + $"\\param\\GameParam\\GameParam.parambnd{(checkBoxRemaster.Checked ? ".dcx" : "")}", 
                    gameDirectory + $"\\param\\GameParam\\GameParam.parambnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak");
                lblMessage.Text = $"Backed up GameParam.parambnd{(checkBoxRemaster.Checked ? ".dcx" : "")} \n\n";
                lblMessage.ForeColor = Color.Black;
                lblMessage.Visible = true;
            }
            //create backups of msgbnds
            if (!File.Exists(gameDirectory + $"\\msg\\ENGLISH\\item.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak"))
            {
                File.Copy(gameDirectory + $"\\msg\\ENGLISH\\item.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}",
                    gameDirectory + $"\\msg\\ENGLISH\\item.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak");
                lblMessage.Text = $"Backed up item.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")} \n\n";
                lblMessage.ForeColor = Color.Black;
                lblMessage.Visible = true;
            }
            if(!File.Exists(gameDirectory + $"\\msg\\ENGLISH\\menu.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak"))
            {
                File.Copy(gameDirectory + $"\\msg\\ENGLISH\\menu.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}",
                    gameDirectory + $"\\msg\\ENGLISH\\menu.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}.paramdomizerbak");
                lblMessage.Text = $"Backed up menu.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")} \n\n";
                lblMessage.ForeColor = Color.Black;
                lblMessage.Visible = true;
            }

            PARAMBND gameparamBnd = DataFile.LoadFromFile<PARAMBND>(gameDirectory + $"\\param\\GameParam\\GameParam.parambnd{(checkBoxRemaster.Checked ? ".dcx" : "")}");

            MSGBND menuMSGBND = DataFile.LoadFromFile<MSGBND>(gameDirectory + $"\\msg\\ENGLISH\\menu.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}");
            MSGBND itemMSGBND = DataFile.LoadFromFile<MSGBND>(gameDirectory + $"\\msg\\ENGLISH\\item.msgbnd{(checkBoxRemaster.Checked ? ".dcx" : "")}");


            gameparamBnd.ApplyDefaultParamDefs();

            //Hash seed so people can use meme seeds
            Random r = new Random(seed.GetHashCode());

            //used for name assignment when randomizing starting gifts
            List<int> itemStartingGifts = new List<int>();
            List<int> itemStartingGiftsAmount = new List<int>();
            List<int> ringStartingGifts = new List<int>();

            int[] startingSpells = new int[3]; //index 0 is sorcerer, 1 is pyromancer, 2 is cleric

            int[,] startingWeapons = new int[10, 2]; //mainhand first then offhand weapons
            int[] secondaryStartingWeapons = new int[4]; //starts with hunter, next 3 indexes are the caster's casting item

            int[] classLevels = new int[10];
            int[,] classStats = new int[10, 8];

            foreach (var paramBndEntry in gameparamBnd)
            {
                var paramShortName = paramBndEntry.Name;
                var paramFile = paramBndEntry.Param;
                if (paramShortName.Contains("AtkParam_Npc"))
                {
                    List<int> allSpEffects = new List<int>();
                    List<float> allKnockbackDists = new List<float>();
                    List<int> allDmgLevels = new List<int>();
                    List<float> allHitRadius = new List<float>();

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "spEffectId0")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allSpEffects.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "knockbackDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allKnockbackDists.Add((float)(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "dmgLevel")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allDmgLevels.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "hit0_Radius")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allHitRadius.Add((float)(prop.GetValue(cell, null)));
                            }
                        }
                    }

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "knockbackDist")
                            {
                                int randomIndex = r.Next(allKnockbackDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkKnockback.Checked)
                                {
                                    prop.SetValue(cell, allKnockbackDists[randomIndex], null);
                                }

                                allKnockbackDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "dmgLevel")
                            {
                                int randomIndex = r.Next(allDmgLevels.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkStaggerLevels.Checked)
                                {
                                    prop.SetValue(cell, allDmgLevels[randomIndex], null);
                                }

                                allDmgLevels.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "hit0_Radius")
                            {
                                int randomIndex = r.Next(allHitRadius.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkHitboxSizes.Checked)
                                {
                                    prop.SetValue(cell, allHitRadius[randomIndex], null);
                                }

                                allHitRadius.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "spEffectId0")
                            {
                                int randomIndex = r.Next(allSpEffects.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkSpeffects.Checked)
                                {
                                    prop.SetValue(cell, allSpEffects[randomIndex], null);
                                }

                                allSpEffects.RemoveAt(randomIndex);
                            }
                        }
                    }
                }
                else if (paramShortName.Contains("AtkParam_Pc"))
                {
                    List<float> allKnockbackDists = new List<float>();
                    List<int> allDmgLevels = new List<int>();
                    List<float> allHitRadius = new List<float>();

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "knockbackDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allKnockbackDists.Add((float)(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "dmgLevel")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allDmgLevels.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "hit0_Radius")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allHitRadius.Add((float)(prop.GetValue(cell, null)));
                            }
                        }
                    }

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "knockbackDist")
                            {
                                int randomIndex = r.Next(allKnockbackDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkKnockback.Checked)
                                {
                                    prop.SetValue(cell, allKnockbackDists[randomIndex], null);
                                }

                                allKnockbackDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "dmgLevel")
                            {
                                int randomIndex = r.Next(allDmgLevels.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkStaggerLevels.Checked)
                                {
                                    prop.SetValue(cell, allDmgLevels[randomIndex], null);
                                }

                                allDmgLevels.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "hit0_Radius")
                            {
                                int randomIndex = r.Next(allHitRadius.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkHitboxSizes.Checked)
                                {
                                    prop.SetValue(cell, allHitRadius[randomIndex], null);
                                }

                                allHitRadius.RemoveAt(randomIndex);
                            }
                        }
                    }
                }
                else if (paramFile.ID == "NPC_PARAM_ST")
                {
                    List<int> allSPeffects = new List<int>();
                    List<float> allTurnVelocities = new List<float>();
                    List<int> allStaminas = new List<int>();
                    List<int> allStaminaRegens = new List<int>();

                    //Dont randomize these speffects
                    int[] invalidSpeffects = { 0, 5300, 7001, 7002, 7003, 7004, 7005, 7006, 7007, 7008, 7009, 7010, 7011, 7012, 7013, 7014, 7015 };

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            //spEffect rando
                            if (cell.Def.Name.StartsWith("spEffectID"))
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                int speffectCheck = Convert.ToInt32(prop.GetValue(cell, null));
                                if (!invalidSpeffects.Contains(speffectCheck))
                                {
                                    allSPeffects.Add(speffectCheck);
                                }
                            }
                            else if (cell.Def.Name == "turnVellocity")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allTurnVelocities.Add((float)(prop.GetValue(cell, null)));
                            }
                            /*else if (cell.Def.Name == "stamina")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allStaminas.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "staminaRecoverBaseVel")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allStaminaRegens.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }*/
                        }
                    }

                    //loop again to set a random value per entry
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name.StartsWith("spEffectID"))
                            {
                                int randomIndex = r.Next(allSPeffects.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                int speffectCheck = Convert.ToInt32(prop.GetValue(cell, null));

                                if (!invalidSpeffects.Contains(speffectCheck))
                                {
                                    if (chkSpeffects.Checked)
                                    {
                                        prop.SetValue(cell, allSPeffects[randomIndex], null);
                                    }
                                    allSPeffects.RemoveAt(randomIndex);
                                }
                            }
                            else if (cell.Def.Name == "turnVellocity")
                            {
                                int randomIndex = r.Next(allTurnVelocities.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkTurnSpeeds.Checked)
                                {
                                    prop.SetValue(cell, allTurnVelocities[randomIndex], null);
                                }

                                allTurnVelocities.RemoveAt(randomIndex);
                            }
                            //else if (cell.Def.Name == "stamina")
                            //{
                            //    int randomIndex = r.Next(allStaminas.Count);
                            //    Type type = cell.GetType();
                            //    PropertyInfo prop = type.GetProperty("Value");

                            //    if (chkStaminaRegen.Checked)
                            //    {
                            //        prop.SetValue(cell, allStaminas[randomIndex], null);
                            //    }

                            //    allStaminas.RemoveAt(randomIndex);
                            //}
                            //else if (cell.Def.Name == "staminaRecoverBaseVal")
                            //{
                            //    int randomIndex = r.Next(allStaminaRegens.Count);
                            //    Type type = cell.GetType();
                            //    PropertyInfo prop = type.GetProperty("Value");

                            //    if (chkStaminaRegen.Checked)
                            //    {
                            //        prop.SetValue(cell, allStaminaRegens[randomIndex], null);
                            //    }

                            //    allStaminaRegens.RemoveAt(randomIndex);
                            //}
                        }
                    }
                }
                else if (paramFile.ID == "NPC_THINK_PARAM_ST")
                {
                    List<float> allNearDists = new List<float>();
                    List<float> allMidDists = new List<float>();
                    List<float> allFarDists = new List<float>();
                    List<float> allOutDists = new List<float>();
                    List<int> allEye_dists = new List<int>();
                    List<int> allEar_dists = new List<int>();
                    List<int> allNose_dists = new List<int>();
                    List<int> allMaxBackhomeDists = new List<int>();
                    List<int> allBackhomeDists = new List<int>();
                    List<int> allBackhomeBattleDists = new List<int>();
                    List<int> allBackHome_LookTargetTimes = new List<int>();
                    List<int> allBackHome_LookTargetDists = new List<int>();
                    List<int> allBattleStartDists = new List<int>();
                    List<int> allEye_angXs = new List<int>();
                    List<int> allEye_angYs = new List<int>();
                    List<int> allEar_angXs = new List<int>();
                    List<int> allEar_angYs = new List<int>();
                    List<int> allSightTargetForgetTimes = new List<int>();
                    List<int> allSoundTargetForgetTimes = new List<int>();

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "nearDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allNearDists.Add((float)(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "midDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allMidDists.Add((float)(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "farDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allFarDists.Add((float)(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "outDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allOutDists.Add((float)(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "eye_dist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allEye_dists.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "ear_dist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allEar_dists.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "nose_dist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allNose_dists.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "maxBackhomeDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allMaxBackhomeDists.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "backhomeDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allBackhomeDists.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "backhomeBattleDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allBackhomeBattleDists.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "BackHome_LookTargetTime")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allBackHome_LookTargetTimes.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "BackHome_LookTargetDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allBackHome_LookTargetDists.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "BattleStartDist")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allBattleStartDists.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "eye_angX")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allEye_angXs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "eye_angY")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allEye_angYs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "ear_angX")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allEar_angXs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "ear_angY")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allEar_angYs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "SightTargetForgetTime")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allSightTargetForgetTimes.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                            else if (cell.Def.Name == "SoundTargetForgetTime")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allSoundTargetForgetTimes.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                        }
                    }
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            int[] hydraIds = { 353000, 353001, 353100, 353200 };
                            if (cell.Def.Name == "nearDist")
                            {
                                int randomIndex = r.Next(allNearDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allNearDists[randomIndex], null);
                                }

                                allNearDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "midDist")
                            {
                                int randomIndex = r.Next(allMidDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allMidDists[randomIndex], null);
                                }

                                allMidDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "farDist")
                            {
                                int randomIndex = r.Next(allFarDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allFarDists[randomIndex], null);
                                }

                                allFarDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "outDist")
                            {
                                int randomIndex = r.Next(allOutDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allOutDists[randomIndex], null);
                                }

                                allOutDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "eye_dist")
                            {
                                int randomIndex = r.Next(allEye_dists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allEye_dists[randomIndex], null);
                                }

                                allEye_dists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "ear_dist")
                            {
                                int randomIndex = r.Next(allEar_dists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allEar_dists[randomIndex], null);
                                }

                                allEar_dists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "nose_dist")
                            {
                                int randomIndex = r.Next(allNose_dists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allNose_dists[randomIndex], null);
                                }

                                allNose_dists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "maxBackhomeDist")
                            {
                                int randomIndex = r.Next(allMaxBackhomeDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allMaxBackhomeDists[randomIndex], null);
                                }

                                allMaxBackhomeDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "backhomeDist")
                            {
                                int randomIndex = r.Next(allBackhomeDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allBackhomeDists[randomIndex], null);
                                }

                                allBackhomeDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "backhomeBattleDist")
                            {
                                int randomIndex = r.Next(allBackhomeBattleDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allBackhomeBattleDists[randomIndex], null);
                                }

                                allBackhomeBattleDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "BackHome_LookTargetTime")
                            {
                                int randomIndex = r.Next(allBackHome_LookTargetTimes.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allBackHome_LookTargetTimes[randomIndex], null);
                                }

                                allBackHome_LookTargetTimes.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "BackHome_LookTargetDist")
                            {
                                int randomIndex = r.Next(allBackHome_LookTargetDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allBackHome_LookTargetDists[randomIndex], null);
                                }

                                allBackHome_LookTargetDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "BattleStartDist")
                            {
                                int randomIndex = r.Next(allBattleStartDists.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allBattleStartDists[randomIndex], null);
                                }

                                allBattleStartDists.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "eye_angX")
                            {
                                int randomIndex = r.Next(allEye_angXs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allEye_angXs[randomIndex], null);
                                }

                                allEye_angXs.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "eye_angY")
                            {
                                int randomIndex = r.Next(allEye_angYs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allEye_angYs[randomIndex], null);
                                }

                                allEye_angYs.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "ear_angX")
                            {
                                int randomIndex = r.Next(allEar_angXs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allEar_angXs[randomIndex], null);
                                }

                                allEar_angXs.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "ear_angY")
                            {
                                int randomIndex = r.Next(allEar_angYs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allEar_angYs[randomIndex], null);
                                }

                                allEar_angYs.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "SightTargetForgetTime")
                            {
                                int randomIndex = r.Next(allSightTargetForgetTimes.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allSightTargetForgetTimes[randomIndex], null);
                                }

                                allSightTargetForgetTimes.RemoveAt(randomIndex);
                            }
                            else if (cell.Def.Name == "SoundTargetForgetTime")
                            {
                                int randomIndex = r.Next(allSoundTargetForgetTimes.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkAggroRadius.Checked && !hydraIds.Contains(paramRow.ID))
                                {
                                    prop.SetValue(cell, allSoundTargetForgetTimes[randomIndex], null);
                                }

                                allSoundTargetForgetTimes.RemoveAt(randomIndex);
                            }
                        }
                    }
                }
                else if (paramFile.ID == "EQUIP_PARAM_ACCESSORY_ST")
                {
                    List<int> allRefIds = new List<int>();
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "refId")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allRefIds.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }
                        }
                    }

                    //loop again to set a random value per entry
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "refId")
                            {
                                int randomIndex = r.Next(allRefIds.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                if (chkRingSpeffects.Checked)
                                {
                                    prop.SetValue(cell, allRefIds[randomIndex], null);
                                }

                                allRefIds.RemoveAt(randomIndex);
                            }
                        }
                    }
                }
                else if (paramFile.ID == "EQUIP_PARAM_GOODS_ST")
                {
                    List<int> allUseAnimations = new List<int>();
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "goodsUseAnim")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                int animid = Convert.ToInt32(prop.GetValue(cell, null));
                                //the empty estus animation - prevents using item
                                if (animid != 254)
                                {
                                    allUseAnimations.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                            }
                        }
                    }

                    //loop again to set a random value per entry
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "goodsUseAnim")
                            {
                                int randomIndex = r.Next(allUseAnimations.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");
                                int animid = Convert.ToInt32(prop.GetValue(cell, null));

                                if (animid != 254)
                                {
                                    if (chkItemAnimations.Checked)
                                    {
                                        prop.SetValue(cell, allUseAnimations[randomIndex], null);
                                    }

                                    allUseAnimations.RemoveAt(randomIndex);
                                }
                            }
                        }
                    }
                }
                else if (paramFile.ID == "EQUIP_PARAM_PROTECTOR_ST")
                {
                    //empty first id, head, body, arms, legs, head(no traveling gear), body(no traveling gear), arms(no traveling gear), legs(no traveling gear),
                    //dragon head, dragon body, dragon arms, dragon legs, egg head, hatched egg
                    List<int> allArmorlessIds = new List<int>() {1, 900000, 901000, 902000, 903000, 950000, 951000, 952000, 953000,
                                                                1000000, 1001000, 1002000, 1003000, 1010000, 1020000};
                    //add hairs to armorless ids
                    for (int i = 10; i < 51; i++)
                    {
                        allArmorlessIds.Add(i * 100);
                    }

                    List<int> allArmorWeights = new List<int>();
                    List<int> allArmorPoise = new List<int>();
                    List<int> allArmorSpEffect = new List<int>(); //using resident effect 3; effect 2 indicates it's armor and effect 1 I am unsure of it's purpose

                    List<int> allArmorDefensePhys = new List<int>();
                    List<int> allArmorDefenseMagic = new List<int>();
                    List<int> allArmorDefenseFire = new List<int>();
                    List<int> allArmorDefenseThunder = new List<int>();
                    List<int> allArmorDefenseSlash = new List<int>();
                    List<int> allArmorDefenseBlow = new List<int>();
                    List<int> allArmorDefenseThrust = new List<int>();
                    List<int> allArmorResistPoison = new List<int>();
                    List<int> allArmorResistDisease = new List<int>();
                    List<int> allArmorResistBlood = new List<int>();
                    List<int> allArmorResistCurse = new List<int>();

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            //if not an armorless armor (if not naked)
                            if (!allArmorlessIds.Contains(paramRow.ID))
                            {
                                if (cell.Def.Name == "weight")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorWeights.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "saDurability") //poise
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorPoise.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "residentSpEffectId3")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorSpEffect.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "defensePhysics")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorDefensePhys.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "defenseMagic")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorDefenseMagic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "defenseFire")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorDefenseFire.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "defenseThunder")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorDefenseThunder.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "defenseSlash")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorDefenseSlash.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "defenseBlow")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorDefenseBlow.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "defenseThrust")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorDefenseThrust.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "resistPoison")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorResistPoison.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "resistDisease")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorResistDisease.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "resistBlood")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorResistBlood.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "resistCurse")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allArmorResistCurse.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                            }
                        }
                    }

                    //loop again to set a random value per entry
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            //if not an armorless armor (if not naked)
                            if (!allArmorlessIds.Contains(paramRow.ID))
                            {
                                if (cell.Def.Name == "weight")
                                {
                                    int randomIndex = r.Next(allArmorWeights.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorWeight.Checked)
                                    {
                                        if(checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorWeight.Checked)
                                        {
                                            //randomized by 0.1 steps
                                            prop.SetValue(cell, r.Next(196) / 10.0, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorWeights[randomIndex], null);
                                        }
                                    }

                                    allArmorWeights.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "saDurability") //poise
                                {
                                    int randomIndex = r.Next(allArmorPoise.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorPoise.Checked)
                                    {
                                        if(checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorPoise.Checked)
                                        {
                                            // 1/2 chance to get poise
                                            if(r.Next(2) == 0)
                                            {
                                                prop.SetValue(cell, r.Next(46) + 2, null);
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, 0, null);
                                            }
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorPoise[randomIndex], null);
                                        }
                                    }

                                    allArmorPoise.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "residentSpEffectId3")
                                {
                                    int randomIndex = r.Next(allArmorSpEffect.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorspEffect.Checked)
                                    {
                                        prop.SetValue(cell, allArmorSpEffect[randomIndex], null);
                                    }

                                    allArmorSpEffect.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "defensePhysics")
                                {
                                    int randomIndex = r.Next(allArmorDefensePhys.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if(checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(104) + 3, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorDefensePhys[randomIndex], null);
                                        }
                                    }

                                    allArmorDefensePhys.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "defenseMagic")
                                {
                                    int randomIndex = r.Next(allArmorDefenseMagic.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(56) + 1, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorDefenseMagic[randomIndex], null);
                                        }
                                    }

                                    allArmorDefenseMagic.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "defenseFire")
                                {
                                    int randomIndex = r.Next(allArmorDefenseFire.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(65) + 2, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorDefenseFire[randomIndex], null);
                                        }
                                    }

                                    allArmorDefenseFire.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "defenseThunder")
                                {
                                    int randomIndex = r.Next(allArmorDefenseThunder.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(58) + 2, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorDefenseThunder[randomIndex], null);
                                        }
                                    }

                                    allArmorDefenseThunder.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "defenseSlash")
                                {
                                    int randomIndex = r.Next(allArmorDefenseSlash.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(25) + 3, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorDefenseSlash[randomIndex], null);
                                        }
                                    }

                                    allArmorDefenseSlash.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "defenseBlow")
                                {
                                    int randomIndex = r.Next(allArmorDefenseBlow.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(116) - 48, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorDefenseBlow[randomIndex], null);
                                        }
                                    }

                                    allArmorDefenseBlow.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "defenseThrust")
                                {
                                    int randomIndex = r.Next(allArmorDefenseThrust.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(35) - 17, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorDefenseThrust[randomIndex], null);
                                        }
                                    }

                                    allArmorDefenseThrust.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "resistPoison")
                                {
                                    int randomIndex = r.Next(allArmorResistPoison.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(91) + 4, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorResistPoison[randomIndex], null);
                                        }
                                    }

                                    allArmorResistPoison.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "resistDisease")
                                {
                                    int randomIndex = r.Next(allArmorResistDisease.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(91) + 4, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorResistDisease[randomIndex], null);
                                        }
                                    }

                                    allArmorResistDisease.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "resistBlood")
                                {
                                    int randomIndex = r.Next(allArmorResistBlood.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            prop.SetValue(cell, r.Next(51) + 4, null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorResistBlood[randomIndex], null);
                                        }
                                    }

                                    allArmorResistBlood.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "resistCurse")
                                {
                                    int randomIndex = r.Next(allArmorResistCurse.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxArmorResistance.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRArmorResistance.Checked)
                                        {
                                            // 1/2 chance to get a value
                                            if(r.Next(1) == 0)
                                            {
                                                prop.SetValue(cell, r.Next(58), null);
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, 0, null);
                                            }
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allArmorResistCurse[randomIndex], null);
                                        }
                                    }

                                    allArmorResistCurse.RemoveAt(randomIndex);
                                }
                            }
                        }
                    }
                }
                else if (paramFile.ID == "EQUIP_PARAM_WEAPON_ST")
                {
                    //loop through all entries once to get list of values
                    List<int> allWepmotionCats = new List<int>();
                    List<int> allWepmotion1hCats = new List<int>();
                    List<int> allWepmotion2hCats = new List<int>();
                    List<int> allspAtkcategories = new List<int>();
                    List<int> allAttackBasePhysic = new List<int>(); //base damage
                    List<int> allAttackBaseMagic = new List<int>();
                    List<int> allAttackBaseFire = new List<int>();
                    List<int> allAttackBaseThunder = new List<int>();
                    List<int> allAttackCorrectStrength = new List<int>(); //scaling
                    List<int> allAttackCorrectAgility = new List<int>();
                    List<int> allAttackCorrectMagic = new List<int>();
                    List<int> allAttackCorrectFaith = new List<int>();
                    List<int> allCastingCorrectMagic = new List<int>();
                    List<int> allCastingCorrectFaith = new List<int>();
                    List<int> allAttackProperStrength = new List<int>(); //minimum stats
                    List<int> allAttackProperAgility = new List<int>();
                    List<int> allAttackProperMagic = new List<int>();
                    List<int> allAttackProperFaith = new List<int>();
                    List<int> allAttackBaseStamina = new List<int>();
                    List<Double> allWepWeight = new List<Double>(); //weapon weight
                    List<int> allEquipModelIds = new List<int>();
                    List<int> allspEffectBehavior = new List<int>(); //special effects like bleed that effect the target
                    List<int> allspEffectBehavior2 = new List<int>(); //secondary special effects like the lifehunt scythe's hp
                    List<int> allresidentspEffect = new List<int>(); //special effects like grass crest shield that effect the user
                    List<int> allPhysicalGuard = new List<int>();
                    List<int> allMagicGuard = new List<int>();
                    List<int> allFireGuard = new List<int>();
                    List<int> allThunderGuard = new List<int>();
                    List<int> allGuardStability = new List<int>();
                    List<int> allPoisonResist = new List<int>();
                    List<int> allDiseaseResist = new List<int>();
                    List<int> allBloodResist = new List<int>();
                    List<int> allCurseResist = new List<int>();
                    List<int> allGuardBaseRepel = new List<int>();
                    List<int> allAttackBaseRepel = new List<int>();

                    //only assign shield ids if shield split is activated
                    List<int> allShieldIDs = new List<int>();
                    if(checkBoxWeaponShieldSplit.Checked)
                    {
                        allShieldIDs.Add(904000); //dark hand
                        //shields, indexes 1400000 to 1410800; steps of 1000
                        for (int i = 0; i < 11; i++)
                        {
                            int x = 1400000 + (i * 1000); //starting index of the shield

                            //skip the following indexes (no shield there)
                            if (x != 1407000)
                            {
                                allShieldIDs.Add(x); //regular variant
                                allShieldIDs.Add(x + 100); //crystal variant
                                allShieldIDs.Add(x + 200); //lightning variant
                                allShieldIDs.Add(x + 400); //magic variant
                                allShieldIDs.Add(x + 600); //divine variant
                                allShieldIDs.Add(x + 800); //fire variant
                            }
                        }
                        //crystal ring shields, indexes 1411000 to 1414600; steps of 100
                        for (int i = 0; i < 37; i++)
                        {
                            int x = 1411000 + (i * 100); //starting index of the shield
                            allShieldIDs.Add(x);
                        }
                        //shields, indexes 1450000 to 1455800; steps of 1000
                        for (int i = 0; i < 6; i++)
                        {
                            int x = 1450000 + (i * 1000); //starting index of the shield

                            allShieldIDs.Add(x); //regular variant
                            allShieldIDs.Add(x + 100); //crystal variant
                            allShieldIDs.Add(x + 200); //lightning variant
                            allShieldIDs.Add(x + 400); //magic variant
                            allShieldIDs.Add(x + 600); //divine variant
                            allShieldIDs.Add(x + 800); //fire variant
                        }
                        allShieldIDs.Add(1456000); //crest shield
                        allShieldIDs.Add(1457000); //dragon crest shield
                        //shields, indexes 1460000 to 1462800; steps of 1000
                        for (int i = 0; i < 3; i++)
                        {
                            int x = 1460000 + (i * 1000); //starting index of the shield

                            allShieldIDs.Add(x); //regular variant
                            allShieldIDs.Add(x + 100); //crystal variant
                            allShieldIDs.Add(x + 200); //lightning variant
                            allShieldIDs.Add(x + 400); //magic variant
                            allShieldIDs.Add(x + 600); //divine variant
                            allShieldIDs.Add(x + 800); //fire variant
                        }
                        //shields, indexes 1470000 to 1477800; steps of 1000
                        for (int i = 0; i < 8; i++)
                        {
                            int x = 1470000 + (i * 1000); //starting index of the shield

                            allShieldIDs.Add(x); //regular variant
                            //skip the following indexes (shields have no variants)
                            if (x != 1471000 && x != 1473000 && x != 1474000)
                            {
                                allShieldIDs.Add(x + 100); //crystal variant
                                allShieldIDs.Add(x + 200); //lightning variant
                                allShieldIDs.Add(x + 400); //magic variant
                                allShieldIDs.Add(x + 600); //divine variant
                                allShieldIDs.Add(x + 800); //fire variant
                            }
                        }
                        allShieldIDs.Add(1478000); //gargoyle shield
                        allShieldIDs.Add(1478000 + 100); //crystal variant
                        allShieldIDs.Add(1478000 + 200); //lightning variant
                        allShieldIDs.Add(1478000 + 400); //magic variant
                        allShieldIDs.Add(1478000 + 600); //divine variant
                        allShieldIDs.Add(1478000 + 800); //fire variant
                        //shields, indexes 1500000 to 1502800; steps of 1000
                        for (int i = 0; i < 7; i++)
                        {
                            int x = 1500000 + (i * 1000); //starting index of the shield

                            allShieldIDs.Add(x); //regular variant
                            allShieldIDs.Add(x + 100); //crystal variant
                            allShieldIDs.Add(x + 200); //lightning variant
                            allShieldIDs.Add(x + 400); //magic variant
                            allShieldIDs.Add(x + 600); //divine variant
                            allShieldIDs.Add(x + 800); //fire variant
                        }
                        allShieldIDs.Add(1503000); //Stone Greatshield
                        allShieldIDs.Add(1505000); //Havel's Greatshield
                        allShieldIDs.Add(1506000); //Bonewheel shield
                        allShieldIDs.Add(1506000 + 100); //crystal variant
                        allShieldIDs.Add(1506000 + 200); //lightning variant
                        allShieldIDs.Add(1506000 + 400); //magic variant
                        allShieldIDs.Add(1506000 + 600); //divine variant
                        allShieldIDs.Add(1506000 + 800); //fire variant
                        //Greatshields of Artorias, indexes 1507000 to 1510600; steps of 100
                        for (int i = 0; i < 37; i++)
                        {
                            int x = 1507000 + (i * 100);
                        }
                        //shields, indexes 9001000 to 9003800; steps of 1000
                        for (int i = 0; i < 3; i++)
                        {
                            int x = 9001000 + (i * 1000); //starting index of the shield

                            allShieldIDs.Add(x); //regular variant
                            allShieldIDs.Add(x + 100); //crystal variant
                            allShieldIDs.Add(x + 200); //lightning variant
                            allShieldIDs.Add(x + 400); //magic variant
                            allShieldIDs.Add(x + 600); //divine variant
                            allShieldIDs.Add(x + 800); //fire variant
                        }
                        allShieldIDs.Add(9014000); //Cleansing Greatshield
                    }

                    //heads up to those who maintain this in the future:
                    //when treat shields seperately is enabled it runs a different set of
                    //loops for getting and setting values.
                    //Any changes you make to the original loops, make sure to make equivalent
                    //changes to the shield only versions of those loops

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        //if shield split is off or if not a shield
                        if(!checkBoxWeaponShieldSplit.Checked || !allShieldIDs.Contains(paramRow.ID))
                        {
                            //check not to randomize moveset of bows which defeats the purpose of bullet rando. Try disabling this check and see what happens maybe
                            MeowDSIO.DataTypes.PARAM.ParamCellValueRef bowCheckCell = paramRow.Cells.First(c => c.Def.Name == "bowDistRate");
                            Type bowchecktype = bowCheckCell.GetType();
                            PropertyInfo bowcheckprop = bowchecktype.GetProperty("Value");

                            //if dont modify fists is off or if not fist
                            if (!checkBoxWeaponFistNo.Checked || paramRow.ID != 900000)
                            {
                                if (Convert.ToInt32(bowcheckprop.GetValue(bowCheckCell, null)) < 0)
                                {
                                    foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                    {
                                        if (cell.Def.Name == "wepmotionCategory")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allWepmotionCats.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "wepmotionOneHandId")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allWepmotion1hCats.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "wepmotionBothHandId")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allWepmotion2hCats.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "spAtkcategory")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allspAtkcategories.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                }

                                foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                {
                                    if (cell.Def.Name == "equipModelId")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        allEquipModelIds.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                    else if (cell.Def.Name == "attackBasePhysics")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        allAttackBasePhysic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                    else if (cell.Def.Name == "attackBaseMagic")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        allAttackBaseMagic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                    else if (cell.Def.Name == "attackBaseFire")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        allAttackBaseFire.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                    else if (cell.Def.Name == "attackBaseThunder")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        allAttackBaseThunder.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                    else if (cell.Def.Name == "correctStrength")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        allAttackCorrectStrength.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                    else if (cell.Def.Name == "correctAgility")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        allAttackCorrectAgility.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                    else if (cell.Def.Name == "correctMagic")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        //if id is between sorcerer catalyst and velka's talisman (is a casting device); treat it's magic and faith correct differently
                                        if(paramRow.ID >= 1300000 && paramRow.ID <= 1367000)
                                        {
                                            allCastingCorrectMagic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else
                                        {
                                            allAttackCorrectMagic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "correctFaith")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        //if id is between sorcerer catalyst and velka's talisman (is a casting device); treat it's magic and faith correct differently
                                        if (paramRow.ID >= 1300000 && paramRow.ID <= 1367000)
                                        {
                                            allCastingCorrectFaith.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else
                                        {
                                            allAttackCorrectFaith.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "properStrength")
                                    {
                                        //if not straightsword hilt
                                        if(paramRow.ID != 212000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackProperStrength.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "properAgility")
                                    {
                                        //if not straightsword hilt
                                        if (paramRow.ID != 212000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackProperAgility.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "properMagic")
                                    {
                                        //if not straightsword hilt
                                        if (paramRow.ID != 212000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackProperMagic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "properFaith")
                                    {
                                        //if not straightsword hilt
                                        if (paramRow.ID != 212000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackProperFaith.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "weight")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        allWepWeight.Add(Convert.ToDouble(prop.GetValue(cell, null)));
                                    }
                                    else if (cell.Def.Name == "attackBaseStamina")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        allAttackBaseStamina.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                    else if (cell.Def.Name == "spEffectBehaviorId0")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allspEffectBehavior.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "spEffectBehaviorId1")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allspEffectBehavior2.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "residentSpEffectId")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allresidentspEffect.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "physGuardCutRate")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allPhysicalGuard.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "magGuardCutRate")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allMagicGuard.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "fireGuardCutRate")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allFireGuard.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "thunGuardCutRate")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allThunderGuard.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "staminaGuardDef")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allGuardStability.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "poisonGuardResist")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allPoisonResist.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "diseaseGuardResist")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allDiseaseResist.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "bloodGuardResist")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allBloodResist.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "curseGuardResist")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allCurseResist.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "guardBaseRepel")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allGuardBaseRepel.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                    else if (cell.Def.Name == "attackBaseRepel")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackBaseRepel.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //loop again to set a random value per entry
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        //if shield split is off or if not a shield
                        if (!checkBoxWeaponShieldSplit.Checked || !allShieldIDs.Contains(paramRow.ID))
                        {
                            MeowDSIO.DataTypes.PARAM.ParamCellValueRef bowCheckCell = paramRow.Cells.First(c => c.Def.Name == "bowDistRate");
                            Type bowchecktype = bowCheckCell.GetType();
                            PropertyInfo bowcheckprop = bowchecktype.GetProperty("Value");

                            //if dont modify fists is off or if not fist
                            if (!checkBoxWeaponFistNo.Checked || paramRow.ID != 900000)
                            {
                                if (Convert.ToInt32(bowcheckprop.GetValue(bowCheckCell, null)) < 0)
                                {
                                    foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                    {
                                        if (cell.Def.Name == "wepmotionCategory")
                                        {
                                            int randomIndex = r.Next(allWepmotionCats.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponMoveset.Checked)
                                            {
                                                prop.SetValue(cell, allWepmotionCats[randomIndex], null);
                                            }

                                            allWepmotionCats.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "wepmotionOneHandId")
                                        {
                                            int randomIndex = r.Next(allWepmotion1hCats.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponMoveset.Checked)
                                            {
                                                prop.SetValue(cell, allWepmotion1hCats[randomIndex], null);
                                            }

                                            allWepmotion1hCats.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "wepmotionBothHandId")
                                        {
                                            int randomIndex = r.Next(allWepmotion2hCats.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponMoveset.Checked)
                                            {
                                                prop.SetValue(cell, allWepmotion2hCats[randomIndex], null);
                                            }

                                            allWepmotion2hCats.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "spAtkcategory")
                                        {
                                            int randomIndex = r.Next(allspAtkcategories.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");

                                            if (chkWeaponMoveset.Checked)
                                            {
                                                prop.SetValue(cell, allspAtkcategories[randomIndex], null);
                                            }

                                            allspAtkcategories.RemoveAt(randomIndex);
                                        }
                                    }
                                }

                                int baseDamage = 0;
                                bool castsMagic = false;
                                List<int> damageTypes = new List<int>(); //values 0-3; phys, magic, fire, thunder
                                                                         //check if casts magic first
                                foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                {
                                    if (cell.Def.Name == "enableMagic:1" || cell.Def.Name == "enableVowMagic:1" || cell.Def.Name == "enableSorcery:1")
                                    {
                                        if (Convert.ToBoolean(cell.GetType().GetProperty("Value").GetValue(cell, null)) == true)
                                        {
                                            castsMagic = true;
                                        }
                                    }
                                }
                                foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                {
                                    if (cell.Def.Name == "equipModelId")
                                    {
                                        int randomIndex = r.Next(allEquipModelIds.Count);
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (chkWeaponModels.Checked)
                                        {
                                            prop.SetValue(cell, allEquipModelIds[randomIndex], null);
                                        }

                                        allEquipModelIds.RemoveAt(randomIndex);
                                    }
                                    else if (cell.Def.Name == "attackBasePhysics")
                                    {
                                        int randomIndex = r.Next(allAttackBasePhysic.Count);
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (chkWeaponDamage.Checked)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDamage.Checked)
                                            {
                                                //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the damage it does
                                                if (r.Next(3) == 0)
                                                {
                                                    damageTypes.Add(0);
                                                    int temp = r.Next(371) + 20;
                                                    baseDamage += temp;
                                                    prop.SetValue(cell, temp, null);
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, 0, null);
                                                }
                                            }
                                            else
                                            {
                                                baseDamage += allAttackBasePhysic[randomIndex];
                                                prop.SetValue(cell, allAttackBasePhysic[randomIndex], null);
                                            }
                                        }

                                        allAttackBasePhysic.RemoveAt(randomIndex);
                                    }
                                    else if (cell.Def.Name == "attackBaseMagic")
                                    {
                                        int randomIndex = r.Next(allAttackBaseMagic.Count);
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (chkWeaponDamage.Checked)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDamage.Checked)
                                            {
                                                //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the damage it does
                                                if (r.Next(3) == 0)
                                                {
                                                    damageTypes.Add(1);
                                                    int temp = r.Next(371) + 20;
                                                    baseDamage += temp;
                                                    prop.SetValue(cell, temp, null);
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, 0, null);
                                                }
                                            }
                                            else
                                            {
                                                baseDamage += allAttackBaseMagic[randomIndex];
                                                prop.SetValue(cell, allAttackBaseMagic[randomIndex], null);
                                            }
                                        }

                                        allAttackBaseMagic.RemoveAt(randomIndex);
                                    }
                                    else if (cell.Def.Name == "attackBaseFire")
                                    {
                                        int randomIndex = r.Next(allAttackBaseFire.Count);
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (chkWeaponDamage.Checked)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDamage.Checked)
                                            {
                                                //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the damage it does
                                                if (r.Next(3) == 0)
                                                {
                                                    damageTypes.Add(2);
                                                    int temp = r.Next(371) + 20;
                                                    baseDamage += temp;
                                                    prop.SetValue(cell, temp, null);
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, 0, null);
                                                }
                                            }
                                            else
                                            {
                                                baseDamage += allAttackBaseFire[randomIndex];
                                                prop.SetValue(cell, allAttackBaseFire[randomIndex], null);
                                            }
                                        }

                                        allAttackBaseFire.RemoveAt(randomIndex);
                                    }
                                    else if (cell.Def.Name == "attackBaseThunder")
                                    {
                                        int randomIndex = r.Next(allAttackBaseThunder.Count);
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (chkWeaponDamage.Checked)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDamage.Checked)
                                            {
                                                //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the damage it does
                                                if (r.Next(3) == 0)
                                                {
                                                    damageTypes.Add(3);
                                                    int temp = r.Next(371) + 20;
                                                    baseDamage += temp;
                                                    prop.SetValue(cell, temp, null);
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, 0, null);
                                                }
                                            }
                                            else
                                            {
                                                baseDamage += allAttackBaseThunder[randomIndex];
                                                prop.SetValue(cell, allAttackBaseThunder[randomIndex], null);
                                            }
                                        }

                                        allAttackBaseThunder.RemoveAt(randomIndex);
                                    }
                                    else if (cell.Def.Name == "correctStrength")
                                    {
                                        int randomIndex = r.Next(allAttackCorrectStrength.Count);
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (checkBoxWeaponScaling.Checked)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponScaling.Checked)
                                            {
                                                //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the scaling it does
                                                if (r.Next(3) == 0)
                                                {
                                                    //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                    if (r.Next(20) == 0)
                                                    {
                                                        //215 is the cap for a stat (tin crystallization catalyst)
                                                        prop.SetValue(cell, r.Next(116) + 100, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, r.Next(0), null);
                                                }
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, allAttackCorrectStrength[randomIndex], null);
                                            }
                                        }

                                        allAttackCorrectStrength.RemoveAt(randomIndex);
                                    }
                                    else if (cell.Def.Name == "correctAgility")
                                    {
                                        int randomIndex = r.Next(allAttackCorrectAgility.Count);
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (checkBoxWeaponScaling.Checked)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponScaling.Checked)
                                            {
                                                //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the scaling it does
                                                if (r.Next(3) == 0)
                                                {
                                                    //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                    if (r.Next(20) == 0)
                                                    {
                                                        //215 is the cap for a stat (tin crystallization catalyst)
                                                        prop.SetValue(cell, r.Next(116) + 100, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, r.Next(0), null);
                                                }
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, allAttackCorrectAgility[randomIndex], null);
                                            }
                                        }

                                        allAttackCorrectAgility.RemoveAt(randomIndex);
                                    }
                                    else if (cell.Def.Name == "correctMagic")
                                    {
                                        bool isCaster = paramRow.ID >= 1300000 && paramRow.ID <= 1367000;
                                        int randomIndex;
                                        if (isCaster)
                                        {
                                            randomIndex = r.Next(allCastingCorrectMagic.Count);
                                        }
                                        else
                                        {
                                            randomIndex = r.Next(allAttackCorrectMagic.Count);
                                        }
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (checkBoxWeaponScaling.Checked)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponScaling.Checked)
                                            {
                                                if(isCaster)
                                                {
                                                    prop.SetValue(cell, r.Next(251) + 0, null);
                                                }
                                                else
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the scaling it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //215 is the cap for a stat (tin crystallization catalyst)
                                                            prop.SetValue(cell, r.Next(116) + 100, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(101), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if(isCaster)
                                                {
                                                    prop.SetValue(cell, allCastingCorrectMagic[randomIndex], null);
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackCorrectMagic[randomIndex], null);
                                                }
                                            }
                                        }
                                        if(isCaster)
                                        {
                                            allCastingCorrectMagic.RemoveAt(randomIndex);
                                        }
                                        else
                                        {
                                            allAttackCorrectMagic.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "correctFaith")
                                    {
                                        bool isCaster = paramRow.ID >= 1300000 && paramRow.ID <= 1367000;
                                        int randomIndex;
                                        if(isCaster)
                                        {
                                            randomIndex = r.Next(allCastingCorrectFaith.Count);
                                        }
                                        else
                                        {
                                            randomIndex = r.Next(allAttackCorrectFaith.Count);
                                        }
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (checkBoxWeaponScaling.Checked)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponScaling.Checked)
                                            {
                                                if(isCaster)
                                                {
                                                    prop.SetValue(cell, r.Next(251), null);
                                                }
                                                else
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the scaling it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //215 is the cap for a stat (tin crystallization catalyst)
                                                            prop.SetValue(cell, r.Next(116) + 100, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(101), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if(isCaster)
                                                {
                                                    prop.SetValue(cell, allCastingCorrectFaith[randomIndex], null);
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackCorrectFaith[randomIndex], null);
                                                }
                                            }
                                        }

                                        if(isCaster)
                                        {
                                            allCastingCorrectFaith.RemoveAt(randomIndex);
                                        }
                                        else
                                        {
                                            allAttackCorrectFaith.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "properStrength")
                                    {
                                        //if not straightsword hilt
                                        if (paramRow.ID != 212000)
                                        {
                                            int randomIndex = r.Next(allAttackProperStrength.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStatMin.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the stat requirements
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //58 is the cap
                                                            prop.SetValue(cell, r.Next(34) + 25, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(26), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackProperStrength[randomIndex], null);
                                                }
                                            }

                                            allAttackProperStrength.RemoveAt(randomIndex);
                                        }
                                        else
                                        {
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked || checkBoxStartingClasses.Checked)
                                            {
                                                prop.SetValue(cell, 0, null);
                                            }
                                        }
                                    }
                                    else if (cell.Def.Name == "properAgility")
                                    {
                                        //if not straightsword hilt
                                        if (paramRow.ID != 212000)
                                        {
                                            int randomIndex = r.Next(allAttackProperAgility.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStatMin.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the stat requirements
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //58 is the cap
                                                            prop.SetValue(cell, r.Next(34) + 25, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(26), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackProperAgility[randomIndex], null);
                                                }
                                            }

                                            allAttackProperAgility.RemoveAt(randomIndex);
                                        }
                                        else
                                        {
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked || checkBoxStartingClasses.Checked)
                                            {
                                                prop.SetValue(cell, 0, null);
                                            }
                                        }
                                    }
                                    else if (cell.Def.Name == "properMagic")
                                    {
                                        //if not straightsword hilt
                                        if (paramRow.ID != 212000)
                                        {
                                            int randomIndex = r.Next(allAttackProperMagic.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStatMin.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the stat requirements
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //58 is the cap
                                                            prop.SetValue(cell, r.Next(34) + 25, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(26), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackProperMagic[randomIndex], null);
                                                }
                                            }

                                            allAttackProperMagic.RemoveAt(randomIndex);
                                        }
                                        else
                                        {
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked || checkBoxStartingClasses.Checked)
                                            {
                                                prop.SetValue(cell, 0, null);
                                            }
                                        }
                                    }
                                    else if (cell.Def.Name == "properFaith")
                                    {
                                        //if not straightsword hilt
                                        if (paramRow.ID != 212000)
                                        {
                                            int randomIndex = r.Next(allAttackProperFaith.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStatMin.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the stat requirements
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //58 is the cap
                                                            prop.SetValue(cell, r.Next(34) + 25, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(26), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackProperFaith[randomIndex], null);
                                                }
                                            }

                                            allAttackProperFaith.RemoveAt(randomIndex);
                                        }
                                        else
                                        {
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked || checkBoxStartingClasses.Checked)
                                            {
                                                prop.SetValue(cell, 0, null);
                                            }
                                        }
                                    }
                                    else if (cell.Def.Name == "weight")
                                    {
                                        MeowDSIO.DataTypes.PARAM.ParamCellValueRef fistCheckCell = paramRow.Cells.First(c => c.Def.Name == "sortId");
                                        Type fistchecktype = fistCheckCell.GetType();
                                        PropertyInfo fistcheckprop = fistchecktype.GetProperty("Value");

                                        int randomIndex = r.Next(allWepWeight.Count);
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        //fists dont get weight
                                        if (checkBoxWeaponWeight.Checked && Convert.ToInt32(fistcheckprop.GetValue(fistCheckCell, null)) != 1750)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponWeight.Checked)
                                            {
                                                //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                if (r.Next(20) == 0)
                                                {
                                                    //28 is the cap; weight is randomized in half steps
                                                    prop.SetValue(cell, (r.Next(37) + 20) / 2.0, null);
                                                }
                                                else
                                                {
                                                    //10 is soft cap; weight is randomized in half steps
                                                    prop.SetValue(cell, (r.Next(21)) / 2.0, null);
                                                }
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, allWepWeight[randomIndex], null);
                                            }
                                        }

                                        allWepWeight.RemoveAt(randomIndex);
                                    }
                                    else if (cell.Def.Name == "attackBaseStamina")
                                    {
                                        int randomIndex = r.Next(allAttackBaseStamina.Count);
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (checkBoxWeaponStamina.Checked)
                                        {
                                            if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStamina.Checked)
                                            {
                                                if (r.Next(3) == 0)
                                                {
                                                    //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                    if (r.Next(33) == 0)
                                                    {
                                                        //100 is the cap
                                                        prop.SetValue(cell, r.Next(61) + 40, null);
                                                    }
                                                    else
                                                    {
                                                        //40 is 2nd soft cap
                                                        prop.SetValue(cell, r.Next(16) + 25, null);
                                                    }
                                                }
                                                else
                                                {
                                                    //first soft cap of 25
                                                    prop.SetValue(cell, r.Next(25) + 1, null);
                                                }
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, allAttackBaseStamina[randomIndex], null);
                                            }
                                        }

                                        allAttackBaseStamina.RemoveAt(randomIndex);
                                    }
                                    else if (cell.Def.Name == "enableMagic:1" || cell.Def.Name == "enableVowMagic:1" || cell.Def.Name == "enableSorcery:1")
                                    {
                                        Type type = cell.GetType();
                                        PropertyInfo prop = type.GetProperty("Value");
                                        if (castsMagic && checkBoxUniversalizeCasters.Checked)
                                        {
                                            prop.SetValue(cell, true, null);
                                        }
                                    }
                                    else if (cell.Def.Name == "spEffectBehaviorId0")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allspEffectBehavior.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponSpeffects.Checked)
                                            {
                                                prop.SetValue(cell, allspEffectBehavior[randomIndex], null);
                                            }

                                            allspEffectBehavior.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "spEffectBehaviorId1")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allspEffectBehavior2.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponSpeffects.Checked)
                                            {
                                                prop.SetValue(cell, allspEffectBehavior2[randomIndex], null);
                                            }

                                            allspEffectBehavior2.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "residentSpEffectId")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allresidentspEffect.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponSpeffects.Checked)
                                            {
                                                prop.SetValue(cell, allresidentspEffect[randomIndex], null);
                                            }

                                            allresidentspEffect.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "physGuardCutRate")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allPhysicalGuard.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if(checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, r.Next(51) + 20, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(81) + 20, null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allPhysicalGuard[randomIndex], null);
                                                }
                                            }

                                            allPhysicalGuard.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "magGuardCutRate")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allMagicGuard.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if (checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, r.Next(46) + 5, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(96) + 5, null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allMagicGuard[randomIndex], null);
                                                }
                                            }

                                            allMagicGuard.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "fireGuardCutRate")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allFireGuard.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if (checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, r.Next(36) + 15, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(86) + 15, null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allFireGuard[randomIndex], null);
                                                }
                                            }

                                            allFireGuard.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "thunGuardCutRate")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allThunderGuard.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if (checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, r.Next(36) + 15, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(86) + 15, null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allThunderGuard[randomIndex], null);
                                                }
                                            }

                                            allThunderGuard.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "staminaGuardDef")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allGuardStability.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if (checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, r.Next(41) + 10, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(81) + 10, null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allGuardStability[randomIndex], null);
                                                }
                                            }

                                            allGuardStability.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "poisonGuardResist")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allPoisonResist.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if (checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, r.Next(61), null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allPoisonResist[randomIndex], null);
                                                }
                                            }

                                            allPoisonResist.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "diseaseGuardResist")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allDiseaseResist.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if (checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, r.Next(61), null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allDiseaseResist[randomIndex], null);
                                                }
                                            }

                                            allDiseaseResist.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "bloodGuardResist")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allBloodResist.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if (checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, r.Next(61), null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allBloodResist[randomIndex], null);
                                                }
                                            }

                                            allBloodResist.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "curseGuardResist")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allCurseResist.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if (checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, r.Next(61), null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allCurseResist[randomIndex], null);
                                                }
                                            }

                                            allCurseResist.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "guardBaseRepel")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allGuardBaseRepel.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    if (checkBoxWeaponShieldSplit.Checked) //this is offensive weapons only
                                                    {
                                                        prop.SetValue(cell, 10, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(71) + 10, null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allGuardBaseRepel[randomIndex], null);
                                                }
                                            }

                                            allGuardBaseRepel.RemoveAt(randomIndex);
                                        }
                                    }
                                    else if (cell.Def.Name == "attackBaseRepel")
                                    {
                                        //if not fists
                                        if (paramRow.ID != 900000)
                                        {
                                            int randomIndex = r.Next(allAttackBaseRepel.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponDefense.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                {
                                                    prop.SetValue(cell, r.Next(56) + 15, null);
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackBaseRepel[randomIndex], null);
                                                }
                                            }

                                            allAttackBaseRepel.RemoveAt(randomIndex);
                                        }
                                    }
                                }

                                //damage dropoff when rolling more than one damage type (Dont randomize by shuffle mode only, shuffle mode will have an empty damagetypes list)
                                while (damageTypes.Count > 0)
                                {
                                    int index = r.Next(damageTypes.Count);
                                    int type = damageTypes[index];

                                    //new damage of this damage type = (rolled damage / total damage types);
                                    //new damage will be the same as rolled damage if only one damage type was rolled
                                    //or if this is the last of the damage types that dropoff is applied to
                                    if (type == 0) //if physical
                                    {
                                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                        {
                                            if (cell.Def.Name == "attackBasePhysics")
                                            {
                                                int newDamage = Convert.ToInt32(cell.GetType().GetProperty("Value").GetValue(cell, null)) / damageTypes.Count;
                                                cell.GetType().GetProperty("Value").SetValue(cell, newDamage, null);
                                            }
                                        }
                                    }
                                    else if (type == 1) //if magic
                                    {
                                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                        {
                                            if (cell.Def.Name == "attackBaseMagic")
                                            {
                                                int newDamage = Convert.ToInt32(cell.GetType().GetProperty("Value").GetValue(cell, null)) / damageTypes.Count;
                                                cell.GetType().GetProperty("Value").SetValue(cell, newDamage, null);
                                            }
                                        }
                                    }
                                    else if (type == 2) // if fire
                                    {
                                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                        {
                                            if (cell.Def.Name == "attackBaseFire")
                                            {
                                                int newDamage = Convert.ToInt32(cell.GetType().GetProperty("Value").GetValue(cell, null)) / damageTypes.Count;
                                                cell.GetType().GetProperty("Value").SetValue(cell, newDamage, null);
                                            }
                                        }
                                    }
                                    else if (type == 3) //if thunder
                                    {
                                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                        {
                                            if (cell.Def.Name == "attackBaseThunder")
                                            {
                                                int newDamage = Convert.ToInt32(cell.GetType().GetProperty("Value").GetValue(cell, null)) / damageTypes.Count;
                                                cell.GetType().GetProperty("Value").SetValue(cell, newDamage, null);
                                            }
                                        }
                                    }

                                    damageTypes.RemoveAt(index);
                                }

                                if (baseDamage == 0)
                                {
                                    int dtype = r.Next(4);
                                    foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                    {
                                        if (cell.Def.Name == "attackBasePhysics" && dtype == 0)
                                        {
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            prop.SetValue(cell, r.Next(371) + 20, null);
                                        }
                                        else if (cell.Def.Name == "attackBaseMagic" && dtype == 1)
                                        {
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            prop.SetValue(cell, r.Next(371) + 20, null);
                                        }
                                        else if (cell.Def.Name == "attackBaseFire" && dtype == 2)
                                        {
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            prop.SetValue(cell, r.Next(371) + 20, null);
                                        }
                                        else if (cell.Def.Name == "attackBaseThunder" && dtype == 3)
                                        {
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            prop.SetValue(cell, r.Next(371) + 20, null);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //now if shields are treated seperately do everything again with only shields this time (additions to weapons above should be added to shield logic below)
                    if(checkBoxWeaponShieldSplit.Checked)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                        {
                            //if is a shield
                            if (allShieldIDs.Contains(paramRow.ID))
                            {
                                //check not to randomize moveset of bows which defeats the purpose of bullet rando. Try disabling this check and see what happens maybe
                                MeowDSIO.DataTypes.PARAM.ParamCellValueRef bowCheckCell = paramRow.Cells.First(c => c.Def.Name == "bowDistRate");
                                Type bowchecktype = bowCheckCell.GetType();
                                PropertyInfo bowcheckprop = bowchecktype.GetProperty("Value");

                                //if dont modify fists is off or if not fist
                                if (!checkBoxWeaponFistNo.Checked || paramRow.ID != 900000)
                                {
                                    if (Convert.ToInt32(bowcheckprop.GetValue(bowCheckCell, null)) < 0)
                                    {
                                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                        {
                                            if (cell.Def.Name == "wepmotionCategory")
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allWepmotionCats.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                            else if (cell.Def.Name == "wepmotionOneHandId")
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allWepmotion1hCats.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                            else if (cell.Def.Name == "wepmotionBothHandId")
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allWepmotion2hCats.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                            else if (cell.Def.Name == "spAtkcategory")
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allspAtkcategories.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                    }

                                    foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                    {
                                        if (cell.Def.Name == "equipModelId")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allEquipModelIds.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "attackBasePhysics")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackBasePhysic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "attackBaseMagic")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackBaseMagic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "attackBaseFire")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackBaseFire.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "attackBaseThunder")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackBaseThunder.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "correctStrength")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackCorrectStrength.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "correctAgility")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackCorrectAgility.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "correctMagic")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackCorrectMagic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "correctFaith")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackCorrectFaith.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "properStrength")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackProperStrength.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "properAgility")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackProperAgility.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "properMagic")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackProperMagic.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "properFaith")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackProperFaith.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "weight")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allWepWeight.Add(Convert.ToDouble(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "attackBaseStamina")
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            allAttackBaseStamina.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                        }
                                        else if (cell.Def.Name == "spEffectBehaviorId0")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allspEffectBehavior.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "spEffectBehaviorId1")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allspEffectBehavior2.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "residentSpEffectId")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allresidentspEffect.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "physGuardCutRate")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allPhysicalGuard.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "magGuardCutRate")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allMagicGuard.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "fireGuardCutRate")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allFireGuard.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "thunGuardCutRate")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allThunderGuard.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "staminaGuardDef")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allGuardStability.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "poisonGuardResist")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allPoisonResist.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "diseaseGuardResist")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allDiseaseResist.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "bloodGuardResist")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allBloodResist.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "curseGuardResist")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allCurseResist.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "guardBaseRepel")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allGuardBaseRepel.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                        else if (cell.Def.Name == "attackBaseRepel")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                                allAttackBaseRepel.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //loop again to set a random value per entry
                        foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                        {
                            //if is a shield
                            if (allShieldIDs.Contains(paramRow.ID))
                            {
                                MeowDSIO.DataTypes.PARAM.ParamCellValueRef bowCheckCell = paramRow.Cells.First(c => c.Def.Name == "bowDistRate");
                                Type bowchecktype = bowCheckCell.GetType();
                                PropertyInfo bowcheckprop = bowchecktype.GetProperty("Value");

                                //if dont modify fists is off or if not fist
                                if (!checkBoxWeaponFistNo.Checked || paramRow.ID != 900000)
                                {
                                    if (Convert.ToInt32(bowcheckprop.GetValue(bowCheckCell, null)) < 0)
                                    {
                                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                        {
                                            if (cell.Def.Name == "wepmotionCategory")
                                            {
                                                int randomIndex = r.Next(allWepmotionCats.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (chkWeaponMoveset.Checked)
                                                {
                                                    prop.SetValue(cell, allWepmotionCats[randomIndex], null);
                                                }

                                                allWepmotionCats.RemoveAt(randomIndex);
                                            }
                                            else if (cell.Def.Name == "wepmotionOneHandId")
                                            {
                                                int randomIndex = r.Next(allWepmotion1hCats.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (chkWeaponMoveset.Checked)
                                                {
                                                    prop.SetValue(cell, allWepmotion1hCats[randomIndex], null);
                                                }

                                                allWepmotion1hCats.RemoveAt(randomIndex);
                                            }
                                            else if (cell.Def.Name == "wepmotionBothHandId")
                                            {
                                                int randomIndex = r.Next(allWepmotion2hCats.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (chkWeaponMoveset.Checked)
                                                {
                                                    prop.SetValue(cell, allWepmotion2hCats[randomIndex], null);
                                                }

                                                allWepmotion2hCats.RemoveAt(randomIndex);
                                            }
                                            else if (cell.Def.Name == "spAtkcategory")
                                            {
                                                int randomIndex = r.Next(allspAtkcategories.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");

                                                if (chkWeaponMoveset.Checked)
                                                {
                                                    prop.SetValue(cell, allspAtkcategories[randomIndex], null);
                                                }

                                                allspAtkcategories.RemoveAt(randomIndex);
                                            }
                                        }
                                    }

                                    int baseDamage = 0;
                                    bool castsMagic = false;
                                    List<int> damageTypes = new List<int>(); //values 0-3; phys, magic, fire, thunder
                                                                             //check if casts magic first
                                    foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                    {
                                        if (cell.Def.Name == "enableMagic" || cell.Def.Name == "enableVowMagic" || cell.Def.Name == "enableSorcery")
                                        {
                                            if (Convert.ToBoolean(cell.GetType().GetProperty("Value").GetValue(cell, null)) == true)
                                            {
                                                castsMagic = true;
                                            }
                                        }
                                    }
                                    foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                    {
                                        if (cell.Def.Name == "equipModelId")
                                        {
                                            int randomIndex = r.Next(allEquipModelIds.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponModels.Checked)
                                            {
                                                prop.SetValue(cell, allEquipModelIds[randomIndex], null);
                                            }

                                            allEquipModelIds.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "attackBasePhysics")
                                        {
                                            int randomIndex = r.Next(allAttackBasePhysic.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponDamage.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDamage.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the damage it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        damageTypes.Add(0);
                                                        int temp = r.Next(61) + 30;
                                                        baseDamage += temp;
                                                        prop.SetValue(cell, temp, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, 0, null);
                                                    }
                                                }
                                                else
                                                {
                                                    baseDamage += allAttackBasePhysic[randomIndex];
                                                    prop.SetValue(cell, allAttackBasePhysic[randomIndex], null);
                                                }
                                            }

                                            allAttackBasePhysic.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "attackBaseMagic")
                                        {
                                            int randomIndex = r.Next(allAttackBaseMagic.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponDamage.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDamage.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the damage it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        damageTypes.Add(1);
                                                        int temp = r.Next(61) + 30;
                                                        baseDamage += temp;
                                                        prop.SetValue(cell, temp, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, 0, null);
                                                    }
                                                }
                                                else
                                                {
                                                    baseDamage += allAttackBaseMagic[randomIndex];
                                                    prop.SetValue(cell, allAttackBaseMagic[randomIndex], null);
                                                }
                                            }

                                            allAttackBaseMagic.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "attackBaseFire")
                                        {
                                            int randomIndex = r.Next(allAttackBaseFire.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponDamage.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDamage.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the damage it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        damageTypes.Add(2);
                                                        int temp = r.Next(61) + 30;
                                                        baseDamage += temp;
                                                        prop.SetValue(cell, temp, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, 0, null);
                                                    }
                                                }
                                                else
                                                {
                                                    baseDamage += allAttackBaseFire[randomIndex];
                                                    prop.SetValue(cell, allAttackBaseFire[randomIndex], null);
                                                }
                                            }

                                            allAttackBaseFire.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "attackBaseThunder")
                                        {
                                            int randomIndex = r.Next(allAttackBaseThunder.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (chkWeaponDamage.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDamage.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the damage it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        damageTypes.Add(3);
                                                        int temp = r.Next(61) + 30;
                                                        baseDamage += temp;
                                                        prop.SetValue(cell, temp, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, 0, null);
                                                    }
                                                }
                                                else
                                                {
                                                    baseDamage += allAttackBaseThunder[randomIndex];
                                                    prop.SetValue(cell, allAttackBaseThunder[randomIndex], null);
                                                }
                                            }

                                            allAttackBaseThunder.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "correctStrength")
                                        {
                                            int randomIndex = r.Next(allAttackCorrectStrength.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponScaling.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponScaling.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the scaling it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //215 is the cap for a stat (tin crystallization catalyst)
                                                            prop.SetValue(cell, r.Next(116) + 100, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(101), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackCorrectStrength[randomIndex], null);
                                                }
                                            }

                                            allAttackCorrectStrength.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "correctAgility")
                                        {
                                            int randomIndex = r.Next(allAttackCorrectAgility.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponScaling.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponScaling.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the scaling it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //215 is the cap for a stat (tin crystallization catalyst)
                                                            prop.SetValue(cell, r.Next(116) + 100, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(101), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackCorrectAgility[randomIndex], null);
                                                }
                                            }

                                            allAttackCorrectAgility.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "correctMagic")
                                        {
                                            int randomIndex = r.Next(allAttackCorrectMagic.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponScaling.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponScaling.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the scaling it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //215 is the cap for a stat (tin crystallization catalyst)
                                                            prop.SetValue(cell, r.Next(116) + 100, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(101), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackCorrectMagic[randomIndex], null);
                                                }
                                            }

                                            allAttackCorrectMagic.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "correctFaith")
                                        {
                                            int randomIndex = r.Next(allAttackCorrectFaith.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponScaling.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponScaling.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the scaling it does
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //215 is the cap for a stat (tin crystallization catalyst)
                                                            prop.SetValue(cell, r.Next(116) + 100, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(101), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackCorrectFaith[randomIndex], null);
                                                }
                                            }

                                            allAttackCorrectFaith.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "properStrength")
                                        {
                                            int randomIndex = r.Next(allAttackProperStrength.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStatMin.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the stat requirements
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //58 is the cap
                                                            prop.SetValue(cell, r.Next(34) + 25, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(26), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackProperStrength[randomIndex], null);
                                                }
                                            }

                                            allAttackProperStrength.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "properAgility")
                                        {
                                            int randomIndex = r.Next(allAttackProperAgility.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStatMin.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the stat requirements
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //58 is the cap
                                                            prop.SetValue(cell, r.Next(34) + 25, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(26), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackProperAgility[randomIndex], null);
                                                }
                                            }

                                            allAttackProperAgility.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "properMagic")
                                        {
                                            int randomIndex = r.Next(allAttackProperMagic.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStatMin.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the stat requirements
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //58 is the cap
                                                            prop.SetValue(cell, r.Next(34) + 25, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(26), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackProperMagic[randomIndex], null);
                                                }
                                            }

                                            allAttackProperMagic.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "properFaith")
                                        {
                                            int randomIndex = r.Next(allAttackProperFaith.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStatMin.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStatMin.Checked)
                                                {
                                                    //if DoTrueRandom, 1/3 chance of an attack type being selected and then randomly role the stat requirements
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(20) == 0)
                                                        {
                                                            //58 is the cap
                                                            prop.SetValue(cell, r.Next(34) + 25, null);
                                                        }
                                                        else
                                                        {
                                                            prop.SetValue(cell, r.Next(26), null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, r.Next(0), null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackProperFaith[randomIndex], null);
                                                }
                                            }

                                            allAttackProperFaith.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "weight")
                                        {
                                            MeowDSIO.DataTypes.PARAM.ParamCellValueRef fistCheckCell = paramRow.Cells.First(c => c.Def.Name == "sortId");
                                            Type fistchecktype = fistCheckCell.GetType();
                                            PropertyInfo fistcheckprop = fistchecktype.GetProperty("Value");

                                            int randomIndex = r.Next(allWepWeight.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            //fists dont get weight
                                            if (checkBoxWeaponWeight.Checked && Convert.ToInt32(fistcheckprop.GetValue(fistCheckCell, null)) != 1750)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponWeight.Checked)
                                                {
                                                    //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                    if (r.Next(20) == 0)
                                                    {
                                                        //28 is the cap; weight is randomized in half steps
                                                        prop.SetValue(cell, (r.Next(37) + 20) / 2.0, null);
                                                    }
                                                    else
                                                    {
                                                        //10 is soft cap; weight is randomized in half steps
                                                        prop.SetValue(cell, (r.Next(21)) / 2.0, null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allWepWeight[randomIndex], null);
                                                }
                                            }

                                            allWepWeight.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "attackBaseStamina")
                                        {
                                            int randomIndex = r.Next(allAttackBaseStamina.Count);
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (checkBoxWeaponStamina.Checked)
                                            {
                                                if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponStamina.Checked)
                                                {
                                                    if (r.Next(3) == 0)
                                                    {
                                                        //small chance that the value will be above a certain value (used to prevent higher values appearing more frequently because outliers are included)
                                                        if (r.Next(33) == 0)
                                                        {
                                                            //100 is the cap
                                                            prop.SetValue(cell, r.Next(61) + 40, null);
                                                        }
                                                        else
                                                        {
                                                            //40 is 2nd soft cap
                                                            prop.SetValue(cell, r.Next(16) + 25, null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //first soft cap of 25
                                                        prop.SetValue(cell, r.Next(25) + 1, null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, allAttackBaseStamina[randomIndex], null);
                                                }
                                            }

                                            allAttackBaseStamina.RemoveAt(randomIndex);
                                        }
                                        else if (cell.Def.Name == "enableMagic" || cell.Def.Name == "enableVowMagic" || cell.Def.Name == "enableSorcery")
                                        {
                                            Type type = cell.GetType();
                                            PropertyInfo prop = type.GetProperty("Value");
                                            if (castsMagic && checkBoxUniversalizeCasters.Checked)
                                            {
                                                prop.SetValue(cell, true, null);
                                            }
                                        }
                                        else if (cell.Def.Name == "spEffectBehaviorId0")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allspEffectBehavior.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (chkWeaponSpeffects.Checked)
                                                {
                                                    prop.SetValue(cell, allspEffectBehavior[randomIndex], null);
                                                }

                                                allspEffectBehavior.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "spEffectBehaviorId1")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allspEffectBehavior2.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (chkWeaponSpeffects.Checked)
                                                {
                                                    prop.SetValue(cell, allspEffectBehavior2[randomIndex], null);
                                                }

                                                allspEffectBehavior2.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "residentSpEffectId")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allresidentspEffect.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (chkWeaponSpeffects.Checked)
                                                {
                                                    prop.SetValue(cell, allresidentspEffect[randomIndex], null);
                                                }

                                                allresidentspEffect.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "physGuardCutRate")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allPhysicalGuard.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if(checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        // 1/3 chance to have 100 defense
                                                        if(r.Next(3) == 0)
                                                        {
                                                            prop.SetValue(cell, 100, null);
                                                        }
                                                        else // 65 to 100
                                                        {
                                                            prop.SetValue(cell, r.Next(36) + 65, null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allPhysicalGuard[randomIndex], null);
                                                    }
                                                }

                                                allPhysicalGuard.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "magGuardCutRate")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allMagicGuard.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        // 1/10 chance to have 80-100 defense
                                                        if (r.Next(10) == 0)
                                                        {
                                                            prop.SetValue(cell, r.Next(21) + 80, null);
                                                        }
                                                        else // 25 to 70
                                                        {
                                                            prop.SetValue(cell, r.Next(46) + 25, null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allMagicGuard[randomIndex], null);
                                                    }
                                                }

                                                allMagicGuard.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "fireGuardCutRate")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allFireGuard.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        // 1/2 chance to have 51-100 defense
                                                        if (r.Next(2) == 0)
                                                        {
                                                            prop.SetValue(cell, r.Next(50) + 51, null);
                                                        }
                                                        else // 10 to 50
                                                        {
                                                            prop.SetValue(cell, r.Next(41) + 10, null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allFireGuard[randomIndex], null);
                                                    }
                                                }

                                                allFireGuard.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "thunGuardCutRate")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allThunderGuard.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        // 1/4 chance to have 61-100 defense
                                                        if (r.Next(4) == 0)
                                                        {
                                                            prop.SetValue(cell, r.Next(40) + 61, null);
                                                        }
                                                        else // 30 to 60
                                                        {
                                                            prop.SetValue(cell, r.Next(31) + 30, null);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allThunderGuard[randomIndex], null);
                                                    }
                                                }

                                                allThunderGuard.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "staminaGuardDef")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allGuardStability.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        //30 to 90
                                                        prop.SetValue(cell, r.Next(61) + 30, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allGuardStability[randomIndex], null);
                                                    }
                                                }

                                                allGuardStability.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "poisonGuardResist")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allPoisonResist.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allPoisonResist[randomIndex], null);
                                                    }
                                                }

                                                allPoisonResist.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "diseaseGuardResist")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allDiseaseResist.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allDiseaseResist[randomIndex], null);
                                                    }
                                                }

                                                allDiseaseResist.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "bloodGuardResist")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allBloodResist.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allBloodResist[randomIndex], null);
                                                    }
                                                }

                                                allBloodResist.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "curseGuardResist")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allCurseResist.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        prop.SetValue(cell, r.Next(101), null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allCurseResist[randomIndex], null);
                                                    }
                                                }

                                                allCurseResist.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "guardBaseRepel")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allGuardBaseRepel.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        prop.SetValue(cell, r.Next(71) + 10, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allGuardBaseRepel[randomIndex], null);
                                                    }
                                                }

                                                allGuardBaseRepel.RemoveAt(randomIndex);
                                            }
                                        }
                                        else if (cell.Def.Name == "attackBaseRepel")
                                        {
                                            //if not fists
                                            if (paramRow.ID != 900000)
                                            {
                                                int randomIndex = r.Next(allAttackBaseRepel.Count);
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                if (checkBoxWeaponDefense.Checked)
                                                {
                                                    if (checkBoxDoTrueRandom.Checked && TRForm.chkTRWeaponDefense.Checked)
                                                    {
                                                        prop.SetValue(cell, r.Next(56) + 15, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, allAttackBaseRepel[randomIndex], null);
                                                    }
                                                }

                                                allAttackBaseRepel.RemoveAt(randomIndex);
                                            }
                                        }
                                    }

                                    //damage dropoff when rolling more than one damage type (Dont randomize by shuffle mode only, shuffle mode will have an empty damagetypes list)
                                    while (damageTypes.Count > 0)
                                    {
                                        int index = r.Next(damageTypes.Count);
                                        int type = damageTypes[index];

                                        //new damage of this damage type = (rolled damage / total damage types);
                                        //new damage will be the same as rolled damage if only one damage type was rolled
                                        //or if this is the last of the damage types that dropoff is applied to
                                        if (type == 0) //if physical
                                        {
                                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                            {
                                                if (cell.Def.Name == "attackBasePhysics")
                                                {
                                                    int newDamage = Convert.ToInt32(cell.GetType().GetProperty("Value").GetValue(cell, null)) / damageTypes.Count;
                                                    cell.GetType().GetProperty("Value").SetValue(cell, newDamage, null);
                                                }
                                            }
                                        }
                                        else if (type == 1) //if magic
                                        {
                                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                            {
                                                if (cell.Def.Name == "attackBaseMagic")
                                                {
                                                    int newDamage = Convert.ToInt32(cell.GetType().GetProperty("Value").GetValue(cell, null)) / damageTypes.Count;
                                                    cell.GetType().GetProperty("Value").SetValue(cell, newDamage, null);
                                                }
                                            }
                                        }
                                        else if (type == 2) // if fire
                                        {
                                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                            {
                                                if (cell.Def.Name == "attackBaseFire")
                                                {
                                                    int newDamage = Convert.ToInt32(cell.GetType().GetProperty("Value").GetValue(cell, null)) / damageTypes.Count;
                                                    cell.GetType().GetProperty("Value").SetValue(cell, newDamage, null);
                                                }
                                            }
                                        }
                                        else if (type == 3) //if thunder
                                        {
                                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                            {
                                                if (cell.Def.Name == "attackBaseThunder")
                                                {
                                                    int newDamage = Convert.ToInt32(cell.GetType().GetProperty("Value").GetValue(cell, null)) / damageTypes.Count;
                                                    cell.GetType().GetProperty("Value").SetValue(cell, newDamage, null);
                                                }
                                            }
                                        }

                                        damageTypes.RemoveAt(index);
                                    }

                                    if (baseDamage == 0)
                                    {
                                        int dtype = r.Next(4);
                                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                        {
                                            if (cell.Def.Name == "attackBasePhysics" && dtype == 0)
                                            {
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                prop.SetValue(cell, r.Next(61) + 30, null);
                                            }
                                            else if (cell.Def.Name == "attackBaseMagic" && dtype == 1)
                                            {
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                prop.SetValue(cell, r.Next(61) + 30, null);
                                            }
                                            else if (cell.Def.Name == "attackBaseFire" && dtype == 2)
                                            {
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                prop.SetValue(cell, r.Next(61) + 30, null);
                                            }
                                            else if (cell.Def.Name == "attackBaseThunder" && dtype == 3)
                                            {
                                                Type type = cell.GetType();
                                                PropertyInfo prop = type.GetProperty("Value");
                                                prop.SetValue(cell, r.Next(61) + 30, null);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (paramFile.ID == "MAGIC_PARAM_ST")
                {
                    //loop through all entries once to get list of all values
                    List<int> allSfxVariationIds = new List<int>();
                    List<int> allSlotLengths = new List<int>();
                    List<int> allRequirementIntellect = new List<int>();
                    List<int> allRequirementFaith = new List<int>();
                    List<int> allMaxQuantity = new List<int>();

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        bool dontUseSpell = false;
                        //check if unattainable spell
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "maxQuantity")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                int quantity = Convert.ToInt32(prop.GetValue(cell, null));
                                if (quantity <= 0)
                                {
                                    dontUseSpell = true;
                                }
                            }
                        }
                        if (!dontUseSpell)
                        {
                            //now do info grab
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "refType")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allSfxVariationIds.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "slotLength")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allSlotLengths.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "requirementIntellect")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allRequirementIntellect.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "requirementFaith")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allRequirementFaith.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "maxQuantity")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    allMaxQuantity.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                            }
                        }
                    }

                    //loop again to set a random value per entry
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        bool dontUseSpell = false;
                        //check if unattainable spell
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "maxQuantity")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                int quantity = Convert.ToInt32(prop.GetValue(cell, null));
                                if (quantity <= 0)
                                {
                                    dontUseSpell = true;
                                }
                            }
                        }
                        if(!dontUseSpell)
                        {
                            //now do info insert
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "refType")
                                {
                                    int randomIndex = r.Next(allSfxVariationIds.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkMagicAnimations.Checked)
                                    {
                                        prop.SetValue(cell, allSfxVariationIds[randomIndex], null);
                                    }

                                    allSfxVariationIds.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "slotLength")
                                {
                                    int randomIndex = r.Next(allSlotLengths.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxRandomizeSpellSlotSize.Checked)
                                    {
                                        if(checkBoxDoTrueRandom.Checked && TRForm.chkTRSpellSlotSize.Checked)
                                        {
                                            // 1/10 chance to get a non one slot size
                                            if(r.Next(10) == 0)
                                            {
                                                // 2/4 chance for 2; 1/4 for 3; 1/4 for 0
                                                if(r.Next(2) == 0)
                                                {
                                                    if(r.Next(2) == 0)
                                                    {
                                                        prop.SetValue(cell, 3, null);
                                                    }
                                                    else
                                                    {
                                                        prop.SetValue(cell, 0, null);
                                                    }
                                                }
                                                else
                                                {
                                                    prop.SetValue(cell, 2, null);
                                                }
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, 1, null);
                                            }
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allSlotLengths[randomIndex], null);
                                        }
                                    }

                                    allSlotLengths.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "requirementIntellect")
                                {
                                    int randomIndex = r.Next(allRequirementIntellect.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxRandomizeSpellRequirements.Checked)
                                    {
                                        if(checkBoxDoTrueRandom.Checked && TRForm.chkTRSpellRequirements.Checked)
                                        {
                                            // 1/3 chance to have no req
                                            if(r.Next(3) == 0)
                                            {
                                                prop.SetValue(cell, r.Next(41) + 10, null);
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, 0, null);
                                            }
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allRequirementIntellect[randomIndex], null);
                                        }
                                    }

                                    allRequirementIntellect.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "requirementFaith")
                                {
                                    int randomIndex = r.Next(allRequirementFaith.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxRandomizeSpellRequirements.Checked)
                                    {
                                        if (checkBoxDoTrueRandom.Checked && TRForm.chkTRSpellRequirements.Checked)
                                        {
                                            // 1/3 chance to have no req
                                            if (r.Next(3) == 0)
                                            {
                                                prop.SetValue(cell, r.Next(41) + 10, null);
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, 0, null);
                                            }
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allRequirementFaith[randomIndex], null);
                                        }
                                    }

                                    allRequirementFaith.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "maxQuantity")
                                {
                                    int randomIndex = r.Next(allMaxQuantity.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxRandomizeSpellQuantity.Checked)
                                    {
                                        if(checkBoxDoTrueRandom.Checked && TRForm.chkTRSpellQuantity.Checked)
                                        {
                                            // 1/5 chance to roll higher
                                            if(r.Next(5) == 0)
                                            {
                                                prop.SetValue(cell, r.Next(101) + 11, null);
                                            }
                                            else
                                            {
                                                prop.SetValue(cell, r.Next(10) + 1, null);
                                            }
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, allMaxQuantity[randomIndex], null);
                                        }
                                    }

                                    allMaxQuantity.RemoveAt(randomIndex);
                                }
                            }

                        }
                    }
                }
                else if (paramFile.ID == "TALK_PARAM_ST")
                {
                    //loop through all entries once to get list of all values
                    List<int> allSounds = new List<int>();
                    List<int> allMsgs = new List<int>();
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        MeowDSIO.DataTypes.PARAM.ParamCellValueRef validVoiceCheck = paramRow.Cells.First(c => c.Def.Name == "voiceId");
                        PropertyInfo voiceCheckProp = validVoiceCheck.GetType().GetProperty("Value");

                        if (!InvalidVoiceIds.Contains(voiceCheckProp.GetValue(validVoiceCheck, null)))
                        {
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "voiceId")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if (!InvalidVoiceIds.Contains(prop.GetValue(cell, null)))
                                    {
                                        allSounds.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                }
                                else if (cell.Def.Name == "msgId")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if (!InvalidVoiceIds.Contains(prop.GetValue(cell, null)))
                                    {
                                        allMsgs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                }
                            }
                        }
                    }

                    //loop again to set a random value per entry
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        MeowDSIO.DataTypes.PARAM.ParamCellValueRef validVoiceCheck = paramRow.Cells.First(c => c.Def.Name == "voiceId");
                        PropertyInfo voiceCheckProp = validVoiceCheck.GetType().GetProperty("Value");

                        if (!InvalidVoiceIds.Contains(voiceCheckProp.GetValue(validVoiceCheck, null)))
                        {
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "voiceId")
                                {
                                    int randomIndex = r.Next(allSounds.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkVoices.Checked && !InvalidVoiceIds.Contains(prop.GetValue(cell, null)))
                                    {
                                        prop.SetValue(cell, allSounds[randomIndex], null);
                                    }

                                    allSounds.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "msgId")
                                {
                                    int randomIndex = r.Next(allMsgs.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (chkVoices.Checked && !InvalidVoiceIds.Contains(prop.GetValue(cell, null)))
                                    {
                                        prop.SetValue(cell, allMsgs[randomIndex], null);
                                    }

                                    allMsgs.RemoveAt(randomIndex);
                                }
                            }
                        }                        
                    }
                }
                else if (paramFile.ID == "RAGDOLL_PARAM_ST")
                {
                    //dunno if this one actually does anything
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            //TODO...maybe
                        }
                    }
                }
                else if (paramFile.ID == "SKELETON_PARAM_ST")
                {
                    List<int> allneckTurnGains = new List<int>();
                    List<int> alloriginalGroundHeightMSs = new List<int>();
                    List<int> allminAnkleHeightMSs = new List<int>();
                    List<int> allmaxAnkleHeightMSs = new List<int>();
                    List<int> allcosineMaxKneeAngles = new List<int>();
                    List<int> allcosineMinKneeAngles = new List<int>();
                    List<int> allfootPlantedAnkleHeightMSs = new List<int>();
                    List<int> allfootRaisedAnkleHeightMSs = new List<int>();
                    List<int> allrayCastDistanceUps = new List<int>();
                    List<int> allraycastDistanceDowns = new List<int>();
                    List<int> allfootEndLS_Xs = new List<int>();
                    List<int> allfootEndLS_Ys = new List<int>();
                    List<int> allfootEndLS_Zs = new List<int>();
                    List<int> allonOffGains = new List<int>();
                    List<int> allgroundAscendingGains = new List<int>();
                    List<int> allgroundDescendingGains = new List<int>();
                    List<int> allfootRaisedGains = new List<int>();
                    List<int> allfootDescendingGains = new List<int>();
                    List<int> allfootUnlockGains = new List<int>();
                    List<int> allkneeAxisTypes = new List<int>();
                    List<int> alluseFootLockings = new List<int>();
                    List<int> allfootPlacementOns = new List<int>();
                    List<int> alltwistKneeAxisTypes = new List<int>();
                    List<int> allneckTurnPrioritys = new List<int>();
                    List<int> allneckTurnMaxAngles = new List<int>();

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "neckTurnGain")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allneckTurnGains.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "originalGroundHeightMS")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                alloriginalGroundHeightMSs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "minAnkleHeightMS")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allminAnkleHeightMSs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "maxAnkleHeightMS")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allmaxAnkleHeightMSs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "cosineMaxKneeAngle")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allcosineMaxKneeAngles.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "cosineMinKneeAngle")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allcosineMinKneeAngles.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "footPlantedAnkleHeightMS")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allfootPlantedAnkleHeightMSs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "footRaisedAnkleHeightMS")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allfootRaisedAnkleHeightMSs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "rayCastDistanceUp")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allrayCastDistanceUps.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "raycastDistanceDown")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allraycastDistanceDowns.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "footEndLS_X")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allfootEndLS_Xs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "footEndLS_Y")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allfootEndLS_Ys.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "footEndLS_Z")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allfootEndLS_Zs.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "onOffGain")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allonOffGains.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "groundAscendingGain")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allgroundAscendingGains.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "groundDescendingGain")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allgroundDescendingGains.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "footRaisedGain")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allfootRaisedGains.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "footDescendingGain")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allfootDescendingGains.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "footUnlockGain")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allfootUnlockGains.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "kneeAxisType")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allkneeAxisTypes.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "useFootLocking")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                alluseFootLockings.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "footPlacementOn")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allfootPlacementOns.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "twistKneeAxisType")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                alltwistKneeAxisTypes.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "neckTurnPriority")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allneckTurnPrioritys.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                            else if (cell.Def.Name == "neckTurnMaxAngle")
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                allneckTurnMaxAngles.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                            }

                        }
                    }

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            if (cell.Def.Name == "neckTurnGain")
                            {
                                int randomIndex = r.Next(allneckTurnGains.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allneckTurnGains[randomIndex], null);
                                }

                                allneckTurnGains.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "originalGroundHeightMS")
                            {
                                int randomIndex = r.Next(alloriginalGroundHeightMSs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, alloriginalGroundHeightMSs[randomIndex], null);
                                }

                                alloriginalGroundHeightMSs.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "minAnkleHeightMS")
                            {
                                int randomIndex = r.Next(allminAnkleHeightMSs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allminAnkleHeightMSs[randomIndex], null);
                                }

                                allminAnkleHeightMSs.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "maxAnkleHeightMS")
                            {
                                int randomIndex = r.Next(allmaxAnkleHeightMSs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allmaxAnkleHeightMSs[randomIndex], null);
                                }

                                allmaxAnkleHeightMSs.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "cosineMaxKneeAngle")
                            {
                                int randomIndex = r.Next(allcosineMaxKneeAngles.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allcosineMaxKneeAngles[randomIndex], null);
                                }

                                allcosineMaxKneeAngles.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "cosineMinKneeAngle")
                            {
                                int randomIndex = r.Next(allcosineMinKneeAngles.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allcosineMinKneeAngles[randomIndex], null);
                                }

                                allcosineMinKneeAngles.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "footPlantedAnkleHeightMS")
                            {
                                int randomIndex = r.Next(allfootPlantedAnkleHeightMSs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allfootPlantedAnkleHeightMSs[randomIndex], null);
                                }

                                allfootPlantedAnkleHeightMSs.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "footRaisedAnkleHeightMS")
                            {
                                int randomIndex = r.Next(allfootRaisedAnkleHeightMSs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allfootRaisedAnkleHeightMSs[randomIndex], null);
                                }

                                allfootRaisedAnkleHeightMSs.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "rayCastDistanceUp")
                            {
                                int randomIndex = r.Next(allrayCastDistanceUps.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allrayCastDistanceUps[randomIndex], null);
                                }

                                allrayCastDistanceUps.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "raycastDistanceDown")
                            {
                                int randomIndex = r.Next(allraycastDistanceDowns.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allraycastDistanceDowns[randomIndex], null);
                                }

                                allraycastDistanceDowns.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "footEndLS_X")
                            {
                                int randomIndex = r.Next(allfootEndLS_Xs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allfootEndLS_Xs[randomIndex], null);
                                }

                                allfootEndLS_Xs.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "footEndLS_Y")
                            {
                                int randomIndex = r.Next(allfootEndLS_Ys.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allfootEndLS_Ys[randomIndex], null);
                                }

                                allfootEndLS_Ys.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "footEndLS_Z")
                            {
                                int randomIndex = r.Next(allfootEndLS_Zs.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allfootEndLS_Zs[randomIndex], null);
                                }

                                allfootEndLS_Zs.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "onOffGain")
                            {
                                int randomIndex = r.Next(allonOffGains.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allonOffGains[randomIndex], null);
                                }

                                allonOffGains.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "groundAscendingGain")
                            {
                                int randomIndex = r.Next(allgroundAscendingGains.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allgroundAscendingGains[randomIndex], null);
                                }

                                allgroundAscendingGains.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "groundDescendingGain")
                            {
                                int randomIndex = r.Next(allgroundDescendingGains.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allgroundDescendingGains[randomIndex], null);
                                }

                                allgroundDescendingGains.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "footRaisedGain")
                            {
                                int randomIndex = r.Next(allfootRaisedGains.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allfootRaisedGains[randomIndex], null);
                                }

                                allfootRaisedGains.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "footDescendingGain")
                            {
                                int randomIndex = r.Next(allfootDescendingGains.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allfootDescendingGains[randomIndex], null);
                                }

                                allfootDescendingGains.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "footUnlockGain")
                            {
                                int randomIndex = r.Next(allfootUnlockGains.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allfootUnlockGains[randomIndex], null);
                                }

                                allfootUnlockGains.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "kneeAxisType")
                            {
                                int randomIndex = r.Next(allkneeAxisTypes.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allkneeAxisTypes[randomIndex], null);
                                }

                                allkneeAxisTypes.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "useFootLocking")
                            {
                                int randomIndex = r.Next(alluseFootLockings.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, alluseFootLockings[randomIndex], null);
                                }

                                alluseFootLockings.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "footPlacementOn")
                            {
                                int randomIndex = r.Next(allfootPlacementOns.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allfootPlacementOns[randomIndex], null);
                                }

                                allfootPlacementOns.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "twistKneeAxisType")
                            {
                                int randomIndex = r.Next(alltwistKneeAxisTypes.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, alltwistKneeAxisTypes[randomIndex], null);
                                }

                                alltwistKneeAxisTypes.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "neckTurnPriority")
                            {
                                int randomIndex = r.Next(allneckTurnPrioritys.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allneckTurnPrioritys[randomIndex], null);
                                }

                                allneckTurnPrioritys.RemoveAt(randomIndex);
                            }

                            else if (cell.Def.Name == "neckTurnMaxAngle")
                            {
                                int randomIndex = r.Next(allneckTurnMaxAngles.Count);
                                Type type = cell.GetType();
                                PropertyInfo prop = type.GetProperty("Value");

                                //if (chkSkeletons.Checked)
                                if (true)
                                {
                                    prop.SetValue(cell, allneckTurnMaxAngles[randomIndex], null);
                                }

                                allneckTurnMaxAngles.RemoveAt(randomIndex);
                            }

                        }
                    }
                }
                else if (paramFile.ID == "BULLET_PARAM_ST")
                {
                    //build a list of all properties to rando
                    List<int> atkId_BulletVals = new List<int>();
                    List<int> sfxId_BulletVals = new List<int>();
                    List<int> sfxId_HitVals = new List<int>();
                    List<int> sfxId_FlickVals = new List<int>();
                    List<float> lifeVals = new List<float>();
                    List<float> distVals = new List<float>();
                    List<float> shootIntervalVals = new List<float>();
                    List<float> gravityInRangeVals = new List<float>();
                    List<float> gravityOutRangeVals = new List<float>();
                    List<float> hormingStopRangeVals = new List<float>();
                    List<float> initVellocityVals = new List<float>();
                    List<float> accelInRangeVals = new List<float>();
                    List<float> accelOutRangeVals = new List<float>();
                    List<float> maxVellocityVals = new List<float>();
                    List<float> minVellocityVals = new List<float>();
                    List<float> accelTimeVals = new List<float>();
                    List<float> homingBeginDistVals = new List<float>();
                    List<float> hitRadiusVals = new List<float>();
                    List<float> hitRadiusMaxVals = new List<float>();
                    List<float> spreadTimeVals = new List<float>();
                    List<float> hormingOffsetRangeVals = new List<float>();
                    List<float> dmgHitRecordLifeTimeVals = new List<float>();
                    List<int> spEffectIDForShooterVals = new List<int>();
                    List<int> HitBulletIDVals = new List<int>();
                    List<int> spEffectId0Vals = new List<int>();
                    List<ushort> numShootVals = new List<ushort>();
                    List<short> homingAngleVals = new List<short>();
                    List<short> shootAngleVals = new List<short>();
                    List<short> shootAngleIntervalVals = new List<short>();
                    List<short> shootAngleXIntervalVals = new List<short>();
                    List<sbyte> damageDampVals = new List<sbyte>();
                    List<sbyte> spelDamageDampVals = new List<sbyte>();
                    List<sbyte> fireDamageDampVals = new List<sbyte>();
                    List<sbyte> thunderDamageDampVals = new List<sbyte>();
                    List<sbyte> staminaDampVals = new List<sbyte>();
                    List<sbyte> knockbackDampVals = new List<sbyte>();
                    List<sbyte> shootAngleXZVals = new List<sbyte>();
                    List<int> lockShootLimitAngVals = new List<int>();
                    List<int> isPenetrateVals = new List<int>();
                    List<int> atkAttributeVals = new List<int>();
                    List<int> spAttributeVals = new List<int>();
                    List<int> Material_AttackTypeVals = new List<int>();
                    List<int> Material_AttackMaterialVals = new List<int>();
                    List<int> Material_SizeVals = new List<int>();
                    List<int> launchConditionTypeVals = new List<int>();
                    List<int> FollowTypeVals = new List<int>();
                    List<int> isAttackSFXVals = new List<int>();
                    List<int> isEndlessHitVals = new List<int>();
                    List<int> isPenetrateMapVals = new List<int>();
                    List<int> isHitBothTeamVals = new List<int>();
                    List<int> isUseSharedHitListVals = new List<int>();
                    List<int> isHitForceMagicVals = new List<int>();
                    List<int> isIgnoreSfxIfHitWaterVals = new List<int>();
                    List<int> IsIgnoreMoveStateIfHitWaterVals = new List<int>();
                    List<int> isHitDarkForceMagicVals = new List<int>();

                    List<int> humanityBulletIds = new List<int>() { 41700, 41710, 41720 };

                    //add to list to rando
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        //if nerf humanities is off or if not humanity
                        if(!checkBoxNerfHumanityBullets.Checked || !humanityBulletIds.Contains(paramRow.ID))
                        {
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "atkId_Bullet")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if (Convert.ToInt32(prop.GetValue(cell, null)) > 0)
                                    {
                                        atkId_BulletVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                }
                                else if (cell.Def.Name == "sfxId_Bullet")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    sfxId_BulletVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "sfxId_Hit")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    sfxId_HitVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "sfxId_Flick")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    sfxId_FlickVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "life")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    lifeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "dist")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if ((float)(prop.GetValue(cell, null)) > 0)
                                    {
                                        distVals.Add((float)(prop.GetValue(cell, null)));
                                    }
                                }
                                else if (cell.Def.Name == "shootInterval")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if ((float)(prop.GetValue(cell, null)) > 0)
                                    {
                                        shootIntervalVals.Add((float)(prop.GetValue(cell, null)));
                                    }
                                }
                                else if (cell.Def.Name == "gravityInRange")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    gravityInRangeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "gravityOutRange")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    gravityOutRangeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "hormingStopRange")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    hormingStopRangeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "initVellocity")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if ((float)(prop.GetValue(cell, null)) > 0)
                                    {
                                        initVellocityVals.Add((float)(prop.GetValue(cell, null)));
                                    }
                                }
                                else if (cell.Def.Name == "accelInRange")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    accelInRangeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "accelOutRange")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    accelOutRangeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "maxVellocity")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if ((float)(prop.GetValue(cell, null)) > 0)
                                    {
                                        maxVellocityVals.Add((float)(prop.GetValue(cell, null)));
                                    }
                                }
                                else if (cell.Def.Name == "minVellocity")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    minVellocityVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "accelTime")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    accelTimeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "homingBeginDist")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    homingBeginDistVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "hitRadius")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    hitRadiusVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "hitRadiusMax")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    hitRadiusMaxVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "spreadTime")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    spreadTimeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "hormingOffsetRange")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    hormingOffsetRangeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "dmgHitRecordLifeTime")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    dmgHitRecordLifeTimeVals.Add((float)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "spEffectIDForShooter")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    spEffectIDForShooterVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "HitBulletID")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if(!checkBoxForceUseableBullets.Checked || Convert.ToInt32(prop.GetValue(cell, null)) != -1)
                                    {
                                        HitBulletIDVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    }
                                }
                                else if (cell.Def.Name == "spEffectId0")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    spEffectId0Vals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "numShoot")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    numShootVals.Add((ushort)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "homingAngle")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    homingAngleVals.Add((short)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "shootAngle")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    shootAngleVals.Add((short)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "shootAngleInterval")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    shootAngleIntervalVals.Add((short)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "shootAngleXInterval")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    shootAngleXIntervalVals.Add((short)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "damageDamp")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    damageDampVals.Add((sbyte)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "spelDamageDamp")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    spelDamageDampVals.Add((sbyte)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "fireDamageDamp")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    fireDamageDampVals.Add((sbyte)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "thunderDamageDamp")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    thunderDamageDampVals.Add((sbyte)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "staminaDamp")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    staminaDampVals.Add((sbyte)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "knockbackDamp")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    knockbackDampVals.Add((sbyte)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "shootAngleXZ")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    shootAngleXZVals.Add((sbyte)(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "lockShootLimitAng")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    lockShootLimitAngVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "isPenetrate")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    isPenetrateVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "atkAttribute")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    atkAttributeVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "spAttribute")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    spAttributeVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "Material_AttackType")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    Material_AttackTypeVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "Material_AttackMaterial")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    Material_AttackMaterialVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "Material_Size")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    Material_SizeVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "launchConditionType")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    launchConditionTypeVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "FollowType")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    FollowTypeVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "isAttackSFX")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    isAttackSFXVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "isEndlessHit")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    isEndlessHitVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "isPenetrateMap")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    isPenetrateMapVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "isHitBothTeam")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    isHitBothTeamVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "isUseSharedHitList")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    isUseSharedHitListVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "isHitForceMagic")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    isHitForceMagicVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "isIgnoreSfxIfHitWater")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    isIgnoreSfxIfHitWaterVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "IsIgnoreMoveStateIfHitWater")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    IsIgnoreMoveStateIfHitWaterVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                                else if (cell.Def.Name == "isHitDarkForceMagic")
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    isHitDarkForceMagicVals.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                }
                            }
                        }
                    }

                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        //if nerf humanities is off or if not humanity
                        if (!checkBoxNerfHumanityBullets.Checked || !humanityBulletIds.Contains(paramRow.ID))
                        {
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "atkId_Bullet")
                                {
                                    int randomIndex = r.Next(atkId_BulletVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (Convert.ToInt32(prop.GetValue(cell, null)) > 0 || checkBoxForceUseableBullets.Checked)
                                    {
                                        if (chkBullets.Checked)
                                        {
                                            prop.SetValue(cell, atkId_BulletVals[randomIndex], null);
                                        }
                                        if(!checkBoxForceUseableBullets.Checked)
                                        {
                                            atkId_BulletVals.RemoveAt(randomIndex);
                                        }
                                    }
                                }
                                else if (cell.Def.Name == "sfxId_Bullet")
                                {
                                    int randomIndex = r.Next(sfxId_BulletVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, sfxId_BulletVals[randomIndex], null);
                                    }
                                    sfxId_BulletVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "sfxId_Hit")
                                {
                                    int randomIndex = r.Next(sfxId_HitVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, sfxId_HitVals[randomIndex], null);
                                    }

                                    sfxId_HitVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "sfxId_Flick")
                                {
                                    int randomIndex = r.Next(sfxId_FlickVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, sfxId_FlickVals[randomIndex], null);
                                    }

                                    sfxId_FlickVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "life")
                                {
                                    int randomIndex = r.Next(lifeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, lifeVals[randomIndex], null);
                                    }

                                    lifeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "dist")
                                {
                                    int randomIndex = r.Next(distVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if ((float)(prop.GetValue(cell, null)) > 0)
                                    {
                                        if (chkBullets.Checked)
                                        {
                                            prop.SetValue(cell, distVals[randomIndex], null);
                                        }

                                        distVals.RemoveAt(randomIndex);
                                    }
                                }
                                else if (cell.Def.Name == "shootInterval")
                                {
                                    int randomIndex = r.Next(shootIntervalVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if ((float)(prop.GetValue(cell, null)) > 0)
                                    {
                                        if (chkBullets.Checked)
                                        {
                                            prop.SetValue(cell, shootIntervalVals[randomIndex], null);
                                        }

                                        shootIntervalVals.RemoveAt(randomIndex);
                                    }
                                }
                                else if (cell.Def.Name == "gravityInRange")
                                {
                                    int randomIndex = r.Next(gravityInRangeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, gravityInRangeVals[randomIndex], null);
                                    }

                                    gravityInRangeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "gravityOutRange")
                                {
                                    int randomIndex = r.Next(gravityOutRangeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, gravityOutRangeVals[randomIndex], null);
                                    }

                                    gravityOutRangeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "hormingStopRange")
                                {
                                    int randomIndex = r.Next(hormingStopRangeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, hormingStopRangeVals[randomIndex], null);
                                    }

                                    hormingStopRangeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "initVellocity")
                                {
                                    int randomIndex = r.Next(initVellocityVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if ((float)(prop.GetValue(cell, null)) > 0)
                                    {
                                        if (chkBullets.Checked)
                                        {
                                            prop.SetValue(cell, initVellocityVals[randomIndex], null);
                                        }

                                        initVellocityVals.RemoveAt(randomIndex);
                                    }
                                }
                                else if (cell.Def.Name == "accelInRange")
                                {
                                    int randomIndex = r.Next(accelInRangeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, accelInRangeVals[randomIndex], null);
                                    }

                                    accelInRangeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "accelOutRange")
                                {
                                    int randomIndex = r.Next(accelOutRangeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, accelOutRangeVals[randomIndex], null);
                                    }

                                    accelOutRangeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "maxVellocity")
                                {
                                    int randomIndex = r.Next(maxVellocityVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if ((float)(prop.GetValue(cell, null)) > 0)
                                    {
                                        if (chkBullets.Checked)
                                        {
                                            prop.SetValue(cell, maxVellocityVals[randomIndex], null);
                                        }

                                        maxVellocityVals.RemoveAt(randomIndex);
                                    }
                                }
                                else if (cell.Def.Name == "minVellocity")
                                {
                                    int randomIndex = r.Next(minVellocityVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, minVellocityVals[randomIndex], null);
                                    }

                                    minVellocityVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "accelTime")
                                {
                                    int randomIndex = r.Next(accelTimeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, accelTimeVals[randomIndex], null);
                                    }

                                    accelTimeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "homingBeginDist")
                                {
                                    int randomIndex = r.Next(homingBeginDistVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, homingBeginDistVals[randomIndex], null);
                                    }

                                    homingBeginDistVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "hitRadius")
                                {
                                    int randomIndex = r.Next(hitRadiusVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        if(!checkBoxForceUseableBullets.Checked || hitRadiusVals[randomIndex] >= 0.5)
                                        {
                                            prop.SetValue(cell, hitRadiusVals[randomIndex], null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cell, 0.5, null);
                                        }
                                    }

                                    hitRadiusVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "hitRadiusMax")
                                {
                                    int randomIndex = r.Next(hitRadiusMaxVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, hitRadiusMaxVals[randomIndex], null);
                                    }

                                    hitRadiusMaxVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "spreadTime")
                                {
                                    int randomIndex = r.Next(spreadTimeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, spreadTimeVals[randomIndex], null);
                                    }

                                    spreadTimeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "hormingOffsetRange")
                                {
                                    int randomIndex = r.Next(hormingOffsetRangeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, hormingOffsetRangeVals[randomIndex], null);
                                    }

                                    hormingOffsetRangeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "dmgHitRecordLifeTime")
                                {
                                    int randomIndex = r.Next(dmgHitRecordLifeTimeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, dmgHitRecordLifeTimeVals[randomIndex], null);
                                    }

                                    dmgHitRecordLifeTimeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "spEffectIDForShooter")
                                {
                                    int randomIndex = r.Next(spEffectIDForShooterVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, spEffectIDForShooterVals[randomIndex], null);
                                    }

                                    spEffectIDForShooterVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "HitBulletID")
                                {
                                    int randomIndex = r.Next(HitBulletIDVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (!checkBoxForceUseableBullets.Checked || Convert.ToInt32(prop.GetValue(cell, null)) != -1)
                                    {
                                        if (chkBullets.Checked)
                                        {
                                            prop.SetValue(cell, HitBulletIDVals[randomIndex], null);
                                        }

                                        HitBulletIDVals.RemoveAt(randomIndex);
                                    }
                                }
                                else if (cell.Def.Name == "spEffectId0")
                                {
                                    int randomIndex = r.Next(spEffectId0Vals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, spEffectId0Vals[randomIndex], null);
                                    }

                                    spEffectId0Vals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "numShoot")
                                {
                                    int randomIndex = r.Next(numShootVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, numShootVals[randomIndex], null);
                                    }

                                    numShootVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "homingAngle")
                                {
                                    int randomIndex = r.Next(homingAngleVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, homingAngleVals[randomIndex], null);
                                    }

                                    homingAngleVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "shootAngle")
                                {
                                    int randomIndex = r.Next(shootAngleVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, shootAngleVals[randomIndex], null);
                                    }

                                    shootAngleVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "shootAngleInterval")
                                {
                                    int randomIndex = r.Next(shootAngleIntervalVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, shootAngleIntervalVals[randomIndex], null);
                                    }

                                    shootAngleIntervalVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "shootAngleXInterval")
                                {
                                    int randomIndex = r.Next(shootAngleXIntervalVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, shootAngleXIntervalVals[randomIndex], null);
                                    }

                                    shootAngleXIntervalVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "damageDamp")
                                {
                                    int randomIndex = r.Next(damageDampVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, damageDampVals[randomIndex], null);
                                    }

                                    damageDampVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "spelDamageDamp")
                                {
                                    int randomIndex = r.Next(spelDamageDampVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, spelDamageDampVals[randomIndex], null);
                                    }

                                    spelDamageDampVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "fireDamageDamp")
                                {
                                    int randomIndex = r.Next(fireDamageDampVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, fireDamageDampVals[randomIndex], null);
                                    }

                                    fireDamageDampVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "thunderDamageDamp")
                                {
                                    int randomIndex = r.Next(thunderDamageDampVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, thunderDamageDampVals[randomIndex], null);
                                    }

                                    thunderDamageDampVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "staminaDamp")
                                {
                                    int randomIndex = r.Next(staminaDampVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, staminaDampVals[randomIndex], null);
                                    }

                                    staminaDampVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "knockbackDamp")
                                {
                                    int randomIndex = r.Next(knockbackDampVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, knockbackDampVals[randomIndex], null);
                                    }

                                    knockbackDampVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "shootAngleXZ")
                                {
                                    int randomIndex = r.Next(shootAngleXZVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, shootAngleXZVals[randomIndex], null);
                                    }

                                    shootAngleXZVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "lockShootLimitAng")
                                {
                                    int randomIndex = r.Next(lockShootLimitAngVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, lockShootLimitAngVals[randomIndex], null);
                                    }

                                    lockShootLimitAngVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "isPenetrate")
                                {
                                    int randomIndex = r.Next(isPenetrateVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, isPenetrateVals[randomIndex], null);
                                    }

                                    isPenetrateVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "atkAttribute")
                                {
                                    int randomIndex = r.Next(atkAttributeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, atkAttributeVals[randomIndex], null);
                                    }

                                    atkAttributeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "spAttribute")
                                {
                                    int randomIndex = r.Next(spAttributeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, spAttributeVals[randomIndex], null);
                                    }

                                    spAttributeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "Material_AttackType")
                                {
                                    int randomIndex = r.Next(Material_AttackTypeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, Material_AttackTypeVals[randomIndex], null);
                                    }

                                    Material_AttackTypeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "Material_AttackMaterial")
                                {
                                    int randomIndex = r.Next(Material_AttackMaterialVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, Material_AttackMaterialVals[randomIndex], null);
                                    }

                                    Material_AttackMaterialVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "Material_Size")
                                {
                                    int randomIndex = r.Next(Material_SizeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, Material_SizeVals[randomIndex], null);
                                    }

                                    Material_SizeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "launchConditionType")
                                {
                                    int randomIndex = r.Next(launchConditionTypeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, launchConditionTypeVals[randomIndex], null);
                                    }

                                    launchConditionTypeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "FollowType")
                                {
                                    int randomIndex = r.Next(FollowTypeVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, FollowTypeVals[randomIndex], null);
                                    }

                                    FollowTypeVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "isAttackSFX")
                                {
                                    int randomIndex = r.Next(isAttackSFXVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, isAttackSFXVals[randomIndex], null);
                                    }

                                    isAttackSFXVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "isEndlessHit")
                                {
                                    int randomIndex = r.Next(isEndlessHitVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, isEndlessHitVals[randomIndex], null);
                                    }

                                    isEndlessHitVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "isPenetrateMap")
                                {
                                    int randomIndex = r.Next(isPenetrateMapVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, isPenetrateMapVals[randomIndex], null);
                                    }

                                    isPenetrateMapVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "isHitBothTeam")
                                {
                                    int randomIndex = r.Next(isHitBothTeamVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, isHitBothTeamVals[randomIndex], null);
                                    }

                                    isHitBothTeamVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "isUseSharedHitList")
                                {
                                    int randomIndex = r.Next(isUseSharedHitListVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, isUseSharedHitListVals[randomIndex], null);
                                    }

                                    isUseSharedHitListVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "isHitForceMagic")
                                {
                                    int randomIndex = r.Next(isHitForceMagicVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, isHitForceMagicVals[randomIndex], null);
                                    }

                                    isHitForceMagicVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "isIgnoreSfxIfHitWater")
                                {
                                    int randomIndex = r.Next(isIgnoreSfxIfHitWaterVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, isIgnoreSfxIfHitWaterVals[randomIndex], null);
                                    }

                                    isIgnoreSfxIfHitWaterVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "IsIgnoreMoveStateIfHitWater")
                                {
                                    int randomIndex = r.Next(IsIgnoreMoveStateIfHitWaterVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, IsIgnoreMoveStateIfHitWaterVals[randomIndex], null);
                                    }

                                    IsIgnoreMoveStateIfHitWaterVals.RemoveAt(randomIndex);
                                }
                                else if (cell.Def.Name == "isHitDarkForceMagicList")
                                {
                                    int randomIndex = r.Next(isHitDarkForceMagicVals.Count);
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkBullets.Checked)
                                    {
                                        prop.SetValue(cell, isHitDarkForceMagicVals[randomIndex], null);
                                    }

                                    isHitDarkForceMagicVals.RemoveAt(randomIndex);
                                }
                            }
                        }
                    }
                }
                else if (paramFile.ID == "CHARACTER_INIT_PARAM")
                {
                    List<int> validItems = new List<int>();
                    List<int> validTitaniteItems = new List<int>();
                    List<int> validSoulItems = new List<int>();
                    List<int> validUnstackableItems = new List<int>();
                    List<int> validRings = new List<int>();
                    List<int> classStartingLevel = new List<int>();
                    List<int> classStatTotals = new List<int>();
                    if(checkBoxStartingGifts.Checked)
                    {
                        //add validUnstackableItems
                        validUnstackableItems.Add(100); //white soapstone
                        validUnstackableItems.Add(101); //red soapstone
                        validUnstackableItems.Add(106); //orange soapstone
                        validUnstackableItems.Add(220); //silver pendant
                        validUnstackableItems.Add(371); //Binoculars
                        validUnstackableItems.Add(376); //Pendant
                        validUnstackableItems.Add(385); //Dried Finger
                        for (int i = 0; i < 5; i++) //carvings
                        {
                            validUnstackableItems.Add(510 + i);
                        }
                        for (int i = 0; i < 3; i++) //smith and repair boxes
                        {
                            validUnstackableItems.Add(2600 + i);
                        }
                        validUnstackableItems.Add(2608); //Bottomless Box

                        //add validItems
                        validItems.Add(240); //divine blessing
                        validItems.Add(260); //green blossom
                        for (int i = 0; i < 6; i++)
                        {
                            if(i != 3)
                            {
                                validItems.Add(270 + i);
                            }
                        }
                        validItems.Add(280); //repair powder
                        for (int i = 0; i < 8; i++)
                        {
                            if (i != 5)
                            {
                                validItems.Add(290 + i);
                            }
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            validItems.Add(310 + i);
                        }
                        validItems.Add(330); //Homeward Bone
                        for (int i = 0; i < 6; i++)
                        {
                            if (i != 1 && i != 2)
                            {
                                validItems.Add(370 + i);
                            }
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            validItems.Add(380 + i);
                        }
                        for (int i = 0; i < 7; i++) //fire keeper souls
                        {
                            validSoulItems.Add(390 + i);
                        }
                        for (int i = 0; i < 10; i++) //generic soul items
                        {
                            validItems.Add(400 + i);
                        }
                        validItems.Add(500); //Humanity
                        validItems.Add(501); //Twin Humanities
                        for (int i = 0; i < 12; i++) //boss souls
                        {
                            validSoulItems.Add(700 + i);
                        }
                        for (int i = 0; i < 14; i++) //upgrade materia
                        {
                            validTitaniteItems.Add(1000 + (i*10));
                        }
                        if(!checkBoxPreventSpellGifts.Checked) //spells, miracles, pyromancies start here
                        {
                            for (int i = 0; i < 13; i++) //sorceries
                            {
                                if (i != 8 && i != 9)
                                {
                                    validItems.Add(3000 + (i * 10));
                                }
                            }
                            validItems.Add(3300);
                            validItems.Add(3310);
                            validItems.Add(3400);
                            validItems.Add(3410);
                            for (int i = 0; i < 6; i++)
                            {
                                validItems.Add(3500 + (i * 10));
                            }
                            validItems.Add(3600);
                            validItems.Add(3610);
                            for (int i = 0; i < 5; i++)
                            {
                                validItems.Add(3700 + (i * 10));
                            }
                            for (int i = 0; i < 7; i++) //pyromancies
                            {
                                validItems.Add(4000 + (i * 10));
                            }
                            validItems.Add(4100);
                            validItems.Add(4110);
                            validItems.Add(4200);
                            validItems.Add(4210);
                            validItems.Add(4220);
                            validItems.Add(4300);
                            validItems.Add(4310);
                            validItems.Add(4360);
                            validItems.Add(4400);
                            validItems.Add(4500);
                            validItems.Add(4510);
                            validItems.Add(4520);
                            validItems.Add(4530);
                            for (int i = 0; i < 6; i++) //miracles
                            {
                                validItems.Add(5000 + (i * 10));
                            }
                            validItems.Add(5100);
                            validItems.Add(5110);
                            validItems.Add(5200);
                            validItems.Add(5210);
                            validItems.Add(5300);
                            validItems.Add(5310);
                            validItems.Add(5320);
                            validItems.Add(5400);
                            validItems.Add(5500);
                            validItems.Add(5510);
                            validItems.Add(5520);
                            validItems.Add(5600);
                            validItems.Add(5610);
                            validItems.Add(5700);
                            validItems.Add(5800);
                            validItems.Add(5810);
                            validItems.Add(5900);
                            validItems.Add(5910);
                        }

                        //add valid rings
                        for (int i = 0; i < 31; i++)
                        {
                            if (i != 12 && i != 18 && i != 29)
                            {
                                validRings.Add(100 + i);
                            }
                        }
                        validRings.Add(137);
                        validRings.Add(138);
                        validRings.Add(139);
                        for (int i = 1; i < 11; i++)
                        {
                            validRings.Add(140 + i);
                        }
                    }

                    //read values
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        //if is a starting class
                        if(paramRow.ID >= 3000 && paramRow.ID <= 3009)
                        {
                            //if is hunter or spellcaster
                            if(paramRow.ID >= 3005 && paramRow.ID <= 3008)
                            {
                                int secondaryIndex = paramRow.ID - 3005;
                                foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                {
                                    if (cell.Def.Name == "equip_Subwep_Right")
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        secondaryStartingWeapons[secondaryIndex] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                }
                            }
                            //if is spellcaster
                            if(paramRow.ID >= 3006 && paramRow.ID <= 3008)
                            {
                                int casterIndex = paramRow.ID - 3006;
                                foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if (cell.Def.Name == "equip_Spell_01")
                                    {
                                        startingSpells[casterIndex] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                }
                            }
                            int statTotal = 0;
                            int classId = paramRow.ID - 3000;
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                if (cell.Def.Name == "soulLv")
                                {
                                    classStartingLevel.Add(Convert.ToInt32(prop.GetValue(cell, null)));
                                    //assign class levels now so they can be read from; will be changed later if randomized
                                    classLevels[classId] = Convert.ToInt32(prop.GetValue(cell, null));
                                }
                                else if (cell.Def.Name == "baseVit" || cell.Def.Name == "baseWil" || cell.Def.Name == "baseEnd" || cell.Def.Name == "baseStr" ||
                                    cell.Def.Name == "baseDex" || cell.Def.Name == "baseMag" || cell.Def.Name == "baseFai" || cell.Def.Name == "baseDurability")
                                {
                                    statTotal += Convert.ToInt32(prop.GetValue(cell, null));
                                    //assign class levels now so they can be read from; will be changed later if randomized
                                    if(cell.Def.Name == "baseVit")
                                    {
                                        classStats[classId, 0] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                    else if(cell.Def.Name == "baseWil")
                                    {
                                        classStats[classId, 1] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                    else if (cell.Def.Name == "baseEnd")
                                    {
                                        classStats[classId, 2] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                    else if (cell.Def.Name == "baseStr")
                                    {
                                        classStats[classId, 3] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                    else if (cell.Def.Name == "baseDex")
                                    {
                                        classStats[classId, 4] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                    else if (cell.Def.Name == "baseMag")
                                    {
                                        classStats[classId, 5] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                    else if (cell.Def.Name == "baseFai")
                                    {
                                        classStats[classId, 6] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                    else if (cell.Def.Name == "baseDurability")
                                    {
                                        classStats[classId, 7] = Convert.ToInt32(prop.GetValue(cell, null));
                                    }
                                }
                                else if (cell.Def.Name == "equip_Wep_Right")
                                {
                                    startingWeapons[classId, 0] = Convert.ToInt32(prop.GetValue(cell, null));
                                }
                                else if (cell.Def.Name == "equip_Wep_Left")
                                {
                                    startingWeapons[classId, 1] = Convert.ToInt32(prop.GetValue(cell, null));
                                }
                            }
                            classStatTotals.Add(statTotal);
                        }
                    }

                    //determine levels and stats of each class
                    for(int i = 0; i < 10; i++)
                    {
                        int indexOfLevel = r.Next(classStartingLevel.Count);
                        int indexOfStatTotal = r.Next(classStatTotals.Count);

                        int level = classStartingLevel[indexOfLevel];
                        int culledStatTotal = classStatTotals[indexOfStatTotal];

                        double VitWeight = r.NextDouble();
                        double WilWeight = r.NextDouble();
                        double EndWeight = r.NextDouble();
                        double StrWeight = r.NextDouble();
                        double DexWeight = r.NextDouble();
                        double MagWeight = r.NextDouble();
                        double FaiWeight = r.NextDouble();
                        double ResistanceWeight = r.NextDouble(); //basedurability

                        double totalWeight = VitWeight + WilWeight + EndWeight + StrWeight + DexWeight + MagWeight + FaiWeight + ResistanceWeight;

                        int Vit = (int)(culledStatTotal * (VitWeight / totalWeight)) + 1;
                        int Wil = (int)(culledStatTotal * (WilWeight / totalWeight)) + 1;
                        int End = (int)(culledStatTotal * (EndWeight / totalWeight)) + 1;
                        int Str = (int)(culledStatTotal * (StrWeight / totalWeight)) + 1;
                        int Dex = (int)(culledStatTotal * (DexWeight / totalWeight)) + 1;
                        int Mag = (int)(culledStatTotal * (MagWeight / totalWeight)) + 1;
                        int Fai = (int)(culledStatTotal * (FaiWeight / totalWeight)) + 1;
                        int Res = (culledStatTotal - (Vit + Wil + End + Str + Dex + Mag + Fai));
                        if (Res < 1)
                        {
                            Res = 1;
                        }

                        if(i >= 6 && i <= 8 && Wil < 10) //6, 7, and 8 are magic based classes. make sure they have at least one attunement slot
                        {
                            int WilToAdd = 10 - Wil;
                            while(WilToAdd > 0)
                            {
                                int statToTry = r.Next(7); //0-7 skipping Wil
                                if(statToTry == 0) //Vit
                                {
                                    if(Vit > 1)
                                    {
                                        Vit--;
                                        WilToAdd--;
                                        Wil++;
                                    }
                                }
                                else if(statToTry == 1) //End
                                {
                                    if (End > 1)
                                    {
                                        End--;
                                        WilToAdd--;
                                        Wil++;
                                    }
                                }
                                else if(statToTry == 2) //Str
                                {
                                    if (Str > 1)
                                    {
                                        Str--;
                                        WilToAdd--;
                                        Wil++;
                                    }
                                }
                                else if (statToTry == 3) //Dex
                                {
                                    if (Dex > 1)
                                    {
                                        Dex--;
                                        WilToAdd--;
                                        Wil++;
                                    }
                                }
                                else if (statToTry == 4) //Mag
                                {
                                    if (Mag > 1)
                                    {
                                        Mag--;
                                        WilToAdd--;
                                        Wil++;
                                    }
                                }
                                else if (statToTry == 5) //Fai
                                {
                                    if (Fai > 1)
                                    {
                                        Fai--;
                                        WilToAdd--;
                                        Wil++;
                                    }
                                }
                                else if (statToTry == 6) //Res
                                {
                                    if (Res > 1)
                                    {
                                        Res--;
                                        WilToAdd--;
                                        Wil++;
                                    }
                                }
                            }
                        }

                        classLevels[i] = level;
                        classStats[i, 0] = Vit;
                        classStats[i, 1] = Wil;
                        classStats[i, 2] = End;
                        classStats[i, 3] = Str;
                        classStats[i, 4] = Dex;
                        classStats[i, 5] = Mag;
                        classStats[i, 6] = Fai;
                        classStats[i, 7] = Res;

                        classStartingLevel.RemoveAt(indexOfLevel);
                        classStatTotals.RemoveAt(indexOfStatTotal);
                    }


                    //set values
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        MeowDSIO.DataTypes.PARAM.ParamCellValueRef bowCheckCell = paramRow.Cells.First(c => c.Def.Name == "npcPlayerFaceGenId");
                        Type bowchecktype = bowCheckCell.GetType();
                        PropertyInfo bowcheckprop = bowchecktype.GetProperty("Value");
                        
                        //if is a starting class and randomizing starting classes
                        if (checkBoxStartingClasses.Checked && ((paramRow.ID >= 3000 && paramRow.ID <= 3009) || (paramRow.ID >= 2000 && paramRow.ID <= 2009)))
                        {
                            int classNumber; //0-9
                            if(paramRow.ID >= 3000)
                            {
                                classNumber = paramRow.ID - 3000;
                            }
                            else
                            {
                                classNumber = paramRow.ID - 2000;
                            }
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                PropertyInfo prop = cell.GetType().GetProperty("Value");
                                if (cell.Def.Name == "soulLv")
                                {
                                    prop.SetValue(cell, classLevels[classNumber], null);
                                }
                                else if (cell.Def.Name == "baseVit")
                                {
                                    prop.SetValue(cell, classStats[classNumber, 0], null);
                                }
                                else if (cell.Def.Name == "baseWil")
                                {
                                    prop.SetValue(cell, classStats[classNumber, 1], null);
                                }
                                else if (cell.Def.Name == "baseEnd")
                                {
                                    prop.SetValue(cell, classStats[classNumber, 2], null);
                                }
                                else if (cell.Def.Name == "baseStr")
                                {
                                    prop.SetValue(cell, classStats[classNumber, 3], null);
                                }
                                else if (cell.Def.Name == "baseDex")
                                {
                                    prop.SetValue(cell, classStats[classNumber, 4], null);
                                }
                                else if (cell.Def.Name == "baseMag")
                                {
                                    prop.SetValue(cell, classStats[classNumber, 5], null);
                                }
                                else if (cell.Def.Name == "baseFai")
                                {
                                    prop.SetValue(cell, classStats[classNumber, 6], null);
                                }
                                else if (cell.Def.Name == "baseDurability")
                                {
                                    prop.SetValue(cell, classStats[classNumber, 7], null);
                                }
                            }
                            
                        }
                        
                        //IDs 2400 to 2408 are gifts: None, Goddess's Blessing, Black Firebomb, Twin Humanities, Binoculars, Pendant, Master Key, Tiny Being's Ring, Old Witch's Ring

                        //Goddess's Blessing to Binoculars
                        if (paramRow.ID >= 2401 && paramRow.ID <= 2404)
                        {
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "item_01")
                                {
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if(checkBoxStartingGifts.Checked)
                                    {
                                        // 1/3 chance for titanite or boss souls to be chosen
                                        if(r.Next(3) == 0)
                                        {
                                            // 1/2 to be a soul item
                                            if(r.Next(2) == 0)
                                            {
                                                int randomIndex = r.Next(validSoulItems.Count);
                                                int itemId = validSoulItems[randomIndex];
                                                itemStartingGifts.Add(itemId);
                                                prop.SetValue(cell, itemId, null);
                                                validSoulItems.RemoveAt(randomIndex);
                                            }
                                            else // else be a titanite item
                                            {
                                                int randomIndex = r.Next(validTitaniteItems.Count);
                                                int itemId = validTitaniteItems[randomIndex];
                                                itemStartingGifts.Add(itemId);
                                                prop.SetValue(cell, itemId, null);
                                                validTitaniteItems.RemoveAt(randomIndex);
                                            }
                                        }
                                        else
                                        {
                                            int randomIndex = r.Next(validItems.Count);
                                            int itemId = validItems[randomIndex];
                                            itemStartingGifts.Add(itemId);
                                            prop.SetValue(cell, itemId, null);
                                            validItems.RemoveAt(randomIndex);
                                        }
                                    }
                                }
                                else if (cell.Def.Name == "itemNum_01")
                                {
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    int itemAmount = Convert.ToInt32(prop.GetValue(cell, null));
                                    if (checkBoxStartingGifts.Checked)
                                    {
                                        //if randomize starting gift amount
                                        if (checkBoxStartingGiftsAmount.Checked)
                                        {
                                            int newAmount = 1;
                                            // 2/5 chance to be greater than 1
                                            if(r.Next(5) <= 1)
                                            {
                                                newAmount = r.Next(9) + 2;
                                            }
                                            prop.SetValue(cell, newAmount, null);
                                            itemAmount = newAmount;
                                        }
                                        itemStartingGiftsAmount.Add(itemAmount);
                                    }
                                }
                            }
                        }
                        //Pendant (unstackable items only)
                        if (paramRow.ID == 2405)
                        {
                            bool isStackable = false;
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "item_01")
                                {
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxStartingGifts.Checked)
                                    {
                                        // 1/3 chance to randomize into a stackable item
                                        if(r.Next(3) == 0)
                                        {
                                            isStackable = true;
                                            // 1/3 chance for titanite or boss souls to be chosen
                                            if (r.Next(3) == 0)
                                            {
                                                // 1/2 to be a soul item
                                                if (r.Next(2) == 0)
                                                {
                                                    int randomIndex = r.Next(validSoulItems.Count);
                                                    int itemId = validSoulItems[randomIndex];
                                                    itemStartingGifts.Add(itemId);
                                                    prop.SetValue(cell, itemId, null);
                                                    validSoulItems.RemoveAt(randomIndex);
                                                }
                                                else // else be a titanite item
                                                {
                                                    int randomIndex = r.Next(validTitaniteItems.Count);
                                                    int itemId = validTitaniteItems[randomIndex];
                                                    itemStartingGifts.Add(itemId);
                                                    prop.SetValue(cell, itemId, null);
                                                    validTitaniteItems.RemoveAt(randomIndex);
                                                }
                                            }
                                            else
                                            {
                                                int randomIndex = r.Next(validItems.Count);
                                                int itemId = validItems[randomIndex];
                                                itemStartingGifts.Add(itemId);
                                                prop.SetValue(cell, itemId, null);
                                                validItems.RemoveAt(randomIndex);
                                            }
                                        }
                                        else
                                        {
                                            int randomIndex = r.Next(validUnstackableItems.Count);
                                            int itemId = validUnstackableItems[randomIndex];
                                            itemStartingGifts.Add(itemId);
                                            prop.SetValue(cell, itemId, null);
                                            validUnstackableItems.RemoveAt(randomIndex);
                                        }
                                    }
                                }
                            }
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "itemNum_01")
                                {
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    int itemAmount = Convert.ToInt32(prop.GetValue(cell, null));
                                    if (checkBoxStartingGifts.Checked)
                                    {
                                        //if randomize starting gift amount
                                        if (checkBoxStartingGiftsAmount.Checked)
                                        {
                                            int newAmount = 1;
                                            // 2/5 chance to be greater than 1; will only be greater than 1 if item is stackable
                                            if (r.Next(5) <= 1 && isStackable)
                                            {
                                                newAmount = r.Next(9) + 2;
                                            }
                                            prop.SetValue(cell, newAmount, null);
                                            itemAmount = newAmount;
                                        }
                                        itemStartingGiftsAmount.Add(itemAmount);
                                    }
                                }
                            }
                        }

                        //Tiny Being's Ring and Old Witch's Ring
                        if (paramRow.ID == 2407 || paramRow.ID == 2408)
                        {
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if (cell.Def.Name == "equip_Accessory01")
                                {
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");
                                    if (checkBoxStartingGifts.Checked)
                                    {
                                        int randomIndex = r.Next(validRings.Count);
                                        int ringId = validRings[randomIndex];
                                        ringStartingGifts.Add(ringId);
                                        prop.SetValue(cell, ringId, null);
                                        validRings.RemoveAt(randomIndex);
                                    }
                                }
                            }
                        }

                        if (Convert.ToInt32(bowcheckprop.GetValue(bowCheckCell, null)) > -1)
                        {
                            foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                            {
                                if(cell.Def.Name.StartsWith("bodyScale"))
                                {
                                    Type type = cell.GetType();
                                    PropertyInfo prop = type.GetProperty("Value");

                                    if (chkRandomFaceData.Checked)
                                    {
                                        //existing values are all multiples of 10 - not sure if necessary but round to nearest 10 for now
                                        int newValue = ((int)Math.Round(r.Next((int)cell.Def.Min, (int)cell.Def.Max) / 10.0)) * 10;
                                        prop.SetValue(cell, newValue, null);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (paramFile.ID == "FACE_PARAM_ST")
                {
                    foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                        {
                            Type type = cell.GetType();
                            PropertyInfo prop = type.GetProperty("Value");

                            if (chkRandomFaceData.Checked)
                            {
                                prop.SetValue(cell, r.Next((int)cell.Def.Min, (int)cell.Def.Max), null);
                            }
                        }
                    }
                }
            }

            //force spells to be useable for their class (the classes's casters too); done after class stats can be changed
            if (checkBoxForceUseableStartSpells.Checked)
            {
                foreach (var paramBndEntry in gameparamBnd)
                {
                    var paramShortName = paramBndEntry.Name;
                    var paramFile = paramBndEntry.Param;
                    if (paramFile.ID == "MAGIC_PARAM_ST")
                    {
                        foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                        {
                            //if is a starting spell
                            if (startingSpells.Contains(paramRow.ID))
                            {
                                int spellId = paramRow.ID;
                                int classIndex = 6; //sorcerer default in case something goes wrong
                                for (int i = 0; i < startingSpells.Length; i++) //get class index from starting spell list (assuming sorcerer is index 0)
                                {
                                    if (spellId == startingSpells[i])
                                    {
                                        classIndex = i + 6; //starting at index of sorcerer
                                        i = startingSpells.Length;
                                    }
                                }
                                foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                {
                                    PropertyInfo prop = cell.GetType().GetProperty("Value");
                                    if (cell.Def.Name == "requirementIntellect")
                                    {
                                        //if value is greater than it's classes's stats, lower required stats
                                        if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 5])
                                        {
                                            prop.SetValue(cell, classStats[classIndex, 5], null);
                                        }
                                    }
                                    else if (cell.Def.Name == "requirementFaith")
                                    {
                                        //if value is greater than it's classes's stats, lower required stats
                                        if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 6])
                                        {
                                            prop.SetValue(cell, classStats[classIndex, 6], null);
                                        }
                                    }
                                    else if (cell.Def.Name == "slotLength")
                                    {
                                        //if slot length is greater than 1, make it length 1
                                        if (Convert.ToInt32(prop.GetValue(cell, null)) > 1)
                                        {
                                            prop.SetValue(cell, 1, null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (paramFile.ID == "EQUIP_PARAM_WEAPON_ST" && !checkBoxForceUseableStartWeapons.Checked) //caster items are already handled if force useable starting weapons is enabled
                    {
                        for(int i = 1; i < secondaryStartingWeapons.Length; i++)
                        {
                            int casterId = secondaryStartingWeapons[i]; //casting item
                            int classIndex = i + 5;
                            foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                            {
                                if(paramRow.ID == casterId)
                                {
                                    foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        if (cell.Def.Name == "properMagic")
                                        {
                                            //if value is greater than it's classes's stats, lower required stats
                                            if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 5])
                                            {
                                                prop.SetValue(cell, classStats[classIndex, 5], null);
                                            }
                                        }
                                        else if (cell.Def.Name == "properFaith")
                                        {
                                            //if value is greater than it's classes's stats, lower required stats
                                            if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 6])
                                            {
                                                prop.SetValue(cell, classStats[classIndex, 6], null);
                                            }
                                        }
                                        else if (cell.Def.Name == "properStrength")
                                        {
                                            //if value is greater than it's classes's stats, lower required stats
                                            if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 3])
                                            {
                                                prop.SetValue(cell, classStats[classIndex, 3], null);
                                            }
                                        }
                                        else if (cell.Def.Name == "properAgility")
                                        {
                                            //if value is greater than it's classes's stats, lower required stats
                                            if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 4])
                                            {
                                                prop.SetValue(cell, classStats[classIndex, 4], null);
                                            }
                                        }
                                    }
                                }
                            }

                        }

                    }
                }
            }

            //forces weapons to be useable for their class
            if(checkBoxForceUseableStartWeapons.Checked)
            {
                foreach (var paramBndEntry in gameparamBnd)
                {
                    var paramShortName = paramBndEntry.Name;
                    var paramFile = paramBndEntry.Param;
                    if (paramFile.ID == "EQUIP_PARAM_WEAPON_ST")
                    {
                        //make all main and off hand weapons useable
                        for(int i = 0; i < 10; i++)
                        {
                            for(int j = 0; j < 2; j++)
                            {
                                int weaponId = startingWeapons[i, j];
                                foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                                {
                                    if(paramRow.ID == weaponId)
                                    {
                                        foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                        {
                                            PropertyInfo prop = cell.GetType().GetProperty("Value");
                                            if (cell.Def.Name == "properStrength")
                                            {
                                                //if value is greater than it's classes's stats, lower required stats
                                                if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[i, 3])
                                                {
                                                    prop.SetValue(cell, classStats[i, 3], null);
                                                }
                                            }
                                            else if (cell.Def.Name == "properAgility")
                                            {
                                                //if value is greater than it's classes's stats, lower required stats
                                                if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[i, 4])
                                                {
                                                    prop.SetValue(cell, classStats[i, 4], null);
                                                }
                                            }
                                            else if (cell.Def.Name == "properMagic")
                                            {
                                                //if value is greater than it's classes's stats, lower required stats
                                                if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[i, 5])
                                                {
                                                    prop.SetValue(cell, classStats[i, 5], null);
                                                }
                                            }
                                            else if (cell.Def.Name == "properFaith")
                                            {
                                                //if value is greater than it's classes's stats, lower required stats
                                                if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[i, 6])
                                                {
                                                    prop.SetValue(cell, classStats[i, 6], null);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        
                        //make the secondary main hand weapons useable
                        for(int i = 0; i < secondaryStartingWeapons.Length; i++)
                        {
                            int weaponId = secondaryStartingWeapons[i];
                            int classIndex = i + 5; //starting at hunter (i = 0)
                            foreach (MeowDSIO.DataTypes.PARAM.ParamRow paramRow in paramFile.Entries)
                            {
                                if (paramRow.ID == weaponId)
                                {
                                    foreach (MeowDSIO.DataTypes.PARAM.ParamCellValueRef cell in paramRow.Cells)
                                    {
                                        PropertyInfo prop = cell.GetType().GetProperty("Value");
                                        if (cell.Def.Name == "properStrength")
                                        {
                                            //if value is greater than it's classes's stats, lower required stats
                                            if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 3])
                                            {
                                                prop.SetValue(cell, classStats[classIndex, 3], null);
                                            }
                                        }
                                        else if (cell.Def.Name == "properAgility")
                                        {
                                            //if value is greater than it's classes's stats, lower required stats
                                            if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 4])
                                            {
                                                prop.SetValue(cell, classStats[classIndex, 4], null);
                                            }
                                        }
                                        else if (cell.Def.Name == "properMagic")
                                        {
                                            //if value is greater than it's classes's stats, lower required stats
                                            if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 5])
                                            {
                                                prop.SetValue(cell, classStats[classIndex, 5], null);
                                            }
                                        }
                                        else if (cell.Def.Name == "properFaith")
                                        {
                                            //if value is greater than it's classes's stats, lower required stats
                                            if (Convert.ToInt32(prop.GetValue(cell, null)) > classStats[classIndex, 6])
                                            {
                                                prop.SetValue(cell, classStats[classIndex, 6], null);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //set the text for the appropriate starting gifts
            if(checkBoxStartingGifts.Checked)
            {
                //bool lists needed to prevent infinite looping because value pairs have to be removed and then added because they cant be modified directly
                List<String> itemStartingNames = new List<String>() {"", "", "", "", ""};
                List<String> itemStartingDescr = new List<String>() {"", "", "", "", ""};
                List<bool> itemDoneStartingNames = new List<bool>() { false, false, false, false, false };
                List<bool> itemDoneStartingDescr = new List<bool>() { false, false, false, false, false };
                List<String> ringStartingNames = new List<String>() {"", ""};
                List<String> ringStartingDescr = new List<String>() {"", ""};
                List<bool> ringDoneStartingNames = new List<bool>() { false, false };
                List<bool> ringDoneStartingDescr = new List<bool>() { false, false };

                //get the list strings (not patch)
                foreach (var itemMsgBndEntry in itemMSGBND)
                {
                    var itemMsgKey = itemMsgBndEntry.Key;
                    var itemMsgFMG = itemMsgBndEntry.Value;
                    
                    if(itemMsgKey == MeowDSIO.DataTypes.FMGBND.FmgType.ItemNames)
                    {
                        foreach (var FMG in itemMsgFMG)
                        {
                            if(itemStartingGifts.Contains(FMG.Key))
                            {
                                int index = itemStartingGifts.IndexOf(FMG.Key);
                                itemStartingNames[index] = FMG.Value;
                            }
                        }
                    }
                    else if (itemMsgKey == MeowDSIO.DataTypes.FMGBND.FmgType.ItemLongDescriptions)
                    {
                        foreach (var FMG in itemMsgFMG)
                        {
                            if (itemStartingGifts.Contains(FMG.Key))
                            {
                                int index = itemStartingGifts.IndexOf(FMG.Key);
                                itemStartingDescr[index] = FMG.Value;
                            }
                        }
                    }
                    else if (itemMsgKey == MeowDSIO.DataTypes.FMGBND.FmgType.RingNames)
                    {
                        foreach (var FMG in itemMsgFMG)
                        {
                            if (ringStartingGifts.Contains(FMG.Key))
                            {
                                int index = ringStartingGifts.IndexOf(FMG.Key);
                                ringStartingNames[index] = FMG.Value;
                            }
                        }
                    }
                    else if (itemMsgKey == MeowDSIO.DataTypes.FMGBND.FmgType.RingLongDescriptions)
                    {
                        foreach (var FMG in itemMsgFMG)
                        {
                            if (ringStartingGifts.Contains(FMG.Key))
                            {
                                int index = ringStartingGifts.IndexOf(FMG.Key);
                                ringStartingDescr[index] = FMG.Value;
                            }
                        }
                    }
                }

                //get the list strings (patch)
                foreach (var menuMsgBndEntry in menuMSGBND)
                {
                    var menuMsgKey = menuMsgBndEntry.Key;
                    var menuMsgFMG = menuMsgBndEntry.Value;

                    if (menuMsgKey == MeowDSIO.DataTypes.FMGBND.FmgType.ItemNames_Patch)
                    {
                        foreach (var FMG in menuMsgFMG)
                        {
                            if (itemStartingGifts.Contains(FMG.Key))
                            {
                                int index = itemStartingGifts.IndexOf(FMG.Key);
                                if(!FMG.Value.Equals("<null>") && FMG.Value != null)
                                {
                                    itemStartingNames[index] = FMG.Value;
                                }
                            }
                        }
                    }
                    else if (menuMsgKey == MeowDSIO.DataTypes.FMGBND.FmgType.ItemLongDescriptions_Patch)
                    {
                        foreach (var FMG in menuMsgFMG)
                        {
                            if (itemStartingGifts.Contains(FMG.Key))
                            {
                                int index = itemStartingGifts.IndexOf(FMG.Key);
                                if (!FMG.Value.Equals("<null>") && FMG.Value != null)
                                {
                                    itemStartingDescr[index] = FMG.Value;
                                }
                            }
                        }
                    }
                    else if (menuMsgKey == MeowDSIO.DataTypes.FMGBND.FmgType.RingNames_Patch)
                    {
                        foreach (var FMG in menuMsgFMG)
                        {
                            if (ringStartingGifts.Contains(FMG.Key))
                            {
                                int index = ringStartingGifts.IndexOf(FMG.Key);
                                if (!FMG.Value.Equals("<null>") && FMG.Value != null)
                                {
                                    ringStartingNames[index] = FMG.Value;
                                }
                            }
                        }
                    }
                    else if (menuMsgKey == MeowDSIO.DataTypes.FMGBND.FmgType.RingLongDescriptions_Patch)
                    {
                        foreach (var FMG in menuMsgFMG)
                        {
                            if (ringStartingGifts.Contains(FMG.Key))
                            {
                                int index = ringStartingGifts.IndexOf(FMG.Key);
                                if (!FMG.Value.Equals("<null>") && FMG.Value != null)
                                {
                                    ringStartingDescr[index] = FMG.Value;
                                }
                            }
                        }
                    }
                }

                //set the starting gift text
                foreach (var menuMsgBndEntry in menuMSGBND)
                {
                    var menuMsgKey = menuMsgBndEntry.Key;
                    var menuMsgFMG = menuMsgBndEntry.Value;

                    if (menuMsgKey == MeowDSIO.DataTypes.FMGBND.FmgType.MenuOther)
                    {
                        for(int i = 0; i < menuMsgFMG.Count; i++)
                        {
                            var FMG = menuMsgFMG.ElementAt(i);

                            if (FMG.Key == 132051 && !itemDoneStartingNames[0]) //blessing name
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                String name = itemStartingNames[0];
                                if(itemStartingGiftsAmount[0] > 1)
                                {
                                    name += " [" + itemStartingGiftsAmount[0] + "]";
                                }
                                menuMsgFMG.Add(FMG.Key, name);
                                itemDoneStartingNames[0] = true;
                                i--;
                            }
                            else if (FMG.Key == 132052 && !itemDoneStartingNames[1]) //firebomb name
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                String name = itemStartingNames[1];
                                if (itemStartingGiftsAmount[1] > 1)
                                {
                                    name += " [" + itemStartingGiftsAmount[1] + "]";
                                }
                                menuMsgFMG.Add(FMG.Key, name);
                                itemDoneStartingNames[1] = true;
                                i--;
                            }
                            else if (FMG.Key == 132053 && !itemDoneStartingNames[2]) //twin humanity name
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                String name = itemStartingNames[2];
                                if (itemStartingGiftsAmount[2] > 1)
                                {
                                    name += " [" + itemStartingGiftsAmount[2] + "]";
                                }
                                menuMsgFMG.Add(FMG.Key, name);
                                itemDoneStartingNames[2] = true;
                                i--;
                            }
                            else if (FMG.Key == 132054 && !itemDoneStartingNames[3]) //binoculars name
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                String name = itemStartingNames[3];
                                if (itemStartingGiftsAmount[3] > 1)
                                {
                                    name += " [" + itemStartingGiftsAmount[3] + "]";
                                }
                                menuMsgFMG.Add(FMG.Key, name);
                                itemDoneStartingNames[3] = true;
                                i--;
                            }
                            else if (FMG.Key == 132055 && !itemDoneStartingNames[4]) //pendant name
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                String name = itemStartingNames[4];
                                if (itemStartingGiftsAmount[4] > 1)
                                {
                                    name += " [" + itemStartingGiftsAmount[4] + "]";
                                }
                                menuMsgFMG.Add(FMG.Key, name);
                                itemDoneStartingNames[4] = true;
                                i--;
                            }
                            else if (FMG.Key == 132351 && !itemDoneStartingDescr[0]) //blessing descr
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                menuMsgFMG.Add(FMG.Key, itemStartingDescr[0]);
                                itemDoneStartingDescr[0] = true;
                                i--;
                            }
                            else if (FMG.Key == 132352 && !itemDoneStartingDescr[1]) //firebomb descr
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                menuMsgFMG.Add(FMG.Key, itemStartingDescr[1]);
                                itemDoneStartingDescr[1] = true;
                                i--;
                            }
                            else if (FMG.Key == 132353 && !itemDoneStartingDescr[2]) //twin humanity descr
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                menuMsgFMG.Add(FMG.Key, itemStartingDescr[2]);
                                itemDoneStartingDescr[2] = true;
                                i--;
                            }
                            else if (FMG.Key == 132354 && !itemDoneStartingDescr[3]) //binoculars descr
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                menuMsgFMG.Add(FMG.Key, itemStartingDescr[3]);
                                itemDoneStartingDescr[3] = true;
                                i--;
                            }
                            else if (FMG.Key == 132355 && !itemDoneStartingDescr[4]) //pendant descr
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                menuMsgFMG.Add(FMG.Key, itemStartingDescr[4]);
                                itemDoneStartingDescr[4] = true;
                                i--;
                            }
                            else if (FMG.Key == 132057 && !ringDoneStartingNames[0]) //tiny being ring name
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                menuMsgFMG.Add(FMG.Key, ringStartingNames[0]);
                                ringDoneStartingNames[0] = true;
                                i--;
                            }
                            else if (FMG.Key == 132058 && !ringDoneStartingNames[1]) //old witch ring name
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                menuMsgFMG.Add(FMG.Key, ringStartingNames[1]);
                                ringDoneStartingNames[1] = true;
                                i--;
                            }
                            else if (FMG.Key == 132357 && !ringDoneStartingDescr[0]) //tiny being ring descr
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                menuMsgFMG.Add(FMG.Key, ringStartingDescr[0]);
                                ringDoneStartingDescr[0] = true;
                                i--;
                            }
                            else if (FMG.Key == 132358 && !ringDoneStartingDescr[1]) //old witch ring descr
                            {
                                menuMsgFMG.Remove(FMG.Key);
                                menuMsgFMG.Add(FMG.Key, ringStartingDescr[1]);
                                ringDoneStartingDescr[1] = true;
                                i--;
                            }
                        }
                    }
                }

            }

            //repack changed menu files if they were changed (for starting gift text to display properly)
            if (checkBoxStartingGifts.Checked)
            {
                DataFile.Resave(menuMSGBND);
            }

            //repack param files
            DataFile.Resave(gameparamBnd);

            lblMessage.Text += "Randomizing Complete!";
            lblMessage.ForeColor = Color.Black;
            lblMessage.Visible = true;
        }

        class UiThread
        {
            public static void WriteToInfoLabel(IProgress<string> progress)
            {
                //why is this necessary
                //without the loop it doesnt run async
                for (var i = 0; i < 5; i++)
                {
                    Task.Delay(10).Wait();
                    progress.Report("Randomizing...\n\n");
                }
            }
        }

        public static string[] InvalidVoiceIds
        {
            //this is a list of all empty voice lines
            //nice job from
            get
            {
                string[] invalidIds = { "10010601", "10010602", "10010603", "10010604", "10010605", "10010606", "10010607", "10010608", "10010609", "10010611", "10010612", "10010613", "10010614",
                    "10010615", "10010616", "10010617", "10010618", "10010619", "10010621", "10010622", "10010623", "10010624", "10010625", "10010626", "10010627", "10010628", "10010629", "10010631",
                    "10010632", "10010633", "10010634", "10010635", "10010636", "10010637", "10010638", "10010639", "11000501", "11000502", "11000503", "11000504", "11000505", "11000506", "11000507",
                    "11000508", "11000509", "11000511", "11000512", "11000513", "11000514", "11000515", "11000516", "11000517", "11000518", "11000519", "11000521", "11000522", "11000523", "11000524",
                    "11000525", "11000526", "11000527", "11000528", "11000529", "11000601", "11000602", "11000603", "11000604", "11000605", "11000606", "11000607", "11000608", "11000609", "11000701",
                    "11000702", "11000703", "11000704", "11000705", "11000706", "11000707", "11000708", "11000709", "11000711", "11000712", "11000713", "11000714", "11000715", "11000716", "11000717",
                    "11000718", "11000719", "11000721", "11000722", "11000723", "11000724", "11000725", "11000726", "11000727", "11000728", "11000729", "13010402", "14000024", "16000000", "16000001",
                    "16000002", "16000003", "16000004", "16000005", "16000006", "16000007", "16000008", "16000009", "16000010", "16000011", "16000012", "16000013", "16000014", "16000015", "16000016",
                    "16000017", "16000018", "16000019", "16000020", "16000021", "16000022", "16000023", "16000024", "16000025", "16000026", "16000027", "16000028", "16000029", "16000030", "16000031",
                    "16000032", "16000033", "16000034", "16000035", "16000036", "16000037", "16000038", "16000039", "16000040", "16000041", "16000042", "16000043", "16000044", "16000045", "16000046",
                    "16000047", "16000048", "16000049", "16000050", "16000051", "16000052", "16000053", "16000054", "16000055", "16000056", "16000057", "16000058", "16000059", "16000060", "16000061",
                    "16000062", "16000063", "16000064", "16000065", "16000066", "16000067", "16000068", "16000069", "16000070", "16000071", "16000072", "16000073", "16000074", "16000075", "16000076",
                    "16000077", "16000078", "16000079", "16000080", "16000081", "16000082", "16000083", "16000084", "16000085", "16000086", "16000087", "16000088", "16000089", "16000090", "16000091",
                    "16000092", "16000093", "16000094", "16000095", "16000096", "16000097", "16000098", "16000099", "16000401", "16000402", "16000403", "16000404", "16000405", "16000406", "16000407",
                    "16000408", "16000409", "16000411", "16000412", "16000413", "16000414", "16000415", "16000416", "16000417", "16000418", "16000419", "16000421", "16000422", "16000423", "16000424",
                    "16000425", "16000426", "16000427", "16000428", "16000429", "16000431", "16000432", "16000433", "16000434", "16000435", "16000436", "16000437", "16000438", "16000439", "16000441",
                    "16000442", "16000443", "16000444", "16000445", "16000446", "16000447", "16000448", "16000449", "17000231", "17000232", "17000233", "17000234", "17000235", "17000236", "17000237",
                    "17000238", "17000239", "17000241", "17000242", "17000243", "17000244", "17000245", "17000246", "17000247", "17000248", "17000249", "18000401", "18000402", "18000403", "18000404",
                    "18000405", "18000406", "18000407", "18000408", "18000409", "18000411", "18000412", "18000413", "18000414", "18000415", "18000416", "18000417", "18000418", "18000419", "18000421",
                    "18000422", "18000423", "18000424", "18000425", "18000426", "18000427", "18000428", "18000429", "18010701", "18010702", "18010703", "18010704", "18010705", "18010706", "18010707",
                    "18010708", "18010709", "18010711", "18010712", "18010713", "18010714", "18010715", "18010716", "18010717", "18010718", "18010719", "18010721", "18010722", "18010723", "18010724",
                    "18010725", "18010726", "18010727", "18010728", "18010729", "19000421", "19000422", "19000423", "19000424", "19000425", "19000426", "19000427", "19000428", "19000429", "26000301",
                    "26001501", "26001502", "26001503", "26001504", "26001505", "26001506", "26001507", "26001508", "26001509", "26001511", "26001512", "26001513", "26001514", "26001515", "26001516",
                    "26001517", "26001518", "26001519", "26001521", "26001522", "26001523", "26001524", "26001525", "26001526", "26001527", "26001528", "26001529", "27000341", "28001001", "29001454",
                    "29001455", "29001456", "29001457", "29001458", "29001459", "29001464", "29001465", "29001466", "29001467", "29001468", "29001469", "32000026", "32000027", "32000028", "32000029",
                    "33000601", "33000602", "33000603", "33000604", "33000605", "33000606", "33000607", "33000608", "33000609", "33000611", "33000612", "33000613", "33000614", "33000615", "33000616",
                    "33000617", "33000618", "33000619", "33000621", "33000622", "33000623", "33000624", "33000625", "33000626", "33000627", "33000628", "33000629", "34000601", "34000602", "34000603",
                    "34000604", "34000605", "34000606", "34000607", "34000608", "34000609", "34000611", "34000612", "34000613", "34000614", "34000615", "34000616", "34000617", "34000618", "34000619",
                    "34000621", "34000622", "34000623", "34000624", "34000625", "34000626", "34000627", "34000628", "34000629", "35000100", "35000200", "35000220", "36000601", "36000602", "36000603",
                    "36000604", "36000605", "36000606", "36000607", "36000608", "36000609", "36000611", "36000612", "36000613", "36000614", "36000615", "36000616", "36000617", "36000618", "36000619",
                    "36000621", "36000622", "36000623", "36000624", "36000625", "36000626", "36000627", "36000628", "36000629", "36000631", "36000632", "36000633", "36000634", "36000635", "36000636",
                    "36000637", "36000638", "36000639", "36000701", "36000702", "36000703", "36000704", "36000705", "36000706", "36000707", "36000708", "36000709", "36000711", "36000712", "36000713",
                    "36000714", "36000715", "36000716", "36000717", "36000718", "36000719", "36000801", "36000802", "36000803", "36000804", "36000805", "36000806", "36000807", "36000808", "36000809",
                    "37002701", "37002702", "37002703", "37002704", "37002705", "37002706", "37002707", "37002708", "37002709", "37002711", "37002712", "37002713", "37002714", "37002715", "37002716",
                    "37002717", "37002718", "37002719", "37002721", "37002722", "37002723", "37002724", "37002725", "37002726", "37002727", "37002728", "37002729", "38040105", "38060108", "38060200",
                    "38060300", "39000231", "39000232", "39000233", "39000234", "39000235", "39000236", "39000237", "39000238", "39000239", "40010101", "41000301", "41000302", "41000303", "41000304",
                    "41000305", "41000306", "41000307", "41000308", "41000309", "41000311", "41000312", "41000313", "41000314", "41000315", "41000316", "41000317", "41000318", "41000319", "41000321",
                    "41000322", "41000323", "41000324", "41000325", "41000326", "41000327", "41000328", "41000329", "42000841", "42000842", "42000843", "42000844", "42000845", "42000846", "42000847",
                    "42000848", "42000849", "42000851", "42000852", "42000853", "42000854", "42000855", "42000856", "42000857", "42000858", "42000859", "42001241", "42001242", "42001243", "42001244",
                    "42001245", "42001246", "42001247", "42001248", "42001249", "42001251", "42001252", "42001253", "42001254", "42001255", "42001256", "42001257", "42001258", "42001259", "43001004",
                    "43001209", "44000400", "44001205", "44001304", "44001402", "44001403", "44001404", "44001405", "47000901", "47000902", "47000903", "47000904", "47000905", "47000906", "47000907",
                    "47000908", "47000909", "47000911", "47000912", "47000913", "47000914", "47000915", "47000916", "47000917", "47000918", "47000919", "47000921", "47000922", "47000923", "47000924",
                    "47000925", "47000926", "47000927", "47000928", "47000929", "47000931", "47000932", "47000933", "47000934", "47000935", "47000936", "47000937", "47000938", "47000939", "52000000",
                    "52000100", "11000000", "14012502", "16000103", "28001908", "29000300", "29000301", "29000302", "29000303", "29000304", "29000305", "29000401", "29000602", "29002702", "36000502",
                    "37000703", "37000802", "37001301", "38000105", "39030101", "40010123", "42000205", "43001006", "43001202", "44001201", "44001202", "44001203", "44001307", "44002009", "56000001",
                    "56000002", "56000003", "56000004", "56000005", "56000006", "56000007", "56000008", "56000009", "56000011", "56000012", "56000013", "56000014", "56000015", "56000016", "56000017",
                    "56000018", "56000019", "56000101", "56000102", "56000103", "56000104", "56000105", "56000106", "56000107", "56000108", "56000109", "56000111", "56000112", "56000113", "56000114",
                    "56000115", "56000116", "56000117", "56000118", "56000119", "56000121", "56000122", "56000123", "56000124", "56000125", "56000126", "56000127", "56000128", "56000129", "56000131",
                    "56000132", "56000133", "56000134", "56000135", "56000136", "56000137", "56000138", "56000139", "56000141", "56000142", "56000143", "56000144", "56000145", "56000146", "56000147",
                    "56000148", "56000149", "56000151", "56000152", "56000153", "56000154", "56000155", "56000156", "56000157", "56000158", "56000159", "56000161", "56000162", "56000163", "56000164",
                    "56000165", "56000166", "56000167", "56000168", "56000169", "56000170", "56000171", "56000172", "56000173", "56000174", "56000175", "56000176", "56000177", "56000178", "56000179",
                    "56000181", "56000182", "56000183", "56000184", "56000185", "56000186", "56000187", "56000188", "56000189", "56000191", "56000192", "56000193", "56000194", "56000195", "56000196",
                    "56000197", "56000198", "56000199", "56000201", "56000202", "56000203", "56000204", "56000205", "56000206", "56000207", "56000208", "56000209", "56000211", "56000212", "56000213",
                    "56000214", "56000215", "56000216", "56000217", "56000218", "56000219", "56000221", "56000222", "56000223", "56000224", "56000225", "56000226", "56000227", "56000228", "56000229",
                    "56000301", "56000302", "56000303", "56000304", "56000305", "56000306", "56000307", "56000308", "56000309", "56000311", "56000312", "56000313", "56000314", "56000315", "56000316",
                    "56000317", "56000318", "56000319", "56000321", "56000322", "56000323", "56000324", "56000325", "56000326", "56000327", "56000328", "56000329", "56000331", "56000332", "56000333",
                    "56000334", "56000335", "56000336", "56000337", "56000338", "56000339", "56000341", "56000342", "56000343", "56000344", "56000345", "56000346", "56000347", "56000348", "56000349",
                    "56000351", "56000352", "56000353", "56000354", "56000355", "56000356", "56000357", "56000358", "56000359", "56000361", "56000362", "56000363", "56000364", "56000365", "56000366",
                    "56000367", "56000368", "56000369", "56005001", "56005002", "56005003", "56005004", "56005005", "56005006", "56005007", "56005008", "56005009", "57000001", "57000002", "57000003",
                    "57000004", "57000005", "57000006", "57000007", "57000008", "57000009", "57000011", "57000012", "57000013", "57000014", "57000015", "57000016", "57000017", "57000018", "57000019",
                    "57000021", "57000022", "57000023", "57000024", "57000025", "57000026", "57000027", "57000028", "57000029", "57000031", "57000032", "57000033", "57000034", "57000035", "57000036",
                    "57000037", "57000038", "57000039", "57000041", "57000042", "57000043", "57000044", "57000045", "57000046", "57000047", "57000048", "57000049", "57000101", "57000102", "57000103",
                    "57000104", "57000105", "57000106", "57000107", "57000108", "57000109", "57000111", "57000112", "57000113", "57000114", "57000115", "57000116", "57000117", "57000118", "57000119",
                    "57000121", "57000122", "57000123", "57000124", "57000125", "57000126", "57000127", "57000128", "57000129", "57000131", "57000132", "57000133", "57000134", "57000135", "57000136",
                    "57000137", "57000138", "57000139", "57000141", "57000142", "57000143", "57000144", "57000145", "57000146", "57000147", "57000148", "57000149", "57005001", "57005002", "57005003",
                    "57005004", "57005005", "57005006", "57005007", "57005008", "57005009", "57005011", "57005012", "57005013", "57005014", "57005015", "57005016", "57005017", "57005018", "57005019",
                    "57005101", "57005102", "57005103", "57005104", "57005105", "57005106", "57005107", "57005108", "57005109", "57005111", "57005112", "57005113", "57005114", "57005115", "57005116",
                    "57005117", "57005118", "57005119", "57005201", "57005202", "57005203", "57005204", "57005205", "57005206", "57005207", "57005208", "57005209", "57005211", "57005212", "57005213",
                    "57005214", "57005215", "57005216", "57005217", "57005218", "57005219", "58000001", "58000002", "58000003", "58000004", "58000005", "58000006", "58000007", "58000008", "58000009",
                    "58000011", "58000012", "58000013", "58000014", "58000015", "58000016", "58000017", "58000018", "58000019", "58000021", "58000022", "58000023", "58000024", "58000025", "58000026",
                    "58000027", "58000028", "58000029", "58000031", "58000032", "58000033", "58000034", "58000035", "58000036", "58000037", "58000038", "58000039", "58000041", "58000042", "58000043",
                    "58000044", "58000045", "58000046", "58000047", "58000048", "58000049", "58000401", "58000402", "58000403", "58000404", "58000405", "58000406", "58000407", "58000408", "58000409",
                    "58000411", "58000412", "58000413", "58000414", "58000415", "58000416", "58000417", "58000418", "58000419", "58000421", "58000422", "58000423", "58000424", "58000425", "58000426",
                    "58000427", "58000428", "58000429", "58000431", "58000432", "58000433", "58000434", "58000435", "58000436", "58000437", "58000438", "58000439", "58000501", "58000502", "58000503",
                    "58000504", "58000505", "58000506", "58000507", "58000508", "58000509", "58000511", "58000512", "58000513", "58000514", "58000515", "58000516", "58000517", "58000518", "58000519",
                    "58000521", "58000522", "58000523", "58000524", "58000525", "58000526", "58000527", "58000528", "58000529", "58000531", "58000532", "58000533", "58000534", "58000535", "58000536",
                    "58000537", "58000538", "58000539", "58000541", "58000542", "58000543", "58000544", "58000545", "58000546", "58000547", "58000548", "58000549", "58000551", "58000552", "58000553",
                    "58000554", "58000555", "58000556", "58000557", "58000558", "58000559", "58000561", "58000562", "58000563", "58000564", "58000565", "58000566", "58000567", "58000568", "58000569",
                    "58000571", "58000572", "58000573", "58000574", "58000575", "58000576", "58000577", "58000578", "58000579", "58000581", "58000582", "58000583", "58000584", "58000585", "58000586",
                    "58000587", "58000588", "58000589", "58000591", "58000592", "58000593", "58000594", "58000595", "58000596", "58000597", "58000598", "58000599", "58001201", "58001202", "58001203",
                    "58001204", "58001205", "58001206", "58001207", "58001208", "58001209", "58001301", "58001302", "58001303", "58001304", "58001305", "58001306", "58001307", "58001308", "58001309",
                    "58001311", "58001312", "58001313", "58001314", "58001315", "58001316", "58001317", "58001318", "58001319", "58001401", "58001402", "58001403", "58001404", "58001405", "58001406",
                    "58001407", "58001408", "58001409", "58001501", "58001502", "58001503", "58001504", "58001505", "58001506", "58001507", "58001508", "58001509", "58001601", "58001602", "58001603",
                    "58001604", "58001605", "58001606", "58001607", "58001608", "58001609", "58001701", "58001702", "58001703", "58001704", "58001705", "58001706", "58001707", "58001708", "58001709",
                    "58001801", "58001802", "58001803", "58001804", "58001805", "58001806", "58001807", "58001808", "58001809", "58001811", "58001812", "58001813", "58001814", "58001815", "58001816",
                    "58001817", "58001818", "58001819", "58001821", "58001822", "58001823", "58001824", "58001825", "58001826", "58001827", "58001828", "58001829", "58001901", "58001902", "58001903",
                    "58001904", "58001905", "58001906", "58001907", "58001908", "58001909", "58001911", "58001912", "58001913", "58001914", "58001915", "58001916", "58001917", "58001918", "58001919",
                    "58001921", "58001922", "58001923", "58001924", "58001925", "58001926", "58001927", "58001928", "58001929", "58002001", "58002002", "58002003", "58002004", "58002005", "58002006",
                    "58002007", "58002008", "58002009", "58002011", "58002012", "58002013", "58002014", "58002015", "58002016", "58002017", "58002018", "58002019", "58002021", "58002022", "58002023",
                    "58002024", "58002025", "58002026", "58002027", "58002028", "58002029", "58002031", "58002032", "58002033", "58002034", "58002035", "58002036", "58002037", "58002038", "58002039",
                    "58002101", "58002102", "58002103", "58002104", "58002105", "58002106", "58002107", "58002108", "58002109", "58002111", "58002112", "58002113", "58002114", "58002115", "58002116",
                    "58002117", "58002118", "58002119", "58002121", "58002122", "58002123", "58002124", "58002125", "58002126", "58002127", "58002128", "58002129", "58002131", "58002132", "58002133",
                    "58002134", "58002135", "58002136", "58002137", "58002138", "58002139", "58002201", "58002202", "58002203", "58002204", "58002205", "58002206", "58002207", "58002208", "58002209",
                    "58002211", "58002212", "58002213", "58002214", "58002215", "58002216", "58002217", "58002218", "58002219", "58002221", "58002222", "58002223", "58002224", "58002225", "58002226",
                    "58002227", "58002228", "58002229", "58002301", "58002302", "58002303", "58002304", "58002305", "58002306", "58002307", "58002308", "58002309", "58002311", "58002312", "58002313",
                    "58002314", "58002315", "58002316", "58002317", "58002318", "58002319", "58002321", "58002322", "58002323", "58002324", "58002325", "58002326", "58002327", "58002328", "58002329",
                    "58002331", "58002332", "58002333", "58002334", "58002335", "58002336", "58002337", "58002338", "58002339", "58002341", "58002342", "58002343", "58002344", "58002345", "58002346",
                    "58002347", "58002348", "58002349", "58002351", "58002352", "58002353", "58002354", "58002355", "58002356", "58002357", "58002358", "58002359", "58002361", "58002362", "58002363",
                    "58002364", "58002365", "58002366", "58002367", "58002368", "58002369", "58002401", "58002402", "58002403", "58002404", "58002405", "58002406", "58002407", "58002408", "58002409",
                    "58002411", "58002412", "58002413", "58002414", "58002415", "58002416", "58002417", "58002418", "58002419", "58002421", "58002422", "58002423", "58002424", "58002425", "58002426",
                    "58002427", "58002428", "58002429", "58002501", "58002502", "58002503", "58002504", "58002505", "58002506", "58002507", "58002508", "58002509", "58002601", "58002602", "58002603",
                    "58002604", "58002605", "58002606", "58002607", "58002608", "58002609", "58002611", "58002612", "58002613", "58002614", "58002615", "58002616", "58002617", "58002618", "58002619",
                    "58002621", "58002622", "58002623", "58002624", "58002625", "58002626", "58002627", "58002628", "58002629", "58002631", "58002632", "58002633", "58002634", "58002635", "58002636",
                    "58002637", "58002638", "58002639", "58002641", "58002642", "58002643", "58002644", "58002645", "58002646", "58002647", "58002648", "58002649", "58002701", "58002702", "58002703",
                    "58002704", "58002705", "58002706", "58002707", "58002708", "58002709", "58002711", "58002712", "58002713", "58002714", "58002715", "58002716", "58002717", "58002718", "58002719",
                    "58002721", "58002722", "58002723", "58002724", "58002725", "58002726", "58002727", "58002728", "58002729", "58002731", "58002732", "58002733", "58002734", "58002735", "58002736",
                    "58002737", "58002738", "58002739", "58002741", "58002742", "58002743", "58002744", "58002745", "58002746", "58002747", "58002748", "58002749", "58005001", "58005002", "58005003",
                    "58005004", "58005005", "58005006", "58005007", "58005008", "58005009", "58005011", "58005012", "58005013", "58005014", "58005015", "58005016", "58005017", "58005018", "58005019",
                    "58005021", "58005022", "58005023", "58005024", "58005025", "58005026", "58005027", "58005028", "58005029", "58005031", "58005032", "58005033", "58005034", "58005035", "58005036",
                    "58005037", "58005038", "58005039", "58005101", "58005102", "58005103", "58005104", "58005105", "58005106", "58005107", "58005108", "58005109", "58005111", "58005112", "58005113",
                    "58005114", "58005115", "58005116", "58005117", "58005118", "58005119", "58005201", "58005202", "58005203", "58005204", "58005205", "58005206", "58005207", "58005208", "58005209",
                    "58005211", "58005212", "58005213", "58005214", "58005215", "58005216", "58005217", "58005218", "58005219", "58005301", "58005302", "58005303", "58005304", "58005305", "58005306",
                    "58005307", "58005308", "58005309", "58005311", "58005312", "58005313", "58005314", "58005315", "58005316", "58005317", "58005318", "58005319", "58005401", "58005402", "58005403",
                    "58005404", "58005405", "58005406", "58005407", "58005408", "58005409", "58005411", "58005412", "58005413", "58005414", "58005415", "58005416", "58005417", "58005418", "58005419",
                    "59000001", "59000002", "59000003", "59000004", "59000005", "59000006", "59000007", "59000008", "59000009", "59000101", "59000102", "59000103", "59000104", "59000105", "59000106",
                    "59000107", "59000108", "59000109", "59000111", "59000112", "59000113", "59000114", "59000115", "59000116", "59000117", "59000118", "59000119", "59000201", "59000202", "59000203",
                    "59000204", "59000205", "59000206", "59000207", "59000208", "59000209", "59000211", "59000212", "59000213", "59000214", "59000215", "59000216", "59000217", "59000218", "59000219",
                    "59000301", "59000302", "59000303", "59000304", "59000305", "59000306", "59000307", "59000308", "59000309", "59000401", "59000402", "59000403", "59000404", "59000405", "59000406",
                    "59000407", "59000408", "59000409", "59000411", "59000412", "59000413", "59000414", "59000415", "59000416", "59000417", "59000418", "59000419", "59000501", "59000502", "59000503",
                    "59000504", "59000505", "59000506", "59000507", "59000508", "59000509", "59000511", "59000512", "59000513", "59000514", "59000515", "59000516", "59000517", "59000518", "59000519",
                    "59000701", "59000702", "59000703", "59000704", "59000705", "59000706", "59000707", "59000708", "59000709", "59000711", "59000712", "59000713", "59000714", "59000715", "59000716",
                    "59000717", "59000718", "59000719", "59000801", "59000802", "59000803", "59000804", "59000805", "59000806", "59000807", "59000808", "59000809", "59000811", "59000812", "59000813",
                    "59000814", "59000815", "59000816", "59000817", "59000818", "59000819", "59000821", "59000822", "59000823", "59000824", "59000825", "59000826", "59000827", "59000828", "59000829",
                    "59000831", "59000832", "59000833", "59000834", "59000835", "59000836", "59000837", "59000838", "59000839", "59000901", "59000902", "59000903", "59000904", "59000905", "59000906",
                    "59000907", "59000908", "59000909", "59000911", "59000912", "59000913", "59000914", "59000915", "59000916", "59000917", "59000918", "59000919", "59000921", "59000922", "59000923",
                    "59000924", "59000925", "59000926", "59000927", "59000928", "59000929", "59000931", "59000932", "59000933", "59000934", "59000935", "59000936", "59000937", "59000938", "59000939",
                    "59001001", "59001002", "59001003", "59001004", "59001005", "59001006", "59001007", "59001008", "59001009", "59001011", "59001012", "59001013", "59001014", "59001015", "59001016",
                    "59001017", "59001018", "59001019", "59001021", "59001022", "59001023", "59001024", "59001025", "59001026", "59001027", "59001028", "59001029", "59001101", "59001102", "59001103",
                    "59001104", "59001105", "59001106", "59001107", "59001108", "59001109", "59001111", "59001112", "59001113", "59001114", "59001115", "59001116", "59001117", "59001118", "59001119",
                    "59001201", "59001202", "59001203", "59001204", "59001205", "59001206", "59001207", "59001208", "59001209", "59001211", "59001212", "59001213", "59001214", "59001215", "59001216",
                    "59001217", "59001218", "59001219", "59001301", "59001302", "59001303", "59001304", "59001305", "59001306", "59001307", "59001308", "59001309", "59001311", "59001312", "59001313",
                    "59001314", "59001315", "59001316", "59001317", "59001318", "59001319", "59001401", "59001402", "59001403", "59001404", "59001405", "59001406", "59001407", "59001408", "59001409",
                    "59001501", "59001502", "59001503", "59001504", "59001505", "59001506", "59001507", "59001508", "59001509", "59001601", "59001602", "59001603", "59001604", "59001605", "59001606",
                    "59001607", "59001608", "59001609", "59001611", "59001612", "59001613", "59001614", "59001615", "59001616", "59001617", "59001618", "59001619", "59001621", "59001622", "59001623",
                    "59001624", "59001625", "59001626", "59001627", "59001628", "59001629", "59001631", "59001632", "59001633", "59001634", "59001635", "59001636", "59001637", "59001638", "59001639",
                    "59001641", "59001642", "59001643", "59001644", "59001645", "59001646", "59001647", "59001648", "59001649", "59001651", "59001652", "59001653", "59001654", "59001655", "59001656",
                    "59001657", "59001658", "59001659", "59005001", "59005002", "59005003", "59005004", "59005005", "59005006", "59005007", "59005008", "59005009", "59005101", "59005102", "59005103",
                    "59005104", "59005105", "59005106", "59005107", "59005108", "59005109", "59005111", "59005112", "59005113", "59005114", "59005115", "59005116", "59005117", "59005118", "59005119",
                    "59005201", "59005202", "59005203", "59005204", "59005205", "59005206", "59005207", "59005208", "59005209", "59005211", "59005212", "59005213", "59005214", "59005215", "59005216",
                    "59005217", "59005218", "59005219", "59005221", "59005222", "59005223", "59005224", "59005225", "59005226", "59005227", "59005228", "59005229", "59005301", "59005302", "59005303",
                    "59005304", "59005305", "59005306", "59005307", "59005308", "59005309", "59005401", "59005402", "59005403", "59005404", "59005405", "59005406", "59005407", "59005408", "59005409",
                    "59005411", "59005412", "59005413", "59005414", "59005415", "59005416", "59005417", "59005418", "59005419", "59005421", "59005422", "59005423", "59005424", "59005425", "59005426",
                    "59005427", "59005428", "59005429", "59005501", "59005502", "59005503", "59005504", "59005505", "59005506", "59005507", "59005508", "59005509", "59005601", "59005602", "59005603",
                    "59005604", "59005605", "59005606", "59005607", "59005608", "59005609", "59005611", "59005612", "59005613", "59005614", "59005615", "59005616", "59005617", "59005618", "59005619",
                    "60000001", "60000002", "60000003", "60000004", "60000005", "60000006", "60000007", "60000008", "60000009", "60000012", "60000013", "60000014", "60000015", "60000016", "60000017",
                    "60000018", "60000019", "60000101", "60000102", "60000103", "60000104", "60000105", "60000106", "60000107", "60000108", "60000109", "60000111", "60000112", "60000113", "60000114",
                    "60000115", "60000116", "60000117", "60000118", "60000119", "60000121", "60000122", "60000123", "60000124", "60000125", "60000126", "60000127", "60000128", "60000129", "60000131",
                    "60000132", "60000133", "60000134", "60000135", "60000136", "60000137", "60000138", "60000139", "60000141", "60000142", "60000143", "60000144", "60000145", "60000146", "60000147",
                    "60000148", "60000149", "60000201", "60000202", "60000203", "60000204", "60000205", "60000206", "60000207", "60000208", "60000209", "60000211", "60000212", "60000213", "60000214",
                    "60000215", "60000216", "60000217", "60000218", "60000219", "60000301", "60000302", "60000303", "60000304", "60000305", "60000306", "60000307", "60000308", "60000309", "60000310",
                    "60000311", "60000312", "60000313", "60000314", "60000315", "60000316", "60000317", "60000318", "60000319", "60000321", "60000322", "60000323", "60000324", "60000325", "60000326",
                    "60000327", "60000328", "60000329", "60000401", "60000402", "60000403", "60000404", "60000405", "60000406", "60000407", "60000408", "60000409", "60000601", "60000602", "60000603",
                    "60000604", "60000605", "60000606", "60000607", "60000608", "60000609", "60000611", "60000612", "60000613", "60000614", "60000615", "60000616", "60000617", "60000618", "60000619",
                    "60000621", "60000622", "60000623", "60000624", "60000625", "60000626", "60000627", "60000628", "60000629", "60000701", "60000702", "60000703", "60000704", "60000705", "60000706",
                    "60000707", "60000708", "60000709", "60000711", "60000712", "60000713", "60000714", "60000715", "60000716", "60000717", "60000718", "60000719", "60000721", "60000722", "60000723",
                    "60000724", "60000725", "60000726", "60000727", "60000728", "60000729", "60005001", "60005002", "60005003", "60005004", "60005005", "60005006", "60005007", "60005008", "60005009",
                    "60005101", "60005102", "60005103", "60005104", "60005105", "60005106", "60005107", "60005108", "60005109", "60005111", "60005112", "60005113", "60005114", "60005115", "60005116",
                    "60005117", "60005118", "60005119", "60005201", "60005202", "60005203", "60005204", "60005205", "60005206", "60005207", "60005208", "60005209", "60005301", "60005302", "60005303",
                    "60005304", "60005305", "60005306", "60005307", "60005308", "60005309", "61000001", "61000002", "61000003", "61000004", "61000005", "61000006", "61000007", "61000008", "61000009",
                    "61000011", "61000012", "61000013", "61000014", "61000015", "61000016", "61000017", "61000018", "61000019", "61000021", "61000022", "61000023", "61000024", "61000025", "61000026",
                    "61000027", "61000028", "61000029", "61000032", "61000033", "61000034", "61000035", "61000036", "61000037", "61000038", "61000039", "61000041", "61000042", "61000043", "61000044",
                    "61000045", "61000046", "61000047", "61000048", "61000049", "61000051", "61000052", "61000053", "61000054", "61000055", "61000056", "61000057", "61000058", "61000059", "61000061",
                    "61000062", "61000063", "61000064", "61000065", "61000066", "61000067", "61000068", "61000069", "61000101", "61000102", "61000103", "61000104", "61000105", "61000106", "61000107",
                    "61000108", "61000109", "61000111", "61000112", "61000113", "61000114", "61000115", "61000116", "61000117", "61000118", "61000119", "61000121", "61000122", "61000123", "61000124",
                    "61000125", "61000126", "61000127", "61000128", "61000129", "61000131", "61000132", "61000133", "61000134", "61000135", "61000136", "61000137", "61000138", "61000139", "61000201",
                    "61000202", "61000203", "61000204", "61000205", "61000206", "61000207", "61000208", "61000209", "61000211", "61000212", "61000213", "61000214", "61000215", "61000216", "61000217",
                    "61000218", "61000219", "61000221", "61000222", "61000223", "61000224", "61000225", "61000226", "61000227", "61000228", "61000229", "61000231", "61000232", "61000233", "61000234",
                    "61000235", "61000236", "61000237", "61000238", "61000239", "61000241", "61000242", "61000243", "61000244", "61000245", "61000246", "61000247", "61000248", "61000249", "61000301",
                    "61000302", "61000303", "61000304", "61000305", "61000306", "61000307", "61000308", "61000309", "61000401", "61000402", "61000403", "61000404", "61000405", "61000406", "61000407",
                    "61000408", "61000409", "61000501", "61000502", "61000503", "61000504", "61000505", "61000506", "61000507", "61000508", "61000509", "61000601", "61000602", "61000603", "61000604",
                    "61000605", "61000606", "61000607", "61000608", "61000609", "61000701", "61000702", "61000703", "61000704", "61000705", "61000706", "61000707", "61000708", "61000709", "61000711",
                    "61000712", "61000713", "61000714", "61000715", "61000716", "61000717", "61000718", "61000719", "61000721", "61000722", "61000723", "61000724", "61000725", "61000726", "61000727",
                    "61000728", "61000729", "61000731", "61000732", "61000733", "61000734", "61000735", "61000736", "61000737", "61000738", "61000739", "61000901", "61000902", "61000903", "61000904",
                    "61000905", "61000906", "61000907", "61000908", "61000909", "61000911", "61000912", "61000913", "61000914", "61000915", "61000916", "61000917", "61000918", "61000919", "61000921",
                    "61000922", "61000923", "61000924", "61000925", "61000926", "61000927", "61000928", "61000929", "61000931", "61000932", "61000933", "61000934", "61000935", "61000936", "61000937",
                    "61000938", "61000939", "61000941", "61000942", "61000943", "61000944", "61000945", "61000946", "61000947", "61000948", "61000949", "61001001", "61001002", "61001003", "61001004",
                    "61001005", "61001006", "61001007", "61001008", "61001009", "61001011", "61001012", "61001013", "61001014", "61001015", "61001016", "61001017", "61001018", "61001019", "61001021",
                    "61001022", "61001023", "61001024", "61001025", "61001026", "61001027", "61001028", "61001029", "61001101", "61001102", "61001103", "61001104", "61001105", "61001106", "61001107",
                    "61001108", "61001109", "61001111", "61001112", "61001113", "61001114", "61001115", "61001116", "61001117", "61001118", "61001119", "61001121", "61001122", "61001123", "61001124",
                    "61001125", "61001126", "61001127", "61001128", "61001129", "61001201", "61001202", "61001203", "61001204", "61001205", "61001206", "61001207", "61001208", "61001209", "61001211",
                    "61001212", "61001213", "61001214", "61001215", "61001216", "61001217", "61001218", "61001219", "61001221", "61001222", "61001223", "61001224", "61001225", "61001226", "61001227",
                    "61001228", "61001229", "61001231", "61001232", "61001233", "61001234", "61001235", "61001236", "61001237", "61001238", "61001239", "61001301", "61001302", "61001303", "61001304",
                    "61001305", "61001306", "61001307", "61001308", "61001309", "61001311", "61001312", "61001313", "61001314", "61001315", "61001316", "61001317", "61001318", "61001319", "61001321",
                    "61001322", "61001323", "61001324", "61001325", "61001326", "61001327", "61001328", "61001329", "61005001", "61005002", "61005003", "61005004", "61005005", "61005006", "61005007",
                    "61005008", "61005009", "61005011", "61005012", "61005013", "61005014", "61005015", "61005016", "61005017", "61005018", "61005019", "61005021", "61005022", "61005023", "61005024",
                    "61005025", "61005026", "61005027", "61005028", "61005029", "61005031", "61005032", "61005033", "61005034", "61005035", "61005036", "61005037", "61005038", "61005039", "61005041",
                    "61005042", "61005043", "61005044", "61005045", "61005046", "61005047", "61005048", "61005049", "61005101", "61005102", "61005103", "61005104", "61005105", "61005106", "61005107",
                    "61005108", "61005109", "61005111", "61005112", "61005113", "61005114", "61005115", "61005116", "61005117", "61005118", "61005119", "61005121", "61005122", "61005123", "61005124",
                    "61005125", "61005126", "61005127", "61005128", "61005129" };
                return invalidIds;
            }
            
        }

        private void checkBoxRemaster_CheckedChanged(object sender, EventArgs e)
        {
            //check that entered path is valid
            gameDirectory = txtGamePath.Text;

            //reset message label
            lblMessage.Text = "";
            lblMessage.ForeColor = new Color();
            lblMessage.Visible = true;

            if (!File.Exists(gameDirectory + (checkBoxRemaster.Checked ? "\\DarkSoulsRemastered.exe" : "\\DARKSOULS.exe")))
            {
                lblMessage.Text = $"Not a valid {(checkBoxRemaster.Checked ? "Dark Souls: Remastered" : "Dark Souls: Prepare to Die Edition")} Data directory.";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            else if (!File.Exists(gameDirectory + "\\param\\GameParam\\GameParam.parambnd" + (checkBoxRemaster.Checked ? ".dcx" : "")))
            {
                if (checkBoxRemaster.Checked)
                {
                    //invalid directory
                    lblMessage.Text = "Invalid Dark Souls: Remastered game directory; No GameParam found.";
                }
                else
                {
                    //user hasn't unpacked their game
                    lblMessage.Text = "You don't seem to have an unpacked Dark Souls: Prepare to Die Edition installation. Please run UDSFM and come back :)";
                }
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }

        private void tooltip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void toolTip2_Popup(object sender, PopupEventArgs e)
        {

        }

        private void chkKnockback_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnLoadPreset_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "presets (*.prndpr)|*.prndpr|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String presetName = dialog.FileName;
                Stream s = dialog.OpenFile();

                Console.WriteLine("Loading...");

                //header (aka check if is valid config)
                byte[] header = new byte[10];
                s.Read(header, 0, 10);
                if(header[0] != 30 || header[1] != 10 || header[2] != 40 || header[3] != 10 || header[4] != 50 || 
                    header[5] != 90 || header[6] != 20 || header[7] != 60 || header[8] != 50 || header[9] != 30 )
                {
                    lblMessage.Name = "";
                    lblMessage.Text = "Invalid Preset File!";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                    s.Close();
                    return;
                }

                //seed string
                byte[] stringBytes = new byte[128];
                s.Read(stringBytes, 0, 128);
                txtSeed.Text = System.Text.Encoding.ASCII.GetString(stringBytes);

                //byte reading now so that the checkbox setting can be done in any order (byte reading MUST be done in the order presets have saved)
                int w1 = s.ReadByte(); //weapon's 1st byte
                int w2 = s.ReadByte(); //weapon's 2nd byte
                int e1 = s.ReadByte(); //enemies byte
                int ep1 = s.ReadByte(); //enemy and player byte
                int a1 = s.ReadByte(); //armor byte
                int s1 = s.ReadByte(); //spells byte
                int o1 = s.ReadByte(); //other settings byte (options in the other settings tab)
                int altRand1 = s.ReadByte(); //alternative randomization's 1st byte (don't randomize by shuffle)
                int altRand2 = s.ReadByte(); //alternative randomization's 2nd byte (don't randomize by shuffle)


                //weapons 1st byte
                chkWeaponDamage.Checked = getState(w1, 0);
                chkWeaponMoveset.Checked = getState(w1, 1);
                chkWeaponModels.Checked = getState(w1, 2);
                checkBoxWeaponWeight.Checked = getState(w1, 3);
                checkBoxWeaponScaling.Checked = getState(w1, 4);
                checkBoxWeaponStamina.Checked = getState(w1, 5);
                checkBoxWeaponStatMin.Checked = getState(w1, 6);
                chkWeaponSpeffects.Checked = getState(w1, 7);

                //weapons 2nd byte
                checkBoxWeaponDefense.Checked = getState(w2, 0);
                checkBoxWeaponShieldSplit.Checked = getState(w2, 1);
                checkBoxWeaponFistNo.Checked = getState(w2, 2);
                checkBoxForceUseableStartWeapons.Checked = getState(w2, 3);

                //enemies byte
                chkAggroRadius.Checked = getState(e1, 0);
                chkTurnSpeeds.Checked = getState(e1, 1);
                chkSpeffects.Checked = getState(e1, 2);

                //enemy and player byte
                chkStaggerLevels.Checked = getState(ep1, 0);
                chkKnockback.Checked = getState(ep1, 1);
                chkBullets.Checked = getState(ep1, 2);
                chkHitboxSizes.Checked = getState(ep1, 3);
                checkBoxNerfHumanityBullets.Checked = getState(ep1, 4);

                //armor byte
                checkBoxArmorResistance.Checked = getState(a1, 0);
                checkBoxArmorWeight.Checked = getState(a1, 1);
                checkBoxArmorPoise.Checked = getState(a1, 2);
                checkBoxArmorspEffect.Checked = getState(a1, 3);

                //spells byte
                checkBoxUniversalizeCasters.Checked = getState(s1, 0);
                checkBoxRandomizeSpellRequirements.Checked = getState(s1, 1);
                checkBoxRandomizeSpellSlotSize.Checked = getState(s1, 2);
                checkBoxRandomizeSpellQuantity.Checked = getState(s1, 3);
                chkMagicAnimations.Checked = getState(s1, 4);
                checkBoxForceUseableStartSpells.Checked = getState(s1, 5);

                //other settings byte (options in the other settings tab)
                chkItemAnimations.Checked = getState(o1, 0);
                chkRandomFaceData.Checked = getState(o1, 1);
                chkRingSpeffects.Checked = getState(o1, 2);
                chkVoices.Checked = getState(o1, 3);
                checkBoxStartingGifts.Checked = getState(o1, 4);
                checkBoxPreventSpellGifts.Checked = getState(o1, 5);
                checkBoxStartingGiftsAmount.Checked = getState(o1, 6);
                checkBoxStartingClasses.Checked = getState(o1, 7);

                //alternative randomization's 1st byte (don't randomize by shuffle)
                checkBoxDoTrueRandom.Checked = getState(altRand1, 0);
                TRForm.chkTRWeaponDamage.Checked = getState(altRand1, 1);
                TRForm.chkTRWeaponWeight.Checked = getState(altRand1, 2);
                TRForm.chkTRWeaponScaling.Checked = getState(altRand1, 3);
                TRForm.chkTRWeaponStamina.Checked = getState(altRand1, 4);
                TRForm.chkTRWeaponStatMin.Checked = getState(altRand1, 5);
                TRForm.chkTRWeaponDefense.Checked = getState(altRand1, 6);
                TRForm.chkTRArmorResistance.Checked = getState(altRand1, 7);

                //alternative randomization's 2nd byte (don't randomize by shuffle)
                TRForm.chkTRArmorPoise.Checked = getState(altRand2, 0);
                TRForm.chkTRArmorWeight.Checked = getState(altRand2, 1);
                TRForm.chkTRSpellRequirements.Checked = getState(altRand2, 2);
                TRForm.chkTRSpellSlotSize.Checked = getState(altRand2, 3);
                TRForm.chkTRSpellQuantity.Checked = getState(altRand2, 4);



                lblMessage.Name = "";
                lblMessage.Text = "Preset Loaded!";
                lblMessage.ForeColor = Color.Black;
                lblMessage.Visible = true;
                s.Close();
            }
        }

        private void btnSavePreset_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "presets (*.prndpr)|*.prndpr|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String presetName = dialog.FileName;
                Stream s = dialog.OpenFile();

                Console.WriteLine("Saving...");


                //Write a 10 byte header to identify the config file; uses first 10 digits of pi multiplied by 10
                byte[] header = new byte[] { 30, 10, 40, 10, 50, 90, 20, 60, 50, 30 };
                s.Write(header, 0, 10);



                byte[] stringBytes = System.Text.Encoding.ASCII.GetBytes(txtSeed.Text);
                if(stringBytes.Length > 128) //has more than 128 characters
                {
                    lblMessage.Name = "";
                    lblMessage.Text = "Preset may not have seeds longer than 128 characters.";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                    s.Close();
                    return;
                }

                //seed string
                Array.Resize<byte>(ref stringBytes, 128);
                s.Write(stringBytes, 0, 128);



                //weapons 1st byte
                writeByte(s, chkWeaponDamage.Checked, chkWeaponMoveset.Checked, chkWeaponModels.Checked, checkBoxWeaponWeight.Checked, 
                   checkBoxWeaponScaling.Checked, checkBoxWeaponStamina.Checked, checkBoxWeaponStatMin.Checked, chkWeaponSpeffects.Checked);

                //weapons 2nd byte
                writeByte(s, checkBoxWeaponDefense.Checked, checkBoxWeaponShieldSplit.Checked, checkBoxWeaponFistNo.Checked, checkBoxForceUseableStartWeapons.Checked);

                //enemies byte
                writeByte(s, chkAggroRadius.Checked, chkTurnSpeeds.Checked, chkSpeffects.Checked);

                //enemy and player byte
                writeByte(s, chkStaggerLevels.Checked, chkKnockback.Checked, chkBullets.Checked, chkHitboxSizes.Checked,
                    checkBoxNerfHumanityBullets.Checked);

                //armor byte
                writeByte(s, checkBoxArmorResistance.Checked, checkBoxArmorWeight.Checked, checkBoxArmorPoise.Checked, checkBoxArmorspEffect.Checked);

                //spells byte
                writeByte(s, checkBoxUniversalizeCasters.Checked, checkBoxRandomizeSpellRequirements.Checked, checkBoxRandomizeSpellSlotSize.Checked, checkBoxRandomizeSpellQuantity.Checked,
                    chkMagicAnimations.Checked, checkBoxForceUseableStartSpells.Checked);

                //other settings byte (options in the other settings tab)
                writeByte(s, chkItemAnimations.Checked, chkRandomFaceData.Checked, chkRingSpeffects.Checked, chkVoices.Checked,
                    checkBoxStartingGifts.Checked, checkBoxPreventSpellGifts.Checked, checkBoxStartingGiftsAmount.Checked, checkBoxStartingClasses.Checked);

                //alternative randomization's 1st byte (don't randomize by shuffle)
                writeByte(s, checkBoxDoTrueRandom.Checked, TRForm.chkTRWeaponDamage.Checked, TRForm.chkTRWeaponWeight.Checked, TRForm.chkTRWeaponScaling.Checked,
                    TRForm.chkTRWeaponStamina.Checked, TRForm.chkTRWeaponStatMin.Checked, TRForm.chkTRWeaponDefense.Checked, TRForm.chkTRArmorResistance.Checked);

                //alternative randomization's 2nd byte (don't randomize by shuffle)
                writeByte(s, TRForm.chkTRArmorPoise.Checked, TRForm.chkTRArmorWeight.Checked, TRForm.chkTRSpellRequirements.Checked, TRForm.chkTRSpellSlotSize.Checked,
                    TRForm.chkTRSpellQuantity.Checked);



                lblMessage.Name = "";
                lblMessage.Text = "Preset Saved!";
                lblMessage.ForeColor = Color.Black;
                lblMessage.Visible = true;
                s.Close();
            }
        }

        private void writeByte(Stream s, params bool[] bits)
        {
            if(bits.Length > 8)
            {
                throw new Exception("can't assign more than 8 bits per byte");
            }
            byte val = 0;
            for(int i = 0; i < bits.Length; i++)
            {
                if(bits[i])
                {
                    val += (byte)(Math.Pow(2, i));
                }
            }
            s.WriteByte(val);
        }

        private bool getState(int b, int index)
        {
            //if at end of stream (old preset) defaults newer features that were not included in that preset as unchecked/disabled
            if(b == -1)
            {
                return false;
            }

            bool state;
            if(index >= 8)
            {
                throw new Exception("can't read more than 8 bits from a byte");
            }
            else if(index < 0)
            {
                throw new Exception("can't read from a negative index");
            }
            uint x = (uint)b;

            x %= (uint)Math.Pow(2, index + 1);
            x /= (uint)Math.Pow(2, index);
            
            state = x == 1;
            return state;
        }

        private void btnDoTrueRandomPopup_Click(object sender, EventArgs e)
        {
            TRForm.ShowDialog();
        }
    }
    
}
