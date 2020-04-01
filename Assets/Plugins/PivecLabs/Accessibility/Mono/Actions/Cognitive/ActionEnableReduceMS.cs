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
	public class ActionEnableReduceMS : IAction 
	{
        public bool mainCameraMotorAdv = false;
        public GameCreator.Camera.CameraMotor cameraMotoradv;
        public bool mainCameraMotorFps = false;
        public GameCreator.Camera.CameraMotor cameraMotorfps;
 
        public bool reduceheadbobVar = false;
        public bool _reduceheadbob = true;
        [VariableFilter(Variable.DataType.Bool)]
        public VariableProperty reduceheadbob = new VariableProperty(Variable.VarType.GlobalVariable);

        public bool increasefieldofviewVarFPS = false;
        [Range(1f, 180.0f)] public float _increasefieldofviewFPS = 60.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty increasefieldofviewFPS = new VariableProperty(Variable.VarType.GlobalVariable);

        public bool increasefieldofviewVarADV = false;
        [Range(1f, 180.0f)] public float _increasefieldofviewADV = 60.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty increasefieldofviewADV = new VariableProperty(Variable.VarType.GlobalVariable);
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
           
          

                GameCreator.Camera.CameraMotor motorfps = (this.mainCameraMotorFps ? GameCreator.Camera.CameraMotor.MAIN_MOTOR : this.cameraMotorfps);
                if (motorfps != null && motorfps.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeFirstPerson))
                {
                    GameCreator.Camera.CameraMotorTypeFirstPerson firstpersonMotor = (GameCreator.Camera.CameraMotorTypeFirstPerson)motorfps.cameraMotorType;

                    if (increasefieldofviewVarFPS == true)
                    {
                        firstpersonMotor.setCameraProperties = true;
                        firstpersonMotor.fieldOfView = (float)this.increasefieldofviewFPS.Get(target);
                    }
                    else
                    {
                        firstpersonMotor.setCameraProperties = true;
                        firstpersonMotor.fieldOfView = _increasefieldofviewFPS;
                    }



                    if (_reduceheadbob == true)
                    {

                        firstpersonMotor.headbobAmount = new Vector3(0.0f, 0.0f, 0.0f);

                    }

                }
                GameCreator.Camera.CameraMotor motoradv = (this.mainCameraMotorAdv ? GameCreator.Camera.CameraMotor.MAIN_MOTOR : this.cameraMotoradv);
                if (motoradv != null && motoradv.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeAdventure))
                {
                    GameCreator.Camera.CameraMotorTypeAdventure adventureMotor = (GameCreator.Camera.CameraMotorTypeAdventure)motoradv.cameraMotorType;


                    if (increasefieldofviewVarADV == true)
                    {
                        adventureMotor.setCameraProperties = true;
                        adventureMotor.fieldOfView = (float)this.increasefieldofviewADV.Get(target);
                    }
                    else
                    {
                        adventureMotor.setCameraProperties = true;
                        adventureMotor.fieldOfView = _increasefieldofviewADV;
                    }

                }

           
           
        
            return true;
        }

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR
        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Cognitive/Enable Reduce MS";
		private const string NODE_TITLE = "Reduce Motion Sickness";

        // PROPERTIES: ----------------------------------------------------------------------------
         private SerializedProperty spMainCameraMotorAdv;
        private SerializedProperty spCameraMotorAdv;
        private SerializedProperty spMainCameraMotorFps;
        private SerializedProperty spCameraMotorFps;
        private SerializedProperty spReduceHeadBob;
        private SerializedProperty spReduceHeadBobBool;
        private SerializedProperty spReduceHeadBobVariable;
        private SerializedProperty spIncreasefieldofviewFPS;
        private SerializedProperty spIncreasefieldofviewFPSFloat;
        private SerializedProperty spIncreasefieldofviewVarFPS;
        private SerializedProperty spIncreasefieldofviewADV;
        private SerializedProperty spIncreasefieldofviewADVFloat;
        private SerializedProperty spIncreasefieldofviewVarADV;


        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
             this.spMainCameraMotorAdv = this.serializedObject.FindProperty("mainCameraMotorAdv");
            this.spCameraMotorAdv = this.serializedObject.FindProperty("cameraMotoradv");
            this.spMainCameraMotorFps = this.serializedObject.FindProperty("mainCameraMotorFps");
            this.spCameraMotorFps = this.serializedObject.FindProperty("cameraMotorfps");
             this.spReduceHeadBob = this.serializedObject.FindProperty("reduceheadbob");
            this.spReduceHeadBobBool = this.serializedObject.FindProperty("_reduceheadbob");
            this.spReduceHeadBobVariable = this.serializedObject.FindProperty("reduceheadbobVar");
            this.spIncreasefieldofviewFPS = this.serializedObject.FindProperty("increasefieldofviewFPS");
            this.spIncreasefieldofviewFPSFloat = this.serializedObject.FindProperty("_increasefieldofviewFPS");
            this.spIncreasefieldofviewVarFPS = this.serializedObject.FindProperty("increasefieldofviewVarFPS");
            this.spIncreasefieldofviewADV = this.serializedObject.FindProperty("increasefieldofviewADV");
            this.spIncreasefieldofviewADVFloat = this.serializedObject.FindProperty("_increasefieldofviewADV");
            this.spIncreasefieldofviewVarADV = this.serializedObject.FindProperty("increasefieldofviewVarADV");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spMainCameraMotorAdv = null;
            this.spCameraMotorAdv = null;
            this.spMainCameraMotorFps = null;
            this.spCameraMotorFps = null;
            this.spReduceHeadBob = null;
            this.spReduceHeadBobBool = null;
            this.spReduceHeadBobVariable = null;
            this.spIncreasefieldofviewFPS = null;
            this.spIncreasefieldofviewFPSFloat = null;
            this.spIncreasefieldofviewVarFPS = null;
            this.spIncreasefieldofviewADV = null;
            this.spIncreasefieldofviewADVFloat = null;
            this.spIncreasefieldofviewVarADV = null;

        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
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
                EditorGUILayout.PropertyField(this.spIncreasefieldofviewVarFPS, new GUIContent("Value from Variable"));
                EditorGUILayout.LabelField("Increase", EditorStyles.boldLabel);

                if (increasefieldofviewVarFPS == true)
                {
                    EditorGUILayout.PropertyField(this.spIncreasefieldofviewFPS, new GUIContent("Field of View"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spIncreasefieldofviewFPSFloat, new GUIContent("Field of View"));
                }
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(this.spReduceHeadBobVariable, new GUIContent("Value from Variable"));
                EditorGUILayout.LabelField("Remove", EditorStyles.boldLabel);

                  if (reduceheadbobVar == true)
                {
                    EditorGUILayout.PropertyField(this.spReduceHeadBob, new GUIContent("HeadBob (FP Camera)"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spReduceHeadBobBool, new GUIContent("HeadBob (FP Camera)"));
                }



                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();




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
                EditorGUILayout.PropertyField(this.spIncreasefieldofviewVarADV, new GUIContent("Value from Variable"));
                EditorGUILayout.LabelField("Increase", EditorStyles.boldLabel);

                if (increasefieldofviewVarADV == true)
                {
                    EditorGUILayout.PropertyField(this.spIncreasefieldofviewADV, new GUIContent("Field of View"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spIncreasefieldofviewADVFloat, new GUIContent("Field of View"));
                }
                EditorGUILayout.Space();


                EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
         
              
                this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}