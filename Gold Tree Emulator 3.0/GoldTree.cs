using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using GoldTree.Core;
using GoldTree.HabboHotel;
using GoldTree.Net;
using GoldTree.Storage;
using GoldTree.Util;
using GoldTree.Communication;
using GoldTree.Messages;
using System.Net;
namespace GoldTree
{
    internal sealed class GoldTree
    {
        public static readonly int build = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;
        public const string string_0 = "localhost";
        private static PacketManager class117_0;
        private static ConfigurationData Configuration;
        private static DatabaseManager DatabaseManager;
        private static SocketsManager ConnectionManage;
        private static MusListener MusListener;
        private static Game Game;
        internal static DateTime ServerStarted;
        public string string_2 = GoldTree.smethod_1(14986.ToString());
        public string string_3 = "LICENCE DELETED" + "licence" + Convert.ToChar(46).ToString() + "php" + Convert.ToChar(63).ToString();
        public static string string_4 = "LICENCE DELETED" + "override" + Convert.ToChar(46).ToString() + "php";
        public static bool bool_0 = false;
        public static int int_1 = 0;
        public static int int_2 = 0;
        public static string string_5 = null;
        public static string string_6;
        public static string string_7;
        private static bool bool_1 = false;

        public static string PrettyVersion
        {
            get
            {
                return "Gold Tree Emulator v3.18.0 (Build " + build + ")";
            }
        }
        internal static Game Class3_0
        {
            get
            {
                return GoldTree.Game;
            }
            set
            {
                GoldTree.Game = value;
            }
        }
        public static string smethod_0(string string_8)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] array = Encoding.UTF8.GetBytes(string_8);
            array = mD5CryptoServiceProvider.ComputeHash(array);
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                byte b = array2[i];
                stringBuilder.Append(b.ToString("x2").ToLower());
            }
            string text = stringBuilder.ToString();
            return text.ToUpper();
        }
        public static string smethod_1(string string_8)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(string_8);
            byte[] array = new SHA1Managed().ComputeHash(bytes);
            string text = string.Empty;
            byte[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                byte b = array2[i];
                text += b.ToString("X2");
            }
            return text;
        }
        public void Initialize()
        {   
            if (!Licence.smethod_0(true))
            {
                GoldTree.ServerStarted = DateTime.Now;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                //Console.WriteLine("        ______  _                       _          _______             "); //Based to phoenix
                //Console.WriteLine("       (_____ \\| |                     (_)        (_______)            "); //Based to phoenix
                //Console.WriteLine("        _____) ) | _   ___   ____ ____  _ _   _    _____   ____  _   _ "); //Based to phoenix
                //Console.WriteLine("       |  ____/| || \\ / _ \\ / _  )  _ \\| ( \\ / )  |  ___) |    \\| | | |"); //Based to phoenix
                //Console.WriteLine("       | |     | | | | |_| ( (/ /| | | | |) X (   | |_____| | | | |_| |"); //Based to phoenix
                //Console.WriteLine("       |_|     |_| |_|\\___/ \\____)_| |_|_(_/ \\_)  |_______)_|_|_|\\____|"); //Based to phoenix
                Console.WriteLine("                      _______   _________   ______ ");
                Console.WriteLine("                     |  _____| |___   ___| | _____|");
                Console.WriteLine("                     | |  ___      | |     | |____");
                Console.WriteLine("                     | | |_  |     | |     |  ____|");
                Console.WriteLine("                     | |___| |     | |     | |____");
                Console.WriteLine("                     |_______|     |_|     |______|");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("                  " + PrettyVersion);
                Console.WriteLine();
                Console.ResetColor();

                try
                {
                    GoldTree.Configuration = new ConfigurationData("config.conf");
                    DateTime now = DateTime.Now;
                    string_6 = GetConfig().data["GTE.username"];
                    string_7 = GetConfig().data["GTE.password"];
                    //Lookds = new Random().Next(Int32.MaxValue).ToString();
                    int num = string_6.Length * string_7.Length;

                    if (string_6 == "" || string_7 == "" || LicenseTools.Boolean_7)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        GoldTree.Destroy("Invalid Licence details found #0001", false);
                    }
                    else
                    {
                        LicenseTools.String_6 = GoldTree.string_6;
                        LicenseTools.String_3 = GoldTree.string_7;
                        string text = new Random().Next(Int32.MaxValue).ToString();
                        text = Licence.smethod_1(text, this.string_3);

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        string str = new Random().Next(Int32.MaxValue).ToString();//text.Substring(32, 32);
                        str = GoldTree.smethod_0(str + GoldTree.string_6);
                        str = GoldTree.smethod_0(str + "4g");
                        str = GoldTree.smethod_1(str + GoldTree.string_7);
                        string b = GoldTree.smethod_0(num.ToString());

                        DatabaseServer Message3_ = new DatabaseServer(GoldTree.GetConfig().data["db.hostname"], uint.Parse(GoldTree.GetConfig().data["db.port"]), GoldTree.GetConfig().data["db.username"], GoldTree.GetConfig().data["db.password"]);
                        text = "r4r43mfgp3kkkr3mgprekw[gktp6ijhy[h]5h76ju6j7uj7";//text.Substring(64, 96);
                        Database Message2_ = new Database(GoldTree.GetConfig().data["db.name"], uint.Parse(GoldTree.GetConfig().data["db.pool.minsize"]), uint.Parse(GoldTree.GetConfig().data["db.pool.maxsize"]));
                        GoldTree.DatabaseManager = new DatabaseManager(Message3_, Message2_);

                        try
                        {
                            using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
                            {
                                @class.ExecuteQuery("UPDATE users SET online = '0'");
                                @class.ExecuteQuery("UPDATE rooms SET users_now = '0'");
                            }
                            GoldTree.ConnectionManage.method_7();
                            GoldTree.Game.ContinueLoading();
                        }
                        catch
                        {
                        }

                        LicenseTools.String_1 = text;
                        GoldTree.Game = new Game(int.Parse(GoldTree.GetConfig().data["game.tcp.conlimit"]));
                        string text2 = LicenseTools.String_5 + GoldTree.smethod_0((LicenseTools.String_6.Length * 10).ToString());
                        text2 += GoldTree.smethod_0((LicenseTools.String_3.Length % 10).ToString());

                        GoldTree.class117_0 = new PacketManager();
                        GoldTree.class117_0.Handshake();
                        GoldTree.class117_0.Messenger();
                        GoldTree.class117_0.Navigator();
                        GoldTree.class117_0.RoomsAction();
                        GoldTree.class117_0.RoomsAvatar();
                        GoldTree.class117_0.RoomsChat();
                        GoldTree.class117_0.RoomsEngine();
                        GoldTree.class117_0.RoomsFurniture();
                        GoldTree.class117_0.RoomsPets();
                        GoldTree.class117_0.RoomsPools();
                        GoldTree.class117_0.RoomsSession();
                        GoldTree.class117_0.RoomsSettings();
                        GoldTree.class117_0.Catalog();
                        GoldTree.class117_0.Marketplace();
                        GoldTree.class117_0.Recycler();
                        GoldTree.class117_0.Quest();
                        GoldTree.class117_0.InventoryAchievements();
                        GoldTree.class117_0.InventoryAvatarFX();
                        GoldTree.class117_0.InventoryBadges();
                        GoldTree.class117_0.InventoryFurni();
                        GoldTree.class117_0.InventoryPurse();
                        GoldTree.class117_0.InventoryTrading();
                        GoldTree.class117_0.Avatar();
                        GoldTree.class117_0.Users();
                        GoldTree.class117_0.Register();
                        GoldTree.class117_0.Help();
                        GoldTree.class117_0.Sound();
                        GoldTree.class117_0.Wired();
                        GoldTree.class117_0.Jukebox();
                    }

                    LicenseTools.int_12 = int.Parse(GoldTree.GetConfig().data["game.tcp.port"]);
                    LicenseTools.int_13 = int.Parse(GoldTree.GetConfig().data["mus.tcp.port"]);
                    try
                    {
                        LicenseTools.ProxyIP = GetConfig().data["game.tcp.proxyip"];
                    }
                    catch
                    {
                    }

                    GoldTree.MusListener = new MusListener(GoldTree.GetConfig().data["mus.tcp.bindip"], LicenseTools.int_13, GoldTree.GetConfig().data["mus.tcp.allowedaddr"].Split(new char[] { ';' }), 20);
                    GoldTree.ConnectionManage = new SocketsManager(LicenseTools.string_33, LicenseTools.int_12, int.Parse(GoldTree.GetConfig().data["game.tcp.conlimit"]));
                    GoldTree.ConnectionManage.method_3().method_0();

                    /*try
                    {
                        if (int.Parse(GoldTree.GetConfig().data["automatic-error-report"]) < 1 || int.Parse(GoldTree.GetConfig().data["automatic-error-report"]) > 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Logging.WriteLine("Erroreita ei raportoida automaattisesti!!!");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (int.Parse(GoldTree.GetConfig().data["automatic-error-report"]) == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Logging.WriteLine("Kaikki errorit reportoidaan automaattisesti");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (int.Parse(GoldTree.GetConfig().data["automatic-error-report"]) > 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Logging.WriteLine("Vain kritikaaliset virheiden reportoidaan automaattisesti");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Logging.WriteLine("Erroreita ei raportoida automaattisesti!!!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }*/

                    TimeSpan timeSpan = DateTime.Now - now;
                    Logging.WriteLine(string.Concat(new object[]
					{
						"Server -> READY! (",
						timeSpan.Seconds,
						" s, ",
						timeSpan.Milliseconds,
						" ms)"
					}));
                    Console.Beep();
                }
                catch (KeyNotFoundException KeyNotFoundException)
                {
                    Logging.WriteLine("Failed to boot, key not found: " + KeyNotFoundException);
                    Logging.WriteLine("Press any key to shut down ...");
                    Console.ReadKey(true);
                    GoldTree.smethod_16();
                }
                catch (InvalidOperationException ex)
                {
                    Logging.WriteLine("Failed to initialize GoldTreeEmulator: " + ex.Message);
                    Logging.WriteLine("Press any key to shut down ...");
                    Console.ReadKey(true);
                    GoldTree.smethod_16();
                }
            }
        }
        public static int smethod_2(string string_8)
        {
            return Convert.ToInt32(string_8);
        }
        public static bool smethod_3(string string_8)
        {
            return string_8 == "1";
        }
        public static string smethod_4(bool bool_2)
        {
            if (bool_2)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        public static int smethod_5(int int_3, int int_4)
        {
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] array = new byte[4];
            rNGCryptoServiceProvider.GetBytes(array);
            int seed = BitConverter.ToInt32(array, 0);
            return new Random(seed).Next(int_3, int_4 + 1);
        }
        public static double GetUnixTimestamp()
        {
            return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }
        public static string FilterString(string str)
        {
            return DoFilter(str, false, false);
        }
        public static string DoFilter(string Input, bool bool_2, bool bool_3)
        {
            Input = Input.Replace(Convert.ToChar(1), ' ');
            Input = Input.Replace(Convert.ToChar(2), ' ');
            Input = Input.Replace(Convert.ToChar(9), ' ');
            if (!bool_2)
            {
                Input = Input.Replace(Convert.ToChar(13), ' ');
            }
            if (bool_3)
            {
                Input = Input.Replace('\'', ' ');
            }
            return Input;
        }
        public static bool smethod_9(string string_8)
        {
            if (string.IsNullOrEmpty(string_8))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < string_8.Length; i++)
                {
                    if (!char.IsLetter(string_8[i]) && !char.IsNumber(string_8[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public static PacketManager smethod_10()
        {
            return GoldTree.class117_0;
        }
        public static ConfigurationData GetConfig()
        {
            return Configuration;
        }
        public static DatabaseManager GetDatabase()
        {
            return DatabaseManager;
        }
        public static Encoding GetDefaultEncoding()
        {
            return Encoding.Default;
        }
        public static SocketsManager smethod_14()
        {
            return GoldTree.ConnectionManage;
        }
        internal static Game GetGame()
        {
            return Game;
        }
        public static void smethod_16()
        {
            Logging.WriteLine("Destroying GoldTreeEmu environment...");
            if (GoldTree.GetGame() != null)
            {
                GoldTree.GetGame().ContinueLoading();
                GoldTree.Game = null;
            }
            if (GoldTree.smethod_14() != null)
            {
                Logging.WriteLine("Destroying connection manager.");
                GoldTree.smethod_14().method_3().method_2();
                GoldTree.smethod_14().method_0();
                GoldTree.ConnectionManage = null;
            }
            if (GoldTree.GetDatabase() != null)
            {
                try
                {
                    Logging.WriteLine("Destroying database manager.");
                    MySqlConnection.ClearAllPools();
                    GoldTree.DatabaseManager = null;
                }
                catch
                {
                }
            }
            Logging.WriteLine("Uninitialized successfully. Closing.");
        }
        internal static void smethod_17(string string_8)
        {
            try
            {
                ServerMessage Message = new ServerMessage(139u);
                Message.AppendStringWithBreak(string_8);
                GoldTree.GetGame().GetClientManager().method_14(Message);
            }
            catch
            {
            }
        }
        internal static void smethod_18()
        {
            GoldTree.Destroy("", true);
        }
        internal static void Destroy(string string_8, bool ExitWhenDone)
        {
            LicenseTools.bool_16 = true;
            try
            {
                if (smethod_10() != null)
                {
                    GoldTree.smethod_10().Clear();
                }
            }
            catch
            {
            }
            if (string_8 != "")
            {
                if (GoldTree.bool_1)
                {
                    return;
                }
                Console.WriteLine(string_8);
                Logging.smethod_7();
                GoldTree.smethod_17("ATTENTION:\r\nThe server is shutting down. All furniture placed in rooms/traded/bought after this message is on your own responsibillity.");
                GoldTree.bool_1 = true;
                Console.WriteLine("Server shutting down...");
                try
                {
                    GoldTree.Game.GetRoomManager().method_4();
                }
                catch
                {
                }
                try
                {
                    GoldTree.smethod_14().method_3().method_1();
                    GoldTree.GetGame().GetClientManager().CloseAll();
                }
                catch
                {
                }
                try
                {
                    Console.WriteLine("Destroying database manager.");
                    MySqlConnection.ClearAllPools();
                    GoldTree.DatabaseManager = null;
                }
                catch
                {
                }
                Console.WriteLine("System disposed, goodbye!");
            }
            else
            {
                Logging.smethod_7();
                GoldTree.bool_1 = true;
                try
                {
                    if (GoldTree.Game != null && GoldTree.Game.GetRoomManager() == null)
                    {
                        GoldTree.Game.GetRoomManager().method_4();
                    }
                }
                catch
                {
                }
                try
                {
                    if (GoldTree.smethod_14() != null)
                    {
                        GoldTree.smethod_14().method_3().method_1();
                        GoldTree.GetGame().GetClientManager().CloseAll();
                    }
                }
                catch
                {
                }
                if (ConnectionManage != null)
                {
                    GoldTree.ConnectionManage.method_7();
                }
                if (GoldTree.Game != null)
                {
                    GoldTree.Game.ContinueLoading();
                }
                Console.WriteLine(string_8);
            }
            if (ExitWhenDone)
            {
                Environment.Exit(0);
            }
        }
        public static bool smethod_20(int int_3, int int_4)
        {
            return int_3 % int_4 == 0;
        }
        public static DateTime smethod_21(double double_0)
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return result.AddSeconds(double_0).ToLocalTime();
        }
    }
}
