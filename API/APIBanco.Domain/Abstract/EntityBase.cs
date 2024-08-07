namespace APIBanco.Domain.Abstract
{
    public class EntityBase
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        
        public EntityBase()
        {
            Key = Guid.NewGuid();
        }
    }
}
