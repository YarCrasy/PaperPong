using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideScript : MonoBehaviour
{
    int actualPage = 0;
    [SerializeField] Text actualPageDisplay;
    [SerializeField] GameObject[] guideObjects, guidePages;

    public void SetActivePage(bool activate)
    {
        guidePages[actualPage].SetActive(activate);
        actualPageDisplay.text = (actualPage + 1) + "/" + guidePages.Length;
    }

    public void NextPage()
    {
        actualPage++;
        if (actualPage > guidePages.Length-1)
        {
            actualPage = 0;
        }
    }

    public void PreviousPage()
    {
        actualPage--;
        if (actualPage < 0)
        {
            actualPage = guidePages.Length - 1;
        }
    }

    public void SetActiveGuideObjects(bool activate)
    {
        for (int i = 0; i < guideObjects.Length; i++)
        {
            guideObjects[i].SetActive(activate);
        }
        SetActivePage(activate);
        if (!activate)
        {
            actualPage = 0;
        }
    }

}
