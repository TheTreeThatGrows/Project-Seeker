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
	public class ActionEnableClosedCaptions : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
        public bool enableVariable = false;

        public bool enableCaptioning = false;

        [VariableFilter(Variable.DataType.Bool)]
        public VariableProperty captionVariable = new VariableProperty(Variable.VarType.GlobalVariable);

        public Color textcolor = Color.white;
        public float timeShowing = 2.0f;
        public float timeBetweenScans = 1.0f;


        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {


          
                if (enableVariable == true)
                {
                    enableCaptioning = (bool)this.captionVariable.Get(target);
                }
               
                if (enableCaptioning == true)
                    {   
                        SoundClosedCaptions.cc.color = textcolor;
                        SoundClosedCaptions.cc.timeShowing = timeShowing;
                        SoundClosedCaptions.cc.timeBetweenScans = timeBetweenScans;
                        SoundClosedCaptions.startCC();

                    }
                    else
                    {
                        SoundClosedCaptions.stopCC();
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

        public static new string NAME = "Accessibility/Auditory/Enable Closed Captions";
		private const string NODE_TITLE = "Enable Closed Captions";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spenableCaptioning;
        private SerializedProperty spenableVariable;
        private SerializedProperty spcaptionVariable;
        private SerializedProperty sptextcolor;
        private SerializedProperty sptimeShowing;
        private SerializedProperty sptimeBetweenScans;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spenableCaptioning = this.serializedObject.FindProperty("enableCaptioning");
            this.spenableVariable = this.serializedObject.FindProperty("enableVariable");
            this.spcaptionVariable = this.serializedObject.FindProperty("captionVariable");
            this.sptextcolor = this.serializedObject.FindProperty("textcolor");
            this.sptimeShowing = this.serializedObject.FindProperty("timeShowing");
            this.sptimeBetweenScans = this.serializedObject.FindProperty("timeBetweenScans");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spenableCaptioning = null;
            this.spenableVariable = null;
            this.spcaptionVariable = null;
            this.sptextcolor = null;
            this.sptimeShowing = null;
            this.sptimeBetweenScans = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spenableVariable, new GUIContent("Value from Variable"));

            if (enableVariable == true)
            {
               EditorGUILayout.PropertyField(this.spcaptionVariable, new GUIContent("Enable Closed Captions"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spenableCaptioning, new GUIContent("Enable Captions"));
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.sptextcolor, new GUIContent("Text Colour"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.sptimeShowing, new GUIContent("Display time"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.sptimeBetweenScans, new GUIContent("Time between scans"));

            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
