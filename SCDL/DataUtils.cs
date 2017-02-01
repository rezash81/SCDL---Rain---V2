using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.OleDb;
using System.IO;
using System.Globalization;
using System.Reflection;




namespace SCDL
{
    class DataUtils
    {

        public static Boolean  conn_state_error=false,disconn_state_error = false;
        public static NpgsqlConnection conn=new NpgsqlConnection();

    //    public static string adrress_ip = "46.165.242.234", port_ = "5432", user_ = "postgres", pass_ = "postgres", database_ = "uasco";
        public static string adrress_ip = "", port_ = "", user_ = "", pass_ = "", database_ = "";

        public static Boolean disconnect_db()
        {


            if ((conn.State.ToString().Equals("Open")))
            {
                conn.Close();
                disconn_state_error = true;
            
            }


            return disconn_state_error;
        }





        public static Boolean connect_db()
         {

       
           //if(!conn_state )
           if (!( conn.State.ToString().Equals("Open")))

           {

               try
               {
                 //  "select * from sensors order where is_active = '" + is_active + "' by channel_index ";

                  

                   conn = new NpgsqlConnection("Server=" + adrress_ip + "; Port=" + port_ + "; User Id=" + user_ + "; Password=" + pass_ + "; Database=" + database_ + ""); //<ip> is an actual ip address

               //    conn = new NpgsqlConnection("Server=46.165.242.234; Port=5432; User Id=postgres; Password=postgres; Database=uasco"); //<ip> is an actual ip address
                   
                   conn.Open();
                   Application.DoEvents();
                   conn_state_error = true;


               }
             
               
                      catch (Exception ex)
                {
                    // throw ex;
                      

                    conn_state_error = false; 
                }
              


                   
                   Application.DoEvents();

                   

                           

           }

           return conn_state_error;

           

}


    }
}
