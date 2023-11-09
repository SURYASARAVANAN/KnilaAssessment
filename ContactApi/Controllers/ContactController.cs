using ContactApi.Modals;
using ContactApi.Modals.Dto;
using ContactApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers
{
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;
        public ContactController(IConfiguration configuration, IContactService contactService, ILogger<ContactController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _contactService = contactService;
        }

        [HttpGet("/api/getAllContacts")]
        public async Task<List<ContactDto>> GetAllContacts()
        {
            try
            {
                var contacts = await _contactService.GetAllContacts();
                if (contacts != null)
                {

                    var contactResults = contacts.OrderByDescending(c => c.CreationTime).Select(c => new ContactDto { Address = c.Address, Id = c.Id, City = c.City, Country = c.Country, Email = c.Email, FirstName = c.FirstName, LastName = c.LastName, PhoneNumber = c.PhoneNumber, PostalCode = c.PostalCode, State = c.State }).ToList();
                    _logger.LogInformation("GetAllContacts List the data");
                    return contactResults;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred in GetAllContacts", ex.Message);
                return null;
            }
        }

        [HttpPost("/api/createContacts")]
        public ContactDto CreateContact(ContactDto contact)
        {
            try
            {
                var newContact = new Contact
                {
                    Address = contact.Address,
                    City = contact.City,
                    Country = contact.Country,
                    CreationTime = DateTime.UtcNow,
                    Email = contact.Email,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    PhoneNumber = contact.PhoneNumber,
                    PostalCode = contact.PostalCode,
                    State = contact.State
                };
                _contactService.CreateContact(newContact);
                return new ContactDto { IsSuccess = true };            
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred in CreateContact", ex.Message);
                return new ContactDto { IsSuccess = false };
            }
        }

        [HttpGet("/api/getContact")]
        public async Task<ContactDto> GetContact(int id)
        {
            try
            {
                var contacts = await _contactService.GetContactById(id);
                if (contacts != null)
                {
                    var contactResult = new ContactDto { Address = contacts.Address, Id = contacts.Id, City = contacts.City, Country = contacts.Country, Email = contacts.Email, FirstName = contacts.FirstName, LastName = contacts.LastName, PhoneNumber = contacts.PhoneNumber, PostalCode = contacts.PostalCode, State = contacts.State, IsSuccess = true };
                    return contactResult;
                }
                return new ContactDto { IsSuccess = false };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred in GetContact", ex.Message);
                return new ContactDto {IsSuccess = false};
            }
        }

        [HttpPut("/api/updateContact")]
        public async Task<ContactDto> UpdateContact(ContactDto updatedContact)
        {
            try
            {
                var existingContact = await _contactService.GetContactById(updatedContact.Id);
                if (existingContact != null)
                {
                    existingContact.Country = updatedContact.Country;
                    existingContact.Email = updatedContact.Email;
                    existingContact.FirstName = updatedContact.FirstName;
                    existingContact.LastName = updatedContact.LastName;
                    existingContact.PhoneNumber = updatedContact.PhoneNumber;
                    existingContact.Address = updatedContact.Address;
                    existingContact.City = updatedContact.City;
                    existingContact.PostalCode = updatedContact.PostalCode;
                    existingContact.State = updatedContact.State;
                    _contactService.UpdateContact(existingContact);
                    updatedContact.IsSuccess = true;
                    return updatedContact;
                }
                return new ContactDto { IsSuccess = false };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred in UpdateContact", ex.Message);
                return new ContactDto { IsSuccess = false};
            }
        }

        [HttpDelete("/api/deleteContact")]
        public async Task<ContactDto> DeleteContact(int contactId)
        {
            try
            {
                var existingContacts = await _contactService.GetContactById(contactId);
                if (existingContacts != null)
                {
                    _contactService.DeleteContact(existingContacts);
                    return new ContactDto { IsSuccess = true };
                }
                return new ContactDto { IsSuccess = false };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred in DeleteContact", ex.Message);
                return new ContactDto { IsSuccess = false };
            }
        }
    }
}
