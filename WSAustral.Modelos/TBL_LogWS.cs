using Dapper.Contrib.Extensions;

namespace WSAustral.Modelos
{
    public class TBL_LogWS
    {
        public string USUARIO { get; set; }
        public DateTime FEC_REGIS { get; set; }
        public string JSON_Request {  get; set; }
        public string JSON_Response { get; set; }
        public string END_POINT { get; set; }
    }
}
