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
	public class ActionChangeTextFont : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
        public bool enableVariable = false;

        public bool changetextFont = false;

        [VariableFilter(Variable.DataType.Bool)]
        public VariableProperty changefontVariable = new VariableProperty(Variable.VarType.GlobalVariable);

        public Font originalFont;
        public Font newFont;

      

        private Text[] textArray;
   
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

              
            return false;
        }

        public override IEnumerator Execute(GameObject target, IAction[] actions, int index)
        {

            if (enableVariable == true)
            {
                changetextFont = (bool)this.changefontVariable.Get(target);
            }
 
            if (changetextFont == true)
               {
                     textArray = Resources.FindObjectsOfTypeAll<Text>();
                        foreach (var x in textArray)
                        {
                            Font en_font = newFont;
                            x.font = en_font;

                        }
                    

                 }
               else
            {
                textArray = Resources.FindObjectsOfTypeAll<Text>();
                foreach (var x in textArray)
                {
                    Font en_font = originalFont;
                    x.font = en_font;

                }


            }


            return base.Execute(target, actions, index);
        }


      

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Cognitive/Change Text Font";
		private const string NODE_TITLE = "Change Text Font";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spchangetextFont;
        private SerializedProperty spenableVariable;
        private SerializedProperty spchangefontVariable;
        private SerializedProperty sporiginalFont;
        private SerializedProperty spnewFont;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spchangetextFont = this.serializedObject.FindProperty("changetextFont");
            this.spenableVariable = this.serializedObject.FindProperty("enableVariable");
            this.spchangefontVariable = this.serializedObject.FindProperty("changefontVariable");
            this.sporiginalFont = this.serializedObject.FindProperty("originalFont");
            this.spnewFont = this.serializedObject.FindProperty("newFont");
        }

        protected override void OnDisableEditorChild ()
		{
            this.spchangetextFont = null;
            this.spenableVariable = null;
            this.spchangefontVariable = null;
            this.sporiginalFont = null;
            this.spnewFont = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spenableVariable, new GUIContent("Value from Variable"));

            if (enableVariable == true)
            {
                EditorGUILayout.PropertyField(this.spchangefontVariable, new GUIContent("Change Text Font"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spchangetextFont, new GUIContent("Change Text Font"));
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.sporiginalFont, new GUIContent("Original Font"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spnewFont, new GUIContent("New Font"));

            EditorGUILayout.Space();
        
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
