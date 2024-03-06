namespace ManagementCoffee.Model
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public DateTime DOB { get; set; }
        public string phone { get; set; }
        public int user_role { get; set; }
        public int del_flag { get; set; }

    }
}
