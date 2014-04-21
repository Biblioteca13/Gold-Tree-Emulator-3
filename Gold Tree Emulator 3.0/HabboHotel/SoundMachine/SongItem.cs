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
        public Item BaseItem;

        public uint ItemId;
        public int SongId;

        public SongItem(UserItem item)
        {
            this.ItemId = item.Id;
            this.SongId = Convert.ToInt32(item.ExtraData);

            this.BaseItem = item.GetBaseItem();
        }

        public SongItem(uint itemID, int songID, int baseItem)
        {
            this.ItemId = itemID;
            this.SongId = songID;

            this.BaseItem = GoldTree.GetGame().GetItemManager().GetBaseItemById((uint)baseItem);
        }

        public void RemoveFromDatabase()
        {
            using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
            {
                dbClient.ExecuteQuery("DELETE FROM items_rooms_songs WHERE itemid = " + ItemId); // <-- old
                dbClient.ExecuteQuery("DELETE FROM items_jukebox_songs WHERE itemid = " + ItemId); // <-- new
                //@class.ExecuteQuery(string.Concat(new object[] { "INSERT INTO items (id, base_item) VALUES ('", itemID, "','", baseItem.UInt32_0, "')" }));
            }
        }

        //public void SaveToDatabase(int roomID) // <-- old
        public void SaveToDatabase(int jukeboxId) // <-- new
        {
            using (DatabaseClient dbClient = GoldTree.GetDatabase().GetClient())
            {
                //@class.ExecuteQuery(string.Concat(new object[] { "INSERT INTO items_rooms_songs VALUES (", itemID, ",", roomID, ",", this.songID, ",", this.baseItem.UInt32_0, ")" })); // <-- old
                dbClient.ExecuteQuery(string.Concat(new object[] { "INSERT INTO items_jukebox_songs VALUES (", ItemId, ",", jukeboxId, ",", this.SongId, ",", this.BaseItem.UInt32_0, ")" })); // <-- new
               //@class.ExecuteQuery("DELETE FROM items WHERE id = '" + itemID + "'");
            }
        }
    }
}
