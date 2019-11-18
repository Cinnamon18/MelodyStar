using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public enum ElementSelectionMethod {
    RESIZE
}

[ExecuteInEditMode]
public class SplineScrollView : Spliny
{
    public Transform elementsHolder;
    public float scrollAmount;
    public float elementSpacing = 100;

    public float scrollSpeed = 1;

    public int selectedElement = 3;

    float vertInputLastFrame;

    int totalNumElements;

    public AnimationCurve scrollOneElementCurve;
    public float singleElemScrollTime = .2f;
    public ElementSelectionMethod selectionMethod;
    public float selectedElemScale = 2f;

    Transform selectedElem;
    Transform lastSelectedElem;

    bool isScrolling = false;

    // Start is called before the first frame update
    void Start()
    {        
        CalculatePercentagesAlongSpline();
        totalNumElements = elementsHolder.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositionsOfElements();

        int v = (int) Input.GetAxisRaw("Vertical");
        if (!isScrolling) {
            if (v != 0 && 0 == vertInputLastFrame) {
                ScrollSingleElem(v);
            }
            if (v == 0 && vertInputLastFrame != 0) {
                // stop scrolling so we should snap to the closest elem
            }
        } else {
            v = 0;
        }
        vertInputLastFrame = v;
    }

    void UpdatePositionsOfElements() {
        int i = 0;
        float elemSpacingScaled = elementSpacing / 100.0f;
        //scrollAmount = Mathf.Clamp(scrollAmount, 0, 1);
        foreach (Transform element in elementsHolder) {
            
            float t = scrollAmount + elemSpacingScaled * i;
            t = Mathf.Clamp(t, 0.0f, 1.0f);
            //element.GetComponentInChildren<TextMeshProUGUI>().text = "t: " + t;
            //}
            element.transform.position = Evaluate(t);
            i++;
        }
    }
    
    void Scroll(float amount) {
        Debug.Log("Scroll Amount: " + amount);
        scrollAmount += amount * scrollSpeed;
        scrollAmount = Mathf.Clamp(scrollAmount, 0f, 1f);
    }

    public void ScrollSingleElem(int dir) {
        selectedElement -= dir;

        if (selectedElement < 0) {
            selectedElement = 0;
        } else if (selectedElement > elementsHolder.childCount - 1) {
            selectedElement = elementsHolder.childCount - 1;
        } else {
            isScrolling = true;
            StartCoroutine(ScrollingSingleElem(dir));
        }
    }

    IEnumerator ScrollingSingleElem(int dir) {
        float progress = 0;
        float speed = 1 / singleElemScrollTime;
        float startScrollAmount = scrollAmount;

        if (dir < 0) {
            lastSelectedElem = elementsHolder.GetChild(selectedElement - 1);
        } else {
            lastSelectedElem = elementsHolder.GetChild(selectedElement + 1);
        }

        if (lastSelectedElem != null)
            lastSelectedElem.GetComponent<Canvas>().sortingOrder = 0;
        selectedElem = elementsHolder.GetChild(selectedElement);
        selectedElem.GetComponent<Canvas>().sortingOrder = 1;

        while (progress < 1) {
            progress += Time.deltaTime * speed;
            if (progress > 1) progress = 1;
            yield return null;

            float progressFromCurve = scrollOneElementCurve.Evaluate(progress);

            scrollAmount = startScrollAmount + elementSpacing / 100.0f * progressFromCurve * dir;

            SetHowSelected(progressFromCurve, dir);

            UpdatePositionsOfElements();
        }
        
        if (false) {
        if (dir < 0) {
            // move the last element to the top
            elementsHolder.GetChild(0).SetSiblingIndex(elementsHolder.childCount - 1); 
            selectedElement -= 1;
        } else {
            // move the last element to the top
            elementsHolder.GetChild(elementsHolder.childCount - 1).SetSiblingIndex(0); 
            selectedElement += 1;
        }
        }

        UpdatePositionsOfElements();
        //scrollAmount = startScrollAmount;
        isScrolling = false;
    }

    void SetHowSelected(float amount, int dir) {
        switch (selectionMethod) {
            case (ElementSelectionMethod.RESIZE):
            selectedElem.localScale 
                = Vector3.Lerp(new Vector3(1,1,1), new Vector3(selectedElemScale, selectedElemScale, selectedElemScale), amount);

            lastSelectedElem.localScale 
            = Vector3.Lerp(new Vector3(1,1,1), new Vector3(selectedElemScale, selectedElemScale, selectedElemScale), 1 - amount);
            break;
        }
    }
}
