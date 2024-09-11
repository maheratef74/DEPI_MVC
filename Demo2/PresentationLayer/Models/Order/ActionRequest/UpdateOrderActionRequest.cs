﻿using BusinessLayer.DTOs;

namespace PresentationLayer.Models.Order.ActionRequest
{
    public class UpdateOrderActionRequest
    {
        public int Rating { get; set; }
        public string Review { get; set; }
    }
    public static class UpdateOrderExtensions
    {
        public static UpdateOrderDto ToDto(this UpdateOrderActionRequest request)
            => new UpdateOrderDto
            {
                Rating = request.Rating,
                Review = request.Review,
            };
    }
}
