using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

class ApiService {
  private HttpClient Client;
  public Url Url;
  public ApiService(string baseUrl) {
    this.Client = new HttpClient();
    this.Url = Url.DefineBaseUrl(baseUrl);
  }

  // Static method that makes a new instance of object
  public static ApiService DefineBaseUrl(string baseUrl) {

    return new ApiService(baseUrl);
  }

  // Method that builds the resources on url
  public ApiService AddResource(string resource) {
    this.Url.AddResource(resource);

    return this;
  }

  // Method that builds the query params
  public ApiService AddQueryParam(string queryParam, string value) {
    this.Url.AddQueryParam(queryParam, value);

    return this;
  }

  public ApiService AddQueryParam(string[][] queryParams) {
    this.Url.AddMultiplesQueryParams(queryParams);

    return this;
  }

  // Method that builds the headers
  public ApiService AddHeader(string field, string value) {
    this.Client.DefaultRequestHeaders.Add(field, value);

    return this;
  }

  // Mathod that print the log of the api 
  public void SendTest() {
    Console.WriteLine(this.Url);
  }

  // Method Build
  public async Task<ResponseType> Send<ResponseType>() {
    string uri = this.Url.Build();
    string responseString = await this.Client.GetStringAsync(uri);

    ResponseType response = JsonConvert.DeserializeObject<ResponseType>(responseString);

    return response;
  }

  public async Task<List<ResponseElementType>> SendRequestForManyPages<ResponseElementType>(int from, int until, string queryParam) {
    // Generating the urls of all pages
    List<string> urls = new List<string>();
    var pages = Enumerable.Range(from, until);

    foreach(int page in pages) {
      string url = Url
        .DefineBaseUrl(this.Url.Build())
        .AddQueryParam(queryParam, page.ToString())
        .Build();
        
      urls.Add(url);
    }

    // Making a list of requests
    List<Task<string>> requests = new List<Task<string>>();

    // Initing the request task and add the request list
    urls.ForEach(delegate(string uri) {
      Task<string> request = this.Client.GetStringAsync(uri);
      requests.Add(request);
    });

    // Awaiting the all requests
    var responsesApi = await Task.WhenAll(requests);
    List<ResponseElementType> data = new List<ResponseElementType>();

    // Deserialiazing the response

    foreach(string responseString in responsesApi) {
      List<ResponseElementType> deserialiazedResponse = JsonConvert.DeserializeObject<List<ResponseElementType>>(responseString);
      data.AddRange(deserialiazedResponse);
    }

    return data;
  }
}