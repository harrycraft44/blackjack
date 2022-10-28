namespace blackjack
{
    public partial class Form1 : Form
    {
        struct card
        {
            public int value;
            public string suit;
        }

        int[] cardValue = { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        string[] suits = { "Diamond", "Spades", "Clubs", "Hearts" };

        public void getCards()
        {
            card card = new card();
            Random rand = new Random();

            card.value = cardValue[rand.Next(13)];
            card.suit = suits[rand.Next(4)];

            label1.Text = card.value.ToString();
            label2.Text = card.suit;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getCards();
        }
    }
}