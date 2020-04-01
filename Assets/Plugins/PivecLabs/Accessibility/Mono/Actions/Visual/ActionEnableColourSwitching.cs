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
	public class ActionEnableColourSwitching : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool enableSwapping = false;

        public bool enableVariable = false;

        [VariableFilter(Variable.DataType.Bool)]
        public VariableProperty swappingVariable = new VariableProperty(Variable.VarType.GlobalVariable);


        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

           return false;
        }

        public override IEnumerator Execute(GameObject target, IAction[] actions, int index)
        {




            if (HookCamera.Instance != null)
            {

                if (HookCamera.Instance.gameObject.GetComponent<CameraColourSwitcher>() == null)
                {
                    HookCamera.Instance.gameObject.AddComponent<CameraColourSwitcher>();
                    Debug.Log("Adding Camera Component");
                    yield return new WaitForSeconds(0.5f);
                }

                CameraColourSwitcher camswap = HookCamera.Instance.Get<CameraColourSwitcher>();

                if (enableVariable == true)
                {
                    camswap.swapping = (bool)this.swappingVariable.Get(target);
                }
                else
                {
                    if (enableSwapping == true)
                    {
                        camswap.swapping = true;
                    }
                    else
                    {
                        camswap.swapping = false;
                    }

                }
            }
            yield return base.Execute(target, actions, index);
           
        }


      

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Visual/Colour Switching Enable";
		private const string NODE_TITLE = "Enable Switching Colours";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spenableSwapping;
        private SerializedProperty spenableVariable;
        private SerializedProperty spswappingVariable;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spenableSwapping = this.serializedObject.FindProperty("enableSwapping");
            this.spenableVariable = this.serializedObject.FindProperty("enableVariable");
            this.spswappingVariable = this.serializedObject.FindProperty("swappingVariable");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spenableSwapping = null;
            this.spenableVariable = null;
            this.spswappingVariable = null;

        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spenableVariable, new GUIContent("Value from Variable"));

            if (enableVariable == true)
            {
                EditorGUILayout.PropertyField(this.spswappingVariable, new GUIContent("Enable Colour Swaps"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spenableSwapping, new GUIContent("Enable Colour Swaps"));
            }


            EditorGUILayout.Space();
        
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
