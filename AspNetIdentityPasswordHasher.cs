using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;

namespace HashIterationCalculator
{
    public class AspNetIdentityPasswordHasher : PasswordHasher<MyUser>
    {
        public AspNetIdentityPasswordHasher(
            int iterations, 
            PasswordHasherCompatibilityMode compatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
            ) 
            : base(Options.Create(new PasswordHasherOptions() {
                IterationCount = iterations,
                CompatibilityMode = compatibilityMode
            }))
        {
        }

        public TimeSpan GetPasswordVerificationTime(int count, string password = "Password1!")
        {
            var hash = HashPassword(null, password);
            var timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                var result = VerifyHashedPassword(null, hash, password);
            }
            timer.Stop();
            return timer.Elapsed;
        }
    }
}
