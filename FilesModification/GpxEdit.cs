using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UploadingStravaActivities.FilesModification.AcitivityModel;

namespace UploadingStravaActivities.FilesModification
{
    public class GpxEdit
    {
        public static void Update(string path, string date, string time, string movingTime)
        {
            Gpx fileGpx = Deserialize(path);

            DateTime dateTime = DataEdit.ConvertDate(date, time);
            int secondsTime = DataEdit.CalculateSeconds(movingTime);
            fileGpx = AddingTimes(fileGpx, dateTime, secondsTime);

            Serialize(path, fileGpx);
        }

        private static Gpx AddingTimes(Gpx gpx, DateTime date, int secondsTime)
        {
            int lines = gpx.Trk.Trkseg.Trkpt.Count;

            double interval = Math.Round((double)secondsTime / lines, MidpointRounding.ToPositiveInfinity);

            gpx.Metadata = new Metadata();
            gpx.Metadata.Time = date;


            for (int i = 0; i < lines; i++)
            {
                gpx.Trk.Trkseg.Trkpt[i].Time = date;
                date = date.AddSeconds(interval);
            }

            return gpx;
        }

        public static Gpx Deserialize(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Gpx));
            StreamReader streamReader = new StreamReader(path);
            XmlReader xmlReader = XmlReader.Create(streamReader);
            Gpx gpx = xml.Deserialize(xmlReader) as Gpx;
            streamReader.Close();
            return gpx;
        }

        public static void Serialize(string path, Gpx gpx)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Gpx));
            File.Delete(path);
            StreamWriter streamWriter = new StreamWriter(path);
            xml.Serialize(streamWriter, gpx);
            streamWriter.Close();
        }
    }
}
