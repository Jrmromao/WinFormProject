using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Project
{
    [Serializable]
    public partial class Login : Form


    {
        public static List<CGeneralStaff> DeserializeStaffFile()
        {

            try
            {

                List<CGeneralStaff> StaffList = new List<CGeneralStaff>();
                XmlSerializer mySerializer = new XmlSerializer(typeof(List<CGeneralStaff>));
                // To read the file, create a FileStream.
                FileStream myFileStream = new FileStream("StaffList.xml", FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                StaffList = (List<CGeneralStaff>)
                mySerializer.Deserialize(myFileStream);

                myFileStream.Close();


                return StaffList;
            }
            catch (SerializationException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        public static List<CManager> DeserializeManagerFile()
        {
            try
            {

                List<CManager> ManagerList = new List<CManager>();
                XmlSerializer mySerializer = new XmlSerializer(typeof(List<CManager>));
                // To read the file, create a FileStream.
                FileStream myFileStream = new FileStream("ManagerList.xml", FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                ManagerList = (List<CManager>)
                mySerializer.Deserialize(myFileStream);

                myFileStream.Close();

                return ManagerList;
            }
            catch (SerializationException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }


        string username = "Mark";
        string ManagerUserName = "Manager";
        int ManaferPassword = 1234;
        int password = 1234;

        public Login()
        {
            InitializeComponent();
          
        }


       private void verifyLogin()
          {
            List<CGeneralStaff> StaffList = DeserializeStaffFile();
            List<CManager> ManagerList = Login.DeserializeManagerFile();

            foreach (CGeneralStaff staffMember in StaffList)
            {

                    if (UserName.Text.ToString() == staffMember.m_username && PassWord.Text.ToString() == staffMember.m_password.ToString())
                {

                    //stream.Close();
                    MessageBox.Show("Welcome " + staffMember.m_firstname + " " + staffMember.m_surname);

                    this.Hide();
                    CustumerForm newForm = new CustumerForm();
                    // Form1 newForm = new Form1();
                    newForm.ShowDialog();
                    this.Close();
                }
                 
            }
            foreach (CManager Manager in ManagerList)
            {

                if (UserName.Text.ToString() == Manager.m_username && PassWord.Text.ToString() == Manager.m_password.ToString())
                {

                    //stream.Close();
                    MessageBox.Show("Welcome " + Manager.m_firstname + " " + Manager.m_surname);

                    this.Hide();
                    ManagerDashboard newForm = new ManagerDashboard();
                    // Form1 newForm = new Form1();
                    newForm.ShowDialog();
                    this.Close();
                }

            }


            }




        

        private void PassWord_TextChanged(object sender, EventArgs e)
         {
            
         }

        private void PassWord_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                verifyLogin();
            }
        }
    }


   





}
