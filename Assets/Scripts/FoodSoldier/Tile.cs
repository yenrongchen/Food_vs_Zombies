using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Tile : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _no;
    [SerializeField] private FoodSoldierSpawner _spawner;

    private FoodSoldier _foodSoldier;

    private bool _occupied;
    private bool _canInteract;

    void Start()
    {
        _spawner = FindObjectOfType<FoodSoldierSpawner>();
        _occupied = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!_canInteract || eventData.pointerDrag == null) return;

        //Debug.Log("DROP");
        if (!_occupied && eventData.pointerDrag.GetComponent<DragDrop>().getFoodCnt() > 0)
        {
            float tmpX = 0.8f + transform.position.x;
            float tmpY = transform.position.y;

            var foodData = eventData.pointerDrag.GetComponent<DragDrop>().getFoodData();

            _foodSoldier = _spawner.Spawn(foodData, new Vector2(tmpX, tmpY), this);
            eventData.pointerDrag.GetComponent<DragDrop>().Pop();
            _occupied = true;
            //Debug.Log("occupied" + _occupied);
        }
        else if (_occupied && eventData.pointerDrag.CompareTag("KillFood")) 
        {
            if (_foodSoldier != null) _foodSoldier.Kill();
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_canInteract || eventData.pointerDrag == null) return;
        
        if (eventData.pointerDrag.CompareTag("KillFood"))
        {
            if (_occupied)
            {
                _no.SetActive(true);
                _highlight.SetActive(true);
            }
        }
        else if (!_occupied)
        {
            _highlight.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_highlight.activeSelf)
        {
            _highlight.SetActive(false);
        }
        
        if (_no.activeSelf)
        {
            _no.SetActive(false);
        }
    }

    public void Clear()
    {
        _occupied = false;
    }

    public void SetInteract(bool interact)
    {
        _canInteract = interact;
    }

}
