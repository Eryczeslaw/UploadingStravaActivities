using System;
using System.IO;

namespace UploadingStravaActivities.FilesModification
{
    public class SavingFiles
    {
        public static string Save(string name, string date, string time)
        {
            string path = ChangeName(name, date, time);

            return path;
        }

        private static string ChangeName(string name, string date, string time)
        {
            name = CorrectName(name);

            string newName = DoNewName(date, time);

            string path = $@"C:\Users\erykh\Downloads\{name}";
            string newPath = $@"C:\Users\erykh\Downloads\{newName}";

            if (File.Exists(path))
            {
                File.Copy(path, newPath, true);
                File.Delete(path);
            }

            return newPath;
        }

        private static string CorrectName(string name)
        {
            name = name.Replace(' ', '_');
            name = name.Replace('ą', '_');
            name = name.Replace('ć', '_');
            name = name.Replace('ę', '_');
            name = name.Replace('ł', '_');
            name = name.Replace('ó', '_');
            name = name.Replace('ś', '_');
            name = name.Replace('ź', '_');
            name = name.Replace('ż', '_');
            name = name.Replace(',', '_');
            name = name.Replace('.', '_');
            name = name.Replace('!', '_');
            name = name.Replace('?', '_');
            name = name.Replace(':', '_');
            name = name.Replace(')', '_');
            name = name.Replace('(', '_');
            name = name.Replace('#', '_');
            name = name.Replace('-', '_');
            name = name.Replace("___", "_");
            name = name.Replace("__", "_");
            name = name.Replace("__", "_");

            name += ".gpx";
            return name;
        }

        private static string DoNewName(string date, string time)
        {
            string tempName;
            string[] tempDate;

            tempDate = date.Split(',');
            time = DoNewTime(time.Substring(time.Length - 8).Trim());

            tempName = $"{tempDate[2].Trim()}-{tempDate[1].Trim()}-{time.Trim()}.gpx";

            return tempName;
        }

        private static string DoNewTime(string time)
        {
            string newTime;
            string[] partsTime = time.Split(' ', ':');

            if (partsTime[2] == "PM")
            {
                if (partsTime[0] != "12")
                {
                    int hours = Convert.ToInt32(partsTime[0]) + 12;
                    partsTime[0] = hours.ToString();
                }
            }
            else if (Convert.ToInt32(partsTime[0]) < 10)
            {
                partsTime[0] = "0" + partsTime[0];
            }

            newTime = $"{partsTime[0]}.{partsTime[1]}";

            return newTime;
        }
    }
}
