using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
namespace GoldTree.Util
{
	internal static class LicenseTools
	{
		public const string string_0 = "a";
		public const string string_1 = "b";
		public const string string_2 = "c";
		public const string string_3 = "d";
		public const string string_4 = "e";
		public const string string_5 = "f";
		public const string string_6 = "g";
		public const string string_7 = "h";
		public const string string_8 = "i";
		public const string string_9 = "j";
		public const string string_10 = "k";
		public const string string_11 = "l";
		public const string string_12 = "m";
		public const string string_13 = "n";
		public const string string_14 = "o";
		public const string string_15 = "p";
		public const string string_16 = "q";
		public const string string_17 = "r";
		public const string string_18 = "s";
		public const string string_19 = "t";
		public const string string_20 = "u";
		public const string string_21 = "v";
		public const string string_22 = "w";
		public const string string_23 = "x";
		public const string string_24 = "y";
		public const string string_25 = "z";
		public static int int_0;
		public static int int_1;
		public static int int_2;
        public static bool AntiDDoSEnabled;
		public static int int_3;
		public static int int_4;
		public static int int_5;
		private static string string_26 = "";
		private static string string_27 = "";
		private static int int_6 = -1;
		private static int int_7 = 15;
		private static int int_8 = 75;
		private static int int_9 = 15;
		private static int int_10 = 0;
		private static bool bool_1 = true;
		private static bool bool_2 = true;
		private static bool bool_3 = true;
		private static bool bool_4 = true;
		private static bool bool_5 = true;
		private static int int_11 = 50;
		private static string string_28 = "disabled";
		private static bool bool_6 = true;
		private static bool bool_7 = false;
		private static bool bool_8 = false;
		private static bool bool_9 = false;
		private static bool bool_10 = false;
		private static bool bool_11 = false;
		private static bool bool_12 = false;
		private static bool bool_13 = false;
		private static string string_29 = "D93196DD0B540C93941AFB3FD1D7E7C3";
		private static string string_30 = "";
		private static string string_31 = "";
		private static string string_32 = "";
		private static bool bool_14 = false;
		public static bool bool_15 = false;
		public static bool bool_16 = false;
		public static string string_33 = "";
		public static int int_12 = 0;
		public static int int_13 = 0;
		public static bool bool_17 = false;
		public static bool bool_18 = true;
		public static bool bool_19 = true;
		public static int int_14 = 300;
		public static int int_15 = 1200;
		public static bool bool_20 = false;
        public static int int_16 = 8;
        public static double vipha_interval = 3600;
        public static bool bool_21 = false;
        public static bool bool_22 = false;
        public static string ProxyIP;
        public static double viphal_interval = 3600;
        public static bool DisableOtherUsersToMovingOtherUsersToDoor;
		public static bool Boolean_0
		{
			get
			{
				return LicenseTools.bool_14;
			}
			set
			{
				LicenseTools.bool_14 = value;
			}
		}
		public static string String_0
		{
			get
			{
				return LicenseTools.string_31;
			}
			set
			{
				LicenseTools.string_31 = EncryptionUtility.smethod_0(value);
			}
		}
		public static string String_1
		{
			get
			{
				return LicenseTools.string_32;
			}
			set
			{
				LicenseTools.string_32 = value;
				LicenseTools.String_0 = value;
			}
		}
		public static bool Boolean_1
		{
			get
			{
				return LicenseTools.bool_5;
			}
			set
			{
				LicenseTools.bool_5 = value;
			}
		}
		public static bool Boolean_2
		{
			get
			{
				return LicenseTools.bool_6;
			}
			set
			{
				LicenseTools.bool_6 = value;
			}
		}
		public static string String_2
		{
			get
			{
				return LicenseTools.string_28;
			}
			set
			{
				LicenseTools.string_28 = value;
			}
		}
		public static string String_3
		{
			get
			{
				return LicenseTools.string_30;
			}
			set
			{
				LicenseTools.string_30 = value;
			}
		}
		public static string String_4
		{
			get
			{
				return LicenseTools.string_26;
			}
			set
			{
				LicenseTools.string_26 = value.Replace("\\n", "\n");
			}
		}
		public static string String_5
		{
			get
			{
				return LicenseTools.string_29;
			}
			set
			{
				LicenseTools.string_29 = LicenseTools.smethod_0(LicenseTools.string_29 + value);
			}
		}
		public static bool Boolean_3
		{
			get
			{
				return LicenseTools.bool_4;
			}
			set
			{
				LicenseTools.bool_4 = value;
			}
		}
		public static bool Boolean_4
		{
			get
			{
				return LicenseTools.bool_1;
			}
			set
			{
				LicenseTools.bool_1 = value;
			}
		}
		public static bool Boolean_5
		{
			get
			{
				return LicenseTools.bool_2;
			}
			set
			{
				LicenseTools.bool_2 = value;
			}
		}
		public static bool Boolean_6
		{
			get
			{
				return LicenseTools.bool_3;
			}
			set
			{
				LicenseTools.bool_3 = value;
			}
		}
		public static int Int32_0
		{
			get
			{
				return LicenseTools.int_7;
			}
			set
			{
				LicenseTools.int_7 = value;
			}
		}
		public static int Int32_1
		{
			get
			{
				return LicenseTools.int_8;
			}
			set
			{
				LicenseTools.int_8 = value;
			}
		}
		public static int Int32_2
		{
			get
			{
				return LicenseTools.int_10;
			}
			set
			{
				LicenseTools.int_10 = value;
			}
		}
		public static int Int32_3
		{
			get
			{
				return LicenseTools.int_9;
			}
			set
			{
				LicenseTools.int_9 = value;
			}
		}
		public static int Int32_4
		{
			get
			{
				return LicenseTools.int_11;
			}
			set
			{
				LicenseTools.int_11 = value;
			}
		}
		public static string String_6
		{
			get
			{
				return LicenseTools.string_27;
			}
			set
			{
				LicenseTools.string_27 = value;
			}
		}
		public static int Int32_5
		{
			get
			{
				return LicenseTools.int_6;
			}
			set
			{
				LicenseTools.int_6 = value;
			}
		}
		public static bool Boolean_7
		{
			get
			{
				return LicenseTools.bool_7;
			}
			set
			{
				if (!LicenseTools.bool_7 && !LicenseTools.Boolean_7)
				{
					LicenseTools.bool_7 = value;
				}
				else
				{
					LicenseTools.bool_7 = false;
				}
			}
		}
		public static bool Boolean_8
		{
			get
			{
				return LicenseTools.bool_8;
			}
			set
			{
				LicenseTools.bool_8 = value;
			}
		}
		public static bool Boolean_9
		{
			get
			{
				return LicenseTools.bool_13;
			}
			set
			{
				LicenseTools.bool_13 = value;
			}
		}
		public static bool Boolean_10
		{
			get
			{
				return LicenseTools.bool_9;
			}
			set
			{
				LicenseTools.bool_9 = value;
			}
		}
		public static bool Boolean_11
		{
			get
			{
				return LicenseTools.bool_10;
			}
			set
			{
				LicenseTools.bool_10 = value;
			}
		}
		public static bool Boolean_12
		{
			get
			{
				return LicenseTools.bool_11;
			}
			set
			{
				LicenseTools.bool_11 = value;
			}
		}
		public static bool Boolean_13
		{
			get
			{
				return LicenseTools.bool_12;
			}
			set
			{
				LicenseTools.bool_12 = value;
			}
		}
		public static string smethod_0(string string_34)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] array = Encoding.UTF8.GetBytes(string_34);
			array = mD5CryptoServiceProvider.ComputeHash(array);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				stringBuilder.Append(b.ToString("x2").ToLower());
			}
			string text = stringBuilder.ToString();
			return text.ToUpper();
		}
		public static string smethod_1(string string_34)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(string_34);
			byte[] array = new SHA1Managed().ComputeHash(bytes);
			string text = string.Empty;
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				text += b.ToString("X2");
			}
			return text;
		}
		private static string smethod_2(string string_34)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(string_34);
			return BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(bytes)).Replace("-", "").ToLower();
		}
	}
}
