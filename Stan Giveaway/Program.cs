using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using System.Threading;
using Discord.WebSocket;
using System.Net;
using System.IO;
using System.Security.Policy;
using System.Runtime.CompilerServices;


namespace Stan_Giveaway
{
    class Program
    {
        private static DiscordSocketClient _client;
        private static string _token;
        private static readonly object guildresult;
       


        static void Main(string[] args)
        {
            string token = "";
            Console.Title = "Stan giveaway";


            Console.WriteLine("");



            Console.ForegroundColor = ConsoleColor.Cyan;
            Colorful.Console.WriteAscii("Soos giveaway");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("________________________________________________________________________________________________________________________");

            Console.ForegroundColor = ConsoleColor.Red;

            if ((Properties.Settings.Default.Token == "null") || (Properties.Settings.Default.Token == ""))
            {
                Console.Write("Bot Token: ");
                Console.ForegroundColor = ConsoleColor.Green;
                token = Console.ReadLine();
                Properties.Settings.Default.Token = _token;
                Properties.Settings.Default.Save();

            }
            else
            {
                token = Properties.Settings.Default.Token;
            }
            _token = token;

            Console.Clear();

            _client = new DiscordSocketClient();


            _client.LoginAsync(TokenType.Bot, token);
            _client.Ready += ReadyAsync;
            _client.Log += _client_Log;
            _client.StartAsync();



            Thread.Sleep(-1);
        }

        private static Task _client_Log(LogMessage arg)
        {
            Log(arg.Message, ConsoleColor.Red);
            return Task.CompletedTask;
        }
        private static void Log(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Wait...");

        }
        public async static Task<Task> ReadyAsync()
        {

            Random rand = new Random();

            Console.Clear();

            Console.Title = $"Giveaway bot | {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}";





            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("___________________________________________________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[1] Fake giveaway");
            Console.WriteLine("[2] Real giveaway");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("____________________________________________________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">");
            string number = Console.ReadLine();


            if (number == "2")
            {


                Console.Write("Channel ID : ");
                string guildid = Console.ReadLine();
                Console.Write("Winner name : ");
                string winername = Console.ReadLine();
                Console.Write("Giveaway price : ");
                string giveawayprice = Console.ReadLine();
                Console.Write("Time (in ms) : ");
                string time = Console.ReadLine();

                int testtime = Convert.ToInt32(time);

                


                EmbedBuilder builder = new EmbedBuilder();

                builder.WithTitle("🎁 Giveaway 🎉");
                builder.AddField("Price : ", giveawayprice, true); 
                builder.AddField("Wait for : ", testtime / 1000 + "s" + " | " + testtime / 60000 + "m+", true);
                builder.WithColor(Color.Green);

                ulong id = Convert.ToUInt64(guildid);
                var chnl = _client.GetChannel(id) as IMessageChannel; 
                var fakegive = await chnl.SendMessageAsync("", false, builder.Build());

                var heartEmoji = new Emoji("\U0001f381");

                await fakegive.AddReactionAsync(heartEmoji);


                Console.WriteLine("Must open this program for " + time + " MS");

                Thread.Sleep(testtime);

                EmbedBuilder builder1 = new EmbedBuilder();

                builder1.WithTitle("🎁 Congrats 🎉");
                builder1.AddField("Winner : ", winername, true);
                builder1.AddField("Price : ", giveawayprice, true);
                builder1.WithColor(Color.Red);


                var miam = await chnl.SendMessageAsync("", false, builder1.Build());

                
            }

            if (number == "1")
            {
                Console.Write("Channel ID : ");
                string chanid = Console.ReadLine();
                ulong converting = Convert.ToUInt64(chanid);
                Console.Write("Winner Price : ");
                string winprice = Console.ReadLine();
                Console.Write("Start in : (in ms) : ");
                string timeid = Console.ReadLine();
                int timeotime = Convert.ToInt32(timeid);

                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("🎁 Giveaway 🎉");
                builder.AddField("Price : ", winprice, true);
                builder.AddField("Wait for : ", timeotime / 1000 + "s" + " | " + timeotime / 60000 + "m+", true);
                builder.WithColor(Color.Green);

                ulong ida = Convert.ToUInt64(chanid);
                var chanl = _client.GetChannel(converting) as IMessageChannel;
                var msgsendo = await chanl.SendMessageAsync("", false, builder.Build());


                var heartEmoji = new Emoji("\U0001f381");



                await msgsendo.AddReactionAsync(heartEmoji);



                Thread.Sleep(timeotime);

                IReadOnlyCollection<IUser> temp = await msgsendo.GetReactionUsersAsync("🎁");

                IUser winner = temp.ElementAt(rand.Next(temp.Count));

                EmbedBuilder builder1 = new EmbedBuilder();

                builder1.WithTitle("🎁 Congrats 🎉");
                builder1.AddField("Winner : ",  winner.Mention, true);
                builder1.AddField("Price : ", winprice, true);
                builder1.WithColor(Color.Red);


                await chanl.SendMessageAsync("", false, builder1.Build());


            }

            return Task.CompletedTask;



        }
    }
}
