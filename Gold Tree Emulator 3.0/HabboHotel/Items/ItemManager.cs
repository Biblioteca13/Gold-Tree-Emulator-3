using System;
using System.Collections.Generic;
using System.Data;

using GoldTree.Core;
using GoldTree.Storage;
using GoldTree.HabboHotel.SoundMachine;

namespace GoldTree.HabboHotel.Items
{
	internal sealed class ItemManager
	{
		private Dictionary<uint, Item> BaseItems;

		public ItemManager()
		{
			this.BaseItems = new Dictionary<uint, Item>();
		}

		public void Initialise(DatabaseClient dbClient)
		{
            Logging.Write("Loading Items..");

			this.BaseItems = new Dictionary<uint, Item>();

			DataTable dataTable = dbClient.ReadDataTable("SELECT * FROM furniture;");

			if (dataTable != null)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					try
					{
                        this.BaseItems.Add((uint)row["Id"], new Item((uint)row["Id"], (int)row["sprite_id"], (string)row["public_name"], (string)row["item_name"], (string)row["type"], (int)row["width"], (int)row["length"], (double)row["stack_height"], GoldTree.StringToBoolean(row["can_stack"].ToString()), GoldTree.StringToBoolean(row["is_walkable"].ToString()), GoldTree.StringToBoolean(row["can_sit"].ToString()), GoldTree.StringToBoolean(row["allow_recycle"].ToString()), GoldTree.StringToBoolean(row["allow_trade"].ToString()), GoldTree.StringToBoolean(row["allow_marketplace_sell"].ToString()), GoldTree.StringToBoolean(row["allow_gift"].ToString()), GoldTree.StringToBoolean(row["allow_inventory_stack"].ToString()), (string)row["interaction_type"], (int)row["interaction_modes_count"], (string)row["vending_ids"], row["height_adjustable"].ToString(), Convert.ToByte((int)row["EffectF"]), Convert.ToByte((int)row["EffectM"]), GoldTree.StringToBoolean(row["HeightOverride"].ToString())));
					}
					catch (Exception e)
					{
						Logging.WriteLine("Could not load item #" + (uint)row["Id"] + ", please verify the data is okay.");
                        Logging.LogItemError(e.Message);
					}
				}
			}

			Logging.WriteLine("completed!", ConsoleColor.Green);
			/*Logging.smethod_0("Loading Soundtracks.."); //OMA LUOTU :3
			this.dictionary_1 = new Dictionary<int, Soundtrack>();
			DataTable dataTable2 = class6_0.ReadDataTable("SELECT * FROM soundtracks;");
			if (dataTable2 != null)
			{
				foreach (DataRow dataRow in dataTable2.Rows)
				{
					try
					{
						this.dictionary_1.Add((int)dataRow["Id"], new Soundtrack((int)dataRow["Id"], (string)dataRow["name"], (string)dataRow["author"], (string)dataRow["track"], (int)dataRow["length"]));
					}
					catch (Exception)
					{
						Logging.WriteLine("Could not load item #" + (uint)dataRow["Id"] + ", please verify the data is okay.");
					}
				}
			}
			Logging.WriteLine("completed!", ConsoleColor.Green);*/

            Logging.Write("Loading Soundtracks..");
            SongManager.Initialize();
            Logging.WriteLine("completed!", ConsoleColor.Green);
		}

		public bool BaseItemExists(uint itemID)
		{
			return BaseItems.ContainsKey(itemID);
		}

		public Item GetBaseItemById(uint itemId)
		{
            if (this.BaseItemExists(itemId))
                return BaseItems[itemId];

            return null;
		}

		/*public bool method_3(int int_0)
		{
			return this.dictionary_1.ContainsKey(int_0);
		}
		public Soundtrack method_4(int int_0)
		{
			Soundtrack result;
			if (this.method_3(int_0))
			{
				result = this.dictionary_1[int_0];
			}
			else
			{
				result = null;
			}
			return result;
		}*/
	}
}
