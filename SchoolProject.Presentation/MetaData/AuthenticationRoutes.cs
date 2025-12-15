namespace SchoolProject.Presentation.MetaData;

public static class AuthenticationRoutes
{
   public const string Prefix = ApiRoutes.Base + "/authentication";

   public const string SignIn = Prefix + "/signin";
   public const string RefreshToken = Prefix + "/refresh-token";
   public const string ValidateToken = Prefix + "/validate-token";

}

