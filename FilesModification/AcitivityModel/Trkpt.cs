using System.Xml.Serialization;

namespace UploadingStravaActivities.FilesModification.AcitivityModel
{
    public class Trkpt
    {
        [XmlAttribute]
        public string Lat { get; set; }

        [XmlAttribute]
        public string Lon { get; set; }
    }
}