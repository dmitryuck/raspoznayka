using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;

public class Scene : MonoBehaviour {

	private static Sprite[] sprites;
	public static string[] sprite_array;

	public ParticleSystem particle;
	public Camera camera;

	public float mouse_sens = 0.2f;
	public float brush_sens = 0.0001f;
	
	private List<Vector3> points;

	public static AudioSource music;
	
	public static bool is_music_play;

	public static Figure figure;

	private float timer_move;

	private bool has_time;
	private bool is_proc;

	public int scores_num;

	public float timer_const = 120f;

	public float timer_play;
	public float timer_step = 1f;

	private bool is_move;

	private bool _is_play;
	public bool is_play{
		get{
			return _is_play;
		}

		set{
			_is_play = value;

			if(value == true){
				game_start();
			}
		}
	}

	// Старт
	void Start(){
		load_sprites ();
		
		add_canvas ();
		add_event_system ();

		figure = new Figure();

		Display.init();

		Cursor.SetCursor(Resources.Load("Sprites/brush") as Texture2D, Vector2.zero, CursorMode.ForceSoftware);

		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		
		points = new List<Vector3>();
		
		load_brush();
		set_bg_size();

		start_menu();
	}

	// Игровое меню
	public void start_menu(){
		Display.menu_wnd_open();

		set_plane_bg("Menu_bg");

		is_play = false;
	}

	// Старт игры
	public void game_start(){
		reset();

		timer_play = timer_const;
		scores_num = 0;

		set_plane_bg("Game_bg");

		Display.game_wnd_open();

		toggle_music(gameObject, "game_music");

		next_figure();
	}

	// Стоп игра
	public void game_stop(){
		Display.game_wnd_close();
		Display.stats_wnd_open();

		stop_music();
	}

	// Генерация фигуры
	public void next_figure(){
		figure = new Figure();
		
		GameWnd game_wnd_comp = Display.game_wnd.GetComponent<GameWnd>();
		game_wnd_comp.set_fig_icon(figure.fig_icon);

		StartCoroutine(timer_start());
	}

	// Вкл/Выкл музыку
	public static void toggle_music(GameObject target, string name){
		if(is_music_play == true){
			is_music_play = false;
			
			music.Stop();
			
			Destroy(music);
		} else {
			is_music_play = true;
			
			music = target.AddComponent<AudioSource>();
			music.volume = 0.3f;
			
			AudioClip clip = Resources.Load(Config.snd_load_path + name) as AudioClip;
			music.PlayOneShot(clip);
		}
	}
	
	// Выкл музыку
	public static void stop_music(){
		if(is_music_play == true){
			is_music_play = false;
			
			music.Stop();			
		
			Destroy(music);
		} 
	}

	// Расчет коефициента размерности БГ
	public void set_bg_size(){
		//gameObject.transform.localScale += new Vector3(Screen.width * 13.705f / 797f, 1, Screen.height * 10.2507f / 598f);
	}

	// Установка материала для фона
	public void set_plane_bg(string bg){
		Renderer rend = gameObject.GetComponent<Renderer>();
		rend.material = Resources.Load(Config.mat_load_path + bg) as Material;
	}

	// Загрузка партиклов для кисти
	public void load_brush(BrushCols col = BrushCols.Default){
		GameObject part = Instantiate(Resources.Load(Config.pref_inst_path + "Particle" + col.ToString()) as GameObject);
		
		particle = part.GetComponent<ParticleSystem>();
	}

	// Сброс параметров
	public void reset(){
		particle.emissionRate = 0;
		points.Clear();
		is_move = true;
		timer_move = 0;
	}

	// Загрузить все спрайты с каталога в массив
	public static void load_sprites(){
		sprites = Resources.LoadAll<Sprite>(Config.ui_sprite_path);
		sprite_array = new string[sprites.Length];
		
		for(int i = 0; i < sprite_array.Length; i++) {
			sprite_array[i] = sprites[i].name;
		}
	}

	// Получить спрайт с массива по имени
	public static Sprite get_sprite(string name){
		Sprite sprites_arr = null;

		try{
			sprites_arr = Scene.sprites[Array.IndexOf(sprite_array, name)];
		} catch (IndexOutOfRangeException ex){
			Funcs.throw_error ("Sprite not Found " + name);
		}

		return sprites_arr;
	}

	// Добавить канву на сцену
	private static void add_canvas(){
		if (GameObject.Find ("Canvas") == null) {
			GameObject canvas = new GameObject ("Canvas");
			
			Canvas canv_comp = canvas.AddComponent<Canvas> ();
			canv_comp.renderMode = RenderMode.ScreenSpaceOverlay;
			canv_comp.pixelPerfect = false;
			canv_comp.sortingOrder = 0;
			
			CanvasScaler canv_scaler = canvas.AddComponent<CanvasScaler> ();
			canv_scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
			canv_scaler.scaleFactor = 1;
			canv_scaler.referencePixelsPerUnit = 100;
			
			GraphicRaycaster graph_raycast = canvas.AddComponent<GraphicRaycaster> ();
			graph_raycast.ignoreReversedGraphics = true;
			graph_raycast.blockingObjects = GraphicRaycaster.BlockingObjects.None;
			
			GameObject canvas_obj = new GameObject ("CanvasObject");
			canvas_obj.transform.SetParent (canvas.transform);
			canvas_obj.AddComponent<CanvasObject> ();
		}
	}
	
	// Добавить систему событий
	private static void add_event_system(){
		if (GameObject.Find ("EventSystem") == null) {
			GameObject evnt_sys = new GameObject ("EventSystem");
			
			EventSystem event_system = evnt_sys.AddComponent<EventSystem> ();
			event_system.firstSelectedGameObject = null;
			event_system.sendNavigationEvents = true;
			event_system.pixelDragThreshold = 5;
			
			StandaloneInputModule stand_input_module = evnt_sys.AddComponent<StandaloneInputModule> ();
			stand_input_module.horizontalAxis = "Horizontal";
			stand_input_module.verticalAxis = "Vertical";
			stand_input_module.submitButton = "Submit";
			stand_input_module.cancelButton = "Cancel";
			stand_input_module.inputActionsPerSecond = 10;
			stand_input_module.forceModuleActive = false;
			
			TouchInputModule touch_input_module = evnt_sys.AddComponent<TouchInputModule> ();
			touch_input_module.forceModuleActive = false;
		}
	}

	//Определение угла между двумя точками, относительно центральной
	public float calc_angle(Vector3 posA, Vector3 posB, Vector3 centr){
		Vector3 ab = (posA - centr).normalized;
		Vector3 ac = (centr - posB).normalized;
		
		float angle = Vector3.Angle(ab, ac);
		
		return angle > 90 ? 180 - angle : angle;
	}

	// Проверка заполнения квадранта
	public bool is_right(Figure fig, Vector3 position){
		Vector2 pos = new Vector2(position.x, position.y);

		bool result = false;

		bool q1 = false;
		bool q2 = false;
		bool q3 = false;
		bool q4 = false;

		int first_count = 0;
		int second_count = 0;

		if(fig.div_plane == "vert"){
			foreach(Vector3 point_pos in points){
				if(point_pos.x < pos.x){
					first_count += 1;
				} else {
					second_count += 1;
				}
			}

			if(fig.quadrant[0] == 0 && fig.quadrant[1] == 1){
				if(first_count < second_count){
					result = true;
				}
			}

			if(fig.quadrant[0] == 1 && fig.quadrant[1] == 0){
				if(first_count > second_count){
					result = true;
				}
			}
		}

		if(fig.div_plane == "hor"){
			foreach(Vector3 point_pos in points){
				if(point_pos.y > pos.y){
					first_count += 1;
				} else {
					second_count += 1;
				}
			}

			if(fig.quadrant[0] == 0 && fig.quadrant[1] == 1){
				if(first_count < second_count){
					result = true;
				}
			}

			if(fig.quadrant[0] == 1 && fig.quadrant[1] == 0){
				if(first_count > second_count){
					result = true;
				}
			}
		}

		if(fig.div_plane == "quad"){
			foreach(Vector3 point_pos in points){
				if(point_pos.x < pos.x && point_pos.y > pos.y){
					q1 = true;
				}

				if(point_pos.x > pos.x && point_pos.y > pos.y){
					q2 = true;
				}

				if(point_pos.x > pos.x && point_pos.y < pos.y){
					q3 = true;
				}

				if(point_pos.x < pos.x && point_pos.y < pos.y){
					q4 = true;
				}
			}

			if(q1 == q2 == q3 == q4 == true)
				result = true;
		}

		return result;
	}

	// Прогресс
	public IEnumerator timer_start(){
		is_proc = true;
		has_time = true;

		bool compl = false;

		float prog_val = timer_play;
		
		float rate = 1 / timer_play;
		
		float i = 0;

		GameWnd game_wnd_comp = Display.game_wnd.GetComponent<GameWnd>();
		game_wnd_comp.set_timer_time("Timer: " + prog_val.ToString());
		
		while (i < 1){
			if(is_proc == true){
				if(Math.Round(prog_val, 1) <= 0){
					game_stop();
					
					is_play = false;
					
					StartCoroutine(Funcs.play_sound(gameObject, "level_end"));

					yield break;
				}

				i += Time.deltaTime * rate;
				
				prog_val -= Time.deltaTime;
				
				game_wnd_comp.set_timer_time("Timer: " + Math.Round(prog_val, 1).ToString());

				yield return 0;
			} else {
				compl = true;

				break;
			}
		}

		has_time = false;

		if(timer_play - timer_step >= 0){
			if (compl == false) {
				StartCoroutine(Funcs.play_sound(gameObject, "fig_error"));
			}

			reset();

			timer_play -= timer_step;

			next_figure();
		} else {
			game_stop();

			is_play = false;

			StartCoroutine(Funcs.play_sound(gameObject, "level_end"));
		}
	}

	// Апдейт
	void Update () {
		if (is_play == false) return;

		Vector3 mouse_pos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(camera.transform.position.z)));
		
		if(Input.GetMouseButton(0)){			
			if(Mathf.Abs(Input.GetAxis("Mouse X")) > mouse_sens || Mathf.Abs(Input.GetAxis("Mouse Y")) > mouse_sens){
				is_move = true;
				timer_move = 0;

				particle.transform.position = mouse_pos;
				particle.emissionRate = 100;
			} else {
				timer_move += Time.deltaTime;

				if(timer_move > brush_sens){
					if(is_move){
						is_move = false;

						points.Add(mouse_pos);
					}
				}
				
			}
		} else if(Input.GetMouseButtonUp(0)) {
			float angle_sum = 0;
			Vector3 pos_sum = Vector3.zero;
			Vector3 center;

			for(int p = 1; p <= points.Count - 1; p++){
				if(p <= points.Count - 2){
					float angle = calc_angle(points[p-1], points[p+1], points[p]);

					angle_sum += angle;
				} else if(p == points.Count - 1){

				}
			}

			for(int b = 0; b < points.Count; b++){
				pos_sum += points[b];
			}
			
			center = pos_sum/points.Count;

			if(points.Count > 0 && has_time == true){
				float pt_dist = Vector3.Distance(points[0], points[points.Count-1]);

				if((angle_sum < 180 && angle_sum > 0) || (angle_sum > 180 && points.Count > 4)){
					/*GameObject a = Instantiate(Resources.Load(Config.pref_inst_path + "Cube")) as GameObject ;
					a.transform.position = points[0];
					a.name = "point 0";

					GameObject a1 = Instantiate(Resources.Load(Config.pref_inst_path + "Cube")) as GameObject ;
					a1.transform.position = points[points.Count-1];
					a1.name = "point 1";*/


					if(pt_dist <= 2f && is_right(figure, center) == true){
						StartCoroutine(Funcs.play_sound(gameObject, "fig_success"));

						GameWnd game_wnd_comp = Display.game_wnd.GetComponent<GameWnd>();
						game_wnd_comp.add_score();
					} else {
						StartCoroutine(Funcs.play_sound(gameObject, "fig_error"));
					}

					is_proc = false;

				}
			}

			reset();
		}
	}
}
