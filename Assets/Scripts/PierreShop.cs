using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PierreShop : MonoBehaviour
{
    public bool isOpen = false;
    public Vector2 posShop;
    public Vector2 zoomShop;
    public GameObject villaPelicanos;
    public CanvasGroup shopPierre;
    public float timeZoom = 3f;
    public Transform parentChar;
    public List<Vector2> positionsChar = new List<Vector2>();
    public float speed = 50;
    public CanvasGroup marketing;
    public Characters charac;

    public Vector2 originPos;
    public Transform originParent;

    public CanvasGroup nodes;
    public CanvasGroup edges;
    public CanvasGroup distance;
    public CanvasGroup target;

    public void OpenShop(Characters _charac)
    {
        charac = _charac;
        originParent = _charac.transform.parent;
        originPos = _charac.transform.localPosition;

        StartCoroutine(StartGameShop());
    }

    IEnumerator StartGameShop()
    {
        ZoomInShop();

        yield return new WaitForSeconds(timeZoom / 2);

        ShowShop();

        yield return new WaitForSeconds(timeZoom / 2);

        StartCoroutine(MoveCharToShop());
    }

    private void ZoomInShop()
    {
        nodes.LeanAlpha(0, timeZoom / 2);
        edges.LeanAlpha(0, timeZoom / 2);
        distance.LeanAlpha(0, timeZoom / 2);
        target.LeanAlpha(0, timeZoom / 2);

        villaPelicanos.LeanMoveLocal(posShop, timeZoom).setEaseInOutQuad();
        villaPelicanos.LeanScale(zoomShop, timeZoom).setEaseInOutQuad();
    }

    private void ShowShop()
    {
        shopPierre.LeanAlpha(1, timeZoom / 2).setEaseOutQuad();
    }

    IEnumerator MoveCharToShop()
    {
        charac.transform.SetParent(parentChar);
        charac.transform.localScale = new Vector2(3, 3);
        charac.gameObject.transform.localPosition = positionsChar[0];

        foreach (Vector2 pos in positionsChar)
        {
            float timeMove = DistanceNode.Distance(pos, charac.transform.localPosition) / speed;
            charac.transform.LeanMoveLocal(pos, timeMove);

            yield return new WaitForSeconds(timeMove);
        }

        StartCoroutine(OpenMarket());
    }

    IEnumerator OpenMarket()
    {
        yield return new WaitForSeconds(.5f);

        marketing.gameObject.SetActive(true);
        marketing.LeanAlpha(1, .5f).setEaseOutQuint();

        yield return new WaitForSeconds(.5f);
    }

    public void ExitShop()
    {
        StartCoroutine(StartCloseShop());
    }

    IEnumerator StartCloseShop()
    {
        StartCoroutine(CloseMarket());

        yield return new WaitForSeconds(1f);

        StartCoroutine(MoveCharToExit());
    }

    IEnumerator CloseMarket()
    {
        
        marketing.LeanAlpha(0, .5f).setEaseOutQuint();

        yield return new WaitForSeconds(.5f);

        marketing.gameObject.SetActive(false);
    }

    IEnumerator MoveCharToExit()
    {
        for (int i = positionsChar.Count -1; i >= 0; i--)
        {
            float timeMove = DistanceNode.Distance(positionsChar[i], charac.transform.localPosition) / speed;
            charac.transform.LeanMoveLocal(positionsChar[i], timeMove);

            yield return new WaitForSeconds(timeMove);
        }

        charac.transform.SetParent(originParent);
        charac.transform.localScale = new Vector2(1, 1);
        charac.gameObject.transform.localPosition = originPos;

        HideShop();
        DezoomInShop();

        yield return new WaitForSeconds(timeZoom);

        isOpen = false;
        gameObject.SetActive(false);
    }

    private void HideShop()
    {
        shopPierre.LeanAlpha(0, timeZoom / 2).setEaseInQuad();
    }

    private void DezoomInShop()
    {
        nodes.LeanAlpha(1, timeZoom / 2);
        edges.LeanAlpha(1, timeZoom / 2);
        distance.LeanAlpha(1, timeZoom / 2);
        target.LeanAlpha(1, timeZoom / 2);

        villaPelicanos.LeanMoveLocal(Vector3.zero, timeZoom).setEaseInOutQuad();
        villaPelicanos.LeanScale(Vector3.one, timeZoom).setEaseInOutQuad();
    }

    
}
