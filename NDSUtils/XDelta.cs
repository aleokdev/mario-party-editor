using System.Diagnostics;
using System.Threading.Tasks;

namespace NDSUtils
{
    // Adapted from https://blog.traal.eu/29/

    public class XDelta
    {
        /// <summary>
        /// Creates a xdelta3 patch from source to target.
        /// </summary>
        public static async void CreatePatch(string targetPath, string sourcePath, string patchPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = "xdelta3.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-e -s \"{sourcePath}\" \"{targetPath}\" \"{patchPath}\""
            };

            await Task.Run(() =>
            {
                using Process exeProcess = Process.Start(startInfo);
                exeProcess.WaitForExit();
            });
        }

        /// <summary>
        /// Applies a xdelta3 patch to source.
        /// </summary>
        public static async void ApplyPatch(string patchPath, string sourcePath, string newFilePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = "xdelta3.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-d -s \"{sourcePath}\" \"{patchPath}\" \"{newFilePath}\""
            };

            await Task.Run(() =>
            {
                using Process exeProcess = Process.Start(startInfo);
                exeProcess.WaitForExit();
            });
        }
    }
}
