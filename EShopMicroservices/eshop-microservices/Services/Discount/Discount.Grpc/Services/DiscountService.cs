using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
    {
    public class DiscountService(DiscountContext dbContext,ILogger<DiscountService> logger):DiscountProtoService.DiscountProtoServiceBase
        {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
            {
            var coupon = await dbContext.
                coupons
                .Where(c => c.ProductName == request.ProductName).FirstOrDefaultAsync();
            if (coupon is null) coupon = new Models.Coupon { ProductName = "No Discount", Description = "No Description", Amount = 0 };

            logger.LogInformation($"Discount is retreived for Productname:{coupon.ProductName} ,  Description is : {coupon.Description} amount id :{coupon.Amount}.");
            var couponModel =  coupon.Adapt<CouponModel>();
            return couponModel;
            }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
            {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null) throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid request object"));
            await dbContext.coupons.AddAsync(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Discount created with Productname:{coupon.ProductName} ,  Description is : {coupon.Description} amount id :{coupon.Amount}.");
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
            }
        public override async  Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
            {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            dbContext.coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Discount updated with Productname:{coupon.ProductName} ,  Description is : {coupon.Description} amount id :{coupon.Amount}.");
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
            }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
            {
            var coupon = await dbContext.coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if (coupon is null) throw new RpcException(new Status(StatusCode.NotFound, $"Product {request.ProductName} not found."));
            dbContext.coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            return new DeleteDiscountResponse { Success = true };
            }
        }
    }
