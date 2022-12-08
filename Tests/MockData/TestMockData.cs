using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalApi.Models;

namespace Tests.MockData
{
    internal class TestMockData
    {
        public static List<Todo> GetTodos()
        {
            return new List<Todo>{
             new Todo{
                 Id = 1,
                 Name = "Need To Go Shopping",
                 IsComplete = true
             },
             new Todo{
                 Id = 2,
                 Name = "Cook Food",
                 IsComplete = true
             },
             new Todo{
                 Id = 3,
                 Name = "Play Games",
                 IsComplete = false
             }
         };
        }
    }
}
