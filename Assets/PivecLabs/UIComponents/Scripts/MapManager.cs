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
	using System.Linq;

public class MapManager : MonoBehaviour
	{
    
		public bool miniMapshowing;
		public bool fullMapshowing;
		public bool miniMapscrollWheel;
		public bool miniMapmouseDrag;
		public int miniMapDragSpeed = 1;
		public int miniMapDragButton = 0;
		
		void Update()
		{
       
			if ((fullMapshowing == true) || (miniMapshowing == true)) 
			{
				
				
				GameObject go = GameObject.Find ("MiniMapCamera");
				var gameObj =  Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "MapMarkerLabel");
	    
				if (gameObj != null)
				{
					foreach (var label in gameObj)
					{
						label.GetComponent<Transform>().eulerAngles = new Vector3(label.transform.eulerAngles.x,go.transform.eulerAngles.y,label.transform.eulerAngles.z);
		   
					}
	    	
				}
				
			}	    
			
			if ((fullMapshowing == true) && (miniMapscrollWheel == true))
				{
					GameObject go = GameObject.Find ("MiniMapCamera");
		
					go.GetComponent<Camera>().orthographicSize += Input.GetAxis("Mouse ScrollWheel");
				}
				
			if ((fullMapshowing == true) && (miniMapmouseDrag == true))
			{
				GameObject go = GameObject.Find ("MiniMapCamera");
		
			 if (Input.GetMouseButton(miniMapDragButton))
			 {
				 
				 go.GetComponent<Camera>().transform.position -= new Vector3(Input.GetAxis("Mouse X") * miniMapDragSpeed, 0, Input.GetAxis("Mouse Y") * miniMapDragSpeed);
			 }
			}
			
			}
		
	
		}
		
}	