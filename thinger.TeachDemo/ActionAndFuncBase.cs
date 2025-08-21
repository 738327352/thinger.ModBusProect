using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.TeachDemo
{
    class ActionAndFuncBase
    {

        public void Test()
        {


            Action<String> action1 = (name) => Console.WriteLine($"我们正在和{name}学习上位机开发技术！");

            Action<String, String> action2 = (name, course) => Console.WriteLine($"我们正在和{name}学习{course}课程");
      
            action1("张三");
            action2("李四", "C#编程基础");

        }


        //public void Test1() { 
        
        
        //Func<int,int,double> myFunc1 = (a, b) => a + b;
        
        //}


    }

}
