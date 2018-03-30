using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class MessageBox : RectComponent {
	public float timer = -6f;

	public static float top_marg = 40f;

	public static float btn_marg = 10f;

	public static float show_time = 2f;

	public static float wnd_alpha = 0.5f;
	public static string wnd_color = "#000000";

	public static string btn_color = "#111111";

	public static string btn_txt_font = "Arial.ttf";
	public static string msg_txt_font = "Arial.ttf";

	public static int box_width = 360;
	public static int box_height = 110;

	public static int msg_txt_width = 260;
	public static int msg_txt_height = 110;
	
	public static int btn_width = 80;
	public static int btn_height = 20;

	public delegate void callback_delegate(object obj);

	private callback_delegate callback;
	private object target_object;


	// Авейк
	void Awake(){
		base.Awake ();

		gameObject.name = "MessageBox";

		Image i = gameObject.AddComponent<Image> ();
		i.color = Funcs.hex_to_col (wnd_color, wnd_alpha);

		set_size (new RectSize(box_width, box_height));
		set_pos (new Vector2 (Screen.width / 2 - box_width / 2, 80), false);
	}

	// Апдейт
	void Update () {
		if(timer > -1){
			timer -= Time.deltaTime;
		}
		
		if(timer < 0 && timer > -6){
			Destroy(gameObject);
		}
	}

	// Инициализация месседж-бокса
	protected static GameObject init(){
		GameObject canvas = Funcs.get_cnv ();

		GameObject msg_box = new GameObject ("Message Box");
		msg_box.transform.SetParent (canvas.transform);

		msg_box.AddComponent<MessageBox> ();

		return msg_box;
	}

	// Вывод всплывающего сообщения 
	public static void hint(string text){
		if (Display.msg_box == null) {
			GameObject msg_box = MessageBox.init ();
		
			add_messge_text (msg_box.transform, text);
		
			msg_box.GetComponent<MessageBox> ().timer = show_time;

			Display.msg_box = msg_box;
		}
	}
	
	// Показ инфо
	public static void show(string text){
		if (Display.msg_box == null) {
			GameObject msg_box = MessageBox.init ();
		
			add_messge_text (msg_box.transform, text);
		
			add_btn_ok (msg_box.transform);
		
			Display.msg_box = msg_box;
		}
	}
	
	// Задать вопрос на подтверждение
	public static void show(string text, MessageBox.callback_delegate callback, object target = null){
		if (Display.msg_box == null) {
			GameObject msg_box = MessageBox.init ();
		
			add_messge_text (msg_box.transform, text);
		
			add_btn_yes (msg_box.transform);
			add_btn_no (msg_box.transform);
		
			MessageBox msg_box_comp = Display.msg_box.GetComponent<MessageBox> ();
		
			msg_box_comp.target_object = target;
			msg_box_comp.callback = callback;
		
			Display.msg_box = msg_box;
		}
	}
	
	// Закрыть диалог
	public void exit(){
		Destroy(gameObject);
		
		Display.msg_box = null;
	}	

	// Добавить текст сообщения
	public static void add_messge_text(Transform parent, string text){
		GameObject msg_txt = new GameObject ("Text");
		msg_txt.transform.SetParent (parent);
		
		RectTransform btn_rect_trans = msg_txt.AddComponent<RectTransform>();
		
		btn_rect_trans.SetWidth(msg_txt_width);
		btn_rect_trans.SetHeight(msg_txt_height);
		
		btn_rect_trans.SetLeftTopPosition(new Vector2(0, 0));

		Text txt_comp = msg_txt.AddComponent<Text>();
		txt_comp.font = Resources.GetBuiltinResource(typeof(Font), msg_txt_font) as Font;
		txt_comp.text = text;
		txt_comp.alignment = TextAnchor.MiddleCenter;
	}

	// Добавить текст на кнопку
	public static void add_btn_text(GameObject btn, string txt){
		GameObject btn_txt = new GameObject("Txt");
		btn_txt.transform.SetParent(btn.transform);
		
		RectTransform txt_rect_trans = btn_txt.AddComponent<RectTransform>();
		
		txt_rect_trans.SetWidth(btn_width);
		txt_rect_trans.SetHeight(btn_height);

		txt_rect_trans.SetLeftTopPosition(new Vector2(0, 0));
		
		Text txt_comp = btn_txt.AddComponent<Text>();
		txt_comp.font = Resources.GetBuiltinResource(typeof(Font), btn_txt_font) as Font;
		txt_comp.alignment = TextAnchor.MiddleCenter;
		
		txt_comp.text = txt;
	}

	// Добавить кнопку ДА к диалоговому окну
	public static void add_btn_ok(Transform parent){
		GameObject btn_ok = new GameObject ("Button");
		btn_ok.transform.SetParent (parent);
		
		RectTransform btn_rect_trans = btn_ok.AddComponent<RectTransform>();
		
		btn_rect_trans.SetWidth(btn_width);
		btn_rect_trans.SetHeight(btn_height);
		
		btn_rect_trans.SetLeftTopPosition(new Vector2(box_width - (btn_width + btn_marg),  (btn_height + btn_marg) - box_height));
		
		Image im = btn_ok.AddComponent<Image> ();
		im.color = Funcs.hex_to_col(btn_color, wnd_alpha);

		Button btn = btn_ok.AddComponent<Button>();
		btn.targetGraphic = im;

		btn.onClick.AddListener (parent.gameObject.GetComponent<MessageBox> ().btn_ok_click);
		
		add_btn_text (btn_ok, "Ok");
	}

	// Добавить кнопку Да
	public static void add_btn_yes(Transform parent){
		
	}

	// Добавить кнопку Нет
	public static void add_btn_no(Transform parent){
		
	}

	// Нажата кнопка ОК
	public void btn_ok_click(){
		this.exit ();
	}

	// Нажатие кнопки Да
	public void btn_yes_click(){
		this.callback (target_object);
	}

	// Нажатие кнопки Нет
	public void btn_no_click(){
		this.exit ();
	}
}
