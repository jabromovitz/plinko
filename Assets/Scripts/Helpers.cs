using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Helpers {

	public static IEnumerator ScaleText(Text text, float startScale, float endScale, float time)
    {
		float elapsedTime = 0;
		Vector3 ss = text.rectTransform.localScale;
     	while (elapsedTime < time)
     	{

			text.rectTransform.localScale =
			Vector3.Lerp(ss, endScale * ss, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
    	}
	}
}
