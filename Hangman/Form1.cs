using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tiedup
{
    public partial class Form1 : Form
    {
        #region variables
        private dictionary dict = new dictionary();
        private Button[] btn = new Button[26];
        private Panel[] pnl = new Panel[5];
        private struct word
        {
            public string letter;
            public bool got;
        }
        private word[] wrd;
        private TransparentPictureBox[] ltrhldr;
        private ComboBox[] drpdwnlst = new ComboBox[2];
        private int wrngcnt = 0;
        private bool hasletter = false;
        private PictureBox[] bttn = new PictureBox[5];
        private Timer[] tmr = new Timer[2];
        private int y = 36;
        #endregion

        #region methods
        public Form1()
        {
            Crtobj();
            InitializeComponent();
        }
        
        private void Crtobj() // create object
        {
            #region panel (A.K.A. game fields)
            for (int i = 0; i < pnl.Length; i++)
            {
                pnl[i] = new Panel();
                pnl[i].Name = "panel";
                pnl[i].TabIndex = 0;
                if (i == 4) // field after game
                {
                    pnl[i].Location = new Point(0, 0);
                    pnl[i].Size = new Size(595, 310);
                    pnl[i].BackgroundImage = Properties.Resources.playagain;
                    this.Controls.Add(pnl[i]);
                }
                else if (i == 3) // letter holder
                {
                    pnl[i].Location = new Point(300, 113);
                    pnl[i].Size = new Size(290, 185);
                    pnl[i].BackColor = Color.Transparent;
                    pnl[2].Controls.Add(pnl[i]);
                }
                else if (i == 2) // game panel
                {
                    pnl[i].Location = new Point(0, 0);
                    pnl[i].Size = new Size(595, 310);
                    pnl[i].BackgroundImage = Properties.Resources._11trials;
                    pnl[i].BorderStyle = BorderStyle.None;
                    this.Controls.Add(pnl[i]);
                }
                else if (i == 1) // choose panel
                {
                    pnl[i].Location = new Point(0, 0);
                    pnl[i].Size = new Size(595, 310);
                    pnl[i].BackgroundImage = Properties.Resources.selectionbackground;
                    pnl[i].BorderStyle = BorderStyle.None;
                    this.Controls.Add(pnl[i]);
                }
                else if (i == 0)
                {
                    pnl[i].Location = new Point(0, 0);
                    pnl[i].Size = new Size(595, 310);
                    pnl[i].BackgroundImage = Properties.Resources.mainbackground;
                    pnl[i].BorderStyle = BorderStyle.None;
                    this.Controls.Add(pnl[i]);
                }
            }
            #endregion
            
            #region create drop down list
            for (int i = 0; i < drpdwnlst.Length; i++)
            {
                drpdwnlst[i] = new ComboBox();
                drpdwnlst[i].FormattingEnabled = true;
                drpdwnlst[i].Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular);
                drpdwnlst[0].Size = new Size(110, 100);
                drpdwnlst[0].Location = new Point(150, 100);
                if (i == 1)
                {
                    drpdwnlst[1].Size = new Size(150, 100);
                    drpdwnlst[1].Location = new Point(130 + (i * 160), 100);
                }
                pnl[1].Controls.Add(drpdwnlst[i]);
            }
            // load items for drpdwnlst[0]
            drpdwnlst[0].Items.AddRange(new object[] {
            "any",
            "common",
            "rare"
            });

            drpdwnlst[0].SelectedItem = "any";

            // load items for drpdwnlst[1]
            drpdwnlst[1].Items.AddRange(new object[] {
            "english",
            "adjective",
            "preposition",
            "pronoun",
            "adverb",
            "noun",
            "compound",
            "verb",
            "technology",
            "computer",
            "engineering",
            "automobile",
            "inventions",
            "science",
            "biology",
            "chemistry",
            "physics",
            "earth_science",
            "medical_health",
            "other_branch",
            "math",
            "basic_math_and_algebra",
            "geometry",
            "trigonometry",
            "statistics_and_probability",
            "logic",
            "algorithm",
            "descrete_math",
            "calculus",
            "business",
            "accountancy",
            "management",
            "finance",
            "marketing",
            "jobs",
            "others",
            "house_office_school",
            "buildings_stuffs",
            "outside_building",
            "sports",
            "time_calendar",
            "zodiac_signs",
            "world"
            });

            drpdwnlst[1].SelectedItem = "english";
            #endregion

            #region create picture as button
            for (int i = 0; i < bttn.Length; i++)
            {
                bttn[i] = new PictureBox();
                bttn[i].Size = new Size(160, 40);
                if (i == 0)
                {
                    bttn[i].BackgroundImage = Properties.Resources.startbutton;
                    bttn[i].Location = new Point(420,180);
                    bttn[i].Click += new EventHandler(start_click);
                    bttn[i].MouseHover += new EventHandler(start_hover);
                    bttn[i].MouseLeave += new EventHandler(start_out);
                    pnl[0].Controls.Add(bttn[i]);
                }
                else if (i == 1)
                {
                    bttn[i].BackgroundImage = Properties.Resources.exitbutton;
                    bttn[i].Location = new Point(420, 240);
                    bttn[i].Click += new EventHandler(End_click);
                    bttn[i].MouseHover += new EventHandler(exit_hover);
                    bttn[i].MouseLeave += new EventHandler(exit_out);
                    pnl[0].Controls.Add(bttn[i]);
                }
                else if (i == 2)
                {
                    bttn[i].BackgroundImage = Properties.Resources.playbutton;
                    bttn[i].Location = new Point(200, 240);
                    bttn[i].Click += new EventHandler(srtschs_click);
                    bttn[i].MouseHover += new EventHandler(play_hover);
                    bttn[i].MouseLeave += new EventHandler(play_out);
                    pnl[1].Controls.Add(bttn[i]);
                }
                else if (i == 3)
                {
                    bttn[i].BackgroundImage = Properties.Resources.play;
                    bttn[i].Location = new Point(60, 150);
                    bttn[i].Click += new EventHandler(plygn_click);
                    bttn[i].MouseHover += new EventHandler(playgn_hover);
                    bttn[i].MouseLeave += new EventHandler(playgn_out);
                    pnl[4].Controls.Add(bttn[i]);
                }
                else if (i == 4)
                {
                    bttn[i].BackgroundImage = Properties.Resources.quit;
                    bttn[i].Location = new Point(230, 150);
                    bttn[i].Click += new EventHandler(End_click);
                    bttn[i].MouseHover += new EventHandler(quit_hover);
                    bttn[i].MouseLeave += new EventHandler(quit_out);
                    pnl[4].Controls.Add(bttn[i]);
                }
            }
            #endregion

            #region create timer
            for (int i = 0; i < tmr.Length; i++)
            {
                tmr[i] = new Timer();
                tmr[i].Stop();
                tmr[i].Enabled = false;
                switch (i)
                {
                    case 0: tmr[0].Tick += new EventHandler(timerbeforegameend);
                        break;
                    case 1: tmr[1].Tick += new EventHandler(timeranimateltrhldr);
                        break;
                }
            }
            #endregion
        }
        
        private void checkletter(string letter) // check chosen letter
        {
            for (int i = 0; i < wrd.Length; i++)
            {
                if (wrd[i].letter == letter)
                {
                    wrd[i].got = true;
                    hasletter = true;
                    setltrhldr(letter, i);
                }
                else
                {
                    if (i == wrd.Length - 1 && hasletter == false)
                    {
                        wrngcnt++;
                    }
                }
            }
            hasletter = false;
           
        }

        private void setltrhldr(string letter, int i)
        {
            switch (letter)
            {
                case "a": ltrhldr[i].BackgroundImage = Properties.Resources.ltrA;
                    break;
                case "b": ltrhldr[i].BackgroundImage = Properties.Resources.ltrB;
                    break;
                case "c": ltrhldr[i].BackgroundImage = Properties.Resources.ltrC;
                    break;
                case "d": ltrhldr[i].BackgroundImage = Properties.Resources.ltrD;
                    break;
                case "e": ltrhldr[i].BackgroundImage = Properties.Resources.ltrE;
                    break;
                case "f": ltrhldr[i].BackgroundImage = Properties.Resources.ltrF;
                    break;
                case "g": ltrhldr[i].BackgroundImage = Properties.Resources.ltrG;
                    break;
                case "h": ltrhldr[i].BackgroundImage = Properties.Resources.ltrH;
                    break;
                case "i": ltrhldr[i].BackgroundImage = Properties.Resources.ltrI;
                    break;
                case "j": ltrhldr[i].BackgroundImage = Properties.Resources.ltrJ;
                    break;
                case "k": ltrhldr[i].BackgroundImage = Properties.Resources.ltrK;
                    break;
                case "l": ltrhldr[i].BackgroundImage = Properties.Resources.ltrL;
                    break;
                case "m": ltrhldr[i].BackgroundImage = Properties.Resources.ltrM;
                    break;
                case "n": ltrhldr[i].BackgroundImage = Properties.Resources.ltrN;
                    break;
                case "o": ltrhldr[i].BackgroundImage = Properties.Resources.ltro;
                    break;
                case "p": ltrhldr[i].BackgroundImage = Properties.Resources.ltrP;
                    break;
                case "q": ltrhldr[i].BackgroundImage = Properties.Resources.ltrQ;
                    break;
                case "r": ltrhldr[i].BackgroundImage = Properties.Resources.ltrR;
                    break;
                case "s": ltrhldr[i].BackgroundImage = Properties.Resources.ltrS;
                    break;
                case "t": ltrhldr[i].BackgroundImage = Properties.Resources.ltrT;
                    break;
                case "u": ltrhldr[i].BackgroundImage = Properties.Resources.ltrU;
                    break;
                case "v": ltrhldr[i].BackgroundImage = Properties.Resources.ltrV;
                    break;
                case "w": ltrhldr[i].BackgroundImage = Properties.Resources.ltrW;
                    break;
                case "x": ltrhldr[i].BackgroundImage = Properties.Resources.ltrX;
                    break;
                case "y": ltrhldr[i].BackgroundImage = Properties.Resources.ltrY;
                    break;
                case "z": ltrhldr[i].BackgroundImage = Properties.Resources.ltrZ;
                    break;
            }
        }
        
        private void letterholderdiposer()
        {
            for (int i = 0; i < wrd.Length; i++)
            {
                ltrhldr[i].Dispose();
            }
        }

        private void checkifdn() // check if game will end or not
        {
            int count = 0;
            if (wrngcnt != 11)
            {
                for (int i = 0; i < wrd.Length; i++)
                {
                    if (wrd[i].got == true)
                    { count++; }
                }
                switch (wrngcnt)
                {
                    case 0: pnl[2].BackgroundImage = Properties.Resources._11trials;
                        break;
                    case 1: pnl[2].BackgroundImage = Properties.Resources._10trials;
                        break;
                    case 2: pnl[2].BackgroundImage = Properties.Resources._9trials;
                        break;
                    case 3: pnl[2].BackgroundImage = Properties.Resources._8trials;
                        break;
                    case 4: pnl[2].BackgroundImage = Properties.Resources._7trials;
                        break;
                    case 5: pnl[2].BackgroundImage = Properties.Resources._6trials;
                        break;
                    case 6: pnl[2].BackgroundImage = Properties.Resources._5trials;
                        break;
                    case 7: pnl[2].BackgroundImage = Properties.Resources._4trials;
                        break;
                    case 8: pnl[2].BackgroundImage = Properties.Resources._3trials;
                        break;
                    case 9: pnl[2].BackgroundImage = Properties.Resources._2trials;
                        break;
                    case 10: pnl[2].BackgroundImage = Properties.Resources._1trials;
                        break;
                }
                if (count == wrd.Length)
                {
                    tmr[0].Interval =1500; tmr[0].Enabled = true; tmr[0].Start();
                }
            }
            else
            {
                for (int i = 0; i < wrd.Length; i++)
                {
                    setltrhldr(wrd[i].letter, i);
                }
                pnl[2].BackgroundImage = Properties.Resources._0trials;
                tmr[0].Interval = 1500; tmr[0].Enabled = true; tmr[0].Start();
            }
        }

        public class TransparentPictureBox : Control
        {
            public TransparentPictureBox()
            {
                this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
                this.BackColor = Color.Transparent;
            }

            private Image _Image;

            public Image Image
            {
                get
                { return _Image; }

                set
                { _Image = value; }
            }
            private bool _autoscale = true;

            public bool AutoScale
            {
                get
                { return _autoscale; }

                set
                { _autoscale = value; }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (_Image != null) //keep from crashing if there is no image
                {
                    if (_autoscale)
                    {
                        //Auto Scale the image to fit in the text box
                        Rectangle rectangle = new Rectangle();
                        Size size = this.Image.Size;
                        float num = Math.Min((float)(((float)base.ClientRectangle.Width) / ((float)size.Width)), (float)(((float)base.ClientRectangle.Height) / ((float)size.Height)));
                        rectangle.Width = (int)(size.Width * num);
                        rectangle.Height = (int)(size.Height * num);
                        rectangle.X = (base.ClientRectangle.Width - rectangle.Width) / 2;
                        rectangle.Y = (base.ClientRectangle.Height - rectangle.Height) / 2;
                        e.Graphics.DrawImage(_Image, rectangle);
                    }
                    else
                    { e.Graphics.DrawImage(_Image, new Point(0, 0)); }
                }
            }
        }
        #endregion

        #region events
        private void timerbeforegameend(object sender, EventArgs e)
        {
            letterholderdiposer(); 
            pnl[0].Visible = false; pnl[1].Visible = false; pnl[2].Visible = false; pnl[4].Visible = true; wrngcnt = 0;
            tmr[0].Stop(); tmr[0].Enabled = false; tmr[1].Stop(); tmr[1].Enabled = false; 
        }

        private void timeranimateltrhldr(object sender, EventArgs e)
        {
            for (int i = 0; i < ltrhldr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    if (y == 36)
                    { y = 16; }
                    else { y = 36; }
                }
                else
                {
                    if (y == 16)
                    { y = 36; }
                    else { y = 16; }
                }
                if (ltrhldr.Length <= 5)
                {
                    ltrhldr[i].Location = new Point(250 + (i * 29), y);
                }
                else if (ltrhldr.Length <= 10)
                {
                    ltrhldr[i].Location = new Point(200 + (i * 29), y);
                }
                else if (ltrhldr.Length <= 15)
                {
                    ltrhldr[i].Location = new Point(120 + (i * 29), y);
                }
                else if (ltrhldr.Length <= 20)
                {
                    ltrhldr[i].Location = new Point(2 + (i * 29), y);
                }
            }
        }

        private void start_click(object sender, EventArgs e)
        {
            pnl[0].Visible = false; pnl[1].Visible = true; pnl[2].Visible = false; pnl[4].Visible = false; 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (pnl[2].Visible == true)
            {
                checkletter(e.KeyData.ToString().ToLower());
                checkifdn();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pnl[0].Visible = true; pnl[1].Visible = false; pnl[2].Visible = false;
        }

        private void End_click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void srtschs_click(object sender, EventArgs e)
        {
            wrd = null; ltrhldr = null;
            dict.getword(drpdwnlst[0].SelectedItem.ToString(),drpdwnlst[1].SelectedItem.ToString(), 1);
            wrd = new word[dict.words[0].Length];

            //create picture as letter holder
            ltrhldr = new TransparentPictureBox[wrd.Length];
            for (int i = 0; i < ltrhldr.Length; i++)
            {
                ltrhldr[i] = new TransparentPictureBox();
                ltrhldr[i].BackgroundImage = Properties.Resources.ltr_;
                ltrhldr[i].BackgroundImageLayout = ImageLayout.Stretch;
                ltrhldr[i].Size = new Size(29, 29);
                if(ltrhldr.Length <= 5)
                {
                    ltrhldr[i].Location = new Point(250 + (i * 29), 26);
                }
                else if (ltrhldr.Length <= 10)
                {
                    ltrhldr[i].Location = new Point(200 + (i * 29), 26);
                }
                else if (ltrhldr.Length <= 15 )
                {
                    ltrhldr[i].Location = new Point(120 + (i * 29), 26);
                }
                else if (ltrhldr.Length <= 20)
                {
                    ltrhldr[i].Location = new Point(2 + (i * 29), 26);
                }
                pnl[2].Controls.Add(ltrhldr[i]);
            }

            // place the letters of the word in an array
            for (int i = 0; i < wrd.Length; i++)
            {
                wrd[i].letter = dict.words[0].Substring(i, 1).ToString();
                wrd[i].got = false;
            }

            tmr[1].Interval = 2000; tmr[1].Enabled = true; tmr[1].Start();

            pnl[2].BackgroundImage = Properties.Resources._11trials;
            
            pnl[0].Visible = false; pnl[1].Visible = false; pnl[2].Visible = true; pnl[4].Visible = false; 
        }

        private void plygn_click(object sender, EventArgs e)
        {
            pnl[0].Visible = false; pnl[1].Visible = true; pnl[2].Visible = false; pnl[4].Visible = false; 
        }

        private void start_hover(object sender, EventArgs e)
        {
            bttn[0].BackgroundImage = Properties.Resources.startbuttonh;
        }

        private void exit_hover(object sender, EventArgs e)
        {
            bttn[1].BackgroundImage = Properties.Resources.exitbuttonh;
        }

        private void play_hover(object sender, EventArgs e)
        {
            bttn[2].BackgroundImage = Properties.Resources.playbuttonh;
        }

        private void playgn_hover(object sender, EventArgs e)
        {
            bttn[3].BackgroundImage = Properties.Resources.playh;
        }

        private void quit_hover(object sender, EventArgs e)
        {
            bttn[4].BackgroundImage = Properties.Resources.quith;
        }

        private void start_out(object sender, EventArgs e)
        {
            bttn[0].BackgroundImage = Properties.Resources.startbutton;
        }

        private void exit_out(object sender, EventArgs e)
        {
            bttn[1].BackgroundImage = Properties.Resources.exitbutton;
        }

        private void play_out(object sender, EventArgs e)
        {
            bttn[2].BackgroundImage = Properties.Resources.playbutton;
        }

        private void playgn_out(object sender, EventArgs e)
        {
            bttn[3].BackgroundImage = Properties.Resources.play;
        }

        private void quit_out(object sender, EventArgs e)
        {
            bttn[4].BackgroundImage = Properties.Resources.quit;
        }
        #endregion
    }
}