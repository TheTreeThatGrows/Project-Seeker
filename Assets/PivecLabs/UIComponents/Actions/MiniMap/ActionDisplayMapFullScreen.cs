namespace GameCreator.UIComponents
{
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.Events;
        using UnityEngine.UI;
        using UnityEngine.Video;
        using GameCreator.Core;
        using GameCreator.Variables;
        using GameCreator.Core.Hooks;
        using GameCreator.Characters;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [AddComponentMenu("")]
    public class ActionDisplayMapFullScreen : IAction
    {
	    public GameObject mapManager;
	    public GameObject mapPanel;
        public GameObject rawImage;
	    public GameObject mapcanvas;
	    [Range(1,60)]
	    public float cameraSize = 20;
	    [Range(1,60)]
	    public float cameraDistance = 20;
 
        private PlayerCharacter player;
     
   
        private float mmwidth;
        private float mmoffsetx;
        private float mmoffsety;
        private RectTransform m_RectTransform;
	    private Image mask;
     

        RenderTexture renderTexture;
        RectTransform rt;
        RawImage img;
        private Camera targetCamera;
    
        
	    public MapManager fullscreen;

	    public bool overlay;
	    public bool lockMap;
	    public bool overlayMap;
	    public bool zoomMap;
	    public bool dragMap;
	    [Range(0,2)]
	    public int dragbutton = 0;
	    [Range(1,10)]
	    public int dragspeed = 1;

      

        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

	        GameObject go = GameObject.Find ("MiniMapCamera");
	      
	        if (go){
	        	
		        img = null;
		        targetCamera = null;
		        renderTexture = null;
		        Destroy (go.gameObject);

	        }
	        
	        player = HookPlayer.Instance.Get<PlayerCharacter>();

	        fullscreen = mapManager.GetComponent<MapManager>();
	        fullscreen.miniMapshowing = false;
	        fullscreen.fullMapshowing = true;
	        fullscreen.miniMapscrollWheel = zoomMap;
	        fullscreen.miniMapmouseDrag = dragMap;
	        fullscreen.miniMapDragButton = dragbutton;
	        fullscreen.miniMapDragSpeed = dragspeed;
	        
	        
	        if (renderTexture != null)
	        {
		        renderTexture.Release();
		        
	       
                rt = (RectTransform)rawImage.transform;
	            renderTexture = new RenderTexture((int)Screen.width, (int)Screen.height, 32);
	            renderTexture.Create();
	        }
           

            if (img == null)
            {

                img = rawImage.gameObject.GetComponent<RawImage>();
                img.texture = renderTexture;

            }
            
	        m_RectTransform = mapPanel.GetComponent<RectTransform>();
	        m_RectTransform.localScale += new Vector3(0, 0, 0);
	        mmwidth = m_RectTransform.rect.width;
        
	        mapPanel.SetActive(false);
	        
	      
	        
         if (targetCamera == null)
         {
            
	         GameObject cameraMinimap = new GameObject();
	         if (lockMap == false)
	         {
	         	cameraMinimap.transform.parent = player.transform;
	         }
	         targetCamera = cameraMinimap.AddComponent<Camera>();
	         targetCamera.enabled = true;
	         targetCamera.allowHDR = false;
	         targetCamera.targetTexture = renderTexture;
	         targetCamera.orthographic = true;
	         targetCamera.orthographicSize = cameraSize;
	         targetCamera.name = "MiniMapCamera";
	         targetCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraDistance, player.transform.position.z);
	         targetCamera.transform.LookAt(player.transform);
	         targetCamera.transform.localRotation = Quaternion.Euler(90.0f,0.0f,0.0f);


         }
	        if (overlay == true)
	        {
	        	
	      
	        if(overlayMap == true)
	        { 
	        	mapcanvas.SetActive(true);
	        	Canvas canvas = mapcanvas.gameObject.GetComponent<Canvas>();
		        canvas.renderMode = RenderMode.ScreenSpaceCamera;
		        canvas.worldCamera = targetCamera;
	        }
	        else 
	        {
		        mapcanvas.SetActive(false);

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

	    public static new string NAME = "UI/MiniMap/Display Map Full Screen";
	    private const string NODE_TITLE = "Display Map Full Screen";
        public const string CUSTOM_ICON_PATH = "Assets/PivecLabs/UIComponents/Icons/";

        // PROPERTIES: ----------------------------------------------------------------------------

	    private SerializedProperty spmapmanager;
        private SerializedProperty spmappanel;
        private SerializedProperty sprawimage;
	    private SerializedProperty spmapcanvas;
	    private SerializedProperty spcamerasize;
	    private SerializedProperty splockmap;
	    private SerializedProperty spoverlay;
	    private SerializedProperty spoverlaymap;
	    private SerializedProperty spzoommap;
	    private SerializedProperty spdragmap;
	    private SerializedProperty spdragbutton;
	    private SerializedProperty spdragspeed;
     

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
            {
                return string.Format(
                     NODE_TITLE
                 );
            }

        protected override void OnEnableEditorChild()
            {
	            this.spmapmanager = this.serializedObject.FindProperty("mapManager");
	            this.spmappanel = this.serializedObject.FindProperty("mapPanel");
            	this.sprawimage = this.serializedObject.FindProperty("rawImage");
            	this.spmapcanvas = this.serializedObject.FindProperty("mapcanvas");
	            this.spcamerasize = this.serializedObject.FindProperty("cameraSize");
	            this.splockmap = this.serializedObject.FindProperty("lockMap");
	            this.spoverlay = this.serializedObject.FindProperty("overlay");
	            this.spoverlaymap = this.serializedObject.FindProperty("overlayMap");
	            this.spzoommap = this.serializedObject.FindProperty("zoomMap");
	            this.spdragmap = this.serializedObject.FindProperty("dragMap");
	            this.spdragbutton = this.serializedObject.FindProperty("dragbutton");
	            this.spdragspeed = this.serializedObject.FindProperty("dragspeed");
            
        }


        protected override void OnDisableEditorChild()
            {
            	this.spmapmanager = null;
	            this.spmappanel = null;
            	this.sprawimage = null;
	            this.spmapcanvas = null;
	            this.spcamerasize = null;
	            this.splockmap = null;
	            this.spoverlay = null;
	            this.spoverlaymap = null;
	            this.spzoommap = null;
	            this.spdragmap = null;
	            this.spdragbutton = null;
	            this.spdragspeed = null;
         

        }

        public override void OnInspectorGUI()
            {
                this.serializedObject.Update();
	            EditorGUILayout.PropertyField(this.spmapmanager, new GUIContent("Map Manager"));
	            EditorGUILayout.PropertyField(this.spmappanel, new GUIContent("Panel - MiniMap"));
        		EditorGUILayout.PropertyField(this.sprawimage, new GUIContent("RawImage texturemap"));
	            EditorGUILayout.PropertyField(this.spoverlay, new GUIContent("Use Overlay Canvas"));
	            if (overlay)
	            {
		            EditorGUILayout.PropertyField(this.spmapcanvas, new GUIContent("Overlay Canvas"));
	            }
	            EditorGUILayout.Space();
	            EditorGUILayout.Space();
	            EditorGUILayout.LabelField("Properties");
	            EditorGUI.indentLevel++;
	            EditorGUILayout.PropertyField(this.spcamerasize, new GUIContent("Field of View"));
	            EditorGUILayout.PropertyField(this.splockmap, new GUIContent("Freeze Map"));
	            if (overlay)
	            {
	            	EditorGUILayout.PropertyField(this.spoverlaymap, new GUIContent("Show Overlay"));
	            }
	            EditorGUILayout.PropertyField(this.spzoommap, new GUIContent("Zoom Map on Scrollwheel"));
	            EditorGUILayout.PropertyField(this.spdragmap, new GUIContent("Drag Map with Mouse"));
	            if (dragMap)
	            {
		            EditorGUI.indentLevel++;
		            EditorGUILayout.PropertyField(this.spdragspeed, new GUIContent("Drag Speed"));
		            EditorGUILayout.PropertyField(this.spdragbutton, new GUIContent("Drag Button"));
		            Rect position = EditorGUILayout.GetControlRect(false, 2 * EditorGUIUtility.singleLineHeight); 
		            position.height *= 0.5f;
           
		            position.y += position.height - 20;
		            position.x += EditorGUIUtility.labelWidth -30;
		            position.width -= EditorGUIUtility.labelWidth + 26; 
		            GUIStyle style = GUI.skin.label;
		            style.fontSize = 10;
		            style.alignment = TextAnchor.UpperLeft; EditorGUI.LabelField(position, "Left", style);
		            style.alignment = TextAnchor.UpperCenter; EditorGUI.LabelField(position, "Right", style);
		            style.alignment = TextAnchor.UpperRight; EditorGUI.LabelField(position, "Middle", style);
		            EditorGUI.indentLevel--;
 
	            }

            this.serializedObject.ApplyModifiedProperties();
            }
	 
#endif

        }
    }
