using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Application.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "9e9a850f-5dd0-4969-b57c-c4d9fc2cc739";
			var writerRoleId = "9671406d-f6c4-4aea-8e85-f55e37c77d7d";

			//Create reader and writer roles
			var roles = new List<IdentityRole>
			{
				new IdentityRole()
				{
					Id = readerRoleId,
					Name = "Reader",
					NormalizedName = "READER",
					ConcurrencyStamp = readerRoleId,
				},
				new IdentityRole()
				{
					Id = writerRoleId,
					Name = "Writer",
					NormalizedName = "WRITER",
					ConcurrencyStamp = writerRoleId
				}
			};

			//seed the roles
			builder.Entity<IdentityRole>().HasData(roles);

			//create an admin user
			var adminUserId = "8a1b5b33-30d1-4c9c-b16c-a76fe078379";
			var admin = new IdentityUser()
			{
				Id = adminUserId,
				UserName = "admin@contratosimples.com.br",
				NormalizedUserName = "admin@contratosimples.com.br".ToUpper(),
				Email = "admin@contratosimples.com.br",
				NormalizedEmail = "admin@contratosimples.com.br".ToUpper(),
				ConcurrencyStamp = adminUserId,
				SecurityStamp = adminUserId,
				PasswordHash = "",
				PhoneNumber = "",
				AccessFailedCount = 0,
				EmailConfirmed = false,
				LockoutEnabled = false,
				LockoutEnd = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
			};

			//admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
			admin.PasswordHash = "AQAAAAIAAYagAAAAEI6qrEVRsGPLoHesHn+gXTEW6TgXyCIDSrAzOTendZ4GQE84jSD3NIgkp3S8VzUlMA==";

			builder.Entity<IdentityUser>().HasData(admin);


			//assign roles to admin
			var adminRoles = new List<IdentityUserRole<string>>()
			{
				new()
				{
					UserId = adminUserId,
					RoleId = readerRoleId
				},
				new()
				{
					UserId = adminUserId,
					RoleId = writerRoleId
				}
			};
			builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
		}

	}
}
