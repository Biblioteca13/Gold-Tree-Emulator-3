using System;
using GoldTree.HabboHotel.GameClients;
using GoldTree.Messages;
namespace GoldTree.Communication.Messages.Messenger
{
	internal sealed class RequestBuddyMessageEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			if (Session.GetHabbo().GetMessenger() != null)
			{
				if (Session.GetHabbo().CurrentQuestId == 4u)
				{
                    GoldTree.GetGame().GetQuestManager().ProgressUserQuest(4u, Session);
				}
				Session.GetHabbo().GetMessenger().method_16(Event.PopFixedString());
			}
		}
	}
}
