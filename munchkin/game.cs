using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace munchkin
{
    public partial class game : Form
    {
        Random rnd = new Random();
        int cardid = 0;
        enum role
        {
            analyst, developer, tester
        }
        enum domitary
        {
            du, pushkina, kp
        }
        enum slot
        {
            head, body, shoes, not_required, hand
        }
        enum table_state
        {
            battle, first_player_move, second_player_move, punishment, waiting_for_action
        }
        abstract class Card
        {
            public int id;
            public string name;
            
        }
        public void CardOpen(int id)
        {
            cardform cardform = new cardform();
            cardform.BackgroundImage = Image.FromFile("c:/Users/kalas/source/repos/munchkin/munchkin/munchkin/Properties/cards/" + id + ".bmp");
            cardform.Show();
        }
        struct monster_bonus
        {
            public int role_value;
            public role bonus_role;
            public int domitary_value;
            public domitary bonus_domitary;
        }
        class item : Card
        {
            public int power;
            public bool big_item;
            public slot slot;
            public bool usable;
            public item(int id, string name, int power, bool big_item, slot slot,bool usable)
            {
                this.id = id;
                this.name = name;
                this.power = power;
                this.big_item = big_item;
                this.slot = slot;
                this.usable = usable;
            }

            public void Drop(slot slot, List<Card> droppedcards, Munchkin player)
            {
                player.items[slot] = null;

            }
        }
        class Munchkin
        {
            public int id;
            public string name;
            public int level;
            public Dictionary<slot, item> items;
            public role role;
            public domitary domitary;

        }
        class Monster : Card
        {
            public int level;
            public monster_bonus bonus;
            public int treasure;
            public Monster(int id, string name, int level, monster_bonus bonus,int treasure)
            {
                this.id= id;
                this.name = name;
                this.level = level;
                this.bonus = bonus;
                this.treasure = treasure;
            }
        }
        public int a = 0;
        table_state state = new table_state();
        
        
        public game()
        {
            InitializeComponent();
            state = table_state.first_player_move;
            if (state == table_state.first_player_move)
            {
                button4.Enabled = true;
                

            }
        }
        List<Card> player1hand = new List<Card>();
        List<Card> player2hand = new List<Card>();

        private void timer1_Tick(object sender, EventArgs e)
        {
            a = a + 1;
            if (a < 60)
            {
                label3.Text = (a % 60 > 9 ? "00:" : "00:0") + Convert.ToString(a);
            }
            else if (a < 3600)
            {
                label3.Text= (a / 60 > 9 ? "" : "0") +Convert.ToString(a/60)+":"+ (a % 60 > 9 ? "" : "0") + Convert.ToString(a%60);
            }
            else
            {
                label3.Text = (a / 3600 > 9 ? "" : "0") + Convert.ToString(a / 3600) + ":" +  (a % 3600/60 > 9 ? "" : "0")+Convert.ToString(a%3600 /60)+":" + (a% 60 > 9 ? "" : "0") + (a%60);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            character_form charform = new character_form();
            charform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            character_form charform = new character_form();
            charform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            pictureBox7.Enabled = true;
            pictureBox7.Visible = true;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            cardid = rnd.Next(0,22);
            CardOpen(cardid);
            //DoorBroken(cardid);
            pictureBox7.Enabled = false;
            pictureBox7.Visible = false;
            database db = new database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand("SELECT * from 'doors' WHERE 'id' = cardid");
            
        }
    }
}
