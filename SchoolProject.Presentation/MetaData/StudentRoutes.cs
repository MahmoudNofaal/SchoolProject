namespace SchoolProject.Presentation.MetaData;

public static class StudentRoutes
{
   public const string Prefix = ApiRoutes.Base + "/students";

   public const string GetList = Prefix + "/list";
   public const string Paginated = Prefix + "/paginated";
   public const string GetById = Prefix + ApiRoutes.SingleRoute;
   public const string Create = Prefix + "/create";
   public const string Edit = Prefix + "/edit";
   public const string Delete = Prefix + "/delete" + ApiRoutes.SingleRoute;

}

