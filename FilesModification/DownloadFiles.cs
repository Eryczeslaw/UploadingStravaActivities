using System;
using System.IO;
using System.Threading;

namespace UploadingStravaActivities.FilesModification
{
    public class DownloadFiles
    {
        public static string Download(string directoryDownloadPath, string name, string date, string time)
        {
            string path = ChangeName(directoryDownloadPath, name, date, time);

            return path;
        }

        private static string ChangeName(string directoryDownloadPath, string name, string date, string time)
        {
            string wantedName = CorrectName(name);
            string wantedFilePath = directoryDownloadPath + "\\" + wantedName + ".gpx";

            string invisibleSpaceName = wantedName + "_";
            string invisibleSpaceFilePath = directoryDownloadPath + "\\" + invisibleSpaceName + ".gpx";

            string newName = DoNewName(date, time);
            string newFilePath = directoryDownloadPath + "\\" + newName + ".gpx";

            for (int i = 0; i < StravaTests.secondsToDownload * 4; i++)
            {
                if (File.Exists(wantedFilePath))
                {
                    File.Copy(wantedFilePath, newFilePath, true);
                    File.Delete(wantedFilePath);
                    return newFilePath;
                }
                else if (File.Exists(invisibleSpaceFilePath))
                {
                    File.Copy(invisibleSpaceFilePath, newFilePath, true);
                    File.Delete(invisibleSpaceFilePath);
                    return newFilePath;
                }

                Thread.Sleep(250);
            }

            throw new FileNotFoundException();
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
            name = name.Replace('/', '_');
            name = name.Replace('\\', '_');
            name = name.Replace('!', '_');
            name = name.Replace('?', '_');
            name = name.Replace(':', '_');
            name = name.Replace(';', '_');
            name = name.Replace(')', '_');
            name = name.Replace('(', '_');
            name = name.Replace('#', '_');
            name = name.Replace('^', '_');
            name = name.Replace('&', '_');
            name = name.Replace('-', '_');
            name = name.Replace('+', '_');
            name = name.Replace("😎", "_");
            name = name.Replace("🏭", "_");
            name = name.Replace("🏘️", "_");
            name = name.Replace("___", "_");
            name = name.Replace("__", "_");
            name = name.Replace("__", "_");

            return name;
        }

        private static string DoNewName(string date, string time)
        {
            string tempName;
            string[] tempDate;

            tempDate = date.Split(',');
            time = ConvertTime(time.Substring(time.Length - 8).Trim());

            tempName = $"{tempDate[2].Trim()}-{tempDate[1].Trim()}-{time.Trim()}";

            return tempName;
        }

        private static string ConvertTime(string time)
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
