namespace GameCreator.Accessibility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
    using UnityEngine.UI;
	using UnityEngine.Events;
	using GameCreator.Core;
    using GameCreator.Variables;

	#if UNITY_EDITOR
	using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionColourTouchStick : IAction
	{
        private Image graphicPanel;
        private Image graphicStick;
      
        public ColorProperty colorBase = new ColorProperty(Color.white);
        public ColorProperty colorStick = new ColorProperty(Color.white);
    
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

            GameObject touchstickP = GameObject.Find("PanelATouchStick");
            if (touchstickP != null) graphicPanel = touchstickP.GetComponent<Image>();
     
      
            GameObject touchstickS = GameObject.Find("ATStick");
            if (touchstickS != null) graphicStick = touchstickS.GetComponent<Image>();

            if (this.graphicPanel != null) this.graphicPanel.color = this.colorBase.GetValue(target);
            if (this.graphicStick != null) this.graphicStick.color = this.colorStick.GetValue(target);
    
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


        public static new string NAME = "Accessibility/Motoric/Colour TouchStick";
		private const string NODE_TITLE = "Change TouchStick Colour";

		// PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spColorBase;
        private SerializedProperty spColorStick;
  
        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
            return NODE_TITLE;
		}

		protected override void OnEnableEditorChild ()
		{
            this.spColorBase = this.serializedObject.FindProperty("colorBase");
            this.spColorStick = this.serializedObject.FindProperty("colorStick");
        }

        protected override void OnDisableEditorChild ()
		{
            this.spColorBase = null;
            this.spColorStick = null;
         }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();

            EditorGUILayout.PropertyField(this.spColorBase);

            EditorGUILayout.PropertyField(this.spColorStick);

   
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
