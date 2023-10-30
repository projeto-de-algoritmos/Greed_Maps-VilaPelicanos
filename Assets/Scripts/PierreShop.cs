using System.Collections;
using System.Collections.Generic;
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

    public void OpenShop(Characters charac)
    {
        StartCoroutine(StartGameShop(charac));
    }

    IEnumerator StartGameShop(Characters charac)
    {
        ZoomInShop();

        yield return new WaitForSeconds(timeZoom / 2);

        ShowShop();

        yield return new WaitForSeconds(timeZoom / 2);

        StartCoroutine(MoveCharToShop(charac));
    }

    private void ZoomInShop()
    {
        villaPelicanos.LeanMoveLocal(posShop, timeZoom).setEaseInOutQuad();
        villaPelicanos.LeanScale(zoomShop, timeZoom).setEaseInOutQuad();
    }

    private void ShowShop()
    {
        shopPierre.LeanAlpha(1, timeZoom / 2).setEaseOutQuad();
    }

    IEnumerator MoveCharToShop(Characters charac)
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

        marketing.LeanAlpha(1, .5f).setEaseOutQuint();

        yield return new WaitForSeconds(.5f);
    }
}
