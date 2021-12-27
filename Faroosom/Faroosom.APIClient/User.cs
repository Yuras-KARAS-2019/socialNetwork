namespace Faroosom.APIClient
{
    record User
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
    }
}
