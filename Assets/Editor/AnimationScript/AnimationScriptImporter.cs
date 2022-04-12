using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AssetImporters;

namespace AnimationScript{
    [ScriptedImporter(1, "as")]
    public class AnimationScriptImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx){
            AnimationScriptModule module = ScriptableObject.CreateInstance<AnimationScriptModule>();
            Compiler compiler = new Compiler(ctx.assetPath);
            compiler.Compile(ctx.assetPath, module);
            compiler.CompilerResult.Print();
            ctx.AddObjectToAsset("module", module);
            ctx.SetMainObject(module);
        }
    }
}