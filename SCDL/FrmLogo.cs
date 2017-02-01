using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCDL
{
    public partial class FrmLogo : Form
    {
        public FrmLogo()
        {
            InitializeComponent();


          


        }

        private void TmrLogo_Tick(object sender, EventArgs e)
        {




            if ((prgsLogo.Value + 2) < 100)
            {

                prgsLogo.Value = prgsLogo.Value + 2;
            }
            else
            {
                
                
                prgsLogo.Value = 100;
                TmrLogo.Enabled = false;

                this.Hide();

                FrmMain FrmMain = new FrmMain();
                FrmMain.ShowDialog();




            }






        }

        private void FrmLogo_Load(object sender, EventArgs e)
        {
            TmrLogo.Enabled = true;


        }

        private void FrmLogo_Click(object sender, EventArgs e)
        {







            //this.Hide();
            //TmrLogo.Enabled = false;
            //FrmMain FrmMain = new FrmMain();
            //FrmMain.ShowDialog();

           




        }
    }
}
