
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Comment : AuditableEntity {
        private Comment(User createdByUser, Task task, string content) : base(createdByUser) {
            Id = Guid.NewGuid();

            Task = task;
            Content = content;
        }

        public Guid Id { get; private set; }
        public Task Task { get; private set; }

        public string Content { get; private set; }

        public ICollection<CommentEmote> Emotes { get; private set; } = new List<CommentEmote>();

        public static Result<Comment> Create(User createdByUser, Task task, string content) {
            if(string.IsNullOrWhiteSpace(content)) {
                return Result<Comment>.Failure("Content is required.");
            }

            return Result<Comment>.Success(new Comment(createdByUser, task, content));
        }

        public void Update(string content) {
            Content = content;
        }

        public void AddEmote(CommentEmote commentEmote) => Emotes.Add(commentEmote);
        public void RemoveEmote(CommentEmote commentEmote) => Emotes.Remove(commentEmote);
    }
}
