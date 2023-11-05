using ECE.Core.Utils;

namespace ECE.Core.DomainObjects
{
	public class Cpf
	{
		public const int CpfMaxLength = 11;
        public string Number { get; private set; }

		protected Cpf() { }

		public Cpf(string number)
		{
			if (!Validate(number)) throw new DomainException("Invalid CPF");
			Number = number;
		}

		public static bool Validate(string cpf)
		{
			cpf = cpf.DigitFilter();
			int[] firstMultiplier = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] secondMultiplier = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digit;
			int sum;
			int rest;
			if (cpf.Length != 11)
				return false;
			tempCpf = cpf.Substring(0, 9);
			sum = 0;
			for (int i = 0; i < 9; i++)
				sum += int.Parse(tempCpf[i].ToString()) * firstMultiplier[i];
			rest = sum % 11;
			if (rest < 2)
				rest = 0;
			else
				rest = 11 - rest;
			digit = rest.ToString();
			tempCpf = tempCpf + digit;
			sum = 0;
			for (int i = 0; i < 10; i++)
				sum += int.Parse(tempCpf[i].ToString()) * secondMultiplier[i];
			rest = sum % 11;
			if (rest < 2)
				rest = 0;
			else
				rest = 11 - rest;
			digit = digit + rest.ToString();
			return cpf.EndsWith(digit);
		}
	}
}
