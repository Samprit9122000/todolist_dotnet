namespace Todolist.Api.Responses
{
    public class Response
    {
        private List<dynamic> data;
        public string clientMessage ;
        public string devMessege ;

        public Response(List<dynamic> data, string clientMessage="", string devMessege="") { 
            
            this.data = data;
            this.clientMessage = clientMessage; 
            this.devMessege = devMessege;
        }
    }
}
