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
    public partial class CustumerForm : Form
    {
        

        List<CCustomer> CustomerList = DeserializeCustomer();
        List<Game> GamesLibrary = DeserializeGames();
        List<CTransaction> TransactionList = DeserializeTransactions();

        public static List<CCustomer> DeserializeCustomer()
        {

            try
            {

                List<CCustomer> CustomerList = new List<CCustomer>();
                XmlSerializer mySerializer = new XmlSerializer(typeof(List<CCustomer>));
                FileStream myFileStream = new FileStream("CustomerListNew.xml", FileMode.Open);
                CustomerList = (List<CCustomer>)
                mySerializer.Deserialize(myFileStream);

                myFileStream.Close();
                return CustomerList;
            }
            catch (SerializationException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        public static List<Game> DeserializeGames()
        {
            try
            {

                List<Game> GamesList = new List<Game>();
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                XmlSerializer mySerializer = new XmlSerializer(typeof(List<Game>));
                // To read the file, create a FileStream.
                FileStream myFileStream = new FileStream("GamesLibrary.xml", FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                GamesList = (List<Game>)
                mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return GamesList;
            }
            catch (SerializationException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }

           

        }


        public static List<CTransaction> DeserializeTransactions()
        {
            try
            {

                List<CTransaction> TransactionList = new List<CTransaction>();
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                XmlSerializer mySerializer = new XmlSerializer(typeof(List<CTransaction>));
                // To read the file, create a FileStream.
                FileStream myFileStream = new FileStream("Transactions.xml", FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                TransactionList = (List<CTransaction>)
                mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return TransactionList;
            }
            catch (SerializationException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }



        }



        public CustumerForm()
        {
            InitializeComponent();
        }


        public bool addCustomer()
        {
            pnlCust.Visible = true;
            Project.CCustomer CustNew = new Project.CCustomer();
            CustNew.m_firstname = textBox1_FName.Text;
            CustNew.m_surname = textBox1_surname.Text;
            CustNew.m_address = textBox1_address.Text;
            CustNew.m_phonenumber = PhoneNumber.Text;
            CustNew.m_membershipfees = Convert.ToDouble(txtMemberFee.Text);
            pnlFirstName.Text = textBox1_FName.Text;
            pnlSurName.Text = textBox1_surname.Text;
            pnlAddress.Text = textBox1_address.Text;
            pnlPhone.Text = PhoneNumber.Text;
            pnlJoin.Text = txtMemberFee.Text;
            pnlEmail.Text = txtEmailAddress.Text;
           
            
            DialogResult dialogResult = MessageBox.Show("Confirm customer details are correct", "Confirmation", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                 CustomerList.Add(CustNew);
                    pnlCust.Visible = false;
            }
            else if (dialogResult == DialogResult.No)
            {
                pnlCust.Visible = false;
                return false;
            }
           

            //CustFile.Close();

            return true;

        }
        public void count()
        {
            for (int i = 0; i < CustomerList.Count; i++)
            {
                MessageBox.Show("count");
            }
        }



        private void Submit_Click(object sender, EventArgs e)
        {
            bool valpass = true;
            string errorMessage = "";
            int parsenumber;
            //firstname check
            if (textBox1_FName.Text.Any(char.IsDigit))
            {
                valpass = false;
                errorMessage = "First Name contains numerical values";
            }
            if (textBox1_surname.Text.Any(char.IsDigit))
            {
                valpass = false;
                errorMessage = " Surname contains numerical values";
            }

            if (!int.TryParse(PhoneNumber.Text, out parsenumber))
            {
                valpass = false;
                errorMessage = "Phone number can only contain numeric values";
            }
            if (!int.TryParse(txtMemberFee.Text, out parsenumber))
            {
                valpass = false;
                errorMessage = "Member Fee Can only contain numberic values";
            }
        
            foreach (CCustomer cust in CustomerList)
            {
                if (cust.m_emailaddress == txtEmailAddress.Text)
                {
                    valpass = false;
                    errorMessage = "Email is already in use";
                }
            }


            if (valpass == true)
            {
                if (addCustomer() && WriteToFile(CustomerList))
                {
                    MessageBox.Show("Successful");
                    WriteToFile(CustomerList);
                }
            }
            else
            {
                MessageBox.Show(errorMessage);
            }

        }

        //private void Submit_Click(object sender, EventArgs e)
        //{
        //    if (addCustomer() && WriteToFile(CustomerList))
        //    {
        //        MessageBox.Show("Successful");
        //        WriteToFile(CustomerList);
        //    }

        //}


        public static bool WriteToFile(List<CCustomer> CustomerList)
        {
     
            try
            {
                using (TextWriter writer = new StreamWriter("CustomerListNew.xml"))
                {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<CCustomer>));                 
                            List<CCustomer> po = new List<CCustomer>();
                    serializer.Serialize(writer, CustomerList);
                    writer.Close();
                    
                }
                return true;
            }
            catch (SerializationException)
            {
                MessageBox.Show("Cannot create file");
                return false;
            }

           
        }

        public static bool WriteGamesToFile(List<Game> GamesLibrary)
        {

            try
            {

                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(GamesLibrary.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, GamesLibrary);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save("GamesLibrary.xml");
                    stream.Close();
                    return true;
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot create file");
                return false;
            }
         
        }

        private static bool WriteTransactionToFile(List<CTransaction> TransactionList)
        {

            try
            {

                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(TransactionList.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, TransactionList);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save("Transactions.xml");
                    stream.Close();
                    return true;
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot create file");
                return false;
            }

        }






        public bool addGame()
        {
            pnlConfirm.Visible = true;
            Project.Game NewGame = new Project.Game();
            NewGame.gameName = textBox4_GameName.Text;
            NewGame.publisher = textBox4_Publisher.Text;
            NewGame.yearMade = Convert.ToInt16(textBox5_YearMade.Text);
            NewGame.genre = textBox5_Genre.Text;
            NewGame.m_ConsoleType = txtConsoleType.Text;
            NewGame.available = true;
            pnlGameName.Text = textBox4_GameName.Text;
            pnlGamePublisher.Text = textBox4_Publisher.Text;
            pnlGameYear.Text = textBox5_YearMade.Text;
            pnlGenre.Text= textBox5_Genre.Text;
            pnlConsoleType.Text = txtConsoleType.Text;
            NewGame.available = true;
            DialogResult dialogResult = MessageBox.Show("Select yes to add game to library", "Confirmation", MessageBoxButtons.YesNo);
           
            if (dialogResult == DialogResult.Yes)
            {
                GamesLibrary.Add(NewGame);
                pnlConfirm.Visible = false;
            }
            else if (dialogResult == DialogResult.No)
            {
                pnlConfirm.Visible = false;
                return false;
            }
            
            
            return true;

        }



        private void txtAccountSearch_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                bool hasemail = false;
                foreach (CCustomer customer in CustomerList)
                {
                    if (txtAccountSearch.Text == customer.m_emailaddress)
                    {
                        hasemail = true;
                        CustomerGridView.Rows.Clear();
                        CustomerGridView.Visible = true;

                        CustomerGridView.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address,customer._id, customer.m_phonenumber, customer.m_emailaddress, customer.m_fees, customer.m_overduefees, customer.m_gameName);
                        btnPayFees.Visible = true;
                        EditCustomer.Visible = true;
                    }
                }
                if (hasemail == false)
                {
                    MessageBox.Show("Email address not found");
                }
            }
        }

        private void button1_AddGame_Click_1(object sender, EventArgs e)
        {

            bool valpass = false;
            int year;
            if (int.TryParse(textBox5_YearMade.Text, out year))
            {
                if (year > 999 && year < 10000)
                {
                    valpass = true;
                }
            }
            if (valpass == true)
            {
                if (addGame() && WriteGamesToFile(GamesLibrary))
                {
                    MessageBox.Show("Game added");
                    WriteGamesToFile(GamesLibrary);
                }
            }
            else
            {
                MessageBox.Show("Invalid input for year");
            }
        }

        private void textBox_GameName_KeyDown(object sender, KeyEventArgs e)
        {
            ViewAllGames.Visible = true;
            EditGame.Visible = true;
            if (e.KeyCode == Keys.Enter)
            {
                GamesView.Rows.Clear();
                int count = 0;
                bool gamefound = false;
                foreach (Game game in GamesLibrary)
                {
                    if (textBox_GameName.Text == game.gameName)
                    {
                        gamefound = true;
                        count++;
                        GamesView.Visible = true;
                        NoAvailabletxt.Visible = true;
                        GamesView.Rows.Add(game.gameName, game.publisher, game.yearMade, game.genre,game.m_Gameid, game.m_ConsoleType);
                        gamesAvailable.Text = count.ToString();
                    }
                }
                if (gamefound == false)
                {
                    MessageBox.Show("Game not found.");
                }

            }
        }

  

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            //StaffFile.Close();
            Login newLog = new Login();

            newLog.ShowDialog();

            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void RentGameName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool gamefound = false;
                foreach (Game game in GamesLibrary)
                {
                    int count = 0;
                    if (RentGameName.Text == game.gameName && game.available == true)
                    {
                        gamefound = true;
                        count++;
                        lblNoAvailable.Visible = true;
                        rentGameButton.Visible = true;

                        noAvailable.Text = count.ToString();
                        rentGameView.Visible = true;
                        rentGameView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        rentGameView.Rows.Add(game.gameName, game.publisher, game.yearMade, game.genre,game.m_Gameid, game.m_ConsoleType); 

                    }
                    else if (RentGameName.Text == game.gameName && game.available == false)
                    {
                        noAvailable.Text = count.ToString();
                        MessageBox.Show("There are no copies of this game available for rent at the moment");
                        gamefound = true;
                    }
                }
                if (gamefound == false)
                {
                    MessageBox.Show("Game not found.");
                }

            }
        }

        private void rentGameButton_Click(object sender, EventArgs e)
        {
            lblCustomerEMail.Visible = true;
            txtCustomerEmail.Visible = true;


        }



        public void RefreshPage()
        {
            lblNoAvailable.Visible = false;
            RentGameName.Clear();
            rentGameButton.Visible = false;
            lblCustomerEMail.Visible = false;
            txtCustomerEmail.Visible = false;
            txtCustomerEmail.Clear();
            rentGrid.Rows.Clear();
            rentGrid.Visible = false;
            noAvailable.Text = null;
        }




        private void txtCustomerEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool hasemail = false;
                foreach (CCustomer customer in CustomerList)
                {
                    if (txtCustomerEmail.Text == customer.m_emailaddress)
                    {
                        hasemail = true;
                        rentGrid.Visible = true;
                        confirmRental.Visible = true;

                        if (customer.m_hasGame == true)
                        {
                            rentGrid.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address,customer._id, customer.m_phonenumber, customer.m_emailaddress, customer.m_gameName);
                        }
                        else
                        {
                            rentGrid.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address,customer._id, customer.m_phonenumber, customer.m_emailaddress, "No Games Rented");
                        }

                    }
                }
                if (hasemail == false)
                {
                    MessageBox.Show("Email address not found");
                }
            }

        }

        //private void confirmRental_Click(object sender, EventArgs e)
        //{

        //    foreach (Game games in GamesLibrary)
        //    {
        //        if (RentGameName.Text == games.gameName)
        //        {

        //            foreach (CCustomer cust in CustomerList)
        //            {
        //                if (txtCustomerEmail.Text == cust.m_emailaddress)
        //                {

        //                    CTransaction newTransaction = new CTransaction(cust._id, games.m_Gameid);
                            
        //                    cust.m_hasGame = true;
        //                    games.available = false;
        //                    cust.m_gameName = games.gameName;
        //                    MessageBox.Show("Transaction Confirmed");
        //                    TransactionList.Add(newTransaction);
        //                    WriteTransactionToFile(TransactionList);
        //                    RefreshPage();
        //                }
        //            }
        //            break;

        //        }
        //    }

        //}

        private void confirmRental_Click(object sender, EventArgs e)
        {

            foreach (Game games in GamesLibrary)
            {
                if (RentGameName.Text == games.gameName)
                {

                    foreach (CCustomer cust in CustomerList)
                    {
                        //if (Convert.ToInt32(txtCustomerEmail.Text) == cust.m_emailaddress)
                        if (txtCustomerEmail.Text == cust.m_emailaddress)
                        {

                            CTransaction newTransaction = new CTransaction(cust._id, games.m_Gameid);

                            cust.m_hasGame = true;
                            games.available = false;
                            cust.m_gameName = games.gameName;
                            MessageBox.Show("Transaction Confirmed");
                            TransactionList.Add(newTransaction);
                            WriteTransactionToFile(TransactionList);
                            WriteToFile(CustomerList);
                            WriteGamesToFile(GamesLibrary);
                            RefreshPage();


                        }
                    }
                    break;

                }
            }

        }

        private void ViewAllCustomers_Click(object sender, EventArgs e)
        {
            CustomerGridView.Rows.Clear();
            foreach (CCustomer customer in CustomerList)
            {

                CustomerGridView.Visible = true;
               
                EditCustomer.Visible = true;

                //CustomerGridView.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address, customer.m_phonenumber, customer.m_fees, customer.m_overduefees, customer.m_gameName);
                if (customer.m_hasGame == true)
                {
                    CustomerGridView.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address,customer._id, customer.m_phonenumber, customer.m_emailaddress, customer.m_fees, customer.m_overduefees, customer.m_gameName);
                }
                else
                {
                    CustomerGridView.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address,customer._id, customer.m_phonenumber, customer.m_emailaddress, customer.m_fees, customer.m_overduefees, "No Games Rented");
                }

            }

        }

        private void ReturnCustomerEmail__KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool hasemail = false;
                foreach (CCustomer customer in CustomerList)
                {
                    if (ReturnCustomerEmail.Text == customer.m_emailaddress)
                    {
                        hasemail = true;
                        returncustdetails.Visible = true;
                        ReturnGame.Visible = true;


                        if (customer.m_hasGame == true)
                        {
                            returncustdetails.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address, customer.m_phonenumber, customer.m_emailaddress, customer.m_gameName);

                        }
                        else
                        {
                            returncustdetails.Rows.Add(customer.m_firstname, customer.m_surname, customer.m_address, customer.m_phonenumber, customer.m_emailaddress, "No Games Rented");
                        }
                    }
                }
                if (hasemail == false)
                {
                    MessageBox.Show("Email address not found");
                }
            }
        }

        public void RefreshPagereturn()
        {
            returncustdetails.Visible = true;
            ReturnGame.Visible = true;
            ReturnCustomerEmail.Clear();
            returncustdetails.Rows.Clear();
        }
        private void ReturnGame_Click(object sender, EventArgs e)
        {
            
                foreach (CCustomer cust in CustomerList)
                    if (ReturnCustomerEmail.Text == cust.m_emailaddress)
                    {
                        foreach (Game games in GamesLibrary)
                        {
                            if (games.gameName == cust.m_gameName)
                            {
                                foreach (CTransaction Transaction in TransactionList)
                                {
                                    if (Transaction.m_custid == cust._id)
                                    {
                                    CTransaction newTransaction = new CTransaction(cust._id, games.m_Gameid);
                                    Transaction.returngame();
                                        cust.m_hasGame = false;
                                        cust.m_overduefees += Transaction.m_overduefee;
                                        cust.m_fees += Transaction.m_rentalfee;
                                        games.available = true;
                                        cust.m_gameName = "";
                                        MessageBox.Show("Game Returned");
                                    TransactionList.Add(newTransaction);
                                    WriteTransactionToFile(TransactionList);
                                    WriteToFile(CustomerList);
                                    WriteGamesToFile(GamesLibrary);
                                    RefreshPagereturn();
                                        break;
                                    }
                                }
                                
                            }
                        }
                    }
            
        }

        private void button2_Click(object sender, EventArgs e)
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

                    if (txtAccountSearch.Text == customer.m_emailaddress)
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


      
        private void CustomerGridView_AutoSizeRowsModeChanged(object sender, DataGridViewAutoSizeModeEventArgs e)
        {
            CustomerGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void EditCustomer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please change the customer details in the grid and click done editing when finished");
            CustomerGridView.Enabled = true;
            CustomerGridView.ReadOnly = false;
            DoneEditing.Visible = true;
        
            
        }

        private void DoneEditing_Click(object sender, EventArgs e)
        {
            foreach (CCustomer customer in CustomerList)
            {

                if (customer._id == Convert.ToInt32(CustomerGridView[3, CustomerGridView.CurrentCell.RowIndex].Value))
                {
                    int counter = CustomerGridView.CurrentCell.RowIndex;

                    customer.m_firstname = CustomerGridView[0, counter].Value.ToString();
                    customer.m_surname = CustomerGridView[1, counter].Value.ToString();
                    customer.m_address = CustomerGridView[2, counter].Value.ToString();
                    customer._id = Convert.ToInt32(CustomerGridView[3, counter].Value);
                    customer.m_phonenumber = CustomerGridView[4, counter].Value.ToString();
                    customer.m_emailaddress = CustomerGridView[5, counter].Value.ToString();
                    WriteToFile(CustomerList);
                    
                    MessageBox.Show("Customer details updated");
                
                    
                }
            }
        }

        private void ViewAllGames_Click(object sender, EventArgs e)
        {

            GamesView.Rows.Clear();
            foreach (Game game in GamesLibrary)
            {

                GamesView.Visible = true;

                GamesView.Rows.Add(game.gameName, game.publisher, game.yearMade, game.genre, game.m_Gameid, game.m_ConsoleType);
            }

        }

        private void EditGame_Click(object sender, EventArgs e)
        {
            DoneEditingGames.Visible = true;
            GamesView.Enabled = true;
            GamesView.ReadOnly = false;
        }

        private void DoneEditingGames_Click(object sender, EventArgs e)
        {
            foreach (Game game in GamesLibrary)
            {

                if (game.m_Gameid == Convert.ToInt32(GamesView[4, GamesView.CurrentCell.RowIndex].Value))
                {
                    int counter = GamesView.CurrentCell.RowIndex;

                    game.gameName = GamesView[0, counter].Value.ToString();
                    game.publisher = GamesView[1, counter].Value.ToString();
                    game.yearMade = Convert.ToInt32(GamesView[2, counter].Value);
                    game.genre = GamesView[3, counter].Value.ToString();

                    game.m_ConsoleType = GamesView[5, counter].Value.ToString();
                    WriteGamesToFile(GamesLibrary);

                    MessageBox.Show("Game details updated");


                }
            }
        }

       
    }
}
