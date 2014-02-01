using GoldTree.HabboHotel.Items;
using GoldTree.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldTree.Source.HabboHotel.SoundMachine
{
    class SongItem
    {
        public Item baseItem;
        public int itemID;
        public int songID;

        public SongItem(UserItem item)
        {
            this.itemID = (int)item.uint_0;
            this.songID = Convert.ToInt32(item.string_0);
            this.baseItem = item.method_1();
        }

        public SongItem(int itemID, int songID, int baseItem)
        {
            this.itemID = itemID;
            this.songID = songID;
            this.baseItem = GoldTree.GetGame().GetItemManager().method_2((uint)baseItem);
        }

        public void RemoveFromDatabase()
        {
            using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
            {
                @class.ExecuteQuery("DELETE FROM items_rooms_songs WHERE itemid = " + itemID);
                //@class.ExecuteQuery(string.Concat(new object[] { "INSERT INTO items (id, base_item) VALUES ('", itemID, "','", baseItem.UInt32_0, "')" }));
            }
        }

        public void SaveToDatabase(int roomID)
        {
            using (DatabaseClient @class = GoldTree.GetDatabase().GetClient())
            {
                @class.ExecuteQuery(string.Concat(new object[] { "INSERT INTO items_rooms_songs VALUES (", itemID, ",", roomID, ",", this.songID, ",", this.baseItem.UInt32_0, ")" }));
               //@class.ExecuteQuery("DELETE FROM items WHERE id = '" + itemID + "'");
            }
        }
    }
}
