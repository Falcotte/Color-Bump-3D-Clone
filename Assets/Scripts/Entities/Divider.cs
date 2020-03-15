using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Divider : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI dividerText;
    [SerializeField] private int positionPercentage;

    private void Start() {
        PlaceDivider();
    }

    private void PlaceDivider() {
        float yPos = 0f;

        switch(positionPercentage) {
            case 0:
                dividerText.text = "STAGE " + (DataManager.Instance.Level + 1).ToString();
                GetComponent<RectTransform>().localPosition = new Vector3(0, -35f, -0.2f);
                break;

            case 25:
                yPos = (LevelSettings.Level.GetLevelLength() - 15f) / 4f - 35f;
                dividerText.text = "%25";
                GetComponent<RectTransform>().localPosition = new Vector3(0, yPos, -0.2f);
                break;

            case 50:
                yPos = (LevelSettings.Level.GetLevelLength() - 15f) * 2f / 4f - 35f;
                dividerText.text = "%50";
                GetComponent<RectTransform>().localPosition = new Vector3(0, yPos, -0.2f);
                break;

            case 75:
                yPos = (LevelSettings.Level.GetLevelLength() - 15f) * 3f / 4f - 35f;
                dividerText.text = "%75";
                GetComponent<RectTransform>().localPosition = new Vector3(0, yPos, -0.2f);
                break;
        }
    }
}
