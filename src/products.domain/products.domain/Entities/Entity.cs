namespace products.domain.Entities
{
    public abstract class Entity
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid().ToString().Substring(0,8);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
