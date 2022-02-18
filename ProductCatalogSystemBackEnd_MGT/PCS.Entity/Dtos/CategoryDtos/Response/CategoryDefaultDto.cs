using System;

namespace PCS.Entity.Dtos.CategoryDtos.Response
{
    public class CategoryDefaultDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public int CategoryLevel { get; set; }
        public int LeftBorder { get; set; }
        public int RightBorder { get; set; }
    }
}
