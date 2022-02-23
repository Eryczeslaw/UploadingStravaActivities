using System.Xml.Serialization;
using UploadingStravaActivities.FilesModification.AcitivityModel;

namespace UploadingStravaActivities.FilesModification
{
    public class XmlEdit
    {
        public XmlEdit()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Gpx));
        }
    }
}
