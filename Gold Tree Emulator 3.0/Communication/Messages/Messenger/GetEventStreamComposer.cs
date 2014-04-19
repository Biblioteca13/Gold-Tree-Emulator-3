using System;
using GoldTree.HabboHotel.GameClients;
using GoldTree.Messages;
using GoldTree.HabboHotel.Rooms;
using System.Data;
namespace GoldTree.Communication.Messages.Messenger
{
    internal sealed class GetEventStreamComposer : Interface
	{
		public void Handle(GameClient Session, ClientMessage Event)
		{
            ServerMessage Message = new ServerMessage(950u);
            int StreamCount = 0;
            foreach (DataRow dRow in Session.GetHabbo().GetUserDataFactory().DataTable_12.Rows)
            {
                StreamCount = StreamCount + 1;
            }
            DataTable dataTable_ = Session.GetHabbo().GetUserDataFactory().DataTable_12;
            foreach (DataRow dataRow in dataTable_.Rows)
            {
                int type = (int)dataRow["type"];
                if (type == 1)
                {
                    DataRow[] DataRow_ = Session.GetHabbo().GetUserDataFactory().DataTable_8.Select("id = " + (uint)dataRow["userid"]);
                    uint userid = (uint)dataRow["userid"];
                    string username = (string)DataRow_[0]["username"];
                    string gender = (string)dataRow["gender"].ToString().ToLower();
                    string look = (string)dataRow["look"];
                    int time = (int)((GoldTree.GetUnixTimestamp() - (double)dataRow["time"]) / 60);
                    string data = (string)dataRow["data"];

                    Message.AppendInt32(StreamCount);
                    Message.AppendUInt(1u);
                    Message.AppendInt32(type);
                    Message.AppendStringWithBreak(userid.ToString());
                    Message.AppendStringWithBreak(username);
                    Message.AppendStringWithBreak(gender);
                    Message.AppendStringWithBreak("http://127.0.0.1/retro/r63/c_images/friendstream/index.gif?figure=" + look + ".gif");
                    Message.AppendInt32WithBreak(time);
                    Message.AppendInt32WithBreak(type + 1);

                    uint RoomID;
                    RoomData RoomData;
                    if (uint.TryParse(data, out RoomID))
                        RoomData = GoldTree.GetGame().GetRoomManager().method_12(RoomID);
                    else
                        RoomData = GoldTree.GetGame().GetRoomManager().method_12(0);

                    if (RoomData != null)
                    {
                        Message.AppendStringWithBreak(RoomData.Id.ToString()); //data
                        Message.AppendStringWithBreak(RoomData.Name); //extra data
                    }
                    else
                    {
                        Message.AppendStringWithBreak("");
                        Message.AppendStringWithBreak("Room deleted");
                    }
                }
            }
            Session.SendMessage(Message);
		}
	}
}
