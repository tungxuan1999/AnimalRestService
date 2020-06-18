using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRestClient.BUS
{
    class AnimalBUS
    {
        private String URI = "http://animalrest.gear.host/api/Animals";
        public List<Animal> GetAll()
        {
            WebClient client = new WebClient();

            /* GetAll_HttpGet */
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            String response = client.DownloadString(new Uri(URI));
            return JsonConvert.DeserializeObject<List<Animal>>(response);
        }

        public Animal GetDetail(int id)
        {
            WebClient client = new WebClient();

            /* GetAll_HttpGet */
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            String response = client.DownloadString(new Uri(URI+"?id="+id));
            return JsonConvert.DeserializeObject<Animal>(response);
        }

        public List<Animal> GetSelectByName(String keyword)
        {
            WebClient client = new WebClient();

            /* GetSelectByName_HttpGet */
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            String response = client.DownloadString(new Uri(URI + "/SelectByName?keyword=" + keyword));
            return JsonConvert.DeserializeObject<List<Animal>>(response);
        }

        public bool Insert(Animal animal)
        {
            WebClient client = new WebClient();

            /* GetSelectByName_HttpGet */
            String data = JsonConvert.SerializeObject(animal);
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            String response = client.UploadString(new Uri(URI), "POST", data);
            return Boolean.Parse(response);
        }

        public bool Update(Animal animal)
        {
            WebClient client = new WebClient();

            /* GetSelectByName_HttpGet */
            String data = JsonConvert.SerializeObject(animal);
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            String response = client.UploadString(new Uri(URI), "PATCH", data);
            return Boolean.Parse(response);
        }

        public bool Delete(Animal animal)
        {
            WebClient client = new WebClient();

            /* GetSelectByName_HttpGet */
            String data = JsonConvert.SerializeObject(animal);
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            String response = client.UploadString(new Uri(URI), "DELETE", data);
            return Boolean.Parse(response);
        }
    }
}
