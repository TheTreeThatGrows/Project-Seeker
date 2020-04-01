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
	public class ActionGameSpeed : IAction 
	{
         public bool mainCameraMotorAdv = false;
         public GameCreator.Camera.CameraMotor cameraMotoradv;
        public bool mainCameraMotorFps = false;
        public GameCreator.Camera.CameraMotor cameraMotorfps;

        public bool timeScaleVar = false;
        public bool sensitivityVar = false;
        public bool sensitivity = false;
        public bool advSensitivityVar = false;
        public bool fpsSensitivityVar = false;

        [Range(0.0f, 1.0f)] public float _timeScale = 1.0f;

        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty timeScale = new VariableProperty(Variable.VarType.GlobalVariable);

        private Vector2 advsensitivity = new Vector2(10.0f, 10.0f);
        [Range(1f, 10.0f)] public float _advSensitivity = 10.0f;

        [VariableFilter(Variable.DataType.Bool)]
        public VariableProperty SensitivityVar = new VariableProperty(Variable.VarType.GlobalVariable);

        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty advSensitivity = new VariableProperty(Variable.VarType.GlobalVariable);

        private Vector2 fpssensitivity = new Vector2(2.0f, 2.0f);
        [Range(0f, 2.0f)] public float _fpsSensitivity = 2.0f;

        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty fpsSensitivity = new VariableProperty(Variable.VarType.GlobalVariable);


        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            if (timeScaleVar == true)
            {
              
                float timeScaleValue = (float)this.timeScale.Get(target);
                TimeManager.Instance.SetTimeScale(timeScaleValue);
            }
            else
            {

                TimeManager.Instance.SetTimeScale(_timeScale);
            }


          GameCreator.Camera.CameraMotor motoradv = (this.mainCameraMotorAdv ? GameCreator.Camera.CameraMotor.MAIN_MOTOR : this.cameraMotoradv);
          if (motoradv != null && motoradv.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeAdventure))
            {
               GameCreator.Camera.CameraMotorTypeAdventure adventureMotor = (GameCreator.Camera.CameraMotorTypeAdventure)motoradv.cameraMotorType;

               
                if (advSensitivityVar == true)
                {
                    advsensitivity = new Vector2((float)this.advSensitivity.Get(target), (float)this.advSensitivity.Get(target));
                    adventureMotor.sensitivity = new Vector2Property(advsensitivity);
                }
                else
                {
                    advsensitivity = new Vector2(_advSensitivity, _advSensitivity);
                    adventureMotor.sensitivity = new Vector2Property(advsensitivity);
                }
            }


            GameCreator.Camera.CameraMotor motorfps = (this.mainCameraMotorFps ? GameCreator.Camera.CameraMotor.MAIN_MOTOR : this.cameraMotorfps);
            if (motorfps != null && motorfps.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeFirstPerson))
            {
                GameCreator.Camera.CameraMotorTypeFirstPerson firstpersonMotor = (GameCreator.Camera.CameraMotorTypeFirstPerson)motorfps.cameraMotorType;
                if (fpsSensitivityVar == true)
                {
                    fpssensitivity = new Vector2((float)this.fpsSensitivity.Get(target), (float)this.fpsSensitivity.Get(target));
                    firstpersonMotor.sensitivity = new Vector2Property(fpssensitivity);
                }
                else
                {
                    fpssensitivity = new Vector2(_fpsSensitivity, _fpsSensitivity);
                    firstpersonMotor.sensitivity = new Vector2Property(fpssensitivity);
                }
            }



            return true;
        }

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR
        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Cognitive/Game Speed";
		private const string NODE_TITLE = "Change Game Speed";

		// PROPERTIES: ----------------------------------------------------------------------------

		private SerializedProperty spTimeScale;
        private SerializedProperty spTimeScaleslider;
        private SerializedProperty spTimeScaleVariable;
        private SerializedProperty spSensitivity;
        private SerializedProperty spSensitivityVar;
        private SerializedProperty spSensitivityVariable;
        private SerializedProperty spMainCameraMotorAdv;
        private SerializedProperty spCameraMotorAdv;
        private SerializedProperty spMainCameraMotorFps;
        private SerializedProperty spCameraMotorFps;
        private SerializedProperty spadvSensitivityVar;
        private SerializedProperty spfpsSensitivityVar;
        private SerializedProperty spadvSensitivity;
        private SerializedProperty spadvSensitivityslider;
        private SerializedProperty spfpsSensitivity;
        private SerializedProperty spfpsSensitivityslider;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
			this.spTimeScale = this.serializedObject.FindProperty("timeScale");
            this.spTimeScaleslider = this.serializedObject.FindProperty("_timeScale");
            this.spTimeScaleVariable = this.serializedObject.FindProperty("timeScaleVar");
            this.spSensitivity = this.serializedObject.FindProperty("sensitivity");
            this.spSensitivityVar = this.serializedObject.FindProperty("sensitivityVar");
            this.spSensitivityVariable = this.serializedObject.FindProperty("SensitivityVar");
            this.spMainCameraMotorAdv = this.serializedObject.FindProperty("mainCameraMotorAdv");
            this.spCameraMotorAdv = this.serializedObject.FindProperty("cameraMotoradv");
            this.spMainCameraMotorFps = this.serializedObject.FindProperty("mainCameraMotorFps");
            this.spCameraMotorFps = this.serializedObject.FindProperty("cameraMotorfps");
            this.spadvSensitivityVar = this.serializedObject.FindProperty("advSensitivityVar");
            this.spfpsSensitivityVar = this.serializedObject.FindProperty("fpsSensitivityVar");
            this.spadvSensitivity = this.serializedObject.FindProperty("advSensitivity");
            this.spadvSensitivityslider = this.serializedObject.FindProperty("_advSensitivity");
            this.spfpsSensitivity = this.serializedObject.FindProperty("fpsSensitivity");
            this.spfpsSensitivityslider = this.serializedObject.FindProperty("_fpsSensitivity");
        }

        protected override void OnDisableEditorChild ()
		{
			this.spTimeScale = null;
            this.spTimeScaleslider = null;
            this.spTimeScaleVariable = null;
            this.spSensitivity = null;
            this.spSensitivityVar = null;
            this.spSensitivityVariable = null;
            this.spMainCameraMotorAdv = null;
            this.spCameraMotorAdv = null;
            this.spMainCameraMotorFps = null;
            this.spCameraMotorFps = null;
            this.spadvSensitivityVar = null;
            this.spfpsSensitivityVar = null;
            this.spadvSensitivity = null;
            this.spadvSensitivityslider = null;
            this.spfpsSensitivity = null;
            this.spfpsSensitivityslider = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.spTimeScaleVariable, new GUIContent("Value from Variable"));

            if (timeScaleVar == true)
            {
                EditorGUILayout.PropertyField(this.spTimeScale, new GUIContent("Reduce Game Speed"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spTimeScaleslider, new GUIContent("Reduce Game Speed"));
            }
            EditorGUILayout.Space();


            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spSensitivityVar, new GUIContent("Value from Variable"));

            if (sensitivityVar == true)
            {
                EditorGUILayout.PropertyField(this.spSensitivityVariable, new GUIContent("Reduce Camera Sensitivity"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spSensitivity, new GUIContent("Reduce Camera Sensitivity"));
            }
            EditorGUILayout.Space();


            EditorGUILayout.Space();

            if (sensitivity == true)
            {
                EditorGUILayout.LabelField("Adventure Camera", EditorStyles.boldLabel);

                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUI.BeginDisabledGroup(this.spMainCameraMotorFps.boolValue);
                EditorGUILayout.PropertyField(this.spMainCameraMotorAdv, new GUIContent("Main Camera Motor"));
                EditorGUI.EndDisabledGroup();
                EditorGUI.BeginDisabledGroup(this.spMainCameraMotorAdv.boolValue);
                EditorGUILayout.PropertyField(this.spCameraMotorAdv, new GUIContent("Adventure Camera Motor"));
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(this.spadvSensitivityVar, new GUIContent("Value from Variable")); 

              if (advSensitivityVar == true)
                {
                    EditorGUILayout.PropertyField(this.spadvSensitivity, new GUIContent("Reduce Camera Sensitivity"));
                }
              else
                {
                    EditorGUILayout.PropertyField(this.spadvSensitivityslider, new GUIContent("Reduce Camera Sensitivity"));
                }


                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("First Person Camera", EditorStyles.boldLabel);

                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUI.BeginDisabledGroup(this.spMainCameraMotorAdv.boolValue);
                EditorGUILayout.PropertyField(this.spMainCameraMotorFps, new GUIContent("Main Camera Motor"));
                EditorGUI.EndDisabledGroup();
                EditorGUI.BeginDisabledGroup(this.spMainCameraMotorFps.boolValue);
                EditorGUILayout.PropertyField(this.spCameraMotorFps, new GUIContent("First Person Camera Motor"));
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(this.spfpsSensitivityVar, new GUIContent("Value from Variable"));

               if (fpsSensitivityVar == true)
                {
                    EditorGUILayout.PropertyField(this.spfpsSensitivity, new GUIContent("Reduce Camera Sensitivity"));
                }
               else
                {
                   EditorGUILayout.PropertyField(this.spfpsSensitivityslider, new GUIContent("Reduce Camera Sensitivity"));
                }

                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }

                this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}