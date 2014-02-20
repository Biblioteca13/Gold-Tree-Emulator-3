using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GoldTree.Core;
using GoldTree.HabboHotel.Users.UserDataManagement;
using GoldTree.HabboHotel.GameClients;
using GoldTree.HabboHotel.Pets;
using GoldTree.HabboHotel.Items;
using GoldTree.Messages;
using GoldTree.Storage;
using GoldTree.HabboHotel.SoundMachine;
using GoldTree.Source.HabboHotel.SoundMachine;
namespace GoldTree.HabboHotel.Users.Inventory
{
	internal sealed class InventoryComponent
	{
        private Hashtable discs;
		public List<UserItem> list_0;
		private Hashtable hashtable_0;
		private Hashtable hashtable_1;
		public List<uint> list_1;
		private GameClient Session;
		public uint uint_0;
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
				return this.hashtable_0.Count;
			}
		}
		public InventoryComponent(uint uint_1, GameClient class16_1, UserDataFactory class12_0)
		{
			this.Session = class16_1;
			this.uint_0 = uint_1;
			this.list_0 = new List<UserItem>();
			this.hashtable_0 = new Hashtable();
			this.hashtable_1 = new Hashtable();
            this.discs = new Hashtable();
			this.list_1 = new List<uint>();
			this.list_0.Clear();
			DataTable dataTable_ = class12_0.DataTable_6;
			foreach (DataRow dataRow in dataTable_.Rows)
			{
                string str;
                uint id = Convert.ToUInt32(dataRow["Id"]);
                uint baseItem = Convert.ToUInt32(dataRow["base_item"]);
                if (!DBNull.Value.Equals(dataRow["extra_data"]))
                {
                    str = (string)dataRow["extra_data"];
                }
                else
                {
                    str = string.Empty;
                }

                list_0.Add(new UserItem(id, baseItem, str));
                UserItem item = new UserItem(id, baseItem, str);

                if (item.method_1().InteractionType == "musicdisc")
                {
                    this.discs.Add(item.uint_0, item);
                }
			}

			this.hashtable_0.Clear();
			DataTable dataTable_2 = class12_0.DataTable_11;
			foreach (DataRow dataRow in dataTable_2.Rows)
			{
				Pet @class = GoldTree.GetGame().GetCatalog().method_12(dataRow);
				this.hashtable_0.Add(@class.PetId, @class);
			}
		}
		public void method_0()
		{
			using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
			{
				@class.ExecuteQuery("DELETE FROM items WHERE room_id = 0 AND user_id = '" + this.uint_0 + "';");
			}
            this.discs.Clear();
			this.hashtable_1.Clear();
			this.list_1.Clear();
			this.list_0.Clear();
			ServerMessage Message5_ = new ServerMessage(101u);
			this.GetClient().SendMessage(Message5_);
		}
		public void method_1(GameClient class16_1)
		{
			int num = 0;
			List<UserItem> list = new List<UserItem>();
			foreach (UserItem current in this.list_0)
			{
				if (current != null && (current.method_1().Name.StartsWith("CF_") || current.method_1().Name.StartsWith("CFC_")))
				{
					string[] array = current.method_1().Name.Split(new char[]
					{
						'_'
					});
					int num2 = int.Parse(array[1]);
					if (!this.list_1.Contains(current.uint_0))
					{
						if (num2 > 0)
						{
							num += num2;
						}
						list.Add(current);
					}
				}
			}
			foreach (UserItem current in list)
			{
				this.method_12(current.uint_0, 0u, false);
			}
			class16_1.GetHabbo().Credits += num;
			class16_1.GetHabbo().method_13(true);
			class16_1.SendNotif("All coins in your inventory have been converted back into " + num + " credits!");
		}
		public void method_2()
		{
			using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
			{
				@class.ExecuteQuery("DELETE FROM user_pets WHERE user_id = " + this.uint_0 + " AND room_id = 0;");
			}
			foreach (Pet class2 in this.hashtable_0.Values)
			{
				ServerMessage Message = new ServerMessage(604u);
				Message.AppendUInt(class2.PetId);
				this.GetClient().SendMessage(Message);
			}
			this.hashtable_0.Clear();
		}
		public void method_3(bool bool_0)
		{
			if (bool_0)
			{
				this.method_8();
			}
			this.GetClient().SendMessage(this.method_15());
		}
		public Pet method_4(uint uint_1)
		{
			return this.hashtable_0[uint_1] as Pet;
		}
		public bool method_5(uint uint_1)
		{
			ServerMessage Message = new ServerMessage(604u);
			Message.AppendUInt(uint_1);
			this.GetClient().SendMessage(Message);
			this.hashtable_0.Remove(uint_1);
			return true;
		}
		public void method_6(uint uint_1, uint uint_2)
		{
			this.method_5(uint_1);
		}
		public void method_7(Pet class15_0)
		{
			try
			{
				if (class15_0 != null)
				{
					class15_0.PlacedInRoom = false;
					if (!this.hashtable_0.ContainsKey(class15_0.PetId))
					{
						this.hashtable_0.Add(class15_0.PetId, class15_0);
					}
					ServerMessage Message5_ = new ServerMessage(603u);
					class15_0.SerializeInventory(Message5_);
					this.GetClient().SendMessage(Message5_);
				}
			}
			catch
			{
			}
		}
		public void method_8()
		{
			using (TimedLock.Lock(this.list_0))
			{
				this.list_0.Clear();
				this.hashtable_1.Clear();
				this.list_1.Clear();
				DataTable dataTable;
				using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
				{
					dataTable = @class.ReadDataTable("SELECT Id,base_item,extra_data,user_id FROM items WHERE room_id = 0 AND user_id = " + this.uint_0);
				}
				if (dataTable != null)
				{
					foreach (DataRow dataRow in dataTable.Rows)
					{
						this.list_0.Add(new UserItem((uint)dataRow["Id"], (uint)dataRow["base_item"], (string)dataRow["extra_data"]));
					}
				}
				using (TimedLock.Lock(this.hashtable_0))
				{
					this.hashtable_0.Clear();
					DataTable dataTable2;
					using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
					{
						@class.AddParamWithValue("userid", this.uint_0);
						dataTable2 = @class.ReadDataTable("SELECT Id, user_id, room_id, name, type, race, color, expirience, energy, nutrition, respect, createstamp, x, y, z FROM user_pets WHERE user_id = @userid AND room_id <= 0");
					}
					if (dataTable2 != null)
					{
						foreach (DataRow dataRow in dataTable2.Rows)
						{
							Pet class2 = GoldTree.GetGame().GetCatalog().method_12(dataRow);
							this.hashtable_0.Add(class2.PetId, class2);
						}
					}
				}
			}
		}
		public void method_9(bool bool_0)
		{
			if (bool_0)
			{
				this.method_8();
				this.method_18();
			}
			if (this.GetClient() != null)
			{
				this.GetClient().SendMessage(new ServerMessage(101u));
			}
		}
		public UserItem method_10(uint uint_1)
		{
			List<UserItem>.Enumerator enumerator = this.list_0.GetEnumerator();
			UserItem result;
			while (enumerator.MoveNext())
			{
				UserItem current = enumerator.Current;
				if (current.uint_0 == uint_1)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}
		public void method_11(uint uint_1, uint uint_2, string string_0, bool bool_0)
		{
			UserItem item = new UserItem(uint_1, uint_2, string_0);
			this.list_0.Add(item);
			if (this.list_1.Contains(uint_1))
			{
				this.list_1.Remove(uint_1);
			}
			if (!this.hashtable_1.ContainsKey(uint_1))
			{
				if (bool_0)
				{
					using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
					{
						@class.AddParamWithValue("extra_data", string_0);
						@class.ExecuteQuery(string.Concat(new object[]
						{
							"INSERT INTO items (Id,user_id,base_item,extra_data,wall_pos) VALUES ('",
							uint_1,
							"','",
							this.uint_0,
							"','",
							uint_2,
							"',@extra_data, '')"
						}));
						return;
					}
				}

                if (item.method_1().InteractionType == "musicdisc")
                {
                    if (this.discs.ContainsKey(item.uint_0))
                    {
                        this.discs.Add(item.uint_0, item);
                    }
                }

				using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
				{
					@class.ExecuteQuery(string.Concat(new object[]
					{
						"UPDATE items SET room_id = 0, user_id = '",
						this.uint_0,
						"' WHERE Id = '",
						uint_1,
						"'"
					}));
				}
			}
		}
        public void method_12(uint uint_1, uint uint_2, bool bool_0)
        {
            if (this != null && this.GetClient() != null)
            {
                ServerMessage Message = new ServerMessage(99u);
                Message.AppendUInt(uint_1);
                this.GetClient().SendMessage(Message);
                if (this.hashtable_1.ContainsKey(uint_1))
                {
                    this.hashtable_1.Remove(uint_1);
                }
                if (!this.list_1.Contains(uint_1))
                {
                    this.list_0.Remove(this.method_10(uint_1));
                    this.list_1.Add(uint_1);
                    this.discs.Remove(uint_1);
                    if (bool_0)
                    {
                        using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
                        {
                            @class.ExecuteQuery(string.Concat(new object[]
						{
							"UPDATE items SET user_id = '",
							uint_2,
							"' WHERE Id = '",
							uint_1,
							"' LIMIT 1"
						}));
                            return;
                        }
                    }
                    if (uint_2 == 0u && !bool_0)
                    {
                        using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
                        {
                            @class.ExecuteQuery("DELETE FROM items WHERE Id = '" + uint_1 + "' LIMIT 1");
                        }
                    }
                }
            }
        }
		public ServerMessage method_13()
		{
			ServerMessage Message = new ServerMessage(140u);
			Message.AppendStringWithBreak("S");
			Message.AppendInt32(1);
			Message.AppendInt32(1);
			Message.AppendInt32(this.Int32_0);
			List<UserItem>.Enumerator enumerator = this.list_0.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.method_0(Message, true);
			}
			return Message;
		}
		public ServerMessage method_14()
		{
			ServerMessage Message = new ServerMessage(140u);
			Message.AppendStringWithBreak("I");
			Message.AppendString("II");
			Message.AppendInt32(0);
			return Message;
		}
		public ServerMessage method_15()
		{
			ServerMessage Message = new ServerMessage(600u);
			Message.AppendInt32(this.hashtable_0.Count);
			foreach (Pet @class in this.hashtable_0.Values)
			{
				@class.SerializeInventory(Message);
			}
			return Message;
		}
		private GameClient GetClient()
		{
			return GoldTree.GetGame().GetClientManager().method_2(this.uint_0);
		}
		public void method_17(List<RoomItem> list_2)
		{
			foreach (RoomItem current in list_2)
			{
				this.method_11(current.uint_0, current.uint_2, current.ExtraData, false);
			}
		}
		internal void method_18()
		{
			using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
			{
				this.method_19(@class, false);
			}
		}
		internal void method_19(DatabaseClient class6_0, bool bool_0)
		{
			try
			{
				if (this.list_1.Count > 0 || this.hashtable_1.Count > 0 || this.hashtable_0.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (Pet @class in this.hashtable_0.Values)
					{
						if (@class.DBState == DatabaseUpdateState.NeedsInsert)
						{
							class6_0.AddParamWithValue("petname" + @class.PetId, @class.Name);
							class6_0.AddParamWithValue("petcolor" + @class.PetId, @class.Color);
							class6_0.AddParamWithValue("petrace" + @class.PetId, @class.Race);
							stringBuilder.Append(string.Concat(new object[]
							{
								"INSERT INTO `user_pets` VALUES ('",
								@class.PetId,
								"', '",
								@class.OwnerId,
								"', '",
								@class.RoomId,
								"', @petname",
								@class.PetId,
								", @petrace",
								@class.PetId,
								", @petcolor",
								@class.PetId,
								", '",
								@class.Type,
								"', '",
								@class.Expirience,
								"', '",
								@class.Energy,
								"', '",
								@class.Nutrition,
								"', '",
								@class.Respect,
								"', '",
								@class.CreationStamp,
								"', '",
								@class.X,
								"', '",
								@class.Y,
								"', '",
								@class.Z,
								"');"
							}));
						}
						else
						{
							if (@class.DBState == DatabaseUpdateState.NeedsUpdate)
							{
								stringBuilder.Append(string.Concat(new object[]
								{
									"UPDATE user_pets SET room_id = '",
									@class.RoomId,
									"', expirience = '",
									@class.Expirience,
									"', energy = '",
									@class.Energy,
									"', nutrition = '",
									@class.Nutrition,
									"', respect = '",
									@class.Respect,
									"', x = '",
									@class.X,
									"', y = '",
									@class.Y,
									"', z = '",
									@class.Z,
									"' WHERE Id = '",
									@class.PetId,
									"' LIMIT 1; "
								}));
							}
						}
						@class.DBState = DatabaseUpdateState.Updated;
					}
					if (stringBuilder.Length > 0)
					{
						class6_0.ExecuteQuery(stringBuilder.ToString());
					}
				}
				if (bool_0)
				{
					Console.WriteLine("Inventory for user: " + this.GetClient().GetHabbo().Username + " saved.");
				}
			}
			catch (Exception ex)
			{
                Logging.LogCacheError("FATAL ERROR DURING DB UPDATE: " + ex.ToString());
			}
		}

        internal Hashtable songDisks
        {
            get
            {
                return this.discs;
            }
        }

        public void RedeemPixel(GameClient class16_1)
        {
            int num = 0;
            List<UserItem> list = new List<UserItem>();
            foreach (UserItem current in this.list_0)
            {
                if (current != null && (current.method_1().Name.StartsWith("PixEx_")))
                {
                    string[] array = current.method_1().Name.Split(new char[]
					{
						'_'
					});
                    int num2 = int.Parse(array[1]);
                    if (!this.list_1.Contains(current.uint_0))
                    {
                        if (num2 > 0)
                        {
                            num += num2;
                        }
                        list.Add(current);
                    }
                }
            }
            foreach (UserItem current in list)
            {
                this.method_12(current.uint_0, 0u, false);
            }
            class16_1.GetHabbo().ActivityPoints += num;
            class16_1.GetHabbo().method_15(true);
            class16_1.SendNotif("All pixels in your inventory have been converted back into " + num + " pixels!");
        }

        public void RedeemShell(GameClient class16_1)
        {
            int num = 0;
            List<UserItem> list = new List<UserItem>();
            foreach (UserItem current in this.list_0)
            {
                if (current != null && (current.method_1().Name.StartsWith("PntEx_")))
                {
                    string[] array = current.method_1().Name.Split(new char[]
					{
						'_'
					});
                    int num2 = int.Parse(array[1]);
                    if (!this.list_1.Contains(current.uint_0))
                    {
                        if (num2 > 0)
                        {
                            num += num2;
                        }
                        list.Add(current);
                    }
                }
            }
            foreach (UserItem current in list)
            {
                this.method_12(current.uint_0, 0u, false);
            }
            class16_1.GetHabbo().VipPoints += num;
            class16_1.GetHabbo().method_14(false, true);
            class16_1.SendNotif("All shells in your inventory have been converted back into " + num + " shells!");
        }
	}
}
