namespace Strings
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IStringsService
    {
        [OperationContract]
        int StringContainsOtherString(string first, string second);
    }
}