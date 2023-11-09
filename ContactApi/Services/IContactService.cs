using ContactApi.Modals;

namespace ContactApi.Services
{
    public interface IContactService
    {
        public Task<List<Contact>> GetAllContacts();
        public Task<Contact> GetContactById(int id);
        public void UpdateContact(Contact contact);
        public void DeleteContact(Contact contact);
        public void CreateContact(Contact contact);
    }
}
