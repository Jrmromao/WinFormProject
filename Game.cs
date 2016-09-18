using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    [Serializable]
    public class Game
    {
      

        public Game(string GameName, string Publisher, int YearMade, string Genre,string ConsoleType, bool available)
        {
            
            this.gameName = GameName;
            this.publisher = Publisher;
            this.yearMade = YearMade;
            this.genre = Genre;
            
            this.available = true;
            this.m_ConsoleType = ConsoleType;
           

        }  
        
        public Game() {  
            Random rnd = new Random();
            m_Gameid = rnd.Next(1000000, 9999999);
         

        }

        public int m_Gameid;

      

        public string m_ConsoleType { get; set; }
        public string gameName { get; set; }
        public string publisher { get; set; }
        public int yearMade { get; set; }
        public string genre { get; set; }
       

        public bool available { get ;set ;}


    
 
    }
}
