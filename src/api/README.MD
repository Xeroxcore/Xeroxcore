![Xeroxcore logo](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/images/Xeroxcore_Logo.png)

## Prerequisites

The following knowledge is necessary to be able to utilize the web api.
lacking knowledge in one section might lead to difficulties when trying
to fetch/post data to the API.

- [JSON Data](https://www.json.org/)
- HTTP Clients
  - [C#](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netframework-4.8)
  - [Angular](https://angular.io/guide/http)
  - [Ajax](https://www.w3schools.com/js/js_ajax_http_send.asp)
- [Bearer Authentication](https://tools.ietf.org/html/rfc6750#section-2.1)

<!--ts-->

## Table of Contents

- [Common Server Responses](#common-server-responses)
- [Authentication](#Authentication)
  - [Tokenkey](#Tokenkey)
  - [AdminKey](#useraccount-adminkey)
    <!--te-->

## Common Server Responses

| Error Code | Description                                                        |
| :--------: | ------------------------------------------------------------------ |
|   `200`    | OK : Request was successful                                        |
|   `201`    | Created : A resource has been created                              |
|   `204`    | No Content : The request response is empty                         |
|   `400`    | Bad request : The server cant understand your syntax               |
|   `401`    | Unauthorized : User not authenticated                              |
|   `403`    | Forbidden : User don't have the right privileges                    |
|   `404`    | Not found : The url does not exist or has been moved               |
|   `405`    | Method not allowed : Use the correct method GET, POST, PUT, DELETE |
|   `415`    | Unsupported Media Type : Data is in format that is not supported   |
|   `429`    | Too many requests : To many requests within a x amount of time     |
|   `500`    | Internal Server Error : An error has occurred server side           |

## Authentication

This section contains information regarding authentication. It aims at
helping you to get a token key and then utilize it within the header to
either fetch, put, delete or create data.

### Tokenkey

API token key is a key that is used for third party sites refereed to as
external domains that don't require administrative rights. sites with
token keys have limited access and is mostly used to fetch data.

#### Sample API Entry

| Info               | Description                                                                                |
| ------------------ | ------------------------------------------------------------------------------------------ |
| `Title`            | Get a API Token                                                                            |
| `Url`              | /apiauth                                                                                   |
| `Method`           | POST                                                                                       |
| `Url Parameters`   | TokenKey:string {Required} <br/> GroupKey:int {Required}                                   |
| `Success Response` | Status Code: 200 <br/> Response: {TokenKey:"Key", authType:"Bearer", expiration: datetime} |
| `Error Response`   | Status Code: 401                                                                           |

#### Sample API Response

```
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJmYWIyYWQ3NC0yMTZjLTQ4N2MtY
    jBjOC01ZmFkNDI2M2M0MDEiLCJEYXRhYmFzZV9JZCI6IjEiLCJBY3RpdmUiOiJUcnVlIiwiaHR0cDovL3NjaGVt
    YXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE1
    NzM3NDQyMTksImlzcyI6Im1jc3VuaXR5Lm5ldCIsImF1ZCI6IlVzZXIifQ._W8jt7HuzifzEartVSCbDkVKAeFKx
    4FAy6hNuEtDYSQ",
    "authType": "Bearer",
    "expiration": "UTC:2019-11-14 3:10:19 PM"
}
```

### Useraccount (AdminKey)

A useraccount token key poses more rights then a token key it is used for
administrative purposes and thus has rights to access all controllers.
Admin keys also have rights to put,create,delete rights if they are of the
correct role.

#### Sample Useraccount Entry

| Info               | Description                                                                                |
| ------------------ | ------------------------------------------------------------------------------------------ |
| `Title`            | Get a user Token                                                                           |
| `Url`              | /userauth                                                                                  |
| `Method`           | POST                                                                                       |
| `Url Parameters`   | username:string {Required} <br/> password:string {Required}                                |
| `Success Response` | Status Code: 200 <br/> Response: {TokenKey:"Key", authType:"Bearer", expiration: datetime} |
| `Error Response`   | Status Code: 401                                                                           |

#### Sample API Response

```
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJmYWIyYWQ3NC0yMTZjLTQ4N2MtY
    jBjOC01ZmFkNDI2M2M0MDEiLCJEYXRhYmFzZV9JZCI6IjEiLCJBY3RpdmUiOiJUcnVlIiwiaHR0cDovL3NjaGVt
    YXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE1
    NzM3NDQyMTksImlzcyI6Im1jc3VuaXR5Lm5ldCIsImF1ZCI6IlVzZXIifQ._W8jt7HuzifzEartVSCbDkVKAeFKx
    4FAy6hNuEtDYSQ",
    "authType": "Bearer",
    "expiration": "UTC:2019-11-14 3:10:19 PM"
}
```
