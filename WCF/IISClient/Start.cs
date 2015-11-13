namespace IISClient
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using Strings;

    class IISConsoleClient
    {
        private static void Main()
        {
            var serviceAddress = new Uri("http://localhost:8881/strings");
            ServiceHost selfHost = new ServiceHost(
                typeof(StringsService),
                serviceAddress);

            var smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            selfHost.Description.Behaviors.Add(smb);

            selfHost.Open();
            Console.WriteLine("Running at " + serviceAddress);

            StringsServiceClient client = new StringsServiceClient();

            using (client)
            {
                var result = client.StringContainsOtherString("as", "asblablass"); // returns 2
                Console.WriteLine("Using the service: \"as\" is contained in \"asblablass\" {0} times\n", result);
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}