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
    public class CEmployee : CPerson
    {

        public CEmployee()
        {
            m_firstname = "";
            m_surname = "";
            m_username = "";
            m_password = "";
            m_address = "";
            m_phonenumber = "";
            m_salarie = 0;
        }

        public CEmployee(string firstname, string surname, string username, string password, string address, string phonenumber, double salarie, DateTime hiredate)
            : base(firstname, surname, address, phonenumber)
        {
         
            m_salarie = salarie;
            m_datehired = hiredate;
        }


        
  
        public DateTime m_datehired { get; set; }
        public double m_salarie { get; set; }
        public string m_username { get; set; }
        public string m_password { get; set; }


    }
}
