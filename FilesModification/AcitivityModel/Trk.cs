using System.Xml.Serialization;

namespace UploadingStravaActivities.FilesModification.AcitivityModel
{
    public class Trk
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("type")]
        public int Type { get; set; }

        [XmlElement("trkseg")]
        public Trkseg Trkseg { get; set; }
    }
}