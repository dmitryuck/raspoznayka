using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

public class RectButton : RectComponent, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

	public Image img;
	public Button btn;
	public Text txt;

	public string btn_bg_color = "#111111";
	public float btn_bg_alpha = 1f;

	public string text_font = "Arial.ttf";

	public delegate void on_mouse_down_deleg();
	public on_mouse_down_deleg on_mouse_down;

	public delegate void on_mouse_enter_deleg();
	public on_mouse_enter_deleg on_mouse_enter;

	public delegate void on_mouse_exit_deleg();
	public on_mouse_enter_deleg on_mouse_exit;

	// Авейк
	public void Awake(){
		base.Awake ();
		
		add_button ();
	}

	// Установить текст кнопки
	public void set_text(string text){
		add_image (Funcs.hex_to_col (btn_bg_color, btn_bg_alpha));

		add_text ();

		txt.text = text;
	}

	// Установить иконку для кнопки
	public void set_icon(Sprite icon){
		if(gameObject.GetComponent<Image> () == false)
		add_image (new Color(255, 255, 255));

		img.sprite = icon;
	}

	// Нажат
	public void OnPointerDown(PointerEventData data){
		if(on_mouse_down != null)
			on_mouse_down();
	}

	// Наведен
	public void OnPointerEnter(PointerEventData data){
		if(on_mouse_enter != null)
			on_mouse_enter();
	}

	// Отведен
	public void OnPointerExit(PointerEventData data){
		if(on_mouse_exit != null)
			on_mouse_exit();
	}

	// Добавить кнопку
	public void add_button(){
		btn = gameObject.AddComponent<Button> ();
		btn.targetGraphic = img;
	}

	// Добавить элемент картинка
	public void add_image(Color col){
		img = gameObject.AddComponent<Image> ();

		img.color = new Color(col.r, col.g, col.b, btn_bg_alpha);
	}

	// Добавить элемент текст
	public void add_text(){
		GameObject btn_text = new GameObject ("Text");
		btn_text.transform.SetParent (gameObject.transform);

		RectTransform rect_trans = btn_text.AddComponent<RectTransform>();

		rect_trans.SetWidth(get_size().width);
		rect_trans.SetHeight(get_size().height);
		
		rect_trans.SetLeftTopPosition(new Vector2(0, 0));
		
		txt = btn_text.AddComponent<Text> ();
		txt.font = Resources.GetBuiltinResource(typeof(Font), text_font) as Font;
		//txt.fontStyle = FontStyle.Bold;
		txt.alignment = TextAnchor.MiddleCenter;
	}
}
