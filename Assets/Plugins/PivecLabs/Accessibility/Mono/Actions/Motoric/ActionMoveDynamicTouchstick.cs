namespace GameCreator.Accessibility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
    using UnityEngine.UI;

    using UnityEngine.Events;
	using GameCreator.Core;
    using GameCreator.Core.Hooks;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionMoveDynamicTouchstick : IAction
	{
        public enum STICKPOSITION
        {
            Left,
            Right
        }

        public STICKPOSITION stickPostion = STICKPOSITION.Left;
   
		private RectTransform m_RectTransform;
         
		private RectTransform ds;
		
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {


                   switch (this.stickPostion)
                    {
                        case STICKPOSITION.Left:
	                        TouchStickManager.Instance.GetComponentInChildren<DynamicTouchStick>().right = false;
	                        ds = GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<DynamicTouchStick>().GetComponent<RectTransform>();
	                        ds.anchoredPosition =  new Vector2 ((ds.rect.width/2),  ds.rect.y);
  
                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);

                            break;
                       
                        case STICKPOSITION.Right:
	                        TouchStickManager.Instance.GetComponentInChildren<DynamicTouchStick>().right = true;
	                        ds = GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<DynamicTouchStick>().GetComponent<RectTransform>();
	                        ds.anchoredPosition =  new Vector2 ((Screen.width -( ds.rect.width/2)),  ds.rect.y);

                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);


                            break;

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


		public static new string NAME = "Accessibility/Motoric/Move Dynamic TouchStick";

		private const string NODE_TITLE = "Dynamic Touchstick is on the {0}";

		// PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spstickPostion;
   
        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE, this.stickPostion);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spstickPostion = this.serializedObject.FindProperty("stickPostion");
         }

        protected override void OnDisableEditorChild ()
		{
	         this.spstickPostion = null;
         }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
			EditorGUILayout.LabelField(new GUIContent("Dynamic"));
			EditorGUILayout.PropertyField(this.spstickPostion, new GUIContent("Touchstick Position"));
     

            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
