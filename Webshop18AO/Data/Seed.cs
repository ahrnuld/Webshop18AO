using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop18AO.Models;

namespace Webshop18AO.Data
{
    public class Seed
    {
        public static void SeedData(ApplicationDbContext context)
        {
            if (context.Category.Count() > 0)
                return;

            Category catRts = new Category { Name = "Real time strategy" };
            Category catRpg = new Category { Name = "Role playing game" };
            Category catAction = new Category { Name = "Action" };
            Category catSimulation = new Category { Name = "Simulation" };

            context.AddRange(catRts, catRpg, catAction, catSimulation);

            Product p1 = new Product
            {
                Category = catRts,
                Name = "Wargame - European Escalation",
                Price = 7.5,
                ImageUrl = "https://www.mobygames.com/images/covers/l/269696-wargame-european-escalation-windows-front-cover.jpg",
                Description = "Wargame: European Escalation is a real-time strategy game set in an alternate history in which World War III was triggered in 1975. The campaign consists of loosely linked missions which tell the course of war. The player controlled war party changes multiple times during the campaign."
            };

            Product p2 = new Product
            {
                Category = catRts,
                Name = "Wargame - Airland Battle",
                Price = 9.95,
                ImageUrl = "https://vignette.wikia.nocookie.net/wargameeuropeanescalation/images/f/f2/WargameALB_Boxart_Europe.jpg/revision/latest?cb=20130207231204",
                Description = "Wargame: AirLand Battle is a real-time strategy Game set in Europe during the Cold War, most specifically in the years 1975-1985. It is the sequel to Wargame: European Escalation. Playable factions are the Warsaw Pact and NATO. Players can choose various units from the subfactions of the side they are playing on, unlocking new units or improved variants as they progress. In all, there are just over 800 historical units recreated to varying degrees of accuracy."
            };

            Product p3 = new Product
            {
                Category = catRts,
                Name = "Wargame - Red Dragon",
                Price = 14.95,
                ImageUrl = "https://www.mobygames.com/images/covers/l/285336-wargame-red-dragon-windows-front-cover.jpg",
                Description = "Wargame: Red Dragon is a real-time strategy game set in Asia during the Cold War, most specifically in the years 1979-1991. It is the sequel to Wargame: AirLand Battle. Players command the military resources of all 17 nations involved. There are over 1,450 units to choose from. During battles the player gets to command tanks, planes, helicopters, warships and amphibious units. It comes with a new, dynamic campaign system and also offers an extensive multiplayer mode where up to 20 players can compete against each other simultaneously."
            };

            context.AddRange(p1, p2, p3);


            context.SaveChanges();
        }
    }
}
