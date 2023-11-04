using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;
using MongoDB_EF_Driver_Sample.Data;
using MongoDB_EF_Driver_Sample.Enums;
using MongoDB_EF_Driver_Sample.Models;
using System.Xml.Linq;



namespace MongoDB_EF_Driver_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet("InsertACustomers")]
        public async Task<string> InsertACustomers()
        {
            var MongoDBDataBase = new MongoClient("mongodb://localhost:27017")
                .GetDatabase("MongoDB_EF_Driver_Sample");
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>().
                UseMongoDB(MongoDBDataBase.Client,
                MongoDBDataBase.DatabaseNamespace.DatabaseName);
            //
            await using (var context = new ApplicationDBContext(dbContextOptions.Options))
            {
                var customer = new Customer
                {
                    Id = new ObjectId(),
                    Name = "Jamal",
                    shipment = Shipment.LocalDelivery,
                    ContactInfo = new()
                    {
                        ShippingAddress = new()
                        {
                            Line1 = "Barking Gate",
                            Line2 = "Chalk Road",
                            City = "Walpole St Peter",
                            Country = "Malaysia",
                            PostalCode = "PE14 7QQ"
                        },
                        BillingAddress = new()
                        {
                            Line1 = "15a Main St",
                            City = "Ailsworth",
                            Country = "UK",
                            PostalCode = "PE5 7AF"
                        },
                        Phones = new()
                        {
                            HomePhone = new() { CountryCode = 44, Number = "7877 555 555" },
                            MobilePhone = new() { CountryCode = 1, Number = "(555) 2345-678" },
                            WorkPhone = new() { CountryCode = 1, Number = "(555) 2345-678" }
                        }
                    }
                };

                context.Add(customer);
                await context.SaveChangesAsync();
            }

            return "OK";
        }


        [HttpGet("GetAllCustomers")]
        public async Task<Customer> GetAllCustomers()
        {
            var MongoDBDataBase = new MongoClient("mongodb://localhost:27017")
               .GetDatabase("MongoDB_EF_Driver_Sample");
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>().
                UseMongoDB(MongoDBDataBase.Client,
                MongoDBDataBase.DatabaseNamespace.DatabaseName);
            using (var context = new ApplicationDBContext(dbContextOptions.Options))
            {
                var customer = await context.Customers.SingleAsync(c => c.Name == "Jamal");

                return customer;
            }
        }


        
        [HttpGet("UpdateCustomer")]        
        public async Task<string> UpdateCustomers()
        {
            var MongoDBDataBase = new MongoClient("mongodb://localhost:27017")
               .GetDatabase("MongoDB_EF_Driver_Sample");
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>().
                UseMongoDB(MongoDBDataBase.Client,
                MongoDBDataBase.DatabaseNamespace.DatabaseName);
            using (var context = new ApplicationDBContext(dbContextOptions.Options))
            {
                var id =new ObjectId("654506d8735f849f4375e71c");
                
                var baxter = (await context.Customers.FindAsync(id))!;
                baxter.ContactInfo.ShippingAddress = new()
                {
                    Line1 = "Via Giovanni Miani",
                    City = "Rome",
                    Country = "IT",
                    PostalCode = "00154"
                };

                await context.SaveChangesAsync();
            }
            return "Updated";
        }
    }
}
