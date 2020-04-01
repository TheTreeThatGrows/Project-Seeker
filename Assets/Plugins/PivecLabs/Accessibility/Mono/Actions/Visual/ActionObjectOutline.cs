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
	public class ActionObjectOutline : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------

        public Color outlineColour = Color.black;
        public bool colourVar = false;
        [VariableFilter(Variable.DataType.Color)]
        public VariableProperty outlineColourVar = new VariableProperty(Variable.VarType.GlobalVariable);


        [Tooltip("Outline Width value between 0 and 3.")]
        public bool widthVar = false;
        [Tooltip("Outline Only value between 0 and 3.")]
        public bool onlyVar = false;

        [Range(0.0f, 3.0f)] public float _outlineWidth = 0.5f;
        [Range(0.0f, 3.0f)] public float _outlineOnly = 0.0f;

        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty outlineWidth = new VariableProperty(Variable.VarType.GlobalVariable);
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty outlineOnly = new VariableProperty(Variable.VarType.GlobalVariable);


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

                 
                if (colourVar == true)
                {
                    cam.outlineColour = (Color)this.outlineColourVar.Get(target);
                }
                else
                {
                    cam.outlineColour = outlineColour;
                }

                if (widthVar == true)
                {
                    cam.outlineWidth = (float)this.outlineWidth.Get(target);
                }
                else
                {
                    cam.outlineWidth = _outlineWidth;
                }

                if (onlyVar == true)
                {
                    cam.outlineOnly = (float)this.outlineOnly.Get(target);
                }
                else
                {
                    cam.outlineOnly = _outlineOnly;
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

        public static new string NAME = "Accessibility/Visual/Object Outline";
		private const string NODE_TITLE = "Outline Objects";

		// PROPERTIES: ----------------------------------------------------------------------------

		private SerializedProperty spOutlineColour;
        private SerializedProperty spcolourVar;
        private SerializedProperty spoutlineColourVar;

        private SerializedProperty spToggleWidth;
        private SerializedProperty spToggleOnly;

        private SerializedProperty spOutlineWidthSlider;
        private SerializedProperty spOutlineOnlySLider;

        private SerializedProperty spOutlineWidth;
        private SerializedProperty spOutlineOnly;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
			this.spOutlineColour = this.serializedObject.FindProperty("outlineColour");
            this.spcolourVar = this.serializedObject.FindProperty("colourVar");
            this.spoutlineColourVar = this.serializedObject.FindProperty("outlineColourVar");

            this.spToggleWidth = this.serializedObject.FindProperty("widthVar");
            this.spToggleOnly = this.serializedObject.FindProperty("onlyVar");

            this.spOutlineWidthSlider = this.serializedObject.FindProperty("_outlineWidth");
            this.spOutlineOnlySLider = this.serializedObject.FindProperty("_outlineOnly");

            this.spOutlineWidth = this.serializedObject.FindProperty("outlineWidth");
            this.spOutlineOnly = this.serializedObject.FindProperty("outlineOnly");


        }

        protected override void OnDisableEditorChild ()
		{
			this.spOutlineColour = null;
            this.spcolourVar = null;
            this.spoutlineColourVar = null;

            this.spToggleWidth = null; 
            this.spToggleOnly = null;

            this.spOutlineWidthSlider = null;
            this.spOutlineOnlySLider = null;

            this.spOutlineWidth = null;
            this.spOutlineOnly = null;

        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spcolourVar, new GUIContent("Value from Variable"));
            if (colourVar == true)
            {
                EditorGUILayout.PropertyField(this.spoutlineColourVar, new GUIContent("Outline Colour"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spOutlineColour, new GUIContent("Outline Colour"));
            }


            EditorGUILayout.Space();
            EditorGUILayout.Space();


            EditorGUILayout.PropertyField(this.spToggleWidth, new GUIContent("Value from Variable"));
            if (widthVar == true)
            {
                EditorGUILayout.PropertyField(this.spOutlineWidth, new GUIContent("Outline Width"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spOutlineWidthSlider, new GUIContent("Outline Width"));
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.spToggleOnly, new GUIContent("Value from Variable"));
            if (onlyVar == true)
            {
                EditorGUILayout.PropertyField(this.spOutlineOnly, new GUIContent("Only Outline"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spOutlineOnlySLider, new GUIContent("Only Outline"));
            }
            EditorGUILayout.Space();
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
