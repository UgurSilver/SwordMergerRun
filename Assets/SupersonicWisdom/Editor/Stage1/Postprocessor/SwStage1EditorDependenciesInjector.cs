#if SW_STAGE_STAGE1_OR_ABOVE

using UnityEditor.Callbacks;

namespace SupersonicWisdomSDK.Editor
{
    internal class SwStage1EditorDependenciesInjector: SwEditorDependenciesInjector
    {
        private const string NEWTONSOFT_NUGET_JSON_CONVERT_KEY = "com.unity.nuget.newtonsoft-json";
        private const string NEWTONSOFT_NUGET_JSON_CONVERT_VERSION = "3.0.2";

        protected override SwJsonDictionary GetDependenciesToInject()
        {
            return base.GetDependenciesToInject()
                       .Merge(new SwJsonDictionary
                       {
                           {
                               NEWTONSOFT_NUGET_JSON_CONVERT_KEY, NEWTONSOFT_NUGET_JSON_CONVERT_VERSION
                           }
                       });
        }
    }
}

#endif
