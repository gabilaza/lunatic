
using Lunatic.Domain.Entities;


namespace Lunatic.Domain.Tests.Entities.CommentEmoteTests {
    public class UpdateTests {
        [Fact]
        public void GivenCommentEmote_WhenUpdateCommentEmote_ThenUpdatedSuccessfully() {
            // given
            var commentEmote = CommentEmote.Create(Guid.NewGuid(), Guid.NewGuid(), Emote.CRY).Value;

            // when
            commentEmote.Update(Emote.SMILE);

            // then
            Assert.Equal(Emote.SMILE, commentEmote.Emote);
        }
    }
}

