
using Utils;

namespace Entities {
    public class Project {
        private Project(string title, string description) {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<Issue>? Issues { get; private set; }

        public static Result<Project> New(string title, string description) {
            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Project>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Project>.Failure("Description is required.");
            }

            return Result<Project>.Success(new Project(title, description));
        }

        public void AddIssue(Issue issue) {
            if(Issues == null) {
                Issues = new List<Issue>();
            }

            Issues.Add(issue);
        }
    }
}
