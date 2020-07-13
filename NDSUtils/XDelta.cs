using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace NDSUtils
{
    // Adapted from https://blog.traal.eu/29/

    public class XDelta
    {
        /// <summary>
        /// Creates a xdelta3 patch from source to target.
        /// </summary>
        public static void CreatePatch(string targetPath, string sourcePath, string patchPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = "xdelta3.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-f -e -s \"{sourcePath}\" \"{targetPath}\" \"{patchPath}\""
            };

            using Process exeProcess = Process.Start(startInfo);
            exeProcess.WaitForExit();
            startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = "xdelta3.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"printdelta \"{patchPath}\""
            };

            using Process exe2Process = Process.Start(startInfo);
            exe2Process.WaitForExit();
        }

        public static ByteSlice CreatePatch(ByteSlice target, ByteSlice source)
        {
            // TODO: Change this ASAP when I compile the DLLs for XDelta, replace this executable call with a library link, which
            // will be a thousand times faster, specially on PCs with old HDDs.

            var targetFilename = Path.GetTempFileName();
            var sourceFilename = Path.GetTempFileName();
            var patchFilename = Path.GetTempFileName();
            {
                using var targetFile = File.OpenWrite(targetFilename);
                targetFile.Write(target.GetAsArrayCopy(), 0, target.Size);
                using var sourceFile = File.OpenWrite(sourceFilename);
                sourceFile.Write(source.GetAsArrayCopy(), 0, source.Size);
            }

            CreatePatch(targetFilename, sourceFilename, patchFilename);

            using var patchFile = File.OpenRead(patchFilename);
            byte[] patchData = new byte[patchFile.Length];
            patchFile.Read(patchData, 0, (int)patchFile.Length);
            return new ByteSlice(patchData);
        }

        /// <summary>
        /// Applies a xdelta3 patch to source.
        /// </summary>
        public static void ApplyPatch(string patchPath, string sourcePath, string newFilePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = "xdelta3.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-f -d -s \"{sourcePath}\" \"{patchPath}\" \"{newFilePath}\""
            };

            using Process exeProcess = Process.Start(startInfo);
            exeProcess.WaitForExit();
        }

        public static ByteSlice ApplyPatch(ByteSlice patch, ByteSlice source)
        {
            // TODO: Change this ASAP when I compile the DLLs for XDelta, replace this executable call with a library link, which
            // will be a thousand times faster, specially on PCs with old HDDs.

            var patchFilename = Path.GetTempFileName();
            var sourceFilename = Path.GetTempFileName();
            var resultFilename = Path.GetTempFileName();
            {
                using var patchFile = File.OpenWrite(patchFilename);
                patchFile.Write(patch.GetAsArrayCopy(), 0, patch.Size);
                using var sourceFile = File.OpenWrite(sourceFilename);
                sourceFile.Write(source.GetAsArrayCopy(), 0, source.Size);
            }
            

            ApplyPatch(patchFilename, sourceFilename, resultFilename);

            using var resultFile = File.OpenRead(resultFilename);
            byte[] resultData = new byte[resultFile.Length];
            resultFile.Read(resultData, 0, (int)resultFile.Length);
            return new ByteSlice(resultData);
        }
    }
}
