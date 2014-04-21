using GoldTree.HabboHotel.GameClients;
using GoldTree.HabboHotel.Rooms;
using GoldTree.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace GoldTree.HabboHotel.Items.Interactors
{
    class InteractorFreezeTile : FurniInteractor
    {
        public override void OnPlace(GameClient Session, RoomItem Item)
        {
            Item.ExtraData = "0";
            Item.UpdateState(true, true);
        }

        public override void OnRemove(GameClient Session, RoomItem Item)
        {
            Item.ExtraData = "0";
            Item.UpdateState(true, true);
        }

        public async override void OnTrigger(GameClient Session, RoomItem Item, int Request, bool UserHasRights)
        {
            try
            {

                if (Session == null || Session.GetHabbo() == null)
                {
                    return;
                }

                if (!(Item.ExtraData == "11200" || string.IsNullOrEmpty(Item.ExtraData)))
                {
                    return;
                }

                if (Item == null || Item.GetRoom() == null)
                {
                    return;
                }

                Room @class = Item.GetRoom();

                if (@class == null)
                {
                    return;
                }

                RoomUser User = @class.GetRoomUserByHabbo(Session.GetHabbo().Id);

                if (User.Freezed == false)
                {
                    if (User.team != Rooms.Games.Team.None)
                    {
                        if (Item.GetRoom().frzTimer == true)
                        {
                            if (Item.X == User.int_3 || Item.X - 1 == User.int_3 || Item.X + 1 == User.int_3)
                            {
                                if (Item.Y == User.int_4 || Item.Y - 1 == User.int_4 || Item.Y + 1 == User.int_4)
                                {
                                    if (User.FreezeBalls > 0)
                                    {
                                        Rooms.Games.FreezePowerUp BallType = User.freezePowerUp;
                                        User.freezePowerUp = Rooms.Games.FreezePowerUp.None;

                                        bool pX, pY, pD1, pD2, nX, nY, nD1, nD2;
                                        pX = false; pY = false; pD1 = false; pD2 = false; nX = false; nY = false; nD1 = false; nD2 = false;

                                        if (BallType == Rooms.Games.FreezePowerUp.OrangeSnowball)
                                        {
                                            User.FreezeBalls -= 1;
                                            Item.ExtraData = "6000";
                                            Item.UpdateState(false, true);
                                            await Task.Delay(2000);
                                            BreakIceBlock(Item, Item);
                                            FreezeUser(Item, Item);
                                        }
                                        else
                                        {
                                            User.FreezeBalls -= 1;
                                            Item.ExtraData = "1000";
                                            Item.UpdateState(false, true);
                                            await Task.Delay(2000);
                                            BreakIceBlock(Item, Item);
                                            FreezeUser(Item, Item);
                                        }

                                        if (BallType == Rooms.Games.FreezePowerUp.None)
                                        {
                                            BallType = Rooms.Games.FreezePowerUp.None;
                                            for (int i = 1; i < 20; i++)
                                            {
                                                if (User.FreezeRange >= i)
                                                {
                                                    await Task.Delay(200);
                                                    foreach (RoomItem Item2 in Item.GetRoom().GetFreeze().freezeTiles.Values)
                                                    {
                                                        if (Item2.X == Item.X && Item2.Y == Item.Y + i && !pX) { pX = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X && Item2.Y == Item.Y - i && !pY) { pY = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X + i && Item2.Y == Item.Y && !nX) { nX = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X - i && Item2.Y == Item.Y && !nY) { nY = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                    }
                                                }
                                            }
                                        }

                                        else if (BallType == Rooms.Games.FreezePowerUp.GreenArrow)
                                        {
                                            BallType = Rooms.Games.FreezePowerUp.None;
                                            for (int i = 1; i < 20; i++)
                                            {
                                                if (User.FreezeRange >= i)
                                                {
                                                    await Task.Delay(200);
                                                    foreach (RoomItem Item2 in Item.GetRoom().GetFreeze().freezeTiles.Values)
                                                    {
                                                        if (Item2.X == Item.X + i && Item2.Y == Item.Y + i && !pD1) { pD1 = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X + i && Item2.Y == Item.Y - i && !nD1) { nD1 = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X - i && Item2.Y == Item.Y + i && !pD2) { pD2 = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X - i && Item2.Y == Item.Y - i && !nD2) { nD2 = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                    }
                                                }
                                            }
                                        }

                                        else if (BallType == Rooms.Games.FreezePowerUp.OrangeSnowball)
                                        {
                                            BallType = Rooms.Games.FreezePowerUp.None;
                                            for (int i = 1; i < 20; i++)
                                            {
                                                if (User.FreezeRange >= i)
                                                {
                                                    await Task.Delay(200);
                                                    foreach (RoomItem Item2 in Item.GetRoom().GetFreeze().freezeTiles.Values)
                                                    {
                                                        if (Item2.X == Item.X && Item2.Y == Item.Y + i && !pX) { pX = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X && Item2.Y == Item.Y - i && !pY) { pY = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X + i && Item2.Y == Item.Y && !nX) { nX = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X - i && Item2.Y == Item.Y && !nY) { nY = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X + i && Item2.Y == Item.Y + i && !pD1) { pD1 = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X + i && Item2.Y == Item.Y - i && !nD1) { nD1 = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X - i && Item2.Y == Item.Y + i && !pD2) { pD2 = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                        if (Item2.X == Item.X - i && Item2.Y == Item.Y - i && !nD2) { nD2 = BreakIceBlock(Item, Item2); FreezeUser(Item, Item2); }
                                                    }
                                                }
                                            }
                                        }

                                        User.FreezeBalls += 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            catch
            {
            }
        }

        public bool BreakIceBlock(RoomItem Item, RoomItem Item2)
        {
            if (Item.GetRoom().frzTimer == true)
            {
                Item2.ExtraData = "11200";
                Item2.UpdateState(false, true);

                foreach (RoomItem Item3 in Item.GetRoom().GetFreeze().freezeBlocks.Values)
                {
                    if (Item2.X == Item3.X && Item2.Y == Item3.Y)
                    {
                        if (string.IsNullOrEmpty(Item3.ExtraData))
                        {
                            Item3.GetRoom().GetFreeze().SetRandomPowerUp(Item3);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void FreezeUser(RoomItem Item, RoomItem Item2)
        {
            if (Item.GetRoom().frzTimer == true)
            {
                for (int i = 0; i < Item.GetRoom().RoomUsers.Length; i++)
                {
                    RoomUser User2 = Item.GetRoom().RoomUsers[i];
                    if (User2 != null)
                    {
                        if (User2.int_3 == Item2.X && User2.int_4 == Item2.Y)
                        {
                            Item.GetRoom().GetFreeze().FreezeUser(User2);
                        }
                    }
                }
            }
        }
    }
}