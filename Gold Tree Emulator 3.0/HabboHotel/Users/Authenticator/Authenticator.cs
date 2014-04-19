using System;
using System.Data;
using GoldTree.HabboHotel.Users.UserDataManagement;
using GoldTree.HabboHotel.GameClients;
using GoldTree.HabboHotel.Users;
namespace GoldTree.HabboHotel.Users.Authenticator
{
	internal sealed class Authenticator
	{
		internal static Habbo smethod_0(string string_0, GameClient Session, UserDataFactory class12_0, UserDataFactory class12_1)
		{
			return Authenticator.smethod_1(class12_0.DataRow_0, string_0, Session, class12_1);
		}
		private static Habbo smethod_1(DataRow habboData, string SSOTicket, GameClient Session, UserDataFactory class12_0)
		{
			uint Id = (uint)habboData["Id"];
			string Username = (string)habboData["username"];
			string Name = (string)habboData["real_name"];
			uint Rank = (uint)habboData["rank"];
			string Motto = (string)habboData["motto"];
			string ip_last = (string)habboData["ip_last"];
			string look = (string)habboData["look"];
			string gender = (string)habboData["gender"];
			int credits = (int)habboData["credits"];
			int pixels = (int)habboData["activity_points"];
            string account_created = (string)habboData["account_created"];
			double activity_points_lastupdate = (double)habboData["activity_points_lastupdate"];
            string last_loggedin = (string)habboData["last_loggedin"];
            int daily_respect_points = (int)habboData["daily_respect_points"];
            int daily_pet_respect_points = (int)habboData["daily_pet_respect_points"];
            double vipha_last = (double)habboData["vipha_last"];
            double viphal_last = (double)habboData["viphal_last"];
            return new Habbo(Id, Username, Name, SSOTicket, Rank, Motto, look, gender, credits, pixels, activity_points_lastupdate, account_created, GoldTree.StringToBoolean(habboData["is_muted"].ToString()), (uint)habboData["home_room"], (int)habboData["newbie_status"], GoldTree.StringToBoolean(habboData["block_newfriends"].ToString()), GoldTree.StringToBoolean(habboData["hide_inroom"].ToString()), GoldTree.StringToBoolean(habboData["hide_online"].ToString()), GoldTree.StringToBoolean(habboData["vip"].ToString()), (int)habboData["volume"], (int)habboData["vip_points"], GoldTree.StringToBoolean(habboData["accept_trading"].ToString()), ip_last, Session, class12_0, last_loggedin, daily_respect_points, daily_pet_respect_points, vipha_last, viphal_last, GoldTree.StringToBoolean(habboData["friend_stream_enabled"].ToString()));
		}
		internal static Habbo smethod_2(string string_0)
		{
			UserDataFactory @class = new UserDataFactory(string_0, false);
			return Authenticator.smethod_1(@class.DataRow_0, "", null, @class);
		}
	}
}
