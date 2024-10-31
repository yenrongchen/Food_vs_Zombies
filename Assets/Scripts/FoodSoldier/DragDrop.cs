using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _image;
    [SerializeField] private FoodSoldierData _foodData;
    [SerializeField] private int _foodCnt = 0;
    [SerializeField] private Text _cntText;

    private Vector2 _initPosition;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;


    private void Awake()
    {
        _foodCnt = 0;
        _initPosition = GetComponent<RectTransform>().anchoredPosition;
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        if (!CompareTag("KillFood"))
        {
            _cntText.text = _foodCnt.ToString();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("BeginDrag");
        _canvasGroup.alpha = 0.8f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if (_foodCnt > 0 || CompareTag("KillFood"))
        {
            //_rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                _canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector3 globalMousePos
            );

            _rectTransform.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("EndDrag");
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        _rectTransform.anchoredPosition = _initPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void Pop()
    {
        if (_foodCnt > 0 && !CompareTag("KillFood"))
        {
            _foodCnt--;
            _cntText.text = _foodCnt.ToString();

            if (_foodCnt <= 0)
            {
                Color tmpColor = _image.color;
                tmpColor.a = 0.5f;
                _image.color = tmpColor;
                _cntText.color = new Color(_cntText.color.r, _cntText.color.g, _cntText.color.b, 0.5f);
            }
        }
    }

    public void Push()
    {
        if (CompareTag("KillFood")) return;
         
        _foodCnt++;
        _cntText.text = _foodCnt.ToString();

        if (_foodCnt == 1)
        {
            Color tmpColor = _image.color;
            tmpColor.a = 1f;
            _image.color = tmpColor;
            _cntText.color = new Color(_cntText.color.r, _cntText.color.g, _cntText.color.b, 1f);
        }
    }

    public int getFoodCnt()
    {
        return _foodCnt;
    }

    public FoodSoldierData getFoodData()
    {
        return _foodData;
    }
}
