using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project;
using System.Threading.Tasks;

namespace Project
{
    public class CManager : CEmployee
    {
         public CManager()
        {
            Random rnd = new Random();
            m_firstname = "";
            m_surname = "";
            m_username="";
            m_password = "";
            m_address = "";
            m_phonenumber = "";
            
            m_salarie = 0;
            Manager_id = rnd.Next(1000000, 9999999);
            //Manager_id = CManager.nrOfInstances;
            //CManager.nrOfInstances++;

        }

  

        

         public int Manager_id;
        public CManager(string firstname,string surname,string username,string password,string address,string phonenumber,double salarie,DateTime hiredate)
        :base(firstname,surname,username,password,address,phonenumber,salarie,hiredate)
        {
        }
    }
}
