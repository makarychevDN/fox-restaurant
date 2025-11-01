namespace foxRestaurant
{
    public interface ICustomerEffectInstance
    {
        public void Apply(Customer customer, RestaurantEncounter encounter);
    }
}