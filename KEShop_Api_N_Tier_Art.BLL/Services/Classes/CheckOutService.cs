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







namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;

        public CheckOutService(ICartRepository cartRepository) 
        {
            _cartRepository = cartRepository;
        }
        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest)
        {
            // الحصول على محتويات سلة المستخدم
            var cartItem =  _cartRepository.GetUserCart(UserId);

            // التحقق من أن السلة ليست فارغة
            if (!cartItem.Any())
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty."
                };
            }
          


            if (request.PaymentMethod == "Cash") 
            {
                // يمكن إضافة منطق معالجة الدفع النقدي هنا، مثل تحديث حالة الطلب في قاعدة البيانات.
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment will be collected upon delivery.Cash."
                };

            }

            if (request.PaymentMethod == "Visa")
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
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/success",
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

                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment processed successfully.",
                    Url = session.Url
                    //SessionId = session.Id,
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
