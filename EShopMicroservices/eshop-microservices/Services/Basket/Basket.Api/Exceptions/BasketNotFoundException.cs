namespace Basket.Api.Exceptions;

public class BasketNotFoundException:NotFoundException
    {
    public BasketNotFoundException(string UserName):base("User Name",UserName)
        {}
    }

