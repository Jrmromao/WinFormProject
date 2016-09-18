using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class CTransaction
    {
        public CTransaction()
        {
            m_rentalfee = 0;
            m_overduefee = 0;
            m_rentdate = DateTime.Now;
            m_returndate = DateTime.MinValue;
            m_transactionComplete = false;
            m_custid = 0;
            m_gameid = 0;
        }
        public CTransaction(int custid, int gameid)
        {
            m_rentalfee = 3;
            m_overduefee = 0;
            m_rentdate = DateTime.Now;
            m_returndate = DateTime.MinValue;
            m_transactionComplete = false;
            m_custid = custid;
            m_gameid = gameid;
            Random rnd = new Random();
            transaction_id = rnd.Next(1000000, 9999999);
           
        }



        //public CTransaction(CCustomer customer, Game game)
        //{
        //    m_rentalfee = 0;
        //    m_overduefee = 0;
        //    m_rentdate = DateTime.Now;
        //    m_returndate = DateTime.MinValue;
        //    m_customer = customer;
        //    m_gamerented = game;
        //    m_transactionComplete = false;
        //}


        public int transaction_id;

        public int m_custid { get; set; }
        public int m_gameid { get; set; }
        public double m_rentalfee { get; set; }
        public double m_overduefee { get; set; }
        public DateTime m_rentdate { get; set; }
        public DateTime m_returndate { get; set; }
        public bool m_transactionComplete { get; set; }

        public void returngame()
        {
            m_returndate = DateTime.Now;
            int daysrented = (m_rentdate.Date - m_returndate.Date).Days;
            if (daysrented > 3)
            {
                m_overduefee = daysrented - 3;
            }
            m_transactionComplete = true;
        }
    }
}
