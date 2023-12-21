namespace Lunatic.UI.Pages.Teams.TeamPageComponents.Shared {
	public class UserItem {
		public string UserId { get; init; }
		public string Username { get; init; }
		// public string Avatar { get; init; }

		public UserItem(string userId, string username) {
			UserId = userId;
			Username = username;
		}
	}
}
