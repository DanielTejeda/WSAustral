using WSAustral.Modelos;

namespace WSAustral.Constants
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() {Username = "EXT_SGS01", Password ="AimDA6DBFbDFnA#3I8Kv", Rol="Administrador", EmailAddress="empresa@sgs.com", FirstName="SGS", LastName="01" },
            new UserModel() {Username = "EMPRESA2", Password = "5678", Rol = "Vendedor", EmailAddress = "csalas@austral.com.pe", FirstName = "Carlos", LastName = "Salas" },
        };
    }
}
