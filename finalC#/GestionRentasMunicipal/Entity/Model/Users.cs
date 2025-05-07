namespace Entity.Model
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Identification { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }


        //   //Relacion para la tabla pivote RolUsers

        public List<RolUsers> RolUser { get; set; } = new List<RolUsers>();

    }
}
