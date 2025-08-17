using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.TeachDemo
{
     class Test
    {


        public T Add<T, T1, T2>(T1 num1, T2 num2)where T:struct,T1,T2 
        {


            dynamic a = num1;

            dynamic b = num2;

            return a + b;

        }
        

    }



    public class MyList<T> { 
    
    private List<T> list = new List<T> ();
    
    }




}
