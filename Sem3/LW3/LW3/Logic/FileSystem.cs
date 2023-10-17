using System.Text.Json;
using System.Text.Json.Serialization;

namespace LW3.Logic
{
    public static class FileSystem
    {
        public static string FilePath { get; set; } = "simulation.json";
        public static void SaveToFile(Simulation simulation)
        {
            File.WriteAllText(FilePath, string.Empty);

            using (FileStream fileStream = File.OpenWrite(FilePath))
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

            using (FileStream fileStream = File.OpenRead(FilePath))
            {
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                simulation = JsonSerializer.Deserialize<Simulation>(fileStream, options);
            }

            if (simulation == null) throw new Exception("Error reading file");
            return simulation;
        }
    }
}
