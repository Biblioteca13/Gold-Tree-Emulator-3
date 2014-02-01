using System;
using GoldTree.HabboHotel.GameClients;
using GoldTree.Messages;
using GoldTree.HabboHotel.Items;
using GoldTree.HabboHotel.Rooms;
namespace GoldTree.Communication.Messages.Rooms.Furniture
{
	internal sealed class UseFurnitureMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			try
			{
				Room @class = GoldTree.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
				if (@class != null)
				{
					RoomItem class2 = @class.method_28(Event.PopWiredUInt());
					if (class2 != null)
					{
						bool bool_ = false;
						if (@class.method_26(Session))
						{
							bool_ = true;
						}
						class2.Class69_0.OnTrigger(Session, class2, Event.PopWiredInt32(), bool_);
						if (Session.GetHabbo().CurrentQuestId == 12u)
						{
                            GoldTree.GetGame().GetQuestManager().ProgressUserQuest(12u, Session);
						}
						else
						{
							if (Session.GetHabbo().CurrentQuestId == 18u && class2.GetBaseItem().Name == "bw_lgchair")
							{
                                GoldTree.GetGame().GetQuestManager().ProgressUserQuest(18u, Session);
							}
							else
							{
								if (Session.GetHabbo().CurrentQuestId == 20u && class2.GetBaseItem().Name.Contains("bw_sboard"))
								{
                                    GoldTree.GetGame().GetQuestManager().ProgressUserQuest(20u, Session);
								}
								else
								{
									if (Session.GetHabbo().CurrentQuestId == 21u && class2.GetBaseItem().Name.Contains("bw_van"))
									{
                                        GoldTree.GetGame().GetQuestManager().ProgressUserQuest(21u, Session);
									}
									else
									{
										if (Session.GetHabbo().CurrentQuestId == 22u && class2.GetBaseItem().Name.Contains("party_floor"))
										{
                                            GoldTree.GetGame().GetQuestManager().ProgressUserQuest(22u, Session);
										}
										else
										{
											if (Session.GetHabbo().CurrentQuestId == 23u && class2.GetBaseItem().Name.Contains("party_ball"))
											{
                                                GoldTree.GetGame().GetQuestManager().ProgressUserQuest(23u, Session);
											}
											else
											{
                                                if (Session.GetHabbo().CurrentQuestId == 24u && class2.GetBaseItem().Name.Contains("jukebox"))
                                                {
                                                    GoldTree.GetGame().GetQuestManager().ProgressUserQuest(24u, Session);
                                                }
											}
										}
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
	}
}
