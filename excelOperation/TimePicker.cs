using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace excelOperation
{
    public class TimePicker : DateTimePicker
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int EM_GETSEL = 0x00B0;

        // 替换 OnMouseWheel 方法中获取选区的部分，避免使用指针和不安全代码
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Trace.WriteLine($"TimePicker.OnMouseWheel: {e.Delta}");

            if (!this.Focused)
                this.Focus();
            Trace.WriteLine($"-----------------: 1");

            // 获取编辑框句柄
            IntPtr editHandle = GetEditHandle();
            if (editHandle == IntPtr.Zero)
                return;
            Trace.WriteLine($"-----------------:2 ");

            // 使用 Win32 API 获取光标选区（安全方式）
            int selStart = 0, selEnd = 0;
            int lResult = (int)SendMessage(editHandle, EM_GETSEL, IntPtr.Zero, IntPtr.Zero);
            selStart = lResult & 0xFFFF;
            selEnd = (lResult >> 16) & 0xFFFF;
            Trace.WriteLine($"selStart: {selStart}, selEnd: {selEnd}");

            int fieldIndex = GetTimeFieldIndex(selStart);

            int delta = e.Delta > 0 ? 1 : -1;
            DateTime newValue = this.Value;
            Trace.WriteLine($"fieldIndex: {fieldIndex}");
            switch (fieldIndex)
            {
                case 0: newValue = this.Value.AddHours(delta); break;
                case 1: newValue = this.Value.AddMinutes(delta); break;
                case 2: newValue = this.Value.AddSeconds(delta); break;
            }
            this.Value = newValue;

            // 阻止事件继续冒泡
            ((HandledMouseEventArgs)e).Handled = true;
        }

        // 获取编辑框句柄
        private IntPtr GetEditHandle()
        {
            // DateTimePicker 的编辑框不是 TextBox，而是原生 Win32 控件
            // 需要通过 API 获取子窗口句柄
            if (this.IsHandleCreated)
            {
                // 0x00000001 表示子窗口
                IntPtr child = NativeMethods.GetWindow(this.Handle, 5); // GW_CHILD = 5
                return child;
            }
            return IntPtr.Zero;
        }

        // 根据光标位置判断是小时、分钟还是秒
        private int GetTimeFieldIndex(int selStart)
        {
            // 假设格式为"HH:mm:ss"
            // 注意：selStart是以字符为单位的光标位置
            // 0-1: 小时, 3-4: 分钟, 6-7: 秒
            if (selStart <= 2) return 0; // 小时
            if (selStart >= 3 && selStart <= 5) return 1; // 分钟
            return 2; // 秒
        }

        // Win32 API 辅助类
        internal static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        }
    }
}