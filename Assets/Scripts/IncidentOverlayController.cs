using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class IncidentOverlayController : MonoBehaviour {
    [Header("UI References")]
    [SerializeField] private Transform stepCardContainer;
    [SerializeField] private GameObject stepCardPrefab;
    [SerializeField] private TextMeshProUGUI detailTitleText;
    [SerializeField] private Transform checklistContainer;
    [SerializeField] private GameObject checklistItemPrefab;
    [SerializeField] private TextMeshProUGUI phaseBadgeText;
    [SerializeField] private Slider progressSlider;

    private int currentStep = 0;

    void Start() {
        GenerateStepCards();
        ShowStep(0);
    }

    void GenerateStepCards() {
        for (int i = 0; i < TyphoonData.Steps.Count; i++) {
            int index = i;
            var card = Instantiate(stepCardPrefab, stepCardContainer);
            var btn = card.GetComponent<Button>();
            var label = card.GetComponentInChildren<TextMeshProUGUI>();
            label.text = $"Step {TyphoonData.Steps[i].stepNumber}\n{TyphoonData.Steps[i].title}";
            btn.onClick.AddListener(() => ShowStep(index));
        }
    }

    public void ShowStep(int index) {
        currentStep = index;
        var step = TyphoonData.Steps[index];

        detailTitleText.text = $"STEP {step.stepNumber} — {step.title.ToUpper()}";
        phaseBadgeText.text = step.phase;
        progressSlider.value = (float)(index + 1) / TyphoonData.Steps.Count;

        foreach (Transform child in checklistContainer)
            Destroy(child.gameObject);

        foreach (var item in step.checklistItems) {
            var listItem = Instantiate(checklistItemPrefab, checklistContainer);
            listItem.GetComponentInChildren<TextMeshProUGUI>().text = item;
        }
    }
}