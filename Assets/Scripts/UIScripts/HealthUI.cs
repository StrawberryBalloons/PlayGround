using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    float healthTime = 5;

    float lastMadeVisible;

    Transform cam;

    Transform ui;
    Image healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }

        GetComponent<CharacterStats>().OnHealthChanges += OnHealthChanges;
    }

    void OnHealthChanges(int max, float current)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastMadeVisible = Time.time;
            float healthPercent = current / max;

            healthSlider.fillAmount = healthPercent;

            if (current <= 0)
            {
                //Destroy(ui.gameObject);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;
            if(Time.time - lastMadeVisible > healthTime){
                ui.gameObject.SetActive(false);
            }
        }
    }
}
