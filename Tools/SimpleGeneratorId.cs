using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    /// <summary>
    ///     The simple ID generator.
    /// </summary>
    public class SimpleGeneratorId : IGeneratorId
    {
        /// <summary>
        ///     The key size.
        /// </summary>
        private const int KeySize = 32;

        /// <summary>
        ///     The random generator.
        /// </summary>
        private readonly Random RandomGenerator = new Random();

        /// <summary>
        ///     The method generates unique ID.
        /// </summary>
        /// <returns>
        ///     The unique ID.
        /// </returns>
        public string GenerateUniqueId()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < KeySize; i++)
            {
                stringBuilder.Append(RandomGenerator.Next(10));
            }

            return stringBuilder.ToString();
        }
    }
}
