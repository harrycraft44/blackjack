using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace blackjack
{
    public partial class Form1 : Form
    {
        // Card data
        public struct card
        {
            public int value;
            public string suit;
        }

        int[] cardValue = { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        string[] suits = { "Diamond", "Spades", "Clubs", "Hearts" };

        card[] DealerCards = new card[10];
        card[] PlayerCards = new card[10];

        // Amount player bets - Will be matched by dealer
        int playerBet = 0;

        // Assign a card to the player or dealer
        public card getCards()
        {
            card card = new card();
            Random rand = new Random();

            card.value = cardValue[rand.Next(13)];
            card.suit = suits[rand.Next(4)];


            return card;
        }

        // Checks if the dealer or player has won
        public void winstate() {
            int playersTotal = 0 ;
            int dealerTotal = 0;
            foreach (var cards in PlayerCards)
            {
                playersTotal += cards.value;
            }
            foreach (var cards in DealerCards)
            {
                dealerTotal += cards.value;
            }
            if (playersTotal == 21 && dealerTotal == 21)
            {

                string message = "you draw";
                string title = "draw";
                MessageBox.Show(message, title);
            }
            else if (playersTotal == 21)
            {
                string message = "you win";
                string title = "win";
                MessageBox.Show(message, title);
            }
            else if (dealerTotal == 21) {

                string message = "you lose";
                string title = "lose";
                MessageBox.Show(message, title);
            }
        }

        // Check if the player goes bust when they hit
        public void hitwinstate() {

            int playersTotal = 0;

            foreach (var cards in PlayerCards)
            {
                playersTotal += cards.value;
            }
            if (playersTotal > 21) {


                string message = "you bust";
                string title = "bust";
                MessageBox.Show(message, title);
            }
        }

        // Switch UI to bet
        public void betState()
        {
            button1.Hide();
            button2.Hide();

            button3.Show();
            button4.Show();
            button5.Show();
        }

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "£" + playerBet.ToString();
            button3.Hide();
            button4.Hide();
            button5.Hide();

            // initialize cards
            DealerCards[0] = getCards();
            DealerCards[1] = getCards();

            PlayerCards[0] = getCards();
            PlayerCards[1] = getCards();

            // Place bet
           

            winstate();
        }

        // Hit
        private void button1_Click(object sender, EventArgs e)
        {
            PlayerCards[PlayerCards.Length -1] = getCards();
            hitwinstate();
        }

        // Stand
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Hide();
            int playersTotal = 0;
            int dealerTotal = 0;

            foreach (var cards in PlayerCards)
            {
                playersTotal += cards.value;
            }

            foreach (var cards in DealerCards)
            {
                dealerTotal += cards.value;
            }

            if (dealerTotal > 21)
            {
                // Player wins
            }

            else if (playersTotal > 21)
            {
                // Dealer wins
            }

            else if (dealerTotal > playersTotal)
            {
                // Dealer wins
            }

            else
            {
                // Player wins
            }

            betState();
        }

        // Higher bet
        private void button3_Click(object sender, EventArgs e)
        {
            playerBet += 10;
            label1.Text = "£" + playerBet.ToString();
        }

        // Lower bet
        private void button4_Click(object sender, EventArgs e)
        {
            if (playerBet <= 0)
            {
                return;
            }

            playerBet -= 10;
            label1.Text = "£" + playerBet.ToString();
        }

        // Confirm bet
        private void button5_Click(object sender, EventArgs e)
        {
            button1.Show();
            button2.Show();

            button3.Hide();
            button4.Hide();
            button5.Hide();
        }
    }
}