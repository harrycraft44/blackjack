namespace blackjack
{
    public partial class Form1 : Form
    {

        struct card
        {
            string name;
            string suit;
            int value; // Value of the card number 2-11

        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Random rnd = new Random();
            int card = rnd.Next(52);

        }
    }
}