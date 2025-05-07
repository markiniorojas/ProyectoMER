namespace Entity.Model
{
    public class Rols
    {
        public int Id { get; set; }
        public string Name { get; set; }


        /// Propiedades de auditoría que mostrará si el rol esta activo o eliminado
        public bool IsDeleted { get; set; } = false;


        //Relacion para la tabla pivote RolUsers
        public List<RolUsers> RolUser { get; set; } = new List<RolUsers>();

        //Relacion para la tabla pivote RolFormPermission
        public List<RolFormPermissions> RolFormPermission { get; set; } = new List<RolFormPermissions>();


    }
}
