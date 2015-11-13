namespace DayOfWeek
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IGetDayService
    {
        [OperationContract]
        string GetDayInBulgarian(DateTime date);
    }
}
