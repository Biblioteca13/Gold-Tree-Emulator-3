using System;
using System.Data;
using System.Threading.Tasks;
using GoldTree.HabboHotel.Misc;
using GoldTree.Core;
using GoldTree.HabboHotel.Navigators;
using GoldTree.HabboHotel.Catalogs;
using GoldTree.HabboHotel.Support;
using GoldTree.HabboHotel.Roles;
using GoldTree.HabboHotel.GameClients;
using GoldTree.HabboHotel.Items;
using GoldTree.HabboHotel.Rooms;
using GoldTree.HabboHotel.Advertisements;
using GoldTree.HabboHotel.Achievements;
using GoldTree.HabboHotel.RoomBots;
using GoldTree.HabboHotel.Quests;
using GoldTree.Util;
using GoldTree.Storage;
namespace GoldTree.HabboHotel
{
	internal sealed class Game
	{
		private GameClientManager ClientManager;
		private ModerationBanManager BanManager;
		private RoleManager RoleManager;
		private HelpTool HelpTool;
		private Catalog Catalog;
		private Navigator Navigator;
		private ItemManager ItemManager;
		private RoomManager RoomManager;
		private AdvertisementManager AdvertisementManager;
		private PixelManager PixelManager;
		private AchievementManager AchievementManager;
		private ModerationTool ModerationTool;
		private BotManager BotManager;
		private Task task_0;
		private NavigatorCache class276_0;
		private Marketplace Marketplace;
		private QuestManager QuestManager;
		private GoldTreeEnvironment class8_0;
		private Groups Groups;
		public Game(int conns)
		{
			this.ClientManager = new GameClientManager(conns);
			if (GoldTree.GetConfig().data["client.ping.enabled"] == "1")
			{
				this.ClientManager.method_10();
			}
			DateTime arg_45_0 = DateTime.Now;
			Logging.smethod_0("Connecting to database...");
			using (DatabaseClient adapter = GoldTree.GetDatabase().GetClient())
			{
				Logging.WriteLine("completed!");
				GoldTree.Class3_0 = this;
				this.method_17(adapter);
				this.BanManager = new ModerationBanManager();
				this.RoleManager = new RoleManager();
				this.HelpTool = new HelpTool();
				this.Catalog = new Catalog();
				this.Navigator = new Navigator();
				this.ItemManager = new ItemManager();
				this.RoomManager = new RoomManager();
				this.AdvertisementManager = new AdvertisementManager();
				this.PixelManager = new PixelManager();
				this.AchievementManager = new AchievementManager();
				this.ModerationTool = new ModerationTool();
				this.BotManager = new BotManager();
				this.Marketplace = new Marketplace();
				this.QuestManager = new QuestManager();
				this.class8_0 = new GoldTreeEnvironment();
				this.Groups = new Groups();
				GoldTreeEnvironment.smethod_0(adapter);
				this.BanManager.method_0(adapter);
				LicenseTools.String_5 = "FB3A78763D7819F39D79781F6F8DFCCD";
				this.RoleManager.method_0(adapter);
				this.HelpTool.method_0(adapter);
				this.HelpTool.method_3(adapter);
				this.ModerationTool.method_1(adapter);
				this.ModerationTool.method_2(adapter);
				LicenseTools.String_5 = "B8AC48FA7DB791129E59CBA4BC2CC5DD";
				this.ItemManager.method_0(adapter);
				LicenseTools.String_5 = "7866151A40EEB2379D61F640B26ED23B";
				this.Catalog.method_0(adapter);
				this.Catalog.method_1();
				this.Navigator.method_0(adapter);
				LicenseTools.String_5 = LicenseTools.String_6;
				this.RoomManager.method_8(adapter);
				this.RoomManager.method_0();
				this.class276_0 = new NavigatorCache();
				this.AdvertisementManager.method_0(adapter);
				this.BotManager.method_0(adapter);
				LicenseTools.String_5 = LicenseTools.String_3;
				LicenseTools.String_5 = LicenseTools.String_6.Length.ToString();
				AchievementManager.smethod_0(adapter);
				this.PixelManager.method_0();
				ChatCommandHandler.smethod_0(adapter);
				LicenseTools.String_5 = LicenseTools.String_3.Length.ToString();
				this.QuestManager.method_0();
				Groups.smethod_0(adapter);
				this.method_0(adapter, 1);
			}
			this.task_0 = new Task(new Action(LowPriorityWorker.smethod_0));
			this.task_0.Start();
		}
		public void method_0(DatabaseClient class6_0, int int_0)
		{
			Logging.smethod_0(GoldTreeEnvironment.smethod_1("emu_cleandb"));
			bool flag = true;
			try
			{
				if (int.Parse(GoldTree.GetConfig().data["debug"]) == 1)
				{
					flag = false;
				}
			}
			catch
			{
			}
			if (flag)
			{
				class6_0.ExecuteQuery("UPDATE users SET online = '0' WHERE online != '0'");
				class6_0.ExecuteQuery("UPDATE rooms SET users_now = '0' WHERE users_now != '0'");
				class6_0.ExecuteQuery("UPDATE user_roomvisits SET exit_timestamp = UNIX_TIMESTAMP() WHERE exit_timestamp <= 0");
				class6_0.ExecuteQuery(string.Concat(new object[]
				{
					"UPDATE server_status SET status = '",
					int_0,
					"', users_online = '0', rooms_loaded = '0', server_ver = '",
					GoldTree.PrettyVersion,
					"', stamp = UNIX_TIMESTAMP() LIMIT 1;"
				}));
			}
			Logging.WriteLine("completed!");
		}
		public void ContinueLoading()
		{
			if (this.task_0 != null)
			{
				this.task_0 = null;
			}
			using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
			{
				this.method_0(@class, 0);
			}
			if (this.GetClientManager() != null)
			{
				this.GetClientManager().method_6();
				this.GetClientManager().method_11();
			}
			if (this.GetPixelManager() != null)
			{
				this.PixelManager.KeepAlive = false;
			}
			this.ClientManager = null;
			this.BanManager = null;
			this.RoleManager = null;
			this.HelpTool = null;
			this.Catalog = null;
			this.Navigator = null;
			this.ItemManager = null;
			this.RoomManager = null;
			this.AdvertisementManager = null;
			this.PixelManager = null;
		}
		public GameClientManager GetClientManager()
		{
			return this.ClientManager;
		}
		public ModerationBanManager GetBanManager()
		{
			return this.BanManager;
		}
		public RoleManager GetRoleManager()
		{
			return this.RoleManager;
		}
		public HelpTool GetHelpTool()
		{
			return this.HelpTool;
		}
		public Catalog GetCatalog()
		{
			return this.Catalog;
		}
		public Navigator GetNavigator()
		{
			return this.Navigator;
		}
		public ItemManager GetItemManager()
		{
			return this.ItemManager;
		}
		public RoomManager GetRoomManager()
		{
			return this.RoomManager;
		}
		public AdvertisementManager GetAdvertisementManager()
		{
			return this.AdvertisementManager;
		}
		public PixelManager GetPixelManager()
		{
			return this.PixelManager;
		}
		public AchievementManager GetAchievementManager()
		{
			return this.AchievementManager;
		}
		public ModerationTool GetModerationTool()
		{
			return this.ModerationTool;
		}
		public BotManager GetBotManager()
		{
			return this.BotManager;
		}
		internal NavigatorCache method_15()
		{
			return this.class276_0;
		}
		public QuestManager GetQuestManager()
		{
			return this.QuestManager;
		}
		public void method_17(DatabaseClient class6_0)
		{
			Logging.smethod_0("Loading your settings..");
			DataRow dataRow = class6_0.ReadDataRow("SELECT * FROM server_settings LIMIT 1");
			LicenseTools.Int32_4 = (int)dataRow["MaxRoomsPerUser"];
			LicenseTools.String_4 = (string)dataRow["motd"];
			LicenseTools.Int32_0 = (int)dataRow["timer"];
			LicenseTools.Int32_1 = (int)dataRow["credits"];
			LicenseTools.Int32_3 = (int)dataRow["pixels"];
			LicenseTools.Int32_2 = (int)dataRow["points"];
			LicenseTools.int_3 = (int)dataRow["pixels_max"];
			LicenseTools.int_5 = (int)dataRow["credits_max"];
			LicenseTools.int_4 = (int)dataRow["points_max"];
			LicenseTools.int_2 = (int)dataRow["MaxPetsPerRoom"];
			LicenseTools.int_0 = (int)dataRow["MaxMarketPlacePrice"];
			LicenseTools.int_1 = (int)dataRow["MarketPlaceTax"];
			LicenseTools.AntiDDoSEnabled = GoldTree.smethod_3(dataRow["enable_antiddos"].ToString());
			LicenseTools.Boolean_3 = GoldTree.smethod_3(dataRow["vipclothesforhcusers"].ToString());
			LicenseTools.Boolean_4 = GoldTree.smethod_3(dataRow["enable_chatlogs"].ToString());
			LicenseTools.Boolean_5 = GoldTree.smethod_3(dataRow["enable_cmdlogs"].ToString());
			LicenseTools.Boolean_6 = GoldTree.smethod_3(dataRow["enable_roomlogs"].ToString());
			LicenseTools.String_2 = (string)dataRow["enable_externalchatlinks"];
			LicenseTools.Boolean_2 = GoldTree.smethod_3(dataRow["enable_securesessions"].ToString());
			LicenseTools.Boolean_1 = GoldTree.smethod_3(dataRow["allow_friendfurnidrops"].ToString());
			LicenseTools.Boolean_0 = GoldTree.smethod_3(dataRow["enable_cmd_redeemcredits"].ToString());
			LicenseTools.bool_18 = GoldTree.smethod_3(dataRow["unload_crashedrooms"].ToString());
			LicenseTools.bool_19 = GoldTree.smethod_3(dataRow["ShowUsersAndRoomsInAbout"].ToString());
			LicenseTools.int_14 = (int)dataRow["idlesleep"];
			LicenseTools.int_15 = (int)dataRow["idlekick"];
			LicenseTools.bool_20 = GoldTree.smethod_3(dataRow["ip_lastforbans"].ToString());
            LicenseTools.int_16 = (int)dataRow["StaffPicksCategoryID"];
            LicenseTools.vipha_interval = (double)dataRow["vipha_interval"];
            LicenseTools.bool_21 = GoldTree.smethod_3(dataRow["enable_cmd_redeempixels"].ToString());
            LicenseTools.bool_22 = GoldTree.smethod_3(dataRow["enable_cmd_redeemshells"].ToString());
            LicenseTools.viphal_interval = (double)dataRow["viphal_interval"];
            LicenseTools.DisableOtherUsersToMovingOtherUsersToDoor = GoldTree.smethod_3(dataRow["DisableOtherUsersToMovingOtherUsersToDoor"].ToString());
			Logging.WriteLine("completed!");
		}
	}
}
