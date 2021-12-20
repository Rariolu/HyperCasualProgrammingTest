using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : UIScript
{
    [SerializeField]
    Text lblPoints;

    [SerializeField]
    Image pbPointsBackground;

    [SerializeField]
    float typingCharDuration = 0.2f;

    protected override void Start()
    {
        base.Start();
        if (GameStats.Instance.EndState == END_STATE.WIN)
        {
            uint coins = GameStats.Instance.Coins;
            uint collectedBursts = GameStats.Instance.CollectedSpeedBursts;
            uint totalBursts = GameStats.Instance.BurstQuantity;
            uint burstsRemaining = totalBursts - GameStats.Instance.CollectedSpeedBursts;
            float mult = (float)collectedBursts / (float)totalBursts;

            int points = Mathf.RoundToInt(mult * coins);

            string message = string.Format("Coins collected: {0}\nPies collected: {1}\nPies remaining: {2}\nPoints: {3}", coins, collectedBursts, burstsRemaining, points);

            StartCoroutine(WritePointsLabel(message, typingCharDuration));
        }
        else
        {
            string message = "You've lost :'(.\nPlease try again.";
            StartCoroutine(WritePointsLabel(message, typingCharDuration));
        }
    }

    /// <summary>
    /// Append string to label one character at a time at a given duration per character.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="charDuration"></param>
    /// <returns></returns>
    IEnumerator WritePointsLabel(string text, float charDuration)
    {
        foreach(char c in text)
        {
            lblPoints.text += c;
            yield return new WaitForSeconds(charDuration);
        }
    }
}