using UnityEngine.UI;

public class TableView : GameView
{
    public Text tokenCountText;
    public Text platerNameText;
    public Text sessionIdText;
    public PlaceBetView PlaceBetView;
    public BetResultsView BetResultsView;
    public Player player;
    public string gameSessionId;

    public override void Start()
    {
        base.Start();
    }

    public override void UpdateView()
    {
        if (player != null)
        {
            platerNameText.text = player.name;
            tokenCountText.text = player.tokenCount;
        }
        if (gameSessionId != null)
        {
            sessionIdText.text = gameSessionId;
        }
    }

    public void ShowBets() {
        BetResultsView.SetVisible(false);
        PlaceBetView.SetVisible(true);
    }

    public void ShowBetResponse(BetResponse response)
    {
        BetResultsView.betResponse = response;
        BetResultsView.UpdateView();
        BetResultsView.SetVisible(true);
        PlaceBetView.SetVisible(false);
    }
}
