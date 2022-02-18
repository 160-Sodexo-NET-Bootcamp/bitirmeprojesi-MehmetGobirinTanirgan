using System;

namespace PCS.Entity.Dtos.UserDtos.Request
{
    public class UserBuyProductDto
    {
        public Guid ProductId { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
