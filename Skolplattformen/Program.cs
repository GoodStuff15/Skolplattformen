using System.Reflection;
using Skolplattformen.Models;
namespace Skolplattformen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mm = new InputControl(new MainMenu(), new Input());
        }
    }
}
