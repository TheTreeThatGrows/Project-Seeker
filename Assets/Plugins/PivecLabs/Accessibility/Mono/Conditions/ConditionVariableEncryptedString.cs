namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using System.Security.Cryptography;
    using System.Text;

    using GameCreator.Core;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [AddComponentMenu("")]
    public class ConditionVariableEncryptedString : ICondition
    {
        [VariableFilter(Variable.DataType.String)]
        public VariableProperty variable = new VariableProperty();
        public StringProperty compareTo = new StringProperty();
        public string decryptKey = "abcdefghijklmnopqrstuvwxyz123456";
        private string decrypted;

        // OVERRIDERS: ----------------------------------------------------------------------------

        public override bool Check(GameObject target)
        {

            if (this.compareTo.GetValue(target) != null)
            {
                byte[] encryptedBytes = System.Convert.FromBase64String(this.variable.ToStringValue(target));

                decrypted = decrypting(encryptedBytes, decryptKey);


            }
            
	        
            return (string)this.compareTo.GetValue(target) == this.decrypted;
        }

        public static string decrypting(byte[] toEncryptArray, string key)
        {
            try
            {

                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                RijndaelManaged rManaged = new RijndaelManaged();
                rManaged.Key = keyArray;
                rManaged.Mode = CipherMode.ECB;
                rManaged.Padding = PaddingMode.ISO10126;
                ICryptoTransform cTransform = rManaged.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            catch

            {
                Debug.Log("Decrypting Failed");
            }
            return null;
        }
        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";
        public static new string NAME = "Accessibility/Conditions/Variable Encrypted String";
        private const string NODE_TITLE = "Comparing {0} with {1}";

        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spcompareTo;
        private SerializedProperty spvariable;
        private SerializedProperty spKey;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {
            return string.Format(NODE_TITLE, this.variable, this.compareTo);
        }

        protected override void OnEnableEditorChild()
        {
            this.spcompareTo = this.serializedObject.FindProperty("compareTo");
            this.spvariable = this.serializedObject.FindProperty("variable");
            this.spKey = this.serializedObject.FindProperty("decryptKey");
        }

        protected override void OnDisableEditorChild()
        {
            this.spcompareTo = null;
            this.spvariable = null;
            this.spKey = null;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.spvariable, new GUIContent("Encrypted Variable"));
            EditorGUILayout.PropertyField(this.spcompareTo, new GUIContent("Text String"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spKey, new GUIContent("Decryption Key "));
            EditorGUILayout.LabelField(new GUIContent("Must be identical to Encryption Key"));


            EditorGUILayout.Space();

            this.serializedObject.ApplyModifiedProperties();

        }
#endif
	}
}