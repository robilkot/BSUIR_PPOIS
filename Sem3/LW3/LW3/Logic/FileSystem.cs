using System.Text.Json;
using System.Text.Json.Serialization;

namespace LW3.Logic
{
    static class FileSystem
    {
        private static string s_filePath = "simulation.json";
        public static void SaveToFile(Simulation simulation)
        {
            File.WriteAllText(s_filePath, string.Empty);
            
            using (FileStream fileStream = File.OpenWrite(s_filePath))
            {
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                JsonSerializer.Serialize(fileStream, simulation, options);
            }
        }
        public static Simulation ReadFromFile()
        {
            Simulation? simulation;

            using (FileStream fileStream = File.OpenRead(s_filePath))
            {
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                simulation = JsonSerializer.Deserialize<Simulation>(fileStream, options);
            }

            return simulation ?? (new());
        }
        public static void SetFilePath(string filePath)
        {
            s_filePath = filePath;
        }
    }
}
