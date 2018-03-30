using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class RectImage : RectComponent {

	public Image image;

	// Авейк имейджа
	public void Awake () {
		base.Awake();

		image = gameObject.AddComponent<Image>();
	}

	// Установить спрайт
	public void set_sprite(string name){
		image.sprite = Scene.get_sprite(name);
	}

	// Тип имейджа
	public void set_type(Image.Type type){
		image.type = type;
	}

	// Метод заполнения
	public void set_fill_meth(Image.FillMethod meth){
		image.fillMethod = meth;
	}

	// Количество заполнения
	public void set_fill_amon(float val){
		image.fillAmount = val;
	}
}
