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
    class RemoveCDToJukebox : Interface
    {
        public void Handle(GameClient Session, ClientMessage Event)
        {
            if (((Session != null) && (Session.GetHabbo() != null)) && (Session.GetHabbo().CurrentRoom != null))
            {
                Room currentRoom = Session.GetHabbo().CurrentRoom;

                if (currentRoom.CheckRights(Session, true) && currentRoom.GotMusicController())
                {
                    RoomMusicController roomMusicController = currentRoom.GetRoomMusicController();
                    SongItem item = roomMusicController.RemoveDisk(Event.PopWiredInt32());

                    if (item != null)
                    {
                        Session.GetHabbo().GetInventoryComponent().ChangeItemOwner(item.ItemId, Session.GetHabbo().Id, true);
                        Session.GetHabbo().GetInventoryComponent().RefreshInventory(true);

                        Session.SendMessage(JukeboxDiscksComposer.SerializeSongInventory(Session.GetHabbo().GetInventoryComponent().GetDiscs()));
                        Session.SendMessage(JukeboxDiscksComposer.Compose(roomMusicController.PlaylistCapacity, roomMusicController.Playlist.Values.ToList<SongInstance>()));
                    }
                }
            }
        }
    }
}
