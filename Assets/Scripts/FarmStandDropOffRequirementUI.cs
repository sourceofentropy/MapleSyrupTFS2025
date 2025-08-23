using UnityEngine;
using UnityEngine.UI;
using TMPro;

//TODO: this clones DropOffRequirementUI - make an abstract once you have a working test
public class FarmStandDropOffRequirementUI : MonoBehaviour
{
    public Image itemIcon; // Assign in Prefab
    public TextMeshProUGUI progressText; // Assign in Prefab

    private Transform target;

    public void Initialize(Transform followTarget, Sprite icon)
    {
        target = followTarget;
        itemIcon.sprite = icon;
        UpdateDropOffProgress(0);
    }

    public void UpdateDropOffProgress(int current)
    {
        progressText.text = $"{current}";
    }

    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position + Vector3.up * 2f);
        }
    }
}
