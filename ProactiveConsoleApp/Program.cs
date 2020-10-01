using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProactiveConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sending Pro-active Message...");

            var serviceURL = "https://smba.trafficmanager.net/emea/";

            var conversationId = "a:1MPKSjwg3FaLsf-4m_Oo4AuHa86HnkgM_ElnhEnOKRHTAJtgx48vMvtpvHLJiBxzQ44uKKBPvD9IG8Zso4Xtt5fFmGYnJ" + File.ReadAllText(@"c:\temp\conversationid.txt");

            var uri = new Uri(serviceURL);
            var appId = "0dcfc815-2b88-4e33-91e5-8947a2e00b22";
            var appSecret = File.ReadAllText(@"c:\temp\secret.txt");
            ConnectorClient connector = new ConnectorClient(uri, appId, appSecret);

            // This line is SUPER important
            MicrosoftAppCredentials.TrustServiceUrl(serviceURL);

            var activity = new Activity()
            {
                Type = ActivityTypes.Message,
                Text = "Test proactive message"
            };

            connector.Conversations.SendToConversationAsync(conversationId, activity).Wait();
            
            Console.WriteLine("Done");
            Console.WriteLine("");

            Console.WriteLine("Getting conversation members...");

            string groupConversationId = "19:4b5ba" + File.ReadAllText(@"c:\temp\conversationid2.txt") + "@thread.skype";
            IList<ChannelAccount> teamMembers = connector.Conversations.GetConversationMembersAsync(groupConversationId).Result;

            foreach (var item in teamMembers)
            {
                var teamsUser = JsonConvert.DeserializeObject<MicrosoftTeamUser>(item.Properties.ToString());

                Console.WriteLine($"{teamsUser.GivenName} {teamsUser.Surname} ({teamsUser.Email})");
            }
        }
    }

    public class MicrosoftTeamUser
    {
        public string Id { get; set; }
        public string ObjectId { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserPrincipalName { get; set; }
        public string TenantId { get; set; }
    }
}
