using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rockweed
{
    public partial class TinyGame : Form
    {
        private const int GRID_SIZE = 15;      // 棋盘大小 15x15
        private const int CELL_SIZE = 40;      // 每个格子的大小
        private const int OFFSET = 30;         // 棋盘边距
        private int[,] board;                  // 棋盘数组：0空、1黑、2白

        private bool isBlackTurn = true;       // true 为黑棋回合
        private bool gameOver = false;         // 游戏是否结束

        private Button restButton;             // 重新开始按钮
        public TinyGame()
        {
            InitializeComponent();
            InitializeGame();
            this.Paint += new PaintEventHandler(Form2_Paint);
            this.DoubleBuffered = true;
            this.ClientSize = new Size(OFFSET * 2 + (CELL_SIZE * GRID_SIZE), //width and height
                                        OFFSET * 2 + (CELL_SIZE * GRID_SIZE) + 50);

            this.MouseClick += new MouseEventHandler(Form2_MouseClick); // 处理鼠标点击事件



            restButton = new Button();          //重新开始按钮
            restButton.Text = "重新开始";
            restButton.Size = new Size(100, 30);
            restButton.Location = new Point(OFFSET, GRID_SIZE * CELL_SIZE + OFFSET + 10);
            restButton.Click += (object sender, EventArgs e) => InitializeGame(); // 重新开始游戏

            this.Controls.Add(restButton);


            // 禁止最大化窗口
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        //绘制棋盘
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // vertical lines
            for (int i = 0; i <= GRID_SIZE; i++)
            {
                g.DrawLine(Pens.Black,
                            OFFSET + CELL_SIZE * i,
                            OFFSET,
                            OFFSET + CELL_SIZE * i,
                            OFFSET + GRID_SIZE * CELL_SIZE
                 );
            }

            // horizontal lines
            for (int i = 0; i <= GRID_SIZE; i++)
            {
                g.DrawLine(Pens.Black,
                            OFFSET,
                            OFFSET + CELL_SIZE * i,
                            OFFSET + GRID_SIZE * CELL_SIZE,
                            OFFSET + CELL_SIZE * i
                 );
            }

            // 绘制棋子
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if (board[i, j] != 0)
                    {

                        Brush brush = board[i, j] == 1 ? Brushes.Black : Brushes.White;
                        g.FillEllipse(brush,
                            OFFSET + i * CELL_SIZE - CELL_SIZE / 2 + CELL_SIZE / 8,
                            OFFSET + j * CELL_SIZE - CELL_SIZE / 2 + CELL_SIZE / 8,
                            CELL_SIZE * 3 / 4,
                            CELL_SIZE * 3 / 4);

                    }

                }

            }


        }
        private void Form2_MouseClick(Object sender, MouseEventArgs e)
        {
            int x = (e.X - OFFSET + CELL_SIZE / 2) / CELL_SIZE;// 获取鼠标点击的X坐标 对坐标进行处理符合reflection40 * 15
            int y = (e.Y - OFFSET + CELL_SIZE / 2) / CELL_SIZE;

            if (x < 0 || x >= GRID_SIZE || y < 0 || y >= GRID_SIZE)
                return;
            if (board[x, y] != 0)
                return;

            board[x, y] = isBlackTurn ? 1 : 2; // 黑棋为1，白棋为2

            if (CheckedWin(x, y))
            {
                gameOver = true;
                MessageBox.Show(
                    (isBlackTurn ? "黑棋" : "白棋") + "获胜！",
                    "游戏结束",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            isBlackTurn = !isBlackTurn;
            this.Refresh(); // 刷新界面

        }
        private bool CheckedWin(int x, int y)
        {

            int currentPlayer = board[x, y];
            int[][] directions = new int[][]
            {
                            new int[] {1, 0}, // 水平
                            new int[] {0, 1}, // 垂直
                            new int[] {1, 1}, // 主对角线
                            new int[] {1, -1} // 副对角线
            };

            foreach (var dir in directions)
            {
                int count = 1;
                for (int i = 1; i < 5; i++)
                {
                    int newX = x + dir[0] * i;
                    int newY = y + dir[1] * i;
                    if (newX < 0 || newX >= GRID_SIZE || newY < 0 || newY >= GRID_SIZE)
                        break;
                    if (board[newX, newY] != currentPlayer)
                        break; // 如果不是当前玩家的棋子，则中断
                    count++;
                    Trace.WriteLine($"Checking win: ({newX}, {newY}) - Count: {count}");
                }
                for (int i = 1; i < 5; i++)
                {
                    int newX = x - dir[0] * i;
                    int newY = y - dir[1] * i;
                    if (newX < 0 || newX >= GRID_SIZE || newY < 0 || newY >= GRID_SIZE)
                        break;
                    if (board[newX, newY] != currentPlayer)
                        break; // 如果不是当前玩家的棋子，则中断
                    count++;
                    Trace.WriteLine($"Checking win: ({newX}, {newY}) - Count: {count}");
                }
                if (count >= 5) return true; // 如果连续五个棋子，则获胜
            }
            return false;
        }
        private void InitializeGame()
        {
            board = new int[GRID_SIZE, GRID_SIZE];
            isBlackTurn = true; // 黑棋先行
            gameOver = false;
            this.Refresh(); // 刷新界面

        }

        private void Form2_Load(object sender, EventArgs e)
        {



        }
    }
}
