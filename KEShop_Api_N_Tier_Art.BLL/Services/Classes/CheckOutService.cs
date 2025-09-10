using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;

using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Stripe;
using Stripe.Checkout;
using Session = Stripe.Checkout.Session;
using SessionLineItemOptions = Stripe.Checkout.SessionLineItemOptions;
using SessionLineItemPriceDataOptions = Stripe.Checkout.SessionLineItemPriceDataOptions;
using SessionLineItemPriceDataProductDataOptions = Stripe.Checkout.SessionLineItemPriceDataProductDataOptions;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository _orderItemRepository;

        public CheckOutService(ICartRepository cartRepository, 
                               IOrderRepository orderRepository, 
                               IEmailSender emailSender,
                               IOrderItemRepository orderItemRepository


                                ) 
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
           var order = await _orderRepository.GetUserByOrderAsync(orderId);

            var subject = "";
            var body = "";
            if (order.PaymentMethod == PaymentMethodEnum.Visa)
            {

                order.Status = OrderStatusEnum.Approved;
                var carts = await _cartRepository.GetUserCartAsync(order.UserId);
                var orderItems = new List<OrderItem>();
                foreach (var cartItem in carts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        totalPrice = cartItem.Product.Price * cartItem.Count,
                        Price = cartItem.Product.Price,
                        Count = cartItem.Count
                    };
                    orderItems.Add(orderItem);
                }

                await _orderItemRepository.AddRangeAsync(orderItems);


                subject = "Payment Successful - kashop";
                body = $"<h1>thank you for your payment</h1>" +
                    $"<p>your payment for order {orderId}</p>" +
                    $"<p>total Amount : ${order.TotalAmount}";
            }
            else if (order.PaymentMethod == PaymentMethodEnum.Cash)
            {
                subject = "order placed successfully";
                body = $"<h1>thank you for your order</h1>" +
                    $"<p>your payment for order {orderId}</p>" +
                    $"<p>total Amount : ${order.TotalAmount}";
            }

            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;


        }

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest)
        {
            // الحصول على محتويات سلة المستخدم
            var cartItem = await  _cartRepository.GetUserCartAsync(UserId);

            // التحقق من أن السلة ليست فارغة
            if (!cartItem.Any())
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty."
                };
            }

            // Order 
            Order order = new Order 
                {
                UserId = UserId,
                PaymentMethod = request.PaymentMethod,
                TotalAmount = cartItem.Sum(ci => ci.Product.Price * ci.Count),
                //Status = OrderStatusEnum.Pending,
                //OrderDate = DateTime.Now
            };
            await _orderRepository.AddAsync(order);

            // معالجة الدفع بناءً على طريقة الدفع المحددة



            if (request.PaymentMethod == PaymentMethodEnum.Cash) 
            {
                // يمكن إضافة منطق معالجة الدفع النقدي هنا، مثل تحديث حالة الطلب في قاعدة البيانات.
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment will be collected upon delivery.Cash."
                };

            }

            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    // إنشاء قائمة عناصر الدفع (LineItems)
                    PaymentMethodTypes = new List<string> { "card" },
                    // ملء القائمة بعناصر سلة التسوق
                    LineItems = new List<SessionLineItemOptions>
            {
               
            },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/checkouts/Success/{order.Id}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/cancel",
                };

                foreach (var item in cartItem)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                            UnitAmount = (long)item.Product.Price,
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);

                order.PaymentId = session.Id;

                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment processed successfully.",
                    PaymentId = session.Id,
                    Url = session.Url,
                   
                };


            }

            return new CheckOutResponse
            {
                Success = false,
                Message = "Invalid payment method."
            };
        }

       
    }
}
