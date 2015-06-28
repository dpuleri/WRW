using System.Collections;
using System.Text;
namespace WinRedditWallpaper
{
    public class DailyTriggerTime
    {
        //quick class to be able to pass around the time set

        public int hour;
        public int minute;
        public string DisplayString;
        public DailyTriggerTime()
        { }

        public int getHour()
        {
            return hour;
        }

        public int getMinute()
        {
            return minute;
        }

        public void setHour(int hour)
        {
            //working on 24 hour hour
            if (hour >= 0 && hour < 24)
            {
                this.hour = hour;
            }
            else
            {
             //fail silently and default to zero
             //okay since this is just internal
                this.hour = 0;
            }
            DisplayString = ToString();
        }

        public void setMinute(int minute)
        {
            //make sure it's a valid minute
            if (minute >= 0 && minute <= 60)
            {
                this.minute = minute;
            }
            else
            {
                this.minute = 0;
            }
            DisplayString = ToString();
        }

        public override string ToString()
        {
            string ampm = " AM";
            int strHr = hour;
            if (strHr > 12)
            {
                strHr -= 12;
                ampm = " PM";
            }
            else if (strHr == 0)
            {
                strHr = 12;
            }
            else if (strHr == 12)
            {
                ampm = " PM";
            }
            StringBuilder sb = new StringBuilder();
            if (strHr < 10)
            {
                sb.Append("0");
            }
            sb.Append(strHr);
            sb.Append(":");
            if (minute < 10)
            {
                sb.Append("0");
            }
            sb.Append(minute);
            sb.Append(ampm);
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast return false.
            DailyTriggerTime other = obj as DailyTriggerTime;
            if ((System.Object) other == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.hour == other.hour) && (this.minute == other.minute);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + hour.GetHashCode();
            hash = (hash * 7) + minute.GetHashCode();
            return hash;
        }
    }
}
