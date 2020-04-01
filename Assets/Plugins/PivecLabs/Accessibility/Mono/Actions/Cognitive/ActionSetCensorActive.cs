namespace GameCreator.Accessibility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using GameCreator.Core;
    using GameCreator.Variables;

	#if UNITY_EDITOR
	using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionSetCensorActive : IAction 
	{
        public bool enableVariable = false;

        public bool enableCensor = false;

        [VariableFilter(Variable.DataType.Bool)]
        public VariableProperty enableCensorVariable = new VariableProperty(Variable.VarType.GlobalVariable);
       
         private GameObject[] CensorObjects;

        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

            if (enableVariable == true)
            {
                enableCensor = (bool)this.enableCensorVariable.Get(target);
            }

            CensorObjects = GameObject.FindGameObjectsWithTag("CensorObject");

            if (enableCensor == true)
            {

                foreach (GameObject censor in CensorObjects)
                {
                  
                    censor.GetComponent<MeshRenderer>().enabled = true;
                }


            }
            else
            {
                foreach (GameObject censor in CensorObjects)
                {

                    censor.GetComponent<MeshRenderer>().enabled = false;
                }

            }


            return true;
        }

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Cognitive/Enable Censoring";
		private const string NODE_TITLE = "Enable/Disable Censor Objects";

        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spenableCensor;
        private SerializedProperty spenableVariable;
        private SerializedProperty spenableCensorVariable;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
            return string.Format(NODE_TITLE);
        }

        protected override void OnEnableEditorChild ()
		{
            this.spenableCensor = this.serializedObject.FindProperty("enableCensor");
            this.spenableVariable = this.serializedObject.FindProperty("enableVariable");
            this.spenableCensorVariable = this.serializedObject.FindProperty("enableCensorVariable");
        }

        protected override void OnDisableEditorChild ()
		{
            this.spenableCensor = null;
            this.spenableVariable = null;
            this.spenableCensorVariable = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spenableVariable, new GUIContent("Value from Variable"));

            if (enableVariable == true)
            {
                EditorGUILayout.PropertyField(this.spenableCensorVariable, new GUIContent("Enable Censor Objects"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spenableCensor, new GUIContent("Enable Censor Objects"));
            }

            EditorGUILayout.Space();

            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}