namespace Logic
{
    public class Librarian : Users
    {

        public Librarian(string name, string password) : base(name, password)
        {
        }

        public override string ToString()
        {
            return $"Labrarian {base.ToString()}";
        }
    }
}

