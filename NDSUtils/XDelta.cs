using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
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
    }

    [Serializable]
    public readonly struct XDeltaPatch : ISerializable
    {
        public readonly byte[] Data;
        public readonly int OriginalFilesize;
        public readonly int PatchedFilesize;

        public XDeltaPatch(byte[] source, byte[] target)
        {
            OriginalFilesize = source.Length;
            PatchedFilesize = target.Length;

            // TODO: Change this ASAP when I compile the DLLs for XDelta, replace this executable call with a library link, which
            // will be a thousand times faster, specially on PCs with old HDDs.

            var targetFilename = Path.GetTempFileName();
            var sourceFilename = Path.GetTempFileName();
            var patchFilename = Path.GetTempFileName();
            {
                using var targetFile = File.OpenWrite(targetFilename);
                targetFile.Write(target, 0, target.Length);
                using var sourceFile = File.OpenWrite(sourceFilename);
                sourceFile.Write(source, 0, source.Length);
            }

            XDelta.CreatePatch(targetFilename, sourceFilename, patchFilename);

            using var patchFile = File.OpenRead(patchFilename);
            Data = new byte[patchFile.Length];
            patchFile.Read(Data, 0, (int)patchFile.Length);
        }

        #region Serialization / Deserialization
        public XDeltaPatch(SerializationInfo info, StreamingContext context)
        {
            Data = info.GetValue(nameof(Data), typeof(byte[])) as byte[];
            OriginalFilesize = info.GetInt32(nameof(OriginalFilesize));
            PatchedFilesize = info.GetInt32(nameof(PatchedFilesize));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Data), Data);
            info.AddValue(nameof(OriginalFilesize), OriginalFilesize);
            info.AddValue(nameof(PatchedFilesize), PatchedFilesize);
        }
        #endregion

        public byte[] Apply(byte[] source)
        {
            // TODO: Change this ASAP when I compile the DLLs for XDelta, replace this executable call with a library link, which
            // will be a thousand times faster, specially on PCs with old HDDs.

            var patchFilename = Path.GetTempFileName();
            var sourceFilename = Path.GetTempFileName();
            var resultFilename = Path.GetTempFileName();
            {
                using var patchFile = File.OpenWrite(patchFilename);
                patchFile.Write(Data, 0, Data.Length);
                using var sourceFile = File.OpenWrite(sourceFilename);
                sourceFile.Write(source, 0, source.Length);
            }


            XDelta.ApplyPatch(patchFilename, sourceFilename, resultFilename);

            using var resultFile = File.OpenRead(resultFilename);
            byte[] resultData = new byte[resultFile.Length];
            resultFile.Read(resultData, 0, (int)resultFile.Length);
            return resultData;
        }
    }
}
