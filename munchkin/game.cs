using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace munchkin
{
    public partial class game : Form
    {
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
            public Munchkin equipped;
            public item(int id, string name, int power, bool big_item, slot slot, Munchkin equipped)
            {
                this.id = id;
                this.name = name;
                this.power = power;
                this.big_item = big_item;
                this.slot = slot;
                this.equipped = equipped;
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
            public Monster(int id, string name, int level, monster_bonus bonus)
            {

            }
        }
        public int a = 0;
        public game()
        {
            InitializeComponent();
        }

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
    }
}
