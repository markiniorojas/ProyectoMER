namespace Entity.Model
{
    public class Modules
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }

        //Dependencia para la tabla pivote ModuleForm
        public List<ModuleForms> ModuleForm { get; set; } = new List<ModuleForms>();
    }
}
