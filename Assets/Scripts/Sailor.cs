using UnityEngine;
using UnityEngine.UI;

public class Sailor : MonoBehaviour
{
    public enum State { Idle, Working }
    public State currentState = State.Idle;

    public float taskDuration = 5f;
    private float taskTimer = 0f;

    [Header("UI")]
    public GameObject progressBarUI; // Slider parent
    public Slider progressSlider;    // Le slider lui-même

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                if (Input.GetMouseButtonDown(0) && IsClicked())
                {
                    StartTask();
                }
                break;

            case State.Working:
                taskTimer += Time.deltaTime;
                float progress = Mathf.Clamp01(taskTimer / taskDuration);
                progressSlider.value = progress;

                if (progress >= 1f)
                {
                    EndTask();
                }
                break;
        }
    }

    void StartTask()
    {
        currentState = State.Working;
        taskTimer = 0f;

        progressSlider.value = 0f;
        progressBarUI.SetActive(true);
    }

    void EndTask()
    {
        currentState = State.Idle;
        progressBarUI.SetActive(false);
    }

    bool IsClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == transform;
        }
        return false;
    }
}
