﻿using System;
using NUnit.Framework;

namespace MazeLibrary.Tests
{
    [TestFixture]
    public class MazeSolverTests
    {
        private readonly int[] startXs = { 3, 0, 1, 0 };

        private readonly int[] startYs = { 5, 4, 0, 1 };

        private readonly int[][,] sourceData = new int[][,]
        {
            new int[,]
            {
                { -1, -1, -1, -1, -1, -1 },
                {  0,  0,  0, -1,  0, -1 },
                { -1,  0, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0,  0 },
                { -1,  0,  0,  0, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1, -1, -1, -1,  0, -1 },
                {  0,  0,  0, -1,  0, -1 },
                { -1,  0, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0, -1 },
                { -1,  0,  0,  0, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                {  0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0, -1 },
                { -1,  0, -1,  0, -1, -1,  0,  0, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0, -1,  0,  0,  0,  0,  0, -1 },
                { -1,  0, -1, -1,  0, -1, -1, -1, -1, -1, -1, -1 },
                { -1,  0, -1,  0,  0, -1,  0, -1,  0,  0,  0, -1 },
                { -1,  0, -1,  0, -1, -1,  0,  0,  0, -1,  0, -1 },
                { -1,  0, -1,  0,  0,  0,  0, -1, -1, -1,  0, -1 },
                { -1,  0, -1,  0, -1,  0,  0, -1,  0, -1,  0, -1 },
                { -1,  0, -1, -1, -1, -1,  0, -1,  0, -1,  0, -1 },
                { -1,  0,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0 },
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1,  0, -1, -1, -1, -1, -1, -1, -1,  0, -1, -1 },
                { -1,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0, -1 },
                { -1,  0, -1,  0, -1, -1,  0,  0, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0, -1,  0,  0,  0,  0,  0, -1 },
                { -1,  0, -1, -1,  0, -1, -1, -1, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0, -1,  0, -1,  0,  0,  0, -1 },
                { -1,  0, -1,  0, -1, -1,  0,  0,  0, -1,  0, -1 },
                { -1,  0, -1,  0,  0,  0,  0, -1, -1, -1,  0, -1 },
                { -1,  0, -1,  0, -1,  0,  0, -1,  0, -1,  0, -1 },
                { -1,  0, -1, -1, -1, -1,  0, -1,  0, -1,  0, -1 },
                { -1,  0,  0,  0,  0,  0,  0, -1,  0,  0,  0, -1 },
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            }
        };

        private readonly int[][,] result = new int[][,]
        {
            new int[,]
            {
                { -1, -1, -1, -1, -1, -1 },
                { 10,  9,  0, -1,  0, -1 },
                { -1,  8, -1, -1,  0, -1 },
                { -1,  7, -1,  3,  2,  1 },
                { -1,  6,  5,  4, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1, -1, -1, -1,  1, -1 },
                { 12, 11,  0, -1,  2, -1 },
                { -1, 10, -1, -1,  3, -1 },
                { -1,  9, -1,  5,  4, -1 },
                { -1,  8,  7,  6, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }
            },           
            new int[,]
            {
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                {  1,  2, -1, 30, 31, 32, 33, 34, -1, 44, 43, -1 },
                { -1,  3, -1, 29, -1, -1, 36, 35, -1, -1, 42, -1 },
                { -1,  4, -1, 28, 27, -1, 37, 38, 39, 40, 41, -1 },
                { -1,  5, -1, -1, 26, -1, -1, -1, -1, -1, -1, -1 },
                { -1,  6, -1, 24, 25, -1,  0, -1, 50, 51, 52, -1 },
                { -1,  7, -1, 23, -1, -1, 47, 48, 49, -1, 53, -1 },
                { -1,  8, -1, 22, 21, 20, 46, -1, -1, -1, 54, -1 },
                { -1,  9, -1, 45, -1, 19, 18, -1, 61, -1, 55, -1 },
                { -1, 10, -1, -1, -1, -1, 17, -1, 60, -1, 56, -1 },
                { -1, 11, 12, 13, 14, 15, 16, -1, 59, 58, 57, 62 },
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }

             // { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
             // {  1,  2, -1,  0,  0,  0,  0,  0, -1,  0,  0, -1 },
             // { -1,  3, -1,  0, -1, -1,  0,  0, -1, -1,  0, -1 },
             // { -1,  4, -1,  0,  0, -1,  0,  0,  0,  0,  0, -1 },
             // { -1,  5, -1, -1,  0, -1, -1, -1, -1, -1, -1, -1 },
             // { -1,  6, -1,  0,  0, -1,  0, -1, 23, 24, 25, -1 },
             // { -1,  7, -1,  0, -1, -1, 20, 21, 22, -1, 26, -1 },
             // { -1,  8, -1,  0,  0,  0, 19, -1, -1, -1, 27, -1 },
             // { -1,  9, -1,  0, -1,  0, 18, -1,  0, -1, 28, -1 },
             // { -1, 10, -1, -1, -1, -1, 17, -1,  0, -1, 29, -1 },
             // { -1, 11, 12, 13, 14, 15, 16, -1,  0,  0, 30, 31 },
             // { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1,  1, -1, -1, -1, -1, -1, -1, -1, 45, -1, -1 },
                { -1,  2, -1, 30, 31, 32, 33, 34, -1, 44, 43, -1 },
                { -1,  3, -1, 29, -1, -1, 36, 35, -1, -1, 42, -1 },
                { -1,  4, -1, 28, 27, -1, 37, 38, 39, 40, 41, -1 },
                { -1,  5, -1, -1, 26, -1, -1, -1, -1, -1,  0, -1 },
                { -1,  6, -1, 24, 25, -1,  0, -1,  0,  0,  0, -1 },
                { -1,  7, -1, 23, -1, -1,  0,  0,  0, -1,  0, -1 },
                { -1,  8, -1, 22, 21, 20,  0, -1, -1, -1,  0, -1 },
                { -1,  9, -1,  0, -1, 19, 18, -1,  0, -1,  0, -1 },
                { -1, 10, -1, -1, -1, -1, 17, -1,  0, -1,  0, -1 },
                { -1, 11, 12, 13, 14, 15, 16, -1,  0,  0,  0, -1 },
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }

             // { -1,  1, -1, -1, -1, -1, -1, -1, -1, 31, -1, -1 },
             // { -1,  2, -1,  0,  0,  0,  0,  0, -1, 30, 29, -1 },
             // { -1,  3, -1,  0, -1, -1,  0,  0, -1, -1, 28, -1 },
             // { -1,  4, -1,  0,  0, -1,  0,  0,  0,  0, 27, -1 },
             // { -1,  5, -1, -1,  0, -1, -1, -1, -1, -1, 26, -1 },
             // { -1,  6, -1,  0,  0, -1,  0, -1, 23, 24, 25, -1 },
             // { -1,  7, -1,  0, -1, -1, 20, 21, 22, -1,  0, -1 },
             // { -1,  8, -1,  0,  0,  0, 19, -1, -1, -1,  0, -1 },
             // { -1,  9, -1,  0, -1,  0, 18, -1,  0, -1,  0, -1 },
             // { -1, 10, -1, -1, -1, -1, 17, -1,  0, -1,  0, -1 },
             // { -1, 11, 12, 13, 14, 15, 16, -1,  0,  0,  0, -1 },
             // { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            }
        };

        [Test]
        public void MazeSolverConstructor_WithNull_ThrowsArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => new MazeSolver(null, 1, 2));

        [Test]
        public void MazeSolverConstructor_WithInvalidStartIndexX_ThrowsArgumentException()
            => Assert.Throws<ArgumentException>(() => new MazeSolver(sourceData[1], -12, 2));

        [Test]
        public void MazeSolverConstructor_WithInvalidStartIndexY_ThrowsArgumentException()
            => Assert.Throws<ArgumentException>(() => new MazeSolver(sourceData[1], 0, -2));

        [Test]
        public void PassMaze_SuccessfulTests_Case1()
        {
            MazeSolver solver = new MazeSolver(sourceData[0], startXs[0], startYs[0]);

            solver.PassMaze();
            int[,] output = solver.MazeWithPass();

            CollectionAssert.AreEquivalent(output, result[0]);
        }

        [Test]
        public void PassMaze_SuccessfulTests_Case2()
        {
            MazeSolver solver = new MazeSolver(sourceData[1], startXs[1], startYs[1]);

            solver.PassMaze();
            int[,] output = solver.MazeWithPass();

            CollectionAssert.AreEquivalent(output, result[1]);
        }

        [Test]
        public void PassMaze_SuccessfulTests_Case3()
        {
            MazeSolver solver = new MazeSolver(sourceData[2], startXs[2], startYs[2]);

            solver.PassMaze();
            int[,] output = solver.MazeWithPass();

            CollectionAssert.AreEquivalent(output, result[2]);
        }

        [Test]
        public void PassMaze_SuccessfulTests_Case4()
        {
            MazeSolver solver = new MazeSolver(sourceData[3], startXs[3], startYs[3]);

            solver.PassMaze();
            int[,] output = solver.MazeWithPass();

            CollectionAssert.AreEquivalent(output, result[3]);
        }
    }
}