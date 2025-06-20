using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class UserDetail
    {
        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }


        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }


        private string _LoginId;

        public string LoginId
        {
            get { return _LoginId; }
            set { _LoginId = value; }
        }


    }
}
