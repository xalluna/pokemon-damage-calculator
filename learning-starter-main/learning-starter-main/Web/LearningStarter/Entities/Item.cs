namespace LearningStarter.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    
    public class ItemCreateDto
    {
        public string Name { get; set; }
    }
    
    public class ItemGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    
    public class ItemUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}