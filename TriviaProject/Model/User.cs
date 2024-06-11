using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaProject.Model;

namespace TriviaProject.Model
{
   
        public class User : INotifyPropertyChanged
        {
            private int userId;
            private string firstName;
            private string lastName;
          
            private string eMail;
            private string password;

            public string Email
            {
                get
                {
                    return eMail;
                }
                set
                {
                    eMail = value;
                    OnPropertyChanged("Email");
                }
            }
            public string Password
            {
                get
                {
                    return password;
                }
                set
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
            public int UserId
            {
                get
                {
                    return userId;
                }
                set
                {
                    userId = value;
                    OnPropertyChanged("UserId");
                }
            }

            public string FirstName
            {
                get
                {
                    return firstName;
                }
                set
                {
                    firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }

            public string LastName
            {
                get
                {
                    return lastName;
                }
                set
                {
                    lastName = value;
                    OnPropertyChanged("LastName");
                }
            }

            

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion
        }
    }
