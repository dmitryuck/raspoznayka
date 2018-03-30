using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class RectText : RectComponent {

	public Text text;

	// Авейк
	void Awake () {
		base.Awake();

		text = gameObject.AddComponent<Text> ();
	}

	// Шрифт текста
	public void set_font(string name = "Arial.ttf"){
		text.font = Resources.GetBuiltinResource(typeof(Font), name) as Font;
	}

	// Размер шрифта
	public void set_font_size(int val){
		text.fontSize = val;
	}

	// Установить цвет фона
	public void set_color(Color col){
		text.color = col;
	}

	// Стиль текста
	public void set_style(FontStyle style){
		text.fontStyle = style;
	}

	// Установить текст
	public void set_text(string val){
		text.text = val;
	}
}
