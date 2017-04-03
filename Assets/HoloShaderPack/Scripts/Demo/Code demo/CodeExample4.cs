using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample4 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Fields
		public float TargetStrength;

		// Mono
		void Update()
		{
			if (Time.time%2 < 1)
			{
				HoloShader.EnableRim(Material, HoloShader.Rim.Inverted);
			}
			else
			{
				HoloShader.EnableRim(Material, HoloShader.Rim.Off);
			}
			Material.SetFloat("_RimStrength", CodeExampleHelper.NormalizedTime*TargetStrength);
		}
	}
}