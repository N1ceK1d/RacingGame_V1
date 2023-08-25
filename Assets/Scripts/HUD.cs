using System.Collections;
using UnityEngine;
public class HUD : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Rigidbody player;
    public float duration = .5f;

    private void FixedUpdate() {
        if((int)(player.velocity.magnitude * 3.6) > 15 && !Input.GetKey(KeyCode.I))
        {
            StartCoroutine(DoHide(canvasGroup, canvasGroup.alpha, 0));
        }

        if((int)(player.velocity.magnitude * 3.6) < 10 || Input.GetKey(KeyCode.I)) 
        {
            StartCoroutine(DoShow(canvasGroup, canvasGroup.alpha, 1));
        }
    }

    public IEnumerator DoHide(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while(counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }
    public IEnumerator DoShow(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while(counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }

}
