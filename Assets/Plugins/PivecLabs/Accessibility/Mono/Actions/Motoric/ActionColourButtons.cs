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
	public class ActionColourButtons : IAction
	{
        private Image graphicButtonA;
        private Image graphicButtonB;
        private Image graphicButtonC;
        private Image graphicButtonD;
        private Image graphicButtonE;

        public ColorProperty colourButtonA = new ColorProperty(Color.white);
        public ColorProperty colourButtonB = new ColorProperty(Color.white);
        public ColorProperty colourButtonC = new ColorProperty(Color.white);
        public ColorProperty colourButtonD = new ColorProperty(Color.white);
        public ColorProperty colourButtonE = new ColorProperty(Color.white);

        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

            GameObject buttonA = GameObject.Find("ATSButtonA");
            if (buttonA != null) graphicButtonA = buttonA.GetComponent<Image>();
            if (this.graphicButtonA != null) this.graphicButtonA.color = this.colourButtonA.GetValue(target);

            GameObject buttonB = GameObject.Find("ATSButtonB");
            if (buttonB != null) graphicButtonB = buttonB.GetComponent<Image>();
            if (this.graphicButtonB != null) this.graphicButtonB.color = this.colourButtonB.GetValue(target);

            GameObject buttonC = GameObject.Find("ATSButtonC");
            if (buttonC != null) graphicButtonC = buttonC.GetComponent<Image>();
            if (this.graphicButtonC != null) this.graphicButtonC.color = this.colourButtonC.GetValue(target);

            GameObject buttonD = GameObject.Find("ATSButtonD");
            if (buttonD != null) graphicButtonD = buttonD.GetComponent<Image>();
            if (this.graphicButtonD != null) this.graphicButtonD.color = this.colourButtonD.GetValue(target);

            GameObject buttonE = GameObject.Find("ATSButtonE");
            if (buttonE != null) graphicButtonE = buttonE.GetComponent<Image>();
            if (this.graphicButtonE != null) this.graphicButtonE.color = this.colourButtonE.GetValue(target);

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

        public static new string NAME = "Accessibility/Motoric/Colour ButtonBar";

 		private const string NODE_TITLE = "Change ButtonBar Colour";

		// PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spColorButtonA;
        private SerializedProperty spColorButtonB;
        private SerializedProperty spColorButtonC;
        private SerializedProperty spColorButtonD;
        private SerializedProperty spColorButtonE;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
            return NODE_TITLE;
		}

		protected override void OnEnableEditorChild ()
		{
            this.spColorButtonA = this.serializedObject.FindProperty("colourButtonA");
            this.spColorButtonB = this.serializedObject.FindProperty("colourButtonB");
            this.spColorButtonC = this.serializedObject.FindProperty("colourButtonC");
            this.spColorButtonD = this.serializedObject.FindProperty("colourButtonD");
            this.spColorButtonE = this.serializedObject.FindProperty("colourButtonE");
        }

        protected override void OnDisableEditorChild ()
		{
            this.spColorButtonA = null;
            this.spColorButtonB = null;
            this.spColorButtonC = null;
            this.spColorButtonD = null;
            this.spColorButtonE = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();

     
            EditorGUILayout.PropertyField(this.spColorButtonA);
            EditorGUILayout.PropertyField(this.spColorButtonB);
            EditorGUILayout.PropertyField(this.spColorButtonC);
            EditorGUILayout.PropertyField(this.spColorButtonD);
            EditorGUILayout.PropertyField(this.spColorButtonE);

            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
