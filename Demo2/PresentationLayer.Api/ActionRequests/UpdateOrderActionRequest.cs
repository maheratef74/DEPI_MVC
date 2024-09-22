using BusinessLayer.DTOs;

namespace PresentationLayer.Api.ActionRequests
{
    public class UpdateOrderActionRequest
    {
        public int OrderId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }

    public static class UpdateOrderActionRequestExtensions
    {
        public static UpdateOrderDto ToDto(this UpdateOrderActionRequest request)
            => new UpdateOrderDto
            {
                Id = request.OrderId,
                Rating = request.Rating,
                Review = request.Review,
            };
        
    }
}
