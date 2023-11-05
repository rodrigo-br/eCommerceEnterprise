using System.Text.RegularExpressions;

namespace ECE.Core.DomainObjects
{
	public class Email
	{
		public const int EmailMaxLength = 128;
		public const int EmailMinLength = 5;

        public string EmailAddress { get; private set; }

		protected Email() { }

		public Email(string emailAddress)
		{
			if (!Validate(emailAddress)) throw new DomainException("Invalid E-mail");
			EmailAddress = emailAddress;
		}

		public static bool Validate(string emailAddress)
		{
			var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
			return regexEmail.IsMatch(emailAddress);
		}

    }
}
