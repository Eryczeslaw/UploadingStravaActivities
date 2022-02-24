using System;
using System.Xml.Serialization;

namespace UploadingStravaActivities.FilesModification.AcitivityModel
{
    [XmlRoot(ElementName = "metadata", Namespace = "http://www.topografix.com/GPX/1/1")]
    public class Metadata
    {
        [XmlElement("time")]
        public DateTime Time { get; set; }
    }
}