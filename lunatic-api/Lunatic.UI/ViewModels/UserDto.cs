namespace Lunatic.UI.ViewModels {
	public class UserDto {
		public Guid UserId { get; set; } = default!;
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string Username { get; set; } = default!;
		public string Password { get; set; } = default!;
		public Role Role { get; set; } = default!;
		public List<Guid>? TeamIds { get; set; }

		public override string ToString() {
			return $"UserDto(UserId: {UserId}, FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Username: {Username}, Password: {Password}, Role: {Role}, TeamIds: {string.Join(Environment.NewLine, TeamIds == null ? [] : TeamIds)})";
		}
	}
}