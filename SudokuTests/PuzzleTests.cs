using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace Sudoku.Tests
{
    [TestClass()]
    public class PuzzleTests
    {
        List<List<int>> board = FileIO.readBoard("Puzzles/testPuzzle.txt");

        [TestMethod()]
        [Ignore]
        public void IsBoxSumOkTest()
        {
            /*
            Puzzle puzzle = new Puzzle(board);
            bool actual = puzzle.IsBoxSumOk(0, 0);
            Assert.IsTrue(actual, "Sum is not correct");
            actual = puzzle.IsBoxSumOk(1, 0);
            Assert.IsTrue(actual, "Sum is not correct");
            */
        }

        [TestMethod()]
        [Ignore]
        public void CheckSumsTest()
        {
            /*
            Puzzle puzzle = new Puzzle(board);
            bool actual = puzzle.CheckSums(0, 0);
            Assert.IsTrue(actual, "Sum is not correct");
            */
        }

        [TestMethod()]
        [Ignore]
        public void CheckRowDuplicatesTest()
        {
            /*
            Puzzle puzzle = new Puzzle(board);
            bool actual = puzzle.CheckRowDuplicates(8);
            Assert.IsFalse(actual, "There are some duplicates in this row!");
            */
        }

        /*
        [TestMethod()]
        public void IsSolvedTest()
        {
            board = FileIO.readBoard("Puzzles/fullBoard.txt");
            Puzzle puzzle = new Puzzle(board);
            Assert.IsTrue(puzzle.IsSolved(), "The board is not solved yet!");
        }

        [TestMethod()]
        public void CheckAllBoxDuplicateTest()
        {
            board = FileIO.readBoard("Puzzles/fullBoard.txt");
            Puzzle puzzle = new Puzzle(board);
            Assert.IsTrue(puzzle.CheckAllBoxDuplicate(), "Not all boxes are correct!");
        }

        [TestMethod()]
        public void CheckBoxDuplicatesTest()
        {
            board = FileIO.readBoard("Puzzles/fullBoard.txt");
            Puzzle puzzle = new Puzzle(board);
            Assert.IsTrue(puzzle.CheckBoxDuplicates(0, 0), "Box is incorrect!");
        }
        */
    }
}