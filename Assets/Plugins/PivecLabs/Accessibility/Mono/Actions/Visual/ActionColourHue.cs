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
	public class ActionColourHue : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
         public bool hueVar = false;
       
       
        [Range(-180f, 180f)] public float _hue = 1.0f;
       
      
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty camera_hue = new VariableProperty(Variable.VarType.GlobalVariable);
       
     
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

        public static new string NAME = "Accessibility/Visual/Colour Hue";
		private const string NODE_TITLE = "Correcting Colour Hue";

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
           
            this.spHueSlider = this.serializedObject.FindProperty("_hue");
         
            this.spHue = this.serializedObject.FindProperty("camera_hue");
        
        }

        protected override void OnDisableEditorChild ()
		{
            this.spHueToggle = null;
         
            this.spHueSlider = null;
        
            this.spHue = null;
         }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.LabelField("Correcting all Colour Hue in a Scene", EditorStyles.boldLabel);
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

            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
