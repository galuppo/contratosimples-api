using contratosimples_api.Common.Models.Entities;
using contratosimples_api.Management.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Management.Data
{
	public class AppManagementDbContext : IdentityDbContext
	{
		public DbSet<AppCliente> AppCliente { get; set; }

		public AppManagementDbContext(DbContextOptions<AppManagementDbContext> options) : base(options) {
			base.Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);

			var readerRoleId = "9e9a850f-5dd0-4969-b57c-c4d9fc2cc739";
			var writerRoleId = "9671406d-f6c4-4aea-8e85-f55e37c77d7d";
			var appAdminRoleId = "5f5d6c20-6612-4f03-bab3-5ab047a3d572";

			//Create reader and writer roles
			var roles = new List<UsuarioRole>
			{
				new UsuarioRole()
				{
					Id = readerRoleId,
					Name = UsuarioRole.READER_ROLE,
					NormalizedName = UsuarioRole.READER_ROLE.ToUpper(),
					ConcurrencyStamp = readerRoleId,
				},
				new UsuarioRole()
				{
					Id = writerRoleId,
					Name = UsuarioRole.WRITER_ROLE,
					NormalizedName = UsuarioRole.WRITER_ROLE.ToUpper(),
					ConcurrencyStamp = writerRoleId
				},
				new UsuarioRole()
				{
					Id = appAdminRoleId,
					Name = UsuarioRole.APP_ADMIN,
					NormalizedName = UsuarioRole.APP_ADMIN.ToUpper(),
					ConcurrencyStamp = appAdminRoleId
				}
			};

			//seed the roles
			builder.Entity<UsuarioRole>().HasData(roles);

			//create an admin user
			var adminUserId = "8a1b5b33-30d1-4c9c-b16c-a76fe078379";
			var admin = new Usuario() {
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

			builder.Entity<Usuario>().HasData(admin);


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
