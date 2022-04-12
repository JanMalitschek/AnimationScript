using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;

namespace AnimationScript{
    [CustomEditor(typeof(AnimationScriptImporter))]
    public class AnimationScriptImporterEditor : ScriptedImporterEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Animation Script!");
            base.ApplyRevertGUI();
        }
    }
}