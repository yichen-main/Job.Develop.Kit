using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Calculate.Statistics.Boundaries;
internal interface IMedianHelper<TSelf> where TSelf : IMedianHelper<TSelf>
{
    public static abstract TSelf operator +(TSelf a, TSelf b);
    public static abstract TSelf Zero { get; }
}