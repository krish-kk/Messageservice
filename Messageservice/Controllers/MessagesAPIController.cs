using Messageservice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Optimization;

namespace Messageservice.Controllers
{
    public class MessagesAPIController : ApiController
    {
        // GET: api/message
        
        public IEnumerable<Messages> Get()
        {
            List<Messages> message = null;
            MessageModel myModel = new MessageModel();
            message = myModel.GetAllData();

            return message;
        } 
        // GET: api/message/5
        public IEnumerable<Messages> Get(int id)
        {
            List<Messages> message = null;
            MessageModel myModel = new MessageModel();
            message = myModel.GetDataById(id);
            return message;       
        }

            // POST: api/message
         public HttpResponseMessage Post([FromBody] Messages msg)
        {
            HttpResponseMessage response = Request.CreateResponse<Messages>(HttpStatusCode.Created, msg);

            // Delivery Date should be greater than  current date should be scheduled successfully otherwise message will not be scheduled
            if (Convert.ToDateTime(msg.delivered_dt) > DateTime.Now)
            {
                List<Messages> message = null;
                MessageModel myModel = new MessageModel();
                message = myModel.CreateMessage(msg);
                                
                response.StatusCode = HttpStatusCode.Accepted;
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;

            //List<Messages> message = null;
            //MessageModel myModel = new MessageModel();
            //message = myModel.CreateMessage(msg);
            
            //return message;
        }

        // PUT: api/message/5
        public IEnumerable<Messages> Put([FromBody]Messages msg)
        {
            List<Messages> message = null;
            MessageModel myModel = new MessageModel();
            message = myModel.UpdateMessage(msg);

            return message;
        }

        // DELETE: api/message/5
        public IEnumerable<Messages> Delete(int id)
        {
            List<Messages> message = null;
            MessageModel myModel = new MessageModel();

            message = myModel.GetDataById(id);
            myModel.DeleteMessage(id);
            return message;
        }
    }
}


