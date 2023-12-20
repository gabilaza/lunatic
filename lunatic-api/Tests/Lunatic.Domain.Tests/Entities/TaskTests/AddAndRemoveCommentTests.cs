
using Lunatic.Domain.Entities;
using Task = Lunatic.Domain.Entities.Task;


namespace Lunatic.Domain.Tests.Entities.TaskTests {
    public class AddAndRemoveCommentTests {
        public const string TaskTitle = "Lunatic";
        public const string TaskDescription = "Lunatic Description";

        public const string CommentContent = "Lunatic";

        [Fact]
        public void GivenTaskWithProject_WhenAddAndRemoveProject_ThenProjectAddedAndRemovedSuccessfully() {
            // given
            var task = Task.Create(Guid.NewGuid(), Guid.NewGuid(), TaskTitle, TaskDescription, TaskPriority.LOW).Value;
            var comment = Comment.Create(Guid.NewGuid(), Guid.NewGuid(), CommentContent).Value;
            var commentId = Guid.NewGuid();

            // when
            task.AddComment(comment);
            task.AddComment(commentId);
            task.RemoveComment(comment);
            task.RemoveComment(commentId);

            // then
            Assert.DoesNotContain(comment.CommentId, task.CommentIds);
            Assert.DoesNotContain(commentId, task.CommentIds);
        }
    }
}

