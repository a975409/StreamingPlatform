namespace StreamingPlatform.Framework
{
    public static class DateTimeExpansion
    {
        /// <summary>
        /// 取得指定DateTime的UnixTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetUnixTimeSecByDateTime(this DateTime dateTime)
        {
            return GetUnixTimeSecByDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        /// <summary>
        /// 取得指定DateTime的UnixTime
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">時</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <returns></returns>
        private static int GetUnixTimeSecByDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            DateTime dateTime = new DateTime(year, month, day, hour, minute, second);

            return (int)dateTime.ToUniversalTime()//將目前 DateTime 物件的值轉換成UTC時間，會依據標記的 Kind 屬性而做轉換。
                                                  //DateTimeKind.Utc=>標記該 DateTime 為 Utc時間
                                                  .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}
