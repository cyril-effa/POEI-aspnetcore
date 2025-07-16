using TDD_BowlingGame;

namespace TDD_BowlingGame_TESTS
{
    [TestClass]
    public sealed class BowlingGameTests
    {
        [TestMethod]
        public void CreateGame_ShouldReturnNotNull()
        {
            var game = new BowlingGame();

            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void GameResult_ShouldReturn0_WhenAllFail()
        {
            var game = new BowlingGame();
            game.MakeRolls(20, 0);
            var result = game.GetScore();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GameResult_ShouldReturn300_WhenAllStrike()
        {
            var game = new BowlingGame();
            game.MakeRolls(12, 10);
            var result = game.GetScore();
            Assert.AreEqual(300, result);
        }

        [TestMethod]
        public void GameResult_ShouldReturn150_WhenAllSpare()
        {
            var game = new BowlingGame();
            game.MakeRolls(21, 5);
            var result = game.GetScore();
            Assert.AreEqual(150, result);
        }

        [TestMethod]
        public void GameResult_ShouldReturn18_WhenOneSpare()
        {
            var game = new BowlingGame();
            game.MakeRolls(1, 7);
            game.MakeRolls(1, 3);
            game.MakeRolls(1, 4);
            game.MakeRolls(17, 0);
            var result = game.GetScore();
            Assert.AreEqual(18, result);
        }

        [TestMethod]
        public void GameResult_ShouldReturn24_WhenOneStrike()
        {
            var game = new BowlingGame();
            game.MakeRolls(1, 10);
            game.MakeRolls(1, 3);
            game.MakeRolls(1, 4);
            game.MakeRolls(17, 0);
            var result = game.GetScore();
            Assert.AreEqual(24, result);
        }
    }
}
