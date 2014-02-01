using System;
using GoldTree.HabboHotel.GameClients;
using GoldTree.Messages;
namespace GoldTree.Communication.Messages.Inventory.AvatarFX
{
	internal sealed class AvatarEffectActivatedEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.GetHabbo().method_24().method_3(Event.PopWiredInt32());
		}
	}
}
