


using thinger.TeachDemo;
using System.Threading;
using System.Threading.Tasks;
class Program
{
    static void Main(string[] args)
    {
        //ActionAndFuncBase testBase=new ActionAndFuncBase();

        //testBase.Test();
        //Console.ReadLine();

        //Test1();
        //Test2();
        Test3();
        Console.ReadLine();




    }

    static void Test1() {

        Task task = new Task(() =>
        {

            Console.WriteLine("new一个新的task，对应的线程ID！" + Thread.CurrentThread.ManagedThreadId);


        });
        task.Start();


    }
    static void Test2()
    {

        Task task = Task.Run(() =>
        {
            //这里编写具体的任务

            Console.WriteLine("使用Task中的RUn方法，对应线程中的ID！" + Thread.CurrentThread.ManagedThreadId);


        });
        


    }
    static void Test3()
    {

        Task task = Task.Factory.StartNew(() =>
        {
            //这里编写具体的任务

            Console.WriteLine("使用Task中的Factory.StartNew方法，对应线程中的ID！" + Thread.CurrentThread.ManagedThreadId);


        });
       


    }
}