using System;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ContactApp.Helpers;
using ContactApp.Models;
using ContactApp.Properties;
using ContactApp.Services;

namespace ContactApp.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        private ContactListViewModel _contactListViewModel;
        private AddEditContactViewModel _addEditContactViewModel;


        private BindableBase _currentViewModel;

        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            private set => this.SetProperty(ref _currentViewModel, value);
        }
        

        public MainWindowViewModel()
        {
            var repo = new ContactRepository(Resources.ResourceManager.GetString("ConnectionString"));

            _addEditContactViewModel = new AddEditContactViewModel(repo);
            _addEditContactViewModel.NavToContacts += NavToContacts;

            _contactListViewModel = new ContactListViewModel(repo);
            _contactListViewModel.AddContactEvent += NavToAddContactView;
            _contactListViewModel.EditContactEvent += NavToEditContactView;

            CurrentViewModel = _contactListViewModel;
        }

        private void NavToEditContactView(Contact obj)
        {
            _addEditContactViewModel.Contact = obj;
            _addEditContactViewModel.EditMode = true;

            CurrentViewModel = _addEditContactViewModel;
        }

        private void NavToAddContactView()
        {
            _addEditContactViewModel.Contact = new Contact()
            {
                Id = Guid.NewGuid(),
            };
            _addEditContactViewModel.EditMode = false;

            CurrentViewModel = _addEditContactViewModel;
        }

        private void NavToContacts()
        {
            CurrentViewModel = _contactListViewModel;
        }
    }
}