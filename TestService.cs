using System;

namespace SkeletonConsoleApp
{
    public class TestService : IBaseService
    {
        private string _text;

        public TestService(string text)
        {
            _text = text;
        }

        public void Run()
        {
            Console.WriteLine(_text);
        }
    }
}
