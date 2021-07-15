using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ContactApp.Helpers;
using ContactApp.Models;
using ContactApp.Services;

namespace ContactApp.ViewModels
{
    public class ContactListViewModel:BindableBase
    {
        private IContactRepository _repo;
        private ObservableCollection<Contact> _contacts;

        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            private set => this.SetProperty(ref _contacts, value);
        }

        public ICommand AddContactCommand { get; private set; }
        public ICommand EditContactCommand { get; private set; }
        public ICommand DeleteContactCommand { get; private set; }

        public Contact SelectedContact { get; set; }

        public event Action AddContactEvent;
        public event Action<Contact> EditContactEvent;

        public ContactListViewModel(IContactRepository repo)
        {
            AddContactCommand = new RelayCommand(AddContact);
            EditContactCommand = new RelayCommand(EditContact);
            DeleteContactCommand = new RelayCommand(DeleteContact);
            _repo = repo;
        }

        private void DeleteContact(object obj)
        {
            _repo.DeleteContactAsync(SelectedContact.Id);
            MessageBox.Show("Contact deleted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            // refresh list

            LoadContacts();
        }

        private void EditContact(object obj)
        {
            EditContactEvent?.Invoke(SelectedContact);
        }


        private void AddContact(object obj)
        {
            AddContactEvent?.Invoke();
        }


        public async void LoadContacts()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            Contacts = await _repo.GetContactsAsync();
        }
    }
}