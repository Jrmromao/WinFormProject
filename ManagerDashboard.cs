using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Project
{

    [Serializable]
    public partial class ManagerDashboard : Form
    {

        List<CGeneralStaff> StaffList = Login.DeserializeStaffFile();
        List<CManager> ManagerList = Login.DeserializeManagerFile();
        List<Game> GamesLibrary = CustumerForm.DeserializeGames();
        List<CCustomer> CustomerList = CustumerForm.DeserializeCustomer();
        

        public ManagerDashboard()
        {
            InitializeComponent();
            StaffCombo.Items.Insert(0, "Staff Type");
            StaffCombo.SelectedIndex = 0;
            
        }

        public bool addStaffMember()
        {

            Project.CGeneralStaff StaffNew = new Project.CGeneralStaff();
            StaffNew.m_firstname = txtfirstNameStaff.Text;
            StaffNew.m_surname = SurnameStaff.Text;
            StaffNew.m_password = PasswordStaff.Text;
            StaffNew.m_username = UserNameStaff.Text;
            StaffNew.m_address = AddressStaff.Text;
            StaffNew.m_phonenumber = PhoneNumberStaff.Text;
            StaffNew.m_salarie = Convert.ToDouble(SalaryStaff.Text);
            StaffNew.m_datehired = DateTime.Now;

            StaffList.Add(StaffNew);

            return true;

        }



        private static bool WriteToStaffFile(List<CGeneralStaff> StaffList)
        {
            try
            {

                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(StaffList.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, StaffList);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save("StaffList.xml");
                    stream.Close();


                    return true;

                }
    
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
          

        }

        private void SubmitStaff_KeyDown(object sender, KeyEventArgs e)
        {
            if (addStaffMember() && WriteToStaffFile(StaffList))
            {
                MessageBox.Show("Staff Member Added");
               
                WriteToStaffFile(StaffList);
            }
        }

        private void ManagerLogOut_Click(object sender, EventArgs e)
        {
      
            this.Hide();
            Login newLog = new Login();
            newLog.ShowDialog();
            this.Close();
        }

        private void SubmitStaff_Click(object sender, EventArgs e)
        {

            pnlstaff.Visible = true;
            pnlStaffFname.Text = txtfirstNameStaff.Text;
            pnlStaffSName.Text = SurnameStaff.Text;
            pnlStaffUsername.Text = UserNameStaff.Text;
            pnlStaffPassword.Text = PasswordStaff.Text;
            pnlStaffadd.Text = AddressStaff.Text;
            pnlStaffphone.Text = PhoneNumberStaff.Text;
            pnlStaffSalary.Text = SalaryStaff.Text;
            bool valpass = true;
            string errorMessage = "";
            int parsenumber;
            //firstname check
            if (txtfirstNameStaff.Text.Any(char.IsDigit))
            {
                valpass = false;
                errorMessage = "First Name contains numerical values";
            }
            //check surname
            if (SurnameStaff.Text.Any(char.IsDigit))
            {
                valpass = false;
                errorMessage = " Surname contains numerical values";
            }
            //validate email
            foreach (CGeneralStaff staff in StaffList)
            {
                if (staff.m_username == UserNameStaff.Text)
                {
                    valpass = false;
                    errorMessage = "Username is already in use";
                }

            }
            //validate phone number
            if (!int.TryParse(PhoneNumberStaff.Text, out parsenumber))
            {
                valpass = false;
                errorMessage = "Phone number can only contain numeric values";
            }

            if (!int.TryParse(SalaryStaff.Text, out parsenumber))
            {
                valpass = false;
                errorMessage = "Salary can only contain numeric values";

            }

             DialogResult dialogResult = MessageBox.Show("Please confirm details are correct", "Confirmation", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                      

                        if (valpass == true)
                                    {
                                        if (addStaffMember() && WriteToStaffFile(StaffList))
                                        {
                                            MessageBox.Show("Staff Member Added");
                                            WriteToStaffFile(StaffList);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(errorMessage);
                                    }

                        
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        pnlstaff.Visible = false;
                        MessageBox.Show("Staff member not added");
                    }

            

     

        }

        private void StaffCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( StaffCombo.SelectedIndex == 1)
            {
                gneralStaffPanel.Visible = true;
                ManagerPanel.Visible = false;
                gneralStaffPanel.Location = new Point(95, 48);
            
        
            }
            else if (StaffCombo.SelectedIndex == 2)
            {
                
                ManagerPanel.Visible = true;
                gneralStaffPanel.Visible = false;
                ManagerPanel.Location = new Point(95, 48);
            }
        }



        public bool addManager()
        {         
            Project.CManager mgrNew = new Project.CManager();
            mgrNew.m_firstname = mgrFirstName.Text;

            mgrNew.m_surname = mgrSurname.Text;
            mgrNew.m_password = mgrPassword.Text;
            mgrNew.m_username = mgrUserName.Text;
            mgrNew.m_address = mgrAddress.Text;
            mgrNew.m_phonenumber = mgrPhoneNumber.Text;
            mgrNew.m_salarie = Convert.ToDouble(mgrSalarie.Text);
            mgrNew.m_datehired = DateTime.Now;
            ManagerList.Add(mgrNew);
            return true;
        }

        private static bool WriteToManagerFile(List<CManager> ManagerList)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(ManagerList.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, ManagerList);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save("ManagerList.xml");
                    stream.Close();
                    return true;
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
        }

      

        private void mgrSubmit_Click(object sender, EventArgs e)
        {

            pnlstaff.Visible = true;
            pnlStaffFname.Text = mgrFirstName.Text;
            pnlStaffSName.Text = mgrSurname.Text;
            pnlStaffUsername.Text = mgrUserName.Text;
            pnlStaffPassword.Text = mgrPassword.Text;
            pnlStaffadd.Text = mgrAddress.Text;
            pnlStaffphone.Text = mgrPhoneNumber.Text;
            pnlStaffSalary.Text = mgrSalarie.Text;
            bool valpass = true;
            string errorMessage = "";
            int parsenumber;
            //firstname check
            if (mgrFirstName.Text.Any(char.IsDigit))
            {
                    valpass = false;
                    errorMessage = "First Name contains numerical values";
            }
            //check surname
            if (mgrSurname.Text.Any(char.IsDigit))
            {
                    valpass = false;
                    errorMessage = " Surname contains numerical values";
            }
            //validate email
            foreach (CManager Manager in ManagerList)
            {

                if (Manager.m_username == mgrUserName.Text)
               {
                     valpass = false;
                     errorMessage = "Username is already in use";
               }
              
           }
            //validate phone number
            if (!int.TryParse(mgrPhoneNumber.Text, out parsenumber))
           {
                      valpass = false;
                      errorMessage = "Phone number can only contain numeric values";
           }

           if (!int.TryParse(mgrSalarie.Text, out parsenumber))
           {
               valpass = false;
               errorMessage = "Salary can only contain numeric values";

           }



           DialogResult dialogResult = MessageBox.Show("Please confirm details are correct", "Confirmation", MessageBoxButtons.YesNo);

           if (dialogResult == DialogResult.Yes)
           {


               if (valpass == true)
               {
                   if (addManager() && WriteToManagerFile(ManagerList))
                   {
                       MessageBox.Show("Manager Added");
                       WriteToManagerFile(ManagerList);
                       pnlstaff.Visible = false;
                   }
                   else
                   {
                       MessageBox.Show(errorMessage);
                   }
               }
               else
               {
                   MessageBox.Show(errorMessage);
               }


           }
           else if (dialogResult == DialogResult.No)
           {
               pnlstaff.Visible = false;
               MessageBox.Show("Manager not added");
           }


         

        }

        private void ViewStaff_Click(object sender, EventArgs e)
        {
            StaffGridView.Rows.Clear();
            foreach (CGeneralStaff staff in StaffList)
            {  
                StaffGridView.Visible = true;
                EditStaff.Visible = true;
                StaffGridView.Rows.Add(staff.m_firstname, staff.m_surname, staff.m_username, staff.m_password, staff.m_address, staff.m_phonenumber, staff.m_salarie, staff.Staff_id, staff.m_datehired);
            } 
            foreach (CManager managers in ManagerList)
                {

                    StaffGridView.Rows.Add(managers.m_firstname, managers.m_surname, managers.m_username, managers.m_password, managers.m_address, managers.m_phonenumber, managers.m_salarie, managers.Manager_id, managers.m_datehired);

                }
        }

        private void EditStaff_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please change the staff details in the grid and click done editing when finished");
            StaffGridView.Enabled = true;
            StaffGridView.ReadOnly = false;
            DoneEditingStaff.Visible = true;
            delStaff.Visible = true;
        }

        private void DoneEditingStaff_Click(object sender, EventArgs e)
                        {
                                foreach (CGeneralStaff staff in StaffList)
                                {

                                    if (staff.Staff_id == Convert.ToInt32(StaffGridView[7, StaffGridView.CurrentCell.RowIndex].Value))
                                    {
                                        int counter = StaffGridView.CurrentCell.RowIndex;
                                        staff.m_firstname = StaffGridView[0, counter].Value.ToString();
                                        staff.m_surname = StaffGridView[1, counter].Value.ToString();
                                        staff.m_username = StaffGridView[2, counter].Value.ToString();
                                        staff.m_password = StaffGridView[3, counter].Value.ToString();
                                        staff.m_address = StaffGridView[4, counter].Value.ToString();
                                        staff.m_phonenumber = StaffGridView[5, counter].Value.ToString();
                                        staff.m_salarie = Convert.ToDouble(StaffGridView[6, counter].Value);
                                        WriteToStaffFile(StaffList);
                                        MessageBox.Show("Staff details updated");
                                        
                                    }
                                    
                                }
                           
                                foreach (CManager manager in ManagerList)
                                {

                                    if (manager.Manager_id ==Convert.ToInt32(StaffGridView[7, StaffGridView.CurrentCell.RowIndex].Value))
                                    {
                                        int counter = StaffGridView.CurrentCell.RowIndex;
                                        manager.m_firstname = StaffGridView[0, counter].Value.ToString();
                                        manager.m_surname = StaffGridView[1, counter].Value.ToString();
                                        manager.m_username = StaffGridView[2, counter].Value.ToString();
                                        manager.m_password = StaffGridView[3, counter].Value.ToString();
                                        manager.m_address = StaffGridView[4, counter].Value.ToString();
                                        manager.m_phonenumber = StaffGridView[5, counter].Value.ToString();
                                        manager.m_salarie = Convert.ToDouble(StaffGridView[6, counter].Value);
                                        WriteToManagerFile(ManagerList);
                                        MessageBox.Show("Manager details updated");
                                        
                                    }
                                }
      
                    }

    

        private void delStaff_Click_1(object sender, EventArgs e)
        {
            for (int i = StaffList.Count - 1; i >= 0; i--)
            {
                if (StaffList[i].Staff_id == Convert.ToInt32(StaffGridView[7, StaffGridView.CurrentCell.RowIndex].Value))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this staff member", "Confirmation", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        StaffList.RemoveAt(i);
                        WriteToStaffFile(StaffList);
                        MessageBox.Show("Staff member deleted");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Staff member not deleted");
                    }

                }
            }
            for (int i = ManagerList.Count - 1; i >= 0; i--)
            {
                if (ManagerList[i].Manager_id.ToString() == StaffGridView[7, StaffGridView.CurrentCell.RowIndex].Value.ToString())
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this Manager", "Confirmation", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        ManagerList.RemoveAt(i);
                        WriteToManagerFile(ManagerList);
                        MessageBox.Show("Manager deleted");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Manager not deleted");
                    }

                }
            }
        }

        private void viewGamesManager_Click(object sender, EventArgs e)
        {
            GamesViewManager.Rows.Clear();
            foreach (Game game in GamesLibrary)
            {

                GamesViewManager.Visible = true;
                editGamesManager.Visible = true;
                GamesViewManager.Rows.Add(game.gameName, game.publisher, game.yearMade, game.genre, game.m_Gameid, game.m_ConsoleType);
            }
        }

        private void editGamesManager_Click(object sender, EventArgs e)
        {
            mgrDoneEditing.Visible = true;
            delGameManager.Visible = true;
            GamesViewManager.Enabled = true;
            GamesViewManager.ReadOnly = false;
        }

        private void mgrDoneEditing_Click(object sender, EventArgs e)
        {
            foreach (Game game in GamesLibrary)
            {

                if (game.m_Gameid == Convert.ToInt32(GamesViewManager[4, GamesViewManager.CurrentCell.RowIndex].Value))
                {
                    int counter = GamesViewManager.CurrentCell.RowIndex;

                    game.gameName = GamesViewManager[0, counter].Value.ToString();
                    game.publisher = GamesViewManager[1, counter].Value.ToString();
                    game.yearMade = Convert.ToInt32(GamesViewManager[2, counter].Value);
                    game.genre = GamesViewManager[3, counter].Value.ToString();

                    game.m_ConsoleType = GamesViewManager[5, counter].Value.ToString();
                    CustumerForm.WriteGamesToFile(GamesLibrary);

                    MessageBox.Show("Game details updated");
                    GamesViewManager.Rows.Clear();


                }
            }

        }

        private void delGameManager_Click(object sender, EventArgs e)
        {
            for (int i = GamesLibrary.Count - 1; i >= 0; i--)
            {
                if (GamesLibrary[i].m_Gameid == Convert.ToInt32(GamesViewManager[4, GamesViewManager.CurrentCell.RowIndex].Value))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " + GamesLibrary[i].gameName, "Confirmation", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        GamesLibrary.RemoveAt(i);
                        CustumerForm.WriteGamesToFile(GamesLibrary);
                        MessageBox.Show("Game deleted");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Game not deleted");
                    }

                }
            }
        }

        private void ViewAllCustomersmgr_Click(object sender, EventArgs e)
        {
            mgrCustomerView.Rows.Clear();
            foreach (CCustomer customer in CustomerList)
            {

                mgrCustomerView.Visible = true;

                EditCustomer.Visible = true;

                //CustomerGridView.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address, customer.m_phonenumber, customer.m_fees, customer.m_overduefees, customer.m_gameName);
                if (customer.m_hasGame == true)
                {
                    mgrCustomerView.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address, customer._id, customer.m_phonenumber, customer.m_emailaddress, customer.m_fees, customer.m_overduefees, customer.m_gameName);
                }
                else
                {
                    mgrCustomerView.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address, customer._id, customer.m_phonenumber, customer.m_emailaddress, customer.m_fees, customer.m_overduefees, "No Games Rented");
                }

            }

        }

        private void EditCustomer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please change the customer details in the grid and click done editing when finished");
            mgrCustomerView.Enabled = true;
            mgrCustomerView.ReadOnly = false;
            mgrDeleteCustomer.Visible = true;
            DoneEditingmgr.Visible = true;
        }

        private void DoneEditingmgr_Click(object sender, EventArgs e)
        {
            foreach (CCustomer customer in CustomerList)
            {

                if (customer._id == Convert.ToInt32(mgrCustomerView[3, mgrCustomerView.CurrentCell.RowIndex].Value))
                {
                    int counter = mgrCustomerView.CurrentCell.RowIndex;

                    customer.m_firstname = mgrCustomerView[0, counter].Value.ToString();
                    customer.m_surname = mgrCustomerView[1, counter].Value.ToString();
                    customer.m_address = mgrCustomerView[2, counter].Value.ToString();
                    customer._id = Convert.ToInt32(mgrCustomerView[3, counter].Value);
                    customer.m_phonenumber = mgrCustomerView[4, counter].Value.ToString();
                    customer.m_emailaddress = mgrCustomerView[5, counter].Value.ToString();
                    CustumerForm.WriteToFile(CustomerList);

                    MessageBox.Show("Customer details updated");


                }
            }


        }

        private void btnPayFees_Click(object sender, EventArgs e)
        {
            txt_Amount.Visible = true;
            lbl_Amount.Visible = true;
        }

        private void txt_Amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double amount;
                foreach (CCustomer customer in CustomerList)
                {

                    if (customer._id == Convert.ToInt32(mgrCustomerView[3, mgrCustomerView.CurrentCell.RowIndex].Value))
                    {
                 
                        if (double.TryParse(txt_Amount.Text, out amount))
                        {
                            customer.m_fees -= amount;
                            MessageBox.Show(txt_Amount.Text + " Euro has been payed ");
                        }
                        else
                        {
                            MessageBox.Show("Please enter numerical value");
                        }
                    }
                }

            }
        }

        private void mgrDeleteCustomer_Click(object sender, EventArgs e)
        {
            for (int i = CustomerList.Count - 1; i >= 0; i--)
            {
                if (CustomerList[i]._id == Convert.ToInt32(mgrCustomerView[3, mgrCustomerView.CurrentCell.RowIndex].Value))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " + CustomerList[i].m_firstname + " " + CustomerList[i].m_surname +"\n\tfrom the database", "Confirmation", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        CustomerList.RemoveAt(i);
                        CustumerForm.WriteToFile(CustomerList);
                        MessageBox.Show("Customer deleted");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Customer not deleted"); 
                        
                    }

                }
            
               
            }
        }

        private void pnlstaff_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bt_viewOverdueGAme_Click(object sender, EventArgs e)
        {


            grid_overdueGame.Rows.Clear();
            foreach (CCustomer customerO in CustomerList)
            {

                if ((customerO.m_hasGame) && (customerO.m_overduefees > 0))
                {
                    grid_overdueGame.Visible = true;
                    grid_overdueGame.Visible = true;
                    grid_overdueGame.Rows.Add(customerO.m_firstname,customerO._id, customerO.m_gameName);


                }

            }
           

        }

    

     


        

    }
}
