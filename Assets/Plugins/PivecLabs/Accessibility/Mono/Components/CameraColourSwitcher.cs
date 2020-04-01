using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;
using GameCreator.Core;
using GameCreator.Core.Hooks;


namespace GameCreator.Accessibility
{
    [RequireComponent(typeof(HookCamera))]
    [AddComponentMenu("Game Creator/Accessibility/Camera Component Switcher", 100)]

  
    public class CameraColourSwitcher : MonoBehaviour
    {

        // PROPERTIES: ----------------------------------------------------------------------------

        [HideInInspector] public bool swapping = false;

        private static Shader switchShader;
        private static Material switchMaterial;


        [System.Serializable]
        public class SwitchColor
        {
            [HideInInspector]
            public bool enabled = true;

            [HideInInspector]
            public Color oldColor = Color.white;

            [HideInInspector]
            public Color newColor = Color.white;

            [HideInInspector]
            [Range(0, 1)]

            public float tolerance = 0.05f;

            [HideInInspector]
            [Range(0, .5f)]
            public float feathering = 0.04f;

        }

        [HideInInspector]
        public SwitchColor[] switchColors = new SwitchColor[4];

      
        // METHODS: ----------------------------------------------------------------------------

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
           
            if (swapping == true)
            {
                Change(src, dest);
            }
           else
            {
                Graphics.Blit(src, dest);
            }
        }

   
 
        protected void Blit(Texture src, Texture2D dest, Material material) {

		var renderTexture = RenderTexture.GetTemporary(dest.width, dest.height, 0);
		var oldrenderTexture = RenderTexture.active;
		
            RenderTexture.active = renderTexture;
      		Graphics.Blit(src, material, -1);
		    dest.ReadPixels(new Rect(0, 0, dest.width, dest.height), 0, 0, true);
		    RenderTexture.active = oldrenderTexture;
		    RenderTexture.ReleaseTemporary(renderTexture);

		
		dest.Apply(true);
		
        }


        public RenderTexture Change(Texture src, RenderTexture dest) {
		
   		var interations = 4;

		if (interations == 1) {
			doPass(mat => {
				if (dest) dest.DiscardContents();
				Graphics.Blit(src, dest, mat);
			});

		} 

        else 
        {
			
			var temprenderTexture = RenderTexture.GetTemporary(src.width, src.height, 0);
			var temprenderTexture2 = interations > 2 ? RenderTexture.GetTemporary(src.width, src.height, 0) : null;
			var current = src;
			var next = temprenderTexture;
   			var i = 0;
			doPass(mat => {
				if (i == 0) {
					next.DiscardContents();
					Graphics.Blit(current, next, mat);
					current = next;
					next = temprenderTexture2;
				} 
                else if (i == interations - 1)
                {
					if (dest) dest.DiscardContents();
					Graphics.Blit(current, dest, mat);
				} 
                else 
                {
					next.DiscardContents();
					Graphics.Blit(current, next, mat);
					var _ = current;
					current = next;
					next = (RenderTexture)_;
				}
				++i;
			});

			RenderTexture.ReleaseTemporary(temprenderTexture);
			if (temprenderTexture2) RenderTexture.ReleaseTemporary(temprenderTexture2);
		}

		return dest;
	}

       


        public void doPass(Action<Material> doPassing) {

        switchShader = Resources.Load<Shader>("Shaders/ColourSwitch");
        switchMaterial = new Material(switchShader);
       

		
		for (int i = 0; i < 4; ++i)
         {
			var mat = switchMaterial;

			var oldColorHSV = new Vector3();
			Color.RGBToHSV(switchColors[i].oldColor, out oldColorHSV.x, out oldColorHSV.y, out oldColorHSV.z);
			var newColorHSV = new Vector3();
			Color.RGBToHSV(switchColors[i].newColor, out newColorHSV.x, out newColorHSV.y, out newColorHSV.z);

			mat.SetVector("_SourceColor", switchColors[i].oldColor);
			mat.SetVector("_SourceColorHSV", oldColorHSV);
			mat.SetVector("_DestColorHSV", newColorHSV);
			mat.SetFloat("_Tolerance", switchColors[i].tolerance);
			mat.SetFloat("_TransitionSoftness", switchColors[i].feathering);

			doPassing(mat);

			
		}

	      
		}
        	
	}


}

