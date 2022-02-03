using System;
using System.Collections.Generic;
using System.IO;

namespace Dupetopia
{
    class Duper
    {
        public Duper(string worldFolderNum, string masterIslandNum, string[] islandNums)
        {
            string appdataPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            string gameWorldPath = @"LocalLow\PocketPair\Craftopia\Save\Worlds\";

            _gameSavePath = Path.Combine(appdataPath, gameWorldPath, worldFolderNum);
            _dupeSavePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "WorkSpace");

            _masterIslandName = GetIslandFileName(masterIslandNum);
            _targetSaves = new List<string>(islandNums);

            MakeCopyFromMainSave();
        }

        private void MakeCopyFromMainSave()
        {
            string srcIslandPath = Path.Combine(_gameSavePath, _masterIslandName);
            string masterIslandPath = Path.Combine(_dupeSavePath, _masterIslandName);

            Directory.CreateDirectory(_dupeSavePath);

            ReplaceIslandFile(srcIslandPath, masterIslandPath);

            string subIslandPath;
            foreach (var subIslandNum in _targetSaves)
            {
                subIslandPath = Path.Combine(_dupeSavePath, GetIslandFileName(subIslandNum));

                ReplaceIslandFile(masterIslandPath, subIslandPath);
                ReplaceIslandFile(subIslandPath, Path.Combine(_gameSavePath, GetIslandFileName(subIslandNum)));
            }
        }

        private string GetIslandFileName(string saveIslandNum)
        {
            return "Island" + saveIslandNum + ".ocs";
        }

        private void ReplaceIslandFile(string srcIslandPath, string destIslandPath)
        {
            if (File.Exists(destIslandPath))
            {
                File.Delete(destIslandPath);
            }

            File.Copy(srcIslandPath, destIslandPath, true);
        }

        private string _gameSavePath;
        private string _dupeSavePath;
        private string _masterIslandName;
        private List<string> _targetSaves;
    }
}
