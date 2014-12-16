using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adzerk.Api.Models
{
    public enum PaymentOption : int
    {
        Check = 0,
        Paypal = 1
    }

    public class Address
    {
        public string Line1;
        public string Line2;
        public string City;
        public string StateProvince;
        public string Country;
        public string PostalCode;
    }

    public class Publisher
    {
        public long Id;
        public string FirstName;
        public string LastName;
        public string CompanyName;
        public string PaypalEmail;
        public PaymentOption PaymentOption;
        public Address Address;
    }

    public class PublisherDTO
    {
        public long Id;
        public string FirstName;
        public string LastName;
        public string CompanyName;
        public string PaypalEmail;
        public int PaymentOption;
        public Address Address;

        public Publisher ToPublisher()
        {
            var p = new Publisher();

            p.Id = Id;
            p.FirstName = FirstName;
            p.LastName = LastName;
            p.CompanyName = CompanyName;
            p.PaypalEmail = PaypalEmail;
            p.PaymentOption = (PaymentOption)PaymentOption;
            p.Address = Address;

            return p;
        }
    }
}
