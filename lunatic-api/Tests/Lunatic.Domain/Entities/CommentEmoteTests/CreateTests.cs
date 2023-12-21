
using Lunatic.Domain.Entities;


namespace Tests.Lunatic.Domain.Entities.CommentEmoteTests {
    public class CreateTests {
        [Fact]
        public void GivenValidCommentEmote_WhenCreateCommentEmote_ThenSuccessResult() {
            // given

            // when
            var commentEmoteResult = CommentEmote.Create(Guid.NewGuid(), Guid.NewGuid(), Emote.CRY);

            // then
            Assert.True(commentEmoteResult.IsSuccess);
        }

        [Fact]
        public void GivenInvalidCommentEmote_WhenCreateCommentEmote_ThenFailureResult() {
            // given

            // when
            var commentEmoteResult = CommentEmote.Create(default, Guid.NewGuid(), Emote.CRY);

            // then
            Assert.False(commentEmoteResult.IsSuccess);
        }

        [Fact]
        public void GivenInvalidCommentEmote_WhenCreateCommentEmote_ThenFailureResult_2() {
            // given

            // when
            var commentEmoteResult = CommentEmote.Create(Guid.NewGuid(), default, Emote.CRY);

            // then
            Assert.False(commentEmoteResult.IsSuccess);
        }
    }
}

