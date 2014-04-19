using System;
using System.Data;
using GoldTree.Util;
using GoldTree.Storage;
using GoldTree.Core;
namespace GoldTree.HabboHotel.Users.UserDataManagement
{
	internal sealed class UserDataFactory
	{
		private bool bool_0;
		private DataRow dataRow_0;
		private DataTable dataTable_0;
		private DataTable dataTable_1;
		private DataTable dataTable_2;
		private DataTable dataTable_3;
		private DataTable dataTable_4;
		private DataTable dataTable_5;
		private DataTable dataTable_6;
		private DataTable dataTable_7;
		private DataTable dataTable_8;
		private DataTable dataTable_9;
		private DataTable dataTable_10;
		private DataTable dataTable_11;
        private DataTable dataTable_12;
		internal bool Boolean_0
		{
			get
			{
				return this.bool_0;
			}
		}
		internal DataRow DataRow_0
		{
			get
			{
				return this.dataRow_0;
			}
		}
		internal DataTable DataTable_0
		{
			get
			{
				return this.dataTable_0;
			}
		}
		internal DataTable DataTable_1
		{
			get
			{
				return this.dataTable_1;
			}
		}
		internal DataTable DataTable_2
		{
			get
			{
				return this.dataTable_2;
			}
		}
		internal DataTable DataTable_3
		{
			get
			{
				return this.dataTable_3;
			}
		}
		internal DataTable DataTable_4
		{
			get
			{
				return this.dataTable_4;
			}
		}
		internal DataTable DataTable_5
		{
			get
			{
				return this.dataTable_5;
			}
		}
		internal DataTable DataTable_6
		{
			get
			{
				return this.dataTable_6;
			}
		}
		internal DataTable DataTable_7
		{
			get
			{
				return this.dataTable_7;
			}
		}
		internal DataTable DataTable_8
		{
			get
			{
				return this.dataTable_8;
			}
		}
		internal DataTable DataTable_9
		{
			get
			{
				return this.dataTable_9;
			}
		}
		internal DataTable DataTable_10
		{
			get
			{
				return this.dataTable_10;
			}
			set
			{
				this.dataTable_10 = value;
			}
		}
		internal DataTable DataTable_11
		{
			get
			{
				return this.dataTable_11;
			}
		}
        internal DataTable DataTable_12
        {
            get
            {
                return this.dataTable_12;
            }
        }
		public UserDataFactory(string string_0, string string_1, bool bool_1)
		{
            if (string.IsNullOrEmpty(string_0))
            {
                this.bool_0 = false;
            }
            else
            {
                using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
                {
                    @class.AddParamWithValue("auth_ticket", string_0);
                    string str = "";
                    if (ServerConfiguration.EnableSSO)
                    {
                        str = "AND ip_last = '" + string_1 + "' ";
                    }
                    try
                    {
                        if (int.Parse(GoldTree.GetConfig().data["debug"]) == 1)
                        {
                            str = "";
                        }
                    }
                    catch
                    {
                    }
                    this.dataRow_0 = @class.ReadDataRow("SELECT * FROM users WHERE auth_ticket = @auth_ticket " + str + " LIMIT 1;");
                    if (this.dataRow_0 != null)
                    {
                        this.bool_0 = true;
                        uint num = (uint)this.dataRow_0["Id"];
                        if (bool_1)
                        {
                            this.dataTable_0 = @class.ReadDataTable("SELECT achievement_id,achievement_level FROM user_achievements WHERE user_id = '" + num + "'");
                            this.dataTable_1 = @class.ReadDataTable("SELECT room_id FROM user_favorites WHERE user_id = '" + num + "'");
                            this.dataTable_2 = @class.ReadDataTable("SELECT ignore_id FROM user_ignores WHERE user_id = '" + num + "'");
                            this.dataTable_3 = @class.ReadDataTable("SELECT tag FROM user_tags WHERE user_id = '" + num + "'");
                            this.dataTable_4 = @class.ReadDataTable("SELECT subscription_id, timestamp_activated, timestamp_expire FROM user_subscriptions WHERE user_id = '" + num + "'");
                            this.dataTable_5 = @class.ReadDataTable("SELECT user_badges.badge_id,user_badges.badge_slot FROM user_badges WHERE user_id = " + num);
                            this.dataTable_6 = @class.ReadDataTable("SELECT items.Id,items.base_item,items_extra_data.extra_data FROM items LEFT JOIN items_extra_data ON items_extra_data.item_id = items.Id WHERE room_id = 0 AND user_id = " + num);
                            this.dataTable_7 = @class.ReadDataTable("SELECT user_effects.effect_id,user_effects.total_duration,user_effects.is_activated,user_effects.activated_stamp FROM user_effects WHERE user_id =  " + num);
                            this.dataTable_8 = @class.ReadDataTable("SELECT users.Id,users.username,users.motto,users.look,users.last_online FROM users JOIN messenger_friendships ON users.Id = messenger_friendships.user_two_id WHERE messenger_friendships.user_one_id = '" + num + "'");
                            this.dataTable_9 = @class.ReadDataTable("SELECT messenger_requests.Id,messenger_requests.from_id,users.username FROM users JOIN messenger_requests ON users.Id = messenger_requests.from_id WHERE messenger_requests.to_id = '" + num + "'");
                            @class.AddParamWithValue("name", (string)this.dataRow_0["username"]);
                            this.dataTable_10 = @class.ReadDataTable("SELECT * FROM rooms WHERE owner = @name ORDER BY Id ASC LIMIT " + ServerConfiguration.RoomUserLimit);
                            this.dataTable_11 = @class.ReadDataTable("SELECT Id, user_id, room_id, name, type, race, color, expirience, energy, nutrition, respect, createstamp, x, y, z FROM user_pets WHERE user_id = " + num + " AND room_id = 0");
                            this.dataTable_12 = @class.ReadDataTable("SELECT friend_stream.id, friend_stream.type, friend_stream.userid, friend_stream.gender, friend_stream.look, friend_stream.time, friend_stream.data, friend_stream.data_extra FROM friend_stream JOIN messenger_friendships ON friend_stream.userid = messenger_friendships.user_two_id WHERE messenger_friendships.user_one_id = '" + num + "' ORDER BY friend_stream.time DESC LIMIT 15");
                            @class.ExecuteQuery(string.Concat(new object[]
                        {
                            "UPDATE users SET online = '1', auth_ticket = '' WHERE Id = '",
                            num,
                            "' LIMIT 1; UPDATE user_info SET login_timestamp = '",
                            GoldTree.GetUnixTimestamp(),
                            "' WHERE user_id = '",
                            num,
                            "' LIMIT 1;"
                        }));
                        //    @class.ExecuteQuery(string.Concat(new object[]
                        //{
                        //    "UPDATE users SET online = '1'" + /*auth_ticket = ''*/ "WHERE Id = '",
                        //    num,
                        //    "' LIMIT 1; UPDATE user_info SET login_timestamp = '",
                        //    GoldTree.GetUnixTimestamp(),
                        //    "' WHERE user_id = '",
                        //    num,
                        //    "' LIMIT 1;"
                        //}));
                        }
                    }
                    else
                    {
                        this.bool_0 = false;
                    }
                }
			}
		}
		public UserDataFactory(string string_0, bool bool_1)
		{
			using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
			{
				@class.AddParamWithValue("username", string_0);
				this.dataRow_0 = @class.ReadDataRow("SELECT * FROM users WHERE username = @username LIMIT 1;");
				if (this.dataRow_0 != null)
				{
					this.bool_0 = true;
					uint num = (uint)this.dataRow_0["Id"];
					if (bool_1)
					{
						this.dataTable_0 = @class.ReadDataTable("SELECT achievement_id,achievement_level FROM user_achievements WHERE user_id = '" + num + "'");
						this.dataTable_1 = @class.ReadDataTable("SELECT room_id FROM user_favorites WHERE user_id = '" + num + "'");
						this.dataTable_2 = @class.ReadDataTable("SELECT ignore_id FROM user_ignores WHERE user_id = '" + num + "'");
						this.dataTable_3 = @class.ReadDataTable("SELECT tag FROM user_tags WHERE user_id = '" + num + "'");
						this.dataTable_4 = @class.ReadDataTable("SELECT subscription_id, timestamp_activated, timestamp_expire FROM user_subscriptions WHERE user_id = '" + num + "'");
						this.dataTable_5 = @class.ReadDataTable("SELECT user_badges.badge_id,user_badges.badge_slot FROM user_badges WHERE user_id = " + num);
						this.dataTable_6 = @class.ReadDataTable("SELECT items.Id,items.base_item,items_extra_data.extra_data FROM items LEFT JOIN items_extra_data ON items_extra_data.item_id = items.Id WHERE room_id = 0 AND user_id = " + num);
						this.dataTable_7 = @class.ReadDataTable("SELECT user_effects.effect_id,user_effects.total_duration,user_effects.is_activated,user_effects.activated_stamp FROM user_effects WHERE user_id =  " + num);
						this.dataTable_8 = @class.ReadDataTable("SELECT users.Id,users.username,users.motto,users.look,users.last_online FROM users JOIN messenger_friendships ON users.Id = messenger_friendships.user_two_id WHERE messenger_friendships.user_one_id = '" + num + "'");
						this.dataTable_9 = @class.ReadDataTable("SELECT messenger_requests.Id,messenger_requests.from_id,users.username FROM users JOIN messenger_requests ON users.Id = messenger_requests.from_id WHERE messenger_requests.to_id = '" + num + "'");
						@class.AddParamWithValue("name", (string)this.dataRow_0["username"]);
						this.dataTable_10 = @class.ReadDataTable("SELECT * FROM rooms WHERE owner = @name ORDER BY Id ASC LIMIT " + ServerConfiguration.RoomUserLimit);
                        this.dataTable_12 = @class.ReadDataTable("SELECT friend_stream.id, friend_stream.type, friend_stream.userid, friend_stream.gender, friend_stream.look, friend_stream.time, friend_stream.data, friend_stream.data_extra FROM friend_stream JOIN messenger_friendships ON friend_stream.userid = messenger_friendships.user_two_id WHERE messenger_friendships.user_one_id = '" + num + "' ORDER BY friend_stream.time DESC LIMIT 15");
					}
				}
				else
				{
					this.bool_0 = false;
				}
			}
		}
	}
}
