using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;

namespace JsonSerialize
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Введите ваши данные: ");
            string searchText = Console.ReadLine();
            toJsonFile(new ObjectToSer(searchText, searchInformation(searchText)));
        }

        private static void toJsonFile(ObjectToSer objectToSer)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ObjectToSer));
            try
            {
                using (FileStream fs = new FileStream(@DateTime.Now.ToString("yyyy.MM.dd, HH.mm.ss") + ".json", FileMode.OpenOrCreate))
                {
                    ser.WriteObject(fs, objectToSer);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("IOExeption\n" + e);
            }
        }

        private static IList<Result> searchInformation(string searchText)
        {
            const string apiKey = "AIzaSyBqeXZn4KLGwfrc7ORF7M3Wvm9YWPmJPfM";
            const string searchEngineId = "000642482869273596404:ceihrj9xyqw";
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            IList<Result> paging = new List<Result>();
            var listRequest = customSearchService.Cse.List(searchText);
            listRequest.Cx = searchEngineId;

            Console.WriteLine("Поиск");
            int count = 0;
            while (paging != null)
            {
                Console.WriteLine($"Страница {count}");
                listRequest.Start = count * 10 + 1;
                try
                {
                    paging = listRequest.Execute().Items;
                }
                catch (Google.GoogleApiException)
                {
                    break;
                }

                if (paging != null)
                {
                    foreach (var item in paging)
                    {
                        Console.WriteLine(item.Title + Environment.NewLine + "Link : " + item.Link +
                        Environment.NewLine + Environment.NewLine);
                        using (StreamWriter sw = new StreamWriter(@DateTime.Now.ToString("yyyy.MM.dd, HH.mm.ss") + ".txt", true))
                        {

                            sw.WriteLine(item.Title + Environment.NewLine + "Link : " + item.Link +
                    Environment.NewLine + Environment.NewLine + "\n");
                        }
                        count++;
                    }
                }
                Console.WriteLine("Выполнено");
            }
            return paging;
        }
    }
}