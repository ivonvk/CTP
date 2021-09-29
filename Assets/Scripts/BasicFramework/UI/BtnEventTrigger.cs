using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnEventTrigger : MonoBehaviour, IPointerUpHandler, IPointerDownHandler,IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    public PlayerBtnDown BtnDownMethodToCall;
    public PlayerBtnUp BtnUpMethodToCall;
    public PlayerBtnEnter BtnEnterMethodToCall;
    public PlayerBtnExit BtnExitMethodToCall;
    public RectTransform MiniPos;
    public void SetMiniPos(GameObject obj)
    {
        MiniPos = obj.GetComponent<RectTransform>();
    }
    public virtual void OnPointerUp(PointerEventData ped)
      {
         // BtnUp(BtnUpMethodToCall);
      }
    public virtual void OnDrag(PointerEventData ped)
    {

    }
    public virtual void OnPointerExit(PointerEventData ped)
    {
        BtnExit(BtnExitMethodToCall);
    }
    public virtual void OnPointerEnter(PointerEventData ped)
    {
        BtnEnter(BtnEnterMethodToCall);
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        BtnDown(BtnDownMethodToCall);
    }

    public virtual void BtnDown(PlayerBtnDown method)
    {
        method();
    }
    public virtual void BtnUp(PlayerBtnUp method)
    {
        method();
    }
    public virtual void BtnEnter(PlayerBtnEnter method)
    {
        if (method != null)
        {
            method();
        }
    }
    public virtual void BtnExit(PlayerBtnExit method)
    {
        method();
    }
}
