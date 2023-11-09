using ContactApi.Controllers;
using ContactApi.Modals.Dto;
using ContactApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using Xunit;

namespace ContactApiTest
{
    public class ContactControllerTest
    {
        private readonly ContactController contactController;
        private readonly ContactService contactService;
        private readonly IConfiguration configuration;
        private readonly DbContextService dbContextService;

        public ContactControllerTest()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ContactController>();
            var optionsBuilder = new DbContextOptionsBuilder<DbContextService>().UseSqlServer("Server=;Database=ContactDB;User Id=internal;Password=;Encrypt=true;TrustServerCertificate=True");
            dbContextService = new DbContextService(optionsBuilder.Options); 
            contactService = new ContactService(dbContextService);
            configuration = new ConfigurationBuilder().AddJsonFile("appsetting.json", optional: true, reloadOnChange: true).Build();
            contactController = new ContactController(configuration, contactService, logger);
        }   

        [Fact]
        public async Task GetContactTest()
        {
            var contactId = 15;
            var result = await contactController.GetContact(contactId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllContactTest()
        {
            var result = await contactController.GetAllContacts();
            Assert.InRange(result.Count(), 0, 10);
        }

        [Fact]
        public void CreateContactTest()
        {
            var contact = new ContactDto
            {
                FirstName = "ram",
                LastName = "kumar",
                Address = "1 street",
                City = "New Jersy",
                State = "St Josh",
                Country = "US",
                Email = "ramkumar@test.com",
                PhoneNumber = "8394895012",
                PostalCode = "545589"
            };
            var result = contactController.CreateContact(contact);
            Assert.NotNull(result);
        }
    }
}