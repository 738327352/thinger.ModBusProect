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

        // �滻 OnMouseWheel �����л�ȡѡ���Ĳ��֣�����ʹ��ָ��Ͳ���ȫ����
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Trace.WriteLine($"TimePicker.OnMouseWheel: {e.Delta}");

            if (!this.Focused)
                this.Focus();
            Trace.WriteLine($"-----------------: 1");

            // ��ȡ�༭����
            IntPtr editHandle = GetEditHandle();
            if (editHandle == IntPtr.Zero)
                return;
            Trace.WriteLine($"-----------------:2 ");

            // ʹ�� Win32 API ��ȡ���ѡ������ȫ��ʽ��
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

            // ��ֹ�¼�����ð��
            ((HandledMouseEventArgs)e).Handled = true;
        }

        // ��ȡ�༭����
        private IntPtr GetEditHandle()
        {
            // DateTimePicker �ı༭���� TextBox������ԭ�� Win32 �ؼ�
            // ��Ҫͨ�� API ��ȡ�Ӵ��ھ��
            if (this.IsHandleCreated)
            {
                // 0x00000001 ��ʾ�Ӵ���
                IntPtr child = NativeMethods.GetWindow(this.Handle, 5); // GW_CHILD = 5
                return child;
            }
            return IntPtr.Zero;
        }

        // ���ݹ��λ���ж���Сʱ�����ӻ�����
        private int GetTimeFieldIndex(int selStart)
        {
            // �����ʽΪ"HH:mm:ss"
            // ע�⣺selStart�����ַ�Ϊ��λ�Ĺ��λ��
            // 0-1: Сʱ, 3-4: ����, 6-7: ��
            if (selStart <= 2) return 0; // Сʱ
            if (selStart >= 3 && selStart <= 5) return 1; // ����
            return 2; // ��
        }

        // Win32 API ������
        internal static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        }
    }
}