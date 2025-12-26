namespace FT.BLL.DTOs.Tour
{
    public class ReadTourDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public string? Created { get; set; }
    }
}
