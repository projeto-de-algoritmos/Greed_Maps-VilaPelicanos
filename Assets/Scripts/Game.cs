using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class Game : MonoBehaviour
{
    public Manager manager;

    public GameObject prefabChar;
    public Transform parentChar;

    public GameObject prefabTarget;
    public Transform parentTarget;
    public Sprite target01;
    public Sprite target02;

    public List<Button> characters;

    public Image char01;
    public TextMeshProUGUI startNode01;
    public TextMeshProUGUI endNode01;

    public Image char02;
    public TextMeshProUGUI startNode02;
    public TextMeshProUGUI endNode02;

    public GameObject charObj01;
    public GameObject charObj02;

    public GameObject targetObj1;
    public GameObject targetObj2;

    public float tamBarra = 50f;
    
    public int selectionChar = 1;
    public int selectionText = 1;

    public int isMoving = 0;

    public void CreateCharacter(Node node, int numChar)
    {
        GameObject characterInstance = Instantiate(prefabChar, parentChar);
        characterInstance.transform.localPosition = node.gameObject.transform.localPosition;

        Image characterImage = characterInstance.GetComponent<Image>();

        if (numChar == 1)
        {
            if (charObj01 != null)
            {
                Destroy(charObj01);
            }

            characterImage.sprite = char01.sprite;
            charObj01 = characterInstance;
        }
        else
        {
            if (charObj02 != null)
            {
                Destroy(charObj02);
            }

            characterImage.sprite = char02.sprite;
            charObj02 = characterInstance;
        }
    }

    public void CreateTarget(Node node, int numChar)
    {
        GameObject targetInstance = Instantiate(prefabChar, parentTarget);
        targetInstance.transform.localPosition = new Vector2(
            node.gameObject.transform.localPosition.x,
            node.gameObject.transform.localPosition.y + targetInstance.GetComponent<RectTransform>().sizeDelta.y / 2
            );

        Image targetImage = targetInstance.GetComponent<Image>();

        if (numChar == 1)
        {
            if (targetObj1 != null)
            {
                Destroy(targetObj1);
            }

            targetImage.sprite = target01;
            targetObj1 = targetInstance;
        }
        else
        {
            if (targetObj2 != null)
            {
                Destroy(targetObj2);
            }

            targetImage.sprite = target02;
            targetObj2 = targetInstance;
        }
    }

    public void StartMoveChars(List<Node> nodes, int numChar)
    {
        if (isMoving == 2 || manager.finishChars != 0)
            return;

        GameObject charObj;

        if (numChar == 1)
        {
            if (charObj01 == null)
                CreateCharacter(nodes[0], numChar);

            if (targetObj1 == null)
                CreateTarget(nodes[^1], numChar);

            charObj = charObj01;
        }
        else
        {
            if (charObj02 == null)
                CreateCharacter(nodes[0], numChar);

            if (targetObj2 == null)
                CreateTarget(nodes[^1], numChar);

            charObj = charObj02;
        }

        charObj.GetComponent<Characters>().StartChar(nodes, manager, this);
        isMoving++;
    }

    public void SelectionCharacter(Characters charac)
    {
        if (isMoving != 0)
            return;

        if (selectionChar == 1)
        {
            char01.sprite = charac.imageChar;
            if (charObj01 != null)
                charObj01.GetComponent<Image>().sprite = charac.imageChar;
        }
        else if (selectionChar == 2)
        {
            char02.sprite = charac.imageChar;
            if (charObj02 != null)
                charObj02.GetComponent<Image>().sprite = charac.imageChar;
        }
    }

    public void setSelectionText(int text)
    {
        selectionText = text;
    }

    public void SelectionNode(Node node)
    {
        if (isMoving != 0)
            return;

        if (selectionText == 1)
        {
            setNodeStart(node);
        }
        else 
        { 
            setNodeEnd(node);
        }
    }

    public void setNodeStart(Node node)
    {
        CreateCharacter(node, selectionChar);

        if (selectionChar == 1) 
        {
            startNode01.text = node.Id.ToString();
            manager.startCharacter01 = node;                        
        }
        else if (selectionChar == 2)
        {
            startNode02.text = node.Id.ToString();
            manager.startCharacter02 = node;
        }
    }

    public void setNodeEnd(Node node)
    {
        CreateTarget(node, selectionChar);

        if (selectionChar == 1)
        {
            endNode01.text = node.Id.ToString();
            manager.endCharacter01 = node;
        }
        else if (selectionChar == 2)
        {
            endNode02.text = node.Id.ToString();
            manager.endCharacter02 = node;
        }
    }

    public void setSelectionChar(int charac) 
    {
        selectionChar = charac;
    }
}
