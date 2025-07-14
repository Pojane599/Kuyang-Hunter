using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndTextEffect : MonoBehaviour
{
    public TextMeshProUGUI endText;
    public float typingSpeed = 0.1f;
    public float delayBeforeQuestionMark = 1.5f;
    public float fadeDuration = 1.5f;

    private string fullText = "The End";
    private string fullTextWithQuestion = "The End?";

    private void Start()
    {
        endText.text = "";
        endText.alpha = 0f;
        StartCoroutine(PlayTextEffect());
    }

    private IEnumerator PlayTextEffect()
    {
        // Fade-in
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            endText.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        // Typing animation
        for (int i = 0; i < fullText.Length; i++)
        {
            endText.text += fullText[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        // Delay sebelum tanda tanya
        yield return new WaitForSeconds(delayBeforeQuestionMark);

        // Tambahkan tanda tanya
        endText.text = fullTextWithQuestion;
    }
}

