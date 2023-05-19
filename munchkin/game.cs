using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace munchkin
{
    public partial class game : Form
    {
        Random rnd = new Random();
        int cardid = 0;
        enum role
        {
            analyst, developer, tester, none
        }
        enum domitary
        {
            du, pushkina, kp, none
        }
        public enum slot
        {
            head, body, shoes, not_required, left_hand, right_hand
        }
        public enum table_state
        {
            battle, first_player_move, second_player_move, punishment, waiting_for_action
        }
        abstract class Card
        {
            public int id;
            public string name;

        }
        public List<Door> doors = new List<Door>();
        Door id1 = new Door(1, "curse", 0, "", 0, "", 0, 0, "lvl");
        Door id2 = new Door(2, "monster", 16, "", 0, "", 0, 4, "lvl");
        Door id3 = new Door(3, "monster", 1, "", 0, "analyst", 5, 1, "");
        Door id4 = new Door(4, "role", 0, "", 0, "analyst", 0, 0, "");
        Door id5 = new Door(5, "monster", 6, "", 0, "", 0, 2, "death");
        Door id6 = new Door(6, "monster", 4, "du", 5, "", 0, 0, "domitary");
        Door id7 = new Door(7, "role", 0, "", 0, "developer", 0, 0, "");
        Door id8 = new Door(8, "domitary", 0, "du", 0, "", 0, 0, "");
        Door id9 = new Door(9, "monster", 16, "", 0, "", 0, 4, "death");
        Door id10 = new Door(10, "monster", 14, "", 0, "tester", 3, 3, "role");
        Door id11 = new Door(11, "monster", 25, "", 0, "", 0, 0, "death");
        Door id12 = new Door(12, "monster", 8, "", 0, "", 0, 2, "");
        Door id13 = new Door(13, "monster", 2, "du", 3, "", 0, 1, "shoes");
        Door id14 = new Door(14, "monster", 18, "", 0, "analyst", 5, 4, "role");
        Door id15 = new Door(15, "curse", 0, "", 0, "", 0, 0, "lvl");
        Door id16 = new Door(16, "domitary", 0, "pushkina", 0, "", 0, 0, "");
        Door id17 = new Door(17, "monster", 12, "", 0, "", 0, 3, "2lvl");
        Door id18 = new Door(18, "monster", 12, "", 0, "analyst", 4, 3, "lvl");
        Door id19 = new Door(19, "role", 0, "", 0, "tester", 0, 0, "");
        Door id20 = new Door(20, "curse", 0, "", 0, "", 0, 0, "shoes");
        Door id21 = new Door(21, "monster", 10, "", 0, "tester", 0, 0, "2lvl");
        Door id22 = new Door(22, "curse", 0, "", 0, "", 0, 0, "arm");
        public List<Treasure> treasures = new List<Treasure>();
        Treasure id23 = new Treasure(23, false, false, slot.body, 2);
        Treasure id24 = new Treasure(24, false, false, slot.head, 1);
        Treasure id25 = new Treasure(25, false, false, slot.left_hand, 2);
        Treasure id26 = new Treasure(26, false, true, slot.body, 2);
        Treasure id27 = new Treasure(27, false, true, slot.body, 4);
        Treasure id28 = new Treasure(28, false, false, slot.right_hand, 3);
        Treasure id29 = new Treasure(29, false, false, slot.left_hand, 6);
        Treasure id30 = new Treasure(30, true, false, slot.body, 2);
        Treasure id31 = new Treasure(31, true, false, slot.body, 2);
        Treasure id32 = new Treasure(32, true, false, slot.body, 2);
        Treasure id33 = new Treasure(33, true, false, slot.body, 2);
        Treasure id34 = new Treasure(34, false, false, slot.right_hand, 4);
        Treasure id35 = new Treasure(35, false, false, slot.body, 2);
        Treasure id36 = new Treasure(36, false, false, slot.head, 3);
        Treasure id37 = new Treasure(37, false, true, slot.body, 3);
        Treasure id38 = new Treasure(38, false, false, slot.head, 2);
        Treasure id39 = new Treasure(39, false, false, slot.head, 3);
        Treasure id40 = new Treasure(40, false, false, slot.left_hand, 1);
        Treasure id41 = new Treasure(41, false, false, slot.shoes, 1);
        public static table_state deskstate = new table_state();

        public class Door
        {
            public int id;
            public string cardclass;
            public int power;
            public string domitary;
            public int domitarybonus;
            public string role;
            public int rolebonus;
            public int treasures;
            public string punishment;
            public Door(int id, string cardclass, int power, string domitary, int domitarybonus, string role, int rolebonus, int treasures, string punishment)
            {
                this.id = id;
                this.cardclass = cardclass;
                this.power = power;
                this.domitary = domitary;
                this.domitarybonus = domitarybonus;
                this.role = role;
                this.rolebonus = rolebonus;
                this.treasures = treasures;
                this.punishment = punishment;
            }
        }
        public class Treasure
        {
            public int id;
            public bool lvlup;
            public bool usable;
            public slot slot;
            public int bonus;
            public Treasure(int id, bool lvlup, bool usable, slot slot, int bonus)
            {
                this.id = id;
                this.lvlup = lvlup;
                this.usable = usable;
                this.slot = slot;
                this.bonus = bonus;
            }
        }
        static Dictionary<slot,int> playeritems = new Dictionary<slot,int>();
        Munchkin player = new Munchkin(1, 1,playeritems,role.none,domitary.none);
        int selectedcard = 0;
        public void Curse(string curse)
        {

            if (curse == "lvl" & player.level > 1 & player.role!=role.analyst)
            {               
                player.level = player.level - 1;
            }
            else if (curse == "2lvl" & player.level>2 & player.role != role.analyst)
            {
                player.level = player.level - 2;
            }
            else if (curse == "death")
            {
                MessageBox.Show("Ты проиграл!");
                Close();
            }
            else if (curse == "arm")
            {
                player.items.Remove(slot.left_hand);
                player.items.Add(slot.left_hand, 0);
                player.items.Remove(slot.right_hand);
                player.items.Add(slot.right_hand, 0);
            }
            else if (curse == "shoes")
            {
                player.items.Remove(slot.shoes);
                player.items.Add(slot.shoes,0);
            }
            else if (curse == "domitary")
            {
                player.domitary = domitary.none;
            }
            else if (curse == "role")
            {
                player.role = role.none;
            }
        }
        int battlepower = 0;
        int treasurernd = 0;
        public bool endbattle = false;
        public void DropCard(int id)
        {

        }
        public delegate void carddelegate();
        //int index = 0;
        public int SelectCard(int id)
        {
           foreach (var card in doors)
                {
                    if (card.id == id)
                {
                    selectedcard = id; break;
                }
                
                }
           return selectedcard;
        }
        public void PlayCard(int id)
        {
            doors.Add(id1);
            doors.Add(id2);
            doors.Add(id3);
            doors.Add(id4);
            doors.Add(id5);
            doors.Add(id6);
            doors.Add(id7);
            doors.Add(id8);
            doors.Add(id9);
            doors.Add(id10);
            doors.Add(id11);
            doors.Add(id12);
            doors.Add(id13);
            doors.Add(id14);
            doors.Add(id15);
            doors.Add(id16);
            doors.Add(id17);
            doors.Add(id18);
            doors.Add(id19);
            doors.Add(id20);
            doors.Add(id21);
            doors.Add(id22);
            foreach (var card in doors)
            {
                if (card.id == id)
                {
                    if (card.cardclass == "monster")
                    {
                        Battle(card);
                        DropCard(id);
                    }
                    else if (card.cardclass == "role")
                    {
                        if (card.role == "analyst")
                        {
                            player.role = role.analyst;
                        }
                        if (card.role == "developer")
                        {
                            player.role = role.developer;
                        }
                        if (card.role =="tester")
                        {
                            player.role = role.tester;
                        }
                        DropCard(id);
                    }
                    else if (card.cardclass == "domitary")
                    {
                        if (card.domitary == "du")
                        {
                            player.domitary = domitary.du;
                        }
                        if (card.domitary == "pushka")
                        {
                            player.domitary = domitary.pushkina;
                        }
                        DropCard(id);
                    }
                    else if (card.cardclass == "curse")
                    {
                        DropCard(id);
                    }
                }
                
                
            }
            foreach (var card in treasures)
            {
                if (cardid == card.id)
                {
                    if (card.lvlup)
                    {
                        DropCard(id);
                        player.level++;
                    }
                    else if (card.usable & deskstate == table_state.battle)
                    {
                        battlepower = battlepower + card.bonus;
                        DropCard(id);
                    }
                    else if (card.usable)
                    {
                        DropCard(id);
                    }
                    else if (!card.usable & !card.lvlup )
                    {
                        player.items.Remove(card.slot);
                        player.items.Add(card.slot, card.bonus);
                        DropCard(id);
                    }
                }
            }
        }
        public void Battle(Door monster)
        {
            
            {
                //while (endbattle == false)
                //{
                    foreach (var item in player.items)
                    {
                        battlepower = battlepower + item.Value;
                    }
                    foreach (var card in player1hand)
                    {
                        cardform cardform = new cardform();
                        cardform.BackgroundImage = Image.FromFile("c:/Users/kalas/source/repos/munchkin/munchkin/munchkin/Properties/cards/" + cardid + ".bmp");
                        cardform.Show();
                    }
                battlepower = battlepower + player.level;
                //}
                if (battlepower > monster.power)
                {
                    for (int treasures = 0; treasures < monster.treasures; treasures++)
                    {
                        
                        treasurernd = rnd.Next(22, 42);
                        player1hand.Add( treasurernd);
                        
                    }
                    player.level++;
                    MessageBox.Show("win");
                }
                else
                {
                    Curse(monster.punishment);
                    MessageBox.Show(monster.punishment);
                }
                deskstate = table_state.first_player_move;
                button4.Enabled = true;
                battlepower = 0;
            }

        }

        int max = 0;
        public void CardOpen(int cardid)
        {
            Console.WriteLine("good");
            doors.Add(id1);
            doors.Add(id2);
            doors.Add(id3);
            doors.Add(id4);
            doors.Add(id5);
            doors.Add(id6);
            doors.Add(id7);
            doors.Add(id8);
            doors.Add(id9);
            doors.Add(id10);
            doors.Add(id11);
            doors.Add(id12);
            doors.Add(id13);
            doors.Add(id14);
            doors.Add(id15);
            doors.Add(id16);
            doors.Add(id17);
            doors.Add(id18);
            doors.Add(id19);
            doors.Add(id20);
            doors.Add(id21);
            doors.Add(id22);
            cardform cardform = new cardform();
            cardform.BackgroundImage = Image.FromFile("c:/Users/kalas/source/repos/munchkin/munchkin/munchkin/Properties/cards/" + cardid + ".bmp");
            cardform.Show();
            foreach (var a in doors)
            {
                if (state == table_state.first_player_move & a.id == cardid & a.cardclass == "monster")
                {
                    Battle(a);
                    button3.Enabled = true;
                    break;
                }
                else if (state == table_state.second_player_move & a.id == cardid)
                {
                    
                    player1hand.Add(cardid);
                    button3.Enabled = true;
                    break;
                }
                else if (state == table_state.first_player_move & a.id == cardid & a.cardclass == "curse")
                {
                    Curse(a.punishment);
                    button3.Enabled = true;
                    break;
                }
                else if (state == table_state.first_player_move & a.id == cardid & (a.cardclass == "role" || a.cardclass == "domitary"))
                {
                    player1hand.Add( cardid);
                    state = table_state.second_player_move;
                    button4.Enabled = true;
                    break;
                }
            }
            
            doors.Clear();
            

        }
        
        
        class Munchkin
        {
            public int id;
            public int level;
            public Dictionary<slot,int> items;
            public role role;
            public domitary domitary;
            public Munchkin(int  id,int level,Dictionary<slot,int> items,role role,domitary domitary )
            {
                this.id = id;
                this.level = level;
                this.items = items;
                this.role = role;
                this.domitary = domitary;
            }
        }
        
        public int a = 0;
        static table_state state = new table_state();
        
        
        public game()
        {
            InitializeComponent();
            state = table_state.first_player_move;
            if (state == table_state.first_player_move)
            {
                button4.Enabled = true;
                

            }
        }
        List<int> player1hand = new List<int>();

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
        
        public static void StartGame()
        {
            
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {

            cardid = rnd.Next(1,22);
            
            CardOpen(cardid);
            SelectCard(cardid);

            //DoorBroken(cardid);
            pictureBox7.Enabled = false;
            pictureBox7.Visible = false;            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StartGame();
            button6.Visible = false;
            button6.Enabled = false;
        }
        int cardcount = 0;
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (player1hand != null)
            {
                foreach (var a in player1hand)
                {
                    cardcount++;
                    
                    cardform cardform = new cardform();
                    cardform.BackgroundImage = Image.FromFile("c:/Users/kalas/source/repos/munchkin/munchkin/munchkin/Properties/cards/" + a + ".bmp");
                    cardform.Show();
                }
                label4.Text = "x" + cardcount.ToString();
                cardcount = 0;
            }
        }
        int index = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            deskstate = table_state.first_player_move;
            button4.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            battlepower = battlepower + 10;
            MessageBox.Show("ты получил 10 боевой силы");
            label6.Text = battlepower.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            battlepower = battlepower - 10;
            MessageBox.Show("ты потерял 10 боевой силы");
            label6.Text = battlepower.ToString();
        }
    }
}
