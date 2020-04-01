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
	public class ActionEnableCrosshairs : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
        public bool enableVariable = false;

        public bool showCrosshairs = false;
             [VariableFilter(Variable.DataType.Bool)]
        public VariableProperty showCrosshairsVariable = new VariableProperty(Variable.VarType.GlobalVariable);

        public CanvasGroup canvasGroup;

        [Range(0.0f, 1.0f)] public float alphaslider = 0.5f;
        public bool enablealphavariable = false;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty alphavariable = new VariableProperty(Variable.VarType.GlobalVariable);

        public Image image;
        public SpriteProperty sprite = new SpriteProperty();

        [Range(1.0f, 10.0f)] public float imageslider = 1.0f;
        public bool enableimagevariable = false;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty imagevariable = new VariableProperty(Variable.VarType.GlobalVariable);

        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

            if (enableVariable == true)
            {
                showCrosshairs = (bool)this.showCrosshairsVariable.Get(target);
            }

            if (showCrosshairs == true)
            {
                if (enablealphavariable == true)
                {
                   this.canvasGroup.alpha = (float)this.alphavariable.Get(target);
                }
                else
                {

                    this.canvasGroup.alpha = alphaslider;
                }


                image = this.canvasGroup.transform.Find("CrosshairImage").gameObject.GetComponent<Image>();

                if (this.image != null)
                {
                    if (enableimagevariable == true)
                    {
                        float imagevar = (float)this.imagevariable.Get(target);
                        this.image.rectTransform.sizeDelta = new Vector2((imagevar * 100), (imagevar * 100));
                    }
                    else
                    {

                        this.image.rectTransform.sizeDelta = new Vector2((imageslider * 100), (imageslider * 100));
                    }



                }

            }
            else
            {
                this.canvasGroup.alpha = 0.0f;

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

        public static new string NAME = "Accessibility/Cognitive/Enable Crosshairs";
		private const string NODE_TITLE = "Enable Crosshairs";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spshowCrosshairs;
        private SerializedProperty spenableVariable;
        private SerializedProperty spshowCrosshairsVariable;
        private SerializedProperty spCanvasGroup;
        private SerializedProperty spenableAlpha;
        private SerializedProperty spAlphaSlider;
        private SerializedProperty spAlphaVariable;
        private SerializedProperty spenableimage;
        private SerializedProperty spimageSlider;
        private SerializedProperty spimageVariable;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spshowCrosshairs = this.serializedObject.FindProperty("showCrosshairs");
            this.spenableVariable = this.serializedObject.FindProperty("enableVariable");
            this.spshowCrosshairsVariable = this.serializedObject.FindProperty("showCrosshairsVariable");
            this.spCanvasGroup = this.serializedObject.FindProperty("canvasGroup");
            this.spenableAlpha = this.serializedObject.FindProperty("enablealphavariable");
            this.spAlphaSlider = this.serializedObject.FindProperty("alphaslider");
            this.spAlphaVariable = this.serializedObject.FindProperty("alphavariable");
            this.spenableimage = this.serializedObject.FindProperty("enableimagevariable");
            this.spimageSlider = this.serializedObject.FindProperty("imageslider");
            this.spimageVariable = this.serializedObject.FindProperty("imagevariable");
        }

        protected override void OnDisableEditorChild ()
		{
            this.spshowCrosshairs = null;
            this.spenableVariable = null;
            this.spshowCrosshairsVariable = null;
            this.spCanvasGroup = null;
            this.spenableAlpha = null;
            this.spAlphaSlider = null;
            this.spAlphaVariable = null;
            this.spenableimage = null;
            this.spimageSlider = null;
            this.spimageVariable = null;
        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spenableVariable, new GUIContent("Value from Variable"));

            if (enableVariable == true)
            {
                EditorGUILayout.PropertyField(this.spshowCrosshairsVariable, new GUIContent("Show Crosshairs"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spshowCrosshairs, new GUIContent("Show Crosshairs"));
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spCanvasGroup);

            EditorGUILayout.PropertyField(this.spenableAlpha, new GUIContent("Value from Variable"));

            if (enablealphavariable == true)
            {
                EditorGUILayout.PropertyField(this.spAlphaVariable, new GUIContent("Crosshairs Alpha"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spAlphaSlider, new GUIContent("Crosshairs Alpha"));
            }
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spenableimage, new GUIContent("Value from Variable"));

            if (enableimagevariable == true)
            {
                EditorGUILayout.PropertyField(this.spimageVariable, new GUIContent("Enlarge Crosshairs"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spimageSlider, new GUIContent("Enlarge Crosshairs"));
            }

            EditorGUILayout.Space();
        
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
