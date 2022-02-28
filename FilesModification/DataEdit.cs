using System;

namespace UploadingStravaActivities.FilesModification
{
    public class DataEdit
    {
        public static DateTime ConvertDate(string date, string time)
        {
            string[] partsDate = date.Replace(", ", ",").Split(',', ' ');
            string[] partsTime = time.Substring(time.Length - 8).Trim().Split(' ', ':');


            int year = Convert.ToInt32(partsDate[partsDate.Length - 1]);
            int month = ConvertMonth(partsDate);
            int day = Convert.ToInt32(partsDate[partsDate.Length - 2]);
            int hour = Convert.ToInt32(partsTime[0]) - 1;
            int minute = Convert.ToInt32(partsTime[1]);

            if (partsTime[2] == "PM" && hour < 11)
            {
                hour += 12;
            }
            if (partsTime[2] == "AM" && hour == 11)
            {
                hour = 0;
            }

            DateTime dateTime = new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc);

            return dateTime;
        }

        private static int ConvertMonth(string[] date)
        {
            int month = 0;
            switch (date[date.Length - 3])
            {
                case "January":
                    {
                        month = 1;
                    }
                    break;
                case "February":
                    {
                        month = 2;
                    }
                    break;
                case "March":
                    {
                        month = 3;
                    }
                    break;
                case "April":
                    {
                        month = 4;
                    }
                    break;
                case "May":
                    {
                        month = 5;
                    }
                    break;
                case "June":
                    {
                        month = 6;
                    }
                    break;
                case "July":
                    {
                        month = 7;
                    }
                    break;
                case "August":
                    {
                        month = 8;
                    }
                    break;
                case "September":
                    {
                        month = 9;
                    }
                    break;
                case "October":
                    {
                        month = 10;
                    }
                    break;
                case "November":
                    {
                        month = 11;
                    }
                    break;
                case "December":
                    {
                        month = 12;
                    }
                    break;
            }

            return month;
        }

        public static int CalculateSeconds(string movingTime)
        {
            int secondsTime = 0;

            string[] times = movingTime.Split(':');
            for (int i = 0; i < times.Length; i++)
            {
                secondsTime = secondsTime * 60 + Convert.ToInt32(times[i]);
            }

            return secondsTime;
        }

        public static string Title(string file)
        {
            string title;
            string[] partsFileName = file.Split("\\");
            title = partsFileName[partsFileName.Length - 1].Substring(0, partsFileName[partsFileName.Length - 1].Length - 4);

            return title;
        }
    }
}
