namespace Ordering.Domain.ValueObjects;

public record Payment
    {
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public string PaymentMethod { get; } = default!;
    protected  Payment(){}
    private  Payment(string cardName, string cardNumber, string expiration, string cVV, string paymentMethod)
        {
        CardName = cardName; 
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cVV;
        PaymentMethod = paymentMethod;
        }
    public static Payment  Of(string cardName, string cardNumber, string expiration, string cVV, string paymentMethod)
        {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(cVV);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cVV.Length, 3);
        return new Payment(cardName, cardNumber, expiration, cVV, paymentMethod);
        }
    }

