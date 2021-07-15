using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using ContactApp.Models;

namespace ContactApp.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly SqlConnection _sqlConnection;

        public ContactRepository(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public async Task<ObservableCollection<Contact>> GetContactsAsync()
        {
            this.Open();

            ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();

            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = "SELECT * FROM contacts ORDER BY contactName;";
            sqlCommand.Connection = _sqlConnection;

            SqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync().ConfigureAwait(false);

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var contact = new Contact();

                    contact.Id = Guid.Parse(dataReader.GetString(0));
                    contact.Name = dataReader.GetString(1);
                    contact.PhoneNumber = dataReader.GetString(2);

                    contacts.Add(contact);
                }
            }

            await dataReader.CloseAsync();

            this.Close();

            return contacts;
        }

        public async Task<bool> AddContactAsync(Contact contact)
        {
            this.Open();

            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText =
                $"INSERT INTO contacts(id, contactName, phoneNumber) VALUES('{contact.Id.ToString()}', '{contact.Name}', '{contact.PhoneNumber}');";
            sqlCommand.Connection = _sqlConnection;

            var state = await sqlCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
            
            this.Close();

            return state > 0;
        }

        public async Task<bool> DeleteContactAsync(Guid guid)
        {
            this.Open();

            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = $"DELETE FROM contacts WHERE id = '{guid.ToString()}';";
            sqlCommand.Connection = _sqlConnection;

            var state = await sqlCommand.ExecuteNonQueryAsync().ConfigureAwait(false);

            this.Close();

            return state > 0;
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            this.Open();
            
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = $"UPDATE contacts SET contactName = '{contact.Name}', phoneNumber = '{contact.PhoneNumber}' WHERE id = '{contact.Id.ToString()}';";
            sqlCommand.Connection = _sqlConnection;

            var state = await sqlCommand.ExecuteNonQueryAsync().ConfigureAwait(false);

            this.Close();

            return state > 0;
        }

        private void Close()
        {
            if (_sqlConnection.State == ConnectionState.Open) 
                _sqlConnection.Close();
        }

        private void Open()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
                _sqlConnection.Open();
        }
    }
}