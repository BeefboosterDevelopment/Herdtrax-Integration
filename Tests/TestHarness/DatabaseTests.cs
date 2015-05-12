using System;
using BBDM;

namespace TestHarness
{
    public class DatabaseTests
    {
        private BBModel _model;

        public void Test()
        {
            Console.WriteLine("Testing database");
            _model = new BBModel();

            int lastSN = _model.GetLastSN();
            Console.WriteLine("Current last SN:{0}", _model.GetLastSN());


            var newLastSN = _model.ReserveSNRange(3);
            Console.WriteLine("Resered 3 serial numbers. From {0} to {1}", newLastSN-3, newLastSN);

            

            //_model.SetLastSN(lastSN);



        }
    }
}