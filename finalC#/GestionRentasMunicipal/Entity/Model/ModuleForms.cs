namespace Entity.Model
{
    public class ModuleForms
    {
        public int Id { get; set; }


        public int ModuleId { get; set; }
        public int FormId { get; set; }

        public Modules Module { get; set; }
        public Forms Form { get; set; }
    }
}
