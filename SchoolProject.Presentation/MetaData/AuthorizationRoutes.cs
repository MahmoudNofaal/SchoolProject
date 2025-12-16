namespace SchoolProject.Presentation.MetaData;

public static class AuthorizationRoutes
{
   public const string Prefix = ApiRoutes.Base + "/authorization";

   public const string CreateRole = Prefix + "/role/create";
   public const string EditRole = Prefix + "/role/edit";
   public const string DeleteRole = Prefix + "/role/delete" + ApiRoutes.SingleRoute;
   public const string RoleList = Prefix + "/role/list";
   public const string SingleRole = Prefix + "/role" + ApiRoutes.SingleRoute;
   public const string GetUserRoles = Prefix + "/user-roles/list" + ApiRoutes.SingleRoute;
   public const string UpdateUserRoles = Prefix + "/user-roles/update";
   public const string GetUserClaims = Prefix + "/user-claims/list" + ApiRoutes.SingleRoute;
   public const string UpdateUserClaims = Prefix + "/user-claims/update";

}

