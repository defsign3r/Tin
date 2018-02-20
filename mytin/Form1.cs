using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace mytin
{
    public partial class Form1 : Form
    {
        Point[] point = new Point[4];
        Point[] center_of_a_circle = new Point[4];
        private Pen pen = new Pen(Color.Black,2);//画笔
        private Brush brush = Brushes.Red;//画刷
        Graphics g;//画图
        Random rand = new Random();//生成随机数
        int[]x=new int[4];
        int[]y=new int[4];

        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)//单击第一个按钮发生事件生成四个随机点并记录在表中
        {
            this.Refresh();//清空画布
            listBox1.Items.Clear();
            listBox3.Items.Clear();//清除坐标表中的内容
            for (int i = 0; i < 4; i++)
            {
                x[i] = rand.Next(50, 300);
                y[i] = rand.Next(50, 300);
                g = this.CreateGraphics();
                g.FillEllipse(brush, x[i], y[i], 5, 5);
                listBox1.Items.Add("point"+i+":"+x[i] + "," + y[i]);//输出点坐标
                point[i].X = x[i];
                point[i].Y = y[i];
                g.DrawString("point"+i, new Font("宋体", 13),Brushes.Blue, new PointF(x[i] + 5, y[i] + 5));
                listBox3.Items.Add("point"+i);
            }
        }

        public double Ed(Point a,Point b)//定义了一个通过两端点求线段长度的函数
        {
            double ed = System.Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
            return ed;
        }

        public double R(Point a, Point c, Point d)//定义了一个通过三角形三点求外接圆半径的函数
        {
            double ed_ac = System.Math.Sqrt((a.X - c.X) * (a.X - c.X) + (a.Y - c.Y) * (a.Y - c.Y));
            double ed_ad = System.Math.Sqrt((a.X - d.X) * (a.X - d.X) + (a.Y - d.Y) * (a.Y - d.Y));
            double ed_cd = System.Math.Sqrt((d.X - c.X) * (d.X - c.X) + (d.Y - c.Y) * (d.Y - c.Y));
            double p = (ed_ac  + ed_ad  + ed_cd ) / 2;
            double s = System.Math.Sqrt(p * (p - ed_ac ) * (p - ed_ad ) * (p - ed_cd));
            double r = ed_ac  * ed_ad  * ed_cd  / (4 * s);
            return r;
        }

        public System.Drawing.Point Center_of_a_circle(Point a, Point c, Point d)//定义一个求三角形外接圆圆心的函数
        {
            double t1 = a.X * a.X + a.Y * a.Y;
            double t2 = c.X * c.X + c.Y * c.Y;
            double t3 = d.X * d.X + d.Y * d.Y; 
            double ed_ac = System.Math.Sqrt((a.X - c.X) * (a.X - c.X) + (a.Y - c.Y) * (a.Y - c.Y));
            double ed_ad = System.Math.Sqrt((a.X - d.X) * (a.X - d.X) + (a.Y - d.Y) * (a.Y - d.Y));
            double ed_cd = System.Math.Sqrt((d.X - c.X) * (d.X - c.X) + (d.Y - c.Y) * (d.Y - c.Y));
            double temp = a.X * c.Y + c.X * d.Y + d.X * a.Y -a.X * d.Y - c.X * a.Y - d.X * c.Y;
            double p = (ed_ac + ed_ad + ed_cd) / 2;
            double s = System.Math.Sqrt(p * (p - ed_ac) * (p - ed_ad) * (p - ed_cd));
            double x0 = (t2 * d.Y + t1 * c.Y + t3 * a.Y - t2 * a.Y - t3 * c.Y - t1 * d.Y) / temp / 2;
            double y0 = (t3 * c.X + t2 * a.X + t1 * d.X - t1 * c.X - t2 * d.X - t3 * a.X) / temp / 2; 
            center_of_a_circle[0].X = (int)x0 ;
            center_of_a_circle[0].Y = (int)y0 ;
            return center_of_a_circle[0];

        }

        private void quchongfu(ListBox lb)//一个去除listbox中重复项的函数，参数是目标listbox
        {
            

            for (int i = 0; i < lb.Items.Count; i++)
            {
                for (int j = i + 1; j < lb.Items.Count; j++)
                {
                    if (lb.Items[i].Equals(lb.Items[j]))
                    {
                        lb.Items.Remove(lb.Items[j]);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            g = this.CreateGraphics();
            if (System.Math.Sqrt((Center_of_a_circle(point[3], point[0], point[2]).X - point[1].X) * (Center_of_a_circle(point[3], point[0], point[2]).X - point[1].X) + (Center_of_a_circle(point[3], point[0], point[2]).Y - point[1].Y) * (Center_of_a_circle(point[3], point[0], point[2]).Y - point[1].Y)) > R(point[3], point[0], point[2]))
            {
                g.DrawLine(pen, point[3], point[0]);
                g.DrawLine(pen, point[3], point[2]);
                g.DrawLine(pen, point[2], point[0]);
                listBox2.Items.Add("ed03");
                listBox2.Items.Add("ed23");
                listBox2.Items.Add("ed02");
            }

            if (System.Math.Sqrt((Center_of_a_circle(point[3], point[0], point[1]).X - point[2].X) * (Center_of_a_circle(point[3], point[0], point[1]).X - point[2].X) + (Center_of_a_circle(point[3], point[0], point[1]).Y - point[2].Y) * (Center_of_a_circle(point[3], point[0], point[1]).Y - point[2].Y)) > R(point[3], point[0], point[1]))
            {
                g.DrawLine(pen, point[3], point[0]);
                g.DrawLine(pen, point[3], point[1]);
                g.DrawLine(pen, point[1], point[0]);
                listBox2.Items.Add("ed03");
                listBox2.Items.Add("ed13");
                listBox2.Items.Add("ed01");
            }

            if (System.Math.Sqrt((Center_of_a_circle(point[3], point[2], point[1]).X - point[0].X) * (Center_of_a_circle(point[3], point[2], point[1]).X - point[0].X) + (Center_of_a_circle(point[3], point[2], point[1]).Y - point[0].Y) * (Center_of_a_circle(point[3], point[2], point[1]).Y - point[0].Y)) > R(point[3], point[2], point[1]))
            {
                g.DrawLine(pen, point[3], point[2]);
                g.DrawLine(pen, point[3], point[1]);
                g.DrawLine(pen, point[1], point[2]);
                listBox2.Items.Add("ed23");
                listBox2.Items.Add("ed13");
                listBox2.Items.Add("ed12");
            }

            if (System.Math.Sqrt((Center_of_a_circle(point[0], point[2], point[1]).X - point[3].X) * (Center_of_a_circle(point[0], point[2], point[1]).X - point[3].X) + (Center_of_a_circle(point[0], point[2], point[1]).Y - point[3].Y) * (Center_of_a_circle(point[0], point[2], point[1]).Y - point[3].Y)) > R(point[0], point[2], point[1]))
            {
                g.DrawLine(pen, point[0], point[2]);
                g.DrawLine(pen, point[0], point[1]);
                g.DrawLine(pen, point[1], point[2]);
                listBox2.Items.Add("ed02");
                listBox2.Items.Add("ed01");
                listBox2.Items.Add("ed12");
            }
            quchongfu(listBox2);//通过去重复函数将边表中的重复项去除掉
        }           
    }
}
