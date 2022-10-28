namespace blackjack
{
    public partial class Form1 : Form
    {
        public struct card
        {
            public int value;
            public string suit;
        }

        int[] cardValue = { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        string[] suits = { "Diamond", "Spades", "Clubs", "Hearts" };

        card[] DealerCards = new card[10];
        card[] PlayerCards = new card[10];

        public card getCards()
        {
            card card = new card();
            Random rand = new Random();

            card.value = cardValue[rand.Next(13)];
            card.suit = suits[rand.Next(4)];

            label1.Text = card.value.ToString();
            label2.Text = card.suit;

            return card;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // add dealer's cards 
            DealerCards[0] = getCards();
            DealerCards[1] = getCards();

            label1.Text = DealerCards[0].value.ToString()+ DealerCards[0].suit;
            label2.Text = DealerCards[1].value.ToString() + DealerCards[1].suit;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}