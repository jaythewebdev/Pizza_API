using PizzaApi.Interfaces;
using PizzaApi.Models;
using System.Diagnostics;

namespace PizzaApi.Services
{
    public class PizzaRepo : IPizza<int, Pizza>
    {
        private readonly PizzaContext _pizzaContext;
        public PizzaRepo(PizzaContext pizzaContext)
        {
            _pizzaContext= pizzaContext;
        }
        public Pizza Add(Pizza item)
        {
            try
            {
                _pizzaContext.Pizzas.Add(item);
                _pizzaContext.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(item);
            }
            return null;
        }

        public ICollection<Pizza> GetAll()
        {
            var pizzas = _pizzaContext.Pizzas.ToList();
            return pizzas;
        }

    }
}
