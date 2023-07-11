using PizzaApi.Interfaces;
using PizzaApi.Models;
using PizzaApi.Models.DTO;

namespace PizzaApi.Services
{
    public class PizzaServices
    {
        private readonly IPizza<int, Pizza> _iPizza;

        public PizzaServices(IPizza<int,Pizza> iPizza) 
        { 
            _iPizza=iPizza;
        }
        public ICollection<Pizza> GetAll()
        {
            var pizzas= _iPizza.GetAll().ToList();
            if (pizzas.Count> 0)
            {
                return pizzas;
            }
            return null;
        }
        public Pizza Add(Pizza pizza)
        {
            var pizzaNew = _iPizza.Add(pizza);
            if (pizzaNew != null)
            {
                return pizzaNew;
            }
            return null;
        }

        public ICollection<Pizza> GetProduct(RangeDTO rangeDTO)
        {
            var pizzasList = _iPizza.GetAll().ToList();
            var pizzas = pizzasList.Where(p => p.Price >= rangeDTO.Min && p.Price <= rangeDTO.Max).ToList();
            if (pizzas.Count > 0)
            {
                return pizzas;
            }
            return null;
        }
    }
}
