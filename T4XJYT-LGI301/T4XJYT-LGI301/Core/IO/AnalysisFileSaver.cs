using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301.Core.IO
{
    public class AnalysisFileSaver : IAnalysisFileSaver
    {
        public async Task SaveAnalysis(TextAnalysis textAnalysis, FileFormat format)
        {
            try
            {
                await Task.Run(() =>
                {
                    string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SavedFiles");
                    Directory.CreateDirectory(folderPath);

                    string fileExtension = format == FileFormat.Xml ? "xml" : "json";
                    string fileName = Path.Combine(folderPath, $"TextAnalysis_{DateTime.Now.ToString("yyyyMMddHHmmss")}.{fileExtension}");

                    using StreamWriter writer = new StreamWriter(fileName);

                    if (format == FileFormat.Xml)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(TextAnalysis));
                        serializer.Serialize(writer, textAnalysis);
                    }
                    else
                    {
                        string json = JsonConvert.SerializeObject(textAnalysis, Formatting.Indented);
                        writer.Write(json);
                    }
                });
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error during XML serialization: {ex.Message}");
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error during JSON serialization: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
