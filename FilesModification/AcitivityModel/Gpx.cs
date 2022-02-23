using System.Xml.Serialization;

namespace UploadingStravaActivities.FilesModification.AcitivityModel
{
    public class Gpx
    {
        [XmlAttribute]
        public string Creator { get; set; }
        
        [XmlAttribute]
        public string Version { get; set; }
        
        [XmlAttribute]
        public string Xmlns { get; set; }

        public Metadata Metadata { get; set; }
        
        public Trk trk { get; set; }
    }
}
