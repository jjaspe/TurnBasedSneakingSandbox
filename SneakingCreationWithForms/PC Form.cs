using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CharacterSystemLibrary.Classes;
using OpenGlGameCommon.Classes;
using SneakingCommon.System_Classes;
using OpenGlGameCommon.Interfaces;
using SneakingCreationWithForms.MVP;

namespace SneakingCreationWithForms
{

    public partial class CreatePCForm :Form
    {
        String filepath = (System.Reflection.Assembly.GetExecutingAssembly().Location).
            Replace("SneakingCreationWithForms\\bin\\Debug\\SneakingCreationWithForms.exe", "TBSneaking Data\\"),
            statToSkillsFilename = "Stat To Skill Factors.txt",
            guardsFilename = "Guards\\saveGuardsTest.txt";

        Presenter MyPresenter;
        Character MyPC
        {
            get { return MyPresenter.Model.PC.MyCharacter; }
        }
        GameSystem MyGS
        {
            get { return MyPresenter.Model.System; }
        }
        public CreatePCForm()
        {
            InitializeComponent();
           
        }

        public CreatePCForm(Presenter presenter)
        {
            InitializeComponent();
            MyPresenter = presenter;
            MyPresenter.loadSystem(filepath + statToSkillsFilename);
        }

        public void saveStats()
        {
            MyPC.Name = nameText.Text;
            MyPC.setStat("Strength", Int32.Parse(strText.Text));
            MyPC.setStat("Perception", Int32.Parse(perText.Text));
            MyPC.setStat("Intelligence", Int32.Parse(intText.Text));
            MyPC.setStat("Dexterity", Int32.Parse(dexText.Text));
            MyPC.setStat("Armor", Int32.Parse(armorText.Text));
            MyPC.setStat("Weapon Skill", Int32.Parse(weapText.Text));
            MyPC.setStat("Field of View", Int32.Parse(FoVText.Text));
            MyPC.setStat("Field of Hearing", Int32.Parse(FOHText.Text));
            MyPC.setStat("AP", Int32.Parse(APText.Text));
            MyPC.setStat("Suspicion Propensity", Int32.Parse(SPText.Text));
            MyPC.setStat("Knows Map", knowsMap.Checked ? 1 : 0);

            MyPresenter.editPC(MyPC);
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveStats();
            SaveFileDialog pcDialog = new SaveFileDialog();
            pcDialog.Filter = "pc Files (*.pc)|*.pc";
            pcDialog.DefaultExt = ".pc";
            pcDialog.InitialDirectory = "C:/TBSneaking/PCs/";

            string filename = pcDialog.ShowDialog() == DialogResult.OK ? pcDialog.FileName : null;
            if (filename == null)
            {
                MessageBox.Show("Couldn't Save PC");
                return;
            }

            MyPresenter.savePC(filename);

        }


        private void intText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SPText.Text = MyGS.getSP(double.Parse(intText.Text)).ToString(); 
            }
            catch (Exception)
            {
                
                MessageBox.Show("Integers only");
            }
        }

        private void perText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.FoVText.Text = MyGS.getFoV(double.Parse(perText.Text)).ToString();
                this.FOHText.Text = MyGS.getFoH(double.Parse(perText.Text)).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Integers only");
            }
        }

        private void dexText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                APText.Text = MyGS.getAP(double.Parse(dexText.Text)).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Integers only");
            }
            
        }
    }
}
