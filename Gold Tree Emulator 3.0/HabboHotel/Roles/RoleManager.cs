using System;
using System.Collections.Generic;
using System.Data;
using GoldTree.Core;
using GoldTree.HabboHotel.GameClients;
using GoldTree.Util;
using GoldTree.Storage;
namespace GoldTree.HabboHotel.Roles
{
	internal sealed class RoleManager
	{
		private Dictionary<uint, List<string>> dictionary_0;
		private Dictionary<uint, List<string>> dictionary_1;
		public Dictionary<uint, string> dictionary_2;
		private Dictionary<uint, int> dictionary_3;
		public Dictionary<string, int> dictionary_4;
		public Dictionary<string, int> dictionary_5;
		public RoleManager()
		{
			this.dictionary_0 = new Dictionary<uint, List<string>>();
			this.dictionary_1 = new Dictionary<uint, List<string>>();
			this.dictionary_2 = new Dictionary<uint, string>();
			this.dictionary_3 = new Dictionary<uint, int>();
			this.dictionary_4 = new Dictionary<string, int>();
			this.dictionary_5 = new Dictionary<string, int>();
		}
		public void method_0(DatabaseClient class6_0)
		{
			Logging.smethod_0(GoldTreeEnvironment.smethod_1("emu_loadroles"));
			this.method_10();
			DataTable dataTable = class6_0.ReadDataTable("SELECT * FROM ranks ORDER BY Id ASC;");
			if (dataTable != null)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					this.dictionary_2.Add((uint)dataRow["Id"], dataRow["badgeid"].ToString());
				}
			}
			dataTable = class6_0.ReadDataTable("SELECT * FROM permissions_users ORDER BY userid ASC;");
            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    List<string> list = new List<string>();
                    if (GoldTree.smethod_3(dataRow["cmd_update_settings"].ToString()))
                    {
                        list.Add("cmd_update_settings");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_update_bans"].ToString()))
                    {
                        list.Add("cmd_update_bans");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_update_bots"].ToString()))
                    {
                        list.Add("cmd_update_bots");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_update_catalogue"].ToString()))
                    {
                        list.Add("cmd_update_catalogue");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_update_navigator"].ToString()))
                    {
                        list.Add("cmd_update_navigator");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_update_items"].ToString()))
                    {
                        list.Add("cmd_update_items");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_award"].ToString()))
                    {
                        list.Add("cmd_award");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_coords"].ToString()))
                    {
                        list.Add("cmd_coords");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_override"].ToString()))
                    {
                        list.Add("cmd_override");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_coins"].ToString()))
                    {
                        list.Add("cmd_coins");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_pixels"].ToString()))
                    {
                        list.Add("cmd_pixels");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_ha"].ToString()))
                    {
                        list.Add("cmd_ha");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_hal"].ToString()))
                    {
                        list.Add("cmd_hal");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_freeze"].ToString()))
                    {
                        list.Add("cmd_freeze");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_enable"].ToString()))
                    {
                        list.Add("cmd_enable");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_roommute"].ToString()))
                    {
                        list.Add("cmd_roommute");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_setspeed"].ToString()))
                    {
                        list.Add("cmd_setspeed");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_masscredits"].ToString()))
                    {
                        list.Add("cmd_masscredits");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_globalcredits"].ToString()))
                    {
                        list.Add("cmd_globalcredits");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_masspixels"].ToString()))
                    {
                        list.Add("cmd_masspixels");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_globalpixels"].ToString()))
                    {
                        list.Add("cmd_globalpixels");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_roombadge"].ToString()))
                    {
                        list.Add("cmd_roombadge");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_massbadge"].ToString()))
                    {
                        list.Add("cmd_massbadge");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_userinfo"].ToString()))
                    {
                        list.Add("cmd_userinfo");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_userinfo_viewip"].ToString()))
                    {
                        list.Add("cmd_userinfo_viewip");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_shutdown"].ToString()))
                    {
                        list.Add("cmd_shutdown");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_givebadge"].ToString()))
                    {
                        list.Add("cmd_givebadge");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_removebadge"].ToString()))
                    {
                        list.Add("cmd_removebadge");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_summon"].ToString()))
                    {
                        list.Add("cmd_summon");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_invisible"].ToString()))
                    {
                        list.Add("cmd_invisible");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_ban"].ToString()))
                    {
                        list.Add("cmd_ban");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_superban"].ToString()))
                    {
                        list.Add("cmd_superban");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_roomkick"].ToString()))
                    {
                        list.Add("cmd_roomkick");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_roomalert"].ToString()))
                    {
                        list.Add("cmd_roomalert");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_mute"].ToString()))
                    {
                        list.Add("cmd_mute");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_unmute"].ToString()))
                    {
                        list.Add("cmd_unmute");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_alert"].ToString()))
                    {
                        list.Add("cmd_alert");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_motd"].ToString()))
                    {
                        list.Add("cmd_motd");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_kick"].ToString()))
                    {
                        list.Add("cmd_kick");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_update_filter"].ToString()))
                    {
                        list.Add("cmd_update_filter");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_update_permissions"].ToString()))
                    {
                        list.Add("cmd_update_permissions");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_sa"].ToString()))
                    {
                        list.Add("cmd_sa");
                    }
                    if (GoldTree.smethod_3(dataRow["receive_sa"].ToString()))
                    {
                        list.Add("receive_sa");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_ipban"].ToString()))
                    {
                        list.Add("cmd_ipban");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_spull"].ToString()))
                    {
                        list.Add("cmd_spull");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_disconnect"].ToString()))
                    {
                        list.Add("cmd_disconnect");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_update_achievements"].ToString()))
                    {
                        list.Add("cmd_update_achievements");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_update_texts"].ToString()))
                    {
                        list.Add("cmd_update_texts");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_teleport"].ToString()))
                    {
                        list.Add("cmd_teleport");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_points"].ToString()))
                    {
                        list.Add("cmd_points");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_masspoints"].ToString()))
                    {
                        list.Add("cmd_masspoints");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_globalpoints"].ToString()))
                    {
                        list.Add("cmd_globalpoints");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_empty"].ToString()))
                    {
                        list.Add("cmd_empty");
                    }
                    if (GoldTree.smethod_3(dataRow["ignore_roommute"].ToString()))
                    {
                        list.Add("ignore_roommute");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_anyroomrights"].ToString()))
                    {
                        list.Add("acc_anyroomrights");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_anyroomowner"].ToString()))
                    {
                        list.Add("acc_anyroomowner");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_supporttool"].ToString()))
                    {
                        list.Add("acc_supporttool");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_chatlogs"].ToString()))
                    {
                        list.Add("acc_chatlogs");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_enter_fullrooms"].ToString()))
                    {
                        list.Add("acc_enter_fullrooms");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_enter_anyroom"].ToString()))
                    {
                        list.Add("acc_enter_anyroom");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_restrictedrooms"].ToString()))
                    {
                        list.Add("acc_restrictedrooms");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_unkickable"].ToString()))
                    {
                        list.Add("acc_unkickable");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_unbannable"].ToString()))
                    {
                        list.Add("acc_unbannable");
                    }
                    if (GoldTree.smethod_3(dataRow["ignore_friendsettings"].ToString()))
                    {
                        list.Add("ignore_friendsettings");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_sql"].ToString()))
                    {
                        list.Add("wired_give_sql");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_badge"].ToString()))
                    {
                        list.Add("wired_give_badge");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_effect"].ToString()))
                    {
                        list.Add("wired_give_effect");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_award"].ToString()))
                    {
                        list.Add("wired_give_award");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_dance"].ToString()))
                    {
                        list.Add("wired_give_dance");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_send"].ToString()))
                    {
                        list.Add("wired_give_send");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_credits"].ToString()))
                    {
                        list.Add("wired_give_credits");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_pixels"].ToString()))
                    {
                        list.Add("wired_give_pixels");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_points"].ToString()))
                    {
                        list.Add("wired_give_points");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_rank"].ToString()))
                    {
                        list.Add("wired_give_rank");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_respect"].ToString()))
                    {
                        list.Add("wired_give_respect");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_handitem"].ToString()))
                    {
                        list.Add("wired_give_handitem");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_alert"].ToString()))
                    {
                        list.Add("wired_give_alert");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_roomusers"].ToString()))
                    {
                        list.Add("wired_cnd_roomusers");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_userhasachievement"].ToString()))
                    {
                        list.Add("wired_cnd_userhasachievement");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_userhasbadge"].ToString()))
                    {
                        list.Add("wired_cnd_userhasbadge");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_userhasvip"].ToString()))
                    {
                        list.Add("wired_cnd_userhasvip");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_userhaseffect"].ToString()))
                    {
                        list.Add("wired_cnd_userhaseffect");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_userrank"].ToString()))
                    {
                        list.Add("wired_cnd_userrank");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_usercredits"].ToString()))
                    {
                        list.Add("wired_cnd_usercredits");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_userpixels"].ToString()))
                    {
                        list.Add("wired_cnd_userpixels");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_userpoints"].ToString()))
                    {
                        list.Add("wired_cnd_userpoints");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_usergroups"].ToString()))
                    {
                        list.Add("wired_cnd_usergroups");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_wearing"].ToString()))
                    {
                        list.Add("wired_cnd_wearing");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_carrying"].ToString()))
                    {
                        list.Add("wired_cnd_carrying");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_give_wiredactived"].ToString()))
                    {
                        list.Add("wired_give_wiredactived");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_wiredactived"].ToString()))
                    {
                        list.Add("wired_cnd_wiredactived");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_unlimitedselects"].ToString()))
                    {
                        list.Add("wired_unlimitedselects");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_dance"].ToString()))
                    {
                        list.Add("cmd_dance");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_rave"].ToString()))
                    {
                        list.Add("cmd_rave");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_roll"].ToString()))
                    {
                        list.Add("cmd_roll");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_control"].ToString()))
                    {
                        list.Add("cmd_control");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_makesay"].ToString()))
                    {
                        list.Add("cmd_makesay");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_sitdown"].ToString()))
                    {
                        list.Add("cmd_sitdown");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_lay"].ToString()))
                    {
                        list.Add("cmd_lay");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_push"].ToString()))
                    {
                        list.Add("cmd_push");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_pull"].ToString()))
                    {
                        list.Add("cmd_pull");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_flagme"].ToString()))
                    {
                        list.Add("cmd_flagme");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_mimic"].ToString()))
                    {
                        list.Add("cmd_mimic");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_moonwalk"].ToString()))
                    {
                        list.Add("cmd_moonwalk");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_follow"].ToString()))
                    {
                        list.Add("cmd_follow");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_handitem"].ToString()))
                    {
                        list.Add("cmd_handitem");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_startquestion"].ToString()))
                    {
                        list.Add("cmd_startquestion");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_vipha"].ToString()))
                    {
                        list.Add("cmd_vipha");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_spush"].ToString()))
                    {
                        list.Add("cmd_spush");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_roomeffect"].ToString()))
                    {
                        list.Add("cmd_roomeffect");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_viphal"].ToString()))
                    {
                        list.Add("cmd_viphal");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_moveotheruserstodoor"].ToString()))
                    {
                        list.Add("acc_moveotheruserstodoor");
                    }
                    this.dictionary_0.Add((uint)dataRow["userid"], list);
                }
            }
			dataTable = class6_0.ReadDataTable("SELECT * FROM permissions_ranks ORDER BY rank ASC;");
			if (dataTable != null)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					this.dictionary_3.Add((uint)dataRow["rank"], (int)dataRow["floodtime"]);
				}
				foreach (DataRow dataRow in dataTable.Rows)
				{
					List<string> list = new List<string>();
					if (GoldTree.smethod_3(dataRow["cmd_update_settings"].ToString()))
					{
						list.Add("cmd_update_settings");
					}
					if (GoldTree.smethod_3(dataRow["cmd_update_bans"].ToString()))
					{
						list.Add("cmd_update_bans");
					}
					if (GoldTree.smethod_3(dataRow["cmd_update_bots"].ToString()))
					{
						list.Add("cmd_update_bots");
					}
					if (GoldTree.smethod_3(dataRow["cmd_update_catalogue"].ToString()))
					{
						list.Add("cmd_update_catalogue");
					}
					if (GoldTree.smethod_3(dataRow["cmd_update_navigator"].ToString()))
					{
						list.Add("cmd_update_navigator");
					}
					if (GoldTree.smethod_3(dataRow["cmd_update_items"].ToString()))
					{
						list.Add("cmd_update_items");
					}
					if (GoldTree.smethod_3(dataRow["cmd_award"].ToString()))
					{
						list.Add("cmd_award");
					}
					if (GoldTree.smethod_3(dataRow["cmd_coords"].ToString()))
					{
						list.Add("cmd_coords");
					}
					if (GoldTree.smethod_3(dataRow["cmd_override"].ToString()))
					{
						list.Add("cmd_override");
					}
					if (GoldTree.smethod_3(dataRow["cmd_coins"].ToString()))
					{
						list.Add("cmd_coins");
					}
					if (GoldTree.smethod_3(dataRow["cmd_pixels"].ToString()))
					{
						list.Add("cmd_pixels");
					}
					if (GoldTree.smethod_3(dataRow["cmd_ha"].ToString()))
					{
						list.Add("cmd_ha");
					}
					if (GoldTree.smethod_3(dataRow["cmd_hal"].ToString()))
					{
						list.Add("cmd_hal");
					}
					if (GoldTree.smethod_3(dataRow["cmd_freeze"].ToString()))
					{
						list.Add("cmd_freeze");
					}
					if (GoldTree.smethod_3(dataRow["cmd_enable"].ToString()))
					{
						list.Add("cmd_enable");
					}
					if (GoldTree.smethod_3(dataRow["cmd_roommute"].ToString()))
					{
						list.Add("cmd_roommute");
					}
					if (GoldTree.smethod_3(dataRow["cmd_setspeed"].ToString()))
					{
						list.Add("cmd_setspeed");
					}
					if (GoldTree.smethod_3(dataRow["cmd_masscredits"].ToString()))
					{
						list.Add("cmd_masscredits");
					}
					if (GoldTree.smethod_3(dataRow["cmd_globalcredits"].ToString()))
					{
						list.Add("cmd_globalcredits");
					}
					if (GoldTree.smethod_3(dataRow["cmd_masspixels"].ToString()))
					{
						list.Add("cmd_masspixels");
					}
					if (GoldTree.smethod_3(dataRow["cmd_globalpixels"].ToString()))
					{
						list.Add("cmd_globalpixels");
					}
					if (GoldTree.smethod_3(dataRow["cmd_roombadge"].ToString()))
					{
						list.Add("cmd_roombadge");
					}
					if (GoldTree.smethod_3(dataRow["cmd_massbadge"].ToString()))
					{
						list.Add("cmd_massbadge");
					}
					if (GoldTree.smethod_3(dataRow["cmd_userinfo"].ToString()))
					{
						list.Add("cmd_userinfo");
					}
					if (GoldTree.smethod_3(dataRow["cmd_userinfo_viewip"].ToString()))
					{
						list.Add("cmd_userinfo_viewip");
					}
					if (GoldTree.smethod_3(dataRow["cmd_shutdown"].ToString()))
					{
						list.Add("cmd_shutdown");
					}
					if (GoldTree.smethod_3(dataRow["cmd_givebadge"].ToString()))
					{
						list.Add("cmd_givebadge");
					}
					if (GoldTree.smethod_3(dataRow["cmd_removebadge"].ToString()))
					{
						list.Add("cmd_removebadge");
					}
					if (GoldTree.smethod_3(dataRow["cmd_summon"].ToString()))
					{
						list.Add("cmd_summon");
					}
					if (GoldTree.smethod_3(dataRow["cmd_invisible"].ToString()))
					{
						list.Add("cmd_invisible");
					}
					if (GoldTree.smethod_3(dataRow["cmd_ban"].ToString()))
					{
						list.Add("cmd_ban");
					}
					if (GoldTree.smethod_3(dataRow["cmd_superban"].ToString()))
					{
						list.Add("cmd_superban");
					}
					if (GoldTree.smethod_3(dataRow["cmd_roomkick"].ToString()))
					{
						list.Add("cmd_roomkick");
					}
					if (GoldTree.smethod_3(dataRow["cmd_roomalert"].ToString()))
					{
						list.Add("cmd_roomalert");
					}
					if (GoldTree.smethod_3(dataRow["cmd_mute"].ToString()))
					{
						list.Add("cmd_mute");
					}
					if (GoldTree.smethod_3(dataRow["cmd_unmute"].ToString()))
					{
						list.Add("cmd_unmute");
					}
					if (GoldTree.smethod_3(dataRow["cmd_alert"].ToString()))
					{
						list.Add("cmd_alert");
					}
					if (GoldTree.smethod_3(dataRow["cmd_motd"].ToString()))
					{
						list.Add("cmd_motd");
					}
					if (GoldTree.smethod_3(dataRow["cmd_kick"].ToString()))
					{
						list.Add("cmd_kick");
					}
					if (GoldTree.smethod_3(dataRow["cmd_update_filter"].ToString()))
					{
						list.Add("cmd_update_filter");
					}
					if (GoldTree.smethod_3(dataRow["cmd_update_permissions"].ToString()))
					{
						list.Add("cmd_update_permissions");
					}
					if (GoldTree.smethod_3(dataRow["cmd_sa"].ToString()))
					{
						list.Add("cmd_sa");
					}
					if (GoldTree.smethod_3(dataRow["receive_sa"].ToString()))
					{
						list.Add("receive_sa");
					}
					if (GoldTree.smethod_3(dataRow["cmd_ipban"].ToString()))
					{
						list.Add("cmd_ipban");
					}
					if (GoldTree.smethod_3(dataRow["cmd_spull"].ToString()))
					{
						list.Add("cmd_spull");
					}
					if (GoldTree.smethod_3(dataRow["cmd_disconnect"].ToString()))
					{
						list.Add("cmd_disconnect");
					}
					if (GoldTree.smethod_3(dataRow["cmd_update_achievements"].ToString()))
					{
						list.Add("cmd_update_achievements");
					}
					if (GoldTree.smethod_3(dataRow["cmd_update_texts"].ToString()))
					{
						list.Add("cmd_update_texts");
					}
					if (GoldTree.smethod_3(dataRow["cmd_teleport"].ToString()))
					{
						list.Add("cmd_teleport");
					}
					if (GoldTree.smethod_3(dataRow["cmd_points"].ToString()))
					{
						list.Add("cmd_points");
					}
					if (GoldTree.smethod_3(dataRow["cmd_masspoints"].ToString()))
					{
						list.Add("cmd_masspoints");
					}
					if (GoldTree.smethod_3(dataRow["cmd_globalpoints"].ToString()))
					{
						list.Add("cmd_globalpoints");
					}
					if (GoldTree.smethod_3(dataRow["cmd_empty"].ToString()))
					{
						list.Add("cmd_empty");
					}
					if (GoldTree.smethod_3(dataRow["ignore_roommute"].ToString()))
					{
						list.Add("ignore_roommute");
					}
					if (GoldTree.smethod_3(dataRow["acc_anyroomrights"].ToString()))
					{
						list.Add("acc_anyroomrights");
					}
					if (GoldTree.smethod_3(dataRow["acc_anyroomowner"].ToString()))
					{
						list.Add("acc_anyroomowner");
					}
					if (GoldTree.smethod_3(dataRow["acc_supporttool"].ToString()))
					{
						list.Add("acc_supporttool");
					}
					if (GoldTree.smethod_3(dataRow["acc_chatlogs"].ToString()))
					{
						list.Add("acc_chatlogs");
					}
					if (GoldTree.smethod_3(dataRow["acc_enter_fullrooms"].ToString()))
					{
						list.Add("acc_enter_fullrooms");
					}
					if (GoldTree.smethod_3(dataRow["acc_enter_anyroom"].ToString()))
					{
						list.Add("acc_enter_anyroom");
					}
					if (GoldTree.smethod_3(dataRow["acc_restrictedrooms"].ToString()))
					{
						list.Add("acc_restrictedrooms");
					}
					if (GoldTree.smethod_3(dataRow["acc_unkickable"].ToString()))
					{
						list.Add("acc_unkickable");
					}
					if (GoldTree.smethod_3(dataRow["acc_unbannable"].ToString()))
					{
						list.Add("acc_unbannable");
					}
					if (GoldTree.smethod_3(dataRow["ignore_friendsettings"].ToString()))
					{
						list.Add("ignore_friendsettings");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_sql"].ToString()))
					{
						list.Add("wired_give_sql");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_badge"].ToString()))
					{
						list.Add("wired_give_badge");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_effect"].ToString()))
					{
						list.Add("wired_give_effect");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_award"].ToString()))
					{
						list.Add("wired_give_award");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_dance"].ToString()))
					{
						list.Add("wired_give_dance");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_send"].ToString()))
					{
						list.Add("wired_give_send");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_credits"].ToString()))
					{
						list.Add("wired_give_credits");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_pixels"].ToString()))
					{
						list.Add("wired_give_pixels");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_points"].ToString()))
					{
						list.Add("wired_give_points");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_rank"].ToString()))
					{
						list.Add("wired_give_rank");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_respect"].ToString()))
					{
						list.Add("wired_give_respect");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_handitem"].ToString()))
					{
						list.Add("wired_give_handitem");
					}
					if (GoldTree.smethod_3(dataRow["wired_give_alert"].ToString()))
					{
						list.Add("wired_give_alert");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_roomusers"].ToString()))
					{
						list.Add("wired_cnd_roomusers");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_userhasachievement"].ToString()))
					{
						list.Add("wired_cnd_userhasachievement");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_userhasbadge"].ToString()))
					{
						list.Add("wired_cnd_userhasbadge");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_userhasvip"].ToString()))
					{
						list.Add("wired_cnd_userhasvip");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_userhaseffect"].ToString()))
					{
						list.Add("wired_cnd_userhaseffect");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_userrank"].ToString()))
					{
						list.Add("wired_cnd_userrank");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_usercredits"].ToString()))
					{
						list.Add("wired_cnd_usercredits");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_userpixels"].ToString()))
					{
						list.Add("wired_cnd_userpixels");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_userpoints"].ToString()))
					{
						list.Add("wired_cnd_userpoints");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_usergroups"].ToString()))
					{
						list.Add("wired_cnd_usergroups");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_wearing"].ToString()))
					{
						list.Add("wired_cnd_wearing");
					}
					if (GoldTree.smethod_3(dataRow["wired_cnd_carrying"].ToString()))
					{
						list.Add("wired_cnd_carrying");
					}
                    if (GoldTree.smethod_3(dataRow["wired_give_wiredactived"].ToString()))
                    {
                        list.Add("wired_give_wiredactived");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_cnd_wiredactived"].ToString()))
                    {
                        list.Add("wired_cnd_wiredactived");
                    }
                    if (GoldTree.smethod_3(dataRow["wired_unlimitedselects"].ToString()))
                    {
                        list.Add("wired_unlimitedselects");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_dance"].ToString()))
                    {
                        list.Add("cmd_dance");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_rave"].ToString()))
                    {
                        list.Add("cmd_rave");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_roll"].ToString()))
                    {
                        list.Add("cmd_roll");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_control"].ToString()))
                    {
                        list.Add("cmd_control");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_makesay"].ToString()))
                    {
                        list.Add("cmd_makesay");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_sitdown"].ToString()))
                    {
                        list.Add("cmd_sitdown");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_lay"].ToString()))
                    {
                        list.Add("cmd_lay");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_push"].ToString()))
                    {
                        list.Add("cmd_push");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_pull"].ToString()))
                    {
                        list.Add("cmd_pull");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_flagme"].ToString()))
                    {
                        list.Add("cmd_flagme");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_mimic"].ToString()))
                    {
                        list.Add("cmd_mimic");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_moonwalk"].ToString()))
                    {
                        list.Add("cmd_moonwalk");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_follow"].ToString()))
                    {
                        list.Add("cmd_follow");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_handitem"].ToString()))
                    {
                        list.Add("cmd_handitem");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_startquestion"].ToString()))
                    {
                        list.Add("cmd_startquestion");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_vipha"].ToString()))
                    {
                        list.Add("cmd_vipha");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_spush"].ToString()))
                    {
                        list.Add("cmd_spush");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_roomeffect"].ToString()))
                    {
                        list.Add("cmd_roomeffect");
                    }
                    if (GoldTree.smethod_3(dataRow["cmd_viphal"].ToString()))
                    {
                        list.Add("cmd_viphal");
                    }
                    if (GoldTree.smethod_3(dataRow["acc_moveotheruserstodoor"].ToString()))
                    {
                        list.Add("acc_moveotheruserstodoor");
                    }
					this.dictionary_1.Add((uint)dataRow["rank"], list);
				}
			}
			dataTable = class6_0.ReadDataTable("SELECT * FROM permissions_vip;");
			if (dataTable != null)
			{
				LicenseTools.Boolean_8 = false;
				LicenseTools.Boolean_10 = false;
				LicenseTools.Boolean_11 = false;
				LicenseTools.Boolean_12 = false;
				LicenseTools.Boolean_13 = false;
				LicenseTools.Boolean_9 = false;
				foreach (DataRow dataRow in dataTable.Rows)
				{
					if (GoldTree.smethod_3(dataRow["cmdPush"].ToString()))
					{
						LicenseTools.Boolean_8 = true;
					}
					if (GoldTree.smethod_3(dataRow["cmdPull"].ToString()))
					{
						LicenseTools.Boolean_10 = true;
					}
					if (GoldTree.smethod_3(dataRow["cmdFlagme"].ToString()))
					{
						LicenseTools.Boolean_11 = true;
					}
					if (GoldTree.smethod_3(dataRow["cmdMimic"].ToString()))
					{
						LicenseTools.Boolean_12 = true;
					}
					if (GoldTree.smethod_3(dataRow["cmdMoonwalk"].ToString()))
					{
						LicenseTools.Boolean_13 = true;
					}
					if (GoldTree.smethod_3(dataRow["cmdFollow"].ToString()))
					{
						LicenseTools.Boolean_9 = true;
					}
				}
			}
			this.dictionary_5.Clear();
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_sleep"), 1);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_free"), 2);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_sit"), 3);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_lay"), 4);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_stay"), 5);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_here"), 6);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_dead"), 7);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_beg"), 8);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_jump"), 9);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_stfu"), 10);
			this.dictionary_5.Add(GoldTreeEnvironment.smethod_1("pet_cmd_talk"), 11);
			this.dictionary_4.Clear();
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_about_name"), 1);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_alert_name"), 2);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_award_name"), 3);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_ban_name"), 4);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_buy_name"), 5);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_coins_name"), 6);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_coords_name"), 7);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_disablediagonal_name"), 8);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_emptyitems_name"), 9);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_empty_name"), 10);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_enable_name"), 11);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_flagme_name"), 12);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_follow_name"), 13);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_freeze_name"), 14);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_givebadge_name"), 15);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_globalcredits_name"), 16);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_globalpixels_name"), 17);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_globalpoints_name"), 18);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_hal_name"), 19);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_ha_name"), 20);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_invisible_name"), 21);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_ipban_name"), 22);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_kick_name"), 23);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_massbadge_name"), 24);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_masscredits_name"), 25);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_masspixels_name"), 26);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_masspoints_name"), 27);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_mimic_name"), 28);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_moonwalk_name"), 29);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_motd_name"), 30);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_mute_name"), 31);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_override_name"), 32);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_pickall_name"), 33);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_pixels_name"), 34);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_points_name"), 35);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_pull_name"), 36);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_push_name"), 37);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_redeemcreds_name"), 38);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_removebadge_name"), 39);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_ride_name"), 40);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_roomalert_name"), 41);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_roombadge_name"), 42);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_roomkick_name"), 43);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_roommute_name"), 44);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_sa_name"), 45);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_setmax_name"), 46);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_setspeed_name"), 47);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_shutdown_name"), 48);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_spull_name"), 49);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_summon_name"), 50);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_superban_name"), 51);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_teleport_name"), 52);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_unload_name"), 53);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_unmute_name"), 54);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_achievements_name"), 55);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_bans_name"), 56);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_bots_name"), 57);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_catalogue_name"), 58);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_filter_name"), 59);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_items_name"), 60);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_navigator_name"), 61);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_permissions_name"), 62);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_settings_name"), 63);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_userinfo_name"), 64);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_update_texts_name"), 65);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_disconnect_name"), 66);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_commands_name"), 67);
			this.dictionary_4.Add("about", 68);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_roominfo_name"), 69);
			this.dictionary_4.Add("neto737", 70);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_dance_name"), 71);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_rave_name"), 72);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_roll_name"), 73);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_control_name"), 74);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_makesay_name"), 75);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_sitdown_name"), 76);
            this.dictionary_4.Add("exe", 77);
			this.dictionary_4.Add("giveitem", 79);
			this.dictionary_4.Add("sit", 80);
			this.dictionary_4.Add("dismount", 81);
			this.dictionary_4.Add("getoff", 82);
			this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_emptypets_name"), 83);
            this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_startquestion_name"), 94);
            this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_lay_name"), 86);
            this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_handitem_name"), 85);
            this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_vipha_name"), 87);
            this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_spush_name"), 88);
            this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_roomeffect_name"), 91);
            this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_redeempixel_name"), 95);
            this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_redeemshell_name"), 96);
            this.dictionary_4.Add(GoldTreeEnvironment.smethod_1("cmd_viphal_name"), 97);
			Logging.WriteLine("completed!");
			try
			{
				if (int.Parse(GoldTree.GetConfig().data["debug"]) == 1)
				{
					Logging.WriteLine("Commands loaded:" + this.dictionary_4.Count.ToString());
				}
			}
			catch
			{
			}
		}
		public bool method_1(uint uint_0, string string_0)
		{
			bool result;
			/*if (GoldTree.Length == 0)
			{
				result = false;
			}
			else*/
			{
				if (!this.method_7(uint_0))
				{
					result = false;
				}
				else
				{
					List<string> list = this.dictionary_1[uint_0];
					result = list.Contains(string_0);
				}
			}
			return result;
		}
		public int method_2(uint uint_0)
		{
			return this.dictionary_3[uint_0];
		}
		public bool method_3(uint uint_0)
		{
			return this.method_6(uint_0);
		}
		public bool method_4(uint uint_0, string string_0)
		{
			bool result;
			if (!this.method_6(uint_0))
			{
				result = false;
			}
			else
			{
				List<string> list = this.dictionary_0[uint_0];
				result = list.Contains(string_0);
			}
			return result;
		}
		public List<string> method_5(uint uint_0, uint uint_1)
		{
			List<string> result = new List<string>();
			if (this.method_6(uint_0))
			{
				result = this.dictionary_0[uint_0];
			}
			else
			{
				result = this.dictionary_1[uint_1];
			}
			return result;
		}
		public bool method_6(uint uint_0)
		{
			return this.dictionary_0.ContainsKey(uint_0);
		}
		public bool method_7(uint uint_0)
		{
			return this.dictionary_1.ContainsKey(uint_0);
		}
		public string method_8(uint uint_0)
		{
            if (this.dictionary_2.ContainsKey(uint_0))
            {
                return this.dictionary_2[uint_0];
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nCan't find rank: " + uint_0);
                Console.ForegroundColor = ConsoleColor.Gray;
                return "error";
            }
		}
		public int method_9()
		{
			return this.dictionary_2.Count;
		}
		public void method_10()
		{
			this.dictionary_2.Clear();
			this.dictionary_0.Clear();
			this.dictionary_1.Clear();
			this.dictionary_3.Clear();
		}
		public bool method_11(string string_0, GameClient Session)
		{
			bool result;
			switch (string_0)
			{
			case "roomuserseq":
			case "roomuserslt":
			case "roomusersmt":
			case "roomusersmte":
			case "roomuserslte":
				if (Session.GetHabbo().HasFuse("wired_cnd_roomusers"))
				{
					result = true;
					return result;
				}
				break;
			case "userhasachievement":
			case "userhasntachievement":
				if (Session.GetHabbo().HasFuse("wired_cnd_userhasachievement"))
				{
					result = true;
					return result;
				}
				break;
			case "userhasbadge":
			case "userhasntbadge":
				if (Session.GetHabbo().HasFuse("wired_cnd_userhasbadge"))
				{
					result = true;
					return result;
				}
				break;
			case "userhasvip":
			case "userhasntvip":
				if (Session.GetHabbo().HasFuse("wired_cnd_userhasvip"))
				{
					result = true;
					return result;
				}
				break;
			case "userhaseffect":
			case "userhasnteffect":
				if (Session.GetHabbo().HasFuse("wired_cnd_userhaseffect"))
				{
					result = true;
					return result;
				}
				break;
			case "userrankeq":
			case "userrankmt":
			case "userrankmte":
			case "userranklt":
			case "userranklte":
				if (Session.GetHabbo().HasFuse("wired_cnd_userrank"))
				{
					result = true;
					return result;
				}
				break;
			case "usercreditseq":
			case "usercreditsmt":
			case "usercreditsmte":
			case "usercreditslt":
			case "usercreditslte":
				if (Session.GetHabbo().HasFuse("wired_cnd_usercredits"))
				{
					result = true;
					return result;
				}
				break;
			case "userpixelseq":
			case "userpixelsmt":
			case "userpixelsmte":
			case "userpixelslt":
			case "userpixelslte":
				if (Session.GetHabbo().HasFuse("wired_cnd_userpixels"))
				{
					result = true;
					return result;
				}
				break;
			case "userpointseq":
			case "userpointsmt":
			case "userpointsmte":
			case "userpointslt":
			case "userpointslte":
				if (Session.GetHabbo().HasFuse("wired_cnd_userpoints"))
				{
					result = true;
					return result;
				}
				break;
			case "usergroupeq":
			case "userisingroup":
				if (Session.GetHabbo().HasFuse("wired_cnd_usergroups"))
				{
					result = true;
					return result;
				}
				break;
			case "wearing":
			case "notwearing":
				if (Session.GetHabbo().HasFuse("wired_cnd_wearing"))
				{
					result = true;
					return result;
				}
				break;
			case "carrying":
			case "notcarrying":
				if (Session.GetHabbo().HasFuse("wired_cnd_carrying"))
				{
					result = true;
					return result;
				}
				break;
            case "wiredactived":
            case "notwiredactived":
                if (Session.GetHabbo().HasFuse("wired_cnd_wiredactived"))
                {
                    result = true;
                    return result;
                }
                break;
			}
			result = false;
			return result;
		}
		public bool method_12(string string_0, GameClient Session)
		{
			bool result;
			switch (string_0)
			{
			case "sql":
				if (Session.GetHabbo().HasFuse("wired_give_sql"))
				{
					result = true;
					return result;
				}
				break;
			case "badge":
				if (Session.GetHabbo().HasFuse("wired_give_badge"))
				{
					result = true;
					return result;
				}
				break;
			case "effect":
				if (Session.GetHabbo().HasFuse("wired_give_effect"))
				{
					result = true;
					return result;
				}
				break;
			case "award":
				if (Session.GetHabbo().HasFuse("wired_give_award"))
				{
					result = true;
					return result;
				}
				break;
			case "dance":
				if (Session.GetHabbo().HasFuse("wired_give_dance"))
				{
					result = true;
					return result;
				}
				break;
			case "send":
				if (Session.GetHabbo().HasFuse("wired_give_send"))
				{
					result = true;
					return result;
				}
				break;
			case "credits":
				if (Session.GetHabbo().HasFuse("wired_give_credits"))
				{
					result = true;
					return result;
				}
				break;
			case "pixels":
				if (Session.GetHabbo().HasFuse("wired_give_pixels"))
				{
					result = true;
					return result;
				}
				break;
			case "points":
				if (Session.GetHabbo().HasFuse("wired_give_points"))
				{
					result = true;
					return result;
				}
				break;
			case "rank":
				if (Session.GetHabbo().HasFuse("wired_give_rank"))
				{
					result = true;
					return result;
				}
				break;
			case "respect":
				if (Session.GetHabbo().HasFuse("wired_give_respect"))
				{
					result = true;
					return result;
				}
				break;
			case "handitem":
				if (Session.GetHabbo().HasFuse("wired_give_handitem"))
				{
					result = true;
					return result;
				}
				break;
			case "alert":
				if (Session.GetHabbo().HasFuse("wired_give_alert"))
				{
					result = true;
					return result;
				}
				break;
            case "wiredactived":
                if (Session.GetHabbo().HasFuse("wired_give_wiredactived"))
                {
                    result = true;
                    return result;
                }
                break;
			}
			result = false;
			return result;
		}
	}
}