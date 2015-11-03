namespace MusicCatalog.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    using MusicCatalog.Data;
    using MusicCatalog.ServiceApi.Models;

    using Newtonsoft.Json;

    using Formatting = Newtonsoft.Json.Formatting;

    class CatalogOperations
    {
        static void Main()
        {
            var data = new MusicCatalogData();

            using (var client = new HttpClient())
            {

                Console.Write("Enter the port of the localhost: ");
                var port = int.Parse(Console.ReadLine().Trim());
                var connection = new Uri(string.Format("http://localhost:{0}/", port));
                client.BaseAddress = connection;

                PostRequests(client);

                PrintJson(client, connection, "Artists");
                PrintJson(client, connection, "Albums");
                PrintJson(client, connection, "Songs");

                PrintXml(connection, "Artists");
                PrintXml(connection, "Albums");
                PrintXml(connection, "Songs");
                Console.ReadLine();
            }
        }

        private static void PrintXml(Uri connection, string collectionName)
        {
            HttpClient xmlClient = new HttpClient();

            xmlClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            Task<string> requestXml = GetResponse(xmlClient, connection, "api/" + collectionName);
            var responseAsXml = requestXml.Result;
            Console.WriteLine(collectionName + " XML:\n{0}", FormatXml(responseAsXml));
        }

        private static void PrintJson(HttpClient client, Uri connection, string collectionName)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Task<string> response = GetResponse(client, connection, "api/" + collectionName);
            var resultAsJson = response.Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<object>>(resultAsJson);
            Console.WriteLine(collectionName + " JSON:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        private static async void PostRequests(HttpClient client)
        {
            var sampleArtist = new ArtistServiceModel
            {
                Name = "Pesho Testa",
                Country = "Bulgaria",
                DateOfBirth = new DateTime(1989, 10, 10)
            };

            var sampleSong = new SongServiceModel
            {
                Title = "Pesen za Pesho",
                ReleasedOn = new DateTime(2015, 10, 10),
                Artist = sampleArtist.Name,
                Genre = "Punk"
            };

            var sampleAlbum = new AlbumServiceModel
            {
                Title = "Pesho 2",
                Genre = "Psychedelic",
                Artists = new List<string> { "Pesho Testa" },
                Songs = new List<string> { "Pesen za Pesho" }
            };

            var httpResponseArtist = await client.PostAsJsonAsync("api/Artists", sampleArtist);
            var httpResponseSong = await client.PostAsJsonAsync("api/Songs", sampleSong);
            var httpResponseAlbum = await client.PostAsJsonAsync("api/Albums", sampleAlbum);
        }

        public static async Task<string> GetResponse(HttpClient httpClient, Uri connection, string path)
        {
            var response = await httpClient.GetAsync(connection + "/" + path);
            var contents = await response.Content.ReadAsStringAsync();

            return contents;
        }

        public static string FormatXml(string xml)
        {
            string Result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(xml);

                writer.Formatting = System.Xml.Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                string FormattedXml = sReader.ReadToEnd();

                Result = FormattedXml;
            }
            catch (XmlException)
            {
            }

            mStream.Close();
            writer.Close();

            return Result;
        }
    }
}
