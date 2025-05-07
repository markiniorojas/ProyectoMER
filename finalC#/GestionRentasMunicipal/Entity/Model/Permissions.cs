namespace Entity.Model
{
    public class Permissions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        //Relacion con la tabla RolFormPermissions
        public List<RolFormPermissions> RolFormPermission { get; set; } = new List<RolFormPermissions>();

    }
}
