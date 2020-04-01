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
	public class ActionColourContrast : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
          public bool cntVar = false;

       
         [Range(-1.0f, 3.0f)] public float _contrast = 1.0f;

      
        [VariableFilter(Variable.DataType.Number)]
         public VariableProperty camera_contrast = new VariableProperty(Variable.VarType.GlobalVariable);

     
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
                  if (cntVar == true)
                {
                    camcolour.contrast = (float)this.camera_contrast.Get(target);
                }
                else
                {
                    camcolour.contrast = _contrast;
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

        public static new string NAME = "Accessibility/Visual/Colour Contrast";
		private const string NODE_TITLE = "Correcting Overall Contrast";

        // PROPERTIES: ----------------------------------------------------------------------------
         private SerializedProperty spContrastToggle;

          private SerializedProperty spContrastSlider;

         private SerializedProperty spContrast;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
             this.spContrastToggle = this.serializedObject.FindProperty("cntVar");

            this.spContrastSlider = this.serializedObject.FindProperty("_contrast");

            this.spContrast = this.serializedObject.FindProperty("camera_contrast");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spContrastToggle = null;

            this.spContrastSlider = null;

            this.spContrast = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.LabelField("Correcting Overall Contrast in a Scene", EditorStyles.boldLabel);
            EditorGUILayout.Space();
          
     
            EditorGUILayout.LabelField("Overall Contrast", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(this.spContrastToggle, new GUIContent("Value from Variable"));
            if (cntVar == true)
            {
                EditorGUILayout.PropertyField(this.spContrast, new GUIContent("Contrast Value")); ;
            }
            else
            {
                EditorGUILayout.PropertyField(this.spContrastSlider, new GUIContent("From -1 to +3"));
            }
            EditorGUILayout.Space();

            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
