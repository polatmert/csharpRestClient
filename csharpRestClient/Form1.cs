using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace csharpRestClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region UI Event Handlers

        private void cmdGO_Click(object sender, EventArgs e)
        {

            //example Url: http://dry-cliffs-19849.herokuapp.com/users.json

            RestClient rClient = new RestClient();
            rClient.endPoint = txtRequestUrl.Text;
           // rClient.authTech = authenticationTechnique.RollYourOwn;
            rClient.authType = authenticationType.Basic;
            rClient.userName = txtUserName.Text;
            rClient.Password = txtPassword.Text;

            string strResponse = string.Empty;
            strResponse = rClient.makeRequest();
            debugOutput(strResponse);
        }

        #endregion

        #region debug Function
        private void debugOutput(string strDebugText)
        {
            try
            {
                System.Diagnostics.Debug.Write(strDebugText + Environment.NewLine);
                txtResponse.Text = txtResponse.Text + strDebugText + Environment.NewLine;
                txtResponse.SelectionStart = txtResponse.TextLength;
                txtResponse.ScrollToCaret();
            }

            catch(Exception e) 
            {
                txtResponse.Text = e.Message.ToString();
            }          
        }
        #endregion

     
    }
}
