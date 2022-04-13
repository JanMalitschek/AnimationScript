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
                serializedObject.ApplyModifiedProperties();
                runtime.FindTargets();
                EditorUtility.SetDirty(runtime);
            }

            showTargets = EditorGUILayout.Foldout(showTargets, "Targets", EditorStyles.foldoutHeader);
            if(showTargets){
                EditorGUI.indentLevel++;
                for(int i = 0; i < targetsProperty.arraySize; i++){
                    SerializedProperty targetProperty = targetsProperty.GetArrayElementAtIndex(i);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(targetProperty.FindPropertyRelative("target"), new GUIContent(targetProperty.FindPropertyRelative("name").stringValue));
                    if(EditorGUI.EndChangeCheck())
                        runtime.FindPropertyInfos();
                }
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}