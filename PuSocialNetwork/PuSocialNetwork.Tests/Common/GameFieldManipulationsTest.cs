﻿namespace PuSocialNetwork.Tests.Common
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    using static PuSocialNetwork.Common.GameConstants;
    using static PuSocialNetwork.Common.FieldManipulations.FieldHelper;

    [TestClass]
    public class GameFieldManipulationsTest
    {
        private int[,] grid;

        [TestInitialize]
        public void SetInit()
        {
            this.grid = new int[,]
                {
                    {2,0,2,128},
                    {0,8,8,0},
                    {8,8,0,0},
                    {0,16,0,16}
                 };
        }

        [TestMethod]
        public void GetNonZeroValuesReturnsCorrectValues()
        {
            var nonZeroValues = GetNonZeroNumbers(this.grid);
            var expectedValues = new List<int>() { 2, 2, 128, 8, 8, 8, 8, 16, 16 };

            CollectionAssert.AreEqual(expectedValues, nonZeroValues);
        }

        [TestMethod]
        public void MatrixConvertsToStringReturnsCorrectValue()
        {
            var matrixToString = ConvertMatrixToString(this.grid);

            Assert.AreEqual("2,,2,128,,8,8,,8,8,,,,16,,16", matrixToString);
        }

        [TestMethod]
        public void ConvertStringToMatrixReturnsCorrectValue()
        {
            var matrix = GetMatrixFromString("2,,2,128,,8,8,,8,8,,,,16,,16");

            CollectionAssert.AreEqual(this.grid, matrix);
        }

        [TestMethod]
        public void RndNumberAddExactlyOneNumberTwoOrFour()
        {
            var field = new int[FieldSize, FieldSize];

            for (int i = 0; i < 100; i++)
            {
                AddRandomNumber(field);
                var sum = 0;
                var addedNumsCount = 0;

                for (int k = 0; k < field.GetLength(0); k++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if (field[k, j] != 0)
                        {
                            sum += field[k, j];
                            addedNumsCount++;
                        }
                    }
                }

                Assert.AreEqual(1, addedNumsCount);
                Assert.IsTrue(sum == 2 || sum == 4);

                field = new int[FieldSize, FieldSize];
            }
        }

        [TestMethod]
        public void GetZeroIndexesReturnsCorrectValues()
        {
            var expected = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 0),
                new KeyValuePair<int, int>(1, 3),
                new KeyValuePair<int, int>(2, 2),
                new KeyValuePair<int, int>(2, 3),
                new KeyValuePair<int, int>(3, 0),
                new KeyValuePair<int, int>(3, 2),
            };

            var actualResult = GetZeroIndexes(this.grid);

            CollectionAssert.AreEqual(expected, actualResult);
        }

        [TestMethod]
        public void HasZeroValuesReturnsCorrectValues()
        {
            var actualResult = HasZeroValues(this.grid);

            Assert.AreEqual(true, actualResult);
        }
    }
}
