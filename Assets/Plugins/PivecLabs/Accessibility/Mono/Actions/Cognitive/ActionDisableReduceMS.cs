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
	public class ActionDisableReduceMMS : IAction 
	{
        public bool mainCameraMotorAdv = false;
        public GameCreator.Camera.CameraMotor cameraMotoradv;
        public bool mainCameraMotorFps = false;
        public GameCreator.Camera.CameraMotor cameraMotorfps;

    
           // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
           
           


                GameCreator.Camera.CameraMotor motorfps = (this.mainCameraMotorFps ? GameCreator.Camera.CameraMotor.MAIN_MOTOR : this.cameraMotorfps);
                if (motorfps != null && motorfps.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeFirstPerson))
                {
                    GameCreator.Camera.CameraMotorTypeFirstPerson firstpersonMotor = (GameCreator.Camera.CameraMotorTypeFirstPerson)motorfps.cameraMotorType;

                    firstpersonMotor.setCameraProperties = true;
                    firstpersonMotor.fieldOfView = 60.0f;

                    firstpersonMotor.headbobAmount = new Vector3(0.05f, 0.05f, 0.01f);



                }
                GameCreator.Camera.CameraMotor motoradv = (this.mainCameraMotorAdv ? GameCreator.Camera.CameraMotor.MAIN_MOTOR : this.cameraMotoradv);
                if (motoradv != null && motoradv.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeAdventure))
                {
                    GameCreator.Camera.CameraMotorTypeAdventure adventureMotor = (GameCreator.Camera.CameraMotorTypeAdventure)motoradv.cameraMotorType;

                    adventureMotor.setCameraProperties = true;
                    adventureMotor.fieldOfView = 60.0f;

                }

           
        
            return true;
        }

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR
        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Cognitive/Disable Reduce MS";
		private const string NODE_TITLE = "Disable Reduce Motion Sickness";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spMainCameraMotorAdv;
        private SerializedProperty spCameraMotorAdv;
        private SerializedProperty spMainCameraMotorFps;
        private SerializedProperty spCameraMotorFps;
 

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
 
        }

        protected override void OnDisableEditorChild ()
		{
            this.spMainCameraMotorAdv = null;
            this.spCameraMotorAdv = null;
            this.spMainCameraMotorFps = null;
            this.spCameraMotorFps = null;
 
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
         
                EditorGUILayout.Space();


                EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
         
               
                this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}