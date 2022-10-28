using System.Diagnostics;
using System.Transactions;
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
        string[] suits = { "diamonds", "spades", "clubs", "hearts" };
        int count = 2;
        card[] DealerCards = new card[10];
        card[] PlayerCards = new card[10];

        // Amount player bets - Will be matched by dealer
        int playerBet = 0;

        double playerstotalwinings= 0;

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
        public void restart(string reason, double mut,bool win,bool draw) {
            button1.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            button5.Hide();
            dcard2.Show();
            button6.Show();
            label2.Show();
            button6.BringToFront();
            label2.BringToFront();
            Properties.Settings.Default.playerslastbet = playerBet;
            Properties.Settings.Default.playerstotalwins = Math.Round(playerstotalwinings + (playerBet * mut));
            Properties.Settings.Default.Save();
            label2.Text = "total winnings: £" + playerstotalwinings.ToString();

            if (win)
            {
                label2.Text = reason + "\n you have won £" + (playerBet * mut).ToString();


            }
            else if(draw){
                label2.Text = reason + "\n you got your money back" ;

            }
            else {
                label2.Text = reason + "\n dealer has won £" + (playerBet * mut).ToString();


            }



        }
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

                restart("you draw",0,false,true);

            }
            else if (playersTotal == 21)
            {
                restart("you win",3.5,true,false);

            }
            else if (dealerTotal == 21) {

                restart("you lost", 3.5, true, false);
            }
        }
        public string getcardfile(int value, string suit) {
            Random rand = new Random();

            if (value == 11 ||value == 1)
            {

                return AppDomain.CurrentDomain.BaseDirectory + "res\\ace_of_" + suit + ".png";


            }
            else if (value == 10)
            {
                String[] ops = { "10", "jack", "queen", "king" };
                return AppDomain.CurrentDomain.BaseDirectory + "res\\" + ops[rand.Next(3)] + "_of_" + suit + ".png";


            }
            else {

                return AppDomain.CurrentDomain.BaseDirectory + "res\\" + value + "_of_" + suit + ".png";

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


                restart("gone bust", 2, false, false);
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
            playerBet = Properties.Settings.Default.playerslastbet;
            playerstotalwinings = Properties.Settings.Default.playerstotalwins;
            label1.Text = "£" + playerBet.ToString();

            label3.Text = "total winnings: £" + playerstotalwinings.ToString();
            button3.Hide();
            button4.Hide();
            button5.Hide();
            card1.Hide();
            card2.Hide();
            dcard1.Hide();
            dcard2.Hide();
            card3.Hide();
            card4.Hide();
            card5.Hide();
            button6.Hide();
            label2.Hide();

            // initialize cards
            DealerCards[0] = getCards();
            DealerCards[1] = getCards();

            PlayerCards[0] = getCards();
            PlayerCards[1] = getCards();

            // Place bet
            betState();

        }

        // Hit
        private void button1_Click(object sender, EventArgs e)
        {
           count++;

            PlayerCards[count - 1] = getCards();
            if (count == 3)
            {
                card3.BackgroundImage = Image.FromFile(getcardfile(PlayerCards[2].value, PlayerCards[2].suit));
                card3.Show();
            }
            else if (count == 4) {

                card4.BackgroundImage = Image.FromFile(getcardfile(PlayerCards[3].value, PlayerCards[3].suit));
                card4.Show();
            }
            else if (count == 5)
            {

                card5.BackgroundImage = Image.FromFile(getcardfile(PlayerCards[4].value, PlayerCards[4].suit));
                card5.Show();
            }

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
                restart("you win", 2, true, false);

            }

            else if (playersTotal > 21)
            {
                // Dealer wins
                restart("gone bust", 2, false, false);

            }

            else if (dealerTotal > playersTotal)
            {
                // Dealer wins
                restart("you lost", 2, false, false);

            }

            else
            {
                restart("you win", 2, true, false);
            }


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
            card1.BackgroundImage = Image.FromFile(getcardfile(PlayerCards[0].value, PlayerCards[0].suit));
            dcard1.BackgroundImage = Image.FromFile(getcardfile(DealerCards[0].value, DealerCards[0].suit));
            card2.BackgroundImage = Image.FromFile(getcardfile(PlayerCards[1].value, PlayerCards[1].suit));
            dcard2.BackgroundImage = Image.FromFile(getcardfile(DealerCards[1].value, DealerCards[1].suit));
            card1.Show();
            dcard1.Show();
            card2.Show();
            button1.Show();
            button2.Show();

            button3.Hide();
            button4.Hide();
            button5.Hide();
            winstate();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath); // to start new instance of application
            this.Close(); //to turn off current app
        }
    }
}