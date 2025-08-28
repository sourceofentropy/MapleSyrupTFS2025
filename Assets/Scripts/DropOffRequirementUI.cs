using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropOffRequirementUI : MonoBehaviour
{
    public Image itemIcon; // Assign in Prefab
    public TextMeshProUGUI progressText; // Assign in Prefab

    private Transform target;

    public void Initialize(Transform followTarget, Sprite icon, int totalRequired)
    {
        target = followTarget;
        itemIcon.sprite = icon;
        UpdateDropOffProgress(0, totalRequired);
    }

    public void InitializeFarmStand(Transform followTarget, Sprite icon, int totalRequired)
    {
        target = followTarget;
        itemIcon.sprite = icon;
        UpdateDropOffProgress(0, totalRequired);
    }

    public void UpdateDropOffProgress(int current, int total)
    {
        progressText.text = $"{current}/{total}";
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
