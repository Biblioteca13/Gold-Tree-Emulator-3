using System;
using GoldTree.HabboHotel.GameClients;
using GoldTree.Messages;
namespace GoldTree.Communication.Messages.Messenger
{
	internal sealed class SetEventStreamingAllowedEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			ServerMessage Message = new ServerMessage(950u);
			Message.AppendInt32(1);
			Message.AppendUInt(1u);
			Message.AppendInt32(2);
			Message.AppendStringWithBreak(Session.GetHabbo().Id.ToString());
			Message.AppendStringWithBreak(Session.GetHabbo().Username);
			Message.AppendStringWithBreak(Session.GetHabbo().Gender.ToLower());
			Message.AppendStringWithBreak("http://habboon.com/images/avatar_head.cfm?figure=");
			Message.AppendInt32(0);
			Message.AppendInt32(1);
			Message.AppendStringWithBreak("");
			Message.AppendStringWithBreak("");
			Session.SendMessage(Message);
		}
	}
}
