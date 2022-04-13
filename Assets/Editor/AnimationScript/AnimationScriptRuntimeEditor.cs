using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AnimationScript{
    [CustomEditor(typeof(AnimationScriptRuntime))]
    public class AnimationScriptRuntimeEditor : Editor
    {
        private AnimationScriptRuntime runtime;

        private bool showTargets = false;

        void Awake(){
            runtime = (AnimationScriptRuntime)target;
        }

        public override void OnInspectorGUI(){
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("module"));
            SerializedProperty targetsProperty = serializedObject.FindProperty("targets");
            // serializedObject.Update();
            
            if(EditorGUI.EndChangeCheck()){
                runtime.targets.Clear();
                if(runtime.module != null){
                    foreach(TimelineClip clip in runtime.module.timeline.targetClips)
                        runtime.targets.Add(new RuntimeTarget(clip.target));
                    EditorUtility.SetDirty(runtime);
                }
                serializedObject.ApplyModifiedProperties();
            }

            showTargets = EditorGUILayout.Foldout(showTargets, "Targets", EditorStyles.foldoutHeader);
            if(showTargets){
                EditorGUI.indentLevel++;
                for(int i = 0; i < targetsProperty.arraySize; i++){
                    SerializedProperty targetProperty = targetsProperty.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(targetProperty.FindPropertyRelative("target"), new GUIContent(targetProperty.FindPropertyRelative("name").stringValue));
                }
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}