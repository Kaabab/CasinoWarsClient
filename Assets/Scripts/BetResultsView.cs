using UnityEngine.UI;

public class BetResultsView : GameView
{
    public Text casinoCardText;
    public Text playerCardText;
    public Text resultText;

    public BetResponse betResponse;
    
    public override void UpdateView()
    {
        base.UpdateView();
        casinoCardText.text = betResponse.casinoCard.name;
        playerCardText.text = betResponse.playerCard.name;
        resultText.text = betResponse.result;
    }

}
