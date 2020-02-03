using System;
using System.Collections.Generic;

namespace ClassGetMS.Models
{
    public class RCFMessage
    {
        public DateTime? Date;
        public string TypeJour;

        public RCFMessage()
        { 

        }

        public RCFMessage(DateTime date, string typeJour)
        {
            this.Date = date;
            this.TypeJour = typeJour;
        }
    }

    public class RefreshPlanningMessage
    {

    }

    public class TransfertPaieMessage
    {
        public Dictionary<string, FonctionCalcul> listeFonctions;
        public Dictionary<string, FonctionCalculDates> listeFonctionsDates;
        public List<Agent> listAgents;
        public List<LigneTransfert> lignesCatTransfert;
        public MinutesCentiemes minsCents;
    }
}
