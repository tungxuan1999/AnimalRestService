using AnimalRestService.DAL;
using AnimalRestService.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace AnimalRestService.Controllers
{
    public class AnimalController : ApiController
    {
        [HttpGet]
        [Route("api/Animals")]
        public IHttpActionResult GetAll()
        {
            List<Animal> animals = new AnimalDAO().SelectALL();
            return Json(animals);
        }

        [HttpGet]
        [Route("api/Animals")]
        public IHttpActionResult GetSelect(int id)
        {
            Animal animals = new AnimalDAO().SelectByID(id);
            if (animals != null)
                return Json(animals);
            else return Json(new Object());
        }

        [HttpPost]
        [Route("api/Animals")]
        public IHttpActionResult InsertAnimal(Animal animals)
        {
            return Json(new AnimalDAO().InsertAnimal(animals));
        }

        [HttpPatch]
        [Route("api/Animals")]
        public IHttpActionResult UpdateAnimals(Animal animals)
        {
            return Json(new AnimalDAO().UpdateAnimals(animals));
        }

        [HttpDelete]
        [Route("api/Animals")]
        public IHttpActionResult DeleteAnimals(Animal animals)
        {
            return Json(new AnimalDAO().DeleteAnimals(animals));
        }

        [HttpGet]
        [Route("api/Animals/SelectByName")]
        public IHttpActionResult SelectByName(String keyword)
        {
            List<Animal> animals = new AnimalDAO().SelectByName(keyword);
            return Json(animals);
        }

        [HttpPost]
        [Route("api/Animals/Image")]
        [AllowAnonymous]
        public HttpResponseMessage PostUserImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {



                            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + "8.jpg");

                            postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
    }
}