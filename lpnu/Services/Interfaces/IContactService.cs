using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface IContactService
    {
        Task AddContactAsync(Contact contact);
        Task DeleteContactAsync(int contactId);
        Task<Contact> GetContactByIdAsync(int contactId);
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task UpdateContactAsync(Contact contact);
    }
}
