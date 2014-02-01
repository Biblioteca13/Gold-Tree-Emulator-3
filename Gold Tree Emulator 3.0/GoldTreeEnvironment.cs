using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using GoldTree.Core;
using GoldTree.Storage;
namespace GoldTree
{
	internal sealed class GoldTreeEnvironment
	{
		private static Dictionary<string, string> dictionary_0;
		public GoldTreeEnvironment()
		{
			GoldTreeEnvironment.dictionary_0 = new Dictionary<string, string>();
		}
		public static void smethod_0(DatabaseClient class6_0)
		{
            Logging.smethod_0("Loading external texts...");
			GoldTreeEnvironment.smethod_2();
			DataTable dataTable = class6_0.ReadDataTable("SELECT identifier, display_text FROM texts ORDER BY identifier ASC;");
			if (dataTable != null)
			{
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    GoldTreeEnvironment.dictionary_0.Add(dataRow["identifier"].ToString(), dataRow["display_text"].ToString());
                }
			}
			Logging.WriteLine("completed!");
		}
		public static string smethod_1(string string_0)
		{
			string result;
            if (GoldTreeEnvironment.dictionary_0 != null && GoldTreeEnvironment.dictionary_0.Count > 0 && GoldTreeEnvironment.dictionary_0.ContainsKey(string_0))
			{
				result = GoldTreeEnvironment.dictionary_0[string_0];
			}
            else if (!GoldTreeEnvironment.dictionary_0.ContainsKey(string_0))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nCan't find on texts: " + string_0);
                Console.ForegroundColor = ConsoleColor.Gray;
                result = string_0;
            }
			else
			{
				result = string_0;
			}
			return result;
		}
		public static void smethod_2()
		{
			GoldTreeEnvironment.dictionary_0.Clear();
		}
        public static int GetRandomNumber(int Min, int Max)
        {
            Random Quick = new Random();

            try
            {
                return Quick.Next(Min, Max);
            }
            catch
            {
                return Min;
            }
        }
	}
}