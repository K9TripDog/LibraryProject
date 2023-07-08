namespace Logic
{
    public class Users
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserType => this.GetType().Name;
        public Users(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
