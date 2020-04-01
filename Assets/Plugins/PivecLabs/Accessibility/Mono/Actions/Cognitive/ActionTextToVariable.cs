namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using GameCreator.Core;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
    public class ActionTextToVariable : IAction
    {
    
        [VariableFilter(Variable.DataType.String)]
        public VariableProperty targetVariable = new VariableProperty(Variable.VarType.GlobalVariable);
        public InputField text;
        public string content = "{0}";


        // EXECUTABLE: ----------------------------------------------------------------------------
        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            if (this.text != null)  
            {
              
                this.targetVariable.Set(this.text.text, target);

               
            }


            return true;
        }


      
        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Cognitive/Text to Variable";
        private const string NODE_TITLE = "Text to Variable";

        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty sptext;
        private SerializedProperty sptargetVariable;
   
        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {
            return string.Format(NODE_TITLE);
        }

        protected override void OnEnableEditorChild()
        {
            this.sptext = this.serializedObject.FindProperty("text");
            this.sptargetVariable = this.serializedObject.FindProperty("targetVariable");
        }

        protected override void OnDisableEditorChild()
        {
            this.sptext = null;
            this.sptargetVariable = null;
         }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.sptext, new GUIContent("UI Text InputField"));

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.sptargetVariable, new GUIContent("Target Variable"));
     
            EditorGUILayout.Space();

            this.serializedObject.ApplyModifiedProperties();
        }

#endif
    }
}