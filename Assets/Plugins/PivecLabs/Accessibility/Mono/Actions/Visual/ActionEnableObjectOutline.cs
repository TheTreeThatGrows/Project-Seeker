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
	public class ActionEnableObjectOutline : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
        public bool enableVariable = false;

        public bool enableOutlining = false;

    
        [VariableFilter(Variable.DataType.Bool)]
        public VariableProperty outlineVariable = new VariableProperty(Variable.VarType.GlobalVariable);


        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {


            if (HookCamera.Instance != null)
            {

                if (HookCamera.Instance.gameObject.GetComponent<CameraObjectOutline>() == null)
                {
                    HookCamera.Instance.gameObject.AddComponent<CameraObjectOutline>();
                }
                CameraObjectOutline cam = HookCamera.Instance.Get<CameraObjectOutline>();

                if (enableVariable == true)
                {
                    cam.outlining = (bool)this.outlineVariable.Get(target);
                }
                else
                {
                    if (enableOutlining == true)
                    {
                        cam.outlining = true;
                    }
                    else
                    {
                        cam.outlining = false;
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

        public static new string NAME = "Accessibility/Visual/Object Outline Enable";
		private const string NODE_TITLE = "Enable Object Outline";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spenableOutlining;
        private SerializedProperty spenableVariable;
        private SerializedProperty spoutlineVariable;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spenableOutlining = this.serializedObject.FindProperty("enableOutlining");
            this.spenableVariable = this.serializedObject.FindProperty("enableVariable");
            this.spoutlineVariable = this.serializedObject.FindProperty("outlineVariable");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spenableOutlining = null;
            this.spenableVariable = null;
            this.spoutlineVariable = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spenableVariable, new GUIContent("Value from Variable"));

            if (enableVariable == true)
            {
               EditorGUILayout.PropertyField(this.spoutlineVariable, new GUIContent("Enable Object Outline"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spenableOutlining, new GUIContent("Enable Object Outline"));
            }

            EditorGUILayout.Space();
        
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
