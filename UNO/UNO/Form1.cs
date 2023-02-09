namespace UNO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOrdered_Click(object sender, EventArgs e)
        {
            lbxCards.Items.Clear();
            foreach (UNOCard card in UNOCard.GenerateOrderedDeck())
            {
                lbxCards.Items.Add(card.GetImage());
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            lbxCards.Items.Clear();
            List<UNOCard> list = UNOCard.GenerateUnorderedDeck();
            foreach (UNOCard card in list)
            {
                lbxCards.Items.Add(card.GetImage());
            }
        }
    }
}