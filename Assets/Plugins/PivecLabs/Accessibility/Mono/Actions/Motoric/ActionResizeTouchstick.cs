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
	public class ActionResizeTouchstick : IAction
	{
        public NumberProperty resizeamount = new NumberProperty(0.0f);

        private TouchStick touchStick;

        private RectTransform m_RectTransform;
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

          

             {

                touchStick = TouchStickManager.Instance.GetComponentInChildren<TouchStick>();

                if (touchStick != null)
                {

                    touchStick.jsContainer.rectTransform.localScale += new Vector3(resizeamount.GetValue(target), resizeamount.GetValue(target), 0);

                    m_RectTransform = touchStick.jsContainer.rectTransform;


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

        public static new string NAME = "Accessibility/Motoric/Resize TouchStick";
        private const string NODE_TITLE = "Touchstick is {0} times bigger";

		// PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spstickResize;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
            return string.Format(NODE_TITLE, (resizeamount == null ? "none" : resizeamount.value.ToString()));

        }

		protected override void OnEnableEditorChild ()
		{
            this.spstickResize = this.serializedObject.FindProperty("resizeamount");
        }

        protected override void OnDisableEditorChild ()
		{
	         this.spstickResize = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();

             EditorGUILayout.PropertyField(this.spstickResize, new GUIContent("Resize Touch Stick by"));

            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
