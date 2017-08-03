using Microsoft.AspNetCore.Identity;
using System;

namespace HashIterationCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ASP.NET Identity Core Hash Iteration Calculator");
            Console.WriteLine("How many users do you need to authenticate per second?");

            var targetNumberOfUsersAuthenticatingPerSecond = 3;
            var input = Console.ReadLine();
            if (int.TryParse(input, out int inputInteger) && inputInteger > 0 && inputInteger <= 2000)
            {
                targetNumberOfUsersAuthenticatingPerSecond = inputInteger;
            }
            var minimumIterations = 10000;
            var iterations = minimumIterations;
            var hasher = new AspNetIdentityPasswordHasher(iterations);
            var time = hasher.GetPasswordVerificationTime(targetNumberOfUsersAuthenticatingPerSecond);
            var factor = 1000.0f / time.TotalMilliseconds;
            if(factor >= 1.0f)
            {
                // If the minimum iteration value used less than a second, calculate the suggested 
                // number of iterations and validate that the server can indeed verify the target
                // number of users per second using the suggested iteration count.
                iterations = Convert.ToInt32(iterations * factor);
                hasher = new AspNetIdentityPasswordHasher(iterations);
                time = hasher.GetPasswordVerificationTime(targetNumberOfUsersAuthenticatingPerSecond);
            }
            Console.WriteLine(string.Format("Using {0} iterations, you could authenticate up to {1} users in {2} seconds on this machine.", iterations, targetNumberOfUsersAuthenticatingPerSecond, time.TotalSeconds));
        }
    }
}