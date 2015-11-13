namespace Client
{
    using System;

    using Client.DayOfWeek;

    class ServicesTest
    {
        private static void Main()
        {
            GetDayServiceClient client = new GetDayServiceClient();
            using (client)
            {
                var result = client.GetDayInBulgarian(DateTime.Now);
                Console.WriteLine(result);
            }
        }
    }
}
