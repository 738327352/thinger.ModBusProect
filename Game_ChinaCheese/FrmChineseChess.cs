using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
namespace Game_ChinaCheese
{
    public partial class FrmChineseChess : Form
    {
        



        private const int BoardSize = 9; // 9列
        private const int BoardRows = 10; // 10行
        private const int CellSize = 60;
        private ChessPiece[,] board = new ChessPiece[BoardRows, BoardSize];
        private Point? selected = null;
        private bool isRedTurn = true;

        public FrmChineseChess()
        {
            this.Text = "中国象棋";
            this.ClientSize = new Size(BoardSize * CellSize, BoardRows * CellSize);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            InitBoard();

        }

        private void InitBoard()
        {
            // 初始化棋盘（这里只放置部分棋子，完整规则可自行补充）
            // 红方
            board[9, 0] = new ChessPiece("车", true);
            board[9, 1] = new ChessPiece("马", true);
            board[9, 2] = new ChessPiece("相", true);
            board[9, 3] = new ChessPiece("仕", true);
            board[9, 4] = new ChessPiece("帅", true);
            board[9, 5] = new ChessPiece("仕", true);
            board[9, 6] = new ChessPiece("相", true);
            board[9, 7] = new ChessPiece("马", true);
            board[9, 8] = new ChessPiece("车", true);
            board[7, 1] = new ChessPiece("炮", true);
            board[7, 7] = new ChessPiece("炮", true);
            for (int i = 0; i < 9; i += 2)
                board[6, i] = new ChessPiece("兵", true);

            // 黑方
            board[0, 0] = new ChessPiece("车", false);
            board[0, 1] = new ChessPiece("马", false);
            board[0, 2] = new ChessPiece("象", false);
            board[0, 3] = new ChessPiece("士", false);
            board[0, 4] = new ChessPiece("将", false);
            board[0, 5] = new ChessPiece("士", false);
            board[0, 6] = new ChessPiece("象", false);
            board[0, 7] = new ChessPiece("马", false);
            board[0, 8] = new ChessPiece("车", false);
            board[2, 1] = new ChessPiece("炮", false);
            board[2, 7] = new ChessPiece("炮", false);
            for (int i = 0; i < 9; i += 2)
                board[3, i] = new ChessPiece("卒", false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Trace.WriteLine("OnPaint called");
            base.OnPaint(e);
            DrawBoard(e.Graphics);
            DrawPieces(e.Graphics);
        }

        private void DrawBoard(Graphics g)
        {
            Pen pen = Pens.Black;
            for (int i = 0; i < BoardRows; i++)
                g.DrawLine(pen, CellSize / 2, CellSize / 2 + i * CellSize, CellSize / 2 + (BoardSize - 1) * CellSize, CellSize / 2 + i * CellSize);
            for (int i = 0; i < BoardSize; i++)
                g.DrawLine(pen, CellSize / 2 + i * CellSize, CellSize / 2, CellSize / 2 + i * CellSize, CellSize / 2 + (BoardRows - 1) * CellSize);
            
            Rectangle rect = new Rectangle(CellSize/2 , 4 * CellSize + CellSize /2, CellSize * 8 ,CellSize);
            Pen myPen = new Pen(Color.Red, 2);
            g.DrawEllipse(myPen, rect);
            g.FillEllipse(Brushes.LightGray, rect);
            // 画楚河汉界
            g.DrawString("楚河", new Font("宋体", 18), Brushes.Blue, CellSize , CellSize * 4 + 10 + 20 + 20);
            g.DrawString("汉界", new Font("宋体", 18), Brushes.Red, CellSize * 7, CellSize * 4 + 10 + 20+ 20 );
        }

        private void DrawPieces(Graphics g)
        {
            for (int r = 0; r < BoardRows; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    var piece = board[r, c];
                    if (piece != null)
                    {
                        Rectangle rect = new Rectangle(CellSize / 2 + c * CellSize - 20, CellSize / 2 + r * CellSize - 20, 40, 40);
                        g.FillEllipse(piece.IsRed ? Brushes.Pink : Brushes.LightGray, rect);
                        g.DrawEllipse(Pens.Black, rect);
                        var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        g.DrawString(piece.Name, new Font("宋体", 16, FontStyle.Bold), piece.IsRed ? Brushes.Red : Brushes.Black, rect, sf);
                    }
                }
            }
            // 高亮选中
            if (selected.HasValue)
            {
                int r = selected.Value.Y, c = selected.Value.X;
                Rectangle rect = new Rectangle(CellSize / 2 + c * CellSize - 22, CellSize / 2 + r * CellSize - 22, 44, 44);
                g.DrawEllipse(new Pen(Color.Green, 2), rect);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Trace.WriteLine($"Mouse Down at ({e.X}, {e.Y})");
            int c = (e.X - CellSize / 2 + CellSize / 2) / CellSize; // 计算列
            int r = (e.Y - CellSize / 2 + CellSize / 2) / CellSize; // 计算行
            if (c < 0 || c >= BoardSize || r < 0 || r >= BoardRows) return;

            if (selected == null)
            {
                // 选中己方棋子
                var piece = board[r, c];
                if (piece != null && piece.IsRed == isRedTurn)
                {
                    selected = new Point(c, r);
                    Invalidate();
                }
            }
            else
            {
                // 移动棋子（这里只做简单移动，不做规则校验） 规则校验可以根据具体规则添加
                var from = selected.Value; // 选中的棋子位置
                var movingPiece = board[from.Y, from.X]; // 获取选中的棋子
                if (movingPiece != null && (board[r, c] == null || board[r, c].IsRed != movingPiece.IsRed))
                {
                    // 棋子移动
                    if (board[r, c] != null)
                    {
                        // 吃掉对方棋子
                        Trace.WriteLine($"吃掉棋子: {board[r, c].Name}");
                    }



                    board[r, c] = movingPiece; // 将选中的棋子移动到新位置
                    board[from.Y, from.X] = null; // 清除原位置的棋子
                    isRedTurn = !isRedTurn; // 切换回合
                }
                selected = null;
                Invalidate();
            }
        }
    }

    public class ChessPiece
    {
        public string Name { get; }
        public bool IsRed { get; }
        public ChessPiece(string name, bool isRed)
        {
            Name = name;
            IsRed = isRed;
        }
    }
}