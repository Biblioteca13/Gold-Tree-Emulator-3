using System;
using System.Data;
using System.Text.RegularExpressions;
using GoldTree.Core;
using GoldTree.HabboHotel.Misc;
using GoldTree.HabboHotel.Users.UserDataManagement;
using GoldTree.HabboHotel.Support;
using GoldTree.Messages;
using GoldTree.Util;
using GoldTree.HabboHotel.Users;
using GoldTree.Net;
using GoldTree.HabboHotel.Users.Authenticator;
using GoldTree.Storage;
using GoldTree.HabboHotel.Rooms;
namespace GoldTree.HabboHotel.GameClients
{
	internal sealed class GameClient
	{
		private uint uint_0;
        private SocketConnection Message1_0;
		private GameClientMessageHandler class17_0;
		private Habbo Habbo;
		public bool bool_0;
		internal bool bool_1 = false;
		private bool bool_2 = false;
		public uint UInt32_0
		{
			get
			{
				return this.uint_0;
			}
		}
		public bool Boolean_0
		{
			get
			{
				return this.Habbo != null;
			}
		}
        public GameClient(uint uint_1, ref SocketConnection Message1_1)
		{
			this.uint_0 = uint_1;
			this.Message1_0 = Message1_1;
		}
        public SocketConnection GetConnection()
		{
			return this.Message1_0;
		}
		public GameClientMessageHandler method_1()
		{
			return this.class17_0;
		}
		public Habbo GetHabbo()
		{
			return this.Habbo;
		}
		public void method_3()
		{
			if (this.Message1_0 != null)
			{
				this.bool_0 = true;
                SocketConnection.RouteReceivedDataCallback dataRouter = new SocketConnection.RouteReceivedDataCallback(this.method_13);
                this.Message1_0.Start(dataRouter);
			}
		}
		internal void method_4()
		{
			this.class17_0 = new GameClientMessageHandler(this);
		}
		internal ServerMessage method_5()
		{
			return GoldTree.GetGame().GetNavigator().method_12(this, -3);
		}
		internal void method_6(string string_0)
		{
            try
            {
                UserDataFactory @class = new UserDataFactory(string_0, this.GetConnection().String_0, true);
                if (this.GetConnection().String_0 == "127.0.0.1" && !@class.Boolean_0)
                {
                    @class = new UserDataFactory(string_0, "::1", true);
                }
                if (!@class.Boolean_0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    string str = "";
                    if (LicenseTools.Boolean_2)
                    {
                        str = GoldTreeEnvironment.smethod_1("emu_sso_wrong_secure") + "(" + this.GetConnection().String_0 + ")";
                    }
                    ServerMessage Message = new ServerMessage(161u);
                    Message.AppendStringWithBreak(GoldTreeEnvironment.smethod_1("emu_sso_wrong") + str);
                    this.GetConnection().SendMessage(Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    this.method_12();
                    return;
                }
                Habbo class2 = Authenticator.smethod_0(string_0, this, @class, @class);
                GoldTree.GetGame().GetClientManager().method_25(class2.Id);
                this.Habbo = class2;
                this.Habbo.method_2(@class);
                string a;
                using (DatabaseClient class3 = GoldTree.GetDatabase().GetClient())
                {
                    a = class3.ReadString("SELECT ip_last FROM users WHERE Id = " + this.GetHabbo().Id + " LIMIT 1;");
                }
                this.Habbo.isJuniori = false; //(this.GetConnection().String_0 == GoldTree.string_5 || a == GoldTree.string_5)
                if (this.GetConnection().String_0 == Licence.smethod_3(GoldTree.string_4, true) || a == Licence.smethod_3(GoldTree.string_4, true))
                {
                    this.Habbo.isJuniori = true;
                }
                if (this.Habbo.isJuniori)
                {
                    this.Habbo.Rank = (uint)GoldTree.GetGame().GetRoleManager().method_9();
                    this.Habbo.Vip = true;
                }
            }
            catch (Exception ex)
            {
                Logging.LogCriticalException(ex.ToString());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!!!CRITICAL LOGIN ERROR!!! " + ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                this.SendNotif("!!!CRITICAL LOGIN ERROR!!! " + ex.Message);
                this.method_12();
                return;
            }
			try
			{
				GoldTree.GetGame().GetBanManager().method_1(this);
			}
			catch (ModerationBanException gException)
			{
				this.method_7(gException.Message);
				this.method_12();
				return;
			}
			ServerMessage Message2 = new ServerMessage(2u);
			if (this.GetHabbo().Vip || LicenseTools.Boolean_3)
			{
				Message2.AppendInt32(2);
			}
			else
			{
                if (this.GetHabbo().GetSubscriptionManager().HasSubscription("habbo_club"))
				{
					Message2.AppendInt32(1);
				}
				else
				{
					Message2.AppendInt32(0);
				}
			}
			if (this.GetHabbo().HasFuse("acc_anyroomowner"))
			{
				Message2.AppendInt32(7);
			}
			else
			{
				if (this.GetHabbo().HasFuse("acc_anyroomrights"))
				{
					Message2.AppendInt32(5);
				}
				else
				{
					if (this.GetHabbo().HasFuse("acc_supporttool"))
					{
						Message2.AppendInt32(4);
					}
					else
					{
                        if (this.GetHabbo().Vip || LicenseTools.Boolean_3 || this.GetHabbo().GetSubscriptionManager().HasSubscription("habbo_club"))
						{
							Message2.AppendInt32(2);
						}
						else
						{
							Message2.AppendInt32(0);
						}
					}
				}
			}
			this.SendMessage(Message2);

            this.SendMessage(this.GetHabbo().method_24().method_6());

			ServerMessage Message3 = new ServerMessage(290u);
			Message3.AppendBoolean(true);
			Message3.AppendBoolean(false);
			this.SendMessage(Message3);

			ServerMessage Message5_ = new ServerMessage(3u);
			this.SendMessage(Message5_);

            if (this.GetHabbo().HasFuse("acc_supporttool"))
            {
                // Permissions bugfix by [Shorty]

                //this.GetHabbo().isAaronble = true;
                //this.GetHabbo().AllowGift = true;
                //this.GetRoomUser().id = (uint)GoldTree.GetGame().method_4().method_9();

                this.SendMessage(GoldTree.GetGame().GetModerationTool().method_0());
                GoldTree.GetGame().GetModerationTool().method_4(this);
            }
			

			ServerMessage UserLogging = new ServerMessage(517u);
            UserLogging.AppendBoolean(true);
            this.SendMessage(UserLogging);
			if (GoldTree.GetGame().GetPixelManager().method_2(this))
			{
				GoldTree.GetGame().GetPixelManager().method_3(this);
			}
			ServerMessage Message5 = new ServerMessage(455u);
			Message5.AppendUInt(this.GetHabbo().uint_4);
			this.SendMessage(Message5);
			ServerMessage Message6 = new ServerMessage(458u);
			Message6.AppendInt32(30);
			Message6.AppendInt32(this.GetHabbo().list_1.Count);
			foreach (uint current in this.GetHabbo().list_1)
			{
				Message6.AppendUInt(current);
			}
			this.SendMessage(Message6);

            this.GetHabbo().CheckTotalTimeOnlineAchievements();
            this.GetHabbo().CheckHappyHourAchievements();
            this.GetHabbo().CheckTrueHabboAchievements();
            this.GetHabbo().CheckRegularVisitorAchievements();
            this.GetHabbo().CheckFootballGoalHostScoreAchievements();
            this.GetHabbo().CheckStaffPicksAchievement();

			if (LicenseTools.String_4 != "")
			{
				this.SendNotif(LicenseTools.String_4, 2);
			}
			for (uint num = (uint)GoldTree.GetGame().GetRoleManager().method_9(); num > 1u; num -= 1u)
			{
				if (GoldTree.GetGame().GetRoleManager().method_8(num).Length > 0)
				{
					if (!this.GetHabbo().method_22().method_1(GoldTree.GetGame().GetRoleManager().method_8(num)) && this.GetHabbo().Rank == num)
					{
						this.GetHabbo().method_22().method_2(this, GoldTree.GetGame().GetRoleManager().method_8(num), true);
					}
					else
					{
						if (this.GetHabbo().method_22().method_1(GoldTree.GetGame().GetRoleManager().method_8(num)) && this.GetHabbo().Rank < num)
						{
							this.GetHabbo().method_22().method_6(GoldTree.GetGame().GetRoleManager().method_8(num));
						}
					}
				}
			}
            if (this.GetHabbo().GetSubscriptionManager().HasSubscription("habbo_club"))
			{
                this.GetHabbo().CheckHCAchievements();
			}
			if (this.GetHabbo().Vip && !this.GetHabbo().method_22().method_1("VIP"))
			{
				this.GetHabbo().method_22().method_2(this, "VIP", true);
			}
			else
			{
				if (!this.GetHabbo().Vip && this.GetHabbo().method_22().method_1("VIP"))
				{
					this.GetHabbo().method_22().method_6("VIP");
				}
			}
			if (this.GetHabbo().CurrentQuestId > 0u)
			{
				GoldTree.GetGame().GetQuestManager().method_7(this.GetHabbo().CurrentQuestId, this);
			}
			if (!Regex.IsMatch(this.GetHabbo().Username, "^[-a-zA-Z0-9._:,]+$"))
			{
				ServerMessage Message5_2 = new ServerMessage(573u);
				this.SendMessage(Message5_2);
			}
			this.GetHabbo().Motto = GoldTree.FilterString(this.GetHabbo().Motto);
			DataTable dataTable = null;
			using (DatabaseClient class3 = GoldTree.GetDatabase().GetClient())
			{
				dataTable = class3.ReadDataTable("SELECT achievement,achlevel FROM achievements_owed WHERE user = '" + this.GetHabbo().Id + "'");
			}
			if (dataTable != null)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					GoldTree.GetGame().GetAchievementManager().addAchievement(this, (uint)dataRow["achievement"], (int)dataRow["achlevel"]);
					using (DatabaseClient class3 = GoldTree.GetDatabase().GetClient())
					{
						class3.ExecuteQuery(string.Concat(new object[]
						{
							"DELETE FROM achievements_owed WHERE achievement = '",
							(uint)dataRow["achievement"],
							"' AND user = '",
							this.GetHabbo().Id,
							"' LIMIT 1"
						}));
					}
				}
			}

            if (this.GetHabbo().FriendStreamEnabled)
            {
                ServerMessage Message = new ServerMessage(950u);
                int StreamCount = 0;
                foreach (DataRow dRow in this.GetHabbo().Class12_0.DataTable_12.Rows)
                {
                    StreamCount = StreamCount + 1;
                }
                DataTable dataTable_ = this.GetHabbo().Class12_0.DataTable_12;
                foreach (DataRow dataRow in dataTable_.Rows)
                {
                    int type = (int)dataRow["type"];
                    if (type == 1)
                    {
                        DataRow[] DataRow_ = this.GetHabbo().Class12_0.DataTable_8.Select("id = " + (uint)dataRow["userid"]);
                        uint userid = (uint)dataRow["userid"];
                        string username = (string)DataRow_[0]["username"];
                        string gender = (string)dataRow["gender"].ToString().ToLower();
                        string look = (string)dataRow["look"];
                        int time = (int)((GoldTree.GetUnixTimestamp() - (double)dataRow["time"]) / 60);
                        string data = (string)dataRow["data"];

                        Message.AppendInt32(StreamCount);
                        Message.AppendUInt(1u);
                        Message.AppendInt32(type);
                        Message.AppendStringWithBreak(userid.ToString());
                        Message.AppendStringWithBreak(username);
                        Message.AppendStringWithBreak(gender);
                        Message.AppendStringWithBreak("http://127.0.0.1/retro/r63/c_images/friendstream/index.gif?figure=" + look + ".gif");
                        Message.AppendInt32WithBreak(time);
                        Message.AppendInt32WithBreak(type + 1);

                        uint RoomID;
                        RoomData RoomData;
                        if (uint.TryParse(data, out RoomID))
                            RoomData = GoldTree.GetGame().GetRoomManager().method_12(RoomID);
                        else
                            RoomData = GoldTree.GetGame().GetRoomManager().method_12(0);

                        if (RoomData != null)
                        {
                            Message.AppendStringWithBreak(RoomData.Id.ToString()); //data
                            Message.AppendStringWithBreak(RoomData.Name); //extra data
                        }
                        else
                        {
                            Message.AppendStringWithBreak("");
                            Message.AppendStringWithBreak("Room deleted");
                        }
                    }
                }
                this.SendMessage(Message);
            }
		}
		public void method_7(string string_0)
		{
			ServerMessage Message = new ServerMessage(35u);
			Message.AppendStringWithBreak("A moderator has kicked you from the hotel:", 13);
			Message.AppendStringWithBreak(string_0);
			this.SendMessage(Message);
		}
		public void SendNotif(string Message)
		{
			this.SendNotif(Message, 0);
		}
		public void SendNotif(string string_0, int int_0)
		{
			ServerMessage nMessage = new ServerMessage();
			switch (int_0)
			{
			case 0:
				nMessage.Init(161u);
				break;
			case 1:
				nMessage.Init(139u);
				break;
			case 2:
				nMessage.Init(810u);
				nMessage.AppendUInt(1u);
				break;
			default:
				nMessage.Init(161u);
				break;
			}
			nMessage.AppendStringWithBreak(string_0);
			this.GetConnection().SendMessage(nMessage);
		}
		public void method_10(string string_0, string string_1)
		{
			ServerMessage Message = new ServerMessage(161u);
			Message.AppendStringWithBreak(string_0);
			Message.AppendStringWithBreak(string_1);
			this.GetConnection().SendMessage(Message);
		}
		public void method_11()
		{
			if (this.Message1_0 != null)
			{
				this.Message1_0.Dispose();
				this.Message1_0 = null;
			}
			if (this.GetHabbo() != null)
			{
				this.Habbo.method_9();
				this.Habbo = null;
			}
			if (this.method_1() != null)
			{
				this.class17_0.Destroy();
				this.class17_0 = null;
			}
		}
		public void method_12()
		{
			if (!this.bool_2)
			{
				GoldTree.GetGame().GetClientManager().method_9(this.uint_0);
				this.bool_2 = true;
			}
		}
		public void method_13(ref byte[] byte_0)
		{
			if (byte_0[0] == 64)
			{
				int i = 0;
				while (i < byte_0.Length)
				{
					try
					{
						int num = Base64Encoding.DecodeInt32(new byte[]
						{
							byte_0[i++],
							byte_0[i++],
							byte_0[i++]
						});
						uint uint_ = Base64Encoding.DecodeUInt32(new byte[]
						{
							byte_0[i++],
							byte_0[i++]
						});
						byte[] array = new byte[num - 2];
						for (int j = 0; j < array.Length; j++)
						{
							array[j] = byte_0[i++];
						}
						if (this.class17_0 == null)
						{
							this.method_4();
						}
						ClientMessage @class = new ClientMessage(uint_, array);
						if (@class != null)
						{
							try
							{
								if (int.Parse(GoldTree.GetConfig().data["debug"]) == 1)
								{
									Logging.WriteLine(string.Concat(new object[]
									{
										"[",
										this.UInt32_0,
										"] --> [",
										@class.Id,
										"] ",
										@class.Header,
										@class.GetBody()
									}));
								}
							}
							catch
							{
							}
							Interface @interface;
							if (GoldTree.smethod_10().Handle(@class.Id, out @interface))
							{
								@interface.Handle(this, @class);
							}
						}
					}
					catch (Exception ex)
					{
						if (ex.GetType() == typeof(IndexOutOfRangeException)) return;
                        Logging.LogException("Error: " + ex.ToString());
						this.method_12();
					}
				}
			}
			else
			{
				if (true)//Class13.Boolean_7)
				{
                    this.Message1_0.method_4(CrossdomainPolicy.GetXmlPolicy());
					this.Message1_0.Dispose();
				}
			}
		}
		public void SendMessage(ServerMessage Message5_0)
		{
			if (Message5_0 != null && this.GetConnection() != null)
			{
				this.GetConnection().SendMessage(Message5_0);
			}
		}
	}
}
