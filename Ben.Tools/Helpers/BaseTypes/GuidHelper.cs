using System;
using System.Collections.Generic;
using System.Linq;

namespace BenTools.Helpers.BaseTypes
{
    public static class GuidHelper
    {
        /// <summary>
        /// Le ToList() à la fin est important car autrement une nouvelle génération d'éléments se fera à chaque fois que vous allez parcourir votre séquence.
        /// </summary>
        public static List<Guid> Generate(int count = 10) => Enumerable.Range(0, count).Select(_ => Guid.NewGuid()).ToList();
    }
}