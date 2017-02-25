using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        //  int[] primet = new int[6550];
        public Form1()
        {
            InitializeComponent();
            /////////////////////////////////////////
            //int cou = 1, swit = 0;
            //double s;
            //primet[0] = 2;
            //for (int i = 3; cou < 6550; i += 2)
            //{
            //    swit = 0;
            //    s = Math.Sqrt(i);
            //    for (int j = 0; primet[j++] <= s; )
            //    {
            //        if (i % primet[j] == 0)
            //        {
            //            swit = 1;
            //            break;
            //        }
            //    }
            //    if (swit == 0)
            //    {
            //        primet[cou++] = i;
            //    }
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /*其他控制///////////////////////////////////////////////////////////
          *
          *
          */
        private void tocls()
        {
            maxlen.Text = "";
            noweven.Text = "";

        }
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checknow.Text = "";
            sometext.Text = "";
            tocls();
        }
        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            checknow.Text = "";
            tocls();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tocls();
        }
        public void moveresult()
        {
            oldresult3.Text = oldresult2.Text;
            oldresult2.Text = oldresult1.Text;
            oldresult1.Text = printresult.Text;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("by 謝昌龍 2015/2\n\n簡易的小工具\n有bug還請告知,ㄎㄎ", "關於");
        }
        /*自己的方法///////////////////////////////////////////////////////////
         *
         *
         */
        ///////////////////////////////////////////////////////////
        bool busynow = false;
        private bool isbusy()
        {
            if (busynow) MessageBox.Show("計算中請稍後...", "等一下吧");
            return busynow;
        }
        private void isbusy(bool i)
        {
            busynow = i;
        }
        public bool IsNumber(string a)
        {
            int len = a.Length;
            if (len == 0) return false;
            foreach (var i in a)
            {
                if (Char.IsDigit(i) == false) return false;
            }
            return true;
        }

        public long phicount(long prime, int n)
        {
            if (n == 1) return prime - 1;
            long b = 1;
            while (n > 1) { b *= prime; --n; }
            return b * (prime - 1);
        }
        public long serprime(long num, ref long p, double sq)
        {
            for (; p < sq; p += 2)
                if (num % p == 0) { return p; }
            return num;
        }
        public long phim(long a)
        {
            int c = 0; long b = 1;
            if (a % 2 == 0)
            {
                do
                {
                    a /= 2;
                    ++c;
                } while (a % 2 == 0);
                b *= (phicount(2, c));
            }
            long i = 3;
            double sq = Math.Sqrt(a);
            for (; i <= sq; i += 2)
            {
                if (a % i == 0)
                {
                    c = 0;
                    do
                    {
                        a /= i;
                        ++c;
                    } while (a % i == 0);
                    b *= (phicount(i, c));
                    sq = Math.Sqrt(a);
                }
            }
            if (a != 1) { b *= (a - 1); }
            return b;
        }

        public long mygcd(long a, long b)
        {
            while (a != 0 && b != 0) if (a > b) a %= b; else b %= a;
            return (a == 0) ? b : a;
        }
        public long mylcm(long a, long b)
        {
            return (a / mygcd(a, b)) * b;
        }
        public long mypow(long a, int b)
        {
            if (b <= 0) return 1;
            while (b > 1) { a *= a; --b; }
            return a;
        }

        public void mytextboxcheck(ref TextBox s, int l)
        {
            int len = s.Text.Length;
            maxlen.Text = "" + l;
            checknow.Text = "";
            if (!IsNumber(s.Text))
            {
                s.Clear();
            }
            else if (len > l)
            {
                MessageBox.Show("輸入的數字必須在" + l + "位數以內!", "太超過了");
                s.Text = Convert.ToString((Convert.ToInt64(s.Text) / 10));
            }
            noweven.Text = "" + len;
        }
        public void ex_eul(long a, long b, ref long x, ref long y)
        {
            long c = 0, x1 = 1, y1 = 0, x2 = 0, y2 = 1;

            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    if (a % b > 0)
                    {
                        c = a / b;
                        x1 -= c * x2;
                        y1 -= c * y2;
                    }
                    a %= b;
                }
                else
                {
                    if (b % a > 0)
                    {
                        c = b / a;
                        x2 -= c * x1;
                        y2 -= c * y1;
                    }
                    b %= a;
                }
            }
            x = (a == 0 ? x2 : x1);
            y = (a == 0 ? y2 : y1);
        }
        private long congune(long a, long m, long modp)
        {
            if (m == 0)
            {
                return 1 % modp;
            }
            else if (a == 0)
            {
                return 0;
            }
            else
            {
                string to2 = Convert.ToString(m, 2);
                int len = to2.Length;
                long result = 1;
                a %= modp;
                for (int i = len - 1; i >= 0; --i)
                {
                    if (to2[i] == '1') { result = (result * a) % modp; }
                    a = (a * a) % modp;
                }
                return result;
            }

        }
        public void consqm(ref TextBox ba, ref TextBox bm, string bp, ref Label resultstring)
        {/*連續平方法*/
            long a = Convert.ToInt64(ba.Text), p = Convert.ToInt64(bp), m = Convert.ToInt64(bm.Text);
            rsatable.Rows.Clear();
            if (m == 0)
            {
                resultstring.Text = "" + 1 % p;
                checknow.Text = "Trivial !";
            }
            else if (a == 0)
            {
                resultstring.Text = "0";
                checknow.Text = "= =??????????";
            }
            else
            {
                checknow.Text = "指數部轉二進位:";
                sometext.Text = Convert.ToString(m, 2);
                int len = sometext.Text.Length;
                long result = 1, half = p / 2;
                a %= p;
                for (int i = len - 1; i >= 0; --i)
                {
                    if (a > half) a -= p;
                    if (sometext.Text[i] == '1') { result = (result * a) % p; }
                    rsatable.Rows.Add(new object[] { "2^" + (len - 1 - i), ((sometext.Text[i] == '1') ? "V" : ""), a, result });
                    a = (a * a) % p;
                }
                if (result < 0) result += p;
                rsatable.Rows.Add(new object[] { "", "", "", "" });
                rsatable.Rows.Add(new object[] { "", "", "結果:", result });
                resultstring.Text = "" + result;
            }
            ba.Focus();
        }
        //private bool isprime(int n)
        //{
        //    if (n < 2) return false;
        //    else if (n == 2) return true;
        //    else
        //    {
        //        if (n % 2 == 0) return false;
        //        double sq = Math.Sqrt(n);
        //        for (int j = 0; ((primet[j]) <= sq); j++)
        //        {
        //            if ((n % (primet[j])) == 0) return false;
        //        }
        //    }
        //    return true;
        //}
        private bool isprime(long n)
        {
            if (n < 2) return false;
            else if (n == 2) return true;
            else
            {
                if (n % 2 == 0) return false;
                double sq = Math.Sqrt(n);
                for (long i = 3; i <= sq; i += 2) if (n % i == 0) return false;
            }
            return true;
        }
        private long eulercriterion(long a, long p)
        {
            return congune(a, (p - 1) / 2, p);
        }






        /*質因數分解///////////////////////////////////////////////////////////
         *
         *
         */
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref textBox1, 18);

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(textBox1.Text))
            {
                string result = "";
                long a = Convert.ToInt64(textBox1.Text);
                moveresult();
                if (a == 0) { printresult.Text = "無意義"; textBox1.Clear(); return; }
                if (a == 1) { printresult.Text = "1 ^ 1"; textBox1.Clear(); return; }
                this.Cursor = Cursors.WaitCursor;
                int c = 0;
                result = "" + a + "=\n";
                if (a % 2 == 0)
                {
                    result += 2 + " ^ ";
                    do
                    {
                        a /= 2;
                        ++c;
                    } while (a % 2 == 0);
                    result += c + "\n";
                }
                long i = 3;
                double sq = Math.Sqrt(a);
                for (; i <= sq; i += 2)
                {
                    nowcount.Value = (int)(((double)(i - 2) / sq) * 100);
                    if (a % i == 0)
                    {
                        c = 0;
                        result += i + " ^ ";
                        do
                        {
                            a /= i;
                            ++c;
                        } while (a % i == 0);
                        result += c + "\n";
                        sq = Math.Sqrt(a);
                    }
                }
                if (a != 1) { result += a + " ^ 1\n"; }
                if (a == Convert.ToInt64(textBox1.Text))
                {
                    result += "是質數!";
                }
                printresult.Text = result;
                textBox1.Clear();
                this.Cursor = Cursors.Default;
                nowcount.Value = 0;
            }
        }
        /*最大公因數///////////////////////////////////////////////////////////
         *
         *
         */
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref textBox2, 18);

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref textBox3, 18);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsNumber(textBox2.Text) && IsNumber(textBox3.Text))
            {
                string result = "";
                textBox2.Focus();
                moveresult();
                long a = Convert.ToInt64(textBox2.Text), b = Convert.ToInt64(textBox3.Text);
                if (a == 0 || b == 0) { result = "= =?"; }
                else
                {
                    result = a + "_" + b + "\n";
                    while (a != 0 && b != 0)
                    {
                        if (a > b) a %= b; else b %= a;
                        if (a != 0 && b != 0) if (a > b) a %= b; else b %= a;
                        result += a + "_" + b + "\n";
                    }
                }
                if (checkBox1.Checked == false) result = textBox2.Text + "_" + textBox3.Text + "\n";
                result += "gcd: " + (a == 0 ? b : a);
                if ((a == 0 ? b : a) == 1) result += " 兩數互質!";
                textBox3.Clear();
                textBox2.Clear();
                printresult.Text = result;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(textBox2.Text)) this.textBox3.Focus();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(textBox3.Text)) this.button1.Focus();
        }
        /*最小公倍數///////////////////////////////////////////////////////////
         *
         *
         */
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref textBox5, 9);

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref textBox4, 9);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsNumber(textBox4.Text) && IsNumber(textBox5.Text))
            {
                textBox5.Focus();
                moveresult();
                long a = Convert.ToInt64(textBox5.Text), b = Convert.ToInt64(textBox4.Text);
                if (a == 0 || b == 0)
                {
                    printresult.Text = "= =?";
                }
                else
                {
                    printresult.Text = textBox5.Text + "_" + textBox4.Text;
                    printresult.Text += "\nlcm: " + mylcm(a, b);
                }
                textBox4.Clear();
                textBox5.Clear();
            }
        }
        /*phi函數///////////////////////////////////////////////////////////
        *
        *
        */
        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(textBox5.Text)) this.textBox4.Focus();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(textBox4.Text)) this.button2.Focus();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref textBox6, 18);

        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(textBox6.Text))
            {
                this.Cursor = Cursors.AppStarting;
                long a = Convert.ToInt64(textBox6.Text);
                moveresult();
                if (a == 0) { printresult.Text = "= 口 =?!"; textBox6.Clear(); return; }
                if (a == 1) { printresult.Text = "ϕ(1)=\n1"; textBox6.Clear(); return; }
                printresult.Text = "ϕ(" + a + ")=\n" + phim(a);
                textBox6.Clear();
                this.Cursor = Cursors.Default;
            }
        }
        /*擴展輾轉相除法///////////////////////////////////////////////////////////
        *
        *
        */
        private void e5box1_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref e5box1, 18);

        }

        private void e5box2_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref e5box2, 18);

        }
        private void e5box1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(e5box1.Text)) e5box2.Focus();
        }

        private void e5box2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(e5box2.Text)) button3.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!IsNumber(e5box1.Text))
            {
                e5box1.Focus();
            }
            else if (!IsNumber(e5box2.Text))
            {
                e5box2.Focus();
            }
            else
            {
                long a = Convert.ToInt64(e5box1.Text), b = Convert.ToInt64(e5box2.Text), x = 0, y = 0;
                moveresult();
                ex_eul(a, b, ref x, ref  y);
                printresult.Text = "ax+by=gcd(a,b)\na=" + a + "\nb=" + b + "\ngcd(a,b)=" + mygcd(a, b) + "\n\n整數解:\nx=" + x + "\ny=" + y;
                e5box1.Clear();
                e5box2.Clear();
                e5box1.Focus();
            }

        }
        /*Jacobi///////////////////////////////////////////////////////////
        *
        *
        */
        private bool Jacobicheck()
        {
            checknow.Text = "";
            if (!IsNumber(Jacobia.Text) || !IsNumber(Jacobib.Text)) return false;
            long a = Convert.ToInt64(Jacobia.Text), b = Convert.ToInt64(Jacobib.Text);
            if (b % 2 == 0)
            {
                checknow.Text += "分母b不得是偶數!";
                return false;
            }
            return true;
        }


        private void Jacobia_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref  Jacobia, 18);
            Jacobicheck();
        }

        private void Jacobib_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref  Jacobib, 18);
            Jacobicheck();
        }
        private void change(ref long a, ref long b)
        {
            long c = a;
            a = b;
            b = c;
        }
        private void Jacobicount_Click(object sender, EventArgs e)
        {
            Jacobia.Focus();
            if (Jacobicheck())
            {
                moveresult();
                printresult.Text = "a=" + Jacobia.Text + "\nb=" + Jacobib.Text;
                long a = Convert.ToInt64(Jacobia.Text), b = Convert.ToInt64(Jacobib.Text);
                int result = 1;
                if (mygcd(a, b) != 1 || a == 0)
                {
                    result = 0;
                }
                else
                {
                    int count = 0;
                    long mod;
                    while (a != 1)
                    {
                        if (a >= b)//先讓a的值保證小於b
                        {
                            a %= b;
                        }
                        else if (a % 2 == 0)//此時若a是二的倍數,則把二提乾淨
                        {
                            count = 0;
                            do//統計能提幾個2
                            {
                                if (a % 2 == 0)
                                {
                                    ++count;
                                    a /= 2;
                                }
                            } while (a % 2 == 0);
                            if (count % 2 == 1)//若有奇數個,考慮變號
                            {
                                mod = b % 8;
                                if (mod == 3 || mod == 5) result *= -1;//需變號
                            }
                        }
                        else//此時保證a<b且a和b都是奇數,可倒數
                        {
                            if (a % 4 == 3 && b % 4 == 3)//若a和b(mod4)=3,則需變號
                            {
                                result *= -1;
                            }
                            change(ref a, ref b);
                        }
                    }
                }
                printresult.Text += "\n a\n(-) =" + result + "\n p";
                Jacobia.Clear();
                Jacobib.Clear();
            }
        }

        private void Jacobia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(Jacobia.Text)) Jacobib.Focus();
        }

        private void Jacobib_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(Jacobib.Text)) Jacobicount.Focus();
        }
        /*ema_m///////////////////////////////////////////////////////////
       *
       *
       */
        private void ema_m_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref ema_m, 9);
        }

        private void ema_a_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref ema_a, 9);
        }

        private void ema_start_Click(object sender, EventArgs e)
        {
            if (IsNumber(ema_m.Text) && IsNumber(ema_a.Text))
            {
                long m = Convert.ToInt64(ema_m.Text), a = Convert.ToInt64(ema_a.Text);
                moveresult();
               printresult.Text="e_m(a)\nm="+m+"\na="+a;
                if (mygcd(a, m) != 1)
                {
                    printresult.Text += "\n因m和a不互質\n故無定義";
                }
                else
                {
                    long pm = phim(m), pm_2 = pm / 2,i=1;
                    a %= m;
                    for (; i <= pm_2; ++i)
                    {
                        if (pm % i == 0 && congune(a, i, m) == 1) break;
                    }
                    if (i > pm_2) i = pm;

                    printresult.Text += "\ne_m(a)=" +i;
                }
                ema_a.Clear();
                ema_m.Clear();
            }
            ema_m.Focus();
        }
        private void ema_m_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(ema_m.Text)) ema_a.Focus();
        }

        private void ema_a_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(ema_a.Text)) ema_start.Focus();
        }
        //primitive///////////////////////////////////////////////////////////////////////////////////////////////
        private bool isprimitiveroot(long g, long m)
        {
            //把絕對不行的刪掉
            if (mygcd(g, m) != 1) return false;
            //檢查m是否有機會有
            if (m <= 1) return false;           
            else if (m == 2)
            {
                if(g==1) return true;
                return false;
            }
            else if (m == 4)
            {
                if (g == 3) return true;
                return false;
            }
            else if (m % 4 == 0)
            {
                return false;
            }
            else//其他數字
            {
                long test = m;//刪掉絕不會有的
                if (test % 2 == 0) test /= 2;
                double sq = Math.Sqrt(test);
                for (long i = 3; i <= sq; i += 2)
                {
                    if (test % i == 0)
                    {
                        do
                        {
                            test /= i;
                        } while (test % i == 0);
                        if (test != 1) return false;
                    }
                }
                long pm = phim(m), pm_2 = pm / 2;
                if (congune(g, pm_2, m) == 1) return false;
                for (long i=1; i <= pm_2; ++i)
                {
                    if (pm % i == 0 && congune(g, i, m) == 1) return false;
                }
                return true;
            }
        }
        
        private void primitivem_TextChanged(object sender, EventArgs e)
        {
            checknow.Text = "";
            mytextboxcheck(ref primitivem, 9);
        }
        private void primitivestart_Click(object sender, EventArgs e)
        {
            if (IsNumber(primitivem.Text) )
            {
                moveresult();
                long m = Convert.ToInt64(primitivem.Text);
                printresult.Text ="m="+m+ "\nprimitive root(s)\n" ;
                primitivem.Clear();
                primitivem.Focus();
                if (m <= 1) 
                {
                printresult.Text +="啥= =?"; 
                }
                else if (m == 2)
                {
                    printresult.Text += "1";
                }
                else if (m == 4)
                {
                    printresult.Text += "3";
                }
                else if (m % 4==0)
                {
                    printresult.Text += "ㄜ....\n沒有= =...";
                }
                else 
                {
                    long test = m;
                    if (test % 2 == 0) test /= 2;
                    double sq = Math.Sqrt(test);
                    for (long i = 3; i <= sq; i += 2)
                    {
                        if (test % i == 0)
                        {
                            do
                            {
                                test /= i;
                            } while (test % i == 0);
                            if (test != 1) 
                            {
                                printresult.Text += "ㄜ....\n沒有= =..."; 
                                return;
                            }
                        }
                    }
                    table.Cursor = Cursors.AppStarting;

                    long pr = 2;
                    for (; pr < m; ++pr)
                    {
                        if (isprimitiveroot(pr, m)) 
                        {
                            break;
                        }
                        
                    }
                    long pm=phim(m),ppm = phim(pm),count=0;
                    printresult.Text += "有"+ppm+"個原根:\n";
                    if (ppm > 100) printresult.Text += "因個數太多\n故顯示100個\n"; 
                    string result = "";
                    for (long i = 1;i<pm&&count<100&&count<ppm ; ++i)
                    {
                        if (mygcd(i, pm) == 1) 
                        {
                            ++count;
                            result += "(" + count + "):" + congune(pr, i, m) + "\n";
                        }
                    }
                    printresult.Text += result;
                        nowcount.Value = 0;
                    table.Cursor = Cursors.Default;
                }
            
            }
            
        }

        private void primitivem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(primitivem.Text)) primitivestart.Focus();
        }

        //大型計算///////////////////////////////////////////////////////////////////////////////////////////////
        //大型計算///////////////////////////////////////////////////////////////////////////////////////////////
        /*連續平方法///////////////////////////////////////////////////////////
        *
        *
        */


        private void dif1box1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(dif1box1.Text)) dif1box2.Focus();
        }
        private void dif1box2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(dif1box2.Text)) dif1box3.Focus();
        }

        private void dif1box3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(dif1box3.Text)) startmod.Focus();
        }

        private void dif1box1_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref dif1box1, 18);
        }
        private void dif1box2_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref dif1box2, 9);
        }

        private void dif1box3_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref dif1box3, 9);
        }


        private void startmod_Click(object sender, EventArgs e)
        {

            if (IsNumber(dif1box1.Text) && IsNumber(dif1box2.Text) && IsNumber(dif1box3.Text))
            {
                if (Convert.ToInt64(dif1box3.Text) == 0)
                {
                    dif1box3.Clear();
                    dif1box3.Focus();
                    MessageBox.Show("mod 0  <--= =|||", "有事乎?");
                    return;
                }
                this.Cursor = Cursors.AppStarting;
                consqm(ref dif1box1, ref dif1box2, dif1box3.Text, ref modresult);
                this.Cursor = Cursors.Default;
            }
        }

        /*RSA加密///////////////////////////////////////////////////////////
        *
        *
        */
        private bool RSA1che()
        {
            checknow.Text = "";
            RSA1u.Text = "";
            bool ok = true;
            if (!IsNumber(RSA1p.Text) || !isprime(Convert.ToInt64(RSA1p.Text))) { checknow.Text += "p不是質數 "; ok = false; }
            if (!IsNumber(RSA1q.Text) || !isprime(Convert.ToInt64(RSA1q.Text))) { checknow.Text += "q不是質數 "; ok = false; }
            if (ok)
            {
                long p = Convert.ToInt64(RSA1p.Text), q = Convert.ToInt64(RSA1q.Text);
                if (p == q) { checknow.Text = "必須是相異的p和q!"; return false; }
                long m = p * q, m_ = (p - 1) * (q - 1);
                checknow.Text += "m=" + m;
                checknow.Text += "  ϕ(m)=" + m_;
                if (!IsNumber(RSA1k.Text)) { return false; }
                long k = Convert.ToInt64(RSA1k.Text);
                if (k <= 1 || k >= m_) { checknow.Text += "未符合 1<k<ϕ(m)"; return false; }
                else if (mygcd(k, m_) != 1) { checknow.Text += " k 未和 ϕ(m)互質"; return false; }
                long x1 = 0, y1 = 0;
                ex_eul(k, m_, ref x1, ref y1);
                if (x1 > m_) x1 %= m_;
                while (x1 < 0) x1 += m_;
                RSA1u.Text = "" + x1;
                if (RSA1x.Text.Length == 0) { return false; }
                long x = Convert.ToInt64(RSA1x.Text);
                if (x >= m) { checknow.Text += "x必須小於m!"; return false; }

                return true;
            }
            return false;
        }
        private void RSA1p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(RSA1p.Text)) RSA1q.Focus();
        }

        private void RSA1q_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(RSA1q.Text)) RSA1k.Focus();
        }

        private void RSA1k_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(RSA1k.Text)) RSA1x.Focus();
        }

        private void RSA1x_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(RSA1x.Text)) button4.Focus();
        }
        private void RSA1p_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref RSA1p, 4);
            RSA1che();
        }

        private void RSA1q_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref RSA1q, 4);
            RSA1che();
        }

        private void RSA1k_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref RSA1k, 8);
            RSA1che();
        }

        private void RSA1x_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref RSA1x, 8);
            RSA1che();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (RSA1che())
            {
                this.Cursor = Cursors.AppStarting;
                RSA1withx.Text = RSA1x.Text;
                consqm(ref RSA1x, ref RSA1k, Convert.ToString(Convert.ToInt64(RSA1p.Text) * Convert.ToInt64(RSA1q.Text)), ref RSA1b);
                RSA1x.Clear();
                RSA1x.Focus();
                this.Cursor = Cursors.Default;
            }
        }


        /*RSA解密///////////////////////////////////////////////////////////
        *
        *
        */
        private bool RSA2ch()
        {
            RSA2u.Text = "";
            if (IsNumber(RSA2m.Text) && IsNumber(RSA2k.Text))
            {
                long m = Convert.ToInt64(RSA2m.Text), k = Convert.ToInt64(RSA2k.Text);
                long p = 3;
                if ((m % 2 == 0 && isprime(m / 2) == false) || (false == isprime(m / (serprime(m, ref p, Math.Sqrt(m))))))
                {
                    checknow.Text = "m並不是兩個質數的乘積";
                    return false;
                }
                long q = (m / p), m_ = (p - 1) * (q - 1);
                checknow.Text = "m=" + p + "x" + q + " ϕ(m)=" + m_;
                if (mygcd(k, m_) != 1)
                {
                    checknow.Text += "  k 未和 ϕ(m)互質";
                    return false;
                }
                long x = 0, y = 0;
                ex_eul(Convert.ToInt64(RSA2k.Text), m_, ref x, ref y);
                if (x > m_) x %= m_;
                while (x < 0) x += m_;
                RSA2u.Text = "" + x;
                return true;
            }
            return false;
        }
        private void RSA2m_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref RSA2m, 8);
            RSA2ch();
        }

        private void RSA2k_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref RSA2k, 8);
            RSA2ch();
        }

        private void RSA2b_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref RSA2b, 8);
            RSA2ch();
        }
        private void RSA2m_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(RSA2m.Text)) RSA2k.Focus();
        }

        private void RSA2k_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(RSA2k.Text)) RSA2b.Focus();
        }

        private void RSA2b_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(RSA2b.Text)) button5.Focus();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (RSA2ch())
            {
                this.Cursor = Cursors.AppStarting;
                RSA2br.Text = RSA2b.Text;
                consqm(ref RSA2b, ref RSA2u, RSA2m.Text, ref RSA2x);
                RSA2b.Clear();
                RSA2b.Focus();
                this.Cursor = Cursors.Default;
            }
        }
        //製表///////////////////////////////////////////////////////////////////////////////////////////////
        //製表///////////////////////////////////////////////////////////////////////////////////////////////
        /*質數表///////////////////////////////////////////////////////////
        *
        *
        */
        private void primetable1_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref primetable1, 9);
        }

        private void primetable2_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref primetable2, 9);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!isbusy() && IsNumber(primetable1.Text) && IsNumber(primetable2.Text))
            {
                isbusy(true);
                table.Cursor = Cursors.AppStarting;
                primetable.Cursor = Cursors.AppStarting;
                workforprimetable.RunWorkerAsync();
            }
        }

        private void primetable1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(primetable1.Text)) primetable2.Focus();
        }

        private void primetable2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(primetable2.Text)) button6.Focus();
        }

        string resultofprimetable;
        private void workforprimetable_DoWork(object sender, DoWorkEventArgs e)
        {
            int a = Convert.ToInt32(primetable1.Text), b = Convert.ToInt32(primetable2.Text);
            resultofprimetable = "";
            long countprime = 0, counttextlen = 0;
            int max = (a > b ? a : b), min = (max == b ? a : b);
            resultofprimetable += "範圍:" + min + " ~ " + max + "\n";
            int numlen;
            int amo = (max == min) ? 1 : max - min + 1;
            for (int i = min; i <= max; ++i)
            {
                if (isprime(i))
                {
                    countprime++;
                    numlen = i.ToString().Length;
                    if (counttextlen + numlen > 100)
                    {
                        resultofprimetable += "\n" + i + " ";
                        counttextlen = numlen + 1;
                    }
                    else
                    {
                        resultofprimetable += i + " ";
                        counttextlen += numlen + 1;
                    }
                }
                workforprimetable.ReportProgress((int)((((double)(i - min)) / amo) * 100));
            }
            resultofprimetable += "\n共找到" + countprime + "個質數";
        }

        private void workforprimetable_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            nowcount.Value = e.ProgressPercentage;
        }

        private void workforprimetable_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            primetable.Text = resultofprimetable;
            table.Cursor = Cursors.Default;
            primetable.Cursor = Cursors.Default;
            nowcount.Value = 0;
            MessageBox.Show("完成!", "質數表");
            isbusy(false);
        }
        /*squares modulo表///////////////////////////////////////////////////////////
        *
        *
        */
        private void squaresm_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref primetable1, 4);
        }

        private void squaresm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(squaresm.Text))
            {
                squarestable.Rows.Clear();
                int m = Convert.ToInt16(squaresm.Text),stop=(m%2==0?m/2:(m-1)/2);
                if (m == 0) { squarestable.Rows.Add(new object[] { "= =?", "有事乎" }); return; }
                for (int i = 1; i<=stop;++i )
                    squarestable.Rows.Add(new object[] { i,(i*i)%m });
                squarestable.Rows.Add(new object[] { "以下對稱", "故省略" });
            }
        }
        /*Index Table///////////////////////////////////////////////////////////
        *
        *
        */
        private bool IndexTablecheck()
        {
            checknow.Text = "";
        if(!IsNumber(IndexTablem.Text))return false;
        if (IndexTablegdetprime.Checked==false && !isprime(Convert.ToInt64(IndexTablem.Text)))
        {
            checknow.Text += "p不是質數";
            return false;
        }
        if(!IsNumber(IndexTableg.Text)) return false;
            if (IndexTablegdet.Checked==false&&!isprimitiveroot(Convert.ToInt64(IndexTableg.Text), Convert.ToInt64(IndexTablem.Text)))
        {
            checknow.Text += "g不是原根";
            return false;
        }
        return true;
        }
        
        private void IndexTablem_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref IndexTablem, 5);
            IndexTablecheck();
        }

        private void IndexTableg_TextChanged(object sender, EventArgs e)
        {
            mytextboxcheck(ref IndexTableg, 5);
            IndexTablecheck();
        }

        private void IndexTablestart_Click(object sender, EventArgs e)
        {
            
            if (IndexTablecheck())
            {
               this.Cursor = Cursors.WaitCursor;
                IndexTabletable.Rows.Clear();
                long g = Convert.ToInt64(IndexTableg.Text), m = Convert.ToInt64(IndexTablem.Text);
                long ng=1;
                for (long i = 1; i < m; ++i)
                {
                    ng = (ng * g) % m;
                    IndexTabletable.Rows.Add(new object[] { ng,i});
                    nowcount.Value = (int)(((double)i / m) * 100);
                }
                nowcount.Value = 0;
                this.Cursor = Cursors.Default;
                MessageBox.Show("完成!", "Index Table");
            }
            IndexTablem.Focus();
        }

        private void IndexTablem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(IndexTablem.Text)) IndexTableg.Focus();
        }

        private void IndexTableg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsNumber(IndexTableg.Text)) IndexTablestart.Focus();
        }

        private void IndexTablesorta_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IndexTabletable.Sort(IndexTabletable.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            this.Cursor = Cursors.Default;
        }

        private void IndexTablesortIa_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IndexTabletable.Sort(IndexTabletable.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
            this.Cursor = Cursors.Default;
        }
       



        /* ///////////////////////////////////////////////////////////
        *
        *
        */

    }

}
