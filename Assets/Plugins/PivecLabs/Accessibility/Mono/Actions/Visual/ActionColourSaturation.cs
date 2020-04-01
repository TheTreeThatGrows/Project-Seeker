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
	public class ActionColourSaturation : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
            public bool satVar = false;
      
       
          [Range(0.0f, 3.0f)] public float _saturation = 1.0f;
     
      
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty camera_saturation = new VariableProperty(Variable.VarType.GlobalVariable);
  
     
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
        
                if (satVar == true)
                {
                    camcolour.saturation = (float)this.camera_saturation.Get(target);
                }
                else
                {
                    camcolour.saturation = _saturation;
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

        public static new string NAME = "Accessibility/Visual/Colour Saturation";
		private const string NODE_TITLE = "Correcting Colour Saturation";

        // PROPERTIES: ----------------------------------------------------------------------------
         private SerializedProperty spSaturationToggle;
     
         private SerializedProperty spSaturationSlider;
    
        private SerializedProperty spSaturation;
    
        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
             this.spSaturationToggle = this.serializedObject.FindProperty("satVar");
             this.spSaturationSlider = this.serializedObject.FindProperty("_saturation");
             this.spSaturation = this.serializedObject.FindProperty("camera_saturation");
   
        }

        protected override void OnDisableEditorChild ()
		{
            this.spSaturationToggle = null;
            this.spSaturationSlider = null;
            this.spSaturation = null; 
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.LabelField("Correcting all Colour Saturation in a Scene", EditorStyles.boldLabel);
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
           
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
