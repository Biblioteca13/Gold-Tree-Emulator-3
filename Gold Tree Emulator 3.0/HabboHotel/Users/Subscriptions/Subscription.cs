using System;
namespace GoldTree.HabboHotel.Users.Subscriptions
{
	internal sealed class Subscription
	{
		private string Type;
		private int Months;
		private int ItemsSelected;
		public string String_0
		{
			get
			{
				return this.Type;
			}
		}
		public int Int32_0
		{
			get
			{
				return this.ItemsSelected;
			}
		}
        public int Int32_1
        {
            get
            {
                return this.Months;
            }
        }
		public Subscription(string mType, int mMonths, int mItemsSelected)
		{
			this.Type = mType;
			this.Months = mMonths;
			this.ItemsSelected = mItemsSelected;
		}
		public bool method_0()
		{
			return (double)this.ItemsSelected > GoldTree.GetUnixTimestamp();
		}
		public void method_1(int int_2)
		{
			if (this.ItemsSelected + int_2 < 2147483647)
			{
				this.ItemsSelected += int_2;
			}
		}
	}
}
