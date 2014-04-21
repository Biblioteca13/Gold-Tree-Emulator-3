using GoldTree.HabboHotel.GameClients;
using GoldTree.HabboHotel.Items;
using GoldTree.HabboHotel.Rooms;
using GoldTree.HabboHotel.SoundMachine;
using GoldTree.Messages;
using GoldTree.Source.HabboHotel.SoundMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldTree.Communication.Messages.SoundMachine
{
    class AddNewJukeboxCD : Interface
    {
        public void Handle(GameClient session, ClientMessage message)
        {
            if (((session != null) && (session.GetHabbo() != null)) && (session.GetHabbo().CurrentRoom != null))
            {
                Room currentRoom = session.GetHabbo().CurrentRoom;
                
                if (currentRoom.CheckRights(session, true))
                {
                    RoomMusicController roomMusicController = currentRoom.GetRoomMusicController();

                    if (roomMusicController.PlaylistSize < roomMusicController.PlaylistCapacity)
                    {
                        int itemId = message.PopWiredInt32();
                        UserItem item = session.GetHabbo().GetInventoryComponent().GetItemById((uint)itemId);

                        if ((item != null) && (item.GetBaseItem().InteractionType == "musicdisc"))
                        {
                            SongItem diskItem = new SongItem(item);

                            if (roomMusicController.AddDisk(diskItem) >= 0)
                            {
                                //diskItem.SaveToDatabase((int)currentRoom.Id); // <-- old
                                diskItem.SaveToDatabase((int)roomMusicController.LinkedItemId); // <-- new

                                session.GetHabbo().GetInventoryComponent().ChangeItemOwner((uint)itemId, 0u, true);
                                session.GetHabbo().GetInventoryComponent().RefreshInventory(true);

                                session.SendMessage(JukeboxDiscksComposer.Compose(roomMusicController.PlaylistCapacity, roomMusicController.Playlist.Values.ToList<SongInstance>()));
                            }
                        }
                    }
                }
            }
        }
    }
}
