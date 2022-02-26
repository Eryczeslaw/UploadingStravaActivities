using System;
using System.IO;

namespace UploadingStravaActivities.FilesModification
{
    public class SavingFiles
    {
        public static string Save(string directoryDownloadPath, string name, string date, string time)
        {
            string path = ChangeName(directoryDownloadPath, name, date, time);

            return path;
        }

        private static string ChangeName(string directoryDownloadPath, string name, string date, string time)
        {
            name = CorrectName(name);

            string newName = DoNewName(date, time);
            string filePath = directoryDownloadPath + name;
            string fileNewPath = directoryDownloadPath + newName;

            if (File.Exists(filePath))
            {
                File.Copy(filePath, fileNewPath, true);
                File.Delete(filePath);
            }

            return fileNewPath;
        }

        private static string CorrectName(string name)
        {
            name = name.Replace(' ', '_');
            name = name.Replace('ą', '_');
            name = name.Replace('ć', '_');
            name = name.Replace('ę', '_');
            name = name.Replace('ł', '_');
            name = name.Replace('ń', '_');
            name = name.Replace('ó', '_');
            name = name.Replace('ś', '_');
            name = name.Replace('ź', '_');
            name = name.Replace('ż', '_');
            name = name.Replace(',', '_');
            name = name.Replace('.', '_');
            name = name.Replace('!', '_');
            name = name.Replace('?', '_');
            name = name.Replace(':', '_');
            name = name.Replace(';', '_');
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
            int hours = Convert.ToInt32(partsTime[0]) - 1;

            if (partsTime[2] == "PM")
            {
                if (hours != 10)
                {
                    hours += 12;
                    partsTime[0] = hours.ToString();
                }
            }
            else if (hours < 10)
            {
                partsTime[0] = "0" + hours.ToString();
            }
            else
            {
                hours += 12;
            }

            newTime = $"{partsTime[0]}.{partsTime[1]}";

            return newTime;
        }
    }
}
