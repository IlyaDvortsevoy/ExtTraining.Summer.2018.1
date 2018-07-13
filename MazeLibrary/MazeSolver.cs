using System;

namespace MazeLibrary
{
    public class MazeSolver
    {
        private int[,] mazeArray;
        private int startX;
        private int startY;

        public MazeSolver(int[,] mazeModel, int startX, int startY)
        {
            ValidateArray(mazeModel);
            ValidatePositions(mazeModel, startX, startY);

            mazeArray = mazeModel;
            this.startX = startX;
            this.startY = startY;
        }

        public int[,] MazeWithPass() => mazeArray;

        public void PassMaze()
        {
            int xPos = startX;
            int yPos = startY;
            int count = 1;

            while (true)
            {
                if (mazeArray.GetLength(0) >= xPos + 1 &&
                    mazeArray[xPos + 1, yPos] == 0)
                {
                    mazeArray[++xPos, yPos] = count++;
                }
                else if (xPos - 1 >= 0 &&
                    mazeArray[xPos - 1, yPos] == 0)
                {
                    mazeArray[--xPos, yPos] = count++;
                }
                else if (mazeArray.GetLength(1) >= yPos + 1 &&
                    mazeArray[xPos, yPos + 1] == 0)
                {
                    mazeArray[xPos, ++yPos] = count++;
                }
                else if (yPos - 1 >= 0 &&
                    mazeArray[xPos, yPos - 1] == 0)
                {
                    mazeArray[xPos, --yPos] = count++;
                }

                if (mazeArray[xPos + 1, yPos] != 0
                    && mazeArray[xPos - 1, yPos] != 0
                    && mazeArray[xPos, yPos + 1] != 0
                    && mazeArray[xPos, yPos - 1] != 0)
                {
                    break;
                }
            }
        }

        #region Private Helper Methods
        private void ValidateArray(int[,] mazeModel)
        {
            if (mazeModel == null)
            {
                throw new ArgumentNullException();
            }
        }

        private void ValidatePositions(int[,] mazeModel, int startX, int startY)
        {
            if (startX > mazeModel.GetLongLength(0)
                || startX < 0
                || startY > mazeModel.GetLongLength(1)
                || startY < 0)
            {
                throw new ArgumentException();
            }
        }
        #endregion
    }
}