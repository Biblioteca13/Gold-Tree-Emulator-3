using System;
using GoldTree.HabboHotel.GameClients;
using GoldTree.Messages;
namespace GoldTree.Communication.Messages.Inventory.AvatarFX
{
	internal sealed class AvatarEffectSelectedEvent : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
			Session.GetHabbo().method_24().method_2(Event.PopWiredInt32(), false);
		}
	}
}
