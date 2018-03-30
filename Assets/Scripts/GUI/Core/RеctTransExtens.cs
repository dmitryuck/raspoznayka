using UnityEngine;

using System.Collections;

public static class RеctTransExtens {

	// Взять координаты левого верхнего угла экрана
	public static Vector2 GetScreenCenter(){
		GameObject canvas = Funcs.get_cnv ();
		
		RectTransform canvas_rect = (RectTransform)canvas.GetComponent<RectTransform> ();
		
		return new Vector2 (canvas_rect.pivot.x - Screen.width / 2, canvas_rect.pivot.y + Screen.height / 2);
	}
	
	// Установить позицию элемента относительно верхнего левого угла экрана
	public static Vector2 PosOnScreen(Vector2 pos){
		Vector2 v2 = GetScreenCenter ();
		
		return new Vector2 (v2.x + pos.x, v2.y - pos.y);
	}

	// Установить положение пивота в обьекте
	public static void SetPivot(this RectTransform trans, Vector2 newPos) {
		trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
	}

	// Установить пивот в левый верхний угол
	public static void SetLeftTopPivot(this RectTransform trans){
		trans.pivot = new Vector2(0,1);
		
		trans.anchorMin = new Vector2(0,1);
		trans.anchorMax = new Vector2(0,1);
	}

	/*
	// Установить пиво в левый нижний угол
	public static void SetLeftBottomPivot(this RectTransform trans){
		trans.pivot = new Vector2(0,1);
		
		trans.anchorMin = new Vector2(0,0);
		trans.anchorMax = new Vector2(0,0);
	}*/

	// Установить положение якорей обьекта
	public static void SetAnchors(this RectTransform trans, Vector2 minPos, Vector2 maxPos){
		trans.anchorMin = minPos;
		trans.anchorMax = maxPos;
	}

	// Установить позицию обьекта по его левому верхнему углу
	public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos) {
		SetLeftTopPivot (trans);

		trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
	}

	// Установить положение обьекта по его левому нижнему углу
	public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos) {
		trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
	}

	/*
	// Установить положение обьекта по его правому верхнему углу
	public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos) {
		trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
	}

	// Установить положение обьекта по его правому нижнему углу
	public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos) {
		trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
	}*/

	// Установить размеры для Рект Трансформа
	public static void SetSize(this RectTransform trans, Vector2 newSize) {
		Vector2 oldSize = trans.rect.size;
		Vector2 deltaSize = newSize - oldSize;

		trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
		trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
	}

	// Установить дефолтный скале
	public static void SetDefaultScale(this RectTransform trans) {
		trans.localScale = new Vector3(1, 1, 1);
	}

	// Взять значение размеров обьекта
	public static Vector2 GetSize(this RectTransform trans) {
		return trans.rect.size;
	}

	// Взять ширину обьекта
	public static float GetWidth(this RectTransform trans) {
		return trans.rect.width;
	}

	// Взять высоту обьекта
	public static float GetHeight(this RectTransform trans) {
		return trans.rect.height;
	}

	// Установить ширину обьекта
	public static void SetWidth(this RectTransform trans, float newSize) {
		SetSize(trans, new Vector2(newSize, trans.rect.size.y));
	}

	// Установить высоту обьекта
	public static void SetHeight(this RectTransform trans, float newSize) {
		SetSize(trans, new Vector2(trans.rect.size.x, newSize));
	}
}