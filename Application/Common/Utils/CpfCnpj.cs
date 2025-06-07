using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace contratosimples_api.Application.Common.Utils
{
	public class CpfCnpj
	{
		public static bool ValidaCpf(string cpf) {
			//remove todos os caracteres que não sejam númericos
			cpf = string.Concat(cpf.Where(char.IsDigit));

			//verifica se tem 11 digitos e se são 11 digitos repetidos
			if((cpf.Length != 11) || Regex.IsMatch(cpf, @"(\d)\1{10}") ) {
				return false;
			}

			int[] digitos = cpf.ToArray().Select(d => (int)char.GetNumericValue(d)).ToArray();

			//Soma e multiplica os digitos
			var soma1 = 0;
			var soma2 = 0;
			for (int i = 0; i < 10; i++) {
				if (i < 9)
					soma1 += digitos[i] * (10 - i);
				soma2 += digitos[i] * (11 - i);
			}

			//valida primeiro digito
			var mod1 = (soma1 * 10) % 11;
			if (mod1 == 10 || mod1 == 11)
				mod1 = 0;
			if (digitos[9] != mod1)
				return false;

			//valida segundo digito
			var mod2 = (soma2 * 10) % 11;
			if(mod2 == 10 || mod2 == 11)
				mod2 = 0;
			if (digitos[10] != mod2)
				return false;
			return true;
		}

		public static bool ValidaCnpj(string cnpj) {
			//remove todos os caracteres que não sejam númericos
			cnpj = string.Concat(cnpj.Where(char.IsDigit));

			//verifica se tem 14 digitos e se são 14 digitos repetidos
			if ((cnpj.Length != 14) || Regex.IsMatch(cnpj, @"(\d)\1{13}")) {
				return false;
			}

			int[] digitos = cnpj.ToArray().Select(d => (int)char.GetNumericValue(d)).ToArray();
			int[] mult1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
			int[] mult2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

			//soma e multiplica os digitos
			var soma1 = 0;
			var soma2 = 0;
			for (int i = 0; i < 13; i++) {
				if (i < 12)
					soma1 += digitos[i] * mult1[i];
				soma2 += digitos[i] * mult2[i];
			}

			//verifica o primeiro digito
			var mod1 = soma1 % 11;
			if (mod1 == 1)
				mod1 = 0;
			if (digitos[12] != (11 - mod1))
				return false;

			//verifica o segundo digito
			var mod2 = soma2 % 11;
			if(mod2 == 1) 
				mod2 = 0;
			if(digitos[13] != (11 -mod2)) 
				return false;

			return true;
		}

		public static bool ValidaCpfCnpj(string cpfCnpj) {
			//remove todos os caracteres que não sejam númericos
			cpfCnpj = string.Concat(cpfCnpj.Where(char.IsDigit));
			if(cpfCnpj.Length == 11)
				return ValidaCpf(cpfCnpj);
			else if (cpfCnpj.Length == 14)
				return ValidaCnpj(cpfCnpj);
			else
				return false;
		}

	}
}
