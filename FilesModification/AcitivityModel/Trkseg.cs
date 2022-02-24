using System.Collections.Generic;
using System.Xml.Serialization;

namespace UploadingStravaActivities.FilesModification.AcitivityModel
{
    [XmlRoot(ElementName = "trkseg", Namespace = "http://www.topografix.com/GPX/1/1")]
    public class Trkseg
    {
        [XmlElement("trkpt")]
        public List<Trkpt> Trkpt { get; set; }
    }
}