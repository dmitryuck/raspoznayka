using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class CursorBrush : MonoBehaviour {

	RectTransform rect_trans;
	Image i;

	// Авейк курсора
	void Awake () {
		CanvasGroup canv_grp = gameObject.AddComponent<CanvasGroup> ();
		canv_grp.blocksRaycasts = false;

		rect_trans = gameObject.GetComponent<RectTransform>();
		rect_trans.SetLeftTopPivot();

		i = gameObject.AddComponent<Image>();
		i.sprite = Scene.get_sprite("brush");

		Cursor.visible = false;
	}
}
