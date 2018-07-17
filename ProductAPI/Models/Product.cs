namespace WebAPI.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double? Version { get; set; }
        public double? Price { get; set; }
        public int? RAM { get; set; }
        public int? Year { get; set; }
        public int? Display { get; set; }
        public string Battery { get; set; }
        public int? Camera { get; set; }
        public string Image { get; set; }
    }
}