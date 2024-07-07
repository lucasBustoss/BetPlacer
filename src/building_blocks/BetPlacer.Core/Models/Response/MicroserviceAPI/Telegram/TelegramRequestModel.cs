using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Telegram
{
    public class TelegramRequestModel
    {
        public TelegramRequestModel(int type, List<string> objects, Dictionary<string, string> markets)
        {
            Type = type;
            Objects = objects;
            Markets = markets;
        }

        public int Type { get; set; }
        public List<string> Objects { get; set; }
        public Dictionary<string, string> Markets { get; set; }
    }
}
