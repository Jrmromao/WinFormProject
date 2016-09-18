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
    public class CGeneralStaff : CEmployee
    {
        
        public CGeneralStaff()
        {
            Random rnd = new Random();
            m_firstname = "";
            m_surname = "";
            m_username = "";
            m_password = "";
            m_address = "";
            m_phonenumber = "";
            m_salarie = 0;

            Staff_id = rnd.Next(1000000, 9999999);
            //Staff_id = CGeneralStaff.nrOfInstances;
            //CGeneralStaff.nrOfInstances++;

        }

        
        public int Staff_id;
        public CGeneralStaff(string firstname, string surname, string username, string password, string address, string phonenumber, double salarie, DateTime hiredate)
            : base(firstname, surname, username, password, address, phonenumber,  salarie, hiredate)
        {
        }


    }
}
