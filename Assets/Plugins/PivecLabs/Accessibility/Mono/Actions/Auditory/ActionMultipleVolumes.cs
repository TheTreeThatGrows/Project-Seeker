namespace GameCreator.Accessibility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
    using GameCreator.Core;
    using GameCreator.Characters;
    using GameCreator.Core.Hooks;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionMultipleVolumes : IAction 
	{

        public bool musicVolumeVar = false;
        [Range(0.0f, 1.0f)] public float _musicVolume = 1.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty musicVolume = new VariableProperty(Variable.VarType.GlobalVariable);

      
        public bool soundVolumeVar = false;
        [Range(0.0f, 1.0f)] public float _soundVolume = 1.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty soundVolume = new VariableProperty(Variable.VarType.GlobalVariable);

     
        public bool voiceVolumeVar = false;
        [Range(0.0f, 1.0f)] public float _voiceVolume = 1.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty voiceVolume = new VariableProperty(Variable.VarType.GlobalVariable);

        public bool audio1VolumeVar = false;
        [Range(0.0f, 1.0f)] public float _audio1Volume = 1.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty audio1Volume = new VariableProperty(Variable.VarType.GlobalVariable);

        public bool audio2VolumeVar = false;
        [Range(0.0f, 1.0f)] public float _audio2Volume = 1.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty audio2Volume = new VariableProperty(Variable.VarType.GlobalVariable);

        public AudioSource audioSource;
        private AudioSource[] soundsArrayTag1;
        private AudioSource[] soundsArrayTag2;



        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            if (musicVolumeVar == true)
            {
                float value = (float)this.musicVolume.Get(target);
                AudioManager.Instance.SetGlobalMusicVolume(value);
            }
            else
            {
                AudioManager.Instance.SetGlobalMusicVolume(_musicVolume);
            }


            if (soundVolumeVar == true)
            {
                float value = (float)this.soundVolume.Get(target);
                AudioManager.Instance.SetGlobalSoundVolume(value);
            }
            else
            {
                AudioManager.Instance.SetGlobalSoundVolume(_soundVolume);
            }

            if (voiceVolumeVar == true)
            {
                float value = (float)this.voiceVolume.Get(target);
                AudioManager.Instance.SetGlobalVoiceVolume(value);
            }
            else
            {
                AudioManager.Instance.SetGlobalVoiceVolume(_voiceVolume);
            }

           
            var temp1 = GameObject.FindGameObjectsWithTag("SoundTag1");
            if (temp1 != null)
            {
                soundsArrayTag1 = new AudioSource[temp1.Length];
                for (int i = 0; i < soundsArrayTag1.Length; i++)
                {
                    soundsArrayTag1[i] = temp1[i].GetComponentInChildren<AudioSource>();
                }

            }

        
            if (audio1VolumeVar == true)
            {
                float value = (float)this.audio1Volume.Get(target);
                for (int i = 0; i < this.soundsArrayTag1.Length; ++i)
                {
                    soundsArrayTag1[i].volume = value;
                }
            }
            else
            {
                for (int i = 0; i < this.soundsArrayTag1.Length; ++i)
                {
                    soundsArrayTag1[i].volume = _audio1Volume;
                }
                
            }


            var temp2 = GameObject.FindGameObjectsWithTag("SoundTag2");
            if (temp2 != null)
            {
                soundsArrayTag2 = new AudioSource[temp2.Length];
                for (int i = 0; i < soundsArrayTag2.Length; i++)
                {
                    soundsArrayTag2[i] = temp2[i].GetComponentInChildren<AudioSource>();
                }

            }

            if (audio2VolumeVar == true)
            {
                float value = (float)this.audio2Volume.Get(target);
                for (int i = 0; i < this.soundsArrayTag2.Length; ++i)
                {
                    soundsArrayTag2[i].volume = value;
                }
            }
            else
            {
                for (int i = 0; i < this.soundsArrayTag2.Length; ++i)
                {
                    soundsArrayTag2[i].volume = _audio2Volume;
                }

            }
            return true;
        }




        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Auditory/Multiple Volume Controls";
        private const string NODE_TITLE = "Multiple Volume Controls";


        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spMusicToggle;
        private SerializedProperty spSoundToggle;
        private SerializedProperty spVoiceToggle;
        private SerializedProperty spAudio1Toggle;
        private SerializedProperty spAudio2Toggle;

        private SerializedProperty spMusicSlider;
        private SerializedProperty spSoundSlider;
        private SerializedProperty spVoiceSlider;
        private SerializedProperty spAudio1Slider;
        private SerializedProperty spAudio2Slider;

        private SerializedProperty spMusic;
        private SerializedProperty spSound;
        private SerializedProperty spVoice;
        private SerializedProperty spAudio1;
        private SerializedProperty spAudio2;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
            return string.Format(NODE_TITLE);
        }

		protected override void OnEnableEditorChild ()
		{
            this.spMusicToggle = this.serializedObject.FindProperty("musicVolumeVar");
            this.spSoundToggle = this.serializedObject.FindProperty("soundVolumeVar");
            this.spVoiceToggle = this.serializedObject.FindProperty("voiceVolumeVar");
            this.spAudio1Toggle = this.serializedObject.FindProperty("audio1VolumeVar");
            this.spAudio2Toggle = this.serializedObject.FindProperty("audio2VolumeVar");

            this.spMusicSlider = this.serializedObject.FindProperty("_musicVolume");
            this.spSoundSlider = this.serializedObject.FindProperty("_soundVolume");
            this.spVoiceSlider = this.serializedObject.FindProperty("_voiceVolume");
            this.spAudio1Slider = this.serializedObject.FindProperty("_audio1Volume");
            this.spAudio2Slider = this.serializedObject.FindProperty("_audio2Volume");

            this.spMusic = this.serializedObject.FindProperty("musicVolume");
            this.spSound = this.serializedObject.FindProperty("soundVolume");
            this.spVoice = this.serializedObject.FindProperty("voiceVolume");
            this.spAudio1 = this.serializedObject.FindProperty("audio1Volume");
            this.spAudio2 = this.serializedObject.FindProperty("audio2Volume");
        }

        protected override void OnDisableEditorChild ()
		{
            this.spMusicToggle = null;
            this.spSoundToggle = null;
            this.spVoiceToggle = null;
            this.spAudio1Toggle = null;
            this.spAudio2Toggle = null;

            this.spMusicSlider = null;
            this.spSoundSlider = null;
            this.spVoiceSlider = null;
            this.spAudio1Slider = null;
            this.spAudio2Slider = null;

            this.spMusic = null;
            this.spSound = null;
            this.spVoice = null;
            this.spAudio1 = null;
            this.spAudio2 = null;
        }

        public override void OnInspectorGUI()
		{
            this.serializedObject.Update();
            
            EditorGUILayout.LabelField("Game Creator Audio", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Music Volume", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spMusicToggle, new GUIContent("Value from Variable"));
            if (musicVolumeVar == true)
            {
                EditorGUILayout.PropertyField(this.spMusic, new GUIContent("Volume Value"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spMusicSlider, new GUIContent("From 0 to 1"));
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Sound Volume", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spSoundToggle, new GUIContent("Value from Variable"));
            if (soundVolumeVar == true)
            {
                EditorGUILayout.PropertyField(this.spSound, new GUIContent("Volume Value"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spSoundSlider, new GUIContent("From 0 to 1"));
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Voice Volume", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spVoiceToggle, new GUIContent("Value from Variable"));
            if (voiceVolumeVar == true)
            {
                EditorGUILayout.PropertyField(this.spVoice, new GUIContent("Volume Value"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spVoiceSlider, new GUIContent("From 0 to 1"));
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorGUILayout.LabelField("Audio Source Objects", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Audio Volume (SoundTag1)", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spAudio1Toggle, new GUIContent("Value from Variable"));
            if (audio1VolumeVar == true)
            {
                EditorGUILayout.PropertyField(this.spAudio1, new GUIContent("Volume Value"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spAudio1Slider, new GUIContent("From 0 to 1"));
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Audio Volume (SoundTag2)", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spAudio2Toggle, new GUIContent("Value from Variable"));
            if (audio2VolumeVar == true)
            {
                EditorGUILayout.PropertyField(this.spAudio2, new GUIContent("Volume Value"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spAudio2Slider, new GUIContent("From 0 to 1"));
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();


            this.serializedObject.ApplyModifiedProperties();
        }

		#endif
	}
}