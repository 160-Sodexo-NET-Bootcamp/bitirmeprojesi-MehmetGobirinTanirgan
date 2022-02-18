using System;

namespace PCS.Core.Utils.Concrete
{
    public static class ExceptionMessages
    {
        public static string A1(string emailAddress) => $"Refresh login attempt failed. Email is not valid. Email Address: {emailAddress}";
        public static string A3(Guid UnauthUserId, Guid AccountOwnerId) => $"Unauthorized user detected. Tried to reset another user's password." +
            $"Unauthrized UserId: {UnauthUserId}, Account OwnerId: {AccountOwnerId}";
        public static string A4(Guid userId) => $"Password reset attempt failed. Current password does not match. UserId: {userId}";
        public static string O1(Guid userId, Guid productId) => $"Users cannot make an offer to their own product. " +
            $"UserId: {userId}, ProductId: {productId}";
        public static string O2(Guid userId, Guid productId) => $"Cannot make an offer to this product." +
            $"UserId: {userId}, ProductId: {productId}";
        public static string O3(Guid userId, Guid productId) => $"User already has an offer for this product." +
            $"UserId: {userId}, ProductId: {productId}";
        public static string O4(Guid userId, Guid offerId) => $"Unauthorized user detected. Tried to withdraw another user's offer." +
            $"UserId: {userId}, OfferId: {offerId}";
        public static string O5(Guid userId, Guid offerId) => $"Offer is not in process anymore." +
           $"UserId: {userId}, OfferId: {offerId}";
        public static string O6(Guid userId, Guid offerId) => $"Unauthorized user detected. Tried to accept another user's offer." +
            $"UserId: {userId}, OfferId: {offerId}";
        public static string O7(Guid userId, Guid offerId) => $"Unauthorized user detected. Tried to reject another user's offer." +
            $"UserId: {userId}, OfferId: {offerId}";
        public static string O8(Guid userId, Guid productId) => $"Product sold, can not make an offer to this product." +
           $"UserId: {userId}, ProductId: {productId}";

        public static string P1() => "Unexpected situation. Category, brand or color table is deleted.";
        public static string U1(Guid userId, Guid productId) => $"Users can not buy their own products. UserId: {userId}, ProductId: {productId}";
    }
}
