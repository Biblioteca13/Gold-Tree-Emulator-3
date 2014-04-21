using System;
using GoldTree.Core;
using GoldTree.Messages;
namespace GoldTree.HabboHotel.Items
{
	internal sealed class UserItem
	{
		internal uint Id;
		internal uint BaseItem;

		internal string ExtraData;

		private Item Item;

		internal UserItem(uint id, uint baseItem, string extraData)
		{
			this.Id = id;
			this.BaseItem = baseItem;
			this.ExtraData = extraData;
			this.Item = this.GetBaseItem();
		}

		internal void SerializeMessage(ServerMessage Message, bool bool_0)
		{
			if (this.Item == null)
			{
                Logging.LogException("Unknown base: " + this.BaseItem);
			}

			Message.AppendUInt(this.Id);

			Message.AppendStringWithBreak(this.Item.Type.ToString().ToUpper());

			Message.AppendUInt(this.Id);

			Message.AppendInt32(this.Item.Sprite);

			if (this.Item.Name.Contains("a2 "))
			{
				Message.AppendInt32(3);
			}
			else
			{
				if (this.Item.Name.Contains("wallpaper"))
				{
					Message.AppendInt32(2);
				}
				else
				{
					if (this.Item.Name.Contains("landscape"))
					{
						Message.AppendInt32(4);
					}
					else
					{
						if (this.GetBaseItem().Name == "poster")
						{
							Message.AppendInt32(6);
						}
						else
						{
							if (this.GetBaseItem().Name == "song_disk")
							{
								Message.AppendInt32(8);
							}
							else
							{
								Message.AppendInt32(1);
							}
						}
					}
				}
			}
			if (this.GetBaseItem().Name == "song_disk")
			{
				Message.AppendInt32(0);
				Message.AppendStringWithBreak("");
			}
			else
			{
				if (this.GetBaseItem().Name.StartsWith("poster_"))
				{
					Message.AppendStringWithBreak(this.GetBaseItem().Name.Split(new char[]
					{
						'_'
					})[1]);
				}
				else
				{
					Message.AppendInt32(0);
					Message.AppendStringWithBreak(this.ExtraData);
				}
			}

			Message.AppendBoolean(this.Item.AllowRecycle);
			Message.AppendBoolean(this.Item.AllowTrade);
			Message.AppendBoolean(this.Item.AllowInventoryStack);

			Message.AppendBoolean(GoldTree.GetGame().GetCatalog().method_22().method_0(this));

			Message.AppendInt32(-1);

			if (this.Item.Type == 's')
			{
				Message.AppendStringWithBreak("");
				if (this.GetBaseItem().Name == "song_disk" && this.ExtraData.Length > 0)
				{
					Message.AppendInt32(Convert.ToInt32(this.ExtraData));
				}
				else
				{
					Message.AppendInt32(0);
				}
			}
		}

		internal Item GetBaseItem()
		{
			return GoldTree.GetGame().GetItemManager().GetBaseItemById(this.BaseItem);
		}
	}
}
