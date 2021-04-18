namespace Core.Entities
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int Number { get; set; }
    }
}
