using lpnu.Data;
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace lpnu.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly LpnuContext _context;

        public ContactService(LpnuContext context)
        {
            _context = context;
        }

        public async Task AddContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
            return await _context.Contacts.FindAsync(contactId);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(contact.Id))
                {
                    throw new InvalidOperationException("Contact not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
