namespace NA0K08_GK10ZO_UnitTests
{
    public class Tests
    {
        [TestMethod]
        public void GetRaceDataFromCsv_InvalidFilePath_ThrowsException()
        {

            string path = "invalidpath.csv";


            Assert.ThrowsException<FileNotFoundException>(() => FileManager.GetRaceDataFromCsv(path));
        }

        [TestMethod]
        public void GetRaceDataFromCsv_ValidFilePath_ReturnsList()
        {

            string path = @"TestData\raceResults.csv";


            var result = FileManager.GetRaceDataFromCsv(path);


            Assert.IsInstanceOfType(result, typeof(List<RaceResult>));
        }


        [TestMethod]
        public void GetRaceDataFromCsv_EmptyFile_ReturnsEmptyList()
        {

            string path = @"TestData\emptyFile.csv";


            var result = FileManager.GetRaceDataFromCsv(path);


            Assert.AreEqual(0, result.Count);
        }
    }
}