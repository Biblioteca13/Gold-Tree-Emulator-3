using System;
using System.Collections.Generic;
using System.Data;
using GoldTree.HabboHotel.Users.UserDataManagement;
using GoldTree.HabboHotel.GameClients;
using GoldTree.Messages;
using GoldTree.Storage;
namespace GoldTree.HabboHotel.Users.Badges
{
	internal sealed class BadgeComponent
	{
		private List<Badge> list_0;
		private uint uint_0;
		public int Int32_0
		{
			get
			{
				return this.list_0.Count;
			}
		}
		public int Int32_1
		{
			get
			{
				int num = 0;
				foreach (Badge current in this.list_0)
				{
					if (current.Slot > 0)
					{
						num++;
					}
				}
				return num;
			}
		}
		public List<Badge> List_0
		{
			get
			{
				return this.list_0;
			}
		}
		public BadgeComponent(uint uint_1, UserDataFactory class12_0)
		{
			this.list_0 = new List<Badge>();
			this.uint_0 = uint_1;
			DataTable dataTable_ = class12_0.DataTable_5;
			if (dataTable_ != null)
			{
				foreach (DataRow dataRow in dataTable_.Rows)
				{
					this.list_0.Add(new Badge((string)dataRow["badge_id"], (int)dataRow["badge_slot"]));
				}
			}
		}
		public Badge method_0(string string_0)
		{
			Badge result;
			foreach (Badge current in this.list_0)
			{
				if (string_0.ToLower() == current.Code.ToLower())
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}
		public bool method_1(string string_0)
		{
			return this.method_0(string_0) != null;
		}
		public void method_2(GameClient Session, string string_0, bool bool_0)
		{
            if (Session != null && Session.GetHabbo() != null)
            {
                this.method_3(string_0, 0, bool_0);
                ServerMessage Message = new ServerMessage(832u);
                Message.AppendInt32(1);
                Message.AppendInt32(4);
                Message.AppendInt32(1);
                Message.AppendUInt(GoldTree.GetGame().GetAchievementManager().method_0(string_0));
                Session.SendMessage(Message);
                Session.SendMessage(Session.GetHabbo().GetBadgeComponent().method_7());
            }
		}
		public void method_3(string string_0, int int_0, bool bool_0)
		{
			if (!this.method_1(string_0))
			{
				if (bool_0)
				{
					using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
					{
						@class.AddParamWithValue("badge", string_0);
						@class.ExecuteQuery(string.Concat(new object[]
						{
							"INSERT INTO user_badges (user_id,badge_id,badge_slot) VALUES ('",
							this.uint_0,
							"',@badge,'",
							int_0,
							"')"
						}));
					}
				}
				this.list_0.Add(new Badge(string_0, int_0));
			}
		}
		public void method_4(string string_0, int int_0)
		{
			Badge @class = this.method_0(string_0);
			if (@class != null)
			{
				@class.Slot = int_0;
			}
		}
		public void method_5()
		{
			foreach (Badge current in this.list_0)
			{
				current.Slot = 0;
			}
		}
		public void method_6(string string_0)
		{
			if (this.method_1(string_0))
			{
				using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
				{
					@class.AddParamWithValue("badge", string_0);
					@class.ExecuteQuery("DELETE FROM user_badges WHERE badge_id = @badge AND user_id = '" + this.uint_0 + "' LIMIT 1");
				}
				this.list_0.Remove(this.method_0(string_0));
			}
		}
		public ServerMessage method_7()
		{
			List<Badge> list = new List<Badge>();
			ServerMessage Message = new ServerMessage(229u);
			Message.AppendInt32(this.Int32_0);
			foreach (Badge current in this.list_0)
			{
				Message.AppendUInt(GoldTree.GetGame().GetAchievementManager().method_0(current.Code));
				Message.AppendStringWithBreak(current.Code);
				if (current.Slot > 0)
				{
					list.Add(current);
				}
			}
			Message.AppendInt32(list.Count);
			foreach (Badge current in list)
			{
				Message.AppendInt32(current.Slot);
				Message.AppendStringWithBreak(current.Code);
			}
			return Message;
		}
	}
}
