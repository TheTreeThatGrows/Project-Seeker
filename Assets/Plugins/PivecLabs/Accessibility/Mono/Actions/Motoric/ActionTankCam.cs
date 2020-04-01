namespace GameCreator.Accessibility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using GameCreator.Core;
    using GameCreator.Characters;
    using GameCreator.Core.Hooks;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionTankCam : IAction
	{
        private PlayerCharacter player;
        public bool enableVariable = false;
		public bool enableTankCam = false;
		[VariableFilter(Variable.DataType.Bool)]
		public VariableProperty tankcamVariable = new VariableProperty(Variable.VarType.GlobalVariable);

        public TankCamComponent tankcamera;

     
        public bool sensitivityVar = false;
        [Range(0.0f, 5.0f)] public float _sensitivity = 2.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty sensitivity = new VariableProperty(Variable.VarType.GlobalVariable);


        public bool characterleanVar = false;
        [Range(0.0f, 30.0f)] public float _characterlean = 10.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty characterlean = new VariableProperty(Variable.VarType.GlobalVariable);

        public bool angularspeedVar = false;
        [Range(0.0f, 720.0f)] public float _angularspeed = 720.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty angularspeed = new VariableProperty(Variable.VarType.GlobalVariable);



        public enum FACE_DIRECTION
        {
            MovementDirection,
            CameraDirection
        }
     
      
        public CharacterLocomotion.FACE_DIRECTION direction;
        public FACE_DIRECTION faceDirection = FACE_DIRECTION.MovementDirection;

        public bool camerarotate = true;


        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {


            if (enableVariable == true)
            {
                if (tankcamera != null)
                    tankcamera.enableTankCamera = (bool)this.tankcamVariable.Get(target);
            }
            else
            {
                if (tankcamera != null)
                {

                    if (enableTankCam == true)
                    {
                        tankcamera.enableTankCamera = true;
                    }
                    else
                    {
                        tankcamera.enableTankCamera = false;
                    }

                }
            }

                if (tankcamera.enableTankCamera == true)

                {
                    player = HookPlayer.Instance.Get<PlayerCharacter>();

                    if (sensitivityVar == true)
                    {
                        float value = (float)this.sensitivity.Get(target);
                        tankcamera.Sensitivity = value;
                    }
                    else
                    {
                        tankcamera.Sensitivity = _sensitivity;
                    }


                    if (angularspeedVar == true)
                    {
                        float value = (float)this.angularspeed.Get(target);
                        player.characterLocomotion.angularSpeed = value;
                    }
                    else
                    {
                        player.characterLocomotion.angularSpeed = _angularspeed;
                    }


                    if (characterleanVar == true)
                    {
                        float value = (float)this.characterlean.Get(target);
                        tankcamera.LeftLean = value;
                        tankcamera.RightLean = -value;

                    }
                    else
                    {
                        tankcamera.LeftLean = _characterlean;
                        tankcamera.RightLean = -_characterlean;
                    }

                    switch (this.faceDirection)
                    {
                        case FACE_DIRECTION.CameraDirection:
                            player.characterLocomotion.faceDirection = CharacterLocomotion.FACE_DIRECTION.CameraDirection;
                            break;
                        case FACE_DIRECTION.MovementDirection:
                            player.characterLocomotion.faceDirection = CharacterLocomotion.FACE_DIRECTION.MovementDirection;
                            break;
                    }

                    if(camerarotate == true)
                    {
                        var controller = HookCamera.Instance.Get<GameCreator.Camera.CameraController>();

                        if (controller.currentCameraMotor != null && controller.currentCameraMotor.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeAdventure))
                        {
                            var orbit = controller.currentCameraMotor.GetComponent<GameCreator.Camera.CameraMotorTypeAdventure>();
                            orbit.orbitInput = GameCreator.Camera.CameraMotorTypeAdventure.OrbitInput.HoldRightMouse;

                        }

                        if (controller.currentCameraMotor != null && controller.currentCameraMotor.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeFirstPerson))
                        {
                            var orbit = controller.currentCameraMotor.GetComponent<GameCreator.Camera.CameraMotorTypeFirstPerson>();
                            orbit.rotateInput = GameCreator.Camera.CameraMotorTypeFirstPerson.RotateInput.HoldRightMouse;


                        }


                    }



               

            }
            return true;
        }

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Motoric/Tank Camera";
        private const string NODE_TITLE = "Enable Tank Camera";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spTankcam;

        private SerializedProperty spenableTankcam;
        private SerializedProperty spenableVariable;
        private SerializedProperty sptankcamVariable;
        private SerializedProperty spSensitivityToggle;
        private SerializedProperty spSensitivitySlider;
        private SerializedProperty spSensitivity;
        private SerializedProperty spCharacterLeanToggle;
        private SerializedProperty spCharacterLeanSlider;
        private SerializedProperty spCharacterLean;
        private SerializedProperty spAngularSpeedToggle;
        private SerializedProperty spAngularSpeedSlider;
        private SerializedProperty spAngularSpeed;
        private SerializedProperty spDirection;
        private SerializedProperty spRotate;


        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
            return string.Format(NODE_TITLE);
        }

		protected override void OnEnableEditorChild ()
		{
            this.spTankcam = this.serializedObject.FindProperty("tankcamera");
            this.spenableTankcam = this.serializedObject.FindProperty("enableTankCam");
            this.spenableVariable = this.serializedObject.FindProperty("enableVariable");
            this.sptankcamVariable = this.serializedObject.FindProperty("tankcamVariable");
            this.spSensitivityToggle = this.serializedObject.FindProperty("sensitivityVar");
            this.spSensitivitySlider = this.serializedObject.FindProperty("_sensitivity");
            this.spSensitivity = this.serializedObject.FindProperty("sensitivity");
            this.spCharacterLeanToggle = this.serializedObject.FindProperty("characterleanVar");
            this.spCharacterLeanSlider = this.serializedObject.FindProperty("_characterlean");
            this.spCharacterLean = this.serializedObject.FindProperty("characterlean");
            this.spAngularSpeedToggle = this.serializedObject.FindProperty("angularspeedVar");
            this.spAngularSpeedSlider = this.serializedObject.FindProperty("_angularspeed");
            this.spAngularSpeed = this.serializedObject.FindProperty("angularspeed");
            this.spDirection = this.serializedObject.FindProperty("faceDirection");
            this.spRotate = this.serializedObject.FindProperty("camerarotate");

        }

        protected override void OnDisableEditorChild ()
		{
            this.spTankcam = null;
            this.spenableTankcam = null;
            this.spenableVariable = null;
            this.sptankcamVariable = null;
            this.spSensitivityToggle = null;
            this.spSensitivitySlider = null;
            this.spSensitivity = null;
            this.spCharacterLeanToggle = null;
            this.spCharacterLeanSlider = null;
            this.spCharacterLean = null;
            this.spAngularSpeedToggle = null;
            this.spAngularSpeedSlider = null;
            this.spAngularSpeed = null;
            this.spDirection = null;
            this.spRotate = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.spTankcam, new GUIContent("TankCamera Prefab"));

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.spenableVariable, new GUIContent("Value from Variable"));

            if (enableVariable == true)
            {
                EditorGUILayout.PropertyField(this.sptankcamVariable, new GUIContent("Enable Tank Camera"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spenableTankcam, new GUIContent("Enable Tank Camera"));
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Sensitivity", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spSensitivityToggle, new GUIContent("Value from Variable"));
            if (sensitivityVar == true)
            {
                EditorGUILayout.PropertyField(this.spSensitivity, new GUIContent("Sensitivity"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spSensitivitySlider, new GUIContent("From 0 to 5"));
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Character Lean", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spCharacterLeanToggle, new GUIContent("Value from Variable"));
            if (characterleanVar == true)
            {
                EditorGUILayout.PropertyField(this.spCharacterLean, new GUIContent("Character Lean"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spCharacterLeanSlider, new GUIContent("From 0 to 30"));
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Angular Speed", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spAngularSpeedToggle, new GUIContent("Value from Variable"));
            if (angularspeedVar == true)
            {
                EditorGUILayout.PropertyField(this.spAngularSpeed, new GUIContent("Angular Speed"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spAngularSpeedSlider, new GUIContent("From 0 to 720"));
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Movement Diection", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spDirection, new GUIContent("Character Faces"));

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Camera Rotate", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spRotate, new GUIContent("Right Mouse Hold"));

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
