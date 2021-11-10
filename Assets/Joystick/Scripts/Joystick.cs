using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IInitializePotentialDragHandler, IDragHandler
{
	public RectTransform pointer;

	[SerializeField] private float minDistance;
	[SerializeField] private float maxDistance;
	[SerializeField] private float minValueThreshold;
	[SerializeField] private float maxValueThreshold;
	[SerializeField] private int unitAngleCount;

	// these variables are automatically set in Awake 
	private float sqrMinDistance;
	private float sqrMaxDistance;
	private float unitAngleInRadian;
	//

	private RectTransform rectTransform;
	private Camera uiCamera;

	private const float maxAngleInRadian = 2f * Mathf.PI;

	public Vector2 InputVector { get; private set; }
	public float Value { get; private set; }

	private void Awake()
	{
		OnPropertyChanged();

		rectTransform = GetComponent<RectTransform>();

		Canvas parentCanvas = GetComponentInParent<Canvas>();
		uiCamera = parentCanvas.worldCamera;
	}

	private void MovePointerTo(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, uiCamera, out Vector2 pos))
		{
			Vector2 dir = pos.normalized;
			float sqrMagnitude = pos.sqrMagnitude;
			if (sqrMagnitude < sqrMinDistance)
			{
				pos = Vector2.zero;
			}
			else if (sqrMagnitude > sqrMaxDistance)
			{
				pos = dir * maxDistance;
			}

			float magnitude = pos.magnitude;
			if (unitAngleCount > 0)
			{
				float angleInRadian = Mathf.Acos(Vector2.Dot(Vector2.right, dir));
				if (Vector2.Dot(Vector2.up, dir) < 0)
				{
					angleInRadian = -angleInRadian + maxAngleInRadian;
				}

				int angleIdx = (int)(angleInRadian / unitAngleInRadian + 0.5f);
				float snappedAngleInRadian = unitAngleInRadian * angleIdx;

				pos = new Vector2(Mathf.Cos(snappedAngleInRadian), Mathf.Sin(snappedAngleInRadian)) * magnitude;
			}

			pointer.localPosition = pos;
			InputVector = pos.normalized;
			Value = minValueThreshold != maxValueThreshold ?
				Mathf.Clamp01((magnitude - minValueThreshold) / (maxValueThreshold - minValueThreshold)) :
				1f;
		}
	}

	private void ResetPointer()
	{
		pointer.localPosition = Vector2.zero;
		InputVector = Vector2.zero;
		Value = 0;
	}

	#region OnPointer Interfaces
	public void OnPointerDown(PointerEventData eventData)
	{
		MovePointerTo(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		ResetPointer();
	}
	#endregion

	#region OnDrag Interfaces
	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		eventData.useDragThreshold = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		MovePointerTo(eventData);
	}
	#endregion

	public void OnPropertyChanged()
	{
		sqrMinDistance = minDistance * minDistance;
		sqrMaxDistance = maxDistance * maxDistance;
		unitAngleInRadian = maxAngleInRadian / unitAngleCount;
	}

#if UNITY_EDITOR
	private void Update()
	{
		for (int i = 0; i < unitAngleCount; i++)
		{
			float snappedAngleInRadian = unitAngleInRadian * i;
			Vector3 dir = new Vector3(Mathf.Cos(snappedAngleInRadian), Mathf.Sin(snappedAngleInRadian), 0);
			Vector3 start = rectTransform.position + rectTransform.TransformVector(dir * minDistance);
			Vector3 end = rectTransform.position + rectTransform.TransformVector(dir * maxDistance);

			Debug.DrawLine(start, end, Color.green);
		}

		Debug.DrawLine(rectTransform.position, pointer.position, Color.red);
	}
#endif
}
