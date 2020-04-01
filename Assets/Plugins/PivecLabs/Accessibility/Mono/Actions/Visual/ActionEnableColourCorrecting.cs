namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using GameCreator.Core;
    using GameCreator.Core.Hooks;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionEnableColourCorrecting : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool enableCorrecting = false;

        public bool enableVariable = false;

        [VariableFilter(Variable.DataType.Bool)]
        public VariableProperty correctingVariable = new VariableProperty(Variable.VarType.GlobalVariable);


        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {


            if (HookCamera.Instance != null)
            {
                if (HookCamera.Instance.gameObject.GetComponent<CameraColourCorrecting>() == null)
                {
                    HookCamera.Instance.gameObject.AddComponent<CameraColourCorrecting>();
                }

                CameraColourCorrecting camcolour = HookCamera.Instance.Get<CameraColourCorrecting>();

                if (enableVariable == true)
                {
                    camcolour.correcting = (bool)this.correctingVariable.Get(target);
                }
                else
                {
                    if (enableCorrecting == true)
                    {
                        camcolour.correcting = true;
                    }
                    else
                    {
                        camcolour.correcting = false;
                    }
                }
             
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

        public static new string NAME = "Accessibility/Visual/Colour Correcting Enable";
		private const string NODE_TITLE = "Enable Correcting Colours";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spenableCorrecting;
        private SerializedProperty spenableVariable;
        private SerializedProperty spcorrectingVariable;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spenableCorrecting = this.serializedObject.FindProperty("enableCorrecting");
            this.spenableVariable = this.serializedObject.FindProperty("enableVariable");
            this.spcorrectingVariable = this.serializedObject.FindProperty("correctingVariable");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spenableCorrecting = null;
            this.spenableVariable = null;
            this.spcorrectingVariable = null;

        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spenableVariable, new GUIContent("Value from Variable"));

            if (enableVariable == true)
            {
                EditorGUILayout.PropertyField(this.spcorrectingVariable, new GUIContent("Enable Correcting"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spenableCorrecting, new GUIContent("Enable Correcting"));
            }

            EditorGUILayout.Space();
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
