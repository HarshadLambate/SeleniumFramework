using Newtonsoft.Json;
using SeleniumDemo.Config;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SeleniumDemo
{
    public class TestHelper
    {
        public async Task<string> ReadJsonFileAsync(string fileName)
        {//asynchronous file reading to improve responsiveness
            try
            {
                string filePath = Path.Combine(TestHelper.GetProjectPath(), fileName);
                using (StreamReader reader = new StreamReader(filePath))
                {//Use "using" statements for StreamReader and JsonReader to ensure resources are properly disposed.
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error reading JSON file: {ex.Message}");
            }
        }
        public async Task<EnvironmentData> GetEnvironmentDataAsync()
        {
            try
            {
                string appData = await ReadJsonFileAsync(@"Config\AppConfig.json");
                var jsonObj = JsonConvert.DeserializeObject<AppConfig>(appData);
                string environmentName = jsonObj?.environment;

                string envData = await ReadJsonFileAsync(@"Config\EnvironmentConfig.json");
                var root = JsonConvert.DeserializeObject<Root>(envData);
                EnvironmentData targetEnvironment = root?.environments?.Find(env => env.name.Equals(environmentName, StringComparison.OrdinalIgnoreCase));
                return targetEnvironment;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error getting environment data: {ex.Message}");
            }
        }

        public static AppConfig GetAppConfig()
        {
            string projectPath = GetProjectPath();
            string fileName = @"Config\AppConfig.json";
            string filePath = Path.Combine(projectPath, fileName);
            return JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(filePath));
        }

        public static string GetProjectPath()
        {
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            return assemblyPath.Replace(@"bin\Debug\", "");
        }
    }
}
