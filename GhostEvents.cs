using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StrykerDG.GhostFunctions.DTOs.Pages;
using Newtonsoft.Json.Linq;
using RestSharp;
using StrykerDG.GhostFunctions.DTOs.Tags;
using StrykerDG.GhostFunctions.DTOs.Posts;
using StrykerDG.GhostFunctions.DTOs.Members;
using StrykerDG.GhostFunctions.Interfaces;

namespace StrykerDG.GhostFunctions
{
    public static class GhostEvents
    {
        [FunctionName("MemberAdded")]
        public static async Task<IActionResult> MemberAdded(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "member")] HttpRequest request,
            ILogger log
        )
        {
            Member newMember = await ParseObjectFromBody<Member, MemberEvent>("member", request.Body);

            object discordContent = new
            {
                username = "StrykerDG",
                content = $"{newMember.Name} registered at {newMember.Created}!",
            };

            string baseUrl = Environment.GetEnvironmentVariable("BaseUrl");
            string webhook = Environment.GetEnvironmentVariable("MemberWebhook");

            RestClient client = new RestClient(baseUrl);
            IRestRequest discordRequest = new RestRequest(webhook, Method.POST)
                .AddJsonBody(discordContent);

            await client.PostAsync<IRestResponse>(discordRequest);

            return new OkResult();
        }

        [FunctionName("PagePublished")]
        public static async Task<IActionResult> PagePublished(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "page")] HttpRequest request,
            ILogger log
        )
        {
            Page publishedPage = await ParseObjectFromBody<Page, PageEvent>("page", request.Body);

            object discordContent = new
            {
                username = "StrykerDG",
                content = $"A new page, {publishedPage.Url}, was just created by {publishedPage.PrimaryAuthor.Name}",
            };

            string baseUrl = Environment.GetEnvironmentVariable("BaseUrl");
            string webhook = Environment.GetEnvironmentVariable("PageWebhook");

            RestClient client = new RestClient(baseUrl);
            IRestRequest discordRequest = new RestRequest(webhook, Method.POST)
                .AddJsonBody(discordContent);

            await client.PostAsync<IRestResponse>(discordRequest);

            return new OkResult();
        }

        [FunctionName("PostPublished")]
        public static async Task<IActionResult> PostPublished(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "post")] HttpRequest request,
            ILogger log
        )
        {
            Post publishedPost = await ParseObjectFromBody<Post, PostEvent>("post", request.Body);

            object discordContent = new
            {
                username = "StrykerDG",
                content = $"A new post, {publishedPost.Title}, is now available. Check it out at {publishedPost.Url}",
            };

            string baseUrl = Environment.GetEnvironmentVariable("BaseUrl");
            string webhook = Environment.GetEnvironmentVariable("PostWebhook");

            RestClient client = new RestClient(baseUrl);
            IRestRequest discordRequest = new RestRequest(webhook, Method.POST)
                .AddJsonBody(discordContent);

            await client.PostAsync<IRestResponse>(discordRequest);
            return new OkResult();
        }

        [FunctionName("TagPublished")]
        public static async Task<IActionResult> TagPublished(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "tag")] HttpRequest request,
            ILogger log
        )
        {
            Tag publishedTag = await ParseObjectFromBody<Tag, TagEvent>("tag", request.Body);

            object discordContent = new
            {
                username = "StrykerDG",
                content = $"A new tag, {publishedTag.Name}, was just created. You can see related posts at {publishedTag.Url}",
            };

            string baseUrl = Environment.GetEnvironmentVariable("BaseUrl");
            string webhook = Environment.GetEnvironmentVariable("TagWebhook");

            RestClient client = new RestClient(baseUrl);
            IRestRequest discordRequest = new RestRequest(webhook, Method.POST)
                .AddJsonBody(discordContent);

            await client.PostAsync<IRestResponse>(discordRequest);

            return new OkResult();
        }

        private static async Task<T> ParseObjectFromBody<T, U>(string token, Stream stream) where T : IGhostObject where U : IGhostEvent<T>
        {
            JObject data = await ParseRequestBody(stream);
            U eventData = data.SelectToken(token).ToObject<U>();
            return eventData.Current;
        }

        private static async Task<JObject> ParseRequestBody(Stream stream)
        {
            string body = await new StreamReader(stream).ReadToEndAsync();
            return JObject.Parse(body);
        }
    }
}
