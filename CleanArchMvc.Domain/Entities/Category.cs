using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }
        public ICollection<Product> Products { get; private set; }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidationDomain(name);
        }

        public Category(string name)
        {
            ValidationDomain(name);
        }

        public void Update(string name)
        {
            ValidationDomain(name);
        }

        private void ValidationDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Nmae is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 charecters");

            Name = name;
        }
    }
}
