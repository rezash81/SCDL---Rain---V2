using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Globalization;
using System.Reflection;
using Npgsql;

using System.Data.SqlClient;




using Excel = Microsoft.Office.Interop.Excel;

//using Excel = Microsoft.Win32



//using Excel = Microsoft.Office.Interop.Excel;


using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;



using System.Collections;


namespace SCDL
{
    public partial class FrmMain : Form
    {



        public string FTPAddress = "";


        public string FTPAddressdownloaddata = "";



        public string ftpusername = "";


        public string[] sensors_index_name = new string[100];


        public int[] client_info_id = new int[100];
        public int[] sensors_index = new int[100];

      //  public float[] sensors_index_max_min = new float[,];

        float[] sensors_index_max = new float[60];
        float[] sensors_index_min = new float[60];
        string str_max_min_sensors = "";


        float graph_1_min = 0;
        float graph_1_max = 0;


        float graph_2_min = 0;
        float graph_2_max = 0;


        Boolean has_two_graph = false;



        int cnt_random_data = 1;


        public string[] sensors_values_excell = new string[100];

        // public float[] battery_voltag = new float[60];
        public float[] battery_voltag;
        public int[] battery_values;



        public int glb_id_server_code;
        public string glb_server_spec = "";

        public Boolean show_time_log = false;

        public string ftppassword = "";

        public Boolean glbguid = false;

        public Boolean glb_stop_rdl = false;
        public int glb_client_id = 0;



        private byte[] downloadedData;

        private byte[] downloadedDatadata;


        public static string[] channelname;

        public string[] savingtype;


        public Boolean glb_autorite_stopr_rdl;
        public Boolean glb_autorite_change_date;

        public string glb_BATA = "", glb_RAN_TOT = "", glb_RAN_24 = "", glb_RAN_12 = "", glb_RAN_5M = "";
        public string glb_BATA_instant = "", glb_RAN_TOT_instant = "";
        public string glb_BATA_show_gauge = "";


        public string glbguidID;
        public string glb_num_mobile = "";

        public string glbguidID_zap;



        string ftpFilename = "";


        //  public OleDbConnection myconn;
        public OleDbDataAdapter da;
        public OleDbCommand cmd;
        public DataSet ds;

        public OleDbConnection myconn;

        public string tmp0, tmpordinalnumber, tmp2, tmp3, tmp4, tmp5;
        public int tmpA, tmpB, tmpC;
        public Boolean get_log_sms = false;


        // Hashtables to hold the various client, distant server and room objects
        private static Hashtable tmpClients = new Hashtable();
        private static Hashtable clClients = new Hashtable();
        private static Hashtable rmRooms = new Hashtable();

        // My local address settings
        private static IPAddress localHost;
        // The port on which clients connect
        private static int myPort;

        // Threads and listeners for handling client connections
        private static Thread myThread;
        private static TcpListener myListener;








        public FrmMain()
        {
            InitializeComponent();



            // OleDbConnection myconn;

            //            myconn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\picture-co\\Qar2\\Scdl.accdb");

            myconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\screen\\scdl.jpg");

            channelname = new string[50];


        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();




        }



        private void FrmMain_Load(object sender, EventArgs e)
        {
            ////myconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\screen\\scdl.jpg");

            //   Wm.URL = Application.StartupPath + "\\screen\\1.mp3"; 

            //   glbguid = true;


            DateTime theDate = DateTime.UtcNow;



            string customdate = theDate.ToString("d");
            //  string custom = theDate.ToString("hh:mm:ss tt");

            ////DataUtils dt1 = new DataUtils();
            ////dt1.connect_db();

            load_battery_calib();
            
            set_array_min_max();



           



            ////battery_voltag[0] = 10;
            ////battery_voltag[1] = 10.5f;
            ////battery_voltag[2] = 10.9f;
            ////battery_voltag[3] = 11.8f;
            ////battery_voltag[4] = 12.2f;
            ////battery_voltag[5] = 12.6f;
            ////battery_voltag[6] = 13.5f;




            ////battery_values[0] = 1;
            ////battery_values[1] = 5;
            ////battery_values[2] = 15;
            ////battery_values[3] = 50;
            ////battery_values[4] = 80;
            ////battery_values[5] = 90;
            ////battery_values[6] = 100;


            // public int[] battery_values = new int[7];





            label131_Click(null, null);

            pnlshowgauge.Left = 0;
            pnlshowgauge.Top = 34;



            glb_autorite_stopr_rdl = false;
            glb_autorite_change_date = false;


            lbltime2.Text = DateTime.Now.ToLongTimeString().ToString();

            lbldate2.Text = customdate;



            picconnected.Left = picdisconnected.Left;
            picconnected.Top = picdisconnected.Top;

            //////picshowlogsms.Left=picshowdatetime.Left;
            //////picshowlogsms.Left = picshowdatetime.Left;


            pnlhashcode.Left = 251;
            pnlhashcode.Top = 206;



            pnlloaddata.Left = 158;
            pnlloaddata.Top = 159;


            pnlshowdata.Left = 158;
            pnlshowdata.Top = 130;

            //   pnlshowdata.Top = 159;




            pnlSensors.Left = 219;
            pnlSensors.Top = 232;



            lblshow_guage_panel.Left = 280;
            lblshow_guage_panel.Top = 171;
            lblshow_guage_panel.Width = 457;
            lblshow_guage_panel.Height = 463;



            pnlInputs.Left = 64;
            pnlInputs.Top = 200;


            pnlCalibration.Left = 229;
            pnlCalibration.Top = 284;


            pnldatatransfer.Left = 97;
            pnldatatransfer.Top = 250;




            pnlweb.Width = 1024;
            pnlweb.Height = 611;

            pnlweb.Left = 1;
            pnlweb.Top = 137;



            webshow.Left = 0;
            webshow.Top = 0;

            webshow.Width = 1024;
            // webshow.Height = pnlweb.Height-50;

            webshow.Height = 600;


            ///////////////


            pnlhelp.Width = 1024;
            pnlhelp.Height = 611;

            pnlhelp.Left = 1;
            pnlhelp.Top = 137;



            webhelp.Left = 0;
            webhelp.Top = 0;

            webhelp.Width = 1024;
            // webshow.Height = pnlweb.Height-50;

            webhelp.Height = 600;

            rthelp.Left = 0;
            rthelp.Top = 0;

            rthelp.Width = 1024;
            // webshow.Height = pnlweb.Height-50;

            rthelp.Height = 600;





            //datagridloadtempcalibrationdata.Width = datagridloadtempdata.Width;

            //datagridloadtempcalibrationdata.Height = datagridloadtempdata.Height;

            //datagridloadtempcalibrationdata.Top = datagridloadtempdata.Top;

            //datagridloadtempcalibrationdata.Left = datagridloadtempdata.Left;








        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void button2_Click(object sender, EventArgs e)
        {






        }

        private void button3_Click(object sender, EventArgs e)
        {





        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void lblsensors_Click(object sender, EventArgs e)
        {



            if (glbguid == false)
            {

                MessageBox.Show("شما مجاز به استفاده از نرم افزار نیستید");
                return;

            }



            if (datagridloadtempdata.Rows.Count == 0)
            {

                MessageBox.Show("ابتدا وارد  بخش بارگذاری اطلاعات شوید  ");
                return;

            }







            pnlloaddata.Visible = true;

            tabControl1.SelectedTab = tabPage1;


            return;




            /////////////////////////////////////////////
            /////////////////////////////////////////////






            // this.contactsTableAdapter.Fill(this.newManageContactsDataSet.Contacts);


            //  OleDbCommand cmd = new OleDbCommand();

            //cmd.CommandType = CommandType.Text;
            //// string query = "insert into Contacts (fname,lname,llnum,mobnum,e-mail,street,city,country) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
            //cmd.CommandText = @"insert into Contacts (fname,lname,llnum,mobnum,e-mail,street,city,country) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
            //cmd.Connection = myconn;
            //myconn.Open();
            //cmd.ExecuteNonQuery();
            //System.Windows.Forms.MessageBox.Show("User Account Succefully Created", "Caption", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //myconn.Close();




            pnlCalibration.Visible = false;


            myconn.Close();


            myconn.Open();





            OleDbDataAdapter da = new OleDbDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            da.SelectCommand = new OleDbCommand("select * from tblSensors order by sensor_Code", myconn);
            da.Fill(ds, "tblSensors");
            dt = ds.Tables["tblSensors"];




            //foreach (DataRow dr in dt.Rows)
            //{
            //    MessageBox.Show(dr["channel name"].ToString());
            //}





            /////for datagrid view

            DataView dtaview = new DataView();
            dtaview.Table = ds.Tables["tblSensors"];


            dataGridView.DataSource = ds; // dataset
            dataGridView.DataMember = "tblSensors";


            myconn.Close();










            dataGridView.Columns[0].HeaderText = "شماره ی سنسور";

            dataGridView.Columns[1].HeaderText = "نام  اختصاری";

            dataGridView.Columns[2].HeaderText = "نام  سنسور";

            dataGridView.Columns[3].Width = 0;


            dataGridView.Columns[4].HeaderText = "نوع خروجی سنسور";



            dataGridView.Columns[5].HeaderText = "واحد اندازه گیری";
            dataGridView.Columns[6].Width = 0;
            dataGridView.Columns[7].HeaderText = "نوع سنسور";


            ////////dataGridView.Columns[7].HeaderText = "نوع ذخیره سازی";//savingType

            ////////dataGridView.Columns[8].HeaderText = "نوع کانال";//channelType

            ////////dataGridView.Columns[9].HeaderText = "نوع پورت";//hwPortType

            ////////dataGridView.Columns[10].HeaderText = "شماره پورت";//hwPortNumber

            ////////dataGridView.Columns[11].HeaderText = "شماره پین";//hwPortPinNumber

            ////////dataGridView.Columns[12].HeaderText = "نوع محاسبه";//calculationType

            ////////dataGridView.Columns[13].HeaderText = "فعال/غیرفعال";//IsActive










            pnlSensors.Visible = true;

        }

        private void lblsensorscalibration_Click(object sender, EventArgs e)
        {


            if (glbguid == false)
            {

                MessageBox.Show("شما مجاز به استفاده از نرم افزار نیستید");
                return;

            }



            if (datagridloadtempdata.Rows.Count == 0)
            {

                MessageBox.Show("ابتدا وارد  بخش بارگذاری اطلاعات شوید  ");
                return;

            }


            pnlloaddata.Visible = true;

            tabControl1.SelectedTab = tabPage2;

            return;




            /////////////////////////////////////////////
            /////////////////////////////////////////////






            pnlSensors.Visible = false;


            myconn.Open();





            OleDbDataAdapter da = new OleDbDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            da.SelectCommand = new OleDbCommand("select distinct(sensor_name_persian) from tblSensors ", myconn);
            da.Fill(ds, "tblSensors");
            dt = ds.Tables["tblSensors"];


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cmbshowsensorscalibration.Items.Add(ds.Tables[0].Rows[i][0].ToString());

            }




            pnlCalibration.Visible = true;











        }

        private void cmbshowsensorscalibration_SelectedIndexChanged(object sender, EventArgs e)
        {







            OleDbDataAdapter da = new OleDbDataAdapter();


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            da.SelectCommand = new OleDbCommand("select * from tblCalibration where sensor_name_persian='" + cmbshowsensorscalibration.Text + "' order by num ", myconn);
            da.Fill(ds, "tblCalibration");
            dt = ds.Tables["tblCalibration"];


            /////for datagrid view

            DataView dtaview = new DataView();
            dtaview.Table = ds.Tables["tblCalibration"];


            dtgcalibrations.DataSource = ds; // dataset
            dtgcalibrations.DataMember = "tblCalibration";




            OleDbDataAdapter da1 = new OleDbDataAdapter();


            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();


            da1.SelectCommand = new OleDbCommand("select * from tblSensors where sensor_name_persian='" + cmbshowsensorscalibration.Text + "' order by sensor_Code ", myconn);
            da1.Fill(ds1, "tblSensors");
            dt1 = ds1.Tables["tblSensors"];




            string sensorUnitAbv, sensorType_name;

            sensorUnitAbv = ds1.Tables[0].Rows[0][5].ToString();
            sensorType_name = ds1.Tables[0].Rows[0][4].ToString();








            myconn.Close();










            dtgcalibrations.Columns[0].HeaderText = "شماره ی سنسور";//channel_



            dtgcalibrations.Columns[1].HeaderText = "نام سنسور";//channel index

            dtgcalibrations.Columns[2].HeaderText = "شماره کانال";//channel index

            dtgcalibrations.Columns[3].HeaderText = "ردیف";//channel name

            //     dtgcalibrations.Columns[4].HeaderText = vahed+"مقدار فیزیکی";//sensorCode
            dtgcalibrations.Columns[4].HeaderText = "مقدار فیزیکی" + "(" + sensorType_name + ")";//sensorCode


            dtgcalibrations.Columns[5].HeaderText = "مقدار پارامتر" + "(" + sensorUnitAbv + ")";//sensorCode

            dtgcalibrations.Columns[6].HeaderText = "نام اختصاری";//sensorCode



            dtgcalibrations.Columns[0].Width = 0;





        }

        private void lblshowweb_Click(object sender, EventArgs e)
        {



            pnlCalibration.Visible = false;
            pnlSensors.Visible = false;
            pnlhelp.Visible = false;
            pnlshowdata.Visible = false;
            pnlloaddata.Visible = false;

            pnlhashcode.Visible = false;
            pnldatatransfer.Visible = false;
            pnlshowdata.Visible = false;
            pnlloaddata.Visible = false;

            pnlSensors.Visible = false;
            pnlCalibration.Visible = false;
            pnlweb.Visible = false;
            pnl_show_digital_rain.Visible = false;


            //     webshow.Navigate("www.google.com");

          //  webshow.Navigate("uas.co.ir");

            //      webshow.Navigate("c:\\game\\");


            pnlshowgauge.Visible = true;
            pnlshowgauge.BringToFront();

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            //lblrain_total.Text = glb_RAN_TOT;



            if (picconnected.Visible == true)
            {




                rdinstant.Checked = true;
                chkautomated.Checked = true;
                tmrfetchdata.Enabled = true;


            }








        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pnlweb.Visible = false;

        }

        private void exitsensors_Click(object sender, EventArgs e)
        {
            pnlSensors.Visible = false;

        }

        private void exitcalibrations_Click(object sender, EventArgs e)
        {
            pnlCalibration.Visible = false;

        }

        private void pnldatatransfer_Paint(object sender, PaintEventArgs e)
        {





        }



        private void exittransfer_Click_1(object sender, EventArgs e)
        {
            pnldatatransfer.Visible = false;
        }

        private void lblshowtransfer_Click(object sender, EventArgs e)
        {

            string k = "";
            //   Double  value = 123.672;


            ////  string value = "0.67843";

            ////  float tmp = float.Parse(value);

            //////  value = internlArraye[2];
            ////  k=string.Format("{0:0.00)}", tmp);



            //////////////////////set time 

            DateTime theDate = DateTime.UtcNow;


            string customdate = theDate.ToString("d");

            lbltime2.Text = DateTime.Now.ToLongTimeString().ToString();

            lbldate2.Text = customdate;
            ///////////////////////////



            pnlCalibration.Visible = false;
            pnlSensors.Visible = false;
            pnlweb.Visible = false;
            pnlshowdata.Visible = false;
            pnlloaddata.Visible = false;
            pnlhelp.Visible = false;
            pnlhashcode.Visible = false;
            pnldatatransfer.Visible = true;






            pnldatatransfer.BringToFront();






        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {



            myconn.Close();


            myconn.Open();





            OleDbDataAdapter da = new OleDbDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            da.SelectCommand = new OleDbCommand("select * from tblmatn ", myconn);
            da.Fill(ds, "tblmatn");
            dt = ds.Tables["tblmatn"];





            String txt = "";

            txt = (ds.Tables[0].Rows[0][0].ToString());











            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\config\\rdlsys_channels.conf");


            filewriter.WriteLine(txt);




            filewriter.Close();



            myconn.Close();

            MessageBox.Show("اطلاعات در فایل تنظیمات ذخیره گردید");


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {



            //Console.WriteLine("Enter any Key: ");
            //ConsoleKeyInfo name = Console.ReadKey();
            //Console.WriteLine("You pressed {0}", name.KeyChar);

            //
            //     Main();


            //AllocConsole();






        }


        public void Main()
        {
            Console.WriteLine("test");
            MessageBox.Show("test");
        }

        private void pnlInputs_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {


            pnlSensors.Visible = false;
            pnlCalibration.Visible = false;
            pnlweb.Visible = false;



            myconn.Close();


            myconn.Open();





            OleDbDataAdapter da = new OleDbDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            da.SelectCommand = new OleDbCommand("select * from tblInputs order by channel_code", myconn);
            da.Fill(ds, "tblInputs");
            dt = ds.Tables["tblInputs"];




            //foreach (DataRow dr in dt.Rows)
            //{
            //    MessageBox.Show(dr["channel name"].ToString());
            //}





            /////for datagrid view

            DataView dtaview = new DataView();
            dtaview.Table = ds.Tables["tblInputs"];


            dataGridViewInputs.DataSource = ds; // dataset
            dataGridViewInputs.DataMember = "tblInputs";













            dataGridViewInputs.Columns[0].HeaderText = "شماره ی کانال ورودی";

            dataGridViewInputs.Columns[1].HeaderText = "شماره سنسور";

            dataGridViewInputs.Columns[2].HeaderText = "نام  اختصاری سنسور";


            dataGridViewInputs.Columns[3].HeaderText = "نام سنسور";


            dataGridViewInputs.Columns[4].Width = 0;


            dataGridViewInputs.Columns[5].HeaderText = "نوع محاسبه و ذخیره سازی";



            dataGridViewInputs.Columns[6].HeaderText = "شماره پورت";
            dataGridViewInputs.Columns[7].HeaderText = "شماره پین";


            dataGridViewInputs.Columns[8].HeaderText = "فعال/غیرفعال";//channelType




            OleDbDataAdapter da1 = new OleDbDataAdapter();
            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();



            ////////////////////////fill comobo box to select name of sensor


            da1.SelectCommand = new OleDbCommand("select * from tblSensors ", myconn);
            da1.Fill(ds1, "tblSensors");
            dt1 = ds1.Tables["tblSensors"];


            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                cmbselectsensors.Items.Add(ds1.Tables[0].Rows[i][2].ToString());

            }


            myconn.Close();





            pnlInputs.Visible = true;





        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pnlInputs.Visible = false;

        }

        private void cmbselectsensors_SelectedIndexChanged(object sender, EventArgs e)
        {







            myconn.Close();
            myconn.Open();




            OleDbDataAdapter da = new OleDbDataAdapter();


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            da.SelectCommand = new OleDbCommand("select * from tblInputs where sensor_name_persian='" + cmbselectsensors.Text + "' order by channel_code ", myconn);
            da.Fill(ds, "tblInputs");
            dt = ds.Tables["tblInputs"];


            /////for datagrid view

            DataView dtaview = new DataView();
            dtaview.Table = ds.Tables["tblInputs"];


            dataGridViewInputs.DataSource = ds; // dataset
            dataGridViewInputs.DataMember = "tblInputs";



            myconn.Close();








        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        public void label8_Click(object sender, EventArgs e)
        {




        }

        private void Test_Click(object sender, EventArgs e)
        {







            pnlloaddata.Visible = true;



        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {


            pnlloaddata.Visible = false;




        }

        private void label9_Click_1(object sender, EventArgs e)
        {


            if (rdsensors.Checked == true)
            {





                label8_Click_1(null, null);


            }



        }

        private void label8_Click_1(object sender, EventArgs e)
        {


            //  dlgOpenFile.FileName = "rdlsys_client.conf";
            dlgOpenFile.FileName = "rdlsys_channels.conf";




            //    DialogResult resDialog = dlgOpenFile.ShowDialog();

            //rdlsys_client.conf


            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\rdlsys_channels.conf";


            myconn.Close();
            myconn.Open();

            ////////if (resDialog.ToString() == "OK")
            ////////{





            ////////    flname = dlgOpenFile.FileName;

            ////////}


            ////////////////////////hazf record hay ghably/////////////

            label10_Click(null, null);

            //////////////////////////////////////////////////////////////


            Application.DoEvents();
            Application.DoEvents();





            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);



            //  readline = (Filereader.ReadLine());


            DataSet oDS = new DataSet();


            OleDbDataAdapter da = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM tbltempdata", myconn));

            OleDbCommandBuilder oOrdersCmdBuilder = new OleDbCommandBuilder(da);
            da.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "tbltempdata";




            while ((readline = Filereader.ReadLine()) != null)
            {




                lblstatus.Text = "در حال بار گذاری اطلاعات " + "...............";


                if (progfetchsensors.Value + 2 < 100)
                {

                    progfetchsensors.Value = progfetchsensors.Value + 2;

                }




                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }



                if (readline == "")
                {


                    continue;


                }


                ////// joda kardane reshteha daray , va rikhtan dar yek araye

                ////////////////////////////////////////////////////////////////////





                //<channel index>, <channel name>, <sensorCode>,<sensorUnitCode>, <sensorUnitAbv>, savingType, channelType, hwPortType, hwPortNumber, hwPortPinNumber , calculationType, IsActive







                string s = readline;
                string[] values = s.Split(',');



                ///khandane satre aval baraye yek sensor shamel   etelaate paye 
                String channel_index, channel_name, sensorCode, sensorUnitCode, sensorUnitAbv, savingType, channelType, hwPortType, hwPortNumber, hwPortPinNumber, calculationType, IsActive;



                channel_index = values[0];

                channel_name = values[1];

                sensorCode = values[2];

                sensorUnitCode = values[3];

                sensorUnitAbv = values[4];

                savingType = values[5];

                channelType = values[6];

                hwPortType = values[7];

                hwPortNumber = values[8];

                hwPortPinNumber = values[9];

                calculationType = values[10];

                IsActive = values[11];



                //////making persian messagege to show to user

                string channel_name_persian = "";
                string savingType_persian = "";
                string channelType_persian = "";

                ////1 name sensor




                if (channel_name.Equals("EVP") || channel_name.Equals("EVPN") || channel_name.Equals("EVPX") || channel_name.Equals("EVPA"))
                {

                    channel_name_persian = "تبخیر";

                }



                if (channel_name.Equals("PRS") || channel_name.Equals("PRSX") || channel_name.Equals("PRSA") || channel_name.Equals("PRSN"))
                {

                    channel_name_persian = "فشار";

                }


                if (channel_name.Equals("WSP") || channel_name.Equals("WSPN") || channel_name.Equals("WSPX") || channel_name.Equals("WSPA"))
                {

                    channel_name_persian = "سرعت باد";

                }

                if (channel_name.Equals("HUM") || channel_name.Equals("HUMN") || channel_name.Equals("HUMA") || channel_name.Equals("HUMX"))
                {

                    channel_name_persian = "رطوبت";

                }

                if (channel_name.Equals("TMP") || channel_name.Equals("TMPN") || channel_name.Equals("TMPX") || channel_name.Equals("TMPA"))
                {

                    channel_name_persian = "دما";


                }

                if (channel_name.Equals("WDR") || channel_name.Equals("WDRN") || channel_name.Equals("WDRX") || channel_name.Equals("WDRA"))
                {

                    channel_name_persian = "جهت باد";

                }

                if (channel_name.Equals("RAIN") || channel_name.Equals("RANN") || channel_name.Equals("RANX") || channel_name.Equals("RANA"))
                {

                    channel_name_persian = "باران";


                }

                /////////////////////////////////////
                //////////////////////making name persian for calculationtype


                int savetype = int.Parse(savingType);



                switch (savetype)
                {


                    case 0:
                        savingType_persian = "مینیمم";
                        break;

                    case 1:
                        savingType_persian = "ماکزیمم";
                        break;
                    case 2:
                        savingType_persian = "میانگین";
                        break;

                    case 3:
                        savingType_persian = "آخرین مقدار";
                        break;

                    default:
                        break;


                }


                /////////////////////////////////making channeltype persian



                int chnltype = int.Parse(channelType);


                switch (chnltype)
                {


                    case 1:
                        channelType_persian = "رگولار";
                        break;

                    case 0:
                        channelType_persian = "جمع شونده";
                        break;

                    default:
                        break;


                }







                int num = 1;



                //int myInt = int.Parse(TextBoxD1.Text)
                // Insert the Data

                int myInt = int.Parse(channel_index);
                //if (myInt >= 20)
                //{



                DataRow oOrderRow = oDS.Tables["tbltempdata"].NewRow();
                oOrderRow["channel_index"] = channel_index;
                oOrderRow["channel_name"] = channel_name;




                oOrderRow["channel_name_persian"] = channel_name_persian;     ////add persian to show users



                oOrderRow["sensorCode"] = sensorCode;

                oOrderRow["sensorUnitCode"] = sensorUnitCode;
                oOrderRow["sensorUnitAbv"] = sensorUnitAbv;

                oOrderRow["savingType"] = savingType;
                oOrderRow["savingType_persian"] = savingType_persian;   ////add persian to show users


                oOrderRow["channelType"] = channelType;
                oOrderRow["channelType_persian"] = channelType_persian;    ////add persian to show users


                oOrderRow["hwPortType"] = hwPortType;
                oOrderRow["hwPortNumber"] = hwPortNumber;
                oOrderRow["hwPortPinNumber"] = hwPortPinNumber;

                oOrderRow["calculationType"] = calculationType;

                oOrderRow["IsActive"] = IsActive;



                oDS.Tables["tbltempdata"].Rows.Add(oOrderRow);

                da.Update(oDS, "tbltempdata");

                //}

                int a = datagridloadtempdata.Rows.Count;






                //   readline = Filereader.ReadLine();
                readline = Filereader.ReadLine();




                if (readline == "")
                {


                    readline = Filereader.ReadLine();


                }




                string k = readline;
                string[] tmpcalibrationpair = k.Split(',');


                string h = "";









                DataSet oDS1 = new DataSet();


                OleDbDataAdapter da1 = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM tbltempCalibration", myconn));

                OleDbCommandBuilder oOrdersCmdBuilder1 = new OleDbCommandBuilder(da1);
                da1.FillSchema(oDS1, SchemaType.Source);

                DataTable pTable1 = oDS1.Tables["Table"];
                pTable1.TableName = "tbltempCalibration";



                for (int j = 0; j < tmpcalibrationpair.Count() - 1; j++)
                {


                    //h = calibration[3];

                    String leftphisycalout, rightvalueparameter;


                    string l = tmpcalibrationpair[j];

                    string[] calibrationpair = l.Split('=');


                    leftphisycalout = calibrationpair[0];
                    rightvalueparameter = calibrationpair[1];



                    DataRow oOrderRow1 = oDS1.Tables["tbltempCalibration"].NewRow();
                    oOrderRow1["channel_name_persian"] = channel_name_persian;
                    oOrderRow1["numm"] = j;
                    oOrderRow1["valuee"] = leftphisycalout;     ////add persian to show users
                    oOrderRow1["calibrationValue"] = rightvalueparameter;
                    oOrderRow1["namesensorEn"] = channel_name;

                    oDS1.Tables["tbltempCalibration"].Rows.Add(oOrderRow1);

                    ////da1.Update(oDS1, "tbltempCalibration");






                    ////myconn.Close();



                }

                da1.Update(oDS1, "tbltempCalibration");

                //////hamid   }

                /////////////////////////////// insert data calibrationtable







                readline = Filereader.ReadLine();//yek khat ra rad midahad
                // readline = Filereader.ReadLine();







                if (readline == null)
                {


                    break;


                }








                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }








            }






            ////datagridloadtempdata.Rows[0].Visible = false;
            ////datagridloadtempdata.Rows[2].Visible = false;
            ////datagridloadtempdata.Rows[3].Visible = false;
            ////datagridloadtempdata.Rows[4].Visible = false;
            ////datagridloadtempdata.Rows[5].Visible = false;
            ////datagridloadtempdata.Rows[6].Visible = false;
            ////datagridloadtempdata.Rows[7].Visible = false;
            ////datagridloadtempdata.Rows[8].Visible = false;















            Filereader.Close();


            //      System.Windows.Forms.MessageBox.Show("پابان بارگذاری فایل", "Caption", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            progfetchsensors.Value = 100;



            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();



            label12_Click(null, null);


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            label13_Click(null, null);


            //   lblstatus.Text = "پایان بارگذاری اطلاعات";

            //Strating parsing rdlsys_channels.conf




            ////datagridloadtempdata.Rows[0].Visible = false;
            ////datagridloadtempdata.Rows[2].Visible = false;
            ////datagridloadtempdata.Rows[3].Visible = false;
            ////datagridloadtempdata.Rows[4].Visible = false;
            ////datagridloadtempdata.Rows[5].Visible = false;
            ////datagridloadtempdata.Rows[6].Visible = false;
            ////datagridloadtempdata.Rows[7].Visible = false;
            ////datagridloadtempdata.Rows[8].Visible = false;


            datagridloadtempdata.Rows[0].Height = 0;
            datagridloadtempdata.Rows[1].Height = 0;
            datagridloadtempdata.Rows[2].Height = 0;
            datagridloadtempdata.Rows[3].Height = 0;
            datagridloadtempdata.Rows[4].Height = 0;
            datagridloadtempdata.Rows[5].Height = 0;
            datagridloadtempdata.Rows[6].Height = 0;
            datagridloadtempdata.Rows[7].Height = 0;
            datagridloadtempdata.Rows[8].Height = 0;
            datagridloadtempdata.Rows[9].Height = 0;
            datagridloadtempdata.Rows[10].Height = 0;
            datagridloadtempdata.Rows[11].Height = 0;
            datagridloadtempdata.Rows[12].Height = 0;
            datagridloadtempdata.Rows[13].Height = 0;
            datagridloadtempdata.Rows[14].Height = 0;
            datagridloadtempdata.Rows[15].Height = 0;
            datagridloadtempdata.Rows[16].Height = 0;
            datagridloadtempdata.Rows[17].Height = 0;
            datagridloadtempdata.Rows[18].Height = 0;
            datagridloadtempdata.Rows[19].Height = 0;














        }

        public void label10_Click(object sender, EventArgs e)
        {

            myconn.Close();
            myconn.Open();


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            OleDbDataAdapter da3 = new OleDbDataAdapter();
            da3.SelectCommand = new OleDbCommand("select * from tbltempdata ", myconn);
            da3.Fill(ds, "tbltempdata");
            dt = ds.Tables["tbltempdata"];

            for (int p = 0; p < dt.Rows.Count; p++)
            {

                dt.Rows[p].Delete();


            }


            da3.DeleteCommand = new OleDbCommand("delete  from tbltempdata ", myconn);



            try
            {
                da3.Update(dt);
            }
            catch (Exception ex)
            {
                // throw ex;
            }


            ////////////////////////////////////////////////////////////////////////////////////


            myconn.Close();
            myconn.Open();


            DataSet dss = new DataSet();
            DataTable dtt = new DataTable();

            OleDbDataAdapter da4 = new OleDbDataAdapter();
            da4.SelectCommand = new OleDbCommand("select * from tbltempCalibration ", myconn);
            da4.Fill(dss, "tbltempCalibration");
            dtt = dss.Tables["tbltempCalibration"];

            for (int p = 0; p < dtt.Rows.Count; p++)
            {

                dtt.Rows[p].Delete();


            }


            da4.DeleteCommand = new OleDbCommand("delete  from tbltempCalibration ", myconn);



            try
            {
                da4.Update(dtt);
            }
            catch (Exception ex)
            {
                // throw ex;
            }






            myconn.Close();



        }

        private void label12_Click(object sender, EventArgs e)
        {



            //datagridloadtempdata.Visible = true;
            //datagridloadtempcalibrationdata.Visible = false;



            if (datagridloadtempdata.RowCount > 0)
            {

                return;

            }



            ////myconn.Close();


            ////myconn.Open();









            OleDbDataAdapter da = new OleDbDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            da.SelectCommand = new OleDbCommand("select * from tbltempdata order by channel_index", myconn);
            da.Fill(ds, "tbltempdata");
            dt = ds.Tables["tbltempdata"];




            //foreach (DataRow dr in dt.Rows)
            //{
            //    MessageBox.Show(dr["channel name"].ToString());
            //}





            /////for datagrid view

            DataView dtaview = new DataView();
            dtaview.Table = ds.Tables["tbltempdata"];


            datagridloadtempdata.DataSource = ds; // dataset
            datagridloadtempdata.DataMember = "tbltempdata";


            ////myconn.Close();










            // datagridloadtempdata.Columns[0].HeaderText = "شماره ی سنسور";


            datagridloadtempdata.Columns[0].Width = 0;
            datagridloadtempdata.Columns[1].HeaderText = "شماره کانال";

            datagridloadtempdata.Columns[2].HeaderText = "نام  اختصاری کانال";

            datagridloadtempdata.Columns[3].HeaderText = "نام سنسور";



            datagridloadtempdata.Columns[4].HeaderText = "کد سنسور";



            //////datagridloadtempdata.Columns[5].HeaderText = "واحد اندازه گیری";

            datagridloadtempdata.Columns[6].HeaderText = "واحد اندازه گیری";

            datagridloadtempdata.Columns[7].HeaderText = "کد نوع ذخیره سازی";


            datagridloadtempdata.Columns[8].HeaderText = "نوع ذخیره سازی";//savingType

            datagridloadtempdata.Columns[9].HeaderText = "کد نوع سنسور";//savingType

            datagridloadtempdata.Columns[10].HeaderText = "نوع سنسور";//savingType











            datagridloadtempdata.Columns[11].HeaderText = "نوع پورت";//hwPortType

            datagridloadtempdata.Columns[12].HeaderText = "شماره پورت";//hwPortNumber

            datagridloadtempdata.Columns[13].HeaderText = "شماره پین";//hwPortPinNumber

            datagridloadtempdata.Columns[14].HeaderText = "کد نوع محاسبه";//calculationType

            datagridloadtempdata.Columns[15].HeaderText = "کد فعال/غیرفعال";//IsActive






        }

        private void label13_Click(object sender, EventArgs e)
        {


            //datagridloadtempdata.Visible = false;
            //datagridloadtempcalibrationdata.Visible = true;


            if (datagridloadtempcalibrationdata.RowCount > 0)
            {

                return;

            }



            myconn.Close();


            myconn.Open();





            OleDbDataAdapter da = new OleDbDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            da.SelectCommand = new OleDbCommand("select distinct(namesensorEn) from tbltempCalibration order by namesensorEn", myconn);
            da.Fill(ds, "tbltempCalibration");
            dt = ds.Tables["tbltempCalibration"];






            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // if (i >= 20)
                //{

                //     if (ds.Tables[0].Rows[i][0].ToString().Equals("BAT") == true || ds.Tables[0].Rows[i][0].ToString().Equals("BATN") == true || ds.Tables[0].Rows[i][0].ToString().Equals("BATX") == true || ds.Tables[0].Rows[i][0].ToString().Equals("BATA") == true || ds.Tables[0].Rows[i][0].ToString().Equals("RAN_TOT") == true || ds.Tables[0].Rows[i][0].ToString().Equals("RAN_24") == true || ds.Tables[0].Rows[i][0].ToString().Equals("RAN_12") == true || ds.Tables[0].Rows[i][0].ToString().Equals("RAN_5M") == true)
                if (ds.Tables[0].Rows[i][0].ToString().Equals("BAT") == true || ds.Tables[0].Rows[i][0].ToString().Equals("BATN") == true || ds.Tables[0].Rows[i][0].ToString().Equals("BATX") == true || ds.Tables[0].Rows[i][0].ToString().Equals("BATA") == true)
                {

                    cmbshowsensors.Items.Add(ds.Tables[0].Rows[i][0].ToString());

                }
                //}


            }












        }

        private void datagridloadtempdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void label14_Click(object sender, EventArgs e)
        {




            // lblstatus.Text = "در حال تنظیم فایل خروجی";
            progfetchsensors.Value = 2;



            ////  zakhire kardane maghadire calibration dar file






            myconn.Close();


            progfetchsensors.Value = progfetchsensors.Value + 20;


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();




            myconn.Open();




            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\rdlsys_channels.conf");



            filewriter.WriteLine("##############################");
            filewriter.WriteLine("## channels configuration and calibratiob table");
            filewriter.WriteLine("");
            filewriter.WriteLine("##<channel index>, <channel name>, <sensorCode>,<sensorUnitCode>, <sensorUnitAbv>, savingType, channelType, hwPortType, hwPortNumber, hwPortPinNumber , calculationType, IsActive");
            filewriter.WriteLine("## at next line calibrationValuesPares (simple calib table)");
            filewriter.WriteLine("##channel config values count = 12");
            filewriter.WriteLine("");
            filewriter.WriteLine("# SavingType {Min, Max, Avg, Last, Calculated};");
            filewriter.WriteLine("# ChannelType {AnalogueAditive_Channel, AnalogueInstant_Channel, Digital_Channel}");
            filewriter.WriteLine("# ChannelPortType {GPIO_Port, Serial_Port, Paralle_Prot};");
            filewriter.WriteLine("");
            filewriter.WriteLine("#channel_0 : Tabkhir (self)");




            string dta = "";
            string datcalibration = "";







            for (int i = 0; i < datagridloadtempdata.RowCount - 1; i++)
            {

                progfetchsensors.Value = progfetchsensors.Value + 1;

                dta = dta + datagridloadtempdata.Rows[i].Cells[1].Value.ToString() + ",";
                dta = dta + datagridloadtempdata.Rows[i].Cells[2].Value.ToString() + ",";

                dta = dta + datagridloadtempdata.Rows[i].Cells[4].Value.ToString() + ",";
                dta = dta + datagridloadtempdata.Rows[i].Cells[5].Value.ToString() + ",";
                dta = dta + datagridloadtempdata.Rows[i].Cells[6].Value.ToString() + ",";
                dta = dta + datagridloadtempdata.Rows[i].Cells[7].Value.ToString() + ",";


                dta = dta + datagridloadtempdata.Rows[i].Cells[9].Value.ToString() + ",";
                // dta = dta+datagridloadtempdata.Rows[i].Cells[10].Value.ToString()+",";
                dta = dta + datagridloadtempdata.Rows[i].Cells[11].Value.ToString() + ",";
                dta = dta + datagridloadtempdata.Rows[i].Cells[12].Value.ToString() + ",";

                dta = dta + datagridloadtempdata.Rows[i].Cells[13].Value.ToString() + ",";
                dta = dta + datagridloadtempdata.Rows[i].Cells[14].Value.ToString() + ",";
                dta = dta + datagridloadtempdata.Rows[i].Cells[15].Value.ToString();



                ////setkardane  meghdar calibration baraye har kanal 
                /////peyda kardane name table az jadvale calibrasione jadid va add kard be file 

                string namesenosor = "";
                namesenosor = datagridloadtempdata.Rows[i].Cells[2].Value.ToString();


                datcalibration = "";


                OleDbDataAdapter da = new OleDbDataAdapter();
                DataSet ds = new DataSet();



                da.SelectCommand = new OleDbCommand("select * from tbltempCalibration where namesensorEn='" + namesenosor + "' order by numm ", myconn);
                da.Fill(ds, "tbltempCalibration");
                // dt = ds.Tables["tbltempCalibration"];

                string leftside = "", rightside = "";
                datcalibration = "";

                int p = ds.Tables[0].Rows.Count;


                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    leftside = (ds.Tables[0].Rows[k][2].ToString());
                    rightside = (ds.Tables[0].Rows[k][3].ToString());


                    datcalibration = datcalibration + leftside + "=" + rightside + ",";


                }











                ////////////////////////////////////////////////////////




                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();


                filewriter.WriteLine(dta);
                filewriter.WriteLine(datcalibration);
                filewriter.WriteLine("");


                dta = "";
                datcalibration = "";


            }






            filewriter.Close();

            progfetchsensors.Value = 100;
            //  lblstatus.Text = "پایان مرحله خروجی فایل";




        }

        private void label16_Click(object sender, EventArgs e)
        {


            if (glbguid == false)
            {

                MessageBox.Show("شما مجاز به استفاده از نرم افزار نیستید");
                return;

            }






            ////////////////////////////////////is ok/////////////////////

            ////FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.contoso.com/test.htm");
            ////request.Method = WebRequestMethods.Ftp.DownloadFile;



            ////// This example assumes the FTP site uses anonymous logon.user name,pass
            ////request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            ////FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            ////Stream responseStream = response.GetResponseStream();
            ////StreamReader reader = new StreamReader(responseStream);
            ////Console.WriteLine(reader.ReadToEnd());

            ////Console.WriteLine("Download Complete, status {0}", response.StatusDescription);

            ////reader.Close();
            ////response.Close();  





            //////////////////////////is ok //////////////////////

            //// FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" +  Path.GetFileName(filePath));



            ////request.Method = WebRequestMethods.Ftp.UploadFile;
            ////request.Credentials = new NetworkCredential(username, password);
            ////request.UsePassive = true;
            ////request.UseBinary = true;
            ////request.KeepAlive = false;




            ////FileStream stream = File.OpenRead(filePath);
            ////byte[] buffer = new byte[stream.Length];


            ////stream.Read(buffer, 0, buffer.Length);
            ////stream.Close();

            //////////////////////////////////////////////

            chkautomated.Checked = false;
            pnlCalibration.Visible = false;
            pnlSensors.Visible = false;
            pnlweb.Visible = false;
            pnlshowdata.Visible = false;
            pnlhelp.Visible = false;

            pnlhashcode.Visible = false;
            pnldatatransfer.Visible = false;
            pnl_show_digital_rain.Visible = false;


            pnlloaddata.Visible = true;


            tabControl1.SelectedTab = tabPage4;

        }


        private void getFileList(string FTPAddress, string username, string password)
        {
            List<string> files = new List<string>();
            List<string> fileList = new List<string>();

            listboxFiles.Items.Clear();


            try
            {
                //Optional
                lblstatusftp.Text = "در حال اتصال با سیستم ...";
                Application.DoEvents();

                //Create FTP request
                FtpWebRequest request = FtpWebRequest.Create(FTPAddress) as FtpWebRequest;

                request.Method = WebRequestMethods.Ftp.ListDirectory;

                //   request.Method = WebRequestMethods.Ftp.GetDateTimestamp ;


                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                //Read the server's response
                //  lblstatusftp.Text = "ایجاد لیست فایلها...";
                Application.DoEvents();

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;


                //   Stream responseStream = response.LastModified();



                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                //    StreamReader reader = new StreamReader(responseStream);





                while (!reader.EndOfStream)
                {
                    Application.DoEvents();
                    files.Add(reader.ReadLine());
                }

                //Clean-up
                reader.Close();
                responseStream.Close(); //redundant
                response.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
            }

            username = string.Empty;
            password = string.Empty;

            //  this.Text = "Download Data through FTP"; //Back to normal title

            //If the list was successfully received, display it to the user
            //through a dialog

            fileList = files;
            if (files.Count != 0)
            {


                foreach (string file in fileList)
                {
                    listboxFiles.Items.Add(file);
                }


                //for test and zahedi
                listboxFiles.Items.Add("rdlsys_client.info");
                //for test and zahedi

            }




            if (listboxFiles.Items.Count == 0)
            {


                MessageBox.Show(" مشکل در برقراری ارتباط با سیسیتم لطفا دوباره سعی نمایید");


            }





            if (listboxFiles.Items.Count > 0)
            {


                lblstatusftp.Text = "ارتباط با سیستم با موفقیت انجام شد";


            }









        }








        private void getFileList3(string FTPAddress, string username, string password)
        {
            List<string> files = new List<string>();
            List<string> fileList = new List<string>();

            listboxFiles3.Items.Clear();


            try
            {
                //Optional
                lblstatusftp2.Text = "در حال اتصال با سیستم ...";
                Application.DoEvents();

                //Create FTP request
                FtpWebRequest request = FtpWebRequest.Create(FTPAddress) as FtpWebRequest;

                request.Method = WebRequestMethods.Ftp.ListDirectory;


                //    request.Method = WebRequestMethods.Ftp.GetDateTimestamp;



                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                //Read the server's response
                //  lblstatusftp.Text = "ایجاد لیست فایلها...";
                Application.DoEvents();

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                while (!reader.EndOfStream)
                {
                    Application.DoEvents();
                    files.Add(reader.ReadLine());
                }

                //Clean-up
                reader.Close();
                responseStream.Close(); //redundant
                response.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
            }

            username = string.Empty;
            password = string.Empty;



            fileList = files;
            if (files.Count != 0)
            {


                foreach (string file in fileList)
                {
                    listboxFiles3.Items.Add(file);
                }


            }




            if (listboxFiles3.Items.Count == 0)
            {


                MessageBox.Show(" مشکل در برقراری ارتباط با سیسیتم لطفا دوباره سعی نمایید");


            }





            if (listboxFiles2.Items.Count > 0)
            {


                lblstatusftp2.Text = "ارتباط با سیستم با موفقیت انجام شد";


            }

        }








        private void getFileList2(string FTPAddress, string username, string password)
        {
            List<string> files = new List<string>();
            List<string> fileList = new List<string>();

            listboxFiles2.Items.Clear();


            try
            {
                //Optional
                lblstatusftp2.Text = "در حال اتصال با سیستم ...";
                Application.DoEvents();

                //Create FTP request
                FtpWebRequest request = FtpWebRequest.Create(FTPAddress) as FtpWebRequest;

                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                //Read the server's response
                //  lblstatusftp.Text = "ایجاد لیست فایلها...";
                Application.DoEvents();

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                while (!reader.EndOfStream)
                {
                    Application.DoEvents();
                    files.Add(reader.ReadLine());
                }

                //Clean-up
                reader.Close();
                responseStream.Close(); //redundant
                response.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
            }

            username = string.Empty;
            password = string.Empty;



            fileList = files;
            if (files.Count != 0)
            {


                foreach (string file in fileList)
                {
                    listboxFiles2.Items.Add(file);
                }


            }




            if (listboxFiles2.Items.Count == 0)
            {


                MessageBox.Show(" مشکل در برقراری ارتباط با سیسیتم لطفا دوباره سعی نمایید");


            }





            if (listboxFiles2.Items.Count > 0)
            {


                lblstatusftp2.Text = "ارتباط با سیستم با موفقیت انجام شد";


            }









        }




        //Connects to the FTP server and downloads the file
        private void downloadFile(string FTPAddress, string filename, string username, string password)
        {
            downloadedData = new byte[0];

            try
            {
                //Optional
                lblstatus.Text = "در حال ارتباط...";
                Application.DoEvents();


                FtpWebRequest request = FtpWebRequest.Create(FTPAddress + filename) as FtpWebRequest;

                //Optional
                lblstatus.Text = "خواندن اطلاعات ...";
                Application.DoEvents();

                //Get the file size first (for progress bar)
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = true; //don't close the connection

                int dataLength = (int)request.GetResponse().ContentLength;

                //Optional
                this.Text = "درحال دانلود...";
                Application.DoEvents();

                //Now get the actual data
                request = FtpWebRequest.Create(FTPAddress + "/" + filename) as FtpWebRequest;
                lblsave.Text = FTPAddress + "/" + filename;

                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false; //close the connection when done

                //Set up progress bar
                prgstatusftbdownload.Value = 0;
                prgstatusftbdownload.Maximum = dataLength;

                /////hamid///

                lbProgress.Text = "0/" + dataLength.ToString();

                ///////



                //Streams
                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream reader = response.GetResponseStream();

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                byte[] buffer = new byte[1024]; //downloads in chuncks

                while (true)
                {
                    Application.DoEvents(); //prevent application from crashing

                    //Try to read the data
                    int bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        //Nothing was read, finished downloading
                        prgstatusftbdownload.Value = prgstatusftbdownload.Maximum;


                        //////hamid
                        lbProgress.Text = dataLength.ToString() + "/" + dataLength.ToString();
                        //////



                        Application.DoEvents();
                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        memStream.Write(buffer, 0, bytesRead);

                        //Update the progress bar
                        if (prgstatusftbdownload.Value + bytesRead <= prgstatusftbdownload.Maximum)
                        {
                            prgstatusftbdownload.Value += bytesRead;


                            lbProgress.Text = prgstatusftbdownload.Value.ToString() + "/" + dataLength.ToString();

                            prgstatusftbdownload.Refresh();
                            Application.DoEvents();
                        }
                    }
                }

                //Convert the downloaded stream to a byte array
                downloadedData = memStream.ToArray();

                //Clean up
                reader.Close();
                memStream.Close();
                response.Close();

                //  MessageBox.Show("Downloaded Successfully");
                //  lblstatusftp.Text = "پایان مرحله  دانلود";
                lblstatusftp.Text = ".... در حال  دانلود";



            }
            catch (Exception)
            {
                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
            }

            txtData.Text = downloadedData.Length.ToString();
            this.Text = "پایان مرحله  دانلود";

            username = string.Empty;
            password = string.Empty;
        }

        //Connects to the FTP server and request the list of available files




        private void downloadFileDateTimeStamp(string FTPAddress, string filename, string username, string password)
        {


            try
            {
                //Optional

                //lblstatus.Text = "در حال ارتباط...";

                Application.DoEvents();




                ////////////////////////////hazf data ghably tbldatalog




                myconn.Close();
                myconn.Open();


                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                OleDbDataAdapter da3 = new OleDbDataAdapter();
                da3.SelectCommand = new OleDbCommand("select * from tbldatelog ", myconn);
                da3.Fill(ds, "tbldatelog");
                dt = ds.Tables["tbldatelog"];

                for (int p = 0; p < dt.Rows.Count; p++)
                {

                    dt.Rows[p].Delete();


                }


                da3.DeleteCommand = new OleDbCommand("delete  from tbldatelog ", myconn);



                try
                {
                    da3.Update(dt);
                }
                catch (Exception ex)
                {
                    // throw ex;
                }


                ////////////////////////////////////////////////////////////////////////////////////


                myconn.Close();


                /////////////////////////////////////////////






                myconn.Open();
                for (var i = 0; i < listboxFiles3.Items.Count; i++)
                {

                    Application.DoEvents();
                    filename = listboxFiles3.Items[i].ToString();






                    FtpWebRequest request = FtpWebRequest.Create(FTPAddressdownloaddata + filename) as FtpWebRequest;


                    Application.DoEvents();



                    request.Method = WebRequestMethods.Ftp.GetDateTimestamp;


                    request.Credentials = new NetworkCredential(username, password);
                    request.UsePassive = true;
                    request.UseBinary = true;
                    request.KeepAlive = true; //don't close the connection


                    FtpWebResponse resp = (FtpWebResponse)request.GetResponse();

                    // MessageBox.Show(resp.LastModified.ToString());


                    DateTime DateValue;



                    //   string DateString =resp.LastModified.ToString().Substring(12);

                    string DateString = resp.LastModified.ToString();


                    string sDate = string.Empty;


                    string Day = "", Month = "", Year = "", hour = "", min = "", am = "";
                    DateTime _date = resp.LastModified;




                    int count = 0;
                    string format = "dd-MM-yyyy-HH-mm-ss-tt";



                    sDate = _date.ToString(format);

                    string[] Words = sDate.Split(new char[] { '-' });

                    foreach (string Word in Words)
                    {
                        count += 1;
                        if (count == 1) { Day = Word; }
                        if (count == 2) { Month = Word; }
                        if (count == 3) { Year = Word; }
                        if (count == 4) { hour = Word; }
                        if (count == 5) { min = Word; }
                        if (count == 7) { am = Word; }


                    }



                    ////////////////////////////convert date to persian////////////////

                    Day = "";
                    Year = "";
                    Month = "";



                    string Shamsi = "";

                    DateTime miladi = DateTime.Now;
                    miladi = _date;

                    System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
                    Shamsi = string.Format("{0}/{1}/{2}", shamsi.GetYear(miladi), shamsi.GetMonth(miladi), shamsi.GetDayOfMonth(miladi));

                    string[] pWords = Shamsi.Split(new char[] { '/' });

                    Year = pWords[0];
                    Month = pWords[1];
                    Day = pWords[2];


                    if (int.Parse(Day) < 10)
                    {
                        Day = "0" + Day;

                    }




                    if (int.Parse(Month) < 10)
                    {
                        Month = "0" + Month;

                    }




                    Shamsi = Year + "/" + Month + "/" + Day;

                    ////////////////////adding to db


                    DataSet oDS = new DataSet();
                    OleDbDataAdapter da = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM tbldatelog", myconn));

                    OleDbCommandBuilder oOrdersCmdBuilder = new OleDbCommandBuilder(da);
                    da.FillSchema(oDS, SchemaType.Source);

                    DataTable pTable = oDS.Tables["Table"];
                    pTable.TableName = "tbldatelog";


                    int num = 1;

                    //int myInt = int.Parse(TextBoxD1.Text)
                    // Insert the Data
                    DataRow oOrderRow = oDS.Tables["tbldatelog"].NewRow();
                    oOrderRow["yeardaymounth"] = Shamsi;
                    oOrderRow["logfile"] = filename;


                    oOrderRow["hourval"] = hour;

                    oOrderRow["minval"] = min;



                    oDS.Tables["tbldatelog"].Rows.Add(oOrderRow);

                    da.Update(oDS, "tbldatelog");


                    //////////////////////////////////
                    //   listBox4.Items.Add(resp.LastModified.ToString() + "  :   " + filename);



                    //   Application.DoEvents();

                }


                myconn.Close();
                myconn.Open();

                /////////////////////////////namayesh dar combo///////////////////////////

                OleDbDataAdapter da1 = new OleDbDataAdapter();

                DataSet ds1 = new DataSet();
                DataTable dt1 = new DataTable();

                cmbfilelog.Items.Clear();


                da1.SelectCommand = new OleDbCommand("select distinct(yeardaymounth) from tbldatelog order by yeardaymounth ", myconn);
                da1.Fill(ds1, "tbldatelog");
                dt1 = ds1.Tables["tbldatelog"];


                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    cmbfilelog.Items.Add(ds1.Tables[0].Rows[i][0].ToString());

                }


                /////////////////////////////////////////////////////////////////////////


            }


            catch (Exception)
            {
                MessageBox.Show("مشکل در بارگذاری فایلهای ذخیره شده");
            }





        }



        // public void  


        private void deletedownloadFiledata(string FTPAddress, string filename, string username, string password)
        {



            downloadedDatadata = new byte[0];

            try
            {
                //Optional

                //lblstatus.Text = "در حال ارتباط...";
                Application.DoEvents();


                FtpWebRequest request = FtpWebRequest.Create(FTPAddressdownloaddata + filename) as FtpWebRequest;
                //   FtpWebRequest request = FtpWebRequest.Create(FTPAddressdownloaddata ) as FtpWebRequest;

                //Optional


                lblstatusftp2.Text = "خواندن اطلاعات ...";


                Application.DoEvents();

                //Get the file size first (for progress bar)


                //   request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                //   request.Method = WebRequestMethods.Ftp.GetDateTimestamp ;



                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = true; //don't close the connection

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;

                // int dataLength = (int)request.GetResponse().ContentLength;

                // string g = request.GetResponse().ToString ;


                //Optional
                //  lblstatusftp2.Text = "درحال دانلود...";
                Application.DoEvents();







                lblstatusftp2.Text = "پایان حذف  اطلاعات";



            }

            catch (Exception)
            {
                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
            }

            txtData.Text = downloadedDatadata.Length.ToString();


            username = string.Empty;
            password = string.Empty;


        }


        private void downloadFiledata(string FTPAddress, string filename, string username, string password)
        {
            downloadedDatadata = new byte[0];

            try
            {
                //Optional

                //lblstatus.Text = "در حال ارتباط...";

                ////if (txtnamestation.Visible == true)
                ////{
                ////    return;
                ////}

                if (cmbnamestation.Text.Equals("") == false)
                {
                    return;
                }



                Application.DoEvents();


                FtpWebRequest request = FtpWebRequest.Create(FTPAddressdownloaddata + filename) as FtpWebRequest;

                //Optional


                lblstatusftp2.Text = "خواندن اطلاعات ...";


                Application.DoEvents();

                //Get the file size first (for progress bar)


                request.Method = WebRequestMethods.Ftp.GetFileSize;
                //  request.Method = WebRequestMethods.Ftp.DeleteFile;

                //   request.Method = WebRequestMethods.Ftp.GetDateTimestamp ;



                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = true; //don't close the connection

                int dataLength = (int)request.GetResponse().ContentLength;

                //  string g = request.GetResponse().ToString ;


                //Optional
                //  lblstatusftp2.Text = "درحال دانلود...";
                Application.DoEvents();

                //Now get the actual data
                request = FtpWebRequest.Create(FTPAddressdownloaddata + filename) as FtpWebRequest;

                //  lblsave2.Text = FTPAddress + "/" + filename;

                request.Method = WebRequestMethods.Ftp.DownloadFile;
                //      request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false; //close the connection when done

                //Set up progress bar
                prgstatusftbdownload2.Value = 0;
                prgstatusftbdownload2.Maximum = dataLength;

                /////hamid///

                lbProgress2.Text = "0/" + dataLength.ToString();

                ///////



                //Streams
                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream reader = response.GetResponseStream();

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                byte[] buffer = new byte[1024]; //downloads in chuncks

                while (true)
                {
                    Application.DoEvents(); //prevent application from crashing

                    //Try to read the data
                    int bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        //Nothing was read, finished downloading
                        prgstatusftbdownload2.Value = prgstatusftbdownload2.Maximum;


                        //////hamid
                        lbProgress2.Text = dataLength.ToString() + "/" + dataLength.ToString();
                        //////



                        Application.DoEvents();
                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        memStream.Write(buffer, 0, bytesRead);

                        //Update the progress bar
                        if (prgstatusftbdownload2.Value + bytesRead <= prgstatusftbdownload2.Maximum)
                        {
                            prgstatusftbdownload2.Value += bytesRead;


                            lbProgress2.Text = prgstatusftbdownload2.Value.ToString() + "/" + dataLength.ToString();

                            prgstatusftbdownload2.Refresh();
                            Application.DoEvents();
                        }
                    }
                }

                //Convert the downloaded stream to a byte array
                downloadedDatadata = memStream.ToArray();

                //Clean up
                reader.Close();
                memStream.Close();
                response.Close();

                //  MessageBox.Show("Downloaded Successfully");
                lblstatusftp2.Text = "پایان خواندن اطلاعات";



            }
            catch (Exception)
            {
                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
            }

            txtData.Text = downloadedDatadata.Length.ToString();


            username = string.Empty;
            password = string.Empty;
        }

        //Connects to the FTP server and request the list of available files








        private void pictureBox4_Click_1(object sender, EventArgs e)
        {


            string FTPAddress, username, password;

            //    FTPAddress = "ftp://ftp.microsoft.com//Softlib//";


            // FTPAddress = "ftp://192.168.1.21//config//";

            //   FTPAddress = "ftp://root:password@192.168.1.222//mnt//dom//RDLSystemClient//config//";

            FTPAddress = "ftp://192.168.1.222////mnt//dom//RDLSystemClient//config//";

            //  FTPAddress = "ftp://root:password@192.168.1.222//";


            username = "root";
            password = "password";


            getFileList(FTPAddress, username, password);







            //webftp.Navigate("ftp://root:password@192.168.1.222//mnt//dom//RDLSystemClient//config//");
            //webftp.Visible = true;


            ////webftp.Navigate("ftp://ftp.microsoft.com/Softlib/");
            ////webftp.Visible = true;

            ////////////////////////////////////is ok/////////////////////






            //////////////////////////is ok //////////////////////


            ////  string FTPAddress, username, password;

            ////string filePath = "index.txt";


            ////  FTPAddress = "ftp://ftp.microsoft.com//Softlib//";

            ////  username = "";
            ////  password = "";


            // FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));


            //// FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" +"");



            ////// request.Method = WebRequestMethods.Ftp.UploadFile;
            //// request.Credentials = new NetworkCredential(username, password);

            //// request.Method = WebRequestMethods.Ftp.DownloadFile;


            //// request.UsePassive = true;
            //// request.UseBinary = true;
            //// request.KeepAlive = false;




            //// FileStream stream = File.OpenRead(FTPAddress+ filePath);
            //// byte[] buffer = new byte[stream.Length];


            //// stream.Read(buffer, 0, buffer.Length);
            //// stream.Close();

            //////////////////////////////////////////////






        }



        private void tabControl1_Click(object sender, EventArgs e)
        {



        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {


            //  datagridloadtempcalibrationdata

            tabControl1.SelectedTab = tabPage1;
            //  tabControl1.SelectedTab = tabControl1.TabPages;





            label9_Click_1(null, null);


            Application.DoEvents();
            Application.DoEvents();

            label47_Click(null, null); //// rain

            Application.DoEvents();
            Application.DoEvents();

            label53_Click(null, null); //frequance


            Application.DoEvents();
            Application.DoEvents();

            label33_Click(null, null);    //save settings


            Application.DoEvents();
            Application.DoEvents();


            label22_Click(null, null);    //server


            label79_Click(null, null);    //rain parameter

            label98_Click(null, null);    //rain volume

            label102_Click(null, null);  //sms config




        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {


            string flname = "", caption;




            ////if (rdrain.Checked == true) flname = "باران ";
            ////if (rdsensors.Checked == true) flname = "سنسورها و کالیبراسیون";
            ////if (rdserver.Checked == true) flname = "سرور ";
            ////if (rssavesettings.Checked == true) flname = "تنظیمات ذخیره سازی ";





            string message = "آیا مطمئن هستید که قصد اعمال تنظیمات بر روی سیستم را دارید ؟";

            caption = "خروجی فایل جدید" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }




            label54_Click(null, null); ///frequence
            ///
            Application.DoEvents();
            Application.DoEvents();


            label48_Click(null, null);///rain
            ///

            Application.DoEvents();
            Application.DoEvents();

            label20_Click_1(null, null); ///save settings
            Application.DoEvents();
            Application.DoEvents();


            label23_Click(null, null);  ///server
            Application.DoEvents();
            Application.DoEvents();


            label14_Click(null, null); ///sensors
            Application.DoEvents();
            Application.DoEvents();


            label80_Click(null, null); ///rain parameters
            Application.DoEvents();
            Application.DoEvents();


            label95_Click(null, null); ///rain parameters
            Application.DoEvents();
            Application.DoEvents();


            label68_Click_1(null, null); //sms config
            Application.DoEvents();
            Application.DoEvents();

            ////if (rdfrequence.Checked == true)
            ////{


            ////    label54_Click(null, null);


            ////}





            ////if (rdrain.Checked == true)
            ////{


            ////    label48_Click(null, null);


            ////}





            if (rssavesettings.Checked == true)
            {


                label20_Click_1(null, null);


            }



            if (rdserver.Checked == true)
            {

                label23_Click(null, null);

            }

            else
            {
                label14_Click(null, null);
            }



            /////ersal be system be sorate mostaghim az injaaaaa
            progfetchsensors.Value = 0;
            lblstatus.Text = "ارسال تنظیمات به سیستم ....";
            Thread.Sleep(2000);
            lblsendftp_Click(null, null);
            //lblstatus.Text = "";
            lblstatus.Text = "پایان ارسال اطلاعات به سیستم ";
            progfetchsensors.Value = progfetchsensors.Maximum;
            ///////////////////////////






        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {


            //  dlgOpenFile.FileName = "rdlsys_client.conf";
            dlgOpenFile.FileName = "server.conf";




            //    DialogResult resDialog = dlgOpenFile.ShowDialog();

            //rdlsys_client.conf


            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\server.conf";



            //if (resDialog.ToString() == "OK")
            //{


            //    flname = dlgOpenFile.FileName;

            //}



            ///////////////////
            ////////////////////


            ////////////////////////hazf record hay ghably/////////////

            //label10_Click(null, null);

            //////////////////////////////////////////////////////////////


            Application.DoEvents();
            Application.DoEvents();







            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            //  readline = (Filereader.ReadLine());






            while ((readline = Filereader.ReadLine()) != null)
            {



                lblstatus.Text = "در حال بار گذاری اطلاعات " + "...............";


                if (progfetchsensors.Value + 2 < 100)
                {

                    progfetchsensors.Value = progfetchsensors.Value + 2;

                }




                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }



                if (readline == "")
                {


                    continue;


                }


                ////// joda kardane reshteha daray , va rikhtan dar yek araye

                ////////////////////////////////////////////////////////////////////





                //<channel index>, <channel name>, <sensorCode>,<sensorUnitCode>, <sensorUnitAbv>, savingType, channelType, hwPortType, hwPortNumber, hwPortPinNumber , calculationType, IsActive







                string s = readline;
                string[] values = s.Split(',');



                ///khandane satre aval baraye yek sensor shamel   etelaate paye 
                String tmpyear, tmpmounth, tmpday, tmphour, tmpmin, tmpsecond;

                String tmpservername, tmpserverIP, tmphostname, tmpserverport;


                //////agar 6 ta bood marboot be  khat tanzimat zamn va saat ast

                txtyear.BackColor = Color.White;
                txtmounth.BackColor = Color.White;
                txtday.BackColor = Color.White;
                txthour.BackColor = Color.White;
                txtmin.BackColor = Color.White;
                txtsec.BackColor = Color.White;
                txtserver.BackColor = Color.White;
                txtIp.BackColor = Color.White;
                txthostname.BackColor = Color.White;
                txtport.BackColor = Color.White;





                if (values.Count() == 7)
                {


                    tmpyear = values[0];

                    tmpmounth = values[1];

                    tmpday = values[2];

                    tmphour = values[3];

                    tmpmin = values[4];

                    tmpsecond = values[5];





                    txtyear.Text = tmpyear;
                    txtmounth.Text = tmpmounth;
                    txtday.Text = tmpday;
                    txthour.Text = tmphour;
                    txtmin.Text = tmpmin;
                    txtsec.Text = tmpsecond;



                }


                //////agar 6 ta bood marboot be  khat tanzimat ip,serevr hosr va port as 



                if (values.Count() == 5)
                {


                    tmpservername = values[0];

                    tmpserverIP = values[1];

                    tmphostname = values[2];

                    tmpserverport = values[3];


                    txtserver.Text = tmpservername;
                    txtIp.Text = tmpserverIP;
                    txthostname.Text = tmphostname;
                    txtport.Text = tmpserverport;




                }





                if (readline == null)
                {


                    break;


                }





                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }




            }/////end op loof reading file









            Filereader.Close();

            progfetchsensors.Value = 100;


            Application.DoEvents();


            // lblstatus.Text = "پایان بارگذاری اطلاعات";

            // tabControl1.SelectedTab = tabPage3;

            //////////////////////set time 

            DateTime theDate = DateTime.UtcNow;


            string customdate = theDate.ToString("d");

            lbltime.Text = DateTime.Now.ToLongTimeString().ToString();

            lbldate.Text = customdate;
            ///////////////////////////






        }










        private void label23_Click(object sender, EventArgs e)
        {


            //   lblstatus.Text = "در حال تنظیم فایل خروجی";
            progfetchsensors.Value = 2;


            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\server.conf");



            string savetime;

            string a = "";
            string b = "";

            string c = "";
            string d = "";

            string ee = "";
            string f = "";



            a = (txtyear.Text);
            b = (txtmounth.Text);
            c = (txtday.Text);
            d = (txthour.Text);
            ee = (txtmin.Text);
            f = (txtsec.Text);

            savetime = a + "," + b + "," + c + "," + d + "," + ee + "," + f + ",";



            string saveserver;


            string g = "";
            string h = "";

            string j = "";
            string k = "";

            g = (txtserver.Text);
            h = (txtIp.Text);
            j = (txthostname.Text);
            k = (txtport.Text);


            saveserver = g + "," + h + "," + j + "," + k + ",";



            filewriter.WriteLine("### new one from site! 7h45");
            //  filewriter.WriteLine("### warning: not any sapce allowed in non-comment lines, it result in <<bad formatted file>> exception");
            filewriter.WriteLine("###");

            filewriter.WriteLine("## RDL system configuration");
            filewriter.WriteLine("#");
            filewriter.WriteLine("#modification date_time");



            //////write data for time
            filewriter.WriteLine(savetime);
            ////////////

            filewriter.WriteLine("##");
            filewriter.WriteLine("#server info :<[servername] or   <<  -  >>  as null>, <[server IP]>, <host name>, <server port>,");


            //////write data for server info
            filewriter.WriteLine(saveserver);
            ////////////



            filewriter.Close();

            progfetchsensors.Value = 100;
            //  lblstatus.Text = "پایان مرحله خروجی فایل";






        }

        private void label33_Click(object sender, EventArgs e)
        {



            //  dlgOpenFile.FileName = "rdlsys_client.conf";
            dlgOpenFile.FileName = "rdlsys_client.conf";




            //    DialogResult resDialog = dlgOpenFile.ShowDialog();

            //rdlsys_client.conf


            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\rdlsys_client.conf";



            //if (resDialog.ToString() == "OK")
            //{


            //    flname = dlgOpenFile.FileName;

            //}



            ///////////////////
            ////////////////////








            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            //  readline = (Filereader.ReadLine());







            readline = Filereader.ReadLine();



            if (readline == null)
            {


                return;



            }





            lblstatus.Text = "در حال بار گذاری اطلاعات " + "...............";


            if (progfetchsensors.Value + 2 < 100)
            {

                progfetchsensors.Value = progfetchsensors.Value + 2;

            }







            ////// joda kardane reshteha daray , va rikhtan dar yek araye

            ////////////////////////////////////////////////////////////////////



            //<channel index>, <channel name>, <sensorCode>,<sensorUnitCode>, <sensorUnitAbv>, savingType, channelType, hwPortType, hwPortNumber, hwPortPinNumber , calculationType, IsActive







            string s = readline;
            string[] values = s.Split(',');



            ///khandane satre aval baraye yek sensor shamel   etelaate paye 
            String tmpyear, tmpmounth, tmpday, tmphour, tmpmin, tmpsecond;

            //  String   tmpA, tmpB, tmpC;



            //////agar 6 ta bood marboot be  khat tanzimat zamn va saat ast

            txtyearsave.BackColor = Color.White;
            txtmounthsave.BackColor = Color.White;
            txtdaysave.BackColor = Color.White;
            txthhoursave.BackColor = Color.White;
            txtminsave.BackColor = Color.White;
            txtsecsave.BackColor = Color.White;
            txtserver.BackColor = Color.White;
            txtIp.BackColor = Color.White;
            txthostname.BackColor = Color.White;
            txtport.BackColor = Color.White;


            tmpyear = values[0];

            tmpmounth = values[1];

            tmpday = values[2];

            tmphour = values[3];

            tmpmin = values[4];

            tmpsecond = values[5];


            txtyearsave.Text = tmpyear;

            txtmounthsave.Text = tmpmounth;
            txtdaysave.Text = tmpday;
            txthhoursave.Text = tmphour;
            txtminsave.Text = tmpmin;
            txtsecsave.Text = tmpsecond;


            s = Filereader.ReadLine();

            values = s.Split(',');
            tmpordinalnumber = values[1];


            ////////////////////////////////line 2


            Filereader.Close();  /// finish reading rdlsys_clintd.conf
            /// 
            //////start reading new files


            flname = Application.StartupPath + "\\configdowmload\\rdlsys_client_sample_save.conf";
            System.IO.StreamReader Filereader2 = new System.IO.StreamReader(flname);
            readline = Filereader2.ReadLine();

            if (readline == null)
            {
                return;
            }


            string s2 = "";

            s2 = readline;
            string[] values_a = s2.Split(',');

            /////////////////////////////////////////







            ////tmp0 = values[0];

            ////tmp2 = values[2];
            ////tmp3 = values[3];
            ////tmp4 = values[4];
            ////tmp5 = values[5];



            tmpA = int.Parse(values_a[0]);

            tmpB = int.Parse(values_a[1]);

            tmpC = int.Parse(values_a[2]);



            txtordinal.Text = tmpordinalnumber;

            int a, b, c, sampling, saving, sending;
            a = (tmpA);
            b = (tmpB);
            c = (tmpC);

            sampling = a * c;//////calculate time sampling
            saving = a * b * c;
            sending = a * c * b * c;

            //////////////baraye mohasebeye a vaghty karbar adad ra vared kard an ra bar 5 taghsim mikonim va da a mirizim , chon c ra fix gereftim.

            ////////////////////////////////////////////tabdile vahed

            string tmpvahed = "sec";


            if (sampling >= 60 && sampling < 3600)
            {


                sampling = sampling / 60;
                saving = saving / 60;
                sending = sending / 60;


                tmpvahed = "min";



            }




            if (sampling >= 3600 && sampling < 86400)
            {


                sampling = sampling / 3600;
                saving = saving / 3600;
                sending = sending / 3600;


                tmpvahed = "hour";



            }



            if (sampling >= 86400)
            {


                sampling = sampling / 86400;
                saving = saving / 86400;
                sending = sending / 86400;


                tmpvahed = "day";



            }












            cmbsampling.Text = sampling.ToString() + tmpvahed;


            cmbsaving.Text = (saving / sampling).ToString();


            //    cmbsending.Text = sending.ToString() + "sec";




            txtsaving.Text = saving.ToString() + tmpvahed;

            txtsending.Text = sending.ToString() + tmpvahed;











            Filereader.Close();

            progfetchsensors.Value = 100;


            Application.DoEvents();


            //  lblstatus.Text = "پایان بارگذاری اطلاعات";

            //  tabControl1.SelectedTab = tabPage4;











        }

        private void label20_Click_1(object sender, EventArgs e)
        {

            //  lblstatus.Text = "در حال تنظیم فایل خروجی";
            progfetchsensors.Value = 2;


            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\rdlsys_client_sample_save.conf");

            string aa = "";
            string bb = "";

            string cc = "";

            string tmpvahed = "";





            aa = (cmbsampling.Text);

            if (aa.IndexOf("min") != -1)
            {
                tmpvahed = "min";

            }


            if (aa.IndexOf("sec") != -1)
            {
                tmpvahed = "sec";

            }


            if (aa.IndexOf("hour") != -1)
            {
                tmpvahed = "hour";

            }


            if (aa.IndexOf("day") != -1)
            {
                tmpvahed = "day";

            }




            aa = aa.Replace(tmpvahed, "");

            int a;
            a = int.Parse(aa);

            //int tmpa,tmpb,tmpc ;

            int vaheddevide = 0;


            if (tmpvahed.Equals("sec") == true)
            {
                a = a * 1;
                vaheddevide = 1;



            }




            if (tmpvahed.Equals("min") == true)
            {
                a = a * 60;
                vaheddevide = 60;


            }



            if (tmpvahed.Equals("hour") == true)
            {
                a = a * 3600;
                vaheddevide = 3600;

            }




            if (tmpvahed.Equals("day") == true)
            {
                a = a * 86400;
                vaheddevide = 86400;

            }



            tmpA = (a / 5);
            tmpC = 5;




            int sampling = a * int.Parse(cmbsaving.Text.ToString());


            tmpB = (sampling / a);










            string savetime;

            //  string a = "";
            string b = "";

            string c = "";
            string d = "";

            string ee = "";
            string f = "";



            //////a = (txtyearsave.Text);
            //////b = (txtmounthsave.Text);
            //////c = (txtdaysave.Text);
            //////d = (txthhoursave.Text);
            //////ee = (txtminsave.Text);
            //////f = (txtsecsave.Text);

            //////savetime = a + "," + b + "," + c + "," + d + "," + ee + "," + f;



            string dta;





            aa = (cmbsampling.Text);
            aa = aa.Replace("sec", "");

            bb = (cmbsaving.Text);
            bb = bb.Replace("sec", "");

            cc = (cmbsending.Text);
            cc = cc.Replace("sec", "");





            //  dta = tmp0 + "," + tmpordinalnumber + "," + tmp2 + "," + tmp3 + "," + tmp4 + ","  + tmp5 + ","+ tmpA + "," + tmpB + "," + tmpC;

            dta = tmpA + "," + tmpB + "," + tmpC;






            //////write data for time
            //    filewriter.WriteLine(savetime);
            ////////////


            //////write data for server info
            filewriter.WriteLine(dta);
            ////////////



            filewriter.Close();

            progfetchsensors.Value = 100;
            // lblstatus.Text = "پایان مرحله خروجی فایل";



        }

        private void cmbsampling_SelectedIndexChanged(object sender, EventArgs e)
        {



            cmbsaving.Items.Clear();
            //cmbsaving.Items.Add("2");
            cmbsaving.Items.Add("3");
            //cmbsaving.Items.Add("4");
            cmbsaving.Items.Add("5");
            //cmbsaving.Items.Add("6");
            //cmbsaving.Items.Add("7");
            //cmbsaving.Items.Add("8");
            //cmbsaving.Items.Add("9");
            cmbsaving.Items.Add("10");
            cmbsaving.Items.Add("15");
            cmbsaving.Items.Add("20");
            // cmbsaving.Items.Add("25");
            cmbsaving.Items.Add("30");
            cmbsaving.Items.Add("60");
            cmbsaving.Items.Add("120");
            //cmbsaving.Items.Add("40");
            //cmbsaving.Items.Add("50");
            ////cmbsaving.Items.Add("10");
            ////cmbsaving.Items.Add("10");
            ////cmbsaving.Items.Add("10");

            // cmbsaving.SelectedItem  = 0;





        }

        private void label47_Click(object sender, EventArgs e)
        {

            dtgridrain.Rows.Clear();
            dtgridrain.Columns.Clear();


            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\rain.conf";



            //if (resDialog.ToString() == "OK")
            //{


            //    flname = dlgOpenFile.FileName;

            //}



            ///////////////////
            ////////////////////








            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            //  readline = (Filereader.ReadLine());



            DataGridViewTextBoxColumn textboxcolumn = new DataGridViewTextBoxColumn();
            TextBox txt = new TextBox();
            textboxcolumn.Width = 150;
            dtgridrain.Columns.Add(textboxcolumn);



            DataGridViewTextBoxColumn textboxcolumn2 = new DataGridViewTextBoxColumn();
            TextBox txt2 = new TextBox();
            textboxcolumn2.Width = 150;
            dtgridrain.Columns.Add(textboxcolumn2);





            //dtgridrain.Columns[0].HeaderText = "مقدار ورودی";
            //dtgridrain.Columns[1].HeaderText = "ضریب اصلاحی";

            dtgridrain.Columns[0].HeaderText = "فاصله زمانی پالس";
            dtgridrain.Columns[1].HeaderText = "میزان اصلاح شده بابت آب دزدی";

            while ((readline = Filereader.ReadLine()) != null)
            {




                if (readline == null)
                {


                    break;



                }




                if (readline.Equals("") == true)
                {


                    break;



                }



                lblstatus.Text = "در حال بار گذاری اطلاعات " + "...............";


                if (progfetchsensors.Value + 2 < 100)
                {

                    progfetchsensors.Value = progfetchsensors.Value + 2;

                }



                string s = readline, leftvalue = "", rightvalue = "";
                string[] values = s.Split(' ');

                try
                {




                    leftvalue = values[0];

                    rightvalue = s.Replace(leftvalue, "");
                    rightvalue = rightvalue.Replace(" ", "");






                }


                catch
                {

                }



                dtgridrain.Rows.Add(new object[] { leftvalue, rightvalue });





            }






            Filereader.Close();

            progfetchsensors.Value = 100;


            Application.DoEvents();


            //  lblstatus.Text = "پایان بارگذاری اطلاعات";

            //    tabControl1.SelectedTab = tabPage5;







        }

        private void label48_Click(object sender, EventArgs e)
        {

            //  lblstatus.Text = "در حال تنظیم فایل خروجی";
            progfetchsensors.Value = 2;


            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\rain.conf");



            string leftvalue, rightvalue, savedata;







            for (int i = 0; i < dtgridrain.RowCount - 1; i++)
            {






                leftvalue = dtgridrain.Rows[i].Cells[0].Value.ToString();

                rightvalue = dtgridrain.Rows[i].Cells[1].Value.ToString(); ;

                savedata = leftvalue + "  " + rightvalue;




                filewriter.WriteLine(savedata);



            }






            filewriter.Close();

            progfetchsensors.Value = 100;
            //   lblstatus.Text = "پایان مرحله خروجی فایل";







        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {



            myconn.Close();


            myconn.Open();





            OleDbDataAdapter da = new OleDbDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();









            da.SelectCommand = new OleDbCommand("select * from tbltempCalibration  where namesensorEn='" + cmbshowsensors.Text + "'  order by numm", myconn);
            da.Fill(ds, "tbltempCalibration");
            dt = ds.Tables["tbltempCalibration"];







            /////for datagrid view

            DataView dtaview = new DataView();
            dtaview.Table = ds.Tables["tbltempCalibration"];


            datagridloadtempcalibrationdata.DataSource = ds; // dataset
            datagridloadtempcalibrationdata.DataMember = "tbltempCalibration";


            myconn.Close();



            datagridloadtempcalibrationdata.Columns[0].HeaderText = "نام سنسور";

            datagridloadtempcalibrationdata.Columns[1].HeaderText = "شماره ردیف";

            datagridloadtempcalibrationdata.Columns[2].HeaderText = "مقدار فیزیکی";



            datagridloadtempcalibrationdata.Columns[3].HeaderText = "مقدار پارامتر";

            datagridloadtempcalibrationdata.Columns[4].HeaderText = " نام اختصاری کانال ";

            //   datagridloadtempcalibrationdata.Columns[4].ReadOnly = true;



        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {




            //  datagridloadtempcalibrationdata.LostFocus();
            cmbshowsensors.Focus();




            myconn.Close();
            myconn.Open();


            //////////////////////  dar ebteda etelaate canal ra az db hazf mikonim

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            OleDbDataAdapter da3 = new OleDbDataAdapter();
            da3.SelectCommand = new OleDbCommand("select * from tbltempCalibration where namesensorEn='" + cmbshowsensors.Text + "' ", myconn);
            da3.Fill(ds, "tbltempCalibration");
            dt = ds.Tables["tbltempCalibration"];

            for (int p = 0; p < dt.Rows.Count; p++)
            {

                dt.Rows[p].Delete();


            }


            da3.DeleteCommand = new OleDbCommand("delete  from tbltempCalibration  where namesensorEn='" + cmbshowsensors.Text + "'", myconn);



            try
            {
                da3.Update(dt);
            }
            catch (Exception ex)
            {
                // throw ex;
            }


            ////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////////////now add new information of grid to db//////////////


            Application.DoEvents();



            myconn.Close();
            myconn.Open();


            Application.DoEvents();


            DataSet oDS1 = new DataSet();


            OleDbDataAdapter da1 = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM tbltempCalibration", myconn));

            OleDbCommandBuilder oOrdersCmdBuilder1 = new OleDbCommandBuilder(da1);
            da1.FillSchema(oDS1, SchemaType.Source);

            DataTable pTable1 = oDS1.Tables["Table"];
            pTable1.TableName = "tbltempCalibration";



            for (int i = 0; i < datagridloadtempcalibrationdata.RowCount - 1; i++)
            {


                DataRow oOrderRow1 = oDS1.Tables["tbltempCalibration"].NewRow();

                oOrderRow1["channel_name_persian"] = datagridloadtempcalibrationdata.Rows[i].Cells[0].Value.ToString();

                oOrderRow1["numm"] = datagridloadtempcalibrationdata.Rows[i].Cells[1].Value.ToString(); ;

                oOrderRow1["valuee"] = int.Parse(datagridloadtempcalibrationdata.Rows[i].Cells[2].Value.ToString());     ////add persian to show users

                oOrderRow1["calibrationValue"] = Double.Parse(datagridloadtempcalibrationdata.Rows[i].Cells[3].Value.ToString());

                oOrderRow1["namesensorEn"] = datagridloadtempcalibrationdata.Rows[i].Cells[4].Value.ToString(); ;


                oDS1.Tables["tbltempCalibration"].Rows.Add(oOrderRow1);

                da1.Update(oDS1, "tbltempCalibration");

            }


            myconn.Close();













        }

        //    private void datagridloadtempdata_DoubleClick(object sender, DataGridViewCellEventArgs  e)
        private void datagridloadtempdata_DoubleClick(object sender, EventArgs e)
        {

            //EventArgs





            int i = datagridloadtempdata.CurrentRow.Index;








            string namesensor = "";

            namesensor = datagridloadtempdata.Rows[i].Cells[2].Value.ToString();

            cmbshowsensors.Text = namesensor;

            tabControl1.SelectedTab = tabPage2;









        }

        private void lblgetfile_Click(object sender, EventArgs e)
        {




            if (listboxFiles.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }




            for (var i = 0; i < listboxFiles.Items.Count; i++)
            {
                ftpFilename = listboxFiles.Items[i].ToString();



                //                if (ftpFilename.Equals("rain.conf") == true || ftpFilename.Equals("rdlsys_channels.conf") == true || ftpFilename.Equals("rdlsys_client.conf") == true || ftpFilename.Equals("rdlsys_client.info") == true || ftpFilename.Equals("server.conf") == true || ftpFilename.Equals("software.info") == true)

                //               {



                string filename;



                filename = ftpFilename;

                // username = "";
                //  password = "";


                downloadFile(FTPAddress, filename, ftpusername, ftppassword);

                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();



                lblsave_Click(null, null);

                Application.DoEvents();



                //             }





            }




        }

        private void lblsave_Click(object sender, EventArgs e)
        {


            if (downloadedData != null && downloadedData.Length != 0)
            {



                // this.Text = "Saving Data...";
                Application.DoEvents();

                //Write the bytes to a file
                FileStream newFile = new FileStream(Application.StartupPath + "\\configdowmload\\" + ftpFilename, FileMode.Create);
                FileStream newFile2 = new FileStream(Application.StartupPath + "\\configExport\\" + ftpFilename, FileMode.Create);


                newFile.Write(downloadedData, 0, downloadedData.Length);
                newFile.Close();



                newFile2.Write(downloadedData, 0, downloadedData.Length);
                newFile.Close();

                //  lblstatus.Text = "پایان ذخیره فایل";
                //  MessageBox.Show("Saved Successfully");



            }


        }

        private void lblsendftp_Click(object sender, EventArgs e)
        {




            if (listboxFiles.Items.Count == 0)
            {



                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;




            }



            string filename, filePath, FTPAddresssend;




            FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//config//";



            prgstatusftbdownload.Value = 0;

            for (var i = 0; i < listboxFiles.Items.Count; i++)
            {





                if (prgstatusftbdownload.Value + 10 < 100)
                {
                    prgstatusftbdownload.Value = prgstatusftbdownload.Value + 10;

                }



                filename = listboxFiles.Items[i].ToString();

                if (glb_stop_rdl == false)
                {

                    if (filename.Equals("Rain_Total.conf") == true)
                    {

                        continue;
                    }


                }



                ////if (glb_stop_rdl == true)
                ////{

                ////    glb_stop_rdl = false;
                ////}



                filePath = Application.StartupPath + "\\configExport\\";



                ftpusername = "root";
                ftppassword = "password";



                //Create FTP request
                //  FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpusername, ftppassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                //Load the file


                FileStream stream = File.OpenRead(filePath + filename);
                byte[] buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                //Upload file
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();



                //  MessageBox.Show("Uploaded Successfully");

                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();



            }



            lblstatusftp.Text = "پایان ارسال فایل";
            lblstatus.Text = "پایان ارسال تنظیمات به دستگاه";
            prgstatusftbdownload.Value = prgstatusftbdownload.Maximum;
            glb_stop_rdl = false;
            // progfetchsensors.Value = prgstatusftbdownload.Maximum;



        }

        private void lblstatusftp_Click(object sender, EventArgs e)
        {

            //System.exe

            //////System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //////proc.EnableRaisingEvents = false;
            //////proc.StartInfo.FileName = "ftp://192.168.1.21//" + "//convert.exe";

        }

        private void label19_Click(object sender, EventArgs e)
        {

            //////if (glbguid == false)
            //////{

            //////    MessageBox.Show("شما مجاز به استفاده از نرم افزار نیستید");
            //////    return;

            //////}


            //    tabcontrol2.Enabled = false;


            pnlhelp.Visible = false;
            pnlCalibration.Visible = false;
            pnlSensors.Visible = false;
            pnlweb.Visible = false;
            //   pnlshowdata.Visible = false;


            pnlhashcode.Visible = false;
            pnl_show_digital_rain.Visible = false;
            pnldatatransfer.Visible = false;
            pnlloaddata.Visible = false;

            pnlshowdata.Visible = true;


        }

        private void pictureBox4_Click_2(object sender, EventArgs e)
        {









        }

        private void label51_Click(object sender, EventArgs e)
        {


            string username, password;





            FTPAddress = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//config//";

            //  FTPAddress = "ftp://root:password@192.168.1.222//";


            ftpusername = "root";
            ftppassword = "password";



            getFileList(FTPAddress, ftpusername, ftppassword);




            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();






            if (listboxFiles.Items.Count == 0)
            {


                lblstatusftp.Text = "";

                return;


            }





            lblgetfile_Click(null, null);


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();



            lbltestguid_Click(null, null);

            pictureBox3_Click_1(null, null);

            lblstatusftp.Text = "اتصال به سیستم با موفقیت انجام شد";
            Application.DoEvents();
            lblstatus.Text = "";

            picconnected.Visible = true;
            Application.DoEvents();
            picdisconnected.Visible = false;


            System.Threading.Thread.Sleep(2000);

            pnldatatransfer.Visible = false;





        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            lblname_station.Visible = false;
            label85.Visible = false;
            picsaveexcelllogsms.Visible = false;
            txtnamestation.Visible = false;

            if (cmbnamestation.Text != "")
            {

                glbguidID = glbguidID_zap;
                cmbnamestation.Text = "";



            }

            listboxFiles3.Items.Clear();
            //dtgshowdataonline.Rows.Clear();

            //dtgshowdataonline.Columns.Clear();




            //////MessageBox.Show("کلیک");
            //////return;



            if (tabcontrol2.SelectedTab == tabPage8)
            {

                tmrfetchdata.Enabled = false;

                cmbfilelog.Items.Clear();
                cmbfilelogTo.Items.Clear();

                ///load maghadire config channel name 




                //// download saved data from bord

                string username, password;

                if (glbguidID == null)
                {

                    MessageBox.Show("لطفا مجددا به سیستم متصل شوید");
                    return;


                }





                FTPAddressdownloaddata = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//datalog//0//";



                ftpusername = "root";
                ftppassword = "password";


                getFileList3(FTPAddressdownloaddata, ftpusername, ftppassword);


                Application.DoEvents();

                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();


                string filename = "";

                //  downloadFileDateTimeStamp(FTPAddressdownloaddata, filename, ftpusername, ftppassword);

                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();






                if (listboxFiles3.Items.Count == 0)
                {

                    MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                    return;


                }


                lblstatusftp2.Text = "پایان دریافت اطلاعات سیستم ";
                Application.DoEvents();
                Application.DoEvents();



                label57_Click(null, null);






                return;

            }











            // string username, password;





            FTPAddressdownloaddata = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//applog//";



            ftpusername = "root";
            ftppassword = "password";


            getFileList2(FTPAddressdownloaddata, ftpusername, ftppassword);


            Application.DoEvents();

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();




            if (listboxFiles2.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }


            if (glbguid == false)
            {

                MessageBox.Show("شما مجاز به استفاده از نرم افزار نیستید");
                Application.DoEvents();
                //   tabcontrol2.Focus();

                return;



            }




            lbldownloaddata_Click(null, null);


            Application.DoEvents();

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();




            /////namayesh  data da grid
            //   rdinputsignals.Checked =true ;





            ////if (show_time_log == true)
            ////{
            ////    dtgshowdataonline.Visible = false;
            ////}
            ////else
            ////{
            ////    dtgshowdataonline.Visible = true;
            ////}




            if (get_log_sms == true)
            {
                showdownloadd_log_sms();
            }
            else
            {



                showdownloaddata();



            }



            /////namayesh  data da grid









        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

            pnlshowdata.Visible = false;
            chkautomated.Checked = false;

        }

        private void lbldownloaddata_Click(object sender, EventArgs e)
        {




            if (listboxFiles2.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }




            for (var i = 0; i < listboxFiles2.Items.Count; i++)
            {
                ftpFilename = listboxFiles2.Items[i].ToString();



                if (get_log_sms == true)
                {


                    if (ftpFilename.Equals("alarm_logfile.log") == true)
                    {



                        string filename;



                        filename = ftpFilename;




                        downloadFiledata(FTPAddressdownloaddata, filename, ftpusername, ftppassword);

                        Application.DoEvents();
                        Application.DoEvents();
                        Application.DoEvents();



                        lblsavedata_Click(null, null);

                        Application.DoEvents();




                    }




                }



                else
                {


                    if (ftpFilename.Equals("rdlsys_rawvalue.log") == true || ftpFilename.Equals("rdlsys_instantvalue.log") == true || ftpFilename.Equals("rdlsys_calculatedvalue.log") == true)
                    {



                        string filename;



                        filename = ftpFilename;




                        downloadFiledata(FTPAddressdownloaddata, filename, ftpusername, ftppassword);

                        Application.DoEvents();
                        Application.DoEvents();
                        Application.DoEvents();



                        lblsavedata_Click(null, null);

                        Application.DoEvents();




                    }





                }












            }





        }



        private void lblsavedatadatalog_Click(object sender, EventArgs e)
        {

            if (downloadedDatadata != null && downloadedDatadata.Length != 0)
            {



                // this.Text = "Saving Data...";
                Application.DoEvents();

                //Write the bytes to a file
                FileStream newFile = new FileStream(Application.StartupPath + "\\datalog\\" + glbguidID + "\\" + ftpFilename, FileMode.Create);



                newFile.Write(downloadedDatadata, 0, downloadedDatadata.Length);
                newFile.Close();



                lblstatusftp2.Text = "پایان دریافت اطلاعات";
                //  MessageBox.Show("Saved Successfully");



            }


        }







        private void lblsavedata_Click(object sender, EventArgs e)
        {

            if (downloadedDatadata != null && downloadedDatadata.Length != 0)
            {



                // this.Text = "Saving Data...";
                Application.DoEvents();

                //Write the bytes to a file
                FileStream newFile = new FileStream(Application.StartupPath + "\\configdowmload\\" + ftpFilename, FileMode.Create);



                newFile.Write(downloadedDatadata, 0, downloadedDatadata.Length);
                newFile.Close();



                lblstatusftp2.Text = "پایان دریافت اطلاعات";
                //  MessageBox.Show("Saved Successfully");



            }


        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

            tabcontrol2.SelectedTab = tabPage7;

            showdownloaddata();


        }












        public void showdownloaddata()
        {




            ////if (glbguid == false)
            ////{

            ////    MessageBox.Show("شما مجاز به استفاده از نرم افزار نیستید");
            ////    Application.DoEvents();
            ////    // tabcontrol2.Focus();

            ////    return;



            ////}



            string flname = "";
            string readline = "";



            if (rdinputsignals.Checked == true) flname = Application.StartupPath + "\\configdowmload\\rdlsys_rawvalue.log";

            if (rdinstant.Checked == true) flname = Application.StartupPath + "\\configdowmload\\rdlsys_instantvalue.log";

            if (rdcalculated.Checked == true) flname = Application.StartupPath + "\\configdowmload\\rdlsys_calculatedvalue.log";






            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            //  readline = (Filereader.ReadLine());


            dtgshowdataonline.Rows.Clear();

            dtgshowdataonline.Columns.Clear();




            DataGridViewTextBoxColumn textboxcolumn = new DataGridViewTextBoxColumn();
            TextBox txt = new TextBox();
            // textboxcolumn.Width = 150;
            textboxcolumn.Width = 300;
            textboxcolumn.DefaultCellStyle.Font = new Font("Arial", 14F, GraphicsUnit.Pixel);

            if (show_time_log == false)
            {
                dtgshowdataonline.Columns.Add(textboxcolumn);
            }


            DataGridViewTextBoxColumn textboxcolumn2 = new DataGridViewTextBoxColumn();
            TextBox txt2 = new TextBox();
            //   textboxcolumn2.Width = 150;
            textboxcolumn2.Width = 150;
            textboxcolumn2.DefaultCellStyle.Font = new Font("Arial", 14F, GraphicsUnit.Pixel);
            textboxcolumn2.DefaultCellStyle.ForeColor = Color.Red;

            if (show_time_log == false)
            {
                dtgshowdataonline.Columns.Add(textboxcolumn2);
            }

            DataGridViewTextBoxColumn textboxcolumn3 = new DataGridViewTextBoxColumn();
            TextBox txt3 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn3.Width = 150;

            if (show_time_log == false)
            {
                dtgshowdataonline.Columns.Add(textboxcolumn3);
            }



            if (show_time_log == false)
            {
                dtgshowdataonline.Columns[0].HeaderText = "نام کانال";
                dtgshowdataonline.Columns[1].HeaderText = "مقدار ورودی";
                dtgshowdataonline.Columns[2].HeaderText = "واحد";

                dtgshowdataonline.Columns[0].ReadOnly = true;
                dtgshowdataonline.Columns[1].ReadOnly = true;
                dtgshowdataonline.Columns[2].ReadOnly = true;
            }


            int tmpnumadded = 0;

            while ((readline = Filereader.ReadLine()) != null)
            {




                if (readline == null)
                {


                    break;



                }




                if (readline.Equals("") == true)
                {


                    continue;



                }







                string s = readline, leftvalue = "", rightvalue = "", vahed = "";
                string[] values = s.Split(':');

                try
                {



                    if (rdinstant.Checked == true && tmpnumadded >= 2)//// baraye khanadane time adde to file instantvalue;
                    {


                        if (tmpnumadded == 2)
                        {
                            string tmp_date = s.ToString();
                            //  lblshowdate_realTime.Text = tmp_date;
                            string[] str_temp = tmp_date.Split('-');
                            string year_tmp, mount_tmp, day_tmp;
                            year_tmp = str_temp[0];
                            mount_tmp = str_temp[1];
                            day_tmp = str_temp[2];
                            day_tmp = day_tmp.Substring(0, 2);

                            string tempdate = mount_tmp + "/" + day_tmp + "/" + year_tmp;

                            //       DateTime _date = DateTime.Parse(tempdate, "yyyy-MM-dd");

                            DateTime _date = DateTime.Parse(tempdate);

                            string Shamsi = "";

                            DateTime miladi = DateTime.Now;
                            miladi = _date;

                            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
                            Shamsi = string.Format("{0}/{1}/{2}", shamsi.GetYear(miladi), shamsi.GetMonth(miladi), shamsi.GetDayOfMonth(miladi));

                            string[] pWords = Shamsi.Split(new char[] { '/' });

                            string year, mounth, day;
                            year = pWords[0];
                            mounth = pWords[1];
                            day = pWords[2];


                            if (int.Parse(day) < 10)
                            {
                                day = "0" + day;

                            }




                            if (int.Parse(mounth) < 10)
                            {
                                mounth = "0" + mounth;

                            }




                            Shamsi = year + "/" + mounth + "/" + day;





                            lblshowdate_realTime.Text = Shamsi.ToString();
                            Application.DoEvents();
                            lbltime2.Text = lblshowdate_realTime.Text;
                            tmpnumadded = tmpnumadded + 1;
                            continue;

                        }

                        if (tmpnumadded == 3)
                        {


                            string tmp_date = s.ToString();
                            //  lblshowdate_realTime.Text = tmp_date;
                            string[] str_temp = tmp_date.Split(':');
                            string hour_tmp, min_tmp, sec_tmp;
                            hour_tmp = str_temp[0];
                            min_tmp = str_temp[1];
                            sec_tmp = str_temp[2];
                            sec_tmp = sec_tmp.Substring(0, 2);
                            lblshowtime_realTime.Text = hour_tmp + "-" + min_tmp + "-" + sec_tmp;
                            Application.DoEvents();
                            lbldate2.Text = lblshowtime_realTime.Text;
                            Filereader.Close();
                            return;



                        }


                        // 
                    }



                    leftvalue = values[0];

                    rightvalue = s.Replace(leftvalue, "");
                    rightvalue = rightvalue.Replace(":", "");


                    if (rightvalue.IndexOf("volt") != -1)
                    {
                        vahed = "volt";
                        rightvalue = rightvalue.Replace("volt", "");




                        glb_BATA_instant = rightvalue;
                        //                        batt_logger.Value = 80;



                        if (rdinstant.Checked == true)
                        {

                            glb_BATA_show_gauge = rightvalue;

                        }



                    }



                    if (rightvalue.IndexOf("mm") != -1)
                    {
                        vahed = "mm";
                        rightvalue = rightvalue.Replace("mm", "");

                        glb_RAN_TOT_instant = rightvalue;


                        if (rdinstant.Checked == true)
                        {
                            lblrain_total.Text = glb_RAN_TOT_instant;
                        }
                        //lblrain_24.Text = "";
                        //lblrain_12.Text = "";
                        //lblrain_avg.Text = "";






                    }


                    if (rightvalue.IndexOf("HZ") != -1)
                    {
                        vahed = "HZ";
                        rightvalue = rightvalue.Replace("HZ", "");

                    }


                }


                catch
                {

                }


                //   if leftvalue
                //if (show_time_log == true)
                //{
                //    dtgshowdataonline.Visible = false;
                //}
                //else
                //{
                //    dtgshowdataonline.Visible = true;
                //}





                tmpnumadded = tmpnumadded + 1;


                if (tmpnumadded >= 24 && rdcalculated.Checked == true)
                {

                    // if 

                    // rightvalue = "00.0"+""+"volt";

                    if (show_time_log == false)
                    {


                        dtgshowdataonline.Rows.Add(new object[] { leftvalue, rightvalue, vahed });

                        //public string glb_BATA="",glb_RAN_TOT="",glb_RAN_24="",glb_RAN_12="",RAN_5M="";


                        string compare = leftvalue.Trim();


                        if (compare.Equals("BATA") == true) glb_BATA = rightvalue; //batt_logger.Value = 80;



                        // if (compare.Equals("RAN_TOT") == true) glb_RAN_TOT = rightvalue; lblrain_total.Text = glb_RAN_TOT;
                        //  if (compare.Equals("RAN_TOT") == true) glb_RAN_TOT = rightvalue; lblrain_total.Text = glb_RAN_TOT_instant;
                        if (compare.Equals("RAN_24") == true) glb_RAN_24 = rightvalue; lblrain_24.Text = glb_RAN_24;
                        if (compare.Equals("RAN_12") == true) glb_RAN_12 = rightvalue; lblrain_12.Text = glb_RAN_12;
                        if (compare.Equals("RAN_5M") == true) glb_RAN_5M = rightvalue; lblrain_avg.Text = glb_RAN_5M;





                    }


                }


                if (rdinstant.Checked == true)
                {

                    if (show_time_log == false)
                    {
                        dtgshowdataonline.Rows.Add(new object[] { leftvalue, rightvalue, vahed });
                    }
                }



                if (tmpnumadded == 21 && rdinputsignals.Checked == true)
                {

                    if (show_time_log == false)
                    {

                        vahed = "HZ";
                        dtgshowdataonline.Rows.Add(new object[] { leftvalue, rightvalue, vahed });

                    }
                }




            }


            Filereader.Close();



        }







        public void showdownloadd_log_sms()
        {

            string flname = "";
            string readline = "";


            picsaveexcelllogsms.Visible = true;
            flname = Application.StartupPath + "\\configdowmload\\alarm_logfile.log";




            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            //  readline = (Filereader.ReadLine());


            dtgshowdataonline.Rows.Clear();

            dtgshowdataonline.Columns.Clear();




            DataGridViewTextBoxColumn textboxcolumn = new DataGridViewTextBoxColumn();
            TextBox txt = new TextBox();
            // textboxcolumn.Width = 150;
            textboxcolumn.Width = 50;
            //textboxcolumn.DefaultCellStyle.Font = new Font("Arial", 14F, GraphicsUnit.Pixel);
            dtgshowdataonline.Columns.Add(textboxcolumn);



            DataGridViewTextBoxColumn textboxcolumn2 = new DataGridViewTextBoxColumn();
            TextBox txt2 = new TextBox();
            //   textboxcolumn2.Width = 150;
            textboxcolumn2.Width = 100;
            //textboxcolumn2.DefaultCellStyle.Font = new Font("Arial", 14F, GraphicsUnit.Pixel);
            //textboxcolumn2.DefaultCellStyle.ForeColor = Color.Red;

            dtgshowdataonline.Columns.Add(textboxcolumn2);


            DataGridViewTextBoxColumn textboxcolumn3 = new DataGridViewTextBoxColumn();
            TextBox txt3 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn3.Width = 50;
            dtgshowdataonline.Columns.Add(textboxcolumn3);


            DataGridViewTextBoxColumn textboxcolumn4 = new DataGridViewTextBoxColumn();
            TextBox txt4 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn4.Width = 150;
            dtgshowdataonline.Columns.Add(textboxcolumn4);


            DataGridViewTextBoxColumn textboxcolumn5 = new DataGridViewTextBoxColumn();
            TextBox txt5 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn5.Width = 300;
            dtgshowdataonline.Columns.Add(textboxcolumn5);


            DataGridViewTextBoxColumn textboxcolumn6 = new DataGridViewTextBoxColumn();
            TextBox txt6 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn6.Width = 151;
            dtgshowdataonline.Columns.Add(textboxcolumn6);



            DataGridViewTextBoxColumn textboxcolumn7 = new DataGridViewTextBoxColumn();
            TextBox txt7 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn7.Width = 100;
            dtgshowdataonline.Columns.Add(textboxcolumn7);


            DataGridViewTextBoxColumn textboxcolumn8 = new DataGridViewTextBoxColumn();
            TextBox txt8 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn8.Width = 100;
            dtgshowdataonline.Columns.Add(textboxcolumn8);


            DataGridViewTextBoxColumn textboxcolumn9 = new DataGridViewTextBoxColumn();
            TextBox txt9 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn9.Width = 100;
            dtgshowdataonline.Columns.Add(textboxcolumn9);


            DataGridViewTextBoxColumn textboxcolumn10 = new DataGridViewTextBoxColumn();
            TextBox txt10 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn10.Width = 100;
            dtgshowdataonline.Columns.Add(textboxcolumn10);



            DataGridViewTextBoxColumn textboxcolumn11 = new DataGridViewTextBoxColumn();
            TextBox txt11 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn11.Width = 100;
            dtgshowdataonline.Columns.Add(textboxcolumn11);



            DataGridViewTextBoxColumn textboxcolumn12 = new DataGridViewTextBoxColumn();
            TextBox txt12 = new TextBox();
            //   textboxcolumn3.Width = 50;
            textboxcolumn12.Width = 100;
            dtgshowdataonline.Columns.Add(textboxcolumn12);




            ////DataGridViewTextBoxColumn textboxcolumn11 = new DataGridViewTextBoxColumn();
            ////TextBox txt11 = new TextBox();
            //////   textboxcolumn3.Width = 50;
            ////textboxcolumn11.Width = 156;
            ////dtgshowdataonline.Columns.Add(textboxcolumn11);


            ////DataGridViewTextBoxColumn textboxcolumn12 = new DataGridViewTextBoxColumn();
            ////TextBox txt412 = new TextBox();
            //////   textboxcolumn3.Width = 50;
            ////textboxcolumn12.Width = 157;
            ////dtgshowdataonline.Columns.Add(textboxcolumn12);


            ////DataGridViewTextBoxColumn textboxcolumn13 = new DataGridViewTextBoxColumn();
            ////TextBox txt13 = new TextBox();
            //////   textboxcolumn3.Width = 50;
            ////textboxcolumn13.Width = 158;
            ////dtgshowdataonline.Columns.Add(textboxcolumn13);

            ////DataGridViewTextBoxColumn textboxcolumn14 = new DataGridViewTextBoxColumn();
            ////TextBox txt14 = new TextBox();
            //////   textboxcolumn3.Width = 50;
            ////textboxcolumn14.Width = 159;
            ////dtgshowdataonline.Columns.Add(textboxcolumn14);




            dtgshowdataonline.Columns[0].HeaderText = "Index";
            dtgshowdataonline.Columns[1].HeaderText = "Date";
            dtgshowdataonline.Columns[2].HeaderText = "Time";
            dtgshowdataonline.Columns[3].HeaderText = "Alarm Type";
            dtgshowdataonline.Columns[4].HeaderText = "Text";
            dtgshowdataonline.Columns[5].HeaderText = "Station Name";
            dtgshowdataonline.Columns[6].HeaderText = "Start Time";
            dtgshowdataonline.Columns[7].HeaderText = "Stop time";
            dtgshowdataonline.Columns[8].HeaderText = "Time";
            dtgshowdataonline.Columns[9].HeaderText = "Rain Total-mm";
            dtgshowdataonline.Columns[10].HeaderText = "Rain 12 hours-mm";
            dtgshowdataonline.Columns[11].HeaderText = "Rain 24 hours-mm";

            ////dtgshowdataonline.Columns[11].HeaderText = "f10";
            ////dtgshowdataonline.Columns[12].HeaderText = "f11";
            ////dtgshowdataonline.Columns[13].HeaderText = "f12";







            dtgshowdataonline.Columns[0].ReadOnly = true;
            dtgshowdataonline.Columns[1].ReadOnly = true;
            dtgshowdataonline.Columns[2].ReadOnly = true;
            dtgshowdataonline.Columns[3].ReadOnly = true;
            dtgshowdataonline.Columns[4].ReadOnly = true;
            dtgshowdataonline.Columns[5].ReadOnly = true;
            dtgshowdataonline.Columns[6].ReadOnly = true;
            dtgshowdataonline.Columns[7].ReadOnly = true;
            dtgshowdataonline.Columns[8].ReadOnly = true;
            dtgshowdataonline.Columns[9].ReadOnly = true;
            dtgshowdataonline.Columns[10].ReadOnly = true;
            dtgshowdataonline.Columns[11].ReadOnly = true;

            ////dtgshowdataonline.Columns[11].ReadOnly = true;

            ////dtgshowdataonline.Columns[12].ReadOnly = true;
            ////dtgshowdataonline.Columns[13].ReadOnly = true;





            int tmpnumadded = 0;
            string tmpstring = "";



            while ((readline = Filereader.ReadLine()) != null)
            {


                tmpstring = tmpstring + readline;

                if (readline == null)
                {


                    break;



                }




                if (readline.Equals("") == true)
                {


                    continue;



                }



                if (readline.Equals("##################################################") != true)
                {


                    continue;



                }






                string s = tmpstring, index = "", tmpdate = "", tmptime = "", tmpmatn = "";
                string[] values = s.Split('*');
                tmpstring = "";
                string tmpshowdate_shamsi = "";
                string[] values_matn;
                string f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11;
                f1 = ""; f2 = ""; f3 = ""; f4 = ""; f5 = ""; f6 = ""; f7 = ""; f8 = ""; f9 = ""; f10 = ""; f11 = "";
                string color_code = "";

                try
                {






                    tmpdate = values[0];
                    tmptime = values[1];
                    tmpmatn = values[2];

                    values_matn = tmpmatn.Split('+');




                    if (tmpmatn.IndexOf("&1") != -1)
                    {

                        // f1 = "شروع بارش باران";
                        f1 = "Rain Start";

                        color_code = "1";
                        // f9 = color_code;


                    }


                    if (tmpmatn.IndexOf("&2") != -1)
                    {

                        //  f1 = "رگبار بلند مدت";
                        f1 = "Alarm:Long Term Shower";

                        color_code = "2";
                        //  f9=color_code;

                    }

                    if (tmpmatn.IndexOf("&3") != -1)
                    {

                        //  f1 = "رگبار 5 دقیقه";
                        f1 = "Alarm:Avereage in 5 minute";

                        color_code = "3";
                        //  f9=color_code;

                    }


                    if (tmpmatn.IndexOf("&4") != -1)
                    {

                        //    f1 = "پایان بارش باران";
                        f1 = "Rain Stop";
                        color_code = "4";
                        //  f9=color_code;


                    }




                    if (values_matn.Length > 6)
                    {

                        f2 = values_matn[0];
                        f3 = values_matn[1];
                        f4 = values_matn[2];
                        f5 = "";
                        f6 = "";



                        f7 = values_matn[3];
                        f8 = values_matn[4];
                        f9 = values_matn[5];
                        f10 = values_matn[6];


                        if (color_code.Equals("4") == true)
                        {



                            f2 = values_matn[0];
                            f3 = values_matn[1];
                            f4 = values_matn[2];
                            f5 = values_matn[3];
                            f6 = values_matn[4];



                            f7 = "";
                            f8 = values_matn[5];
                            f9 = "";
                            f10 = "";


                        }







                    }
                    else
                    {






                        if (color_code.Equals("3") == true)
                        {



                            f2 = values_matn[0];
                            f3 = values_matn[1];
                            f4 = values_matn[2];
                            f5 = "";
                            f6 = "";



                            f7 = values_matn[3];
                            f8 = values_matn[4];
                            f9 = "";
                            f10 = "";



                        }







                    }


                    f5 = f5.Replace("Start Time :", "");
                    f6 = f6.Replace("End Time :", "");


                    f4 = f4.Replace("Station :", "");

                    f3 = f3.Replace(": Rate Total  in 2 Hour  ", "");


                    f9 = f9.Replace("Rain 12 hour :", "");
                    f9 = f9.Replace("mm", "");

                    f10 = f10.Replace("Rain 24 hour :", "");
                    f10 = f10.Replace("mm", "");


                    f8 = f8.Replace("Total Rain :", "");
                    f8 = f8.Replace("mm", "");






                    //     string[] values_matn = s.Split(':');

                    //////date convert//////

                    string tmp_date = tmpdate.ToString();
                    //  lblshowdate_realTime.Text = tmp_date;
                    string[] str_temp = tmp_date.Split('-');
                    string year_tmp, mount_tmp, day_tmp;
                    year_tmp = str_temp[0];
                    mount_tmp = str_temp[1];
                    day_tmp = str_temp[2];
                    //  day_tmp = day_tmp.Substring(0, 2);
                    // day_tmp = day_tmp.Substring(0, 2);

                    string tempdate = mount_tmp + "/" + day_tmp + "/" + year_tmp;

                    //       DateTime _date = DateTime.Parse(tempdate, "yyyy-MM-dd");

                    DateTime _date = DateTime.Parse(tempdate);

                    string Shamsi = "";

                    DateTime miladi = DateTime.Now;
                    miladi = _date;

                    System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
                    Shamsi = string.Format("{0}/{1}/{2}", shamsi.GetYear(miladi), shamsi.GetMonth(miladi), shamsi.GetDayOfMonth(miladi));

                    string[] pWords = Shamsi.Split(new char[] { '/' });

                    string year, mounth, day;
                    year = pWords[0];
                    mounth = pWords[1];
                    day = pWords[2];


                    if (int.Parse(day) < 10)
                    {
                        day = "0" + day;

                    }




                    if (int.Parse(mounth) < 10)
                    {
                        mounth = "0" + mounth;

                    }




                    Shamsi = year + "/" + mounth + "/" + day;





                    // lblshowdate_realTime.Text = Shamsi.ToString();
                    tmpshowdate_shamsi = Shamsi.ToString();
                    Application.DoEvents();

                    //////////////////////



                }


                catch
                {

                }


                //   if leftvalue

                tmpnumadded = tmpnumadded + 1;



                ////if( color_code.Equals ("3")==true)
                ////{
                ////    f7 = "";
                ////}




                dtgshowdataonline.Rows.Add(new object[] { tmpnumadded, tmpshowdate_shamsi, tmptime, f1, f3, f4, f5, f6, f7, f8, f9, f10 });

                ////if (color_code.Equals("4") == true)
                ////{
                ////    dtgshowdataonline.Rows.Add(new object[] { tmpnumadded, tmpshowdate_shamsi, tmptime, f1, f3, f4, f5, f6, f7, f8 });

                ////}
                ////else
                ////{


                ////    f5 = "";
                ////    f6 = "";

                ////    dtgshowdataonline.Rows.Add(new object[] { tmpnumadded, tmpshowdate_shamsi, tmptime, f1, f3, f4, f5, f6, f7, f8,f8,f9 });

                ////}









                Application.DoEvents();


                DataGridViewRow dgvr = dtgshowdataonline.Rows[tmpnumadded - 1];
                //  dgvr.Cells[4].Style.BackColor = Color.Blue;

                switch (color_code)
                {
                    case "1":
                        dgvr.Cells[3].Style.ForeColor = Color.Blue;

                        break;
                    case "2":
                        dgvr.Cells[3].Style.ForeColor = Color.Red;
                        break;

                    case "3":
                        dgvr.Cells[3].Style.ForeColor = Color.Pink;
                        break;

                    case "4":
                        dgvr.Cells[3].Style.ForeColor = Color.Green;
                        break;



                    default:

                        break;
                }





                // dtgshowdataonline.Rows[tmpnumadded].Cells[4].Value.ForeColor = Color.Green;







            }

            //DataGridViewRow dgvr = dtgshowdataonline.Rows[3];
            ////  dgvr.Cells[4].Style.BackColor = Color.Blue;
            //dgvr.Cells[4].Style.ForeColor = Color.Green;

            Filereader.Close();



        }







        private void radioButton4_Click(object sender, EventArgs e)
        {






        }

        private void rdinstant_Click(object sender, EventArgs e)
        {

            showdownloaddata();

        }

        private void rdcalculated_Click(object sender, EventArgs e)
        {

            showdownloaddata();

        }

        private void chkautomated_CheckedChanged(object sender, EventArgs e)
        {


            if (chkautomated.Checked == true)
            {

                tmrfetchdata.Enabled = true;





            }

            else
            {
                tmrfetchdata.Enabled = false;


            }







        }

        private void tmrfetchdata_Tick(object sender, EventArgs e)
        {


            ////if (rd3sec.Checked == true) tmrfetchdata.Interval = 3000;

            ////if (rd5sec.Checked == true) tmrfetchdata.Interval = 5000;

            ////if (rd10sec.Checked == true) tmrfetchdata.Interval = 10000;




            pictureBox11_Click(null, null);

        }

        private void label53_Click(object sender, EventArgs e)
        {



            //dtgridfrequence.Rows.Clear();
            //  dtgridfrequence.Columns.Clear();


            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\refinePercentTableFile.conf";









            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            //  readline = (Filereader.ReadLine());



            DataGridViewTextBoxColumn textboxcolumn = new DataGridViewTextBoxColumn();
            TextBox txt = new TextBox();
            textboxcolumn.Width = 150;
            //   dtgridfrequence.Columns.Add(textboxcolumn);



            DataGridViewTextBoxColumn textboxcolumn2 = new DataGridViewTextBoxColumn();
            TextBox txt2 = new TextBox();
            textboxcolumn2.Width = 150;
            //dtgridfrequence.Columns.Add(textboxcolumn2);





            //    dtgridfrequence.Columns[0].HeaderText = "ردیف";
            //    dtgridfrequence.Columns[1].HeaderText = "مقدار";


            int linenumbers = 0;

            while ((readline = Filereader.ReadLine()) != null)
            {




                if (readline == null)
                {


                    break;



                }




                if (readline.Equals("") == true)
                {


                    break;



                }



                lblstatus.Text = "در حال بار گذاری اطلاعات " + "...............";


                if (progfetchsensors.Value + 2 < 100)
                {

                    progfetchsensors.Value = progfetchsensors.Value + 2;

                }




                string s = readline, leftvalue = "", rightvalue = "";
                string[] values = s.Split(',');

                try
                {




                    leftvalue = values[0];
                    rightvalue = values[1];
                    ////rightvalue = s.Replace(leftvalue, "");
                    ////rightvalue = rightvalue.Replace(" ", "");






                }


                catch
                {

                }








                //////string s = readline, leftvalue = "", rightvalue = "";



                //   dtgridfrequence.Rows.Add(new object[] { linenumbers.ToString(), s });

                //dtgridfrequence.Rows.Add(new object[] { leftvalue, rightvalue });

                //////linenumbers = linenumbers + 1;



            }






            Filereader.Close();

            progfetchsensors.Value = 100;


            Application.DoEvents();


            //    lblstatus.Text = "پایان بارگذاری اطلاعات";

            // tabControl1.SelectedTab = tabPage6;





        }

        private void label54_Click(object sender, EventArgs e)
        {




            //    lblstatus.Text = "در حال تنظیم فایل خروجی";
            //    progfetchsensors.Value = 2;


            //    System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\ refinePercentTableFile.conf");





            //    string rightvalue, savedata;







            // //   for (int i = 0; i < dtgridfrequence.RowCount - 1; i++)
            // //   {





            //     //  rightvalue = dtgridfrequence.Rows[i].Cells[1].Value.ToString(); ;

            //    //    savedata = rightvalue;




            // //       filewriter.WriteLine(savedata);



            ////    }






            //    filewriter.Close();

            //    progfetchsensors.Value = 100;
            //    lblstatus.Text = "پایان مرحله خروجی فایل";






        }














        private void lblmin_Click(object sender, EventArgs e)
        {


            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;


        }

        private void pictureBox11_Click_1(object sender, EventArgs e)
        {

            pnlhashcode.Visible = false;




        }

        private void lblshowpnlhash_Click(object sender, EventArgs e)
        {

            //txtpass.Text = "";
            txtshowhash.Text = "";
            //txtshowhash.Enabled = false;

            pnlCalibration.Visible = false;
            pnlSensors.Visible = false;
            pnlweb.Visible = false;
            pnlhelp.Visible = false;
            pnl_show_digital_rain.Visible = false;


            pnlhashcode.Visible = false;
            pnldatatransfer.Visible = false;
            pnlshowdata.Visible = false;
            pnlloaddata.Visible = false;
            pnlhashcode.Visible = true;


        }


        private void loaddatatocombostation()
        {

            string flname = "";
            string readline = "";
            glbguid = true;

            flname = Application.StartupPath + "\\screen\\station.cls";


            cmbnamestation.Items.Clear();







            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);




            while ((readline = Filereader.ReadLine()) != null)
            {




                if (readline == null)
                {


                    break;



                }




                if (readline.Equals("") == true)
                {


                    break;



                }







                //  string s = readline, leftvalue = "", rightvalue = "";

                //  txtshowhash.Enabled = true;





                cmbnamestation.Items.Add(readline);












            }






            Filereader.Close();





        }





        private void label55_Click(object sender, EventArgs e)
        {

            string flname = "";
            string readline = "";
            glbguid = true;

            flname = Application.StartupPath + "\\screen\\cdl.png";









            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);




            while ((readline = Filereader.ReadToEnd()) != null)
            {




                if (readline == null)
                {


                    break;



                }




                if (readline.Equals("") == true)
                {


                    break;



                }







                //  string s = readline, leftvalue = "", rightvalue = "";

                txtshowhash.Enabled = true;





                txtshowhash.Text = readline;












            }






            Filereader.Close();





        }



        private void pictureBox12_Click(object sender, EventArgs e)
        {




            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\screen\\cdl.png");





            string savedata;

            savedata = txtshowhash.Text;

            filewriter.WriteLine(savedata);

            filewriter.Close();


            System.Windows.Forms.MessageBox.Show("تغییرات ذخیره شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);





        }

        private void lbltestguid_Click(object sender, EventArgs e)
        {



            ///////////////////////////////////////////////////
            string flname = "";
            string readline = "";


            //read guid from board////////////

            label62_Click(null, null);


            //read guid from board////////////



            //////////////////////////////////////////readimg  from cdl.png and compare/////


            flname = Application.StartupPath + "\\screen\\cdl.png";



            System.IO.StreamReader Filereader2 = new System.IO.StreamReader(flname);


            while ((readline = Filereader2.ReadLine()) != null)
            {




                if (readline == null)
                {
                    break;
                }




                if (readline.Equals("") == true)
                {

                    break;
                }



                string s2 = readline;


                if (s2.Equals(glbguidID) == true)
                {

                    glbguid = true;

                    Filereader2.Close();
                    return;



                }



            }

            Filereader2.Close();


            if (glbguid == false)
            {

                MessageBox.Show("شما مجاز به استفاده از نرم افزار نیستید");
                return;

            }









        }

        private void txtpass_TextChanged_1(object sender, EventArgs e)
        {


            //////if (txtpass.Text.Equals("uasdelete") == true)
            //////{



            //////    picdeletedata.Visible = true;




            //////}




            //if (txtpass.Text.Equals("password") == true)
            //{



            //   // picdeletedata.Visible = true;
            //   // label55_Click(null, null);
            //   // picsaveguid.Visible = true;

            //   //groupBox2.Text="شناسه های مجاز برای سیستم";

            //}




        }

        private void lbltimerestart_Click(object sender, EventArgs e)
        {




            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\TestThread.log");






            string savedata, strtime, strdate;




            ///// agar 3 gharar dadim anvaght braye tanzim date bashd va 2 khat paiin ra bekhanad.
            ///yek gozine dar barname bezarim baraye tanzim date 
            /////  savedata = "3";

            //  savedata = "1" + "\r" + "22" + "\r" + "33";

            savedata = "1";






            ////filewriter.WriteLine(savedata);
            ////filewriter.WriteLine(savedata);
            ////filewriter.WriteLine(savedata);
            filewriter.Write(savedata);




            //// //////DateTime theDate = DateTime.UtcNow;
            //// //////string customdate = theDate.ToString("d");

            //// //////strtime = DateTime.Now.ToLongTimeString().ToString();
            //// //////strdate = customdate;


            //// //////filewriter.WriteLine(strdate);
            //// //////filewriter.WriteLine(strtime);



            //// filewriter.WriteLine("2");
            //// filewriter.WriteLine("3");


            filewriter.Close();


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            if (listboxFiles.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }



            string filename, filePath, FTPAddresssend;




            FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//applog//";



            prgstatusftbdownload.Value = 0;







            filename = "TestThread.log";


            filePath = Application.StartupPath + "\\configExport\\";



            ftpusername = "root";
            ftppassword = "password";



            //Create FTP request
            //  FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(filePath + filename);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();

            //  MessageBox.Show("Uploaded Successfully");

            lblstatusftp.Text = "راه اندازی مجدد سیستم ";
            prgstatusftbdownload.Value = prgstatusftbdownload.Maximum;

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();






        }

        private void rdinstant_CheckedChanged(object sender, EventArgs e)
        {

            tabcontrol2.SelectedTab = tabPage7;


        }

        private void rdcalculated_CheckedChanged(object sender, EventArgs e)
        {

            tabcontrol2.SelectedTab = tabPage7;
        }

        private void dtgshowdataonline_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public void deletedatafile()
        {


            if (listboxFiles3.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }



            string filename1 = "";



            txtData2.Text = "";
            int cnt = 0;



            int cnttransfer = 0;
            for (int k = 0; k < listboxFiles3.Items.Count; k++)
            {
                filename1 = listboxFiles3.Items[k].ToString();




                ftpFilename = filename1;


                ////if (File.Exists(Application.StartupPath + "\\datalog\\" + ftpFilename))
                ////{
                ////   // Console.WriteLine("file exists");

                ////    continue;
                ////}



                deletedownloadFiledata(FTPAddressdownloaddata, filename1, ftpusername, ftppassword);

                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();



            }



            lblcountfiletransfer.Text = "پابان حذف اطلاعات";

            return;




            /////////////////////////



        }




        private void label57_Click(object sender, EventArgs e)
        {


            if (listboxFiles3.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }


            listBox4.Items.Clear();

            string filename1 = "";



            txtData2.Text = "";
            int cnt = 0;



            for (int j = 0; j < listboxFiles3.Items.Count; j++)
            {
                filename1 = listboxFiles3.Items[j].ToString();







                ftpFilename = filename1;



                string path = Application.StartupPath + "\\datalog\\" + glbguidID + "\\";



                // FileInfo f = new FileInfo((path + ftpFilename));

                ////if (f.Exists == false)
                ////{
                ////    continue;
                ////}

                ////long fs = f.Length;

                // if (File.Exists(path + ftpFilename) && fs ==174000)
                if (File.Exists(path + ftpFilename) == true)
                {
                    // Console.WriteLine("file exists");

                    FileInfo f = new FileInfo((Application.StartupPath + "\\datalog\\" + glbguidID + "\\" + ftpFilename));
                    long fs = f.Length;
                    if (fs == 174000)
                    {
                        continue;
                    }

                    cnt = cnt + 1;



                }
                else
                {

                    cnt = cnt + 1;
                }


                lblcountfiletransfer.Visible = true;

                lblcountfiletransfer.Text = "تعداد فایل جدید برای دانلود" + " : " + cnt.ToString();


            }



            if (cnt == 0)
            {

                lblcountfiletransfer.Visible = true;

                lblcountfiletransfer.Text = "فایل جدیدی برای دانلود وجود ندارد";


            }



            int cnttransfer = 0;
            for (int k = 0; k < listboxFiles3.Items.Count; k++)
            {
                filename1 = listboxFiles3.Items[k].ToString();


                //FileInfo f = new FileInfo(filename1);
                //long s1 = f.Length;


                //File.re

                ftpFilename = filename1;

                // FileInfo f = new FileInfo((Application.StartupPath + "\\datalog\\" + glbguidID + "\\" + ftpFilename));
                // long fs = f.Length;

                //if (File.Exists(Application.StartupPath + "\\datalog\\" + glbguidID + "\\" + ftpFilename) && fs==174000)
                if (File.Exists(Application.StartupPath + "\\datalog\\" + glbguidID + "\\" + ftpFilename) == true)
                {

                    FileInfo f = new FileInfo((Application.StartupPath + "\\datalog\\" + glbguidID + "\\" + ftpFilename));
                    long fs = f.Length;
                    if (fs == 174000)
                    {
                        continue;
                    }
                    // Console.WriteLine("file exists");


                }



                downloadFiledata(FTPAddressdownloaddata, filename1, ftpusername, ftppassword);

                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();



                lblsavedatadatalog_Click(null, null);

                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();

                cnttransfer = cnttransfer + 1;




                txtData2.Text = cnttransfer.ToString();
                listBox4.Items.Add(ftpFilename);



            }


            /////////////////////////////////////////delete table datelog///////////////////
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            OleDbDataAdapter da3 = new OleDbDataAdapter();
            da3.SelectCommand = new OleDbCommand("select * from tbldatelog ", myconn);
            da3.Fill(ds, "tbldatelog");
            dt = ds.Tables["tbldatelog"];

            for (int r = 0; r < dt.Rows.Count; r++)
            {

                dt.Rows[r].Delete();


            }


            da3.DeleteCommand = new OleDbCommand("delete  from tbldatelog ", myconn);



            try
            {
                da3.Update(dt);
            }
            catch (Exception ex)
            {
                // throw ex;
            }

            //////////////////////////////////////////////////////////




            string flnmae = "";

            lblcountfiletransfer.Text = "در حال استخراج اطلاعات.......";
            for (int k = 0; k < listboxFiles3.Items.Count; k++)
            {

                flnmae = listboxFiles3.Items[k].ToString();
                ftpFilename = flnmae;
                Application.DoEvents();
                Application.DoEvents();
                extractdatestampfromfile_Click(null, null);

                Application.DoEvents();
                Application.DoEvents();


            }


            cmbfilelog.Items.Clear();
            cmbfilelogTo.Items.Clear();

            OleDbDataAdapter da1 = new OleDbDataAdapter();

            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();
            da1.SelectCommand = new OleDbCommand("select distinct(yeardaymounth) from tbldatelog order by yeardaymounth ", myconn);
            da1.Fill(ds1, "tbldatelog");
            dt1 = ds1.Tables["tbldatelog"];


            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                cmbfilelog.Items.Add(ds1.Tables[0].Rows[i][0].ToString());
                cmbfilelogTo.Items.Add(ds1.Tables[0].Rows[i][0].ToString());
            }

            int a = cmbfilelogTo.Items.Count;

            ////txtdatefrom.Text = cmbfilelogTo.Items[0].ToString();
            ////txtdateTo.Text = cmbfilelogTo.Items[a - 1].ToString();

            Pcal_txtdatefrom.Text = cmbfilelogTo.Items[0].ToString();
            pcal_txtdateTo.Text = cmbfilelogTo.Items[a - 1].ToString();


            lblcountfiletransfer.Text = "پابان استخراج اطلاعات";

            return;




            /////////////////////////



        }



        private string hex2dec(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            // StringBuilder sb = new BaseNumberConverter();
            sb.Append(long.Parse(hexString, System.Globalization.NumberStyles.HexNumber));
            return sb.ToString();
        }


        public void initial_dtgshowdatapast()
        {



            dtgshowdatapast.Rows.Clear();

            dtgshowdatapast.Columns.Clear();


            ////Application.DoEvents();



            DataGridViewTextBoxColumn textboxcolumn = new DataGridViewTextBoxColumn();
            TextBox txt = new TextBox();
            textboxcolumn.Width = 70;
            dtgshowdatapast.Columns.Add(textboxcolumn);



            DataGridViewTextBoxColumn textboxcolumn2 = new DataGridViewTextBoxColumn();
            TextBox txt2 = new TextBox();
            textboxcolumn2.Width = 70;
            dtgshowdatapast.Columns.Add(textboxcolumn2);




            DataGridViewTextBoxColumn textboxcolumn3 = new DataGridViewTextBoxColumn();
            TextBox txt3 = new TextBox();
            textboxcolumn3.Width = 50;
            dtgshowdatapast.Columns.Add(textboxcolumn3);



            DataGridViewTextBoxColumn textboxcolumn4 = new DataGridViewTextBoxColumn();
            TextBox txt4 = new TextBox();
            textboxcolumn4.Width = 30;
            dtgshowdatapast.Columns.Add(textboxcolumn4);


            DataGridViewTextBoxColumn textboxcolumn5 = new DataGridViewTextBoxColumn();
            TextBox txt5 = new TextBox();
            textboxcolumn5.Width = 30;
            dtgshowdatapast.Columns.Add(textboxcolumn5);



            DataGridViewTextBoxColumn textboxcolumn6 = new DataGridViewTextBoxColumn();
            TextBox txt6 = new TextBox();
            textboxcolumn6.Width = 40;
            dtgshowdatapast.Columns.Add(textboxcolumn6);



            DataGridViewTextBoxColumn textboxcolumn7 = new DataGridViewTextBoxColumn();
            TextBox txt7 = new TextBox();
            textboxcolumn7.Width = 40;
            dtgshowdatapast.Columns.Add(textboxcolumn7);


            DataGridViewTextBoxColumn textboxcolumn8 = new DataGridViewTextBoxColumn();
            TextBox txt8 = new TextBox();
            textboxcolumn8.Width = 40;
            dtgshowdatapast.Columns.Add(textboxcolumn8);


            DataGridViewTextBoxColumn textboxcolumn9 = new DataGridViewTextBoxColumn();
            TextBox txt9 = new TextBox();
            textboxcolumn9.Width = 40;
            dtgshowdatapast.Columns.Add(textboxcolumn9);






            dtgshowdatapast.Columns[0].HeaderText = " Number";
            dtgshowdatapast.Columns[1].HeaderText = "Ordinal Number";



            dtgshowdatapast.Columns[2].HeaderText = "Year";
            dtgshowdatapast.Columns[3].HeaderText = "Mounth";
            dtgshowdatapast.Columns[4].HeaderText = "Day";
            dtgshowdatapast.Columns[5].HeaderText = "Day of Week";
            dtgshowdatapast.Columns[6].HeaderText = "Hour";
            dtgshowdatapast.Columns[7].HeaderText = "Minute";
            dtgshowdatapast.Columns[8].HeaderText = "Sec";





            //////dtgshowdatapast.Columns[0].HeaderText = " ردیف";
            //////dtgshowdatapast.Columns[1].HeaderText = "شماره ترتیبی";



            //////dtgshowdatapast.Columns[2].HeaderText = "سال";
            //////dtgshowdatapast.Columns[3].HeaderText = "ماه";
            //////dtgshowdatapast.Columns[4].HeaderText = "روز";
            //////dtgshowdatapast.Columns[5].HeaderText = "روز در هفته";
            //////dtgshowdatapast.Columns[6].HeaderText = "ساعت";
            //////dtgshowdatapast.Columns[7].HeaderText = "دقیقه";
            //////dtgshowdatapast.Columns[8].HeaderText = "ثانیه";











        }




        private void label58_Click(object sender, EventArgs e)
        {



            int numRecords = 1;
            int itemSectionStart = 0x00000000;
            int itemSectionEnd = 0x00000008;











            string flname = "";




            flname = ftpFilename;





            lblloadconfingchannel_Click(null, null);
            Application.DoEvents();
            Application.DoEvents();


            if (txtnamestation.Visible == true && txtnamestation.Text != "")
            {

                glbguidID = txtnamestation.Text;

            }




            flname = Application.StartupPath + "\\datalog\\" + glbguidID + "\\" + flname;
            using (FileStream str = File.OpenRead(flname))
            {




                int bytetransfer = 0;
                int channelcount = 28;








                BinaryReader breader = new BinaryReader(str);
                breader.BaseStream.Position = itemSectionStart;
                byte[] itemSection = breader.ReadBytes(itemSectionEnd);
                byte[] p = null;


                int j = 0;
                int k = j++;



                bytetransfer = 0;



                string ordinalnumber, year, mounth, day, dayofweek, hour, min, sec, channelindex, intvalue, floatvalue;



                int numcounter = 0;


                DataSet oDS = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM tbldatadownload", myconn));

                OleDbCommandBuilder oOrdersCmdBuilder = new OleDbCommandBuilder(da);
                da.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "tbldatadownload";




                while (true)
                {


                    // break;


                    try
                    {



                        if (bytetransfer == 8)
                        {

                            itemSection = breader.ReadBytes(itemSectionEnd);
                            bytetransfer = 0;

                        }

                        ////ordinal number///////

                        string id = "";


                        id = BitConverter.ToString(itemSection, bytetransfer, 4);
                        string[] strArrayID = id.Split(new char[] { '-' });

                        string reversedID = strArrayID[3] + strArrayID[2] + strArrayID[1] + strArrayID[0];
                        //  listBox1.Items.Add(this.hex2dec(reversedID) + " :" + "  ordinal number");

                        ordinalnumber = this.hex2dec(reversedID);

                        bytetransfer = bytetransfer + 4;






                        ////////////////year reverse//////////////////

                        if (bytetransfer == 8)
                        {
                            itemSection = breader.ReadBytes(itemSectionEnd);
                            bytetransfer = 0;
                        }

                        id = BitConverter.ToString(itemSection, bytetransfer, 2);

                        strArrayID = id.Split(new char[] { '-' });
                        reversedID = strArrayID[1] + strArrayID[0];
                        // listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "year");

                        year = this.hex2dec(reversedID);

                        bytetransfer = bytetransfer + 2;



                        ///////////////////////mounth

                        if (bytetransfer == 8)
                        {
                            itemSection = breader.ReadBytes(itemSectionEnd);
                            bytetransfer = 0;
                        }



                        id = BitConverter.ToString(itemSection, bytetransfer, 1);
                        strArrayID = id.Split(new char[] { '-' });
                        reversedID = strArrayID[0];
                        //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "mounth");

                        mounth = this.hex2dec(reversedID);

                        bytetransfer = bytetransfer + 1;



                        ////////////////day//////////////

                        if (bytetransfer == 8)
                        {
                            itemSection = breader.ReadBytes(itemSectionEnd);
                            bytetransfer = 0;
                        }



                        id = BitConverter.ToString(itemSection, bytetransfer, 1);
                        strArrayID = id.Split(new char[] { '-' });
                        reversedID = strArrayID[0];
                        // listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "day");



                        day = this.hex2dec(reversedID);

                        bytetransfer = bytetransfer + 1;

                        ////////////////////////////////




                        //////convert date to shamsi//////////////



                        //      string tempdate=year + "/" + mounth + "/" + day;

                        string tempdate = mounth + "/" + day + "/" + year;

                        //       DateTime _date = DateTime.Parse(tempdate, "yyyy-MM-dd");

                        DateTime _date = DateTime.Parse(tempdate);

                        string Shamsi = "";

                        DateTime miladi = DateTime.Now;
                        miladi = _date;

                        System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
                        Shamsi = string.Format("{0}/{1}/{2}", shamsi.GetYear(miladi), shamsi.GetMonth(miladi), shamsi.GetDayOfMonth(miladi));

                        string[] pWords = Shamsi.Split(new char[] { '/' });

                        year = pWords[0];
                        mounth = pWords[1];
                        day = pWords[2];


                        if (int.Parse(day) < 10)
                        {
                            day = "0" + day;

                        }




                        if (int.Parse(mounth) < 10)
                        {
                            mounth = "0" + mounth;

                        }




                        Shamsi = year + "/" + mounth + "/" + day;





                        ////////////////////////////////////






                        //  itemSection = breader.ReadBytes(itemSectionEnd);///read new bytes

                        ////////////////day  of week//////////////

                        if (bytetransfer == 8)
                        {
                            itemSection = breader.ReadBytes(itemSectionEnd);
                            bytetransfer = 0;
                        }



                        id = BitConverter.ToString(itemSection, bytetransfer, 1);
                        strArrayID = id.Split(new char[] { '-' });
                        reversedID = strArrayID[0];
                        //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "day of week");

                        dayofweek = this.hex2dec(reversedID);


                        bytetransfer = bytetransfer + 1;

                        ////////////////////////////////


                        ////////////////hour//////////////

                        if (bytetransfer == 8)
                        {
                            itemSection = breader.ReadBytes(itemSectionEnd);
                            bytetransfer = 0;
                        }

                        id = BitConverter.ToString(itemSection, bytetransfer, 1);
                        strArrayID = id.Split(new char[] { '-' });
                        reversedID = strArrayID[0];
                        //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "hour");

                        hour = this.hex2dec(reversedID);

                        bytetransfer = bytetransfer + 1;



                        ////////////////////////////////


                        ////////////////min//////////////

                        if (bytetransfer == 8)
                        {
                            itemSection = breader.ReadBytes(itemSectionEnd);
                            bytetransfer = 0;
                        }


                        id = BitConverter.ToString(itemSection, bytetransfer, 1);
                        strArrayID = id.Split(new char[] { '-' });
                        reversedID = strArrayID[0];
                        //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "min");


                        min = this.hex2dec(reversedID);

                        bytetransfer = bytetransfer + 1;

                        ////////////////////////////////



                        ////////////////sec//////////////



                        if (bytetransfer == 8)
                        {
                            itemSection = breader.ReadBytes(itemSectionEnd);
                            bytetransfer = 0;
                        }

                        id = BitConverter.ToString(itemSection, bytetransfer, 1);
                        strArrayID = id.Split(new char[] { '-' });
                        reversedID = strArrayID[0];
                        //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "sec");


                        sec = this.hex2dec(reversedID);

                        bytetransfer = bytetransfer + 1;
                        ////////////////////////////////



                        string parsstringval = "";


                        for (int i = 0; i < channelcount; i++)
                        {


                            ////////////////channel index//////////////

                            if (bytetransfer == 8)
                            {
                                itemSection = breader.ReadBytes(itemSectionEnd);
                                bytetransfer = 0;
                            }

                            id = BitConverter.ToString(itemSection, bytetransfer, 4);
                            strArrayID = id.Split(new char[] { '-' });
                            reversedID = strArrayID[3] + strArrayID[2] + strArrayID[1] + strArrayID[0];
                            //   listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "channel index");


                            channelindex = this.hex2dec(reversedID);


                            ///// for when for example from chgannel index 15 miravim be 24 va yoho 8 chanal nist in vasat va index loop ra ezafe mikonim

                            if (i < int.Parse(channelindex))
                            {
                                i = int.Parse(channelindex);

                            }


                            if (channelindex.Equals("27") == true)
                            {

                                int kp = 0;

                            }



                            ////i = int.Parse(channelindex);



                            bytetransfer = bytetransfer + 4;
                            ////////////////////////////////




                            /// ********  itemSection = breader.ReadBytes(itemSectionEnd);///read new bytes

                            ////////////////int value //////////////

                            if (bytetransfer == 8)
                            {
                                itemSection = breader.ReadBytes(itemSectionEnd);
                                bytetransfer = 0;
                            }

                            id = BitConverter.ToString(itemSection, bytetransfer, 4);
                            strArrayID = id.Split(new char[] { '-' });
                            reversedID = strArrayID[3] + strArrayID[2] + strArrayID[1] + strArrayID[0];
                            //    listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "int value");

                            intvalue = this.hex2dec(reversedID);

                            bytetransfer = bytetransfer + 4;


                            ////////////////////////////////




                            ////////////////float value //////////////

                            if (bytetransfer == 8)
                            {
                                itemSection = breader.ReadBytes(itemSectionEnd);
                                bytetransfer = 0;
                            }




                            id = BitConverter.ToString(itemSection, bytetransfer, 4);
                            strArrayID = id.Split(new char[] { '-' });
                            reversedID = strArrayID[3] + strArrayID[2] + strArrayID[1] + strArrayID[0];
                            //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "int value");

                            //     listBox1.Items.Add("************");

                            floatvalue = this.hex2dec(reversedID);

                            bytetransfer = bytetransfer + 4;



                            /////////////adding to grid ///////////////////

                            string value = intvalue + "." + floatvalue;


                            int channelindexvalue = int.Parse(channelindex);



                            string channel_name = channelname[channelindexvalue];

                            // string channel_name = "evp";


                            numcounter = numcounter + 1;







                            //     dtgshowdatapast.Rows.Add(new object[] { numcounter.ToString(), ordinalnumber, year, mounth, day, dayofweek, hour, min, sec, channelindex, channel_name, value });


                            /// if baraye rain as vagana shart bardashte mishavad
                            if (i >= 20)
                            {
                                parsstringval = parsstringval + channelindex + ":" + channel_name + ":" + value + "-";
                            }

                            lbProgress2.Text = numcounter.ToString();

                            // Application.DoEvents();


                            ////////////////////////////////


                        } //// for channelindex for  28 bar
                        //////////////////////////////////////////////////////insert into db/////////////////////////////////////////////////////

                        DataRow oOrderRow = oDS.Tables["tbldatadownload"].NewRow();

                        oOrderRow["numcounter"] = numcounter;
                        oOrderRow["ordinalnumber"] = ordinalnumber;

                        oOrderRow["logfile"] = ftpFilename;

                        oOrderRow["yearval"] = year;
                        oOrderRow["mounthval"] = mounth;
                        oOrderRow["dayval"] = day;
                        oOrderRow["Shamsidate"] = Shamsi;
                        oOrderRow["dayofweek"] = dayofweek;
                        oOrderRow["hourval"] = hour;

                        oOrderRow["minval"] = min;
                        oOrderRow["secval"] = sec;


                        oOrderRow["parsstringval"] = parsstringval;





                        oDS.Tables["tbldatadownload"].Rows.Add(oOrderRow);

                        ////da.Update(oDS, "tbldatadownload");





                        ///////////////////////////////////////////////////////////////////////////////////////////////////////








                    }  ////baray catch



                    catch (Exception)
                    {
                        break;
                    }



                }  /// loop while

                da.Update(oDS, "tbldatadownload");
            }







        }







        private void label59_Click(object sender, EventArgs e)
        {







        }

        private void label60_Click(object sender, EventArgs e)
        {







        }





        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }





        private void tabcontrol2_Click(object sender, EventArgs e)
        {



            //if (tabcontrol2.SelectedTab == tabPage8)
            //{




            //}


            if (tabcontrol2.SelectedTab  == tabPage14)
            {

                tabcontrol2.SelectedTab = tabPage14;
                return;
            }



            
            if (cmbnamestation.Visible == true)
            {

                tabcontrol2.SelectedTab = tabPage8;
                return;
            }

            if (cmbnamestation.Visible == false)
            {

                tabcontrol2.SelectedTab = tabPage7;
                return;
            }




            //////if (tabcontrol2.SelectedTab == tabPage7)
            //////{

            //////    picexporttest.Visible = false;
            //////    picexportexcell.Visible = false;
            //////    picexporttest.Visible = false;
            //////    label61.Visible = false;
            //////    cmbfilelog.Visible = false;
            //////    cmbfilelogTo.Visible = false;
            //////    pictureBox10.Visible = false;
            //////    label60.Visible = false;
            //////    groupBox12.Enabled = true;


            //////    chkautomated.Visible = true;

            //////    rd3sec.Visible = true;
            //////    rd5sec.Visible = true;
            //////    rd10sec.Visible = true;

            //////}



            //////if (tabcontrol2.SelectedTab == tabPage8)
            //////{


            //////    picexportexcell.Visible = true;
            //////  picexporttest.Visible = true;
            //////    label61.Visible = true;
            //////    cmbfilelog.Visible = true;
            //////    cmbfilelogTo.Visible = true;
            //////    pictureBox10.Visible = true;
            //////    label60.Visible = true;

            //////    groupBox12.Enabled = false;


            //////    chkautomated.Visible = false;

            //////    rd3sec.Visible = false;
            //////    rd5sec.Visible = false;
            //////    rd10sec.Visible = false;

            //////}









        }

        private void picexporttest_Click(object sender, EventArgs e)
        {




            ////label85.Visible = false;
            ////cmbnamestation.Visible = false;
            cmbnamestation.Width = 0;

            dtgshowdataonline.Rows.Clear();
            dtgshowdataonline.Columns.Clear();
            listboxFiles3.Items.Clear();

            lblname_station.Visible = false;
            label85.Visible = true;
            cmbnamestation.Visible = true;


            //loaddatatocombostation();


            //txtnamestation.Text



            ////if (cmbnamestation.Text == "")
            ////{

            ////    MessageBox.Show("لطفا نام  ایستگاه را وارد نمایید");
            ////    return;


            ////}













            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            string[] files = Directory.GetFiles(fbd.SelectedPath);


            //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");



            DirectoryInfo d = new DirectoryInfo(fbd.SelectedPath);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.log"); //Getting Text files
            string flname = "";






            //  return;

            tmrfetchdata.Enabled = false;

            cmbfilelog.Items.Clear();
            cmbfilelogTo.Items.Clear();

            //    




            ////DirectoryInfo d = new DirectoryInfo(Application.StartupPath +"\\datalog\\");//Assuming Test is your Folder
            ////FileInfo[] Files = d.GetFiles("*.log"); //Getting Text files
            ////string flname = "";
            prgstatusftbdownload2.Value = 0;
            prgstatusftbdownload2.Maximum = 0;

            int cnt = 0;

            string name_folder = "";

            foreach (FileInfo file in Files)
            {




                listboxFiles3.Items.Add(file.Name);

                name_folder = file.DirectoryName;




                // string str = str + ", " + file.Name;
            }





            Application.DoEvents();
            Application.DoEvents();

            if (listboxFiles3.Items.Count == 0)
            {

                MessageBox.Show("هیچ اطلاعاتی در پوشه ی مشخص شده وجود ندارد");
                return;


            }



            ///reading header




            string flname1 = "";
            string tmpguid, tmpname_station, tmp_timstamp;

            flname1 = name_folder + "\\usb_log.cfg";
            cmbnamestation.Enabled = true;


            if (File.Exists(flname1))
            {


                cmbnamestation.Enabled = false;
                System.IO.StreamReader Filereader = new System.IO.StreamReader(flname1);

                tmpguid = Filereader.ReadLine();
                tmpname_station = Filereader.ReadLine();
                tmp_timstamp = Filereader.ReadLine();



                string guid__12 = tmpguid.Substring(tmpguid.Length - 12);




                cmbnamestation.Text = guid__12 + "_" + tmpname_station + "_" + tmp_timstamp;

                lblname_station.Text = cmbnamestation.Text;


                // cmbnamestation.Visible = false;
                cmbnamestation.Width = 0;
                label85.Visible = false;


                MessageBox.Show(lblname_station.Text + " : شماره سریال سیستم");

                // lblname_station.Visible = true;

                //cmbnamestation.vi


            }
            else
            {

                if (cmbnamestation.Text == "")
                {

                    MessageBox.Show("لطفا نام ایستگاه را انتخاب نمایید و دوباره مسیر را انتخاب کنید");
                    lblname_station.Visible = false;
                    return;


                }




            }











            ///









            glbguidID = cmbnamestation.Text;



            string path = Application.StartupPath + "\\datalog\\" + glbguidID + "\\";


            if (!Directory.Exists(path))
            {

                Directory.CreateDirectory(path);

            }




            string[] files2 = Directory.GetFiles(path);


            //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");



            DirectoryInfo d2 = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files2 = d2.GetFiles("*.log"); //Getting Text files
            string flname2 = "";




            foreach (FileInfo file in Files2)
            {




                //   listboxFiles3.Items.Add(file.Name);

                File.Delete(path + "\\" + file.Name);



                // string str = str + ", " + file.Name;
            }


            Application.DoEvents();
            Application.DoEvents();




            foreach (FileInfo file in Files)
            {




                //   listboxFiles3.Items.Add(file.Name);

                File.Copy(fbd.SelectedPath + "\\" + file.Name, (path + "\\" + file.Name), true);


                //  File.Copy (path+"\\" + file.Name, true);


                // string str = str + ", " + file.Name;
            }








            lblstatusftp2.Text = "پایان انتقال اطلاعات به سیستم ";
            Application.DoEvents();
            Application.DoEvents();



            label57_Click(null, null);


            Application.DoEvents();
            Application.DoEvents();

            //glbguidID = null;






        }

        private void picexportexcell_Click(object sender, EventArgs e)
        {


            string flname = "SCDl-UAS-output.csv";

            //System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\export\\" + flname, false, Encoding.Unicode);



            SaveFileDialog saveFileDialog1 = new SaveFileDialog();


            //"txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //  saveFileDialog1.Filter = "csv files (*.csv)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;



            saveFileDialog1.ShowDialog();






            flname = saveFileDialog1.FileName;

            if (flname == "")
            {
                return;


            }


            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(flname);





            string savedata = "";


            prgstatusftbdownload2.Value = 0;
            prgstatusftbdownload2.Maximum = 100;


            int cntgrid = dtgshowdatapast.RowCount;

            prgstatusftbdownload2.Value = cntgrid / 100;



            //////////////////////////////////////////////////adding header////////////////////////////////////////
            for (int r = 0; r < dtgshowdatapast.ColumnCount; r++)
            {

                // savedata = savedata + dtgshowdatapast.Columns[r].HeaderText.ToString() + ";";
                savedata = savedata + dtgshowdatapast.Columns[r].HeaderText.ToString() + ",";


            }


            filewriter.WriteLine(savedata);
            savedata = "";
            //////////////////////////////////////////////////adding header////////////////////////////////////////




            for (int k = 0; k < dtgshowdatapast.RowCount - 1; k++)
            {


                prgstatusftbdownload2.Value = k / 100;



                for (int index = 0; index < dtgshowdatapast.ColumnCount; index++)
                {

                    //try
                    //{







                    //    if (index < dtgshowdatapast.Rows[k].Cells.Count)
                    if (dtgshowdatapast.Rows[k].Cells[index].Value != null)
                    {
                        //  savedata = savedata + dtgshowdatapast.Rows[k].Cells[index].Value.ToString() + ";";

                        savedata = savedata + dtgshowdatapast.Rows[k].Cells[index].Value.ToString() + ",";


                    }




                    //}

                    //catch (Exception)
                    //{
                    //   // continue;
                    //    //return;
                    //}


                }







                filewriter.WriteLine(savedata);

                savedata = "";


            }


            filewriter.Close();
            prgstatusftbdownload2.Value = 100;
            lblstatusftp2.Text = "پایان ایجاد فایل";













            return;

            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program
            app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            // changing the name of active sheet
            worksheet.Name = "Exported from scdl";


            // storing header part in Excel
            for (int i = 1; i < dtgshowdatapast.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dtgshowdatapast.Columns[i - 1].HeaderText;
            }

            lblstatusftp2.Text = "در حال ایجاد فایل اکسل";

            // storing Each row and column value to excel sheet
            for (int i = 0; i < dtgshowdatapast.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dtgshowdatapast.Columns.Count; j++)
                {


                    worksheet.Cells[i + 2, j + 1] = dtgshowdatapast.Rows[i].Cells[j].Value.ToString();
                    Application.DoEvents();
                    lbProgress2.Text = i.ToString();



                }
            }


            // save the application
            workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Exit from the application
            app.Quit();



        }





        private void lblloadconfingchannel_Click(object sender, EventArgs e)
        {

            ////////////////////////////////
            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\rdlsys_channels.conf";

            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);

            //  string[] channelname;


            ////string[] channelname = new string[50];
            ////string[] savingtype = new string[50];
            int channlcount = 0;


            for (var i = 0; i < 50; i++)
            {

                channelname[i] = "#";
                // savingtype[i] = "#";


            }





            while ((readline = Filereader.ReadLine()) != null)
            {




                if (readline.IndexOf("#") != -1)
                {

                    continue;

                }



                if (readline == "")
                {

                    continue;

                }


                ////// joda kardane reshteha daray , va rikhtan dar yek araye
                ////////////////////////////////////////////////////////////////////
                //<channel index>, <channel name>, <sensorCode>,<sensorUnitCode>, <sensorUnitAbv>, savingType, channelType, hwPortType, hwPortNumber, hwPortPinNumber , calculationType, IsActive
                string s = readline;
                string[] values = s.Split(',');



                ///khandane satre aval baraye yek sensor shamel   etelaate paye 
                String channel_name, sensorCode, sensorUnitCode, sensorUnitAbv, savingType, channelType, hwPortType, hwPortNumber, hwPortPinNumber, calculationType, IsActive;

                int channel_index;
                ////string temp = values[1];
                ////int v;
                ////Boolean a;
                ////a = int.TryParse(temp,out v);

                if (s.IndexOf("=") > -1)
                {

                    continue;

                }



                channel_index = int.Parse(values[0]);
                channel_name = values[1];

                savingType = values[5];




                channelname[channel_index] = channel_name;







                int savetype = int.Parse(savingType);
                string savingType_persian = "";



                switch (savetype)
                {


                    case 0:
                        savingType_persian = "مینیمم";
                        break;

                    case 1:
                        savingType_persian = "ماکزیمم";
                        break;
                    case 2:
                        savingType_persian = "میانگین";
                        break;

                    case 3:
                        savingType_persian = "آخرین مقدار";
                        break;

                    default:
                        break;


                }

                ////savingtype[channel_index] = savingType_persian;

                //////   readline = Filereader.ReadLine();
                ////readline = Filereader.ReadLine();



                if (readline == "")
                {


                    readline = Filereader.ReadLine();


                }


                readline = Filereader.ReadLine();//yek khat ra rad midahad
                // readline = Filereader.ReadLine();



                if (readline == null)
                {


                    break;


                }




                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }








            }





            Filereader.Close();


        }

        private void datagridloadtempdata_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void extractdatestampfromfile_Click(object sender, EventArgs e)
        {


            //////string filename = "";


            int numRecords = 1;
            int itemSectionStart = 0x00000000;
            int itemSectionEnd = 0x00000008;


            string flname = "";




            flname = ftpFilename;


            flname = Application.StartupPath + "\\datalog\\" + glbguidID + "\\" + flname;
            using (FileStream str = File.OpenRead(flname))
            {




                int bytetransfer = 0;
                int channelcount = 28;








                BinaryReader breader = new BinaryReader(str);
                breader.BaseStream.Position = itemSectionStart;
                byte[] itemSection = breader.ReadBytes(itemSectionEnd);
                byte[] p = null;


                int j = 0;
                int k = j++;



                bytetransfer = 0;



                string ordinalnumber, year, mounth, day, dayofweek, hour, min, sec, channelindex, intvalue, floatvalue;


                int numcounter = 0;






                DataSet oDS = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM tbldatelog", myconn));

                OleDbCommandBuilder oOrdersCmdBuilder = new OleDbCommandBuilder(da);
                da.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "tbldatelog";










                try
                {



                    if (bytetransfer == 8)
                    {

                        itemSection = breader.ReadBytes(itemSectionEnd);
                        bytetransfer = 0;

                    }

                    ////ordinal number///////

                    string id = "";


                    id = BitConverter.ToString(itemSection, bytetransfer, 4);
                    string[] strArrayID = id.Split(new char[] { '-' });

                    string reversedID = strArrayID[3] + strArrayID[2] + strArrayID[1] + strArrayID[0];
                    //  listBox1.Items.Add(this.hex2dec(reversedID) + " :" + "  ordinal number");

                    ordinalnumber = this.hex2dec(reversedID);

                    bytetransfer = bytetransfer + 4;



                    ////////////////year reverse//////////////////

                    if (bytetransfer == 8)
                    {
                        itemSection = breader.ReadBytes(itemSectionEnd);
                        bytetransfer = 0;
                    }

                    id = BitConverter.ToString(itemSection, bytetransfer, 2);

                    strArrayID = id.Split(new char[] { '-' });
                    reversedID = strArrayID[1] + strArrayID[0];
                    // listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "year");

                    year = this.hex2dec(reversedID);

                    bytetransfer = bytetransfer + 2;



                    ///////////////////////mounth

                    if (bytetransfer == 8)
                    {
                        itemSection = breader.ReadBytes(itemSectionEnd);
                        bytetransfer = 0;
                    }



                    id = BitConverter.ToString(itemSection, bytetransfer, 1);
                    strArrayID = id.Split(new char[] { '-' });
                    reversedID = strArrayID[0];
                    //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "mounth");

                    mounth = this.hex2dec(reversedID);

                    bytetransfer = bytetransfer + 1;



                    ////////////////day//////////////

                    if (bytetransfer == 8)
                    {
                        itemSection = breader.ReadBytes(itemSectionEnd);
                        bytetransfer = 0;
                    }



                    id = BitConverter.ToString(itemSection, bytetransfer, 1);
                    strArrayID = id.Split(new char[] { '-' });
                    reversedID = strArrayID[0];
                    // listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "day");



                    day = this.hex2dec(reversedID);

                    bytetransfer = bytetransfer + 1;

                    ////////////////////////////////




                    //////convert date to shamsi//////////////



                    //      string tempdate=year + "/" + mounth + "/" + day;

                    string tempdate = mounth + "/" + day + "/" + year;

                    //       DateTime _date = DateTime.Parse(tempdate, "yyyy-MM-dd");

                    DateTime _date = DateTime.Parse(tempdate);

                    string Shamsi = "";

                    DateTime miladi = DateTime.Now;
                    miladi = _date;

                    System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
                    Shamsi = string.Format("{0}/{1}/{2}", shamsi.GetYear(miladi), shamsi.GetMonth(miladi), shamsi.GetDayOfMonth(miladi));

                    string[] pWords = Shamsi.Split(new char[] { '/' });

                    year = pWords[0];
                    mounth = pWords[1];
                    day = pWords[2];


                    if (int.Parse(day) < 10)
                    {
                        day = "0" + day;

                    }




                    if (int.Parse(mounth) < 10)
                    {
                        mounth = "0" + mounth;

                    }




                    Shamsi = year + "/" + mounth + "/" + day;





                    ////////////////////////////////////


                    //  itemSection = breader.ReadBytes(itemSectionEnd);///read new bytes

                    ////////////////day  of week//////////////

                    if (bytetransfer == 8)
                    {
                        itemSection = breader.ReadBytes(itemSectionEnd);
                        bytetransfer = 0;
                    }



                    id = BitConverter.ToString(itemSection, bytetransfer, 1);
                    strArrayID = id.Split(new char[] { '-' });
                    reversedID = strArrayID[0];
                    //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "day of week");

                    dayofweek = this.hex2dec(reversedID);


                    bytetransfer = bytetransfer + 1;

                    ////////////////////////////////


                    ////////////////hour//////////////

                    if (bytetransfer == 8)
                    {
                        itemSection = breader.ReadBytes(itemSectionEnd);
                        bytetransfer = 0;
                    }

                    id = BitConverter.ToString(itemSection, bytetransfer, 1);
                    strArrayID = id.Split(new char[] { '-' });
                    reversedID = strArrayID[0];
                    //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "hour");

                    hour = this.hex2dec(reversedID);

                    bytetransfer = bytetransfer + 1;



                    ////////////////////////////////


                    ////////////////min//////////////

                    if (bytetransfer == 8)
                    {
                        itemSection = breader.ReadBytes(itemSectionEnd);
                        bytetransfer = 0;
                    }


                    id = BitConverter.ToString(itemSection, bytetransfer, 1);
                    strArrayID = id.Split(new char[] { '-' });
                    reversedID = strArrayID[0];
                    //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "min");


                    min = this.hex2dec(reversedID);

                    bytetransfer = bytetransfer + 1;

                    ////////////////////////////////



                    ////////////////sec//////////////



                    if (bytetransfer == 8)
                    {
                        itemSection = breader.ReadBytes(itemSectionEnd);
                        bytetransfer = 0;
                    }

                    id = BitConverter.ToString(itemSection, bytetransfer, 1);
                    strArrayID = id.Split(new char[] { '-' });
                    reversedID = strArrayID[0];
                    //  listBox1.Items.Add(this.hex2dec(reversedID) + "  :  " + "sec");


                    sec = this.hex2dec(reversedID);

                    bytetransfer = bytetransfer + 1;
                    ////////////////////////////////


                    DataRow oOrderRow = oDS.Tables["tbldatelog"].NewRow();

                    oOrderRow["yearval"] = year;
                    oOrderRow["mounthval"] = mounth;
                    oOrderRow["dayval"] = day;
                    oOrderRow["yeardaymounth"] = Shamsi;
                    oOrderRow["logfile"] = ftpFilename;
                    oOrderRow["hourval"] = hour;
                    oOrderRow["minval"] = min;


                    oDS.Tables["tbldatelog"].Rows.Add(oOrderRow);

                    da.Update(oDS, "tbldatelog");

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                }  ////baray catch



                catch (Exception)
                {
                    // break;
                    return;
                }






            }





























        }

        private void cmbfilelog_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtdatefrom.Text = cmbfilelog.Text;
        }

        private void lblfromdbtogrid_Click(object sender, EventArgs e)
        {



            initial_dtgshowdatapast();


            ////dtgshowdatapast.Rows.Clear();
            ////dtgshowdatapast.Columns.Clear();


            OleDbDataAdapter da = new OleDbDataAdapter();


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            //   myconn.Open();


            //         da.SelectCommand = new OleDbCommand("select * from tbldatadownload  where Shamsidate='" + cmbfilelog.SelectedItem.ToString() + "'  order by ordinalnumber", myconn);

            //  da.SelectCommand = new OleDbCommand("select * from tbldatadownload    order by ordinalnumber ", myconn);
            da.SelectCommand = new OleDbCommand("select * from tbldatadownload    order by  shamsidate,hourval,minval,secval,ordinalnumber ", myconn);


            da.Fill(ds, "tbldatadownload");
            dt = ds.Tables["tbldatadownload"];



            //    int k = ds.Tables[0].Rows.Count;

            string filename = "";




            //      Application.DoEvents();

            //       lblcountfiletransfer.Text = "فایل جهت استخراج" + " : " + ds.Tables[0].Rows.Count.ToString();


            //  string numcounter,ordinalnumber, year, mounth, day, dayofweek, hour, min, sec, channelindex, intvalue, floatvalue,channel_name,value;


            string numcounter, ordinalnumber, year, mounth, day, dayofweek, hour, min, sec, channelindex, intvalue, floatvalue, channel_name, value;


            year = ""; ordinalnumber = ""; year = ""; numcounter = ""; mounth = ""; day = ""; dayofweek = ""; hour = "";
            min = ""; sec = ""; year = ""; channelindex = ""; intvalue = ""; day = ""; floatvalue = ""; channel_name = ""; value = "";




            lbProgress2.Text = ds.Tables[0].Rows.Count.ToString();


            for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
            {



                ////filename = (ds.Tables[0].Rows[k][7].ToString());
                ////ftpFilename = filename;

                ordinalnumber = (ds.Tables[0].Rows[k][3].ToString());
                numcounter = k.ToString();

                year = (ds.Tables[0].Rows[k][4].ToString());
                mounth = (ds.Tables[0].Rows[k][5].ToString());
                day = (ds.Tables[0].Rows[k][6].ToString());
                dayofweek = (ds.Tables[0].Rows[k][8].ToString());
                hour = (ds.Tables[0].Rows[k][9].ToString());
                min = (ds.Tables[0].Rows[k][10].ToString());
                sec = (ds.Tables[0].Rows[k][11].ToString());

                channelindex = "";
                channel_name = "";
                value = "";




                //      dtgshowdatapast.Rows.Add(new object[] { numcounter.ToString(), ordinalnumber, year, mounth, day, dayofweek, hour, min, sec, channelindex, channel_name, value });
                dtgshowdatapast.Rows.Add(new object[] { numcounter.ToString(), ordinalnumber, year, mounth, day, dayofweek, hour, min, sec });



                string parsstring = "";
                parsstring = (ds.Tables[0].Rows[k][12].ToString());



                string[] parsstringArray = parsstring.Split('-');



                ////baray rain
                for (int i = 0; i < parsstringArray.Count(); i++)
                {



                    string internl = parsstringArray[i];

                    if (internl.Length < 1)
                    {

                        break;
                    }

                    string[] internlArraye = internl.Split(':');

                    channelindex = internlArraye[0];
                    channel_name = internlArraye[1];
                    value = internlArraye[2];


                    // string value = "0.67843";

                    Double tmp = Double.Parse(value);

                    //  value = internlArraye[2];
                    string kk = string.Format("{0:0.00}", tmp);

                    value = kk;





                    //  "{0:0.00}", 123.4567


                    int colIndex = 0;
                    if (k == 0)/////////////////////vaghty ke baray avalin bar sotonha ezafe mishavad
                    {


                        //////////////////
                        DataGridViewColumn col = new DataGridViewTextBoxColumn();

                        colIndex = dtgshowdatapast.Columns.Add(col);
                        dtgshowdatapast.Columns[colIndex].HeaderText = channel_name.ToString();
                        dtgshowdatapast.Rows[k].Cells[colIndex].Value = value.ToString();
                        ////////////


                        //////////DataGridViewColumn col2 = new DataGridViewTextBoxColumn();

                        //////////colIndex = dtgshowdatapast.Columns.Add(col2);
                        //////////dtgshowdatapast.Columns[colIndex].HeaderText = "Channel Name";
                        //////////dtgshowdatapast.Rows[k].Cells[colIndex].Value = channel_name.ToString();
                        /////////////////////
                        //////////DataGridViewColumn col3 = new DataGridViewTextBoxColumn();

                        //////////colIndex = dtgshowdatapast.Columns.Add(col3);
                        //////////dtgshowdatapast.Columns[colIndex].HeaderText = "Value";
                        //////////dtgshowdatapast.Rows[k].Cells[colIndex].Value = value.ToString();






                    }

                    else
                    {

                        //     return;

                        //    int h = i * 3;
                        int h = i + 1;




                        if ((h + 8) >= dtgshowdatapast.Rows[k].Cells.Count)
                        {

                            DataGridViewColumn col = new DataGridViewTextBoxColumn();

                            colIndex = dtgshowdatapast.Columns.Add(col);
                            dtgshowdatapast.Columns[colIndex].HeaderText = channel_name.ToString();
                            dtgshowdatapast.Rows[k].Cells[colIndex].Value = value.ToString();


                        }

                        else
                        {
                            dtgshowdatapast.Rows[k].Cells[h + 8].Value = value.ToString();

                        }



                        ////dtgshowdatapast.Rows[k].Cells[h + 1 + 9].Value = channel_name.ToString();
                        ////dtgshowdatapast.Rows[k].Cells[h + 2 + 9].Value = value.ToString();





                    }



                }











                //  value = (ds.Tables[0].Rows[k][12].ToString());
                //     dtgshowdatapast.Rows.Add(new object[] { numcounter.ToString(), ordinalnumber, year, mounth, day, dayofweek, hour, min, sec, channelindex, channel_name, value });

            }



            //lblcountfiletransfer.Text = "پایان";


        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

            dtgshowdatapast.Rows.Clear();
            dtgshowdatapast.Columns.Clear();




            Application.DoEvents();
            Application.DoEvents();


            prgstatusftbdownload2.Value = 0;


            //return;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();


            DataSet ds2 = new DataSet();
            DataTable dt2 = new DataTable();

            OleDbDataAdapter da3 = new OleDbDataAdapter();
            da3.SelectCommand = new OleDbCommand("select * from tbldatadownload ", myconn);
            da3.Fill(ds, "tbldatadownload");
            dt = ds.Tables["tbldatadownload"];

            for (int p = 0; p < dt.Rows.Count; p++)
            {

                dt.Rows[p].Delete();


            }


            da3.DeleteCommand = new OleDbCommand("delete  from tbldatadownload ", myconn);



            try
            {
                da3.Update(dt);
            }
            catch (Exception ex)
            {
                // throw ex;
            }

            //////////////////////////////








            ////////////////////////////////////////////////////////////////  


            OleDbDataAdapter da = new OleDbDataAdapter();

            ds = new DataSet();
            dt = new DataTable();



            OleDbDataAdapter da1 = new OleDbDataAdapter();

            ds1 = new DataSet();
            dt1 = new DataTable();


            OleDbDataAdapter da2 = new OleDbDataAdapter();

            ds2 = new DataSet();
            dt2 = new DataTable();


            //da.SelectCommand = new OleDbCommand("select * from tbldatelog  where yeardaymounth='" + cmbfilelog.SelectedItem.ToString() + "'  order by hourval", myconn);


            ///first we found the date for lower range
            //order by yeardaymounth desc

            String strDate = "", strDate_To;

            ////strDate = txtdatefrom.Text.Trim();
            ////strDate_To = txtdateTo.Text.Trim();

            strDate = Pcal_txtdatefrom.Text.Trim();
            strDate_To = pcal_txtdateTo.Text.Trim();




            //   strDate = "1392/08/17";

            da.SelectCommand = new OleDbCommand("select * from tbldatelog  where yeardaymounth < '" + strDate + "' order by yeardaymounth desc ", myconn);

            da.Fill(ds, "tbldatelog");
            dt = ds.Tables["tbldatelog"];

            string date_first_found = "";

            int r1 = ds.Tables[0].Rows.Count;

            if (r1 > 0)
            {

                date_first_found = (ds.Tables[0].Rows[0][4].ToString());

            }
            else
            {


                ds.Tables[0].Clear();
                dt.Clear();

                //        myconn.Open();
                // da.UpdateCommand();


                da.SelectCommand = new OleDbCommand("select * from tbldatelog  where yeardaymounth = '" + strDate + "' order by yeardaymounth desc ", myconn);
                da.Fill(ds, "tbldatelog");
                dt = ds.Tables["tbldatelog"];

                //  string date_first_found = "";

                int r5 = ds.Tables[0].Rows.Count;
                if (r5 > 0)
                {

                    date_first_found = (ds.Tables[0].Rows[0][4].ToString());

                }


            }










            ///

            ///tarikh Akhar
            ///


            da1.SelectCommand = new OleDbCommand("select * from tbldatelog  where yeardaymounth > '" + strDate_To + "' order by yeardaymounth ", myconn);

            da1.Fill(ds1, "tbldatelog");
            dt1 = ds1.Tables["tbldatelog"];

            string date_last_found = "";

            int r2 = ds1.Tables[0].Rows.Count;

            if (r2 > 0)
            {

                date_last_found = (ds1.Tables[0].Rows[0][4].ToString());

            }
            else
            {

                ds1.Tables[0].Clear();
                dt1.Clear();

                da1.SelectCommand = new OleDbCommand("select * from tbldatelog  where yeardaymounth = '" + strDate_To + "' order by yeardaymounth ", myconn);



                da1.Fill(ds1, "tbldatelog");
                dt1 = ds1.Tables["tbldatelog"];

                date_last_found = "";

                r2 = ds1.Tables[0].Rows.Count;

                if (r2 > 0)
                {

                    date_last_found = (ds1.Tables[0].Rows[0][4].ToString());

                }



            }


            ///


            if (date_first_found.Equals("") == true || date_last_found.Equals("") == true)
            {

                MessageBox.Show("رکورد ذخیره شده ای در این تاریخ وجود ندارد");
                return;


            }



            ///



            ////     return;


            da2.SelectCommand = new OleDbCommand("select * from tbldatelog  where yeardaymounth >='" + date_first_found + "' and yeardaymounth <='" + date_last_found + "' order by yeardaymounth", myconn);

            //     da.SelectCommand = new OleDbCommand("select * from tbldatelog  where yeardaymounth >='" + txtdatefrom.Text + "' and yeardaymounth <='" + txtdateTo.Text + "' order by yeardaymounth", myconn);
            //   da.SelectCommand = new OleDbCommand("select * from tbldatelog  where yeardaymounth >='" + txtdatefrom.Text + "' ", myconn);
            //    da.SelectCommand = new OleDbCommand("select * from tbldatelog  where yeardaymounth >='" + cmbfilelog.SelectedItem.ToString() + "' ", myconn);




            da2.Fill(ds2, "tbldatelog");
            dt2 = ds2.Tables["tbldatelog"];



            int r3 = ds2.Tables[0].Rows.Count;

            if (r3 == 0)
            {

                MessageBox.Show("رکورد ذخیره شده ای در این تاریخ وجود ندارد");
                return;


            }


            string filename = "";




            Application.DoEvents();

            lblcountfiletransfer.Text = "فایل جهت استخراج" + " : " + ds2.Tables[0].Rows.Count.ToString();



            int cntfile = ds2.Tables[0].Rows.Count;
            int vahed = 100 / cntfile;

            prgstatusftbdownload2.Maximum = 100;



            for (int k = 0; k < ds2.Tables[0].Rows.Count; k++)
            {
                filename = (ds2.Tables[0].Rows[k][7].ToString());





                ftpFilename = filename;




                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();

                label58_Click(null, null);
                Application.DoEvents();
                Application.DoEvents();

                txtData2.Text = (k + 1).ToString();
                prgstatusftbdownload2.Value = prgstatusftbdownload2.Value + vahed;


            }

            prgstatusftbdownload2.Value = 100;

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            lblfromdbtogrid_Click(null, null);


            Application.DoEvents();
            Application.DoEvents();

            label86.Visible = true;
            // txtdatefrom.Visible = true;

            ////txtsearchgrid.Visible = true;
            ////cmbselextsearch.Visible = true;


            // txtsearch_in_grid.Text = txtdatefrom.Text;
            txtsearch_in_grid.Text = Pcal_txtdatefrom.Text;
            txtsearch_in_grid.Visible = true;
            cmbselextsearch.Text = "روز";

            if (dtgshowdatapast.Rows.Count > 0)
            {

                cmb_select_sensors.Items.Clear();



                for (int i = 9; i < dtgshowdatapast.ColumnCount ; i++)
                {
                   
                
                    string a = dtgshowdatapast.Columns[i].HeaderText.ToString();
                    cmb_select_sensors.Items.Add(a);
                  

                
                }

            }




        }

        private void label59_Click_1(object sender, EventArgs e)
        {



            string Day = "", Month = "", Year = "", hour = "", min = "", sec = "";
            DateTime _date = DateTime.Now;

            int count = 0;
            string format = "dd-MM-yyyy-HH-mm-ss";


            string sDate = _date.ToString(format);


            string[] Words = sDate.Split(new char[] { '-' });

            foreach (string Word in Words)
            {
                count += 1;
                if (count == 1) { Day = Word; }
                if (count == 2) { Month = Word; }
                if (count == 3) { Year = Word; }
                if (count == 4) { hour = Word; }
                if (count == 5) { min = Word; }
                if (count == 6) { sec = Word; }


            }

            string datetimesave = "date -s " + "'" + Year + "-" + Month + "-" + Day + " " + hour + ":" + min + ":" + sec + "'";


            //1-  aval file havy etelaat tarikh ra dar system copy mikonim

            //2 badan dastoore 2 ra dar system gharar midahim

            ////////////////////////////////////////////////////////////////// gharar danae tarikh dar file marboot va ersal////////////

            string filename, filePath, FTPAddresssend;


            System.IO.StreamWriter filewriter2 = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\TestThreadDateTime.log");
            string savedata = "";

            savedata = datetimesave;
            filewriter2.Write(savedata);
            filewriter2.Close();


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            if (listboxFiles.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }



            //   string filename, filePath, FTPAddresssend;
            FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//applog//";
            prgstatusftbdownload.Value = 0;
            filename = "TestThreadDateTime.log";


            filePath = Application.StartupPath + "\\configExport\\";



            ftpusername = "root";
            ftppassword = "password";


            FtpWebRequest request2 = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


            request2.Method = WebRequestMethods.Ftp.UploadFile;
            request2.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request2.UsePassive = true;
            request2.UseBinary = true;
            request2.KeepAlive = false;

            //Load the file
            FileStream stream2 = File.OpenRead(filePath + filename);
            byte[] buffer2 = new byte[stream2.Length];

            stream2.Read(buffer2, 0, buffer2.Length);
            stream2.Close();

            //Upload file
            Stream reqStream2 = request2.GetRequestStream();
            reqStream2.Write(buffer2, 0, buffer2.Length);
            reqStream2.Close();



            lblstatusftp.Text = "پایان تنظیم ساعت و زمان   ";
            prgstatusftbdownload.Value = prgstatusftbdownload.Maximum;

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            //////////////////////////////////////////////////////////



            ////////////////////////////////////////////////////////// 2 karadane meghdare file reset baraye fahmidane khandane date & tiem

            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\TestThread.log");

            savedata = "2";
            filewriter.Write(savedata);
            filewriter.Close();


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            if (listboxFiles.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }



            // string filename, filePath, FTPAddresssend;
            FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//applog//";
            prgstatusftbdownload.Value = 0;
            filename = "TestThread.log";


            filePath = Application.StartupPath + "\\configExport\\";



            ftpusername = "root";
            ftppassword = "password";


            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(filePath + filename);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();



            lblstatusftp.Text = "ارتباط با سیستم  ";
            prgstatusftbdownload.Value = prgstatusftbdownload.Maximum;

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            //////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////





















        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            int b = 2;


        }

        private void cmbsaving_SelectedIndexChanged(object sender, EventArgs e)
        {


            string aa = "";
            string bb = "";

            string cc = "";

            string tmpvahed = "";





            aa = (cmbsampling.Text);

            if (aa.IndexOf("min") != -1)
            {
                tmpvahed = "min";

            }


            if (aa.IndexOf("sec") != -1)
            {
                tmpvahed = "sec";

            }


            if (aa.IndexOf("hour") != -1)
            {
                tmpvahed = "hour";

            }


            if (aa.IndexOf("day") != -1)
            {
                tmpvahed = "day";

            }




            aa = aa.Replace(tmpvahed, "");

            int a;
            a = int.Parse(aa);

            //int tmpa,tmpb,tmpc ;

            int vaheddevide = 0;


            if (tmpvahed.Equals("sec") == true)
            {
                a = a * 1;
                vaheddevide = 1;



            }




            if (tmpvahed.Equals("min") == true)
            {
                a = a * 60;
                vaheddevide = 60;


            }



            if (tmpvahed.Equals("hour") == true)
            {
                a = a * 3600;
                vaheddevide = 3600;

            }




            if (tmpvahed.Equals("day") == true)
            {
                a = a * 86400;
                vaheddevide = 86400;

            }



            tmpA = (a / 5);
            tmpC = 5;




            int sampling = a * int.Parse(cmbsaving.Text.ToString());


            tmpB = (sampling / a);


            //     cmbsaving.Text = ((a * tmpB) / vaheddevide).ToString() + tmpvahed;



            txtsaving.Text = (sampling / vaheddevide).ToString() + tmpvahed;


            txtsending.Text = (tmpA * tmpB * tmpC * tmpC / vaheddevide).ToString() + tmpvahed;


            //    cmbsending.Text = ((a * tmpB * tmpC) / vaheddevide).ToString() + tmpvahed;








        }

        private void cmbsending_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label62_Click(object sender, EventArgs e)
        {



            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\rdlsys_client.info";

            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            //  readline = (Filereader.ReadLine());

            readline = Filereader.ReadLine();


            while ((readline = Filereader.ReadLine()) != null)
            {








                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }



                if (readline == "")
                {


                    continue;


                }






                if (readline == null)
                {

                    return;

                }

                string s = readline;

                string[] values = s.Split(',');

                string guid = values[0];


                glbguidID = guid;
                glbguidID_zap = glbguidID;



            }


            Filereader.Close();



            string path = Application.StartupPath + "\\datalog\\" + glbguidID;

            if (!Directory.Exists(path))
            {

                Directory.CreateDirectory(path);

            }





        }

        private void label63_Click(object sender, EventArgs e)
        {

            //   dim myValue as String = InputBox("Enter Value", "Enter Value", "Please Enter Value")

            pnlsms.Left = 316;

            pnlsms.Top = 39;


            pnlsms.Visible = true;


        }

        private void label69_Click(object sender, EventArgs e)
        {
            pnlsms.Visible = false;

        }

        private void label68_Click(object sender, EventArgs e)
        {


            if (txtnumberphone.Text == "")
            {
                txtnumberphone.Text = "09125371360";
            }


            if (txtmatnsms.Text == "")
            {
                txtmatnsms.Text = "salamreza";
            }



            //1-  aval file havy etelaat tarikh ra dar system copy mikonim

            //2 badan dastoore 2 ra dar system gharar midahim

            ////////////////////////////////////////////////////////////////// gharar danae tarikh dar file marboot va ersal////////////




            string filename, filePath, FTPAddresssend;


            System.IO.StreamWriter filewriter2 = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\rdlsys_client_SMS_Number_Text.conf");
            string savedata = "";



            string strtmp = "";



            strtmp = txtmatnsms.Text;
            strtmp = "salamreza";/// for this system we only put this to ,and in the system we put our text.
            int a = txtmatnsms.Text.Length;



            if (a < 13)
            {

                strtmp = strtmp + "               ";
            }




            string tmptime = "";
            int tmpinttime = 0;

            tmptime = cmbrecieveTimeSMS.Text;
            tmptime = tmptime.Replace("min", "");

            tmpinttime = int.Parse(tmptime) * 60;

            tmptime = tmpinttime.ToString();


            savedata = txtnumberphone.Text + "," + strtmp + "," + tmptime;






            filewriter2.Write(savedata);
            filewriter2.Close();


            //  return;


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            if (listboxFiles.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }



            //   string filename, filePath, FTPAddresssend;
            FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//config//";
            prgstatusftbdownload.Value = 0;
            filename = "rdlsys_client_SMS_Number_Text.conf";


            filePath = Application.StartupPath + "\\configExport\\";



            ftpusername = "root";
            ftppassword = "password";


            FtpWebRequest request2 = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


            request2.Method = WebRequestMethods.Ftp.UploadFile;
            request2.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request2.UsePassive = true;
            request2.UseBinary = true;
            request2.KeepAlive = false;

            //Load the file
            FileStream stream2 = File.OpenRead(filePath + filename);
            byte[] buffer2 = new byte[stream2.Length];

            stream2.Read(buffer2, 0, buffer2.Length);
            stream2.Close();

            //Upload file
            Stream reqStream2 = request2.GetRequestStream();
            reqStream2.Write(buffer2, 0, buffer2.Length);
            reqStream2.Close();






        }

        private void cmbsampling_DropDownClosed(object sender, EventArgs e)
        {

            ////////cmbsaving.Items.Clear();
            ////////cmbsaving.Items.Add("2");
            ////////cmbsaving.Items.Add("3");
            ////////cmbsaving.Items.Add("4");
            ////////cmbsaving.Items.Add("5");
            ////////cmbsaving.Items.Add("6");
            ////////cmbsaving.Items.Add("7");
            ////////cmbsaving.Items.Add("8");
            ////////cmbsaving.Items.Add("9");
            ////////cmbsaving.Items.Add("10");




            ////cmbsaving.SelectedItem = 0;
            ////cmbsaving.Text = "2";





        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            int pp = datagridloadtempdata.Rows.Count;


            datagridloadtempdata.Rows[0].Visible = false;

            //      datagridloadtempdata.Rows[1]. = false;

            ////datagridloadtempdata.Rows[2].Height = 0;
            ////datagridloadtempdata.Rows[3].Height = 0;
            ////datagridloadtempdata.Rows[4].Height = 0;
            ////datagridloadtempdata.Rows[5].Height = 0;
            ////datagridloadtempdata.Rows[6].Height = 0;
            ////datagridloadtempdata.Rows[7].Height = 0;
            ////datagridloadtempdata.Rows[8].Height = 0;
            ////datagridloadtempdata.Rows[9].Height = 0;

            ////datagridloadtempdata.Rows[0].Height = 0;
            ////datagridloadtempdata.Rows[1].Height = 0;
            ////datagridloadtempdata.Rows[2].Height = 0;
            ////datagridloadtempdata.Rows[3].Height = 0;
            ////datagridloadtempdata.Rows[4].Height = 0;
            ////datagridloadtempdata.Rows[5].Height = 0;
            ////datagridloadtempdata.Rows[6].Height = 0;
            ////datagridloadtempdata.Rows[7].Height = 0;
            ////datagridloadtempdata.Rows[8].Height = 0;
            ////datagridloadtempdata.Rows[9].Height = 0;


        }

        private void datagridloadtempcalibrationdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label72_Click(object sender, EventArgs e)
        {

        }

        private void label79_Click(object sender, EventArgs e)
        {


            dlgOpenFile.FileName = "Rain_config.conf";


            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\Rain_config.conf";


            Application.DoEvents();
            Application.DoEvents();







            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            while ((readline = Filereader.ReadLine()) != null)
            {



                lblstatus.Text = "در حال بار گذاری اطلاعات " + "...............";


                if (progfetchsensors.Value + 2 < 100)
                {

                    progfetchsensors.Value = progfetchsensors.Value + 2;

                }




                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }



                if (readline == "")
                {


                    continue;


                }





                string s = readline;
                string[] values = s.Split(',');




                String tmpgprs, tmprecievesms, tmpsendsms, tmptimestoprain, tmptimealarm1, tmpvalalarm1, tmpval5min, tmpvalnameistgah, tmpimebeetweensms;


                tmpgprs = "";
                tmprecievesms = "";
                tmpsendsms = "";
                tmptimestoprain = "";
                tmptimealarm1 = "";
                tmpvalalarm1 = "";
                tmpval5min = "";
                tmpvalnameistgah = "";
                tmpimebeetweensms = "";

                if (values.Count() == 9)
                {
                    tmpgprs = values[0];
                    tmprecievesms = values[1];
                    tmpsendsms = values[2];
                    tmptimestoprain = values[3];
                    tmptimealarm1 = values[4];
                    tmpvalalarm1 = values[5];
                    tmpval5min = values[6];
                    tmpvalnameistgah = values[7];
                    tmpimebeetweensms = values[8];

                }


                //////agar 6 ta bood marboot be  khat tanzimat ip,serevr hosr va port as 


                if (Equals(tmpgprs, "1"))
                {
                    chkgprs.Checked = true;
                }
                else
                {
                    chkgprs.Checked = false;
                }

                /////


                if (Equals(tmprecievesms, "1"))
                {
                    chkrecievesms.Checked = true;
                }
                else
                {
                    chkrecievesms.Checked = false;
                }
                //////


                if (Equals(tmpsendsms, "1"))
                {
                    chksendsms.Checked = true;
                }
                else
                {
                    chksendsms.Checked = false;
                }

                tmptimealarm1 = values[4];
                tmpvalalarm1 = values[5];
                tmpval5min = values[6];


                txttimestoprain.Text = tmptimestoprain.ToString();
                txttimealarm1.Text = tmptimealarm1.ToString();
                txtvalalarm1.Text = tmpvalalarm1.ToString();
                txtvalalarm5min.Text = tmpval5min.ToString();
                txtvalalarm5min.Text = tmpval5min.ToString();
                txtnameIstgah.Text = tmpvalnameistgah.ToString();
                txttimebeetweensms.Text = tmpimebeetweensms.ToString();








                if (readline == null)
                {


                    break;


                }





                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }




            }/////end op loof reading file









            Filereader.Close();

            progfetchsensors.Value = 100;


            Application.DoEvents();


            //  lblstatus.Text = "پایان بارگذاری اطلاعات";

            // tabControl1.SelectedTab = tabPage3;

            //////////////////////set time 

            DateTime theDate = DateTime.UtcNow;


            string customdate = theDate.ToString("d");

            lbltime.Text = DateTime.Now.ToLongTimeString().ToString();

            lbldate.Text = customdate;
            ///////////////////////////





        }

        private void label80_Click(object sender, EventArgs e)
        {

            //   lblstatus.Text = "در حال تنظیم فایل خروجی";
            progfetchsensors.Value = 2;


            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\Rain_config.conf");



            String tmpgprs, tmprecievesms, tmpsendsms, tmptimestoprain, tmptimealarm1, tmpvalalarm1, tmpval5min, tmpvalnameistgah, tmpimebeetweensms;

            tmpimebeetweensms = "";

            tmpgprs = "";
            tmprecievesms = "";
            tmpsendsms = "";
            tmptimestoprain = "";
            tmptimealarm1 = "";
            tmpvalalarm1 = "";
            tmpval5min = "";
            tmpvalnameistgah = "";

            if (chkrecievesms.Checked == true || chksendsms.Checked == true)
            {
                chkgprs.Checked = true;


            }






            if (chkgprs.Checked == true)
            {
                tmpgprs = "1";
            }
            else
            {
                tmpgprs = "0";
            }

            /////


            if (chkrecievesms.Checked == true)
            {
                tmprecievesms = "1";
            }
            else
            {
                tmprecievesms = "0";
            }
            //////



            if (chksendsms.Checked == true)
            {
                tmpsendsms = "1";
            }
            else
            {
                tmpsendsms = "0";
            }


            tmptimestoprain = txttimestoprain.Text;
            tmptimealarm1 = txttimealarm1.Text;
            tmpvalalarm1 = txtvalalarm1.Text;
            tmpval5min = txtvalalarm5min.Text;
            tmpvalnameistgah = txtnameIstgah.Text;
            tmpimebeetweensms = txttimebeetweensms.Text;



            string tmpsave;





            tmpsave = tmpgprs + "," + tmprecievesms + "," + tmpsendsms + "," + tmptimestoprain + "," + tmptimealarm1 + "," + tmpvalalarm1 + "," + tmpval5min + "," + tmpvalnameistgah + "," + tmpimebeetweensms;



            //////write data for time
            filewriter.WriteLine(tmpsave);
            ////////////
            filewriter.WriteLine("");
            filewriter.WriteLine("### ### Help ");
            filewriter.WriteLine("### ### 1-Gprs,2-recieve sms,3-send sms,4-time stop rain,5-time alarm ");
            filewriter.WriteLine("###rain,6-value Alarm rain in time,7-value ###rain in 5 min");
            filewriter.WriteLine("");

            filewriter.Close();

            progfetchsensors.Value = 100;
            //   lblstatus.Text = "پایان مرحله خروجی فایل";



        }

        private void chkrecievesms_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkrecievesms_Click(object sender, EventArgs e)
        {





        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {


            ////if (glbguid == false)
            ////{

            ////    MessageBox.Show("شما مجاز به استفاده از نرم افزار نیستید");
            ////    Application.DoEvents();
            ////    tabcontrol2.Focus();

            ////    return;



            ////}



            picsaveexcelllogsms.Visible = false;

            lblname_station.Visible = false;


            label94.Visible = true;
            show_time_log = false;
            picshowdatetime.Visible = true;
            picshowlogsms.Visible = true;
            lblshowdate_realTime.Visible = true;
            lblshowtime_realTime.Visible = true;
            label91.Visible = true;

            tabcontrol2.SelectedTab = tabPage7;

            label86.Visible = false;
            groupBox12.Visible = true;
            picexporttest.Visible = false;
            picexportexcell.Visible = false;
            picexporttest.Visible = false;
            label61.Visible = false;
            cmbfilelog.Visible = false;
            cmbfilelogTo.Visible = false;
            pictureBox10.Visible = false;
            label60.Visible = false;
            groupBox12.Enabled = true;

            txtdatefrom.Visible = false;
            txtdateTo.Visible = false;

            Pcal_txtdatefrom.Visible = false;
            pcal_txtdateTo.Visible = false;

            label85.Visible = false;
            txtnamestation.Visible = false;
            chkautomated.Visible = true;
            cmbselextsearch.Visible = false;
            label85.Visible = false;


            ////cmbnamestation.Visible = false;
            ////txtsearchgrid.Visible = false;

            txtsearch_in_grid.Visible = false;


            ////rd3sec.Visible = true;
            ////rd5sec.Visible = true;
            ////rd10sec.Visible = true;


            rdcalculated.Checked = true;




        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            label94.Visible = false;
            picsaveexcelllogsms.Visible = false;
            label91.Visible = false;
            picshowdatetime.Visible = false;
            picshowlogsms.Visible = false;
            lblshowdate_realTime.Visible = false;
            lblshowtime_realTime.Visible = false;

            tabcontrol2.SelectedTab = tabPage8;
            lblshowdate_realTime.Visible = false;
            lblshowtime_realTime.Visible = false;
            groupBox12.Visible = false;

            picexportexcell.Visible = true;
            picexporttest.Visible = true;
            label61.Visible = true;
            // cmbfilelog.Visible = true;
            //cmbfilelogTo.Visible = true;
            pictureBox10.Visible = true;
            label60.Visible = true;
            // label86.Visible = true;
            groupBox12.Enabled = false;


            ////txtdatefrom.Visible = true;
            ////txtdateTo.Visible = true;

            Pcal_txtdatefrom.Visible = true;
            pcal_txtdateTo.Visible = true;

            chkautomated.Visible = false;

            chkautomated.Checked = false;


            tmrfetchdata.Enabled = false;

            label85.Visible = true;
            cmbnamestation.Visible = false;
            label85.Visible = false;

            cmbnamestation.Text = "";


            rd3sec.Visible = false;
            rd5sec.Visible = false;
            rd10sec.Visible = false;

            loaddatatocombostation();


        }

        private void label81_Click(object sender, EventArgs e)
        {



           ////// DateTime dateNow = DateTime.Now;

          
           ////// Console.WriteLine("The date and time are {0} UTC.",
           //////                    TimeZoneInfo.ConvertTimeToUtc(dateNow));

           ////// ////dateNow.AddHours(2.0);
           ////// ////dateNow.AddMinutes(30);

           ////// string tmp = "11/24/2016 08:44:31 AM";
           //////// dateNow = tmp.ToString();
           //////dateNow= DateTime.Parse(tmp);

           //////Console.WriteLine(dateNow.ToString());

           ////// TimeSpan offSet = TimeSpan.Parse("03:30:00");
           ////// DateTime newDateTime = dateNow + offSet;
            
            
           ////// return;




            pnlCalibration.Visible = false;
            pnlSensors.Visible = false;

            pnlshowdata.Visible = false;
            pnlloaddata.Visible = false;

            pnlhashcode.Visible = false;
            pnldatatransfer.Visible = false;
            pnlshowdata.Visible = false;
            pnlloaddata.Visible = false;
            pnl_show_digital_rain.Visible = false;
            pnlshowgauge.Visible = false;

            pnlSensors.Visible = false;
            pnlCalibration.Visible = false;
            pnlweb.Visible = false;
            pnlhelp.Visible = true;




            rthelp.LoadFile(Application.StartupPath + "\\screen\\help.rtf");
            //  rthelp.LoadFile(Application.StartupPath + "\\screen\\2.docx");
            //   rthelp.RightToLeft=rthelp.RightToLeft;



            //  webshow.Navigate(Application.StartupPath + "\\screen\\help.rtf");

            //     webshow.Navigate("uas.co.ir");

            //      webshow.Navigate("c:\\game\\");





        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            pnlhelp.Visible = false;
        }

        private void pnlloaddata_Paint(object sender, PaintEventArgs e)
        {

        }



        private void pictureBox19_Click(object sender, EventArgs e)
        {


            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد راه اندازی مجدد سیستم باران را دارید ؟";

            caption = "راه اندازی مجدد سیستم" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }





            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\TestThread.log");






            string savedata, strtime, strdate;




            ///// agar 3 gharar dadim anvaght braye tanzim date bashd va 2 khat paiin ra bekhanad.
            ///yek gozine dar barname bezarim baraye tanzim date 
            /////  savedata = "3";

            //  savedata = "1" + "\r" + "22" + "\r" + "33";

            savedata = "1";






            ////filewriter.WriteLine(savedata);
            ////filewriter.WriteLine(savedata);
            ////filewriter.WriteLine(savedata);
            filewriter.Write(savedata);




            //// //////DateTime theDate = DateTime.UtcNow;
            //// //////string customdate = theDate.ToString("d");

            //// //////strtime = DateTime.Now.ToLongTimeString().ToString();
            //// //////strdate = customdate;


            //// //////filewriter.WriteLine(strdate);
            //// //////filewriter.WriteLine(strtime);



            //// filewriter.WriteLine("2");
            //// filewriter.WriteLine("3");


            filewriter.Close();


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            if (listboxFiles.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }



            string filename, filePath, FTPAddresssend;




            FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//applog//";



            prgstatusftbdownload.Value = 0;







            filename = "TestThread.log";


            filePath = Application.StartupPath + "\\configExport\\";



            ftpusername = "root";
            ftppassword = "password";



            //Create FTP request
            //  FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(filePath + filename);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();

            //  MessageBox.Show("Uploaded Successfully");

            lblstatus.Text = "راه اندازی مجدد سیستم ";
            progfetchsensors.Value = progfetchsensors.Maximum;

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


        }

        private void picsettime_Click(object sender, EventArgs e)
        {


            //  rdinstant.Checked = false;
            rdinstant.Checked = true;

            pictureBox11_Click(null, null);


            Application.DoEvents();
            Application.DoEvents();

            lbltime2.Text = lblshowdate_realTime.Text;
            lbldate2.Text = lblshowtime_realTime.Text;


            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد تغییر ساعت و تاریخ سیستم باران را دارید ؟";

            caption = "تغییر ساعت و تاریخ سیستم" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }



            if (glb_autorite_change_date == false)
            {

                System.Windows.Forms.MessageBox.Show("شما مجاز به تغییر تاریخ و زمان سیستم نیستید", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }







            DateTime theDate = DateTime.UtcNow;
            string customdate = theDate.ToString("d");
            //  string custom = theDate.ToString("hh:mm:ss tt");
            lbltime2.Text = DateTime.Now.ToLongTimeString().ToString();
            lbldate2.Text = customdate;

            //////////////////////////

            string Day = "", Month = "", Year = "", hour = "", min = "", sec = "";
            DateTime _date = DateTime.Now;

            int count = 0;
            string format = "dd-MM-yyyy-HH-mm-ss";


            string sDate = _date.ToString(format);


            string[] Words = sDate.Split(new char[] { '-' });

            foreach (string Word in Words)
            {
                count += 1;
                if (count == 1) { Day = Word; }
                if (count == 2) { Month = Word; }
                if (count == 3) { Year = Word; }
                if (count == 4) { hour = Word; }
                if (count == 5) { min = Word; }
                if (count == 6) { sec = Word; }


            }

            string datetimesave = "date -s " + "'" + Year + "-" + Month + "-" + Day + " " + hour + ":" + min + ":" + sec + "'";


            //1-  aval file havy etelaat tarikh ra dar system copy mikonim

            //2 badan dastoore 2 ra dar system gharar midahim

            ////////////////////////////////////////////////////////////////// gharar danae tarikh dar file marboot va ersal////////////

            string filename, filePath, FTPAddresssend;


            System.IO.StreamWriter filewriter2 = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\TestThreadDateTime.log");
            string savedata = "";

            savedata = datetimesave;
            filewriter2.Write(savedata);
            filewriter2.Close();


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            if (listboxFiles.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }



            //   string filename, filePath, FTPAddresssend;
            FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//applog//";
            prgstatusftbdownload.Value = 0;
            filename = "TestThreadDateTime.log";


            filePath = Application.StartupPath + "\\configExport\\";



            ftpusername = "root";
            ftppassword = "password";


            FtpWebRequest request2 = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


            request2.Method = WebRequestMethods.Ftp.UploadFile;
            request2.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request2.UsePassive = true;
            request2.UseBinary = true;
            request2.KeepAlive = false;

            //Load the file
            FileStream stream2 = File.OpenRead(filePath + filename);
            byte[] buffer2 = new byte[stream2.Length];

            stream2.Read(buffer2, 0, buffer2.Length);
            stream2.Close();

            //Upload file
            Stream reqStream2 = request2.GetRequestStream();
            reqStream2.Write(buffer2, 0, buffer2.Length);
            reqStream2.Close();



            lblstatusftp.Text = "پایان تنظیم ساعت و زمان   ";
            prgstatusftbdownload.Value = prgstatusftbdownload.Maximum;

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            //////////////////////////////////////////////////////////



            ////////////////////////////////////////////////////////// 2 karadane meghdare file reset baraye fahmidane khandane date & tiem

            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\TestThread.log");

            savedata = "2";
            filewriter.Write(savedata);
            filewriter.Close();


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            if (listboxFiles.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }



            // string filename, filePath, FTPAddresssend;
            FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//applog//";
            prgstatusftbdownload.Value = 0;
            filename = "TestThread.log";


            filePath = Application.StartupPath + "\\configExport\\";



            ftpusername = "root";
            ftppassword = "password";


            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(filePath + filename);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();



            lblstatusftp.Text = "ارتباط با سیستم  ";
            progfetchsensors.Value = progfetchsensors.Maximum;

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            //////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////
















        }

        private void picsetrecievetime_Click(object sender, EventArgs e)
        {


            pnlsms.Left = 316;

            pnlsms.Top = 39;


            pnlsms.Visible = true;
            pnlsms.BringToFront();



        }

        private void label68_Click_1(object sender, EventArgs e)
        {

            if (txtnumberphone.Text == "")
            {
                txtnumberphone.Text = "09125371360";
            }


            if (txtmatnsms.Text == "")
            {
                txtmatnsms.Text = "salamreza";
            }



            //1-  aval file havy etelaat tarikh ra dar system copy mikonim

            //2 badan dastoore 2 ra dar system gharar midahim

            ////////////////////////////////////////////////////////////////// gharar danae tarikh dar file marboot va ersal////////////




            string filename, filePath, FTPAddresssend;


            System.IO.StreamWriter filewriter2 = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\rdlsys_client_SMS_Number_Text.conf");
            string savedata = "";



            string strtmp = "";

            strtmp = txtmatnsms.Text;
            int a = txtmatnsms.Text.Length;



            if (a < 13)
            {

                strtmp = strtmp + "               ";
            }




            string tmptime = "";
            int tmpinttime = 0;

            tmptime = cmbrecieveTimeSMS.Text;
            tmptime = tmptime.Replace("min", "");

            tmpinttime = int.Parse(tmptime) * 60;

            tmptime = tmpinttime.ToString();


            savedata = txtnumberphone.Text + "," + strtmp + "," + tmptime;






            filewriter2.Write(savedata);
            filewriter2.Close();


            //  return;


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            ////////////if (listboxFiles.Items.Count == 0)
            ////////////{

            ////////////    MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
            ////////////    return;


            ////////////}



            //////////////   string filename, filePath, FTPAddresssend;
            ////////////FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//config//";
            ////////////prgstatusftbdownload.Value = 0;
            ////////////filename = "rdlsys_client_SMS_Number_Text.conf";


            ////////////filePath = Application.StartupPath + "\\configExport\\";



            ////////////ftpusername = "root";
            ////////////ftppassword = "password";


            ////////////FtpWebRequest request2 = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


            ////////////request2.Method = WebRequestMethods.Ftp.UploadFile;
            ////////////request2.Credentials = new NetworkCredential(ftpusername, ftppassword);
            ////////////request2.UsePassive = true;
            ////////////request2.UseBinary = true;
            ////////////request2.KeepAlive = false;

            //////////////Load the file
            ////////////FileStream stream2 = File.OpenRead(filePath + filename);
            ////////////byte[] buffer2 = new byte[stream2.Length];

            ////////////stream2.Read(buffer2, 0, buffer2.Length);
            ////////////stream2.Close();

            //////////////Upload file
            ////////////Stream reqStream2 = request2.GetRequestStream();
            ////////////reqStream2.Write(buffer2, 0, buffer2.Length);
            ////////////reqStream2.Close();


        }

        private void label69_Click_1(object sender, EventArgs e)
        {


            if (txtnumberphone.Text.Equals(glb_num_mobile) == false)
            {

                MessageBox.Show("شماره ی تلفن همراه تغییر کرده است برای ذخیره ی آن گزینه ی ارسال به سیستم را کلیک نمایید");
                //  return;
            }



            pnlsms.Visible = false;

        }

        private void label84_Click(object sender, EventArgs e)
        {


            string username, password;





            FTPAddressdownloaddata = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//datalog//0//";



            ftpusername = "root";
            ftppassword = "password";


            getFileList3(FTPAddressdownloaddata, ftpusername, ftppassword);


            Application.DoEvents();

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            string filename = "";

            //  downloadFileDateTimeStamp(FTPAddressdownloaddata, filename, ftpusername, ftppassword);

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();






            if (listboxFiles3.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }


            lblstatusftp2.Text = "پایان دریافت اطلاعات سیستم ";
            Application.DoEvents();
            Application.DoEvents();



            deletedatafile();






            return;

        }

        private void picdeletedata_Click(object sender, EventArgs e)
        {


            ////string flname = "", caption;


            ////string message = "آیا مطمئن هستید که قصد حذف اطلاعات ذخیره شده در سیستم را دارید ؟";

            ////caption = "حذف اطلاعات" + "  " + flname;

            ////MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            ////DialogResult result;

            ////// Displays the MessageBox.

            ////result = MessageBox.Show(message, caption, buttons);

            ////if (result == System.Windows.Forms.DialogResult.No)
            ////{
            ////    // Closes the parent form.
            ////    return;

            ////}



            ////label84_Click(null, null);



        }

        private void pnlshowdata_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbfilelogTo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtdatefrom_TextChanged(object sender, EventArgs e)
        {


            //     dtgshowdataonline.FindForm = string.Format("country LIKE '%{0}%'", textBox1.Text);



            ////string searchValue = txtdatefrom.Text;
            ////int rowIndex = -1;

            ////dtgshowdataonline.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            ////try
            ////{
            ////    foreach (DataGridViewRow row in dtgshowdataonline.Rows)
            ////    {
            ////        if (row.Cells[row.Index].Value.ToString().Equals(searchValue))
            ////        {
            ////            rowIndex = row.Index;
            ////            dtgshowdataonline.Rows[row.Index].Selected = true;
            ////            break;
            ////        }
            ////    }
            ////}
            ////catch (Exception exc)
            ////{
            ////    MessageBox.Show(exc.Message);
            ////}









        }

        private void dtgshowdataonline_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }





        private void label60_Click_1(object sender, EventArgs e)
        {

            //    string searchValue = txtdatefrom.Text;
            //    int rowIndex = -1;

            // //   dtgshowdatapast.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ////    dtgshowdatapast.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;


            //    for (int i = 0; i < dtgshowdatapast.Rows.Count; i++)
            //    {

            //        //  string a= dtgshowdataonline.Rows[i].;


            //        if (dtgshowdatapast.Rows[i].Cells[4].Value.ToString().Equals(searchValue))
            //        {
            //            //rowIndex = Rows.Index;
            //            dtgshowdatapast.Focus();
            //            dtgshowdatapast.Rows[i].Selected = true;
            //            dtgshowdatapast.CurrentCell = dtgshowdatapast[4, i];
            //           // dtgshowdatapast.CurrentCell = dataGridView1.Rows[0].Cells[0];

            //            break;
            //        }


            //    }




            ////try
            ////{
            ////    foreach (DataGridViewRow row in dtgshowdatapast.Rows)
            ////    {
            ////        if (row.Cells[4].Value.ToString().Equals(searchValue))
            ////        {
            ////            rowIndex = row.Index;
            ////            dtgshowdatapast.Rows[row.Index].Selected = true;
            ////            break;
            ////        }
            ////    }
            ////}
            ////catch (Exception exc)
            ////{
            ////    MessageBox.Show(exc.Message);
            ////}








        }

        private void label86_Click(object sender, EventArgs e)
        {

        }

        private void txtdatefrom_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (Convert.ToInt32(e.KeyChar) == 13)
            {


                string searchValue = txtdatefrom.Text;
                int rowIndex = -1;
                int col = 0;
                if (cmbselextsearch.Text == "روز") col = 4;
                if (cmbselextsearch.Text == "ماه") col = 3;
                if (cmbselextsearch.Text == "سال") col = 2;


                //   dtgshowdatapast.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //    dtgshowdatapast.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;


                for (int i = 0; i < dtgshowdatapast.Rows.Count; i++)
                {

                    //  string a= dtgshowdataonline.Rows[i].;


                    if (dtgshowdatapast.Rows[i].Cells[col].Value.ToString().Equals(searchValue))
                    {
                        //rowIndex = Rows.Index;
                        dtgshowdatapast.Focus();
                        dtgshowdatapast.Rows[i].Selected = true;
                        dtgshowdatapast.CurrentCell = dtgshowdatapast[4, i];
                        // dtgshowdatapast.CurrentCell = dataGridView1.Rows[0].Cells[0];

                        break;
                    }


                }



                //MessageBox.Show("Enter pressed");


            }




        }

        private void txtsearchgrid_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsearchgrid_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Convert.ToInt32(e.KeyChar) == 13)
            {


                string searchValue = txtsearchgrid.Text;
                int rowIndex = -1;
                int col = 0;
                if (cmbselextsearch.Text == "روز") col = 4;
                if (cmbselextsearch.Text == "ماه") col = 3;
                if (cmbselextsearch.Text == "سال") col = 2;


                //   dtgshowdatapast.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //    dtgshowdatapast.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;


                for (int i = 0; i < dtgshowdatapast.Rows.Count - 1; i++)
                {

                    //  string a= dtgshowdataonline.Rows[i].;


                    if (dtgshowdatapast.Rows[i].Cells[col].Value.ToString().Equals(searchValue) == true)
                    {
                        //rowIndex = Rows.Index;
                        dtgshowdatapast.Focus();
                        dtgshowdatapast.Rows[i].Selected = true;
                        dtgshowdatapast.CurrentCell = dtgshowdatapast[4, i];
                        // dtgshowdatapast.CurrentCell = dataGridView1.Rows[0].Cells[0];

                        break;
                    }


                }



            }




        }

        private void picdeleteSaveData_Click(object sender, EventArgs e)
        {


            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد حذف اطلاعات ذخیره شده در سیستم را دارید ؟";

            caption = "حذف اطلاعات" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }



            label84_Click(null, null);



        }



        private void picsaveguid_Click(object sender, EventArgs e)
        {


            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد ذخیره شناسه های مجاز در سیستم را دارید ؟";

            caption = "ذخیره شناسه های مجاز" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }







            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\screen\\cdl.png");





            string savedata;

            savedata = txtshowhash.Text;

            filewriter.WriteLine(savedata);

            filewriter.Close();


            System.Windows.Forms.MessageBox.Show("تغییرات ذخیره شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);





        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            //if (textBox2.Text.Equals("password") == true)
            //{


            //    //picsavenamestations.Visible = true;
            //    //label90_Click(null, null);
            //    //groupBox2.Text = "نام ایستگاه ها";

            //}

        }

        private void label90_Click(object sender, EventArgs e)
        {

            string flname = "";
            string readline = "";
            glbguid = true;

            flname = Application.StartupPath + "\\screen\\station.cls";


            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);




            while ((readline = Filereader.ReadToEnd()) != null)
            {




                if (readline == null)
                {


                    break;



                }




                if (readline.Equals("") == true)
                {


                    break;



                }







                //  string s = readline, leftvalue = "", rightvalue = "";

                txtshowhash.Enabled = true;





                txtshowhash.Text = readline;












            }






            Filereader.Close();





        }

        private void picsavenamestations_Click(object sender, EventArgs e)
        {


            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد ذخیره نام ایستگاه ها در سیستم را  دارید ؟";

            caption = "ذخیره نام ایستگاه ها" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }







            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\screen\\station.cls");





            string savedata;

            savedata = txtshowhash.Text;

            filewriter.WriteLine(savedata);

            filewriter.Close();


            System.Windows.Forms.MessageBox.Show("تغییرات ذخیره شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void label91_Click(object sender, EventArgs e)
        {

        }

        private void lblshowdate_realTime_Click(object sender, EventArgs e)
        {

        }

        private void picshowdatetime_Click(object sender, EventArgs e)
        {


            show_time_log = true;
            rdinstant.Checked = true;
            pictureBox11_Click(null, null);
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            show_time_log = false;

        }

        private void picshowlogsms_Click(object sender, EventArgs e)
        {

            get_log_sms = true;

            pictureBox11_Click(null, null);


            Application.DoEvents();

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            get_log_sms = false;



        }

        private void label98_Click(object sender, EventArgs e)
        {

            dlgOpenFile.FileName = "Rain_Total.conf";


            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\Rain_Total.conf";


            Application.DoEvents();
            Application.DoEvents();







            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            while ((readline = Filereader.ReadLine()) != null)
            {



                lblstatus.Text = "در حال بار گذاری اطلاعات " + "...............";


                if (progfetchsensors.Value + 2 < 100)
                {

                    progfetchsensors.Value = progfetchsensors.Value + 2;

                }




                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }



                if (readline == "")
                {


                    continue;


                }





                string s = readline;
                string[] values = s.Split(',');




                String tmprain_kol, tmprain_12, tmprain_24;


                tmprain_kol = "";
                tmprain_12 = "";
                tmprain_24 = "";


                if (values.Count() == 3)
                {
                    tmprain_kol = values[0];
                    tmprain_12 = values[1];
                    tmprain_24 = values[2];
                }


                //////agar 6 ta bood marboot be  khat tanzimat ip,serevr hosr va port as 




                txtrain_kol.Text = tmprain_kol.ToString();
                txtrain_12.Text = tmprain_12.ToString();
                txtrain_24.Text = tmprain_24.ToString();









                if (readline == null)
                {


                    break;


                }





                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }




            }/////end op loof reading file









            Filereader.Close();

            progfetchsensors.Value = 100;


            Application.DoEvents();


            //////////  lblstatus.Text = "پایان بارگذاری اطلاعات";

            ////////// tabControl1.SelectedTab = tabPage3;

            //////////////////////////////set time 

            ////////DateTime theDate = DateTime.UtcNow;


            ////////string customdate = theDate.ToString("d");

            ////////lbltime.Text = DateTime.Now.ToLongTimeString().ToString();

            ////////lbldate.Text = customdate;
            ///////////////////////////



        }

        private void label95_Click(object sender, EventArgs e)
        {


            //txtrain_kol.Text = tmprain_kol.ToString();
            //txtrain_12.Text = tmprain_12.ToString();
            //txtrain_24.Text = tmprain_24.ToString();


            if (glb_stop_rdl == true)
            {

                //   glb_stop_rdl = false;

            }
            else
            {

                return;

            }



            progfetchsensors.Value = 2;


            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\Rain_Total.conf");



            String tmprain_kol, tmprain_12, tmprain_24;

            tmprain_kol = "";

            tmprain_12 = "";
            tmprain_24 = "";





            tmprain_kol = txtrain_kol.Text;
            tmprain_12 = txtrain_12.Text;
            tmprain_24 = txtrain_24.Text;



            string tmpsave;

            string rain_status = "";

            rain_status = "0";




            tmpsave = tmprain_kol + "," + tmprain_12 + "," + tmprain_24 + "," + rain_status;





            //////write data for time
            filewriter.WriteLine(tmpsave);
            ////////////
            //////filewriter.WriteLine("");
            //////filewriter.WriteLine("### ### Help ");
            //////filewriter.WriteLine("### ### 1-Gprs,2-recieve sms,3-send sms,4-time stop rain,5-time alarm ");
            //////filewriter.WriteLine("###rain,6-value Alarm rain in time,7-value ###rain in 5 min");
            //////filewriter.WriteLine("");

            filewriter.Close();

            progfetchsensors.Value = 100;
            //   lblstatus.Text = "پایان مرحله خروجی فایل";




        }

        private void pic_stop_system_Click(object sender, EventArgs e)
        {

            string flname = "", caption;
            string message = "آیا مطمئن هستید که قصد توقف سیستم باران را دارید ؟";

            caption = "توقف سیستم باران" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }


            if (glb_autorite_stopr_rdl == false)
            {

                System.Windows.Forms.MessageBox.Show("شما مجاز به تغییر مقدار مقادیر سیستم نیستید", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);



                return;
            }





            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\TestThread.log");


            string savedata, strtime, strdate;
            savedata = "4";
            filewriter.Write(savedata);







            filewriter.Close();


            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            if (listboxFiles.Items.Count == 0)
            {

                MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                return;


            }



            string filename, filePath, FTPAddresssend;




            FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//applog//";



            prgstatusftbdownload.Value = 0;







            filename = "TestThread.log";


            filePath = Application.StartupPath + "\\configExport\\";



            ftpusername = "root";
            ftppassword = "password";



            //Create FTP request
            //  FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(filePath + filename);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();

            //  MessageBox.Show("Uploaded Successfully");



            glb_stop_rdl = true;



            lblstatus.Text = "توقف سیستم باران ";
            progfetchsensors.Value = progfetchsensors.Maximum;

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();






        }

        private void button1_Click_3(object sender, EventArgs e)
        {





        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {


            dtgridrain.Focus();
            pictureBox17.Focus();
            // dtgridrain.LostFocus();
            dtgridrain.Update();

        }

        private void label102_Click(object sender, EventArgs e)
        {


            dlgOpenFile.FileName = "rdlsys_client_SMS_Number_Text.conf";


            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\rdlsys_client_SMS_Number_Text.conf";


            Application.DoEvents();
            Application.DoEvents();







            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            while ((readline = Filereader.ReadLine()) != null)
            {



                lblstatus.Text = "در حال بار گذاری اطلاعات " + "...............";


                if (progfetchsensors.Value + 2 < 100)
                {

                    progfetchsensors.Value = progfetchsensors.Value + 2;

                }




                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }



                if (readline == "")
                {


                    continue;


                }





                string s = readline;
                string[] values = s.Split(',');




                String tmpsmsnumber, tmpmatn, tmprecievetime;


                tmpsmsnumber = "";
                tmpmatn = "";
                tmprecievetime = "";


                if (values.Count() == 3)
                {
                    tmpsmsnumber = values[0];
                    tmpmatn = values[1];
                    tmprecievetime = values[2];
                }


                //////agar 6 ta bood marboot be  khat tanzimat ip,serevr hosr va port as 




                txtnumberphone.Text = tmpsmsnumber.ToString();
                glb_num_mobile = tmpsmsnumber.ToString();
                //txtrain_12.Text = tmprain_12.ToString();
                //txtrain_24.Text = tmprain_24.ToString();









                if (readline == null)
                {


                    break;


                }





                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }




            }/////end op loof reading file









            Filereader.Close();

            progfetchsensors.Value = 100;


            Application.DoEvents();


            //////////  lblstatus.Text = "پایان بارگذاری اطلاعات";

            ////////// tabControl1.SelectedTab = tabPage3;

            //////////////////////////////set time 

            ////////DateTime theDate = DateTime.UtcNow;


            ////////string customdate = theDate.ToString("d");

            ////////lbltime.Text = DateTime.Now.ToLongTimeString().ToString();

            ////////lbldate.Text = customdate;
            ///////////////////////////


        }

        private void txtpassrainamount_TextChanged(object sender, EventArgs e)
        {



            if (txtpassrainamount.Text.Equals("password") == true)
            {



                groupBox14.Visible = false;

                System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\configExport\\TestThread.log");


                string savedata, strtime, strdate;
                savedata = "4";
                filewriter.Write(savedata);







                filewriter.Close();


                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();


                if (listboxFiles.Items.Count == 0)
                {

                    MessageBox.Show("مشکل در برقراری ارتباط با دستگاه");
                    return;


                }



                string filename, filePath, FTPAddresssend;




                FTPAddresssend = "ftp://192.168.1.222//mnt//dom//RDLSystemClient//applog//";



                prgstatusftbdownload.Value = 0;







                filename = "TestThread.log";


                filePath = Application.StartupPath + "\\configExport\\";



                ftpusername = "root";
                ftppassword = "password";



                //Create FTP request
                //  FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddresssend + filename);


                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpusername, ftppassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                //Load the file
                FileStream stream = File.OpenRead(filePath + filename);
                byte[] buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                //Upload file
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();

                //  MessageBox.Show("Uploaded Successfully");



                glb_stop_rdl = true;



                lblstatus.Text = "توقف سیستم باران ";
                progfetchsensors.Value = progfetchsensors.Maximum;

                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();





                // picdeletedata.Visible = true;
                //  label55_Click(null, null);
                //  picsaveguid.Visible = true;

                //  groupBox2.Text = "شناسه های مجاز برای سیستم";






            }


        }

        private void picsaveexcelllogsms_Click(object sender, EventArgs e)
        {
            string flname = "";

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();


            //"txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //  saveFileDialog1.Filter = "csv files (*.csv)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;



            saveFileDialog1.ShowDialog();






            flname = saveFileDialog1.FileName;

            if (flname == "")
            {
                return;


            }


            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(flname);





            string savedata = "";


            prgstatusftbdownload2.Value = 0;
            prgstatusftbdownload2.Maximum = 100;


            int cntgrid = dtgshowdatapast.RowCount;

            prgstatusftbdownload2.Value = cntgrid / 100;



            //////////////////////////////////////////////////adding header////////////////////////////////////////
            for (int r = 0; r < dtgshowdataonline.ColumnCount; r++)
            {

                // savedata = savedata + dtgshowdatapast.Columns[r].HeaderText.ToString() + ";";
                savedata = savedata + dtgshowdataonline.Columns[r].HeaderText.ToString() + ",";


            }


            filewriter.WriteLine(savedata);
            savedata = "";
            //////////////////////////////////////////////////adding header////////////////////////////////////////




            for (int k = 0; k < dtgshowdataonline.RowCount - 1; k++)
            {


                prgstatusftbdownload2.Value = k / 100;



                for (int index = 0; index < dtgshowdataonline.ColumnCount; index++)
                {

                    //try
                    //{







                    //    if (index < dtgshowdatapast.Rows[k].Cells.Count)
                    if (dtgshowdataonline.Rows[k].Cells[index].Value != null)
                    {
                        //  savedata = savedata + dtgshowdatapast.Rows[k].Cells[index].Value.ToString() + ";";

                        savedata = savedata + dtgshowdataonline.Rows[k].Cells[index].Value.ToString() + ",";


                    }


                }







                filewriter.WriteLine(savedata);

                savedata = "";


            }


            filewriter.Close();
            prgstatusftbdownload2.Value = 100;
            lblstatusftp2.Text = "پایان ایجاد فایل";


            return;






        }

        private void picdisconnected_Click(object sender, EventArgs e)
        {

            lblshowtransfer_Click(null, null);

            Application.DoEvents();
            Application.DoEvents();

            label51_Click(null, null);


        }

        private void picconnected_Click(object sender, EventArgs e)
        {

           // lblshowtransfer_Click(null, null);

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void txtmanagepass_TextChanged(object sender, EventArgs e)
        {



            if (txtmanagepass.Text.Equals("rootroot") == true)
            {


                picsaverain.Visible = true;
                picdeletedadtasaved.Visible = true;
                picsavedate.Visible = true;
                pic_save_guid.Visible = true;
                pic_save_stations.Visible = true;


                lbl_picsaverain.Visible = true;
                lbl_picdeletedadtasaved.Visible = true;
                lbl_picsavedate.Visible = true;
                lbl_pic_save_guid.Visible = true;
                lbl_pic_save_stations.Visible = true;



            }






            if (txtmanagepass.Text.Equals("admin123") == true)
            {


                picsaverain.Visible = true;
                picdeletedadtasaved.Visible = true;
                picsavedate.Visible = true;
                pic_save_guid.Visible = false;
                pic_save_stations.Visible = false;


                lbl_picsaverain.Visible = true;
                lbl_picdeletedadtasaved.Visible = true;
                lbl_picsavedate.Visible = true;
                lbl_pic_save_guid.Visible = false;
                lbl_pic_save_stations.Visible = false;



            }




            if (txtmanagepass.Text.Equals("useruser") == true)
            {


                picsaverain.Visible = true;
                picdeletedadtasaved.Visible = false;
                picsavedate.Visible = true;
                pic_save_guid.Visible = false;
                pic_save_stations.Visible = false;


                lbl_picsaverain.Visible = true;
                lbl_picdeletedadtasaved.Visible = false;
                lbl_picsavedate.Visible = true;
                lbl_pic_save_guid.Visible = false;
                lbl_pic_save_stations.Visible = false;



            }









        }

        private void pic_save_guid_Click(object sender, EventArgs e)
        {


            label55_Click(null, null);
            //   picsaveguid.Visible = true;

            groupBox2.Text = "شناسه های مجاز برای سیستم";
            picsaveguid_new.Visible = true;
            picsavenamestations_new.Visible = false;



        }

        private void picsaveguid_new_Click(object sender, EventArgs e)
        {


            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد ذخیره شناسه های مجاز در سیستم را دارید ؟";

            caption = "ذخیره شناسه های مجاز" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }







            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\screen\\cdl.png");





            string savedata;

            savedata = txtshowhash.Text;

            filewriter.WriteLine(savedata);

            filewriter.Close();


            System.Windows.Forms.MessageBox.Show("تغییرات ذخیره شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);




        }

        private void pic_save_stations_Click(object sender, EventArgs e)
        {

            picsavenamestations_new.Visible = true;
            picsaveguid_new.Visible = false;
            label90_Click(null, null);
            groupBox2.Text = "نام ایستگاه ها";

        }

        private void picsavenamestations_new_Click(object sender, EventArgs e)
        {

            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد ذخیره نام ایستگاه ها در سیستم را  دارید ؟";

            caption = "ذخیره نام ایستگاه ها" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }







            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(Application.StartupPath + "\\screen\\station.cls");





            string savedata;

            savedata = txtshowhash.Text;

            filewriter.WriteLine(savedata);

            filewriter.Close();


            System.Windows.Forms.MessageBox.Show("تغییرات ذخیره شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        private void picdeletedadtasaved_Click(object sender, EventArgs e)
        {

            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد حذف اطلاعات ذخیره شده در سیستم را دارید ؟";

            caption = "حذف اطلاعات" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }



            label84_Click(null, null);


        }

        private void picsaverain_Click(object sender, EventArgs e)
        {

            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد تغییر مقادیر اندازه گیری شده توسط سیستم را دارید ؟";

            caption = "تغییر مقادیر" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }

            glb_autorite_stopr_rdl = true;
            System.Windows.Forms.MessageBox.Show("شما میتوانید در قسمت تنظیمات و حجم باران ، مقادیر  اندازه گیری شده را تغییر دهید", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        private void picsavedate_Click(object sender, EventArgs e)
        {

            string flname = "", caption;


            string message = "آیا مطمئن هستید که قصد تغییر تاریخ و ساعت سیستم را دارید ؟";

            caption = "تغییر زمان" + "  " + flname;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }

            glb_autorite_change_date = true;
            System.Windows.Forms.MessageBox.Show("شما میتوانید در قسمت تنظیمات تاریخ و ساعت سیستم  را تغییر دهید", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);





        }

        private void lblshow_guage_panel_Click(object sender, EventArgs e)
        {


            pnlshowgauge.Visible = true;
            pnlshowgauge.BringToFront();

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            //lblrain_total.Text = glb_RAN_TOT;



            if (picconnected.Visible == true)
            {




                rdinstant.Checked = true;
                chkautomated.Checked = true;
                tmrfetchdata.Enabled = true;


            }



        }

        private void label104_Click(object sender, EventArgs e)
        {


            chkautomated.Checked = false;

            tmrfetchdata.Enabled = false;

            pnlshowgauge.Visible = false;

        }

        private void pnlshowgauge_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label87_Click(object sender, EventArgs e)
        {

        }

        private void label56_Click(object sender, EventArgs e)
        {

        }

        private void lblrain_total_Click(object sender, EventArgs e)
        {

        }

        private void lblrain_avg_Click(object sender, EventArgs e)
        {

        }



        private void pictureBox19_Click_1(object sender, EventArgs e)
        {


            rdinstant.Checked = true;

            pictureBox11_Click(null, null);

            Application.DoEvents();
            Application.DoEvents();

            chkautomated.Checked = true;
            tmrfetchdata.Enabled = true;


        }

        private void pictureBox18_Click_1(object sender, EventArgs e)
        {




            rdcalculated.Checked = true;
            pictureBox11_Click(null, null);

            Application.DoEvents();
            Application.DoEvents();


            chkautomated.Checked = true;
            tmrfetchdata.Enabled = true;

        }

        private void label122_Click(object sender, EventArgs e)
        {


            if (tabControl3.Visible == true)
            {
                tabControl3.Visible = false;
            }

            else
            {
                tabControl3.Visible = true;
            }



        }

        private void label121_Click(object sender, EventArgs e)
        {


            if (pnlTree_Ostan.Visible == true)
            {

                pnlTree_Ostan.Visible = false;
                return;

            }

            else
            {


                pnlTree_Ostan.Visible = true;

            }







            if (tree_Ostan.Nodes.Count == 0)
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                OleDbDataAdapter da3 = new OleDbDataAdapter();
                da3.SelectCommand = new OleDbCommand("select  * from tblostan order by code_ostan ", myconn);
                da3.Fill(ds, "tbldatelog");
                dt = ds.Tables["tbldatelog"];

                string code_ostan = "", name_ostan, sample, has_station;

                int cnt = 0;

                TreeNode tr = new TreeNode();


                //  Dim trchild1 As New TreeNode


                // TreeNode node = tree_Ostan.Nodes.Add("Level one node");
                // node.Nodes.Add("Level two node");



                foreach (DataRow dr in dt.Rows)
                {
                    cnt++;
                    //  code = (dr["code"].ToString());
                    code_ostan = (dr["code_ostan"].ToString());
                    name_ostan = (dr["name_ostan"].ToString());
                    has_station = (dr["has_station"].ToString());



                    TreeNode node1 = tree_Ostan.Nodes.Add(name_ostan.ToString());
                    node1.Tag = code_ostan;



                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();

                    OleDbDataAdapter da33 = new OleDbDataAdapter();
                    da33.SelectCommand = new OleDbCommand("select  * from tblostan_station where code_ostan= " + code_ostan + "", myconn);
                    da33.Fill(ds1, "tblostan_station");
                    dt1 = ds1.Tables["tblostan_station"];

                    if (dt1.Rows.Count > 0)
                    {
                        node1.Nodes.Add("sample");

                    }




                }


            }




        }

        private void show_systems_Paint(object sender, PaintEventArgs e)
        {


        }

        private void show_systems_Click(object sender, EventArgs e)
        {

            if (lst_name_clients.Items.Count > 0)
            {

                //  MessageBox.Show("ارتباط با سرور مقدور نمی باشد");
                return;
            }




            if (!DataUtils.connect_db())
            {

                MessageBox.Show("ارتباط با سرور مقدور نمی باشد");
                return;
            }


            NpgsqlDataReader dr = null;
            try
            {

                string is_active = "True";
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select * from client_infos order  by id ";

                //   cmd.CommandText = "select * from sensors order where is_active = '" + is_active + "' by channel_index ";

                cmd.Connection = DataUtils.conn;
                dr = cmd.ExecuteReader();
                //   NpgsqlDataReader dr = cmd.ExecuteReader(); //I get InvalidOperationException : The connection is not open.
            }
            catch (Exception ex)
            {

                MessageBox.Show("ارتباط با سرور قطع شده است");
                return;
            }



            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            //if (dr.Read())
            // {
            // boolfound = true;




            //
            string name_sensors = "";

            int cnt = 0;
            while (dr.Read())
            {

                name_sensors = dr[1].ToString();
                client_info_id[cnt] = int.Parse(dr[0].ToString());

                cnt++;
                //  string ss = dr[13].ToString();


                // if (ss.Equals ("True")){
                lst_name_clients.Items.Add(name_sensors);

                //  }

            }





            dr.Close();


        }

        private void panel13_Click(object sender, EventArgs e)
        {



            if (lst_name_clients.SelectedItems.Count == 0)
            {


                MessageBox.Show("لطفا نام ایستگاه را انتخاب نمایید");
                return;

            }


            lst_sensrors_chk.Items.Clear();



            int client_info_id_ = client_info_id[lst_name_clients.SelectedIndex];


            //////retriving client_id
            ////
            if (!DataUtils.connect_db())
            {

                MessageBox.Show("ارتباط با سرور مقدور نمی باشد");
                return;
            }


            NpgsqlDataReader dr = null;
            try
            {


                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select id from clients where client_info_id= " + client_info_id_ + " ";
                cmd.Connection = DataUtils.conn;
                dr = cmd.ExecuteReader();

            }
            catch (Exception ex)
            {

                MessageBox.Show("ارتباط با سرور قطع شده است");
                return;
            }



            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            //
            int client_id = 0;
            while (dr.Read())
            {
                client_id = int.Parse(dr[0].ToString());
            }
            dr.Close();
            /////////////////////////////////////////////////////////


            glb_client_id = client_id;



            //////name sensors/////
            // NpgsqlDataReader dr = null;
            try
            {


                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "  select channel_index,name from sensors   LEFT JOIN client_sensors  ON sensors.id=client_sensors.sensor_id  where client_sensors.client_id=" + client_id + " order by channel_index  ";
                cmd.Connection = DataUtils.conn;
                dr = cmd.ExecuteReader();

            }
            catch (Exception ex)
            {

                MessageBox.Show("ارتباط با سرور قطع شده است");
                return;
            }



            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            //
            int cnt = 0;
            string name_sensors = "";
            while (dr.Read())
            {





              //////  name_sensors = dr[1].ToString();
              //////  sensors_index[cnt] = int.Parse(dr[0].ToString());



              //////  cnt++;

              ////////  if (cnt < 20) continue;

              
              //////  lst_sensrors_chk.Items.Add(name_sensors);





                name_sensors = dr[1].ToString();
                sensors_index[cnt] = int.Parse(dr[0].ToString());



                cnt++;

               // if (cnt < 20) continue;
               

               
                lst_sensrors_chk.Items.Add(name_sensors);









            }
            dr.Close();


        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {


            ////new format like new  excell base

            int cnt_lst = lst_sensrors_chk.Items.Count;
            string add_sql_index_sensor = "*";


            dg_show_onlinedata_newformat.Width = 745;
            dg_show_onlinedata.Width = 745;
            dg_show_onlinedata_newformat.Height = dg_show_onlinedata.Height;

            dg_show_onlinedata_newformat.Rows.Clear();
            dg_show_onlinedata_newformat.Columns.Clear();

            dg_show_onlinedata.Rows.Clear();
            dg_show_onlinedata.Columns.Clear();


            ////dg_show_onlinedata_newformat.Rows.Clear();
            ////dg_show_onlinedata_newformat.Columns.Clear();


            dg_show_onlinedata_newformat.Columns.Add("index", "index");
            dg_show_onlinedata_newformat.Columns.Add("Date", "Date");
            dg_show_onlinedata_newformat.Columns.Add("Time", "Time");

            dg_show_onlinedata_newformat.Columns[0].Width = 50;
            dg_show_onlinedata_newformat.Columns[1].Width = 80;
            dg_show_onlinedata_newformat.Columns[2].Width = 80;




            dg_show_onlinedata.Columns.Add("index", "index");
            dg_show_onlinedata.Columns.Add("Date", "Date");
            dg_show_onlinedata.Columns.Add("Time", "Time");

            dg_show_onlinedata.Columns[0].Width = 50;
            dg_show_onlinedata.Columns[1].Width = 80;
            dg_show_onlinedata.Columns[2].Width = 80;


            dg_show_onlinedata.Left = dg_show_onlinedata_newformat.Left;
            dg_show_onlinedata.Top = dg_show_onlinedata_newformat.Top;

            dg_show_onlinedata.Width = dg_show_onlinedata_newformat.Width;
            dg_show_onlinedata.Height = dg_show_onlinedata_newformat.Height;

            dg_show_onlinedata.Visible = false;
            dg_show_onlinedata_newformat.Visible = true;

            



            int cnt_lstcheked = 0;
            string strfield_tag = "", strfield_Name = "";


            str_max_min_sensors = "";
            foreach (int indexChecked in lst_sensrors_chk.CheckedIndices)
            {

                ///19 is for removal channels
               // int p = indexChecked+19;
               int p = indexChecked ;
                
                
             int k = int.Parse(sensors_index[p].ToString());//for complete version

             //   int k =p;

                str_max_min_sensors = k + "-" + str_max_min_sensors;//adding information about index of sensors for min and max

                add_sql_index_sensor = add_sql_index_sensor + " or channel_index = " + k.ToString();


                strfield_Name = lst_sensrors_chk.Items[indexChecked].ToString();
                strfield_tag = sensors_index[p].ToString();


                //dg_show_onlinedata_newformat.Columns.Add(strfield_Name, strfield_Name);
                dg_show_onlinedata_newformat.Columns.Add(strfield_tag, strfield_Name);
                dg_show_onlinedata_newformat.Columns[cnt_lstcheked + 3].Width = 100;
                dg_show_onlinedata_newformat.Columns[cnt_lstcheked + 3].Tag = strfield_tag;



                dg_show_onlinedata.Columns.Add(strfield_tag, strfield_Name);
                dg_show_onlinedata.Columns[cnt_lstcheked + 3].Width = 100;
                dg_show_onlinedata.Columns[cnt_lstcheked + 3].Tag = strfield_tag;





                cnt_lstcheked++;




            }





            dg_show_onlinedata_newformat.Width = 745;
            dg_show_onlinedata.Width = 745;



            add_sql_index_sensor = add_sql_index_sensor.Replace("* or", "");

            //    add_sql_index_sensor = " and ( " + add_sql_index_sensor+ " ) ";

            if (cnt_lstcheked == 0)
            {
                add_sql_index_sensor = "";
                 MessageBox.Show("نام سنسورها را انتخاب نمایید");
                 return;

            }



            if (chk_graph.Checked == true)
            {

                if (cnt_lstcheked > 2)
                {
                    MessageBox.Show("در حالت نمایش نمودار تنها نام دو  سنسور را انتخاب نمایید");
                    return;

                }


            }


            string new_miladi_to = "", new_miladi_from = "";

            if (rd_date.Checked == true)
            {
                ///////////////////////////////////// date from ///////////////
                // string _date_persian = txtdate_from_web.Text.ToString().Trim();
                string _date_persian = pcal_txtdate_from_web.Text.ToString().Trim();



                string[] str_temp = _date_persian.Split('/');

                string year_tmp, mount_tmp, day_tmp;
                year_tmp = str_temp[0];
                mount_tmp = str_temp[1];
                day_tmp = str_temp[2];
                day_tmp = day_tmp.Substring(0, 2);



                int yr, mn, dy = 0;


                yr = int.Parse(year_tmp);
                mn = int.Parse(mount_tmp);
                dy = int.Parse(day_tmp);



                DateTime dt = new DateTime(yr, mn, dy, new PersianCalendar());
                string date_miladi = dt.ToString(CultureInfo.InvariantCulture);





                string[] miladi_seprate = date_miladi.Split(new char[] { '/' });



                string year, mounth, day;
                year = miladi_seprate[2];
                year = year.Substring(0, 4);
                mounth = miladi_seprate[0];
                day = miladi_seprate[1];

                new_miladi_from = year + "-" + mounth + "-" + day;
                /////////////////////////////////////////////////////



                ///////////////////////////////////// date from ///////////////
                //  string _date_persian_to = txtdate_to_web.Text.ToString().Trim();
                string _date_persian_to = pcal_txtdate_to_web.Text.ToString().Trim();

                string[] str_temp_to = _date_persian_to.Split('/');

                string year_tmp_to, mount_tmp_to, day_tmp_to;
                year_tmp_to = str_temp_to[0];
                mount_tmp_to = str_temp_to[1];
                day_tmp_to = str_temp_to[2];
                day_tmp_to = day_tmp_to.Substring(0, 2);



                int yr_to, mn_to, dy_to = 0;


                yr_to = int.Parse(year_tmp_to);
                mn_to = int.Parse(mount_tmp_to);
                dy_to = int.Parse(day_tmp_to);



                DateTime dt_to = new DateTime(yr_to, mn_to, dy_to, new PersianCalendar());


                TimeSpan offSet = TimeSpan.Parse("01:00:00");
                DateTime newDateTime = dt_to.AddDays (1);
              //  DateTime newDateTime = dt_to + offSet;


                string date_miladi_to = newDateTime.ToString(CultureInfo.InvariantCulture);





                string[] miladi_seprate_to = date_miladi_to.Split(new char[] { '/' });



                string year_to, mounth_to, day_to;
                year_to = miladi_seprate_to[2];
                year_to = year_to.Substring(0, 4);
                mounth_to = miladi_seprate_to[0];
                day_to = miladi_seprate_to[1];

            //    new_miladi_to = "%"+year_to + "-" + mounth_to + "-" + day_to+"%";///+ " 00:00:00";
                new_miladi_to = year_to + "-" + mounth_to + "-" + day_to;///+ " 00:00:00";
                /////////////////////////////////////////////////////







            }








            Application.DoEvents();
            Application.DoEvents();

            string ordinal_number = "", channel_index = "", time_stamp = "", client_id = "", value = "";

            ///redind database
            if (!DataUtils.connect_db())
            {
                MessageBox.Show("ارتباط با سرور مقدور نمی باشد");
                return;
            }


            NpgsqlDataReader dr = null;
            try
            {

                string is_active = "True";
                NpgsqlCommand cmd = new NpgsqlCommand();




                int cnt_record = Int32.Parse(txtcount_record.Text.ToString());



                if (rd_date.Checked == true)
                {



                    if (chk_excel.Checked == false)
                    {
                        cmd.CommandText = "select * from sample_values where  (client_id =" + glb_client_id + ") and " + "(" + add_sql_index_sensor + ")" + " and ( sample_time >= '" + new_miladi_from + "'  and  sample_time<= '" + new_miladi_to + "' ) order by sample_time  ";

                      //  cmd.CommandText = "select * from sample_values where  (client_id =" + glb_client_id + ") and " + "(" + add_sql_index_sensor + ")" + " and ( sample_time >= '" + new_miladi_from + "'  and  sample_time<= '" + new_miladi_to + "' ) order by channel_index,sample_ordinal_num,sample_time  ";

                    //    cmd.CommandText = "select * from sample_values where  (client_id =" + glb_client_id + ") and " + "(" + add_sql_index_sensor + ")" + " and ( sample_time >= '" + new_miladi_from + "'  and  sample_time<= '" + new_miladi_to + "' ) order by sample_time  ";

                        
                        //cmd.CommandText = "select * from sample_values where  (client_id =" + glb_client_id + ") and " + "(" + add_sql_index_sensor + ")" + " and ( sample_time >= '" + new_miladi_from + "' ) and ( sample_time <= '" + new_miladi_to + "' ) order by sample_time,channel_index  ";
                      
                        //cmd.CommandText = "select * from sample_values where  (client_id =" + glb_client_id + ") and " + "(" + add_sql_index_sensor + ")" + " and ( sample_time >= '" + new_miladi_from + "' ) and ( sample_time <= '" + new_miladi_to + "' ) order by sample_time,channel_index  ";
                        //  cmd.CommandText = "select * from sample_values where  (client_id=16) and " + "(" + add_sql_index_sensor + ")" + " and ( sample_time > '" + new_miladi_from + "' ) and ( sample_time < '" + new_miladi_to + "' ) order by sample_time,channel_index limit " + cnt_record + " ";

                    }
                    else
                    {
                        //excell
                        cmd.CommandText = "select * from sample_values where  (client_id =" + glb_client_id + ") and " + "(" + add_sql_index_sensor + ")" + " and ( sample_time >= '" + new_miladi_from + "' ) and ( sample_time <= '" + new_miladi_to + "' )  order by sample_time ";

                    }


                }




                if (rd_100.Checked == true)
                {

                    int cnt_limit = cnt_lstcheked * 100;


                    cmd.CommandText = "select * from sample_values where  (client_id =" + glb_client_id + ") and " + "(" + add_sql_index_sensor + ")" + " order by sample_time desc  limit  " + cnt_limit + "  ";
                    //cmd.CommandText = "select * from sample_values where  (client_id =" + glb_client_id + ") and " + "(" + add_sql_index_sensor + ")" + " order by sample_time desc,channel_index limit  " + cnt_limit + "  ";

                }



                if (rd_real_time.Checked == true)
                {



                    cmd.CommandText = "select * from sample_values where  (client_id =" + glb_client_id + ") and " + "(" + add_sql_index_sensor + ")" + " order by sample_time desc,channel_index  limit  " + cnt_lstcheked + "  ";


                }

                //  if txtcount_record




                cmd.Connection = DataUtils.conn;

                //SqlParameter param = new SqlParameter();
                //param.ParameterName = "@cnt_rec";
                //param.Value = 100;

                //cmd.Parameters.Add(param);

                label123.Text = "در حال دریافت اطلاعات از سرور ....";
                Application.DoEvents();
                Application.DoEvents();

                dr = cmd.ExecuteReader();

            }
            catch (Exception ex)
            {

                MessageBox.Show("ارتباط با سرور قطع شده است");
                return;
            }

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            //
            string name_sensors = "";

            int cnt = 0;


            label123.Text = "در حال بارگذاری اطلاعات در نرم افزار";
            Application.DoEvents();


            string savedata = "", flname = "", svadata1 = "", savedata2 = "";
            //   System.IO.StreamWriter filewriter;

           

            Random rnd = new Random();
            int a1 = rnd.Next(1, 100); // creates a number between 1 and 12
            int b1 = rnd.Next(1, 200);   // creates a number between 1 and 6
            int c1 = rnd.Next(1, 300);
           // int c1 = rnd.Next(52);


            flname = "";

            flname = (a1.ToString ()+"-"+b1.ToString() +"-"+c1.ToString()).ToString()+".txt";


            
            flname = Application.StartupPath + "\\"+flname;

            
            
            ////filewriter = new System.IO.StreamWriter("c://1-1.txt");
            ////filewriter.Close();


            if (chk_excel.Checked == true)
            {

                flname = "";
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "csv files (*.csv)|*.csv";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();

                flname = saveFileDialog1.FileName;

                if (flname == "")
                {
                    return;
                }




            }

          
                System.IO.StreamWriter filewriter = new System.IO.StreamWriter(flname);
           

            //adding header
            for (int r = 0; r < dg_show_onlinedata_newformat.ColumnCount; r++)
            {

                savedata = savedata + dg_show_onlinedata_newformat.Columns[r].HeaderText.ToString() + ",";
                //    savedata = "index" + "," + "ordinal number" + "," + "channel_index" + "," + "channel_name" + "," + "Date" + "," + "Time" + "," + "client_id" + "," + "value";

            }


            filewriter.WriteLine(savedata);
            // savedata = "";




            string old_date = "", old_time = "";

            int rowIndex = 0, current_row;
            var row = this.dg_show_onlinedata_newformat.Rows[rowIndex];
            int cnt_write_excell = 1;


            string old_day = "";
            string old_hour = "";
            string set_time_hour = "";
            string set_time_hour_pm_am = "";
            set_time_hour = cmb_hour_set.Text;

            if (set_time_hour.Equals("") == true && pnl_set_hour.Visible ==true)
            {
                MessageBox.Show("لطفا زمان را انتخاب نمایید");
            }

         
              int val_set_time_hour = int.Parse(set_time_hour.ToString());
         
            if (val_set_time_hour > 12)
            {
                val_set_time_hour = val_set_time_hour - 12;
                set_time_hour_pm_am = "pm";

            }
            else
            {
                set_time_hour_pm_am = "am";
            }


            string hour_select_user="";

            if (val_set_time_hour <10)
            {
               hour_select_user="0"+ val_set_time_hour.ToString ();
            }
            else
            {
                hour_select_user= val_set_time_hour.ToString ();
            }


            while (dr.Read())
            {

                //cnt++;

              

                
                ordinal_number = dr[1].ToString();
                channel_index = dr[2].ToString();
                time_stamp = dr[3].ToString();
                client_id = dr[4].ToString();
                value = dr[6].ToString();

                /////////////////////////////////////convert date//////////////

                string Day = "";
                string Year = "";
                string Month = "";

            

                string Shamsi, tmp_hour = "";

                DateTime miladi = DateTime.Now;
                miladi = DateTime.Parse(time_stamp);

                ////miladi.AddHours(3);
                ////miladi.AddMinutes(30);


                TimeSpan offSet = TimeSpan.Parse("03:30:00");

                DateTime newDateTime = miladi + offSet;



                string miladi_tmp = newDateTime.ToString();
               // string miladi_tmp = miladi.ToString();

                
                string[] miladi_seprate_to = miladi_tmp.Split(new char[] { '/' });


               




                string year_to, mounth_to, day_to;
                year_to = miladi_seprate_to[2];
                year_to = year_to.Substring(0, 4);
                mounth_to = miladi_seprate_to[0];
                day_to = miladi_seprate_to[1];




                new_miladi_to = mounth_to + "/" + day_to + "/" + year_to;


              //hamid  tmp_hour = time_stamp.Replace(new_miladi_to, "");
                tmp_hour = miladi_tmp.Replace(new_miladi_to, "");

                string[] ptimes = tmp_hour.Split(new char[] { ':' });

                string hour_now = ptimes[0];

                string pm_am = ptimes[2];

                
                if (pm_am.Contains("P") == true)
                {
                    pm_am = "pm";
                }
                else
                {
                    pm_am = "am";

                }



                ////DateTime dateNow = DateTime.Now;
                ////Console.WriteLine("The date and time are {0} UTC.",
                ////                   TimeZoneInfo.ConvertTimeToUtc(dateNow));

                ////dateNow.AddHours(2.0);
                ////dateNow.AddMinutes(30);


                ////Console.WriteLine(dateNow.ToString ());



                ////DateTime easternTime = new DateTime(2016, 01, 02, 11, 05, 00);
                ////string easternZoneId = "Eastern Standard Time";
                ////try
                ////{
                ////    TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById(easternZoneId);
                ////    Console.WriteLine("The date and time are {0} UTC.",
                ////                      TimeZoneInfo.ConvertTimeToUtc(easternTime, easternZone));
                ////}
                ////catch (TimeZoneNotFoundException)
                ////{
                ////    Console.WriteLine("Unable to find the {0} zone in the registry.",
                ////                      easternZoneId);
                ////}
                ////catch (InvalidTimeZoneException)
                ////{
                ////    Console.WriteLine("Registry data on the {0} zone has been corrupted.",
                ////                      easternZoneId);
                ////}




                System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
                Shamsi = string.Format("{0}/{1}/{2}", shamsi.GetYear(miladi), shamsi.GetMonth(miladi), shamsi.GetDayOfMonth(miladi));

                string[] pWords = Shamsi.Split(new char[] { '/' });

                Year = pWords[0];
                Month = pWords[1];
                Day = pWords[2];



                    ////if (rd_day.Checked == true)/// for view only distinct days
                    ////{
                    ////            if (Day.Equals(old_day) == true)
                    ////            {
                    ////                old_day = Day;
                    ////                continue;

                    ////            }
                    ////            else
                    ////            {
                    ////                old_day = Day;
                    ////            }

                    ////}


                  

                    //////////if (rd_hour.Checked == true)/// for view only distinct days
                    //////////{
                    //////////    if (hour_now.Equals(old_hour) == true)
                    //////////    {
                    //////////        old_hour = hour_now;
                    //////////        continue;

                    //////////    }
                    //////////    else
                    //////////    {

                    //////////        if (hour_select_user.Trim().Equals(hour_now.Trim ()) == true && set_time_hour_pm_am.Equals(pm_am) == true)
                    //////////        {
                    //////////            old_hour = hour_now;

                    //////////        }
                    //////////        else
                    //////////        {
                    //////////            old_hour = hour_now;
                    //////////            continue;
                    //////////        }

                    //////////    }

                    //////////}






                if (int.Parse(Day) < 10)
                {
                    Day = "0" + Day;

                }




                if (int.Parse(Month) < 10)
                {
                    Month = "0" + Month;

                }




                Shamsi = Year + "/" + Month + "/" + Day;
                string chanel_name = sensors_index_name[int.Parse(channel_index)];

                string tmp_shamsi = Shamsi.ToString();
                //////////////////////////////////////////////


                if (chk_excel.Checked == false)
                {




                    //dg_show_onlinedata.Rows.Add(new object[] { cnt.ToString(), ordinal_number, channel_index, chanel_name, tmp_shamsi.ToString(), tmp_hour, client_id, value });

                    ////////////////hamid 







                    if (tmp_shamsi.Equals(old_date) == false || tmp_hour.Equals(old_time) == false)
                    {

                        rowIndex = this.dg_show_onlinedata_newformat.Rows.Add();
                        row = this.dg_show_onlinedata_newformat.Rows[rowIndex];
                        current_row = rowIndex;

                        old_date = tmp_shamsi.ToString();
                        old_time = tmp_hour.ToString();
                        cnt++;

                    }






                    row.Cells["index"].Value = cnt.ToString() +"-"+ ordinal_number.ToString ();
                    row.Cells["Date"].Value = tmp_shamsi.ToString();
                    row.Cells["time"].Value = tmp_hour.ToString();






                    string str_column_name = channel_index.ToString();
                    // str_column_name = "58";

                    // value = Math.Round(value, 2);
                    float d = float.Parse(value.ToString());
                    string s = d.ToString("N2");
                    row.Cells[str_column_name].Value = s.ToString();

                }


                if (chk_excel.Checked == true)
                {




                    if (tmp_shamsi.Equals(old_date) == false || tmp_hour.Equals(old_time) == false)
                    {


                        savedata = "";

                        string savedata1 = cnt_write_excell.ToString() + "," + tmp_shamsi.ToString() + "," + tmp_hour.ToString();
                        // string savedata_2 = "";

                        float d = float.Parse(value.ToString());
                        string s = d.ToString("N2");
                        string Value_ = s.ToString();

                        sensors_values_excell[cnt] = Value_.ToString();
                        ////savedata_2 = Value_.ToString()+",";
                        ////savedata_2 = savedata_2 + savedata_2;

                        cnt++;

                        if (cnt == cnt_lstcheked)
                        {


                            for (int i = 0; i < cnt_lstcheked; i++)
                            {

                                savedata2 = savedata2 + "," + sensors_values_excell[i].ToString();
                                sensors_values_excell[i] = "";

                            }


                            savedata = savedata1 + savedata2;

                            old_date = tmp_shamsi.ToString();
                            old_time = tmp_hour.ToString();


                            filewriter.WriteLine(savedata);

                            savedata = "";
                            savedata1 = "";
                            savedata2 = "";
                            cnt = 0;
                            cnt_write_excell++;






                        }








                    }


















                }







            }


          





            if (chk_excel.Checked == true)
            {


                filewriter.Close();


                dr.Close();
                label123.Text = "پایان ایجاد فایل";

            }
            else
            {

                dr.Close();
                Application.DoEvents();
                label123.Text = "Record Retrived ::" + cnt.ToString();




                if (pnl_set_hour.Visible == true)
                {
                    
                    
                    //label116_Click(null, null);
                 //  label118_Click(null, null);

                    if (rd_day.Checked == true)
                    {
                        label119_Click(null, null);
                    }
                    else
                    {
                        label116_Click(null, null);
                    }
                    
                    Application.DoEvents();
                    tabControl3.SelectedTab = tabPage12;
                    // return;

                }
                else
                {
                    tabControl3.SelectedTab = tabPage12;

                }



                  

                
                
                
                
                
                if (chk_graph.Checked == true)////draw chart
                {

                    lbl_draw_chart_Click(null, null);

                }


            }






        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {

            //   string flname = Application.StartupPath + "\\screen\\Als_rds.png";


            //   System.IO.StreamWriter filewriter = new System.IO.StreamWriter(flname);


            String address_ip = "", port = "", user = "", pass = "", db = "";

            address_ip = txt_ip.Text;
            port = txt_port.Text;
            user = txt_user.Text;
            pass = txt_pass.Text;
            db = txt_db.Text;



            string tmpsave;





            tmpsave = address_ip + "-" + port + "-" + user + "-" + pass + "-" + db;


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            OleDbDataAdapter da3 = new OleDbDataAdapter();

            //UPDATE Categories SET CategoryName = @CategoryName ";
            da3.SelectCommand = new OleDbCommand("update   tblostan_station set code_connect ='" + tmpsave + "'  where id =" + glb_id_server_code + "   ", myconn);
            da3.Fill(ds, "tblostan_station");
            dt = ds.Tables["tblostan_station"];


            try
            {
                da3.Update(dt);
            }
            catch (Exception ex)
            {

                int ph = 0;
                //throw ex;
            }







            MessageBox.Show("تنظیمات سرور ذخیره شد ");




        }

        private void label131_Click(object sender, EventArgs e)
        {

            // dlgOpenFile.FileName = "Rain_Total.conf";


            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\screen\\Als_rds.png";


            Application.DoEvents();
            Application.DoEvents();







            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);


            readline = "";
            readline = Filereader.ReadLine();

            string s = readline;








            string[] values = s.Split('-');




            String address_ip = "", port = "", user = "", pass = "", db = "";

            address_ip = values[0];
            port = values[1];
            user = values[2];
            pass = values[3];
            db = values[4];

            txt_ip.Text = address_ip.ToString();
            txt_port.Text = port.ToString();
            txt_user.Text = user.ToString();
            txt_pass.Text = pass.ToString();
            txt_db.Text = db.ToString();


            DataUtils.adrress_ip = address_ip.ToString();
            DataUtils.port_ = port.ToString();
            DataUtils.user_ = user.ToString();
            DataUtils.pass_ = pass.ToString();
            DataUtils.database_ = db.ToString();







            //////agar 6 ta bood marboot be  khat tanzimat ip,serevr hosr va port as 




            //txtrain_kol.Text = tmprain_kol.ToString();
            //txtrain_12.Text = tmprain_12.ToString();
            //txtrain_24.Text = tmprain_24.ToString();

            Filereader.Close();




        }

      

        private void tree_Ostan_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {


            if (e.Node.Level > 0)
            {
                return;
            }

            string code_ostan = "", name_station, id;

            e.Node.Nodes.Clear();
            code_ostan = e.Node.Tag.ToString();



            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            OleDbDataAdapter da3 = new OleDbDataAdapter();
            da3.SelectCommand = new OleDbCommand("select  * from tblostan_station where code_ostan=" + code_ostan + " order by id  ", myconn);
            da3.Fill(ds, "tbldatelog");
            dt = ds.Tables["tbldatelog"];



            int cnt = 0;

            TreeNode tr = new TreeNode();


            foreach (DataRow dr in dt.Rows)
            {
                cnt++;
                //  code = (dr["code"].ToString());
                id = (dr["id"].ToString());
                name_station = (dr["name_station"].ToString());




                TreeNode node1 = e.Node.Nodes.Add(name_station.ToString());
                node1.Tag = id;
                node1.ForeColor = Color.Red;

                //  e.Node.Nodes.Add(name_station);
                // e.Node.
                // node1.Nodes.Add("sample");






            }







        }

        private void tree_Ostan_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {


            //string code_ostan = "", name_station, id;


            //code_ostan = e.Node.Tag.ToString();



            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();

            //OleDbDataAdapter da3 = new OleDbDataAdapter();
            //da3.SelectCommand = new OleDbCommand("select  * from tblostan_station where code_ostan=" + code_ostan + " order by id  ", myconn);
            //da3.Fill(ds, "tbldatelog");
            //dt = ds.Tables["tbldatelog"];



            //int cnt = 0;

            //TreeNode tr = new TreeNode();


            //foreach (DataRow dr in dt.Rows)
            //{
            //    cnt++;
            //    //  code = (dr["code"].ToString());
            //    id = (dr["id"].ToString());
            //    name_station = (dr["name_station"].ToString());




            //    TreeNode node1 = e.Node.Nodes.Add(name_station.ToString());
            //    node1.Tag = id;
            //    node1.ForeColor = Color.Red;

            //    //  e.Node.Nodes.Add(name_station);
            //    // e.Node.
            //    // node1.Nodes.Add("sample");






            //}





        }





        private void add_server_Click(object sender, EventArgs e)
        {

            string code_ostan = "", txt_station_server_ = "";


            txt_station_server_ = txt_station_server.Text.ToString();

            // TreeNode node1 = e.Node.Nodes.Add(name_station.ToString());

            TreeNode node1 = tree_Ostan.SelectedNode;
            code_ostan = node1.Tag.ToString();




            if (txt_station_server.Text.Equals("") == true)
            {
                MessageBox.Show("نام سرور را اضافه نمایید");
                return;
            }


            if (node1.Level > 0)
            {
                MessageBox.Show("سرور باید به  استان اضافه شود");
                return;
            }





            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            OleDbDataAdapter da3 = new OleDbDataAdapter();
            da3.SelectCommand = new OleDbCommand("select  * from tblostan_station where code_ostan=" + code_ostan + "  and name_station='" + txt_station_server_ + "'  ", myconn);
            da3.Fill(ds, "tbldatelog");
            dt = ds.Tables["tbldatelog"];



            int cnt = 0;




            foreach (DataRow dr in dt.Rows)
            {
                cnt++;

            }



            if (cnt > 0)
            {
                MessageBox.Show("نام این سرور تکراری است");
                return;
            }






            DataSet oDS = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(new OleDbCommand("select * from tblostan_station", myconn));

            OleDbCommandBuilder oOrdersCmdBuilder = new OleDbCommandBuilder(da);
            da.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "tblostan_station";



            DataRow oOrderRow = oDS.Tables["tblostan_station"].NewRow();
            oOrderRow["code_ostan"] = code_ostan.ToString();
            oOrderRow["name_station"] = txt_station_server_.ToString();
            oDS.Tables["tblostan_station"].Rows.Add(oOrderRow);
            da.Update(oDS, "tblostan_station");







            ///////////


            TreeNode node2 = node1.Nodes.Add(txt_station_server_);
            node2.ForeColor = Color.Red;

            node2.Tag = "";
            node1.Expand();

            //  code_ostan = e.Node.Tag.ToString();
            return;


            ////////DataSet ds = new DataSet();
            ////////DataTable dt = new DataTable();

            ////////OleDbDataAdapter da3 = new OleDbDataAdapter();
            ////////da3.SelectCommand = new OleDbCommand("select  * from tblostan_station where code_ostan=" + code_ostan + " order by id  ", myconn);
            ////////da3.Fill(ds, "tbldatelog");
            ////////dt = ds.Tables["tbldatelog"];



            ////////int cnt = 0;

            ////////TreeNode tr = new TreeNode();


            ////////foreach (DataRow dr in dt.Rows)
            ////////{
            ////////    cnt++;
            ////////    //  code = (dr["code"].ToString());
            ////////    id = (dr["id"].ToString());
            ////////    name_station = (dr["name_station"].ToString());




            ////////    TreeNode node1 = e.Node.Nodes.Add(name_station.ToString());
            ////////    node1.Tag = id;
            ////////    node1.ForeColor = Color.Red;

            ////////    //  e.Node.Nodes.Add(name_station);
            ////////    // e.Node.
            ////////    // node1.Nodes.Add("sample");






            ////////}



        }

        private void add_server_Paint(object sender, PaintEventArgs e)
        {

        }

        private void remove_server_Click(object sender, EventArgs e)
        {

            string code_ostan = "", txt_station_server_ = "";


            //txt_station_server_ = txt_station_server.Text.ToString();

            // TreeNode node1 = e.Node.Nodes.Add(name_station.ToString());

            TreeNode node1 = tree_Ostan.SelectedNode;

            code_ostan = node1.Parent.Tag.ToString();

            string name_station = node1.Text.ToString();


            if (code_ostan.Equals("") == true)
            {
                MessageBox.Show("نام سرور را انتخا نمایید");
                return;
            }


            if (node1.Level == 0)
            {
                MessageBox.Show("نمی توان استان را حذف نمود");
                return;
            }



            string message = "آیا قصد حذف نام سرور را دارید ؟";

            string caption = "حذف نام سرور";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                return;

            }







            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            OleDbDataAdapter da3 = new OleDbDataAdapter();
            da3.SelectCommand = new OleDbCommand("delete  from tblostan_station where code_ostan=" + code_ostan + "  and name_station='" + name_station + "'  ", myconn);
            da3.Fill(ds, "tblostan_station");
            dt = ds.Tables["tblostan_station"];


            try
            {
                da3.Update(dt);
            }
            catch (Exception ex)
            {

                int ph = 0;
                //throw ex;
            }


            node1.Remove();





        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {





            string code_ostan = "", id;


            TreeNode node1 = tree_Ostan.SelectedNode;

            code_ostan = node1.Parent.Tag.ToString();

            if (code_ostan.Equals("") == true)
            {
                MessageBox.Show("نام سرور را انتخا نمایید");
                return;
            }


            if (node1.Level < 1)
            {

                MessageBox.Show("نام سرور را انتخا نمایید");
                return;


            }


            string name_station = node1.Text.ToString();

            txt_station_server.Text = name_station.ToString();





            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            OleDbDataAdapter da3 = new OleDbDataAdapter();
            da3.SelectCommand = new OleDbCommand("select  * from tblostan_station where code_ostan=" + code_ostan + " and name_station='" + name_station + "' ", myconn);
            da3.Fill(ds, "tbldatelog");
            dt = ds.Tables["tbldatelog"];



            int cnt = 0;

            TreeNode tr = new TreeNode();


            foreach (DataRow dr in dt.Rows)
            {
                cnt++;
                //  code = (dr["code"].ToString());
                id = (dr["id"].ToString());

                glb_id_server_code = int.Parse(id.ToString());

                glb_server_spec = (dr["code_connect"].ToString());
                name_station = (dr["name_station"].ToString());
                tabControl3.Visible = true;
                tabControl3.BringToFront();

                txt_neame_server_edit.Text = name_station.ToString();
                label109_Click(null, null);

                tabControl3.SelectedTab = tabPage10;






            }




        }

        private void label109_Click(object sender, EventArgs e)
        {


            string readline = "";


            txt_ip.Text = "";
            txt_port.Text = "";
            txt_user.Text = "";
            txt_pass.Text = "";
            txt_db.Text = "";

            Application.DoEvents();
            Application.DoEvents();








            string s = glb_server_spec.ToString();
            // string s = readline;


            if (s.Equals("") == true)
            {
                return;
            }





            string[] values = s.Split('-');




            String address_ip = "", port = "", user = "", pass = "", db = "";

            address_ip = values[0];
            port = values[1];
            user = values[2];
            pass = values[3];
            db = values[4];

            txt_ip.Text = address_ip.ToString();
            txt_port.Text = port.ToString();
            txt_user.Text = user.ToString();
            txt_pass.Text = pass.ToString();
            txt_db.Text = db.ToString();


            DataUtils.adrress_ip = address_ip.ToString();
            DataUtils.port_ = port.ToString();
            DataUtils.user_ = user.ToString();
            DataUtils.pass_ = pass.ToString();
            DataUtils.database_ = db.ToString();










        }

        private void groupBox16_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {

            DataUtils.disconnect_db();


            String address_ip = "", port = "", user = "", pass = "", db = "";


            address_ip = txt_ip.Text;
            port = txt_port.Text;
            user = txt_user.Text;
            pass = txt_pass.Text;
            db = txt_db.Text;




            DataUtils.adrress_ip = address_ip.ToString();
            DataUtils.port_ = port.ToString();
            DataUtils.user_ = user.ToString();
            DataUtils.pass_ = pass.ToString();
            DataUtils.database_ = db.ToString();







            if (!DataUtils.connect_db())
            {

                MessageBox.Show("ارتباط با سرور مقدور نمی باشد");
                return;
            }
            else
            {

                MessageBox.Show("ارتباط با سرور برقرار شد");

            }





        }

        private void pnlTree_Ostan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {





            rdcalculated.Checked = true;
            // rdinstant.Checked = false;
            pictureBox11_Click(null, null);

            Application.DoEvents();
            Application.DoEvents();


            ////chkautomated.Checked = true;
            ////tmrfetchdata.Enabled = true;




        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {


            rdinstant.Checked = true;

            pictureBox11_Click(null, null);


            Application.DoEvents();

            // label106_Click_2(null, null);

            Application.DoEvents();

            //chkautomated.Checked = true;
            //tmrfetchdata.Enabled = true;



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            ////if (checkBox1.Checked == true)
            ////{

            ////    tmrfetchdata.Enabled = true;





            ////}

            ////else
            ////{
            ////    tmrfetchdata.Enabled = false;


            ////}



        }

        private void tmr_show_digital_Tick(object sender, EventArgs e)
        {


            rdcalculated.Checked = true;
            pictureBox11_Click(null, null);

            Application.DoEvents();
            Application.DoEvents();


            rdinstant.Checked = true;

            pictureBox11_Click(null, null);

            Application.DoEvents();


            label106_Click_2(null, null);

            // Application.DoEvents();






        }

        private void label106_Click(object sender, EventArgs e)
        {
            tmr_show_digital.Enabled = true;

        }

        private void label111_Click(object sender, EventArgs e)
        {


            
          


            pnlCalibration.Visible = false;
            pnlSensors.Visible = false;
            pnlhelp.Visible = false;
            pnlshowdata.Visible = false;
            pnlloaddata.Visible = false;

            pnlhashcode.Visible = false;
            pnldatatransfer.Visible = false;
            pnlshowdata.Visible = false;
            pnlloaddata.Visible = false;

            pnlSensors.Visible = false;
            pnlCalibration.Visible = false;
            pnlweb.Visible = false;



            pnl_show_digital_rain.Left = 251;
            pnl_show_digital_rain.Top = 206;


            pnl_show_digital_rain.Visible = true;
            Application.DoEvents();

            if (glbguid == false)
            {

                MessageBox.Show("شما مجاز به استفاده از نرم افزار نیستید");
                return;

            }
            
            pictureBox27_Click(null, null);
            pictureBox26_Click(null, null);

            Application.DoEvents();

            tmr_show_digital.Enabled = true;
        }

        private void pictureBox18_Click_2(object sender, EventArgs e)
        {

            pnl_show_digital_rain.Visible = false;
            Application.DoEvents();
            tmr_show_digital.Enabled = false;


        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            //////if (Convert.ToInt32(e.KeyChar) == 13)
            //////{


            //////    float  vlotage = float.Parse(textBox2.Text.ToString());
            //////   // int percent = 0;
            //////    string percent = "";

            //////    if (vlotage >= 13.50)
            //////    {

            //////        percent = "100";
            //////        batt_logger.Value = int.Parse(percent);
            //////        batt_logger.Color = Color.Lime;
            //////        lblstatus_battery.Text = percent.ToString() + " % ";
            //////         return;

            //////    }




            //////    if (vlotage <= 10)
            //////    {

            //////        percent = "1";
            //////        batt_logger.Value = int.Parse(percent);
            //////        batt_logger.Color = Color.Red ;
            //////        lblstatus_battery.Text = percent.ToString() + " % ";
            //////        return;

            //////    }


            //////    for (int i = 0; i < 7; i++)
            //////    {


            //////        if (vlotage == battery_voltag[i] )
            //////        {


            //////            percent = battery_values[i].ToString();
            //////            batt_logger.Value = int.Parse(percent);
            //////            batt_logger.Color = Color.Red;
            //////            lblstatus_battery.Text = percent.ToString() + " % ";
            //////            return;

            //////        }



            //////    }






            //////    int upbound=0, downbound = 0,avg=0;


            //////    for (int i = 0; i <6; i++)
            //////    {



            //////        if (vlotage < battery_voltag[i + 1] && vlotage > battery_voltag[i])
            //////        {

            //////            downbound = battery_values[i];
            //////            upbound = battery_values[i + 1];
            //////            avg = (downbound + upbound) / 2;

            //////            percent = avg.ToString();
            //////            batt_logger.Value = int.Parse(percent);
            //////            batt_logger.Color = Color.Red;
            //////            lblstatus_battery.Text = percent.ToString() + " % ";
            //////            return;



            //////        }



            //////    }







            //////}

        }

        private void label106_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label106_Click_2(object sender, EventArgs e)
        {

            //////string txt = textBox2.Text;
            //////if (txt.Equals("") == true)
            //////{
            //////    return;
            //////}
            //////glb_BATA_show_gauge = txt;


            float vlotage = float.Parse(glb_BATA_show_gauge.ToString());
            // int percent = 0;
            string percent = "";
            // batt_logger.Value = 1;


            int array_size = battery_voltag.Length;
            if (vlotage >= battery_voltag[array_size - 1])
            {

                percent = "100";
                batt_logger.Value = int.Parse(percent);
                batt_logger.Color = Color.Lime;
                lblstatus_battery.Text = percent.ToString() + " % ";
                return;

            }




            if (vlotage <= battery_voltag[0])
            {

                percent = "1";
                batt_logger.Value = int.Parse(percent);
                // batt_logger.Color = Color.Red ;
                lblstatus_battery.Text = percent.ToString() + " % ";
                return;

            }


            for (int i = 0; i < array_size; i++)
            {


                if (vlotage == battery_voltag[i])
                {


                    percent = battery_values[i].ToString();
                    batt_logger.Value = int.Parse(percent);
                    batt_logger.Color = Color.Red;
                    lblstatus_battery.Text = percent.ToString() + " % ";
                    return;

                }



            }






            int upbound = 0, downbound = 0;
            float avg = 0;

            for (int i = 0; i < array_size - 1; i++)
            {



                if (vlotage < battery_voltag[i + 1] && vlotage > battery_voltag[i])
                {

                    downbound = battery_values[i];
                    upbound = battery_values[i + 1];
                    avg = (downbound + upbound) / 2;

                    percent = avg.ToString();
                    batt_logger.Value = int.Parse(percent);
                    batt_logger.Color = Color.Red;
                    lblstatus_battery.Text = percent.ToString() + " % ";
                    return;



                }


            }


        }

        private void txtsearch_in_grid_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Convert.ToInt32(e.KeyChar) == 13)
            {


                string searchValue = txtsearch_in_grid.Text;

                if (searchValue.Equals("") == true)
                {

                    return;

                }


                string s = searchValue;
                string[] values = s.Split('/');


                int s_day = int.Parse(values[2]);
                int s_mount = int.Parse(values[1]);
                int s_year = int.Parse(values[0]);



                // int rowIndex = -1;
                int col = 0;



                // if (cmbselextsearch.Text == "روز") col = 4;
                ////if (cmbselextsearch.Text == "ماه") col = 3;
                ////if (cmbselextsearch.Text == "سال") col = 2;


                //   dtgshowdatapast.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //    dtgshowdatapast.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;

                col = 4;//day
                int inx_found = 0;

                searchValue = s_day.ToString();
                for (int i = 0; i < dtgshowdatapast.Rows.Count - 1; i++)
                {

                    //  string a= dtgshowdataonline.Rows[i].;


                    if (dtgshowdatapast.Rows[i].Cells[col].Value.ToString().Equals(searchValue) == true)
                    {
                        //rowIndex = Rows.Index;
                        dtgshowdatapast.Focus();
                        dtgshowdatapast.Rows[i].Selected = true;
                        // dtgshowdatapast.CurrentCell = dtgshowdatapast[4, i];
                        // dtgshowdatapast.CurrentCell = dataGridView1.Rows[0].Cells[0];
                        inx_found = i;
                        break;
                    }


                }




                int b = inx_found;

                int col_mounth = 3;
                int col_year = 2;
                string searchValue_mount = "", searchValue_year = "";

                searchValue_mount = s_mount.ToString();
                searchValue_year = s_year.ToString();


                if (dtgshowdatapast.Rows[inx_found].Cells[col_mounth].Value.ToString().Equals(searchValue_mount) == true && dtgshowdatapast.Rows[inx_found].Cells[col_year].Value.ToString().Equals(searchValue_year) == true)
                {

                    dtgshowdatapast.CurrentCell = dtgshowdatapast[4, inx_found];


                }






            }




        }

        private void txtsearch_in_grid_TextChanged(object sender, EventArgs e)
        {

        }



        public void load_battery_calib()
        {


            ////battery_voltag[0] = 10;
            ////battery_voltag[1] = 10.5f;
            ////battery_voltag[2] = 10.9f;
            ////battery_voltag[3] = 11.8f;
            ////battery_voltag[4] = 12.2f;
            ////battery_voltag[5] = 12.6f;
            ////battery_voltag[6] = 13.5f;




            ////battery_values[0] = 1;
            ////battery_values[1] = 5;
            ////battery_values[2] = 15;
            ////battery_values[3] = 50;
            ////battery_values[4] = 80;
            ////battery_values[5] = 90;
            ////battery_values[6] = 100;







            string flname = "";
            string readline = "";

            flname = Application.StartupPath + "\\configdowmload\\Battery_voltage_oercent.conf";


            Application.DoEvents();
            Application.DoEvents();

            //counting line numbers
            int cnt_array = 0;
            System.IO.StreamReader Filereader = new System.IO.StreamReader(flname);

            while ((readline = Filereader.ReadLine()) != null)
            {

                cnt_array++;

            }/////end of first  loof reading line count 


            Filereader.Close();

            ///////////////////////////////////////////////////////////////////////////
            Application.DoEvents();
            Application.DoEvents();


            battery_voltag = new float[cnt_array];


            battery_values = new int[cnt_array];


            Filereader = new System.IO.StreamReader(flname);



            int cnt = 0;


            while ((readline = Filereader.ReadLine()) != null)
            {



                if (readline.IndexOf("#") != -1)
                {
                    continue;
                }



                if (readline == "")
                {
                    continue;
                }




                string s = readline;
                string[] values = s.Split(',');

                battery_voltag[cnt] = float.Parse(values[0]);
                battery_values[cnt] = int.Parse(values[1]);


                if (readline == null)
                {


                    break;


                }





                if (readline.IndexOf("#") != -1)
                {


                    continue;


                }


                cnt++;

            }/////end op loof reading file




            Filereader.Close();


            Application.DoEvents();







        }

        private void button1_Click_4(object sender, EventArgs e)
        {


            int a = 0;
            DateTimePicker timePicker = new DateTimePicker();
            timePicker = new DateTimePicker();
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.ShowUpDown = true;
            timePicker.Location = new Point(10, 10);
            timePicker.Width = 100;
            Controls.Add(timePicker);



        }

        private void New_Click(object sender, EventArgs e)
        {



            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program
            app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            // changing the name of active sheet
            worksheet.Name = "Exported from scdl";


            // storing header part in Excel
            for (int i = 1; i < 30; i++)
            {
                //  worksheet.Cells[1, i] = dtgshowdatapast.Columns[i - 1].HeaderText;
            }

            lblstatusftp2.Text = "در حال ایجاد فایل اکسل";

            // storing Each row and column value to excel sheet
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {


                    worksheet.Cells[i + 2, j + 1] = (i + j).ToString();
                    Application.DoEvents();
                    // lbProgress2.Text = i.ToString();



                }
            }


            // save the application
            workbook.SaveAs("c:\\output.csv", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Exit from the application
            // app.Quit();





        }

        private void button1_Click_5(object sender, EventArgs e)
        {


            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program
            app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet

            //worksheet.n
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            // changing the name of active sheet
            worksheet.Name = "Exported from scdl";


            //Application excel = new Application();
            workbook = app.Workbooks.Open("c:\\Test_chart.xls");
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkSheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);



            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            Excel.Chart chartPage = myChart.Chart;


            chartRange = xlWorkSheet.get_Range("A1", "d5");
            chartPage.SetSourceData(chartRange, misValue);
            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
            // chartPage.ChartType = Excel.XlChartType.xlConeBarStacked;







        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program
            app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet

            //worksheet.n
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            // changing the name of active sheet
            worksheet.Name = "Exported from scdl";


            //Application excel = new Application();
            workbook = app.Workbooks.Open("c:\\new4.xlsx");
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkSheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);



            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            Excel.Chart chartPage = myChart.Chart;


            // chartRange = xlWorkSheet.get_Range("c1", "d1181");
            chartRange = xlWorkSheet.get_Range("c1", "d100");
            chartPage.SetSourceData(chartRange, misValue);
            // chartPage.ChartType = Excel.XlChartType.xlRadar;
            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;






        }

        private void lbl_draw_chart_Click(object sender, EventArgs e)

            
        {

            string[] str_max_min_sensors_temp=str_max_min_sensors.Split ('-');

            bool one_chart = false;

            if (str_max_min_sensors_temp.Length <= 2)
            {

                one_chart = true;

            }
            else
            {

                one_chart = false;
            
            }

           // str_max_min_sensors = "";

//////////////////////////////////////////////////////////


            string flname = "c:\\2233.csv";
            string savedata = "";

           System.IO.StreamWriter filewriter = new System.IO.StreamWriter(flname);

            //adding header

            




            for (int r = 0; r < dg_show_onlinedata_newformat.ColumnCount; r++)
            {


                if (r != 1)
                {
                    savedata = savedata + dg_show_onlinedata_newformat.Columns[r].HeaderText.ToString() + ",";
                }
                else
                {
                    savedata = savedata + dg_show_onlinedata_newformat.Columns[r].HeaderText.ToString() ;
                }

             

            }


            //////////////////////find min & max////////////////////////
            float find_max=0;
            float find_min = 0;


            float find_max_2 = 0;
            float find_min_2 = 0;

              find_max = float.Parse(dg_show_onlinedata_newformat.Rows[1].Cells[3].Value.ToString());
              find_min = float.Parse(dg_show_onlinedata_newformat.Rows[1].Cells[3].Value.ToString());


              if (one_chart == false)
              {
                  find_max_2 = float.Parse(dg_show_onlinedata_newformat.Rows[1].Cells[4].Value.ToString());
                  find_min_2 = float.Parse(dg_show_onlinedata_newformat.Rows[1].Cells[4].Value.ToString());
              }

            
            
            int count_record = dg_show_onlinedata_newformat.Rows.Count - 1;

            for (int r =1; r < dg_show_onlinedata_newformat.Rows.Count-1 ; r++)
            {

                 
                 float tmp_max =float.Parse ( dg_show_onlinedata_newformat.Rows[r].Cells[3].Value.ToString ());
                 float tmp_min = float.Parse(dg_show_onlinedata_newformat.Rows[r].Cells[3].Value.ToString());

                 if (tmp_max > find_max)
                 {
                     find_max = tmp_max;
                 }



                 if (tmp_min < find_min)
                 {
                     find_min = tmp_min;
                 }

                                
            }



            


            find_max = find_max + 1f;
            find_min = find_min - 1f;


            ////////////////////////////////////////////graph_2//////////////////////////


            if (one_chart == false)
            {

                for (int r = 1; r < dg_show_onlinedata_newformat.Rows.Count - 1; r++)
                {

                    float tmp_max_2 = float.Parse(dg_show_onlinedata_newformat.Rows[r].Cells[4].Value.ToString());
                    float tmp_min_2 = float.Parse(dg_show_onlinedata_newformat.Rows[r].Cells[4].Value.ToString());

                    if (tmp_max_2 > find_max_2)
                    {
                        find_max_2 = tmp_max_2;
                    }



                    if (tmp_min_2 < find_min_2)
                    {
                        find_min_2 = tmp_min_2;
                    }


                }
            
            }


            find_max_2 = find_max_2 + 1f;
            find_min_2 = find_min_2 - 1f;



            /////////////////////////////////////////////
           filewriter.WriteLine(savedata);
           filewriter.Close();
            savedata = "";

            int row_count = dg_show_onlinedata_newformat.RowCount;
            string str_row_count = row_count.ToString();

            chart1.Series.Clear();

           





            var series = chart1.Series.Add("Time-value");//("Time-value");

             var chartarea = chart1.ChartAreas[series.ChartArea];
            // chartarea.AxisX.ScaleView.Zoom(1,100);
             chartarea.AxisX.ScaleView.Zoom(1, count_record);
            

            //chartarea.AxisX.ScaleView.SizeType =DateTimein

           // Charta
            chartarea.CursorX .IsUserSelectionEnabled =true;
            
            chartarea.AxisX.Minimum = 0;
            //chartarea.AxisX.Maximum = 100;
            chartarea.AxisX.Maximum = count_record;


           // int idx = int.Parse(str_max_min_sensors_temp[0].ToString());

            int  idx,comp1,comp2;
            float max=0, min=0, max2=0, min2=0;

            //////if (one_chart == false)
            //////{
            //////     comp1 = int.Parse(str_max_min_sensors_temp[0].ToString());
            //////     comp2 = int.Parse(str_max_min_sensors_temp[1].ToString());

            //////    if (comp1 > comp2)
            //////    {

            //////         max = sensors_index_max[comp2];
            //////         min = sensors_index_min[comp2];

            //////         max2 = sensors_index_max[comp1];
            //////         min2 = sensors_index_min[comp1];


            //////    }
            //////    else
            //////    {

            //////        max = sensors_index_max[comp1];
            //////        min = sensors_index_min[comp1];

            //////        max2 = sensors_index_max[comp2];
            //////        min2 = sensors_index_min[comp2];




            //////    }






            //////}
            //////else
            //////{

            //////     idx = int.Parse(str_max_min_sensors_temp[0].ToString());
            //////     max = sensors_index_max[idx];
            //////     min = sensors_index_min[idx];
            
            //////}




         





            
            
            chartarea.AxisY.Minimum = find_min;
            //chartarea.AxisY2.Minimum 
            chartarea.AxisY.Maximum = find_max;

            chartarea.CursorX.AutoScroll = true;
            chartarea.AxisX.ScaleView.Zoomable = true;
            ////////////////////////////////////////////////////////////////////////
            chart2.Series.Clear();

            var series2 = chart2.Series.Add("Time-value");//("Time-value");

            var chartarea2 = chart2.ChartAreas[series2.ChartArea];
            chartarea2.AxisX.ScaleView.Zoom(1, count_record);
            //chartarea.AxisX.ScaleView.SizeType =DateTimein

            // Charta
            chartarea2.CursorX.IsUserSelectionEnabled = true;

            ////float max2=0,min2=0;
            ////if (one_chart == false)
            ////{

            ////    int idx2 = int.Parse(str_max_min_sensors_temp[2].ToString());

            ////     max2 = sensors_index_max[idx];
            ////     min2 = sensors_index_min[idx];
            
            ////}
            
            chartarea2.AxisX.Minimum = 0;
            chartarea2.AxisX.Maximum = count_record;

            chartarea2.AxisY.Minimum = find_min_2;
            //chartarea.AxisY2.Minimum 
            chartarea2.AxisY.Maximum = find_max_2;

            chartarea2.CursorX.AutoScroll = true;
            chartarea2.AxisX.ScaleView.Zoomable = true;




            //////var series2 = chart1.Series.Add("Time-value-2");//("Time-value");

            //////var chartarea2 = chart1.ChartAreas[series2.ChartArea];
            //////chartarea2.AxisX.ScaleView.Zoom(1, 100);
            ////////chartarea.AxisX.ScaleView.SizeType =DateTimein

            //////// Charta
            //////chartarea2.CursorX.IsUserSelectionEnabled = true;
            //////chartarea2.AxisX.Minimum = 0;
            //////chartarea2.AxisX.Maximum = 100;

            //////chartarea2.AxisY.Minimum = 0;
            //////chartarea2.AxisY.Maximum = 100;

            //////chartarea2.CursorX.AutoScroll = true;
            //////chartarea2.AxisX.ScaleView.Zoomable = true;

            //////////////////////////////////////////////////////////////////////







            Random random = new Random();

            for (int k = 0; k < dg_show_onlinedata_newformat.RowCount - 1; k++)
           // for (int k = 0; k < 6 - 1; k++)
            {



                for (int index = 0; index < dg_show_onlinedata_newformat.ColumnCount; index++)
                {


                    if (dg_show_onlinedata_newformat.Rows[k].Cells[index].Value != null)
                    {



                        if (index != 1)
                        {

                            savedata = savedata + dg_show_onlinedata_newformat.Rows[k].Cells[index].Value.ToString() + ",";

                        }
                        else
                        {
                            savedata = savedata + dg_show_onlinedata_newformat.Rows[k].Cells[index].Value.ToString() + "-";
                        }



                    }



                }



              //  filewriter.WriteLine(savedata);

              

                 string[] seperate_date = savedata.Split(',');

                 string x = seperate_date[1];
                 float y = float.Parse(seperate_date[2]);

                 float z = 0; ;

                 if (one_chart == false)
                 {
                     z = float.Parse(seperate_date[3]);
                 }



                 if (rd_line.Checked == true)
                 {
                     chart1.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                     chart2.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                     //chart1.Series["Time-value-2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                 }

                 else
                 {
                     chart1.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                     chart2.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    // chart1.Series["Time-value-2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                 }



                 if (chk_random.Checked == false)
                 {



                     chart1.Series["Time-value"].Points.AddXY(x, y);

                     if (one_chart == false)
                     {
                           chart2.Series["Time-value"].Points.AddXY(x, z);

                     }
                    // chart1.Series["Time-value-2"].Points.AddXY(x, random.Next(0, 100));
                     //chart1.min


                 }
                 else
                 {



                     chart1.Series["Time-value"].Points.AddXY(x, random.Next(20, 900));
                 }

               


                savedata = "";

            }


          
            
            ////pnl_show_graph.Width = 984;
            ////pnl_show_graph.Height = 485;

            
           

            if (one_chart == true)
            {

                chart1.Height = 488;
                chart2.Visible = false;
            }
            else
            {
                chart1.Height = 282;
            }

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            chart1.Visible = true;
            
            if (one_chart == false)
            {
                chart2.Visible = true;

            }


            Application.DoEvents();
            tabControl3.SelectedTab = tabPage13;



            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            ////pnl_show_graph.Visible = true;
            ////pnl_show_graph.BringToFront();



               Application.DoEvents ();
           //   filewriter.Close ();
             Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();


            /////draw charts////

           ////// Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


           ////// // creating new WorkBook within Excel application
           ////// Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


           ////// // creating new Excelsheet in workbook
           ////// Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

           ////// // see the excel sheet behind the program
           ////// app.Visible = true;

           ////// // get the reference of first sheet. By default its name is Sheet1.
           ////// // store its reference to worksheet

           ////// //worksheet.n
           ////// worksheet = workbook.Sheets["Sheet1"];
           ////// worksheet = workbook.ActiveSheet;

           ////// // changing the name of active sheet
           ////// worksheet.Name = "Exported from scdl";


           ////// //Application excel = new Application();
           ////// workbook = app.Workbooks.Open(flname);
           ////// Excel.Worksheet xlWorkSheet;
           ////// object misValue = System.Reflection.Missing.Value;

           ////// xlWorkSheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);



           ////// Excel.Range chartRange;

           ////// Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
           //////// Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);

           ////// Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 10, 1000, 300);

           ////// Excel.Chart chartPage = myChart.Chart;


           ////// // chartRange = xlWorkSheet.get_Range("c1", "d1181");
           ////// chartRange = xlWorkSheet.get_Range("B2", "C" + str_row_count);
           ////// chartPage.SetSourceData(chartRange, misValue);
           ////// // chartPage.ChartType = Excel.XlChartType.xlRadar;
           ////// chartPage.ChartType = Excel.XlChartType.xlColumnClustered;



        
        
        
        
        
        
        }

        private void pictureBox19_Click_2(object sender, EventArgs e)
        {

            pnl_show_graph.Visible = false;

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void rd_pipe_CheckedChanged(object sender, EventArgs e)
        {

            if (rd_pipe.Checked == true)
            {

                //chart1.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                lbl_draw_chart_Click(null, null);
            }



        }

        private void rd_line_CheckedChanged(object sender, EventArgs e)
        {

            if (rd_line.Checked == true)
            {

                //chart1.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line ;

                lbl_draw_chart_Click(null, null);
            }


        }

        private void groupBox17_Enter(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rd_hour_CheckedChanged(object sender, EventArgs e)
        {

            if (rd_hour.Checked == true)
            {

                pnl_set_hour.Visible = true;
                label115.Visible = false;
                cmb_min_set.Visible = false;

            }


        }

        private void rd_day_CheckedChanged(object sender, EventArgs e)
        {

            if (rd_day.Checked == true)
            {

                pnl_set_hour.Visible = true;
                label115.Visible = true;
                cmb_min_set.Visible = true;
            }


        }

        private void rd_reg_CheckedChanged(object sender, EventArgs e)
        {

            if (rd_reg.Checked == true)
            {

                pnl_set_hour.Visible = false;
            }

        }

        private void rd_line_CheckedChanged_1(object sender, EventArgs e)
        {

            if (rd_line.Checked == true)
            {

                //chart1.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line ;

                lbl_draw_chart_Click(null, null);
            }


        }

        private void rd_pipe_CheckedChanged_1(object sender, EventArgs e)
        {

            if (rd_pipe.Checked == true)
            {

                //chart1.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                lbl_draw_chart_Click(null, null);
            }

        }






        private void label116_Click(object sender, EventArgs e)
        {

             //dg_show_onlinedata


            //int a = dg_show_onlinedata.ColumnCount;
            dg_show_onlinedata.Visible = true;
            dg_show_onlinedata_newformat.Visible = false;


            Application.DoEvents();

           // dg_show_onlinedata.Width = 650;
            dg_show_onlinedata.Height = dg_show_onlinedata.Height;

            dg_show_onlinedata.Rows.Clear();
          //  dg_show_onlinedata.Columns.Clear();

            ////dg_show_onlinedata.Columns.Add("index", "index");
            ////dg_show_onlinedata.Columns.Add("Date", "Date");
            ////dg_show_onlinedata.Columns.Add("Time", "Time");
            ////dg_show_onlinedata.Columns.Add("pressure", "pressure");



            ////dg_show_onlinedata.Columns[0].Width = 50;
            ////dg_show_onlinedata.Columns[1].Width = 80;
            ////dg_show_onlinedata.Columns[2].Width = 80;
            ////dg_show_onlinedata.Columns[3].Width = 50;



            int rowIndex = 0, current_row,cnt=0;
            var row = this.dg_show_onlinedata.Rows[rowIndex];
            /////////////////////////////////////////////////

            string old_day = "";
            string old_hour = "";
            string set_time_hour = "";
            string set_time_hour_pm_am = "";
            string hour_select_user = "";


            int start_grid = 0;

            set_time_hour = cmb_hour_set.Text;


            if (set_time_hour.Equals("") == true && pnl_set_hour.Visible == true)
            {
                MessageBox.Show("لطفا زمان را انتخاب نمایید");
            }




            while (true)
            {
                            set_time_hour = cmb_hour_set.Text;
                            int val_set_time_hour = int.Parse(set_time_hour.ToString());

                            if (val_set_time_hour > 12)
                            {
                                val_set_time_hour = val_set_time_hour - 12;
                                set_time_hour_pm_am = "pm";

                            }
                            else
                            {
                                set_time_hour_pm_am = "am";
                            }


                          

                            if (val_set_time_hour < 10)
                            {
                                hour_select_user = "0" + val_set_time_hour.ToString();
                            }
                            else
                            {
                                hour_select_user = val_set_time_hour.ToString();
                            }

                        ////////////////////////////////////////////////////////////////
                        /////////////////////////finding //////////////////////////////////////



          

                                for (int k = 0; k < dg_show_onlinedata_newformat.RowCount - 1; k++)
                                {

                                    string tmp_hour = "";
                                    tmp_hour = dg_show_onlinedata_newformat.Rows[k].Cells[2].Value.ToString();

                                    string[] ptimes = tmp_hour.Split(new char[] { ':' });


                                    string hour_now = ptimes[0];
                                    string min_now = ptimes[1];


                                    string pm_am = ptimes[2];


                                    if (pm_am.Contains("P") == true)
                                    {
                                        pm_am = "pm";
                                    }
                                    else
                                    {
                                        pm_am = "am";

                                    }


                                    if (hour_select_user.Trim().Equals(hour_now.Trim()) == true && set_time_hour_pm_am.Equals(pm_am) == true)
                                    {
                                        old_hour = hour_now;
                                        start_grid = k;
                                        break;

                                    }
                                    else
                                    {
                                        old_hour = hour_now;
                                        start_grid=-1;
                                        continue;

                                    }



                                }





                                if (start_grid == -1)
                                {

                                    int idx = cmb_hour_set.SelectedIndex;
                                    cmb_hour_set.SelectedIndex = idx + 1;

                                }
                                else
                                {
                                    break;
                                }



            
            
            }



            if (start_grid==-1 )
            {


                MessageBox.Show("اطلاعات با این ساعت ذخیره شده وجود ندارد");
                return;


            }

            old_hour = "";
            old_day = "";


            ////////////////////////////////////////////////////////////////
            
            for (int i=start_grid; i < dg_show_onlinedata_newformat.RowCount - 1; i++)
            
            
            {

                         string Shamsi, tmp_hour = "";

                       // DateTime miladi = DateTime.Now;
                        // miladi = DateTime.Parse(time_stamp);
                  
                          Shamsi =dg_show_onlinedata_newformat.Rows[i].Cells[1].Value.ToString ();
                          tmp_hour = dg_show_onlinedata_newformat.Rows[i].Cells[2].Value.ToString();
                                  

                          string[] Shamsi_seprate_to = Shamsi.Split(new char[] { '/' });
                          string day_now = "";
                          day_now = Shamsi_seprate_to[2];


                  
                
                               string[] ptimes = tmp_hour.Split(new char[] { ':' });
                                  
                
                               string hour_now = ptimes[0];
                               string min_now = ptimes[1];


                              string pm_am = ptimes[2];


                              if (pm_am.Contains("P") == true)
                              {
                                  pm_am = "pm";
                              }
                              else
                              {
                                  pm_am = "am";

                              }


                          


                
                                  ////////////////////////////////////////////////////////////////////////////

                                  if (rd_hour.Checked == true)/// for view only distinct hours
                                  {
                                      if (hour_now.Equals(old_hour) == true)
                                      {
                                          old_hour = hour_now;
                                          continue;

                                      }
                                      else
                                      {


                                          //if (hour_select_user.Trim().Equals(hour_now.Trim()) == true && set_time_hour_pm_am.Equals(pm_am) == true)
                                          //{

                                              cnt++;
                                              old_hour = hour_now;

                                              rowIndex = this.dg_show_onlinedata.Rows.Add();
                                              row = this.dg_show_onlinedata.Rows[rowIndex];
                                              current_row = rowIndex;

                                              row.Cells["index"].Value = cnt.ToString();
                                          
                                          for (int r = 1; r <= dg_show_onlinedata_newformat.ColumnCount - 1; r++)
                                              {

                                                  row.Cells[r].Value = dg_show_onlinedata_newformat.Rows[i].Cells[r].Value.ToString();


                                              }

                                           




                                        //  }


                                      }






                                  }

                              


                 



            }

            dg_show_onlinedata.Width = 745;
            dg_show_onlinedata.Visible = true;
            dg_show_onlinedata.BringToFront();

            
     


         
        



         











        }

        private void auto_recieve_CheckedChanged(object sender, EventArgs e)
        {
            rd_100.Checked = true;

            if (auto_recieve.Checked == true)
            {

                pnl_auto_recieve.Visible = true;
                tmr_fetch_dat_online.Enabled = true;


            }else

            {

                pnl_auto_recieve.Visible = false;
                tmr_fetch_dat_online.Enabled = false;

            }


        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {


            int interval = int.Parse(comboBox6.Text.ToString());

            tmr_fetch_dat_online.Interval = interval;

        }

        private void tmr_fetch_dat_online_Tick(object sender, EventArgs e)
        {

            rd_100.Checked = true;
            pictureBox21_Click(null, null);



        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {

        }

        private void chk_excel_CheckedChanged(object sender, EventArgs e)
        {

        }


        public void set_array_min_max()

        {

            sensors_index_max[0] = 445;
            sensors_index_max[1] = 445;
            sensors_index_max[2] = 445;
            sensors_index_max[3] = 445;


            sensors_index_min[0] = 0;
            sensors_index_min[1] = 0;
            sensors_index_min[2] = 0;
            sensors_index_min[3] = 0;

            ///////////////////////////////////////////////

            sensors_index_max[4] = 1051;
            sensors_index_max[5] = 1051;
            sensors_index_max[6] = 1051;
            sensors_index_max[7] = 1051;


            sensors_index_min[4] = 720;
            sensors_index_min[5] = 720;
            sensors_index_min[6] = 720;
            sensors_index_min[7] = 720;

            ////////////////////////////////////////////////

            sensors_index_max[8] = 60.1f;
            sensors_index_max[9] = 60.1f;
            sensors_index_max[10] = 60.1f;
            sensors_index_max[11] = 60.1f;


            sensors_index_min[8] = 0;
            sensors_index_min[9] = 0;
            sensors_index_min[10] = 0;
            sensors_index_min[11] = 0;

            ////////////////////////////////////////////////
            sensors_index_max[12] = 100;
            sensors_index_max[13] = 100;
            sensors_index_max[14] = 100;
            sensors_index_max[15] = 100;


            sensors_index_min[12] = 0;
            sensors_index_min[13] = 0;
            sensors_index_min[14] = 0;
            sensors_index_min[15] = 0;

            ////////////////////////////////////////////////////////

            sensors_index_max[16] = 70;
            sensors_index_max[17] = 70;
            sensors_index_max[18] = 70;
            sensors_index_max[19] = 70;


            sensors_index_min[16] = -50;
            sensors_index_min[17] = -50;
            sensors_index_min[18] = -50;
            sensors_index_min[19] = -50;

            ////////////////////////////////////////////////////////


            sensors_index_max[20] = 360;
            sensors_index_max[21] = 360;
            sensors_index_max[22] = 360;
            sensors_index_max[23] = 360;


            sensors_index_min[20] = 0;
            sensors_index_min[21] = 0;
            sensors_index_min[22] = 0;
            sensors_index_min[23] = 0;

            ////////////////////////////////////////////////////////
            sensors_index_max[56] = 400;
            sensors_index_max[57] = 400;
            sensors_index_max[58] = 400;
            sensors_index_max[59] = 400;


            sensors_index_min[56] = 0;
            sensors_index_min[57] = 0;
            sensors_index_min[58] = 0;
            sensors_index_min[59] = 0;

            ////////////////////////////////////////////////////////











        }

        private void pnl_show_digital_rain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label117_Click(object sender, EventArgs e)
        {


         //   cnt_random_data++; 

            
            
            
            
            chart3.Series.Clear();





            var series = chart3.Series.Add("Time-value");//("Time-value");

            chart3.Series[0].IsXValueIndexed = true;

            var chartarea = chart3.ChartAreas[series.ChartArea];
          //////////  chartarea.AxisX.ScaleView.Zoom(1, 10);

            //chartarea.AxisX.ScaleView.SizeType =DateTimein

            // Charta
          ///////////  chartarea.CursorX.IsUserSelectionEnabled = true;

            chartarea.AxisX.Minimum = 1;
            chartarea.AxisX.Maximum = 10;


            chartarea.AxisY.Minimum = 700;
            chartarea.AxisY.Maximum = 800;


            chart3.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            ///////////

            ////Random random = new Random();



            ////float x = 0, y = 0;
            ////x = cnt_random_data;

            ////chart3.Series["Time-value"].Points.AddXY(x, random.Next(700, 800));

           // chart3.Series["Time-value"].Points.AddXY(x, y);






        }

        private void tmr_show_random_data_Tick(object sender, EventArgs e)
        {

            //label117_Click(null, null);
            button3_Click_1(null, null);

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {

                cnt_random_data = 1;
                label117_Click(null, null);
                Application.DoEvents();
                tmr_show_random_data.Enabled = true;
                
                tabcontrol2.SelectedTab = tabPage14;

            }
            else
            {
                tmr_show_random_data.Enabled = false;
                tabcontrol2.SelectedTab = tabPage7;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

           

            Random random = new Random();



            float x = 0, y = 0;
            x = cnt_random_data;
          //  cnt_random_data++;

            if (cnt_random_data >= 10)
            {

                chart3.Series[0].IsXValueIndexed = true;
                cnt_random_data = 1;
                x = cnt_random_data;
                // label117_Click(null, null);
               // chart3.Series.Clear();

             chart3.Series[0].Points.Clear();

         //  chart3.Series.RemoveAt(3);

            //    chart3.Series[0].Points.Remove(x);

               //// int r = chart3.Series[0].Points.Count();
               ////// chart3.Series[0].Points.Remove(2);
               //// chart3.Series[0].Points.RemoveAt(1);
               //// ////chart3.Series[0].Points.ResumeUpdates();
               //// ////chart3.Series[0].Points.RemoveAt(2);
               //// ////chart3.Series[0].Points.ResumeUpdates();
               //// ////chart3.Series[0].Points.RemoveAt(3);
               //// ////chart3.Series[0].Points.ResumeUpdates();
               //// cnt_random_data = 0;
               //// return;
               //// chart3.Series[0].Points.RemoveAt(4);
               //// chart3.Series[0].Points.ResumeUpdates();
               //// chart3.Series[0].Points.RemoveAt(5);
               //// chart3.Series[0].Points.ResumeUpdates();
               //// return;
               //// chart3.Series[0].Points.RemoveAt(6);
               //// chart3.Series[0].Points.RemoveAt(7);
               //// chart3.Series[0].Points.RemoveAt(8);
               //// chart3.Series[0].Points.RemoveAt(9);
               //// chart3.Series[0].Points.RemoveAt(10);


                //for (int i = 0; i < 10; i++)
                //{
                //    chart3.Series[0].Points.RemoveAt(i);
                //}
              //  chart3.Series[0].Points.RemoveAt(0);
            // chart3.Series[0].Points.Remove();


                
                
              //  return; 
            }


            int alpha = random.Next(700, 800);
            chart3.Series["Time-value"].Points.AddXY(x, alpha);
            lbl_value_graph.Text =alpha.ToString();
            cnt_random_data++;

        }



        
        
        
        private void label119_Click(object sender, EventArgs e)
        {


            dg_show_onlinedata.Visible = true;
            dg_show_onlinedata_newformat.Visible = false;


            Application.DoEvents();


            dg_show_onlinedata.Height = dg_show_onlinedata.Height;

            dg_show_onlinedata.Rows.Clear();


            int rowIndex = 0, current_row, cnt = 0;
            var row = this.dg_show_onlinedata.Rows[rowIndex];
            /////////////////////////////////////////////////

            string old_day = "";
            string day_now = "";
            string old_hour = "";
            string set_time_hour = "";
            string set_time_min = "";

            string set_time_hour_pm_am = "";
            string hour_select_user = "";
            string min_select_user = "";


            int start_grid = -1;

            int start_grid_min =-1;

            set_time_hour = cmb_hour_set.Text;
            set_time_min = cmb_min_set.Text;


            int idx_global = cmb_hour_set.SelectedIndex;
            // cmb_hour_set.SelectedIndex = idx_global;



            if (set_time_hour.Equals("") == true && pnl_set_hour.Visible == true && set_time_min.Equals("") == true)
            {
                MessageBox.Show("لطفا ساعت و دقیقه را انتخاب نمایید");
            }

            old_day = "";
            day_now = "";


            string day_diffrent_id = "";
            ////////////////////findings  diffrent days///////////////////////////

             idx_global = cmb_hour_set.SelectedIndex;

            for (int h = 0; h < dg_show_onlinedata_newformat.RowCount - 1; h++)
            {

                string tmp_hour = "";
                ////date
                string Shamsi = dg_show_onlinedata_newformat.Rows[h].Cells[1].Value.ToString();



                string[] Shamsi_seprate_to = Shamsi.Split(new char[] { '/' });

                day_now = Shamsi_seprate_to[2];
                ////





                if (day_now.Equals(old_day) == true)
                {
                    old_day = day_now;
                    continue;

                }
                else
                {

                    old_day = day_now;
                    day_diffrent_id = day_diffrent_id + "-" + h.ToString();


                }




            }


            day_diffrent_id = day_diffrent_id + "-" + (dg_show_onlinedata_newformat.RowCount - 1).ToString();

            ////////////////////////////////////////////////

            cnt = 0;

            string[] day_diffrent_id_arry= day_diffrent_id.Split ('-');

            int count_scope=day_diffrent_id_arry.Length ;


            for (int i = 1; i <= count_scope - 1; i++)
            {

                
              cmb_hour_set.SelectedIndex = idx_global;
              int  first_bound = int.Parse (day_diffrent_id_arry[i].ToString());
              int last_bounf = 0;

              if (i != count_scope - 1)
              {
                  last_bounf = int.Parse(day_diffrent_id_arry[i + 1].ToString());
              }
              else
              {
                  return;
              }



                                  while (true)
                                  {

                                      for (int k = first_bound; k < last_bounf - 1; k++)
                                      {




                                          set_time_hour = cmb_hour_set.Text;

                                          set_time_min = cmb_min_set.Text;

                                          int val_set_time_hour = int.Parse(set_time_hour.ToString());

                                          int val_set_time_min = int.Parse(set_time_min.ToString());

                                          if (val_set_time_hour > 12)
                                          {
                                              val_set_time_hour = val_set_time_hour - 12;
                                              set_time_hour_pm_am = "pm";

                                          }
                                          else
                                          {
                                              set_time_hour_pm_am = "am";
                                          }



                                          ////val_set_time_hour = int.Parse(set_time_hour.ToString());

                                          ////val_set_time_min = int.Parse(set_time_min.ToString());




                                          string tmp_hour = "";
                                          tmp_hour = dg_show_onlinedata_newformat.Rows[k].Cells[2].Value.ToString();

                                          string[] ptimes = tmp_hour.Split(new char[] { ':' });


                                          string hour_now = ptimes[0];
                                          string min_now = ptimes[1];


                                          string pm_am = ptimes[2];



                                          if (val_set_time_hour < 10)
                                          {
                                              hour_select_user = "0" + val_set_time_hour.ToString();
                                          }
                                          else
                                          {
                                              hour_select_user = val_set_time_hour.ToString();
                                          }



                                          if (val_set_time_min < 10)
                                          {
                                              min_select_user = "0" + val_set_time_min.ToString();
                                          }
                                          else
                                          {
                                              min_select_user = val_set_time_min.ToString();
                                          }

                                          ////date



                                          ////


                                          if (pm_am.Contains("P") == true)
                                          {
                                              pm_am = "pm";
                                          }
                                          else
                                          {
                                              pm_am = "am";

                                          }


                                          if (hour_select_user.Trim().Equals(hour_now.Trim()) == true && set_time_hour_pm_am.Equals(pm_am) == true)
                                          {
                                              old_hour = hour_now;
                                              start_grid = k;


                                              for (int t = k; t < last_bounf - 1; t++)
                                              {


                                                  string tmp_hour_ = "";
                                                  tmp_hour_ = dg_show_onlinedata_newformat.Rows[t].Cells[2].Value.ToString();

                                                  string[] ptimes_ = tmp_hour_.Split(new char[] { ':' });


                                                  string hour_now_ = ptimes_[0];
                                                  string min_now_ = ptimes_[1];
                                                  string pm_am_ = ptimes[2];

                                                  if (pm_am_.Contains("P") == true)
                                                  {
                                                      pm_am_ = "pm";
                                                  }
                                                  else
                                                  {
                                                      pm_am_ = "am";

                                                  }

                                                  int val_min_now = int.Parse(min_now_.ToString());
                                                  int val_min_user = int.Parse(min_select_user.ToString());




                                                  if (val_min_user <= val_min_now && hour_select_user.Trim().Equals(hour_now_.Trim()) && set_time_hour_pm_am.Equals(pm_am_) == true)
                                                  {

                                                    //  start_grid_min = t;
                                                      
                                                      start_grid = t;
                                                      old_day = day_now;

                                                              /// bekhatere sorting ke dar 100 dada bar aks va avalin dade bozorgtar ast
                                                              if (rd_100.Checked == true)
                                                              {
                                                         
                                                                  continue;
                                                              }

                                                              if (rd_date.Checked == true)
                                                              {
                                                                   break;
                                                          
                                                              }

                                                     

                                                  }
                                                  //////else
                                                  //////{
                                                  //////    start_grid_min = -1;

                                                  //////}




                                              }

                                             break;

                                          }//end of if 






                                      }///end of second for 






                                      if (start_grid == -1)
                                      {

                                          int idx = cmb_hour_set.SelectedIndex;


                                          if (idx >= 23)
                                          {
                                              cmb_hour_set.SelectedIndex = 0;
                                          }
                                          
                                          else

                                          {
                                              cmb_hour_set.SelectedIndex = idx + 1;
                                          }


                                        
                                          start_grid = -1;
                                          continue;


                                      }
                                      else
                                      {

                                          cnt++;
                                          rowIndex = this.dg_show_onlinedata.Rows.Add();
                                          row = this.dg_show_onlinedata.Rows[rowIndex];
                                          current_row = rowIndex;

                                          row.Cells["index"].Value = (cnt).ToString();





                                          for (int r = 1; r <= dg_show_onlinedata_newformat.ColumnCount - 1; r++)
                                          {

                                              row.Cells[r].Value = dg_show_onlinedata_newformat.Rows[start_grid ].Cells[r].Value.ToString();


                                          }
                                          
                                          start_grid=-1;
                                          
                                          
                                          break;
                                       //  continue;
                                      }


                                  }////end of while true loop
            
            
            
            }////loop  for koli 







        }

        private void cmb_select_sensors_Click(object sender, EventArgs e)
        {


         

            lbl_draw_graph_savedata_Click(null, null);






        }

        private void lbl_draw_graph_savedata_Click(object sender, EventArgs e)
        {



         






        }

        private void cmb_select_sensors_SelectedIndexChanged(object sender, EventArgs e)
        {

            int a = cmb_select_sensors.SelectedIndex;

            if (a == -1) return;


            int id_sensors_ingrid = 9 + a;


            string flname = "c:\\2233.csv";
            string savedata = "";

            System.IO.StreamWriter filewriter = new System.IO.StreamWriter(flname);

            //adding header






            ////for (int r = 0; r < dtgshowdatapast.ColumnCount; r++)
            ////{


            ////    if (r != 1)
            ////    {
            ////        savedata = savedata + dtgshowdatapast.Columns[r].HeaderText.ToString() + ",";
            ////    }
            ////    else
            ////    {
            ////        savedata = savedata + dtgshowdatapast.Columns[r].HeaderText.ToString();
            ////    }



         ///   }


            //////////////////////find min & max////////////////////////
            float find_max = 0;
            float find_min = 0;




            find_max = float.Parse(dtgshowdatapast.Rows[1].Cells[id_sensors_ingrid].Value.ToString());
            find_min = float.Parse(dtgshowdatapast.Rows[1].Cells[id_sensors_ingrid].Value.ToString());





            int count_record = dtgshowdatapast.Rows.Count - 1;

            for (int r = 1; r < dtgshowdatapast.Rows.Count - 1; r++)
            {


                float tmp_max = float.Parse(dtgshowdatapast.Rows[r].Cells[id_sensors_ingrid].Value.ToString());
                float tmp_min = float.Parse(dtgshowdatapast.Rows[r].Cells[id_sensors_ingrid].Value.ToString());

                if (tmp_max > find_max)
                {
                    find_max = tmp_max;
                }



                if (tmp_min < find_min)
                {
                    find_min = tmp_min;
                }


            }






            find_max = find_max + 1f;
            find_min = find_min - 1f;



            filewriter.WriteLine(savedata);
            filewriter.Close();
            savedata = "";

            int row_count = dtgshowdatapast.RowCount;
            string str_row_count = row_count.ToString();

            chart3.Series.Clear();







            var series = chart3.Series.Add("Time-value");//("Time-value");

            var chartarea = chart3.ChartAreas[series.ChartArea];
             chartarea.AxisX.ScaleView.Zoom(1,100);
          //  chartarea.AxisX.ScaleView.Zoom(1, count_record);


     
            chartarea.CursorX.IsUserSelectionEnabled = true;

            chartarea.AxisX.Minimum = 0;
            //chartarea.AxisX.Maximum = 100;
            chartarea.AxisX.Maximum = count_record;


        

            int idx, comp1, comp2;
            float max = 0, min = 0, max2 = 0, min2 = 0;








            chartarea.AxisY.Minimum = find_min;
            //chartarea.AxisY2.Minimum 
            chartarea.AxisY.Maximum = find_max;

            chartarea.CursorX.AutoScroll = true;
            chartarea.AxisX.ScaleView.Zoomable = true;
            ////////////////////////////////////////////////////////////////////////
            //////chart2.Series.Clear();

            //////var series2 = chart2.Series.Add("Time-value");//("Time-value");

            //////var chartarea2 = chart2.ChartAreas[series2.ChartArea];
            //////chartarea2.AxisX.ScaleView.Zoom(1, count_record);
            ////////chartarea.AxisX.ScaleView.SizeType =DateTimein

            //////// Charta
            //////chartarea2.CursorX.IsUserSelectionEnabled = true;


            //////chartarea2.AxisX.Minimum = 0;
            //////chartarea2.AxisX.Maximum = count_record;

            //////chartarea2.AxisY.Minimum = 1;
            ////////chartarea.AxisY2.Minimum 
            //////chartarea2.AxisY.Maximum = 100;

            //////chartarea2.CursorX.AutoScroll = true;
            //////chartarea2.AxisX.ScaleView.Zoomable = true;




          

                string date = "";
                string time = "";


                for (int index =0; index < dtgshowdatapast.Rows.Count-1 ; index++)
                {


                    if (dtgshowdatapast.Rows[index].Cells[id_sensors_ingrid].Value != null)
                    {



                      

                        date = dtgshowdatapast.Rows[index].Cells[2].Value.ToString() + "/" + dtgshowdatapast.Rows[index].Cells[3].Value.ToString() + "/" + dtgshowdatapast.Rows[index].Cells[4].Value.ToString();
                        time = dtgshowdatapast.Rows[index].Cells[6].Value.ToString() + ":" + dtgshowdatapast.Rows[index].Cells[7].Value.ToString() + ":" + dtgshowdatapast.Rows[index].Cells[8].Value.ToString();

                        savedata = dtgshowdatapast.Rows[index].Cells[id_sensors_ingrid].Value.ToString() ;
                        ////}



                    }else
                    {
                    
                    continue;
                    }



                



                //  filewriter.WriteLine(savedata);



               // string[] seperate_date = savedata.Split(',');

                //string x = seperate_date[1];
                //float y = float.Parse(seperate_date[2]);

                string x =  time+"-"+ date;
                float y = float.Parse(savedata.ToString ());

             


             //   if (rd_line.Checked == true)
             //   {
                    chart3.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    //chart2.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    //chart1.Series["Time-value-2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //    }

            //    else
            //    {
             //       chart3.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                  ////////  chart2.Series["Time-value"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                  //////  // chart1.Series["Time-value-2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            //    }

                chart3.Series["Time-value"].Points.AddXY(x, y);

             




                savedata = "";



                }

          

            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            chart3.Visible = true;

       


            Application.DoEvents();
  






            Application.DoEvents();
            //   filewriter.Close ();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();





        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

          

            

        }

        private void panel2_Click(object sender, EventArgs e)
        {

            string final_val_errors = "";
            try
            {

                for (int r = 0; r < dg_show_onlinedata_newformat.Rows.Count; r++)
                {


                    double tmp_value1 = double.Parse(dg_show_onlinedata_newformat.Rows[r].Cells[3].Value.ToString());
                    double tmp_value2 = double.Parse(dg_show_onlinedata_newformat.Rows[r + 1].Cells[3].Value.ToString());

                    if (tmp_value2 < tmp_value1)
                    {
                        final_val_errors = final_val_errors + "-" + (r + 1).ToString();
                    }




                }



            }
            catch
            {
                int a = 0;
            }





            MessageBox.Show(final_val_errors.ToString());


        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {


            string final_val_errors = "";
            try
            {

                for (int r = 0; r < dtgshowdatapast.Rows.Count; r++)
                {


                    double tmp_value1 = double.Parse(dtgshowdatapast.Rows[r].Cells[13].Value.ToString());
                    double tmp_value2 = double.Parse(dtgshowdatapast.Rows[r + 1].Cells[13].Value.ToString());

                    if (tmp_value2 < tmp_value1)
                    {
                        final_val_errors = final_val_errors + "-" + (r + 1).ToString();
                    }




                }



            }
            catch
            {
                int a = 0;
            }





            MessageBox.Show(final_val_errors.ToString());



        }

       
    
    
    }






}