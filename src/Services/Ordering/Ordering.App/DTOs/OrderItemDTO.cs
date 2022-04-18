<<<<<<< HEAD:src/Services/Ordering/Ordering.App/DTOs/OrderItemDTO.cs
﻿namespace FPTS.FIT.BDRD.Services.Ordering.App.DTOs
=======
﻿namespace ECom.Services.Ordering.App.DTOs
>>>>>>> bcad93d (change customer to balance service + validator behavior):src/Services/Ordering/Ordering.App/Application/DTOs/OrderItemDTO.cs
#nullable disable
{
    public class OrderItemDTO
    {
        public string ProductName { get; init; }
        public string PictureUrl { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal Discount { get; init; }
        public int Units { get; init; }
        public int ProductId { get; init; }
    }
}
