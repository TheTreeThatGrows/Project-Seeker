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
	public class ActionColourCorrecting : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
         public bool hueVar = false;
         public bool satVar = false;
         public bool brtVar = false;
         public bool cntVar = false;

       
        [Range(-180f, 180f)] public float _hue = 1.0f;
        [Range(0.0f, 3.0f)] public float _saturation = 1.0f;
        [Range(0.0f, 3.0f)] public float _brightness = 1.0f;
        [Range(-1.0f, 3.0f)] public float _contrast = 1.0f;

      
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty camera_hue = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty camera_saturation = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty camera_brightness = new VariableProperty(Variable.VarType.GlobalVariable);
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
                if (hueVar == true)
                {
                    camcolour.hue = (float)this.camera_hue.Get(target);
                }
                else
                {
                    camcolour.hue = _hue;
                }

                if (satVar == true)
                {
                    camcolour.saturation = (float)this.camera_saturation.Get(target);
                }
                else
                {
                    camcolour.saturation = _saturation;
                }

                if (brtVar == true)
                {
                    camcolour.brightness = (float)this.camera_brightness.Get(target);
                }
                else
                {
                    camcolour.brightness = _brightness;
                }

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

        public static new string NAME = "Accessibility/Visual/Colour Correcting";
		private const string NODE_TITLE = "Correcting All Colours";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spHueToggle;
        private SerializedProperty spSaturationToggle;
        private SerializedProperty spBrightnessToggle;
        private SerializedProperty spContrastToggle;

        private SerializedProperty spHueSlider;
        private SerializedProperty spSaturationSlider;
        private SerializedProperty spBrightnessSlider;
        private SerializedProperty spContrastSlider;

        private SerializedProperty spHue;
        private SerializedProperty spSaturation;
        private SerializedProperty spBrightness;
        private SerializedProperty spContrast;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spHueToggle = this.serializedObject.FindProperty("hueVar");
            this.spSaturationToggle = this.serializedObject.FindProperty("satVar");
            this.spBrightnessToggle = this.serializedObject.FindProperty("brtVar");
            this.spContrastToggle = this.serializedObject.FindProperty("cntVar");

            this.spHueSlider = this.serializedObject.FindProperty("_hue");
            this.spSaturationSlider = this.serializedObject.FindProperty("_saturation");
            this.spBrightnessSlider = this.serializedObject.FindProperty("_brightness");
            this.spContrastSlider = this.serializedObject.FindProperty("_contrast");

            this.spHue = this.serializedObject.FindProperty("camera_hue");
            this.spSaturation = this.serializedObject.FindProperty("camera_saturation");
            this.spBrightness = this.serializedObject.FindProperty("camera_brightness");
            this.spContrast = this.serializedObject.FindProperty("camera_contrast");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spHueToggle = null;
            this.spSaturationToggle = null;
            this.spBrightnessToggle = null;
            this.spContrastToggle = null;

            this.spHueSlider = null;
            this.spSaturationSlider = null;
            this.spBrightnessSlider = null;
            this.spContrastSlider = null;

            this.spHue = null;
            this.spSaturation = null; 
            this.spBrightness = null;
            this.spContrast = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.LabelField("Correcting all Colours in a Scene", EditorStyles.boldLabel);
            EditorGUILayout.Space();
          
            EditorGUILayout.LabelField("Colour Hue", EditorStyles.boldLabel);
          
            EditorGUILayout.PropertyField(this.spHueToggle, new GUIContent("Value from Variable"));
            if (hueVar == true)
            {
                EditorGUILayout.PropertyField(this.spHue, new GUIContent("Hue Value"));
            } 
            else
            { 
                EditorGUILayout.PropertyField(this.spHueSlider, new GUIContent("From -180 to +180"));
            }
          
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Colour Saturation", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(this.spSaturationToggle, new GUIContent("Value from Variable"));
            if (satVar == true)
            {
                EditorGUILayout.PropertyField(this.spSaturation, new GUIContent("Saturation Value"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spSaturationSlider, new GUIContent("From 0 to +3"));
            }

            EditorGUILayout.Space();
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
