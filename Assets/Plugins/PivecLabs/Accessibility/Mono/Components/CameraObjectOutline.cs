
namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameCreator.Core;
    using GameCreator.Core.Hooks;

    [RequireComponent(typeof(HookCamera))]
    [AddComponentMenu("Game Creator/Accessibility/Camera Component Outline", 100)]

    public class CameraObjectOutline : MonoBehaviour
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        private Material materialOutline = null;
   
        private Shader ShaderOutline;
        [HideInInspector] public float outlineOnly = 0.0f;
        [HideInInspector] public float outlineWidth = 0.0f;
        [HideInInspector] public Color outlineColour = Color.black;

        [HideInInspector] public bool outlining = false;


        // INITIALIZERS: ----------------------------------------------------------------------------
  

        private Material material1
        {
            get
            {
                materialOutline = CreateMaterial(ShaderOutline, materialOutline);
                return materialOutline;
            }
        }
     
       
        protected Material CreateMaterial(Shader shader, Material material)
        {
            if (shader == null)
            {
                return null;
            }

            if (shader.isSupported && material && material.shader == shader)
                return material;

            if (!shader.isSupported)
            {
                return null;
            }
            else
            {
                material = new Material(shader);
                material.hideFlags = HideFlags.DontSave;
                if (material)
                    return material;
                else
                    return null;
            }
        }

        // METHODS: ----------------------------------------------------------------------------
   
        void OnRenderImage (RenderTexture src, RenderTexture dest) 
		{
            ShaderOutline = Shader.Find("Hidden/ColourOutline");
            if (material1 != null && outlining == true)
			{
                material1.SetColor("_OutlineColor", outlineColour);
                material1.SetFloat("_OutlineOnly", outlineOnly);
				material1.SetFloat("_OutlineWidth", outlineWidth);
	
		    	Graphics.Blit(src, dest, material1);
            
			}
         
        
            else 
			{
				Graphics.Blit(src, dest);
              
            }
		}

    }
}
