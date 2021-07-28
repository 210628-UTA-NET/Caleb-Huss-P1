using System;
using Models;
using DL;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BL
{
    public class CustomerBL : ICustomerBL
    {
        private readonly ICustomerRepository _repo;
        public CustomerBL(ICustomerRepository p_repo)
        {
            _repo = p_repo;
        }
        public Customers AddCustomer(Customers p_cust, string p_password)
        {
            Customers custAdded = _repo.AddCustomer(p_cust);
            byte[] getSalt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(getSalt);
            }
            string getHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: p_password,
                salt: getSalt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            UserLogin newUserLogin = new UserLogin()
            {
                Customer = custAdded,
                CustomerID = custAdded.CustomerID,
                hash = getHash,
                salt = Convert.ToBase64String(getSalt)
            };
            _repo.AddCredentials(newUserLogin);

            return custAdded;
        }

        public Boolean CheckCredentials(string p_email, string p_password)
        {

            Customers searchedCustomer = _repo.GetCustomer(new Customers() { Email = p_email });
            if (searchedCustomer.Email != null)
            {
                UserLogin credToCheck = _repo.GetCredentials(searchedCustomer);
                string checkHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: p_password,
                    salt: Convert.FromBase64String(credToCheck.salt),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                ));
                if ((p_email.ToLower() == searchedCustomer.Email.ToLower()) && (checkHash == credToCheck.hash))
                {
                    return true;
                }
            }

            return false;
        }

        public List<Customers> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customers GetCustomer(Customers p_cust)
        {
            if (String.IsNullOrWhiteSpace(p_cust.Email) && (p_cust.CustomerID == 0) && (p_cust.PhoneNumber == 0))
            {
                return new Customers();
            }
            return _repo.GetCustomer(p_cust);
        }
    }
}