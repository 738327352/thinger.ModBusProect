
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thingerTask
{
    internal class TestLock
    {


        public void TestCount() {

            int nums = 0;

            for (int i = 0; i < 100; i++)
            {
                nums++;

                Console.WriteLine(nums);
            }
        
        }



        public void Start() {

            for (int i = 0; i < 5; i++)
            {
                Task.Run(() =>
                {

                    TestCount();
                });


            }
        
        
        }

    }
}
