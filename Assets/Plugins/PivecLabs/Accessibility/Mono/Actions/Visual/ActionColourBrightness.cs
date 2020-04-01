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
	public class ActionColourBrightness : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
          public bool brtVar = false;
 
       [Range(0.0f, 3.0f)] public float _brightness = 1.0f;

      
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty camera_brightness = new VariableProperty(Variable.VarType.GlobalVariable);
   
     
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
               
                if (brtVar == true)
                {
                    camcolour.brightness = (float)this.camera_brightness.Get(target);
                }
                else
                {
                    camcolour.brightness = _brightness;
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

        public static new string NAME = "Accessibility/Visual/Colour Brightness";
		private const string NODE_TITLE = "Correcting Overall Brightness";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spBrightnessToggle;
        private SerializedProperty spBrightnessSlider;
        private SerializedProperty spBrightness;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
             this.spBrightnessToggle = this.serializedObject.FindProperty("brtVar");
            this.spBrightnessSlider = this.serializedObject.FindProperty("_brightness");
            this.spBrightness = this.serializedObject.FindProperty("camera_brightness");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spBrightnessToggle = null;
            this.spBrightnessSlider = null;
            this.spBrightness = null;

        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.LabelField("Correcting Overall Brightness in a Scene", EditorStyles.boldLabel);
            EditorGUILayout.Space();
          
            EditorGUILayout.LabelField("Overall Brightness", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(this.spBrightnessToggle, new GUIContent("Value from Variable"));
            if (brtVar == true)
            {
                EditorGUILayout.PropertyField(this.spBrightness, new GUIContent("Brightness Value"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spBrightnessSlider, new GUIContent("From 0 to +3"));
            }

            EditorGUILayout.Space();


            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
