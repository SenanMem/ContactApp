using System;
using System.Windows;
using System.Windows.Input;
using ContactApp.Helpers;
using ContactApp.Models;
using ContactApp.Services;

namespace ContactApp.ViewModels
{
    public class AddEditContactViewModel:BindableBase
    {
        private IContactRepository _repo;
        private Contact _contact;

        public AddEditContactViewModel(IContactRepository repo)
        {
            _repo = repo;
            SaveCommand = new RelayCommand(SaveContact);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SaveContact(object obj)
        {
            var message = String.Empty;

            if (EditMode)
            {
                _repo.UpdateContactAsync(Contact);
                message = "Contact updated";
            }
            else
            {
                _repo.AddContactAsync(Contact);
                message = "Contact added";
            }


            MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            NavToContacts?.Invoke();
        }


        public bool EditMode { get; set; }
        private void Cancel(object obj)
        {
            NavToContacts?.Invoke();   
        }

        public Contact Contact
        {
            get => _contact;
            set => base.SetProperty(ref _contact, value);
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }


        public event Action NavToContacts;
    }
}