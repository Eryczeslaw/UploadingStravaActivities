using System.IO;

namespace UploadingStravaActivities
{
    class EditFiles
    {
        public static void Save(string name, string date, string time)
        {
            name = EditFiles.ChangeName(name);

            string newName = EditFiles.DoNewName(date, time);

            string path = $@"C:\Users\erykh\Downloads\{name}";
            string newPath = $@"C:\Users\erykh\Downloads\{newName}";
            if (File.Exists(path))
            {
                File.Copy(path, newPath);
                File.Delete(path);
            }
        }

        private static string ChangeName(string name)
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

            tempName = date + time + ".gpx";

            return tempName;
        }

        public static void Txt(string name)
        {

        }
    }
}
