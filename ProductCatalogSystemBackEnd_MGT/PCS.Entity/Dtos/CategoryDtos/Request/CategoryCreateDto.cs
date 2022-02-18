namespace PCS.Entity.Dtos.CategoryDtos.Request
{
    public class CategoryCreateDto
    {
        public string CategoryName { get; set; }
        public int CategoryLevel { get; set; }
        public int LeftBorder { get; set; }
        public int RightBorder { get; set; }
    }
}
