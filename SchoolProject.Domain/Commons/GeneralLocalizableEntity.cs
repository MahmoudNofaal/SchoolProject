using System.Globalization;

namespace SchoolProject.Domain.Commons;

public class GeneralLocalizableEntity
{

   public string Localize(string text_Ar, string text_En)
   {
      CultureInfo culture = Thread.CurrentThread.CurrentCulture;

      if (culture.TwoLetterISOLanguageName.ToLowerInvariant() == "ar")
      {
         return text_Ar;
      }
      else if (culture.TwoLetterISOLanguageName.ToLowerInvariant() == "en")
      {
         return text_En;
      }
      else
      {
         return text_En; // Default to English if culture is not recognized
      }
   }

}
