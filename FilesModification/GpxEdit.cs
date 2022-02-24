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


            Serialize(path, fileGpx);
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
