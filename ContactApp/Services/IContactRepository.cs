using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ContactApp.Models;

namespace ContactApp.Services
{
    public interface IContactRepository
    {
        Task<ObservableCollection<Contact>> GetContactsAsync();
        Task<bool> AddContactAsync(Contact contact);
        Task<bool> DeleteContactAsync(Guid guid);
        Task<bool> UpdateContactAsync(Contact contact);
    }
}