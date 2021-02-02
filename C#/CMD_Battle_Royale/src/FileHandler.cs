using System;
using System.IO;
using System.Collections.Generic;

namespace BattleRoyale
{
    static class FileHandler
    {
        public static bool SettingsExists()
        {
            return (File.Exists("settings.sett"));
        }

        public static List<string> ReadSettings()
        {
            List<string> file = new List<string>();

            using (StreamReader reader = new StreamReader("settings.sett"))
            {
                string line = reader.ReadLine();
                while(line != null)
                {
                    file.Add(line);
                    line = reader.ReadLine();
                }
            }

            return file;
        }

        public static void SaveSettings(List<string> file)
        {
            using (StreamWriter writer = new StreamWriter("settings.sett", false))
            {
                foreach (string line in file)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
