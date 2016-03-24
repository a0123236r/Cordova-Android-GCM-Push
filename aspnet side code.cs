IService.cs

  [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "sendNotification/{deviceId}/{message}")]
        string sendNotification(string deviceId,string message);


Service.cs

        public string SendNotification(string deviceId, string message)
        {
            var RegId = deviceId;
            var ApplicationID = "AIzaSyDJA-cWmTa0wzgXkxGAvlQ0cd6nPkI31No";
            var SENDER_ID = "268098525942";
            var value = message; //message text box

            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send"); tRequest.Method = "post";
            tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", ApplicationID)); tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
            //Data post to the Server
            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
              + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + RegId + "";
            Console.WriteLine(postData);

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;
            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse tResponse = tRequest.GetResponse(); dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();  //Get response from GCM server  

            tReader.Close(); dataStream.Close();
            tResponse.Close();

            return sResponseFromServer; //Assigning GCM response to Label text
        }

