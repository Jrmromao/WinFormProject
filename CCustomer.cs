using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Project
          
{
    [Serializable]
    public class CCustomer : CPerson
    {
        //

        public CCustomer()
        {
            Random rnd = new Random();

            m_firstname = "";
            m_surname = "";
            m_address = "";
            m_emailaddress = "";
            m_phonenumber = "";
            m_fees = 0;
            m_membershipfees = 0;
            m_overduefees = 0;
            m_hasGame = false;
            m_gameName = "";
            _id = rnd.Next(1000000,9999999);

        }

 
        public int _id;


        public CCustomer(string firstname, string surname, string address, string phonenumber, double fees, double memberfees, double overduefees, int accountNumber, string emailAddress )
            : base(firstname, surname, address, phonenumber)
        {
            m_fees = fees;
            m_membershipfees = memberfees;
            m_overduefees = overduefees;
            m_emailaddress = emailAddress;

        }
        
        

        public string m_emailaddress { get; set; }
        public double m_fees { get; set; }
        public double m_overduefees { get; set; }
        public double m_membershipfees { get; set; }
        public bool m_hasGame { get; set; }

    

        public string m_gameName { get; set; }
        public static int m_accountNumber { get; set; }

  
       
        
    }
}
