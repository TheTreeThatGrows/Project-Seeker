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
	public class ActionMoveTouchstick : IAction
	{
        public enum STICKPOSITION
        {
            BottomLeft,
            BottomRight,
            TopRight,
            TopLeft
        }

        public STICKPOSITION stickPostion = STICKPOSITION.BottomLeft;
        public NumberProperty resizeamount = new NumberProperty(0.0f);

        public bool resized = false;
     
        private TouchStick touchStick;
        private float tswidth;
        private RectTransform m_RectTransform;
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {


            {
                touchStick = TouchStickManager.Instance.GetComponentInChildren<TouchStick>();
                if (touchStick != null)
                    m_RectTransform = touchStick.jsContainer.rectTransform;

                if (resized == true)
                {
                    if (touchStick != null)
                    {
                        touchStick.jsContainer.rectTransform.localScale += new Vector3(resizeamount.GetValue(target), resizeamount.GetValue(target), 0);
                        tswidth = (m_RectTransform.rect.width * (resizeamount.GetValue(target) + 1));
                    }
                        
                 }
               else
                {
                    if (touchStick != null)
                    {
                        touchStick.jsContainer.rectTransform.localScale += new Vector3(0, 0, 0);
                        tswidth = (m_RectTransform.rect.width * (0 + 1));
                    }
                }

                if (touchStick != null)
                {
                    switch (this.stickPostion)
                    {
                        case STICKPOSITION.BottomLeft:
                            touchStick.jsContainer.rectTransform.anchoredPosition = new Vector3(0, 0);
                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);

                            break;
                        case STICKPOSITION.TopLeft:
                            touchStick.jsContainer.rectTransform.anchoredPosition = new Vector3(0, Screen.height - (tswidth));
                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);

                            break;
                        case STICKPOSITION.TopRight:
                            touchStick.jsContainer.rectTransform.anchoredPosition = new Vector3(Screen.width - (+tswidth), Screen.height - (tswidth));
                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                            break;
                        case STICKPOSITION.BottomRight:
                            touchStick.jsContainer.rectTransform.anchoredPosition = new Vector3(Screen.width - (tswidth), 0);
                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);


                            break;

                    }



                    touchStick.joystick.rectTransform.anchoredPosition = Vector3.zero;

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


        public static new string NAME = "Accessibility/Motoric/Move TouchStick";

        private const string NODE_TITLE = "Touchstick is on the {0}";

		// PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spstickPostion;
        private SerializedProperty spstickresized;
        private SerializedProperty spstickResize;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE, this.stickPostion);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spstickPostion = this.serializedObject.FindProperty("stickPostion");
            this.spstickresized = this.serializedObject.FindProperty("resized");
            this.spstickResize = this.serializedObject.FindProperty("resizeamount");
        }

        protected override void OnDisableEditorChild ()
		{
	         this.spstickPostion = null;
            this.spstickresized = null;
            this.spstickResize = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();

             EditorGUILayout.PropertyField(this.spstickPostion, new GUIContent("Touch Stick Position"));
            EditorGUILayout.PropertyField(this.spstickresized, new GUIContent("Touch Stick resized?"));

            if (resized == true)
            {
                EditorGUILayout.PropertyField(this.spstickResize, new GUIContent("Resize Touch Stick by"));

            }


            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
