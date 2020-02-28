using System;
using UnityEditor;

namespace SuperSystems.UnityBuild
{

    [System.Serializable]
    public class BuildWSA : BuildPlatform
    {
        #region Constants

        private const string _name = "Universal Windows Platform";
        private const string _binaryNameFormat = "{0}";
        private const string _dataDirNameFormat = "{0}_Data";
        private const BuildTargetGroup _targetGroup = BuildTargetGroup.WSA;

        #endregion

        public BuildWSA()
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
                new BuildArchitecture(BuildTarget.WSAPlayer, "Universal Windows Platform", true, _binaryNameFormat)
            };
            }

            if (variants == null || variants.Length == 0)
            {
                variants = new BuildVariant[] {
                new BuildVariant("Target Device", new string[] {"AnyDevice", "PC", "Mobile", "HoloLens" }, 0),
                new BuildVariant("Architecture", new string[] { "x64", "x86", "ARM"}, 0),
                new BuildVariant("Build Type", new string[] { "XAML", "D3D"}, 0)
                // new BuildVariant("Build Configuration", new string[] { "Release", "Debug", "Master"}, 0),
            };
            }
        }

        public override void ApplyVariant()
        {
            foreach (var variantOption in variants)
            {
                switch (variantOption.variantName)
                {
                    case "Target Device":
                        SetDeviceType(variantOption.variantKey);
                        break;
                    case "Architecture":
                        SetArchitecture(variantOption.variantKey);
                        break;
                    case "Build Type":
                        SetBuildType(variantOption.variantKey);
                        break;
                    case "Build Configuration":
                        SetBuildConfiguration(variantOption.variantKey);
                        break;
                }
            }
        }

        private void SetDeviceType(string key)
        {
            EditorUserBuildSettings.wsaSubtarget = (WSASubtarget)System.Enum.Parse(typeof(WSASubtarget), key);
        }

        private void SetBuildConfiguration(string key)
        {
            // EditorUserBuildSettings.wsaSDK
        }

        private void SetArchitecture(string key)
        {
            EditorUserBuildSettings.wsaArchitecture = key;
        }

        private void SetBuildType(string key)
        {
            EditorUserBuildSettings.wsaUWPBuildType = (WSAUWPBuildType)System.Enum.Parse(typeof(WSAUWPBuildType), key);
        }
    }

}