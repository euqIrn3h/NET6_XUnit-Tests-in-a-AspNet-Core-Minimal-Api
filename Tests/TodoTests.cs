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
        [Fact]
        public async Task Create_Todo()
        {
            await using var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();

            var result = await client.PostAsJsonAsync("/todo", new Todo
                {
                    Name = "Name",
                    IsComplete = true
                }
            );

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        }

        [Fact]
        public async Task Create_Todo_Validate_Object()
        {
            await using var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();

            var result = await client.PostAsJsonAsync("/todo", new Todo());

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
