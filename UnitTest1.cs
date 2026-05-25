//Importing libraries
using NUnit.Framework;
using RestSharp;
using System.Text.Json;

//Project Namespace
namespace ConsumerCellularSDET;

//JsonPlaceholderApiTests tests various API endpoints of JSONPlaceholder.
//This is a 'fake' REST API that returns realistic data w/o authentication.
//Docs available at https://jsonplaceholder.typicode.com/
[TestFixture]
public class JsonPlaceholderApiTests
{
    //Class-level vars
    private RestClient _client;

    [SetUp]
    public void Setup()
    {
        //RestClient - RestSharp creates connection to execute requests through
        _client = new RestClient("https://jsonplaceholder.typicode.com");
    }

    [TearDown]
    public void TearDown()
    {
        //Get rid of connection when we're done to 
        //    release any active connections default garbage collector can't
        _client.Dispose();
    }

    //GetPosts_ReturnsSuccessStatusCode verifies get requests made to /posts
    //return a Success response (200)
    [Test]
    public void GetPosts_ReturnsSuccessStatusCode()
    {
        var request = new RestRequest("/posts");
        var response = _client.Execute(request);
        int success = 200;
        
        Assert.That((int)response.StatusCode, Is.EqualTo(success));
    }

    //GetPosts_ReturnsNonEmptyList verifies Content recieved from /posts
    //is not-empty (greater than zero)
    [Test]
    public void GetPosts_ReturnsNonEmptyList()
    {
        var request = new RestRequest("/posts");
        var response = _client.Execute(request);
        var posts = JsonSerializer.Deserialize<JsonElement>(response.Content ?? "{}");

        Assert.That(posts.GetArrayLength(), Is.GreaterThan(0));
    }

    //GetSinglePost_ReturnsCorrectId verifies we get the expected post ID
    //when pulling a post by ID (currently 1)
    [Test]
    public void GetSinglePost_ReturnsCorrectId()
    {
        int idToTest = 1; //1 for now, could randomly select in range
        var request = new RestRequest("/posts/" + idToTest);
        var response = _client.Execute(request);
        var post = JsonSerializer.Deserialize<JsonElement>(response.Content ?? "{}");

        Assert.That(post.GetProperty("id").GetInt32(), Is.EqualTo(idToTest));
    }

    //CreatePost_ReturnsCreatedStatusCode verifies post requests
    //with valid objects made to /posts
    //return a 201 Created HTTP response code.
    [Test]
    public void CreatePost_ReturnsCreatedStatusCode()
    {
        var request = new RestRequest("/posts", Method.Post);
        request.AddJsonBody(new 
            { title = "SDET Test", body = "Automated Check", userId = 1});
        var response = _client.Execute(request);
        int created = 201;

        Assert.That((int)response.StatusCode, Is.EqualTo(created));
        //TODO: Add data cleanup to protect test data integrity (DELETE)
    }
}
