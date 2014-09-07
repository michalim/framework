﻿// Accord Unit Tests
// The Accord.NET Framework
// http://accord-framework.net
//
// Copyright © César Souza, 2009-2014
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.Tests.Statistics
{
    using System;
    using System.Globalization;
    using Accord.Statistics;
    using Accord.Statistics.Distributions.Multivariate;
    using Accord.Statistics.Distributions.Univariate;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class LevyDistributionTest
    {


        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }



        [TestMethod()]
        public void ConstructorTest()
        {
            var levy = new LevyDistribution(location: 1, scale: 4.2);

            double mean = levy.Mean;     // +inf
            double median = levy.Median; // 0.47768324427555131
            double mode = levy.Mode;     // NaN
            double var = levy.Variance;  // +inf

            double cdf = levy.DistributionFunction(x: 1.4); // 0.0011937454448720029
            double pdf = levy.ProbabilityDensityFunction(x: 1.4); // 0.016958939623898304
            double lpdf = levy.LogProbabilityDensityFunction(x: 1.4); // -4.0769601727487803

            double ccdf = levy.ComplementaryDistributionFunction(x: 1.4); // 0.99880625455512795
            double icdf = levy.InverseDistributionFunction(p: cdf); // 1.3999999

            double hf = levy.HazardFunction(x: 1.4); // 0.016979208476674869
            double chf = levy.CumulativeHazardFunction(x: 1.4); // 0.0011944585265140923

            string str = levy.ToString(CultureInfo.InvariantCulture); // Lévy(x; μ = 1, c = 4.2)

            // Tested against GNU R's rmutils package
            //
            // dlevy(1.4, m=1, s=4.2)
            // [1] 0.016958939623898303811
            //
            // plevy(1.4, m=1, s=4.2)
            // [1] 0.0011937454448720519196


            Assert.AreEqual(Double.PositiveInfinity, mean);
            Assert.AreEqual(0.47768324427555131, median);
            Assert.IsTrue(Double.IsNaN(mode));
            Assert.AreEqual(Double.PositiveInfinity, var);
            Assert.AreEqual(0.0011944585265140923, chf);
            Assert.AreEqual(0.0011937454448720519196, cdf, 1e-10); // R
            Assert.AreEqual(0.016958939623898303811, pdf, 1e-10); // R
            Assert.AreEqual(-4.0769601727487803, lpdf);
            Assert.AreEqual(0.016979208476674869, hf);
            Assert.AreEqual(0.99880625455512795, ccdf);
            Assert.AreEqual(1.4, icdf, 1e-6);
            Assert.AreEqual("Lévy(x; μ = 1, c = 4.2)", str);
        }

    }
}