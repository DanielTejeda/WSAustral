using Dapper.Contrib.Extensions;

namespace WSAustral.Modelos
{
    [Table("Almacen.LogRCNP")]
    public class LogRCNP
    {
        public string USUARIO { get; set; }
        public string TOKEN { get; set; }
        public DateTime FEC_REGIS { get; set; } 
        public string JSON_Request {  get; set; }
        public string JSON_Response { get; set;}
    }
}