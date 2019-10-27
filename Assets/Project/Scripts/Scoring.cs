using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static float Score;
    public static int CurLevel;
    public static Ticket CurTicket;
    private static int curLevelStep;
    private static Image ticketImage;
    private static Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        CurLevel = 1;
        curLevelStep = 1;

        ticketImage = GameObject.Find("Ticket Image").GetComponent<Image>();
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        scoreText.text = string.Format("${0:#.00}", Score);

        CurTicket = new Ticket();
        CurTicket.NewTicket(CurLevel);
        ticketImage.sprite = CurTicket.CurSprite;
    }

    public static void NextTicket()
    {
        curLevelStep++;
        Score += CurTicket.Profit;
        scoreText.text = string.Format("${0:#.00}", Score);

        if (curLevelStep % 3 == 0)
        {
            CurLevel++;
        }
        CurTicket.NewTicket(CurLevel);
        ticketImage.sprite = CurTicket.CurSprite;
    }
}
