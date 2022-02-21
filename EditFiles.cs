using System;
using System.IO;

namespace UploadingStravaActivities
{
    class EditFiles
    {
        public static void Save(string name)
        {
            name = EditFiles.ChangeName(name);

            DateTime date = DateTime.Now;
            string newName = date.ToString("HH:mm:ss:ff").Replace(':', '.') + ".gpx";

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
            name = name.Replace('!', '_');
            name = name.Replace('?', '_');
            name = name.Replace(':', '_');
            name = name.Replace(')', '_');
            name = name.Replace('(', '_');
            name = name.Replace('#', '_');
            name = name.Replace("___", "_");
            name = name.Replace("__", "_");

            name += ".gpx";
            return name;
        }

        public static void Txt(string name)
        {

        }
    }
}
