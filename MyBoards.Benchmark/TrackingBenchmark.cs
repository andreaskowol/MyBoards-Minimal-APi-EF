using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using MyBoards.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Benchmark
{
    [MemoryDiagnoser]
    public class TrackingBenchmark
    {
        [Benchmark]
        public int WithTracking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyBoardContext>()
                .UseSqlServer("Server=DESKTOP-TV3SA0P;Database=MyBoardDb;Trusted_Connection=True;");
            var _dbContext = new MyBoardContext(optionsBuilder.Options);

            var comments = _dbContext.Comments.ToList();

            return comments.Count;
        }
        [Benchmark]
        public int WithNoTracking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyBoardContext>()
                .UseSqlServer("Server=DESKTOP-TV3SA0P;Database=MyBoardDb;Trusted_Connection=True;");
            var _dbContext = new MyBoardContext(optionsBuilder.Options);

            var comments = _dbContext.Comments.AsNoTracking().ToList();

            return comments.Count;
        }
    }
}
