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
	public class ActionMoveButtonBar : IAction
	{
        public enum BARPOSITION
        {
            BottomLeft,
            BottomRight,
            TopRight,
            TopLeft
        }

        public BARPOSITION barPosition = BARPOSITION.BottomLeft;

        public NumberProperty resizeamount = new NumberProperty(0.0f);

        public bool resized = false;

        private AccessibilityButtonBar ButtonBar;
        private float tswidth;
        private RectTransform m_RectTransform;
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

   
              {

              ButtonBar = TouchStickManager.Instance.GetComponentInChildren<AccessibilityButtonBar>();
                if (ButtonBar != null)
                    m_RectTransform = ButtonBar.jsContainer.rectTransform;
              
                if (resized == true)
                {
                    if (ButtonBar != null)
                    {
                        ButtonBar.jsContainer.rectTransform.localScale += new Vector3(resizeamount.GetValue(target), resizeamount.GetValue(target), 0);
                        tswidth = (m_RectTransform.rect.width * (resizeamount.GetValue(target) + 1));
                    }
                        
                }
                else
                {
                    if (ButtonBar != null)
                    {
                        ButtonBar.jsContainer.rectTransform.localScale += new Vector3(0, 0, 0);
                        tswidth = (m_RectTransform.rect.width * (0 + 1));
                    }
                }

                if (ButtonBar != null)
                {
                    switch (this.barPosition)
                    {
                        case BARPOSITION.BottomLeft:
                            ButtonBar.jsContainer.transform.rotation = Quaternion.Euler(0, 0, 0);
                            ButtonBar.jsContainer.rectTransform.anchoredPosition = new Vector3(0, 0);
                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);

                            break;
                        case BARPOSITION.TopLeft:
                            ButtonBar.jsContainer.transform.rotation = Quaternion.Euler(0, 0, -90);
                            ButtonBar.jsContainer.rectTransform.anchoredPosition = new Vector3(0, Screen.height);
                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);

                            break;
                        case BARPOSITION.TopRight:
                            ButtonBar.jsContainer.transform.rotation = Quaternion.Euler(0, 0, 180);
                            ButtonBar.jsContainer.rectTransform.anchoredPosition = new Vector3(Screen.width, Screen.height);
                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                            break;
                        case BARPOSITION.BottomRight:
                            ButtonBar.jsContainer.transform.rotation = Quaternion.Euler(0, 0, 90);
                            ButtonBar.jsContainer.rectTransform.anchoredPosition = new Vector3(Screen.width, 0);
                            GameCreator.Camera.CameraMotorTypeAdventure.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                            GameCreator.Camera.CameraMotorTypeFirstPerson.MOBILE_RECT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);


                            break;

                    }

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

        public static new string NAME = "Accessibility/Motoric/Move ButtonBar";

		private const string NODE_TITLE = "ButtonBar is on the {0}";

		// PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spstickPosition;
        private SerializedProperty spstickresized;
        private SerializedProperty spstickResize;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE, this.barPosition);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spstickPosition = this.serializedObject.FindProperty("barPosition");
            this.spstickresized = this.serializedObject.FindProperty("resized");
            this.spstickResize = this.serializedObject.FindProperty("resizeamount");
        }

        protected override void OnDisableEditorChild ()
		{
	         this.spstickPosition = null;
            this.spstickresized = null;
            this.spstickResize = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();

             EditorGUILayout.PropertyField(this.spstickPosition, new GUIContent("Button Bar Position"));

            EditorGUILayout.PropertyField(this.spstickresized, new GUIContent("Button Bar resized?"));

            if (resized == true)
            {
                EditorGUILayout.PropertyField(this.spstickResize, new GUIContent("Resize Button Bar by"));

            }



            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
