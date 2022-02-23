namespace UploadingStravaActivities.FilesModification.AcitivityModel
{
    public class Trk
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public Trkseg trkseg { get; set; }
    }
}