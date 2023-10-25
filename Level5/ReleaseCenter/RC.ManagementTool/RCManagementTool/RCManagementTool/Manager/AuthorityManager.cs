namespace RCManagementTool.Manager
{
    public static class AuthorityManager
    {
        public static bool Islogin => Token is not null && !string.IsNullOrWhiteSpace(Token);
        public static string? Token { get; set; } = null;
        public static double Interval { get; set; } = -1;
    }
}
