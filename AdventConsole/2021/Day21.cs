namespace AdventCode.Aoc2021
{
    public class Day21 : IPuzzleDay
    {
        //public object CalculateAnswerPuzzle1() => new Game(10, 9, 1000).Play();
        public object CalculateAnswerPuzzle1() => new Game(4, 8, 1000).Play();

        public object CalculateAnswerPuzzle2()
        {
            return 0;
        }

        private class Game
        {
            private readonly DeterministicDice dice = new();
            private readonly Player player1;
            private readonly Player player2;
            private readonly int scoreToWin;

            public Game(int startPositionPlayer1, int startPositionPlayer2, int scoreToWin)
            {
                player1 = new Player(startPositionPlayer1);
                player2 = new Player(startPositionPlayer2);
                this.scoreToWin = scoreToWin;
            }

            public static Game New(int startPositionPlayer1, int startPositionPlayer2, int scoreToWin)
            {
                return new Game(startPositionPlayer1, startPositionPlayer2, scoreToWin);
            }

            public int Play()
            {
                while (player1.Score < scoreToWin || player2.Score < scoreToWin)
                {
                    player1.Move(dice.Roll());

                    if (player1.Score >= scoreToWin)
                        break;

                    player2.Move(dice.Roll());
                }

                return Math.Min(player1.Score, player2.Score) * dice.TimesRolled;
            }
        }

        private class Player
        {
            private int currentPosition;
            public int Score { get; private set; }

            //Make all 'scores' zero based, don't use map from 1-10 but from 0-9.
            public Player(int startPosition) => currentPosition = startPosition - 1;

            public void Move(int steps)
            {
                currentPosition += steps;
                Score += currentPosition % 10 + 1;
            }
        }

        private class DeterministicDice
        {
            private int currentN = 1;
            public int TimesRolled { get; private set; }

            public int Roll()
            {
                TimesRolled += 3;
                return currentN++ + currentN++ + currentN++;
            }
        }

        private class DiracDice
        {

        }
    }
}