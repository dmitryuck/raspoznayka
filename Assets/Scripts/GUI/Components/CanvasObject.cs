using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

public class CanvasObject : RectComponent, IPointerClickHandler {

	// Авейк
	void Awake(){
		base.Awake ();

		Image img = gameObject.AddComponent<Image> ();
		img.sprite = Scene.get_sprite("empty_texture");

		set_size (new RectSize(Screen.width, Screen.height));
		set_pos (new Vector2 (0, 0), false);
	}

	// Обработка нажатия на канву
	public void OnPointerClick (PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Left) {

		}
				
		if (eventData.button == PointerEventData.InputButton.Right) {

		}

	}
}
