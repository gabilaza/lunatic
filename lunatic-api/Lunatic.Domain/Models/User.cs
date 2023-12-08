using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using Lunatic.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunatic.Domain.Models {
    [Table("AspNetUsers")]
    public class User : IdentityUser {
        private User() {
            // EF Core
        }

        private User(string? firstName, string lastName, string email, string puserName, string password, Role role) {
            CreatedDate = DateTime.UtcNow;
            //Guid = new Guid(base.Id);
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            //Username = username;
            UserName = puserName;
            PasswordHash = new PasswordHasher<User>().HashPassword(this, password);
            //Password = password;
            Role = role;
            IsDeleted = false;
        }

        public DateTime CreatedDate { get; private set; }
        //public Guid Guid { get; private set; }
        public bool IsDeleted { get; private set; }
        public string? FirstName { get; private set; }
        public string LastName { get; private set; }
        //public string Email { get; private set; }
        //public string Username { get; private set; }
        //public string Password { get; private set; }
        public Role Role { get; private set; }
        public List<Team>? Teams { get; private set; }

        public static Result<User> Create(string? firstName, string lastName, string email, string username, string password, string role) {
            //if (string.IsNullOrWhiteSpace(firstName)) {
            //    return Result<User>.Failure("First name is required.");
            //}

            if (string.IsNullOrWhiteSpace(lastName)) {
                return Result<User>.Failure("Last name is required.");
            }

            if (string.IsNullOrWhiteSpace(email)) {
                return Result<User>.Failure("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(username)) {
                return Result<User>.Failure("Username is required.");
            }

            if (string.IsNullOrWhiteSpace(password)) {
                return Result<User>.Failure("Password is required.");
            }

            Role register_role = role switch {
                UserRoles.Admin => Role.ADMIN,
                UserRoles.User => Role.USER,
                _ => throw new NotImplementedException(),
            };

            return Result<User>.Success(new User(firstName, lastName, email, username, password, register_role));
        }

        public void Update(string? firstName, string lastName, string email, string username, string password, Role role, List<Team> teams) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = username;
            PasswordHash = new PasswordHasher<User>().HashPassword(this, password);
            Role = role;
            Teams = teams;
        }

        public void AddTeam(Team team) {
            if (Teams == null) {
                Teams = new List<Team>();
            }

            Teams.Add(team);
        }

        public void RemoveTeam(Team team) {
               if (Teams == null) {
                return;
            }

            Teams.Remove(team);
        }

        public void MarkAsDeleted() {
            IsDeleted = true;
        }
    }
}
