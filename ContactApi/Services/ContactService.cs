using ContactApi.Modals;
using Microsoft.EntityFrameworkCore;

namespace ContactApi.Services
{
    public class ContactService: IContactService
    {
        private readonly DbContextService _dbContextService;
        public ContactService(DbContextService dbContextService)
        {
            _dbContextService = dbContextService;
        }

        public void CreateContact(Contact contact)
        {
            _dbContextService.Contacts.Add(contact);
            _dbContextService.SaveChanges();
        }

        public async Task<Contact> GetContactById(int id)
        {
            var contact = await _dbContextService.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            return contact;
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            var contact = await _dbContextService.Contacts.ToListAsync();
            return contact;
        }

        public void UpdateContact(Contact contact)
        {
           _dbContextService.Contacts.Update(contact);
           _dbContextService.SaveChanges();
        }

        public void DeleteContact(Contact contact)
        {
            _dbContextService.Contacts.Remove(contact);
            _dbContextService.SaveChanges();
        }
    }
}
