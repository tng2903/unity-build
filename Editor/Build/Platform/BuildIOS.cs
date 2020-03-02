using System;
using UnityEditor;

namespace SuperSystems.UnityBuild
{

    [System.Serializable]
    public class BuildIOS : BuildPlatform
    {
        #region Constants

        // TODO: Fix iOS binary/data dir name.
        private const string _name = "iOS";
        private const string _binaryNameFormat = "{0}_ios";
        private const string _dataDirNameFormat = "{0}_ios_Data";
        private const BuildTargetGroup _targetGroup = BuildTargetGroup.iOS;

        #endregion

        public BuildIOS()
        {
            enabled = false;
            Init();
        }

        public override void Init()
        {
            platformName = _name;
            dataDirNameFormat = _dataDirNameFormat;
            targetGroup = _targetGroup;

            if (architectures == null || architectures.Length == 0)
            {
                architectures = new BuildArchitecture[] {
               new BuildArchitecture(BuildTarget.iOS, "iOS", true, _binaryNameFormat)
           };
            }
            if (variants == null || variants.Length == 0)
            {
                variants = new BuildVariant[] {
                new BuildVariant("Run in XCode as", new string[]{"Release", "Debug"}, 0),
                new BuildVariant("Development Build", new string[]{"false", "true"}, 0),
                new BuildVariant("Symlink Binaries", new string[]{"false", "true"}, 1),
           };
            }
        }

        public override void ApplyVariant()
        {
            foreach (var variantOption in variants)
            {
                switch (variantOption.variantName)
                {
                    case "Run in XCode as":
                        SetXcodeRunType(variantOption.variantKey);
                        break;
                    case "Development Build":
                        SetDevelopmentBuild(variantOption.variantKey);
                        break;
                    case "Symlink Binaries":
                        SetSymlinkBinaries(variantOption.variantKey);
                        break;
                }
            }
        }

        private void SetXcodeRunType(string variantKey)
        {
            EditorUserBuildSettings.iOSBuildConfigType = (iOSBuildType)System.Enum.Parse(typeof(iOSBuildType), variantKey);
        }

        private void SetDevelopmentBuild(string variantKey)
        {
            EditorUserBuildSettings.development = bool.Parse(variantKey);
        }
        private void SetSymlinkBinaries(string variantKey)
        {
            EditorUserBuildSettings.symlinkLibraries = bool.Parse(variantKey);
        }
    }

}