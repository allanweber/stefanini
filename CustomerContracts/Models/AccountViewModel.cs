using CustomerContracts.Core;
using System.ComponentModel.DataAnnotations;

namespace CustomerContracts.Models
{
   public class AccountViewModel
    {
        [Required(ErrorMessage="Informe o E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        private string _password;
        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return this._password; }
            set { this._password = Encryption.Crypt(value); } }
    }
}
