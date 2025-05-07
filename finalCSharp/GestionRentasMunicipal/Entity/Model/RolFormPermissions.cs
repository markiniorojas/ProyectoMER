namespace Entity.Model
{
    public class RolFormPermissions
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int PermissionId { get; set; }
        public int FormId { get; set; }

        public Rols Rol { get; set; }
        public Forms Form { get; set; }
        public Permissions Permission { get; set; }
    }
}
