using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using GoldTree.Core;
using GoldTree.HabboHotel.GameClients;
using GoldTree.Util;
using GoldTree.Storage;
namespace GoldTree.HabboHotel.Support
{
	internal sealed class ModerationBanManager
	{
		public List<ModerationBan> list_0;
		public ModerationBanManager()
		{
			this.list_0 = new List<ModerationBan>();
		}
		public void method_0(DatabaseClient class6_0)
		{
            Logging.smethod_0("Loading bans..");
			this.list_0.Clear();
			DataTable dataTable = class6_0.ReadDataTable("SELECT bantype,value,reason,expire FROM bans WHERE expire > '" + GoldTree.GetUnixTimestamp() + "'");
			if (dataTable != null)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					ModerationBanType Type = ModerationBanType.IP;
					if ((string)dataRow["bantype"] == "user")
					{
						Type = ModerationBanType.USERNAME;
					}
					this.list_0.Add(new ModerationBan(Type, (string)dataRow["value"], (string)dataRow["reason"], (double)dataRow["expire"]));
				}
				Logging.WriteLine("completed!");
			}
		}
		public void method_1(GameClient Session)
		{
			foreach (ModerationBan current in this.list_0)
			{
				if (!current.Expired)
				{
					if (current.Type == ModerationBanType.IP && Session.GetConnection().String_0 == current.Variable)
					{
						throw new ModerationBanException(current.ReasonMessage);
					}
					if (Session.GetHabbo() != null && (current.Type == ModerationBanType.USERNAME && Session.GetHabbo().Username.ToLower() == current.Variable.ToLower()))
					{
						throw new ModerationBanException(current.ReasonMessage);
					}
				}
			}
		}
		public void method_2(GameClient Session, string string_0, double double_0, string string_1, bool bool_0)
		{
			if (!Session.GetHabbo().isJuniori)
			{
				ModerationBanType enum4_ = ModerationBanType.USERNAME;
				string text = Session.GetHabbo().Username;
				string object_ = "user";
				double num = GoldTree.GetUnixTimestamp() + double_0;
				if (bool_0)
				{
					enum4_ = ModerationBanType.IP;
					if (!LicenseTools.bool_20)
					{
						text = Session.GetConnection().String_0;
					}
					else
					{
						using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
						{
							text = @class.ReadString("SELECT ip_last FROM users WHERE Id = " + Session.GetHabbo().Id + " LIMIT 1;");
						}
					}
					object_ = "ip";
				}
				this.list_0.Add(new ModerationBan(enum4_, text, string_1, num));
				using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
				{
					@class.AddParamWithValue("rawvar", object_);
					@class.AddParamWithValue("var", text);
					@class.AddParamWithValue("reason", string_1);
					@class.AddParamWithValue("mod", string_0);
					@class.ExecuteQuery(string.Concat(new object[]
					{
						"INSERT INTO bans (bantype,value,reason,expire,added_by,added_date,appeal_state) VALUES (@rawvar,@var,@reason,'",
						num,
						"',@mod,'",
						DateTime.Now.ToLongDateString(),
						"', '1')"
					}));
				}
				if (bool_0)
				{
					DataTable dataTable = null;
					using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
					{
						@class.AddParamWithValue("var", text);
						dataTable = @class.ReadDataTable("SELECT Id FROM users WHERE ip_last = @var");
					}
					if (dataTable == null)
					{
						goto IL_268;
					}
					IEnumerator enumerator = dataTable.Rows.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							DataRow dataRow = (DataRow)enumerator.Current;
							using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
							{
								@class.ExecuteQuery("UPDATE user_info SET bans = bans + 1 WHERE user_id = '" + (uint)dataRow["Id"] + "' LIMIT 1");
							}
						}
						goto IL_268;
					}
					finally
					{
						IDisposable disposable = enumerator as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
				using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
				{
					@class.ExecuteQuery("UPDATE user_info SET bans = bans + 1 WHERE user_id = '" + Session.GetHabbo().Id + "' LIMIT 1");
				}
				IL_268:
				Session.method_7("You have been banned: " + string_1);
				Session.method_12();
			}
		}
	}
}
