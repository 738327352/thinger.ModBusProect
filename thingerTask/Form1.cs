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

namespace thingerTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

                  //取消任务信号源对象
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        //手动停止事件对象
        private ManualResetEvent mResetEvent = new ManualResetEvent(true);

        private void button1_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource.IsCancellationRequested) {
                cancellationTokenSource = new CancellationTokenSource();
            }
            // 创建一个新的 线程任务
            Task task = new Task(() =>
            {
                //用来控制这里是否需要暂停和继续
         

                int count = 0;

                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    mResetEvent.WaitOne(); // 等待信号，默认是true，表示不等待   
                    count++;
                    Console.WriteLine(count);
                    Thread.Sleep(300); // 模拟耗时操作
                }
            },cancellationTokenSource.Token);
            task.Start();

            //task.ContinueWith(t => {

            //    this.lbl_Label.Text = "来自Task的任务已更新";
            
            //},TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            mResetEvent.Set(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mResetEvent.Reset();
        }
    }
}
