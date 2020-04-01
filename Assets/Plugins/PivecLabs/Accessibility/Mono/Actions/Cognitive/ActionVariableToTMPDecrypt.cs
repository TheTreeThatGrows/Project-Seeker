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
    using TMPro;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
    public class ActionVariableToTMPDecrypt : IAction
    {
    
        [VariableFilter(Variable.DataType.String)]
        public VariableProperty targetVariable = new VariableProperty(Variable.VarType.GlobalVariable);
        public TMP_Text tmptext;
        public string content = "{0}";
        public string decryptKey = "abcdefghijklmnopqrstuvwxyz123456";


        // EXECUTABLE: ----------------------------------------------------------------------------
        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            if (this.tmptext != null)
            {
                byte[] encryptedBytes = System.Convert.FromBase64String(this.targetVariable.ToStringValue(target));
            
                string decrypted = decrypting(encryptedBytes, decryptKey);

                this.tmptext.text = decrypted;
            }




            return true;
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

	    public static new string NAME = "Accessibility/Cognitive/Variable To TMP_Decrypt";
	    private const string NODE_TITLE = "Variable To TMP_Decrypt";

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
            this.sptext = this.serializedObject.FindProperty("tmptext");
            this.sptargetVariable = this.serializedObject.FindProperty("targetVariable");
            this.spKey = this.serializedObject.FindProperty("decryptKey");
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
            EditorGUILayout.PropertyField(this.sptargetVariable, new GUIContent("Target Variable"));

            EditorGUILayout.Space();
             EditorGUILayout.PropertyField(this.sptext, new GUIContent("UI TMP_Text Field"));

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spKey, new GUIContent("Decryption Key "));
            EditorGUILayout.LabelField(new GUIContent("Must be identical to Encryption Key"));


            EditorGUILayout.Space();

            this.serializedObject.ApplyModifiedProperties();
        }

#endif
    }
}