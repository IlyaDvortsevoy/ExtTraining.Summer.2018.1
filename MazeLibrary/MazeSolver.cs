using System;
using System.Collections.Generic;

namespace MazeLibrary
{
    /// <summary>
    /// Defines algorithm for finding exit from a maze
    /// </summary>
    public class MazeSolver
    {
        #region Fields
        private int[,] mazeArray;
        private int startX;
        private int startY;
        private int count = 1;
        private bool end = false;
        private List<(int x, int y)> visited = new List<(int x, int y)>();
        #endregion

        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MazeLibrary.MazeSolver" /> class
        /// </summary>
        /// <param name="mazeModel"> The two dimensional array which stores a maze </param>
        /// <param name="startX"> Index of the start point from the first dimension of an array </param>
        /// <param name="startY"> Index of the start point from the second dimension of an array </param>
        public MazeSolver(int[,] mazeModel, int startX, int startY)
        {
            ValidateArray(mazeModel);
            ValidatePositions(mazeModel, startX, startY);

            mazeArray = mazeModel;
            this.startX = startX;
            this.startY = startY;
        }

        /// <summary>
        /// Returns the current state of the array
        /// </summary>
        /// <returns></returns>
        public int[,] MazeWithPass() => mazeArray;

        /// <summary>
        /// Performs finding of the exit from a maze
        /// </summary>
        public void PassMaze()
        {
            PassMaze(startX, startY);
        }
        #endregion

        #region Private Methods
        private void PassMaze(int startX, int startY)
        {
            mazeArray[startX, startY] = count++;
            visited.Add((startX, startY));

            if (CanGoLeft(startX, startY)
                && !IsVisited(startX, startY - 1)
                && !end)
            {
                PassMaze(startX, startY - 1);
            }

            if (CanGoRight(startX, startY)
                && !IsVisited(startX, startY + 1)
                && !end)
            {
                PassMaze(startX, startY + 1);
            }

            if (CanGoUp(startX, startY)
                && !IsVisited(startX - 1, startY)
                && !end)
            {
                PassMaze(startX - 1, startY);
            }

            if (CanGoDown(startX, startY)
                && !IsVisited(startX + 1, startY)
                && !end)
            {
                PassMaze(startX + 1, startY);
            }

            if (startX + 1 > mazeArray.GetLength(0) - 1
                || startX - 1 < 0
                || startY + 1 > mazeArray.GetLength(1) - 1
                || startY - 1 < 0)
            {
                end = true;
            }
        }

        private bool CanGoDown(int startX, int startY)
        {
            if (mazeArray.GetLength(0) - 1 >= startX + 1
                    && mazeArray[startX + 1, startY] == 0)
            {
                return true;
            }

            return false;
        }

        private bool CanGoUp(int startX, int startY)
        {
            if (startX - 1 >= 0
                && mazeArray[startX - 1, startY] == 0)
            {
                return true;
            }

            return false;
        }

        private bool CanGoLeft(int startX, int startY)
        {
            if (startY - 1 >= 0
                && mazeArray[startX, startY - 1] == 0)
            {
                return true;
            }

            return false;
        }

        private bool CanGoRight(int startX, int startY)
        {
            if (mazeArray.GetLength(1) - 1 >= startY + 1
                    && mazeArray[startX, startY + 1] == 0)
            {
                return true;
            }

            return false;
        }

        private bool IsVisited(int startX, int startY)
        {
            (int x, int y) current = (startX, startY);

            if (visited.Contains(current))
            {
                return true;
            }

            return false;
        }

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