using aspCart.Core.Domain.User;
using Dotnet.Sample.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Api.Domain.Services.User
{
    public interface ICustomerService
    {
        IQueryable<Customer> GetAll();
        Customer GetByEmail(string email);
        Customer GetById(Guid id);
        Task Insert(Customer customer);
        Task Update(Customer customer);
    }
    public class CustomerService : ICustomerService
    {
        private readonly IDatabase context;

        public CustomerService(IDatabase context)
        {
            this.context = context;
        }
        public IQueryable<Customer> GetAll()
        {
            return context.Customers;
        }

        public Customer GetByEmail(string email)
        {
            return context.Customers.FirstOrDefault(c => c.Email.Trim().ToLowerInvariant() == email.Trim().ToLowerInvariant());
        }

        public Customer GetById(Guid id)
        {
            return context.Customers.Find(id);
        }

        public async Task Insert(Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }

        public async Task Update(Customer customer)
        {
            context.Customers.Update(customer);
            await context.SaveChangesAsync();
        }
    }
}
