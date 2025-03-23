using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace contratosimples_api.Application.Repositories
{
	public class TokenRepository
	{
		private readonly IConfiguration config;

		public TokenRepository(IConfiguration config)
		{
			this.config = config;
		}

		public string CreateJwtToken(IdentityUser user, IList<string> roles)
		{
			//Create claims
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email)
			};

			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			//JWT token parameters
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer : config["Jwt:Issuer"],
				audience : config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);

			//Return token
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
