using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityPickerItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Ability PickedAbility;

    public static AbilityPickerItem CurrentlyDraggingAbilityPicker;

    [SerializeField] private Image _image;
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector2 _startPosition;
    private Sprite _defaultSprite;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _defaultSprite = _image.sprite;
    }

    private void Start()
    {
        _canvas = UIControl.Instance.MainCanvas;
    }

    public void SetAbility(Ability ability)
    {
        PickedAbility = ability;

        if (ability != null)
        {
            _image.sprite = ability.Data.Icon;
        }
        else
        {
            _image.sprite = _defaultSprite;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PickedAbility != null)
        {
            _canvasGroup.alpha = .8f;
            _startPosition = _rectTransform.anchoredPosition;
            _canvasGroup.blocksRaycasts = false;
            CurrentlyDraggingAbilityPicker = this;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        CurrentlyDraggingAbilityPicker = null;
        _rectTransform.anchoredPosition = _startPosition;
    }
}