using System.Collections;
using UnityEngine;

public class StructureBuilder : MonoBehaviour
{

    //ADD THIS CLASS TO ALL UNLOCKABLE STRUCTURES FOR ANIMATING

    public Transform structure;
    public float buildTime = 1f;
    public string structureName;


    public void Start()
    {

        //disable objects at game start
        structure.gameObject.SetActive(false);

    }

    public void StartBuild()
    {
        structure.gameObject.SetActive(true);


        IStructureUI[] uiComponents = structure.GetComponentsInChildren<IStructureUI>();
        foreach (var uiComponent in uiComponents)
        {
            uiComponent.InitializeUI();
        }

        StartCoroutine(BuildSequence());
    }
   
    private IEnumerator BuildSequence()
    {
        Vector3 originalScale = structure.localScale;
        structure.localScale = Vector3.zero;

        float elapsedTime = 0;

        while (elapsedTime < buildTime)
        {
            elapsedTime += Time.deltaTime;
            structure.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsedTime / buildTime);
            yield return null;
        }
        structure.localScale = originalScale;
    }

}
