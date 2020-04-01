namespace GameCreator.Accessibility
{
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.EventSystems;
	using System.Collections;

	using GameCreator.Characters;
	using GameCreator.Core.Hooks;
	using GameCreator.Variables;

	[AddComponentMenu("")]
	[System.Serializable]
	public class DynamicTouchStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
	{
		public static Rect STICK_RECT_LEFT = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
		public static Rect STICK_RECT_RIGHT = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
		public bool right = false;
		Rect screenRect;
	
		public void OnPointerDown(PointerEventData eventData)
		{
			var ts = GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<GameCreator.Core.TouchStick>();
			
			RectTransform ds = GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<DynamicTouchStick>().GetComponent<RectTransform>();
			
		
		 if (right == true)
			{
				screenRect = new Rect(
					Screen.width * STICK_RECT_RIGHT.x,
					Screen.height * STICK_RECT_RIGHT.y,
					Screen.width * STICK_RECT_RIGHT.width,
					Screen.height * STICK_RECT_RIGHT.height
				);
				
				ds.anchoredPosition =  new Vector2 ((Screen.width -( ds.rect.width/2)),  ds.rect.y);
	
				
			}
			else 
			{
				screenRect = new Rect(
					Screen.width * STICK_RECT_LEFT.x,
					Screen.height * STICK_RECT_LEFT.y,
					Screen.width * STICK_RECT_LEFT.width,
					Screen.height * STICK_RECT_LEFT.height
				);
				
				ds.anchoredPosition =  new Vector2 ((ds.rect.width/2),  ds.rect.y);
	

			}
			
			RectTransform rt = ts.GetComponent<RectTransform>();
		
		
			if (Input.touchCount == 0) {
				
				if(screenRect.Contains(Input.mousePosition))
				{
			
					ts.jsContainer.rectTransform.position = new Vector3(Input.mousePosition.x - (rt.rect.width / 2), Input.mousePosition.y - (rt.rect.height / 2), transform.position.z);
				
						ts.OnPointerDown(eventData);
			
				}
			} 
			else {
			 for (int i = 0; i<Input.touchCount; i++)
			 {			
					if(screenRect.Contains(Input.GetTouch(i).position))
			
					{
			
						ts.jsContainer.rectTransform.position = new Vector3(Input.GetTouch(i).position.x - (rt.rect.width / 2), Input.GetTouch(i).position.y - (rt.rect.height / 2), transform.position.z);
				
								ts.OnPointerDown(eventData);
			
					}
				}
			}
		

		}

		public void OnDrag(PointerEventData eventData)
		{
			var ts = GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<GameCreator.Core.TouchStick>();
			RectTransform ds = GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<DynamicTouchStick>().GetComponent<RectTransform>();
	
	if (right == true)
			{
				screenRect = new Rect(
					Screen.width * STICK_RECT_RIGHT.x,
					Screen.height * STICK_RECT_RIGHT.y,
					Screen.width * STICK_RECT_RIGHT.width,
					Screen.height * STICK_RECT_RIGHT.height
				);
				
				ds.anchoredPosition =  new Vector2 ((Screen.width -( ds.rect.width/2)),  ds.rect.y);
	
			}
			else 
			{
				screenRect = new Rect(
					Screen.width * STICK_RECT_LEFT.x,
					Screen.height * STICK_RECT_LEFT.y,
					Screen.width * STICK_RECT_LEFT.width,
					Screen.height * STICK_RECT_LEFT.height
				); 	
				
				ds.anchoredPosition =  new Vector2 ((ds.rect.width/2), ds.rect.y);
	
			}
			
			
			if (Input.touchCount == 0) {
		
				if(screenRect.Contains(Input.mousePosition))
				{
						ts.OnPointerDown(eventData);
				}
				
			}
			else 
			{
			
				 for (int i = 0; i<Input.touchCount; i++)
				 {			
					if((Input.GetTouch(i).phase == TouchPhase.Moved) && (screenRect.Contains(Input.GetTouch(i).position)))
					{
							ts.OnPointerDown(eventData);
					}
				}
			} 
		}

		public void OnPointerUp(PointerEventData eventData)
		{
				var ts = GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<GameCreator.Core.TouchStick>();

				ts.OnPointerUp(eventData);
			
		}
	}

}
