namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using GameCreator.Core;
    using GameCreator.Core.Hooks;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionVariableTextSize : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
        public bool bestfittextSize = false;

        public bool enableVariable = false;
        public float textsizeIncrease = 0.0f;

        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty fontsizeVariable = new VariableProperty(Variable.VarType.GlobalVariable);

        private float oldSize = 0.0f;
        private float newSize = 0.0f;

        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            Text[] textArray = Resources.FindObjectsOfTypeAll<Text>();

            if (bestfittextSize == true)
            {

                textArray = Resources.FindObjectsOfTypeAll<Text>();
                foreach (var x in textArray)
                {
                    x.resizeTextForBestFit = true;
                }
            }
            else
            {
                if (enableVariable == true)
                {
                    newSize = (float)this.fontsizeVariable.Get(target);
                }
                else
                {
                    newSize = (float)textsizeIncrease;
                }


                textArray = Resources.FindObjectsOfTypeAll<Text>();
                foreach (var x in textArray)
                {
                    x.resizeTextForBestFit = false;

                    if (newSize > oldSize)
                        x.fontSize = x.fontSize + (int)(newSize - oldSize);
                    else
                        x.fontSize = x.fontSize - (int)(oldSize - newSize);



                }

                oldSize = newSize;
            }

            if (enableVariable == false)
            {
                oldSize = 0.0f;
                newSize = 0.0f;

            }
               

            return true;
        }

        public override IEnumerator Execute(GameObject target, IAction[] actions, int index)
        {

       
            return base.Execute(target, actions, index);
        }


      

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Visual/Variable Text Size";
		private const string NODE_TITLE = "Variable Text Size";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spbestfittextSize;
        private SerializedProperty spenableVariable;
        private SerializedProperty sptextsizeIncrease;
        private SerializedProperty spfontsizeVariable;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spbestfittextSize = this.serializedObject.FindProperty("bestfittextSize");
            this.spenableVariable = this.serializedObject.FindProperty("enableVariable");
            this.sptextsizeIncrease = this.serializedObject.FindProperty("textsizeIncrease");
            this.spfontsizeVariable = this.serializedObject.FindProperty("fontsizeVariable");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spbestfittextSize = null;
            this.spenableVariable = null;
            this.sptextsizeIncrease = null;
            this.spfontsizeVariable = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spbestfittextSize, new GUIContent("Enable BestFit Text Size"));
            EditorGUILayout.Space();

            if (bestfittextSize == false)

            {

                EditorGUILayout.PropertyField(this.spenableVariable, new GUIContent("Value from Variable"));

                if (enableVariable == true)
                {
                    EditorGUILayout.PropertyField(this.spfontsizeVariable, new GUIContent("Text Size Increase by"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.sptextsizeIncrease, new GUIContent("Text Size Increase"));
                }


            }

            EditorGUILayout.Space();
        
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
