using NA0K08_GK10ZO;
using NA0K08_GK10ZO.Model;

namespace NA0K08_GK10ZO_UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void GetRaceDataFromCsv_InvalidFilePath_ThrowsException()
        {

            string path = "invalidpath.csv";


            Assert.ThrowsException<FileNotFoundException>(() => FileManager.GetRaceDataFromCsv(path));
        }




        [TestMethod]
        public void SaveToJSON_ValidData_CreatesFile()
        {

            List<TopThree> data = new List<TopThree>() { new TopThree("a", "b", "c") };
            string name = "test.json";


            FileManager.SaveToJSON(data, name);
            bool fileExists = File.Exists(name);


            Assert.IsTrue(fileExists);
            Assert.AreNotEqual(0, new FileInfo(name).Length);
        }


    }
}
