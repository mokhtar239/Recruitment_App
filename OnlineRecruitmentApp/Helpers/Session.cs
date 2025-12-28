namespace OnlineRecruitmentApp.Helpers
{
    public static class Session
    {
        public static int LoggedInUserId { get; set; }
        public static string LoggedInUserName { get; set; }
        public static string UserRole { get; set; }
        public static string UserEmail { get; set; }

        public static void Clear()
        {
            LoggedInUserId = 0;
            LoggedInUserName = null;
            UserRole = null;
            UserEmail = null;
        }

        public static bool IsLoggedIn => LoggedInUserId > 0;
        public static bool IsAdmin => UserRole?.ToLower() == "admin";
        public static bool IsEmployer => UserRole?.ToLower() == "employer";
        public static bool IsJobSeeker => UserRole?.ToLower() == "job seeker";
    }
}
