using UnityEngine;

public class GameView : MonoBehaviour
{
    private const float fadeDuration = 0.4f;
    public CanvasGroup group;

    public bool isTransitioning = false;
    public bool targetVisibility = false;
    public float transitionStartTime = 0.0f;

    public void SetVisible(bool Visible)
    {
        isTransitioning = true;
        targetVisibility = Visible;
        transitionStartTime = Time.time;
    }

    public virtual void Start()
    {
        SetVisible(false);
    }

    public virtual void UpdateView() {

    }
    
    void Update()
    {
        if (isTransitioning) {

            float duration = Time.time - transitionStartTime;
            float progress = duration / fadeDuration;
            if (progress < 1)
            {
                group.alpha = targetVisibility ? progress : (1 - progress);
                group.interactable = targetVisibility;
                group.blocksRaycasts = targetVisibility;
            }
            else {
                isTransitioning = false;
                group.alpha = targetVisibility ? 1.0f : 0.0f;
            }

        }
    }
}
