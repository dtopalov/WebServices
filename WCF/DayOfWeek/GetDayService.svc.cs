namespace DayOfWeek
{
    using System;

    public class GetDayService : IGetDayService
    {
        public string GetDayInBulgarian(DateTime inputDateTime)
        {
            var culture = new System.Globalization.CultureInfo("bg-BG");
            return culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
        }
    }
}
