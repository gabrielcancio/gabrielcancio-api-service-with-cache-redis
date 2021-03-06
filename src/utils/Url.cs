using System;
class Url {
  private string UrlString;

  public Url(String url) {
    this.UrlString = url;
  }

  // Method that define the Url
  public static Url DefineBaseUrl(string url) {

    return new Url(url);
  }

  // Method that adds resources
    public Url AddResource(string resource) {
    string resourceFormated = $"/{resource}";
    this.UrlString = string.Concat(this.UrlString, resourceFormated);

    return this;
  }

  // Method that adds query params
  public Url AddQueryParam(string queryParam, string value) {
    this.performAddQueryParam(queryParam, value);

    return this;
  }

  private bool isFirstQueryParam() {
    
    return this.UrlString.Contains('?');
  }

  private void performAddQueryParam(string queryParam, string value) {
    if(this.isFirstQueryParam()) {
      this.UrlString = string.Concat(this.UrlString, $"&{queryParam}={value}");

      return;
    }

    this.UrlString = string.Concat(this.UrlString, $"?{queryParam}={value}");
  }

  private bool isQueriesMatrixValid(string[][] queries) {
    int sizeOfQueriesMatrix = queries.Length;
    int sizeOfParams = queries[0].Length;
    int sizeOfValues = queries[1].Length;

    if(sizeOfQueriesMatrix != 2) return false;
    if(sizeOfParams != sizeOfValues) return false;

    return true;
  }

  private void performAddQueryWithMultiplesParams(string[][] queries) {
    int queryParamslength = queries[0].Length;

    for(int index = 0; index < queryParamslength; index++) {
      this.performAddQueryParam(queries[0][index], queries[1][index]);
    }
  }

  public Url AddMultiplesQueryParams(string[][] queries) {
    this.performAddQueryWithMultiplesParams(queries);

    return this;
  }

  // Method that return the built url
  public string Build()  {
    return UrlString;
  }
}