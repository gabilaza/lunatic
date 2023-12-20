
using Lunatic.Domain.Entities;


namespace Lunatic.Domain.Tests.Entities.CommentTests {
    public class AddAndRemoveEmoteTests {
        public const string Content = "Lunatic Content";

        [Fact]
        public void GivenCommentWithProject_WhenAddAndRemoveProject_ThenProjectAddedAndRemovedSuccessfully() {
            // given
            var comment = Comment.Create(Guid.NewGuid(), Guid.NewGuid(), Content).Value;
            var commentEmote = CommentEmote.Create(Guid.NewGuid(), Guid.NewGuid(), Emote.CRY).Value;
            var commentEmoteId = Guid.NewGuid();

            // when
            comment.AddEmote(commentEmote);
            comment.AddEmote(commentEmoteId);
            comment.RemoveEmote(commentEmote);
            comment.RemoveEmote(commentEmoteId);

            // then
            Assert.DoesNotContain(commentEmote.CommentEmoteId, comment.EmoteIds);
            Assert.DoesNotContain(commentEmoteId, comment.EmoteIds);
        }
    }
}

