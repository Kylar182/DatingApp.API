namespace DatingApp.API.Helpers
{
  public static class TrimFixer
  {
    public static string TrimFix(this string rawString)
    {
      if (!string.IsNullOrWhiteSpace(rawString))
      {
        return rawString.Trim();
      }
      return null;
    }
  }
}