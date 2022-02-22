using System;

namespace UploadingStravaActivities.FilesModification
{
    public class DataEdit
    {
        public static string ChangeDate(string date, string time)
        {
            string tempDate;

            string[] partsDate = date.Replace(", ", ",").Split(',', ' ');

            time = ConvertTime(time.Substring(time.Length - 8).Trim());

            string newDate = ConvertDate(partsDate);

            tempDate = $"{newDate}{time}";

            return tempDate;
        }

        private static string ConvertTime(string time)
        {
            string newTime;
            string[] partsTime = time.Split(' ', ':');

            if (partsTime[2] == "PM")
            {
                int hours = Convert.ToInt32(partsTime[0]) + 12;
                partsTime[0] = hours.ToString();
            }
            else if (Convert.ToInt32(partsTime[0]) < 10)
            {
                partsTime[0] = "0" + partsTime[0];
            }

            newTime = $"{partsTime[0]}:{partsTime[1]}:00Z";

            return newTime;
        }


        private static string ConvertDate(string[] date)
        {
            string newDate;
            switch (date[1])
            {
                case "January":
                    {
                        date[1] = "01";
                    }
                    break;
                case "February":
                    {
                        date[1] = "02";
                    }
                    break;
                case "March":
                    {
                        date[1] = "03";
                    }
                    break;
                case "April":
                    {
                        date[1] = "04";
                    }
                    break;
                case "May":
                    {
                        date[1] = "05";
                    }
                    break;
                case "June":
                    {
                        date[1] = "06";
                    }
                    break;
                case "July":
                    {
                        date[1] = "07";
                    }
                    break;
                case "August":
                    {
                        date[1] = "08";
                    }
                    break;
                case "September":
                    {
                        date[1] = "09";
                    }
                    break;
                case "October":
                    {
                        date[1] = "10";
                    }
                    break;
                case "November":
                    {
                        date[1] = "11";
                    }
                    break;
                case "December":
                    {
                        date[1] = "12";
                    }
                    break;
            }
            if (Convert.ToInt32(date[2]) < 10)
            {
                date[2] = "0" + date[2];
            }

            newDate = $"{date[3]}-{date[1]}-{date[2]}T";

            return newDate;
        }

        public static string CaculateTime(string date, int interval)
        {
            string sampleTime;
            interval = interval % 60;

            date = date.Replace('T', ' ').Replace('Z', ' ').Trim();

            string[] partsDate = date.Split(' ', '-', ':');

            int seconds = Convert.ToInt32(partsDate[5]);
            int minutes = Convert.ToInt32(partsDate[4]);
            int hours = Convert.ToInt32(partsDate[3]);
            seconds += interval;
            if (seconds > 59)
            {
                seconds -= 60;
                minutes++;
                if (minutes > 59)
                {
                    minutes -= 60;
                    hours++;
                }
            }

            if (seconds < 10)
            {
                partsDate[5] = "0" + seconds.ToString();
            }
            else
            {
                partsDate[5] = seconds.ToString();
            }
            if (minutes < 10)
            {
                partsDate[4] = "0" + minutes.ToString();
            }
            else
            {
                partsDate[4] = minutes.ToString();
            }
            partsDate[3] = hours.ToString();

            sampleTime = $"{partsDate[0]}-{partsDate[1]}-{partsDate[2]}T{partsDate[3]}:{partsDate[4]}:{partsDate[5]}Z";

            return sampleTime;
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

    }
}
