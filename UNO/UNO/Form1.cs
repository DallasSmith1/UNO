namespace UNO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        // testing the creation of an ordered UNO deck
        private void btnOrdered_Click(object sender, EventArgs e)
        {
            lbxCards.Items.Clear();
            foreach (UNOCard card in UNOCard.GenerateOrderedDeck())
            {
                if (card.IsSpecial())
                {
                    lbxCards.Items.Add(card.GetColour() + " " + card.GetValue() + " SPECIAL");
                }
                else
                {
                    lbxCards.Items.Add(card.GetColour() + " " + card.GetValue());
                }
            }
        }


        // testing the creation of an unordered UNO deck
        private void btnRandom_Click(object sender, EventArgs e)
        {
            lbxCards.Items.Clear();
            List<UNOCard> list = UNOCard.GenerateUnorderedDeck();
            foreach (UNOCard card in list)
            {
                if (card.IsSpecial())
                {
                    lbxCards.Items.Add(card.GetColour() + " " + card.GetValue()+" SPECIAL");
                }
                else
                {
                    lbxCards.Items.Add(card.GetColour() + " " + card.GetValue());
                }
            }
        }
    }
}