using UnityEngine;

using System;
using System.Collections;

public class RectComponent : MonoBehaviour {

	protected RectTransform rect_trans;

	// Авейк элемента
	public void Awake(){
		rect_trans = gameObject.AddComponent<RectTransform>();
		
		rect_trans.SetLeftTopPivot ();
	}

	// Добавить дочерный компонент
	public GameObject add_child(Type comp_type, string comp_name = ""){
		GameObject comp = new GameObject (comp_name == string.Empty ? comp_type.ToString() : comp_name);
		comp.transform.SetParent (gameObject.transform);
		
		comp.AddComponent (comp_type);
		
		return comp;
	}

	// Рект трансформ компонента
	public RectTransform get_trans(){
		return rect_trans;
	}

	// Установить позицию элемента
	public virtual void set_pos(Vector2 pos, bool loc = true){
		if (loc == true) {
			rect_trans.SetLeftTopPosition (new Vector2(pos.x, pos.y * -1));
		} else {
			rect_trans.SetLeftTopPosition(RеctTransExtens.PosOnScreen(pos));
		}
	}

	// Положение компонента
	public Vector2 get_pos(bool loc = true){
		if (loc == true) {
			return new Vector2 (rect_trans.localPosition.x, rect_trans.localPosition.y * -1);
		} else {
			GameObject canv = Funcs.get_cnv ();
			
			Vector3 item_pos = canv.transform.TransformPoint (rect_trans.position.x, rect_trans.position.y, 0);
			
			return RеctTransExtens.PosOnScreen(new Vector2(item_pos.x, item_pos.y - Screen.height));
		}
	}

	// Установить дращение компонента
	public void set_rot(){}

	// Угол вращения компонента
	public Vector2 get_rot(){return new Vector2();}

	// Установить размеры элемента
	public void set_size(RectSize size){
		rect_trans.SetWidth (size.width);
		rect_trans.SetHeight (size.height);
	}

	// Получить размеры элемента
	public RectSize get_size(){
		return new RectSize (rect_trans.rect.width, rect_trans.rect.height);
	}
}
