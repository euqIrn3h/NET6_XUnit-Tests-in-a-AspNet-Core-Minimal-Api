using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using MinimalApi.Models;
using System.Net;

namespace Tests
{
    public class TodoTests
    {

        private WebApplicationFactory<Program> App { get; set; }
        private System.Net.Http.HttpClient Client { get; set; }

        public TodoTests()
        {
            App = new WebApplicationFactory<Program>();
            Client = App.CreateClient();
        }

        [Fact]
        public async Task Create_Todo()
        {
            var result = await Client.PostAsJsonAsync("/todo", new Todo
                {
                    Name = "Name",
                    IsComplete = true
                }
            );

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task Get_Empty_Todo_List()
        {
            var result = await Client.GetFromJsonAsync<List<Todo>>("/todo");

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task Create_Todo_Validate_Object()
        {
            var result = await Client.PostAsJsonAsync("/todo", new Todo());

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}
