using System;
namespace GoldTree.HabboHotel.Users.Badges
{
	internal sealed class Badge
	{
		public string Code;
		public int Slot;
		public Badge(string mCode, int mSlot)
		{
			this.Code = mCode;
			this.Slot = mSlot;
		}
	}
}
