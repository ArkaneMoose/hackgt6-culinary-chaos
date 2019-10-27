using System;
using System.Collections.Generic;
using UnityEngine;

public class Ticket
{
  public Sprite CurSprite;
  public float Profit;
  private List<string> curIngredients;
  public bool HasWon;
  private static IDictionary<int, int[]> levelToSpriteRange;
  private static TicketInfo[] possibleTickets;
  private class TicketInfo
  {
    public Sprite Sprite;
    public string[] Ingredients;
    public float Profit;
    public TicketInfo(Sprite sprite, string[] ingredients, float profit)
    {
      this.Sprite = sprite;
      this.Ingredients = ingredients;
      this.Profit = profit;
    }
  }

  public Ticket()
  {
    possibleTickets = new TicketInfo[6];
    string[] ingredients1 = { "patty", "top-bun", "bottom-bun" };
    possibleTickets[0] = new TicketInfo(Resources.Load<Sprite>("ticket-patty"), ingredients1, 3.50f);
    string[] ingredients2 = { "patty", "cheese", "top-bun", "bottom-bun" };
    possibleTickets[1] = new TicketInfo(Resources.Load<Sprite>("ticket-patty-cheese"), ingredients2, 3.75f);
    string[] ingredients3 = { "patty", "tomato", "top-bun", "bottom-bun" };
    possibleTickets[2] = new TicketInfo(Resources.Load<Sprite>("ticket-patty-tomato"), ingredients3, 3.75f);
    string[] ingredients4 = { "patty", "tomato", "lettuce", "top-bun", "bottom-bun" };
    possibleTickets[3] = new TicketInfo(Resources.Load<Sprite>("ticket-patty-tomato-lettuce"), ingredients4, 3.75f);
    string[] ingredients5 = { "patty", "tomato", "cheese", "top-bun", "bottom-bun" };
    possibleTickets[4] = new TicketInfo(Resources.Load<Sprite>("ticket-patty-cheese-tomato"), ingredients5, 4.25f);
    string[] ingredients6 = { "patty", "tomato", "lettuce", "cheese", "top-bun", "bottom-bun" };
    possibleTickets[5] = new TicketInfo(Resources.Load<Sprite>("ticket-patty-cheese-tomato-lettuce"), ingredients6, 4.25f);

    levelToSpriteRange = new Dictionary<int, int[]>();

    levelToSpriteRange.Add(1, new int[] { 0, 2 }); // for level 1, only allow first 2 tickets
    levelToSpriteRange.Add(2, new int[] { 2, 4 }); // for level 2, allow tickets 3 or 4
    levelToSpriteRange.Add(3, new int[] { 3, 6 }); // for level 3, allow tickets 4 through 6
  }
  public void NewTicket(int curLevel)
  {
    HasWon = false;

    int[] randomTicketRange = levelToSpriteRange[curLevel];
    int randIndex = UnityEngine.Random.Range(randomTicketRange[0], randomTicketRange[1]);

    CurSprite = possibleTickets[randIndex].Sprite;
    Profit = possibleTickets[randIndex].Profit;
    curIngredients = new List<string>(possibleTickets[randIndex].Ingredients);
  }
  public bool CheckIngredient(string ingredient)
  {
    int index = curIngredients.IndexOf(ingredient);
    if (index < 0)
      return false;

    curIngredients.RemoveAt(index);
    if (curIngredients.Count == 0)
    {
      HasWon = true;
    }
    return true;
  }
}