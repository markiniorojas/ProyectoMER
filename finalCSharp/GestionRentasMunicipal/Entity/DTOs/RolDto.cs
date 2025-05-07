namespace Entity.DTOs
{
    public class RolDto
    {
        public int RolId { get; set; }
        public string RolName { get; set; }

        //Mostrará el estado del rol en caso de desactivarlo o activarlo
        public bool IsDeleted { get; set; }
    }
}
