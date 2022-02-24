using System;
using System.Xml.Serialization;

namespace UploadingStravaActivities.FilesModification.AcitivityModel
{
    [XmlRoot(ElementName = "trkpt", Namespace = "http://www.topografix.com/GPX/1/1")]
    public class Trkpt
    {
        [XmlAttribute("lat")]
        public string Lat { get; set; }

        [XmlAttribute("lon")]
        public string Lon { get; set; }

        [XmlElement("ele")]
        public double Ele { get; set; }

        [XmlElement("time")]
        public DateTime Time { get; set; }
    }
}