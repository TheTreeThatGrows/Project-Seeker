namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Security.Cryptography;
    using System.Text;
 
    using UnityEngine.Events;
    using GameCreator.Core;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
    public class ActionEncryptToVariable : IAction
    {
    
        [VariableFilter(Variable.DataType.String)]
        public VariableProperty targetVariable = new VariableProperty(Variable.VarType.GlobalVariable);
        public InputField text;
        public string content = "{0}";
        public string encryptKey = "abcdefghijklmnopqrstuvwxyz123456";


        // EXECUTABLE: ----------------------------------------------------------------------------
        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            if (this.text != null)
            {

                byte[] encrypted = encrypting(this.text.text, encryptKey);

                string textString = System.Convert.ToBase64String(encrypted);

                this.targetVariable.Set(textString, target);


            }


            return true;
        }

        public static byte[] encrypting(string toEncrypt, string key)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
                RijndaelManaged rManaged = new RijndaelManaged();
                rManaged.Key = keyArray;
                rManaged.Mode = CipherMode.ECB;
                rManaged.Padding = PaddingMode.ISO10126;
                ICryptoTransform cTransform = rManaged.CreateEncryptor();
                return cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            }
            catch
            {
                Debug.Log("Encrypting Failed");
            }
            return null;
        }


        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";
        public static new string NAME = "Accessibility/Cognitive/Encrypt to Variable";

        private const string NODE_TITLE = "Encrypt to Variable";

        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty sptext;
        private SerializedProperty sptargetVariable;
        private SerializedProperty spKey;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {
            return string.Format(NODE_TITLE);
        }

        protected override void OnEnableEditorChild()
        {
            this.sptext = this.serializedObject.FindProperty("text");
            this.sptargetVariable = this.serializedObject.FindProperty("targetVariable");
            this.spKey = this.serializedObject.FindProperty("encryptKey");
        }

        protected override void OnDisableEditorChild()
        {
            this.sptext = null;
            this.sptargetVariable = null;
            this.spKey = null;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.sptext, new GUIContent("UI Text InputField"));

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.sptargetVariable, new GUIContent("Target Variable"));

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spKey, new GUIContent("Encryption Key"));
            EditorGUILayout.LabelField(new GUIContent("Must be 32 characters/numbers"));

            EditorGUILayout.Space();

            this.serializedObject.ApplyModifiedProperties();
        }

#endif
    }
}