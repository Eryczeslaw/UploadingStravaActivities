using System;
using System.IO;
using System.Text;

namespace UploadingStravaActivities.FilesModification
{
    public class TxtEdit
    {
        public static void Update(string path, string date, string time, string movingTime)
        {
            StreamReader streamReader = File.OpenText(path);
            string[] text = streamReader.ReadToEnd().Split('\n');
            streamReader.Close();

            string startDate = DataEdit.ChangeDate(date, time);
            int secondsTime = DataEdit.CalculateSeconds(movingTime);

            StringBuilder stringBuilder = AddingTimes(text, startDate, secondsTime);

            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.Write(stringBuilder);
            streamWriter.Close();
        }

        private static StringBuilder AddingTimes(string[] text, string date, int secondsTime)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int lines = text.Length;
            double measurements = (lines - 9) / 3;

            int interval = (int)Math.Round(secondsTime / measurements, MidpointRounding.ToPositiveInfinity);

            for (int i = 0; i < text.Length - 1; i++)
            {
                stringBuilder.AppendLine(text[i]);
                if (i == 1)
                {
                    stringBuilder.AppendLine(" <metadata>");
                    stringBuilder.AppendLine($"  <time>{date}</time>");
                    stringBuilder.AppendLine(" </metadata>");
                }
                if (i > 5 && i % 3 == 1 && i < text.Length - 4)
                {
                    stringBuilder.AppendLine($"    <time>{date}</time>");
                    date = DataEdit.CaculateTime(date, interval);
                }
            }

            return stringBuilder;
        }
    }
}
