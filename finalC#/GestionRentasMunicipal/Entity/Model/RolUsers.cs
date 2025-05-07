namespace Entity.Model
{
    public class RolUsers
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int UserId { get; set; }
 
        //Relacion con la tabla Rol
        public Rols Rol { get; set; }

        //Relacion con la tabla User
        public Users User { get; set; }

    }
}
