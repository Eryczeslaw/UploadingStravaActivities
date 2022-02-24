using System.Xml.Serialization;

namespace UploadingStravaActivities.FilesModification.AcitivityModel
{
    [XmlRoot(ElementName = "gpx", Namespace = "http://www.topografix.com/GPX/1/1")]
    public class Gpx
    {
        [XmlAttribute("creator")]
        public string Creator { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlElement("metadata")]
        public Metadata Metadata { get; set; }

        [XmlElement("trk")]
        public Trk Trk { get; set; }
    }
}
