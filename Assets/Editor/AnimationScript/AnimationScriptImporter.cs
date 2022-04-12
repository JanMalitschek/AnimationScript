using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AssetImporters;

namespace AnimationScript{
    [ScriptedImporter(1, "as")]
    public class AnimationScriptImporter : ScriptedImporter
    {
        private Compiler compiler = new Compiler();
        public override void OnImportAsset(AssetImportContext ctx){
            compiler.Compile(ctx.assetPath);
            AnimationScriptModule module = ScriptableObject.CreateInstance<AnimationScriptModule>();
            ctx.AddObjectToAsset("module", module);
            ctx.SetMainObject(module);
        }
    }
}