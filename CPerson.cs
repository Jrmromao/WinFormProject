using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    [Serializable]
    public class CPerson
    {
        public CPerson()
        {
            m_firstname = "";
            m_surname = "";
            m_address = "";
            m_phonenumber = "";
        }
        public CPerson(string firstname, string surname, string address, string phonenumber)
        {
            m_firstname = firstname;
            m_surname = surname;
            m_address = address;
            m_phonenumber = phonenumber;
        }

        public string m_firstname { get; set; }
        public string m_surname { get; set; }
        public string m_address { get; set; }
        public string m_phonenumber { get; set; }

    }
}
