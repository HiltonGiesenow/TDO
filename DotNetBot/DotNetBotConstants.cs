using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBot
{
    public class DotNetBotConstants
    {
        public const string Help = "Help";
        public const string Reset = "Reset";
        public const string Admin = "Admin";
        public const string CardTest1 = "CardTest1";
        public const string CardTest2 = "CardTest2";
    }

    public class FlightIntents
    {
        public const string BookFlight = "BookFlight";
        public const string Cancel = "Cancel";
        public const string GetWeather = "GetWeather";
        public const string None = "None";
    }

    public static class CardConstants
    {
        public const string AdaptiveCardContentType = "application/vnd.microsoft.card.adaptive";
    }

}
