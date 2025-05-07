namespace Entity.Model
{
    public class Forms
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        //Relacion para la tabla pivote RolFormPermission
        public List<RolFormPermissions> RolFormPermission { get; set; } = new List<RolFormPermissions>();

        //Relacione para la tabla pivote ModuleForm
        public List<ModuleForms> ModuleForm { get; set; } = new List<ModuleForms>();
    }
}
