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
        private Hashtable Discs;

		private List<UserItem> Items;

		private Hashtable Pets;

		public List<uint> RemovedItems;

		private GameClient Session;

		public uint UserId;

		public int ItemCount
		{
			get
			{
				return this.Items.Count;
			}
		}

		public int PetCount
		{
			get
			{
				return this.Pets.Count;
			}
		}

        public List<UserItem> GetItems() { return Items; }

        public Hashtable GetDiscs() { return Discs; }

		public InventoryComponent(uint userId, GameClient client, UserDataFactory userdata)
		{
			this.Session = client;
			this.UserId = userId;
       
			this.Items = new List<UserItem>();

			this.Pets = new Hashtable();

            this.Discs = new Hashtable();

			this.RemovedItems = new List<uint>();
			
			foreach (DataRow row in userdata.GetItems().Rows)
			{
                string str;

                uint id = Convert.ToUInt32(row["Id"]);
                uint baseItem = Convert.ToUInt32(row["base_item"]);

                if (!DBNull.Value.Equals(row["extra_data"]))
                    str = (string)row["extra_data"];
                else
                    str = string.Empty;

                UserItem item = new UserItem(id, baseItem, str);
                Items.Add(item);

                if (item.GetBaseItem().InteractionType == "musicdisc")
                    this.Discs.Add(item.Id, item);
			}

			foreach (DataRow row in userdata.GetPets().Rows)
			{
				Pet pet = GoldTree.GetGame().GetCatalog().method_12(row);
				this.Pets.Add(pet.PetId, pet);
			}
		}

		public void ClearInventory()
		{
			using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
			{
				dbClient.ExecuteQuery("DELETE FROM items WHERE room_id = 0 AND user_id = '" + this.UserId + "';");
			}

            this.Discs.Clear();
			this.RemovedItems.Clear();
			this.Items.Clear();

			ServerMessage Message5_ = new ServerMessage(101u);
			this.GetClient().SendMessage(Message5_);
		}

		public void ConvertCoinsToCredits()
		{
			int num = 0;

			List<UserItem> list = new List<UserItem>();

			foreach (UserItem item in Items)
			{
				if (item != null && (item.GetBaseItem().Name.StartsWith("CF_") || item.GetBaseItem().Name.StartsWith("CFC_")))
				{
					string[] array = item.GetBaseItem().Name.Split(new char[]
					{
						'_'
					});

					int num2 = int.Parse(array[1]);

					if (!this.RemovedItems.Contains(item.Id))
					{
						if (num2 > 0)
						{
							num += num2;
						}
						list.Add(item);
					}
				}
			}

			foreach (UserItem current in list)
			{
				this.ChangeItemOwner(current.Id, 0u, false);
			}

			Session.GetHabbo().Credits += num;
			Session.GetHabbo().UpdateCredits(true);

			Session.SendNotification("All coins in your inventory have been converted back into " + num + " credits!");
		}

		public void RemovePetsFromInventory()
		{
			using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
			{
				dbClient.ExecuteQuery("DELETE FROM user_pets WHERE user_id = " + this.UserId + " AND room_id = 0;");
			}

			foreach (Pet pet in Pets.Values)
			{
				ServerMessage Message = new ServerMessage(604u);
				Message.AppendUInt(pet.PetId);
				this.GetClient().SendMessage(Message);
			}

			this.Pets.Clear();
		}

		public void method_3(bool bool_0)
		{
			if (bool_0)
				this.ReloadItems();

			this.GetClient().SendMessage(this.ComposePetInventoryListMessage());
		}

		public Pet GetPetById(uint petId)
		{
			return Pets[petId] as Pet;
		}

		public void RemovePetById(uint petId)
		{
			ServerMessage Message = new ServerMessage(604u);
			Message.AppendUInt(petId);
			this.GetClient().SendMessage(Message);

			this.Pets.Remove(petId);
		}

		public void AddPet(Pet pet)
		{
            try
            {
                if (pet != null)
                {
                    pet.PlacedInRoom = false;

                    if (!Pets.ContainsKey(pet.PetId))
                        Pets.Add(pet.PetId, pet);

                    ServerMessage message = new ServerMessage(603u);
                    pet.SerializeInventory(message);
                    this.GetClient().SendMessage(message);
                }
            }
            catch { }
		}

		public void ReloadItems()
		{
			using (TimedLock.Lock(this.Items))
			{
				Items.Clear();

				RemovedItems.Clear();

				DataTable dataTable;

				using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
				{
                    dataTable = dbClient.ReadDataTable("SELECT items.Id,items.base_item,items_extra_data.extra_data FROM items LEFT JOIN items_extra_data ON items_extra_data.item_id = items.Id WHERE room_id = 0 AND user_id = " + this.UserId);
				}

				if (dataTable != null)
				{
					foreach (DataRow row in dataTable.Rows)
					{
                        string extraData = (row["extra_data"] == DBNull.Value) ? string.Empty : (string)row["extra_data"];

                        if (extraData != null && !DBNull.Value.Equals(extraData) && !string.IsNullOrEmpty(extraData))
                            Items.Add(new UserItem((uint)row["Id"], (uint)row["base_item"], extraData));
                        else
                            Items.Add(new UserItem((uint)row["Id"], (uint)row["base_item"], ""));
					}
				}

				using (TimedLock.Lock(Pets))
				{
                    Pets.Clear();

					using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
					{
						dbClient.AddParamWithValue("userid", this.UserId);
						dataTable = dbClient.ReadDataTable("SELECT Id, user_id, room_id, name, type, race, color, expirience, energy, nutrition, respect, createstamp, x, y, z FROM user_pets WHERE user_id = @userid AND room_id <= 0");
					}

					if (dataTable != null)
					{
						foreach (DataRow row in dataTable.Rows)
						{
							Pet pet = GoldTree.GetGame().GetCatalog().method_12(row);

							this.Pets.Add(pet.PetId, pet);
						}
					}
				}
			}
		}

		public void RefreshInventory(bool reload)
		{
			if (reload)
			{
				this.ReloadItems();
				this.SavePets();
			}

			if (this.GetClient() != null)
				this.GetClient().SendMessage(new ServerMessage(101u));
		}

		public UserItem GetItemById(uint itemId)
		{
			List<UserItem>.Enumerator enumerator = this.Items.GetEnumerator();

			while (enumerator.MoveNext())
			{
				UserItem current = enumerator.Current;

				if (current.Id == itemId)
                    return current;
			}

            return null;
		}

		public void AddItem(uint itemId, uint baseItemId, string extraData, bool addToDatabase)
		{
			UserItem item = new UserItem(itemId, baseItemId, extraData);
			this.Items.Add(item);

			if (addToDatabase)
			{
				using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
				{
					dbClient.ExecuteQuery(string.Concat(new object[]
					{
						"INSERT INTO items (Id,user_id,base_item,wall_pos) VALUES ('",
						itemId,
						"','",
						this.UserId,
						"','",
						baseItemId,
						"', '')"
					}));

                    if (!string.IsNullOrEmpty(extraData))
                    {
                        dbClient.AddParamWithValue("extra_data", extraData);

                        dbClient.ExecuteQuery(string.Concat(new object[]
                        {
                            "DELETE FROM items_extra_data WHERE item_id = '" + itemId + "'; ",
                            "INSERT INTO items_extra_data (item_id,extra_data) VALUES ('" + itemId + "' , @extra_data); "
                        }));
                    }
                    else
                    {
                        dbClient.ExecuteQuery(string.Concat(new object[]
                        {
                            "DELETE FROM items_extra_data WHERE item_id = '" + itemId + "'; "
                        }));
                    }
				}

                if (item.GetBaseItem().InteractionType == "musicdisc")
                {
                    if (this.Discs.ContainsKey(item.Id))
                    {
                        this.Discs.Add(item.Id, item);
                    }
                }
			}
		}

        public void ChangeItemOwner(uint itemId, uint userId, bool updateDatabase)
        {
            if (this != null && this.GetClient() != null)
            {
                ServerMessage Message = new ServerMessage(99u);
                Message.AppendUInt(itemId);
                this.GetClient().SendMessage(Message);

                if (!this.RemovedItems.Contains(itemId))
                {
                    this.Items.Remove(this.GetItemById(itemId));
                    this.RemovedItems.Add(itemId);
                    this.Discs.Remove(itemId);

                    if (updateDatabase)
                    {
                        using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
                        {
                            dbClient.ExecuteQuery(string.Concat(new object[]
						    {
							    "UPDATE items SET user_id = '",
							    userId,
							    "' WHERE Id = '",
							    itemId,
							    "' LIMIT 1"
						    }));

                            return;
                        }
                    }

                    if (userId == 0u && !updateDatabase)
                    {
                        using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
                        {
                            dbClient.ExecuteQuery("DELETE FROM items WHERE Id = '" + itemId + "' LIMIT 1");
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

			Message.AppendInt32(this.ItemCount);

			List<UserItem>.Enumerator enumerator = this.Items.GetEnumerator();

			while (enumerator.MoveNext())
			{
				enumerator.Current.SerializeMessage(Message, true);
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

		public ServerMessage ComposePetInventoryListMessage()
		{
			ServerMessage Message = new ServerMessage(600u);

			Message.AppendInt32(Pets.Count);

			foreach (Pet pet in Pets.Values)
			{
				pet.SerializeInventory(Message);
			}

			return Message;
		}

		private GameClient GetClient()
		{
			return GoldTree.GetGame().GetClientManager().method_2(this.UserId);
		}

		public void method_17(List<RoomItem> list_2)
		{
			foreach (RoomItem current in list_2)
			{
				this.AddItem(current.uint_0, current.BaseItemId, current.ExtraData, false);
			}
		}

		internal void SavePets()
		{
			using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
			{
				this.SavePets(dbClient, false);
			}
		}

        internal void SavePets(DatabaseClient dbClient, bool consoleOutput)
        {
            try
            {
                if (this.RemovedItems.Count > 0 || this.Pets.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    foreach (Pet pet in Pets.Values)
                    {
                        if (pet.DBState == DatabaseUpdateState.NeedsInsert)
                        {
                            dbClient.AddParamWithValue("petname" + pet.PetId, pet.Name);
                            dbClient.AddParamWithValue("petcolor" + pet.PetId, pet.Color);
                            dbClient.AddParamWithValue("petrace" + pet.PetId, pet.Race);

                            stringBuilder.Append(string.Concat(new object[]
							{
								"INSERT INTO `user_pets` VALUES ('",
								pet.PetId,
								"', '",
								pet.OwnerId,
								"', '",
								pet.RoomId,
								"', @petname",
								pet.PetId,
								", @petrace",
								pet.PetId,
								", @petcolor",
								pet.PetId,
								", '",
								pet.Type,
								"', '",
								pet.Expirience,
								"', '",
								pet.Energy,
								"', '",
								pet.Nutrition,
								"', '",
								pet.Respect,
								"', '",
								pet.CreationStamp,
								"', '",
								pet.X,
								"', '",
								pet.Y,
								"', '",
								pet.Z,
								"');"
							}));
                        }
                        else
                        {
                            if (pet.DBState == DatabaseUpdateState.NeedsUpdate)
                            {
                                stringBuilder.Append(string.Concat(new object[]
								{
									"UPDATE user_pets SET room_id = '",
									pet.RoomId,
									"', expirience = '",
									pet.Expirience,
									"', energy = '",
									pet.Energy,
									"', nutrition = '",
									pet.Nutrition,
									"', respect = '",
									pet.Respect,
									"', x = '",
									pet.X,
									"', y = '",
									pet.Y,
									"', z = '",
									pet.Z,
									"' WHERE Id = '",
									pet.PetId,
									"' LIMIT 1; "
								}));
                            }
                        }

                        pet.DBState = DatabaseUpdateState.Updated;
                    }

                    if (stringBuilder.Length > 0)
                    {
                        dbClient.ExecuteQuery(stringBuilder.ToString());
                    }
                }

                if (consoleOutput)
                {
                    Console.WriteLine("Inventory for user: " + this.GetClient().GetHabbo().Username + " saved.");
                }
            }
            catch (Exception ex)
            {
                Logging.LogCacheError("FATAL ERROR DURING DB UPDATE: " + ex.ToString());
            }
        }

        public void RedeemPixel(GameClient client)
        {
            int num = 0;

            List<UserItem> list = new List<UserItem>();

            foreach (UserItem current in this.Items)
            {
                if (current != null && (current.GetBaseItem().Name.StartsWith("PixEx_")))
                {
                    string[] array = current.GetBaseItem().Name.Split(new char[]
					{
						'_'
					});

                    int num2 = int.Parse(array[1]);

                    if (!this.RemovedItems.Contains(current.Id))
                    {
                        if (num2 > 0)
                            num += num2;

                        list.Add(current);
                    }
                }
            }

            foreach (UserItem current in list)
            {
                this.ChangeItemOwner(current.Id, 0u, false);
            }

            client.GetHabbo().ActivityPoints += num;
            client.GetHabbo().UpdateActivityPoints(true);

            client.SendNotification("All pixels in your inventory have been converted back into " + num + " pixels!");
        }

        public void RedeemShell(GameClient client)
        {
            int num = 0;

            List<UserItem> list = new List<UserItem>();

            foreach (UserItem current in this.Items)
            {
                if (current != null && (current.GetBaseItem().Name.StartsWith("PntEx_")))
                {
                    string[] array = current.GetBaseItem().Name.Split(new char[]
					{
						'_'
					});

                    int num2 = int.Parse(array[1]);

                    if (!this.RemovedItems.Contains(current.Id))
                    {
                        if (num2 > 0)
                            num += num2;

                        list.Add(current);
                    }
                }
            }

            foreach (UserItem current in list)
            {
                this.ChangeItemOwner(current.Id, 0u, false);
            }

            client.GetHabbo().VipPoints += num;
            client.GetHabbo().UpdateVipPoints(false, true);

            client.SendNotification("All shells in your inventory have been converted back into " + num + " shells!");
        }
	}
}
